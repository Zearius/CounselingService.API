import { Component } from '@angular/core';

@Component({
  selector: 'app-CounselingServices',
  templateUrl: './CounselingServices.component.html'
})
export class CounselingServicesComponent {
  pageTitle: string = 'Counseling Services';
  counselingServices: any[] =
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
