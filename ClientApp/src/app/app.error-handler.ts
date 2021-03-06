import * as Raven from 'raven-js';
import { ToastyService } from 'ng2-toasty';
import { ErrorHandler, Inject, NgZone, isDevMode } from '@angular/core';

// Global error handling code
export class AppErrorHandler implements ErrorHandler {
    // this class is initialized before we import ToastyModule
    // and angular doesn't know how to inject an instance of ToasyService
    // to solve this problem - we use @Inject()
    constructor(
        private ngZone: NgZone,
        @Inject(ToastyService) private toastyService: ToastyService) {}

    handleError(error: any): void {
        this.ngZone.run(() => {
            this.toastyService.error({
                title: 'error',
                msg: 'An unexpected error happened.',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
              });
        });

        if (!isDevMode()) {
            Raven.captureException(error.originalError || error);
        } else {
            throw error;
        }
    }
}
