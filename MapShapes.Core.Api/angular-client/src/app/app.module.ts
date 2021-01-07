import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MapComponent } from './map/map.component';
import { AgmCoreModule, GoogleMapsAPIWrapper } from "@agm/core";
import { FormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";

@NgModule({
  declarations: [AppComponent, MapComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBgAnmo7oRp5h-2P3leyHh0wsC74UZImaA',
    }),
    FormsModule,
    NgbModule
  ],
  providers: [GoogleMapsAPIWrapper],
  bootstrap: [AppComponent],
})
export class AppModule {}
