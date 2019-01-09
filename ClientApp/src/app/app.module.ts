import * as Raven from 'raven-js';
import { AppErrorHandler } from './app.error-handler';
import { VehicleService } from './services/vehicle.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { HttpModule } from '@angular/http';
import { VehicleListComponent } from './vehicle-list/vehicle-list.component';

Raven.config('https://3a70ad0b86cc45a6a39c83e3c935d461@sentry.io/1367557').install();

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    VehicleFormComponent,
    VehicleListComponent
  ],
  imports: [
    FormsModule,
    ToastyModule.forRoot(),
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
      { path: 'vehicles', component: VehicleListComponent },
      { path: 'vehicles/new', component: VehicleFormComponent },
      { path: 'vehicles/:id', component: VehicleFormComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  // dependency injection
  // we tell angular where we need an instance of ErrorHandler is should create and instance of AppErrorHandler
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    VehicleService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
