import { Component,OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ISpecialEvents } from './ISpecialEvents';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Observable, catchError, tap, throwError } from "rxjs";

@Component({

  templateUrl: './special-events.component.html',
  styleUrls: ['./services-list.component.css']
})
export class SpecialEventsComponent implements OnInit {
  pageTitle: string = "Special Events for Counseling Service"
  specialEvent: ISpecialEvents | undefined;
  private counselingUrl = 'https://localhost:7186/api/v1/CounselingServices/' + '${id}/SpecialEvent';


  constructor(private route: ActivatedRoute,
    private http: HttpClient)
    {

    }

  getCounselings(): Observable<ISpecialEvents[]> {
    return this.http.get<ISpecialEvents[]>(this.counselingUrl).pipe(
      tap(data => console.log('ALL: ', JSON.stringify(data))), catchError(this.handleError));
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

  ngOnInit(): void
    {
    const id = Number(this.route.snapshot.paramMap.get('id'))
    this.pageTitle += `: ${id}`;
    this.specialEvent = {
      'id': id,
      'name': 'Paws',
      'counselor': 'Brian Brackett',
      'description' : 'Dog walking service for local shelters'
    }
  }
  ngOnDestroy() {
    //this.sub.unsubscribe();
  }
}
