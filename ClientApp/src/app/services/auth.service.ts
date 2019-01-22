import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { filter } from 'rxjs/operators';
import Auth0Lock from 'auth0-lock';

@Injectable()
export class AuthService {

  lock = new Auth0Lock('yFdbxDTvnJFwe11nKMfciCibPE9hHwRS', 'vincentproject.eu.auth0.com', {
    autoclose: true,
    auth: {
      redirectUrl: 'https://localhost:5001/callback',
      responseType: 'token id_token',
      params: {
        scope: 'openid'
      }
    }
  });

  constructor(public router: Router) {}

  public login(): void {
    this.lock.show();
  }

  // Call this method in app.component.ts
  // if using path-based routing
  public handleAuthentication(): void {
    this.lock.on('authenticated', (authResult) => {
      if (authResult && authResult.accessToken && authResult.idToken) {
        this.setSession(authResult);
        this.router.navigate(['/']);
      }
    });
    this.lock.on('authorization_error', (err) => {
      this.router.navigate(['/']);
      console.log(err);
      alert(`Error: ${err.error}. Check the console for further details.`);
    });
  }

  // Call this method in app.component.ts
  // if using hash-based routing
  public handleAuthenticationWithHash(): void {
    this
      .router
      .events
      .pipe(
        filter(event => event instanceof NavigationStart),
        filter((event: NavigationStart) => (/access_token|id_token|error/).test(event.url))
      )
      .subscribe(() => {
        this.lock.resumeAuth(window.location.hash, (err, authResult) => {
          if (err) {
            this.router.navigate(['/']);
            console.log(err);
            alert(`Error: ${err.error}. Check the console for further details.`);
            return;
          }
          this.setSession(authResult);
          this.router.navigate(['/']);
        });
    });
  }

  private setSession(authResult): void {
    // Set the time that the access token will expire at
    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
    localStorage.setItem('access_token', authResult.accessToken);
    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', expiresAt);
  }

  public logout(): void {
    // Remove tokens and expiry time from localStorage
    localStorage.removeItem('access_token');
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
    // Go back to the home route
    this.router.navigate(['/']);
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // access token's expiry time
    const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
    return new Date().getTime() < expiresAt;
  }

}