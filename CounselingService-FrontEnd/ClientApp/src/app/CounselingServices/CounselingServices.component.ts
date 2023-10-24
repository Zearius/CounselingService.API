import { Component, OnDestroy } from '@angular/core';
import { ICounselingServices } from './ICounselingServices';
import { CounselingService } from './counseling.service';
import { OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-CounselingServices',
  templateUrl: './CounselingServices.component.html',
  styleUrls: ['./services-list.component.css']
})
export class CounselingServicesComponent implements OnInit, OnDestroy {
  pageTitle: string = 'Counseling Services';
  filteredCounselingServices: ICounselingServices[] = [];
  counselings: ICounselingServices[] = [];
  errorMessage: string = '';
  sub!: Subscription;

  private _listFilter: string = '';
  get listFIlter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredCounselingServices = this.performFilter(value);
  }

  performFilter(filterby: string): ICounselingServices[] {
    filterby = filterby.toLocaleLowerCase();
    return this.counselings.filter((counselingServices: ICounselingServices) =>
      counselingServices.name.toLocaleLowerCase().includes(this.listFilter));
  }


  constructor(private counselingService: CounselingService) { }

  ngOnInit(): void {
    this.sub = this.counselingService.getCounselings().subscribe(
      {
        next: counselings => {
          this.counselings = counselings;
          this.filteredCounselingServices = this.counselings;
        },
        error: err => this.errorMessage = err
      });
  }
  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
