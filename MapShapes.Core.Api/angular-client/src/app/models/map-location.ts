import { Marker } from './marker';

export interface MapLocation {
  lat: number;
  lng: number;
  viewport?: Object;
  zoom: number;
  marker?: Marker;
}

export class MapLocation implements MapLocation {
  constructor(
    lat: number,
    lng: number,
    zoom: number,
    marker?: Marker,
    viewport?: Object
  ) {
    this.lat = lat;
    this.lng = lng;
    this.marker = marker;
    this.zoom = zoom;
  }
}
