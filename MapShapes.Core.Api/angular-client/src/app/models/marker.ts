interface Marker {
  lat: number;
  lng: number;
  label?: string;
  draggable: boolean;
}

class Marker implements Marker {
  constructor(lat: number, lng: number, draggable: boolean, label?: string) {
    this.lat = lat;
    this.lng = lng;
    this.draggable = draggable;
    this.label = label;
  }
}
