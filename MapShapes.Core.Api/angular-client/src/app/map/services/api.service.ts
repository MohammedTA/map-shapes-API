import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpParams,
  HttpHeaders,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  env = environment.apiUrl;
  constructor(public http: HttpClient) {}

  get<T>(
    path: string,
    params: HttpParams = new HttpParams(),
    observe: any = 'body',
    type?
  ): Observable<any> {
    return this.http
      .get<T>(`${this.env}${path}`, {
        observe,
        params,
        headers: this.getHeaders(),
        responseType: type,
      })
      .pipe(
        map((e) => {
          return e;
        }),
        catchError(this.formatErrors)
      );
  }

  put(path: string, body: Object = {}): Observable<any> {
    return this.http
      .put(`${this.env}${path}`, JSON.stringify(body), {
        headers: this.getHeaders(),
      })
      .pipe(
        map((e) => {
          return e;
        }),
        catchError(this.formatErrors)
      );
  }

  post(path: string, body: Object = {}, param?): Observable<any> {
    return this.http
      .post(`${this.env}${path}`, body, {
        headers: this.getHeaders(),
        params: param,
      })
      .pipe(
        map((e) => {
          return e;
        }),
        catchError(this.formatErrors)
      );
  }

  delete(path): Observable<any> {
    return this.http
      .delete(`${this.env}${path}`, { headers: this.getHeaders() })
      .pipe(
        map((e) => {
          return e;
        }),
        catchError(this.formatErrors)
      );
  }

  getHeaders() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
    });
    return headers;
  }

  private formatErrors(error: HttpErrorResponse) {
    console.log(error);
    return throwError(error.error);
  }
}
