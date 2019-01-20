import { Injectable } from '@angular/core';
import { BrowserXhr } from '@angular/http';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class ProgressService {
  // this class derives from Observable
  // gives an ability to push a new value into observable
  private uploadProgress: Subject<any>;

  startTracking() {
    this.uploadProgress = new Subject();
    return this.uploadProgress;
  }

  notify(progress) {
    this.uploadProgress.next(progress);
  }

  endTracking() {
    this.uploadProgress.complete();
  }
}

// To access XMLHttpRequest object in the browser, Angular uses BrowserXhr

@Injectable()
export class BrowserXhrWithProgresss extends BrowserXhr {
  // to call the ctor of the base class use - super();
  constructor(private service: ProgressService) { super(); }

  build(): XMLHttpRequest {
    const xhr: XMLHttpRequest = super.build();

    xhr.upload.onprogress = (event) => {
      this.service.notify(this.createProgress(event));
    };

    // fix memory leak
    xhr.upload.onloadend = () => {
      this.service.endTracking();
    };

    return xhr;
  }

  private createProgress(event) {
    return {
      // total number of bytes
      total: event.total,
      percentage: Math.round(event.loaded / event.total * 100)
    };
  }
}
