import { AgmMap, GoogleMapsAPIWrapper, MapsAPILoader } from '@agm/core';
import { Component, NgZone, OnInit, ViewChild } from '@angular/core';
declare var google: any;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent implements OnInit {
  geocoder: any;
  location: MapLocation;
  circleRadius: number = 5000;

  @ViewChild(AgmMap) map: AgmMap;
  constructor(
    public mapsApiLoader: MapsAPILoader,
    private zone: NgZone,
    private wrapper: GoogleMapsAPIWrapper
  ) {}

  ngOnInit(): void {
    this.location = new MapLocation(51.678418, 7.809007, 5);
    this.mapsApiLoader.load().then(() => {
      this.geocoder = new google.maps.Geocoder();
    });
  }
}
