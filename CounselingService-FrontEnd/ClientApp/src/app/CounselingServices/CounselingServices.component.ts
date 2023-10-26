import { Component, OnDestroy } from '@angular/core';
import { ICounselingServices } from './ICounselingServices';
import { CounselingService } from './counseling.service';
import { OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  templateUrl: './CounselingServices.component.html',
  styleUrls: ['./services-list.component.css']
})
export class CounselingServicesComponent implements OnInit, OnDestroy {
  pageTitle: string = 'Counseling Services';
  filteredCounselingServices: ICounselingServices[] = [];
  counseling: ICounselingServices[] = [];
  errorMessage: string = '';
  sub!: Subscription;

  private _listFilter: string = '';
  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    console.log('In setter:', value);
    this.filteredCounselingServices = this.performFilter(value);
  }

  performFilter(filterby: string): ICounselingServices[] {
    filterby = filterby.toLocaleLowerCase();
    return this.counseling.filter((counseling: ICounselingServices) =>
      counseling.name.toLocaleLowerCase().includes(this.listFilter));
  }


  constructor(private counselingService: CounselingService) { }

  ngOnInit(): void {
    this.sub = this.counselingService.getCounselings().subscribe(
 {
        next: counseling => {
      this.counseling = counseling;
      this.filteredCounselingServices = this.counseling;
        },
        error: err => this.errorMessage = err
      });
  }
  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
