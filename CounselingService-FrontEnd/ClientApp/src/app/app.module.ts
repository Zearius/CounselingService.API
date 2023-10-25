import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CounselingServicesComponent } from './CounselingServices/CounselingServices.component'
import { SpecialEventsComponent } from './CounselingServices/special-events.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    CounselingServicesComponent,
    SpecialEventsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'CounselingServices', component: CounselingServicesComponent },
      { path: 'special-events', component: SpecialEventsComponent },
      { path: 'special-events/:id', component: SpecialEventsComponent },
      { path: 'home', component: HomeComponent },
      { path: '', redirectTo: 'CounselingServices', pathMatch: 'full' },
      { path: '**', redirectTo: 'CounselingServices', pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
