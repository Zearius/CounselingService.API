import { Component,OnInit } from '@angular/core';

@Component({

  templateUrl: './special-events.component.html',
  styleUrls: ['./services-list.component.css']
})
export class SpecialEventsComponent implements OnInit {
  pageTitle: string = "Special Events"

  constructor()
    {

    }

  ngOnInit(): void
    {

  }
  ngOnDestroy() {
    //this.sub.unsubscribe();
  }
}
