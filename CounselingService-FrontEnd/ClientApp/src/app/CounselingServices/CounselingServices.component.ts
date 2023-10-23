import { Component } from '@angular/core';
import { ICounselingServices } from './ICounselingServices';

@Component({
  selector: 'app-CounselingServices',
  templateUrl: './CounselingServices.component.html',
  styleUrls: ['./services-list.component.css']
})
export class CounselingServicesComponent {
  pageTitle: string = 'Counseling Services';
  counselingServices: ICounselingServices[] =
    [
      {
        "counselingServiceID": 1,
        "counselingServiceName": "alcohol",
        "counselorName": "Sarah Johnson",
        "description": "Bi-weekly group, designed to support those recovering from Alcohol Addiction."
      },
      {
        "counselingServiceID": 2,
        "counselingServiceName": "gambling",
        "counselorName": "Brian Brackett",
        "description": "Bi-weekly group, designed to support those recovering from Gambling Addiction."
      },
      {
        "counselingServiceID": 3,
        "counselingServiceName": "narcotics",
        "counselorName": "Sarah Johnson",
        "description": "Bi-weekly group, designed to support those recovering from Narcotics Addiction."
      },
      {
        "counselingServiceID": 4,
        "counselingServiceName": "Test",
        "counselorName": "Test",
        "description": "Test"
      }
    ];
}
