import { Injectable } from "@angular/core";
import { ICounselingServices } from "./ICounselingServices";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Observable, catchError, tap, throwError } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CounselingService {
  private counselingUrl = 'https://localhost:7186/api/v1/CounselingServices';

  constructor(private http: HttpClient) { }

  getCounselings() : Observable<ICounselingServices[]> {
    return this.http.get<ICounselingServices[]>(this.counselingUrl).pipe(
      tap(data => console.log('ALL: ', JSON.stringify(data))),catchError(this.handleError));
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = 'An error occured: ${err.errror.message}';
    }
    else { 
      errorMessage = 'Server returned code: ${err.status}, error message is ${err.message}';
    }
    console.error(errorMessage);
    return throwError(() => errorMessage);
  }
}
