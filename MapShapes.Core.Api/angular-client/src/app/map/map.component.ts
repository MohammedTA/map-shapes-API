import { AgmMap, GoogleMapsAPIWrapper, MapsAPILoader } from '@agm/core';
import { Component, NgZone, OnInit, ViewChild } from '@angular/core';
import { MapLocation } from 'src/app/models/map-location';
import { OverlayShape } from '../models/overlay-shape';
import { MapShapeService } from './services/map-shape.service';
declare var google: any;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.scss'],
})
export class MapComponent implements OnInit {
  geocoder: any;
  location: MapLocation;
  overlayShapes: OverlayShape[] = [];
  overlayShape = new OverlayShape('', '', '');
  circleRadius = 5000;
  isMapLoaded = false;
  googleShapes = [];
  map;
  isDisabled = false;
  constructor(
    public mapsApiLoader: MapsAPILoader,
    public zone: NgZone,
    public wrapper: GoogleMapsAPIWrapper,
    public mapShapeService: MapShapeService
  ) {}

  ngOnInit(): void {
    this.location = new MapLocation(51.678418, 7.809007, 10);
    this.mapsApiLoader.load().then(() => {
      this.geocoder = new google.maps.Geocoder();
    });
    this.getShapes();
  }

  getShapes() {
    this.mapShapeService.getAll().subscribe((t) => {
      this.overlayShapes = [];
      this.overlayShapes = t;
    });
  }

  buildShapes() {
    if (this.isMapLoaded && this.overlayShapes.length > 0) {
      this.cleanShapes();
      this.overlayShapes.forEach((element) => {
        const data = JSON.parse(element.properties);
        if (element.type === 'Circle') {
          const circle = new google.maps.Circle({
            strokeColor: '#FF0000',
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: '#FF0000',
            fillOpacity: 0.35,
            map: this.map,
            center: data.center,
            radius: data.radius,
            id: element.id,
          });
          this.googleShapes.push(circle);
        } else if (element.type === 'Rectangle') {
          const rec = new google.maps.Rectangle({
            strokeColor: '#FF0000',
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: '#FF0000',
            fillOpacity: 0.35,
            map: this.map,
            bounds: data.bounds,
            id: element.id,
          });
          this.googleShapes.push(rec);
        } else {
          const Polyline = new google.maps.Polyline({
            strokeColor: '#FF0000',
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: '#FF0000',
            fillOpacity: 0.35,
            map: this.map,
            path: data.path,
            id: element.id,
          });
          this.googleShapes.push(Polyline);
        }
      });
    }
  }

  cleanShapes() {
    this.googleShapes.forEach((element) => {
      element.setMap(null);
    });
    this.googleShapes = [];
  }

  onMapReady(map) {
    this.map = map;
    this.isMapLoaded = true;
    this.buildShapes();
  }

  selectShape(item) {
      this.googleShapes.forEach((element) => {
        if (element.id === item.id) {
          element.setOptions({
            strokeWeight: 4,
            fillOpacity: 0.85,
          });
        } else {
          element.setOptions({
            strokeWeight: 2,
            fillOpacity: 0.35,
          });
        }
      });
  }

  drawingManager() {
    if (this.overlayShape.type === '' || this.overlayShape.title === '') {
      return;
    }
    if (this.isMapLoaded) {
      this.isDisabled = true;
      const options = {
        drawingControl: false,
        drawingMode: google.maps.drawing.OverlayType[this.overlayShape.type],
        polylineOptions: {
          strokeColor: '#FF0000',
          strokeOpacity: 0.8,
          strokeWeight: 2,
          fillColor: '#FF0000',
          fillOpacity: 0.35,
        },
        circleOptions: {
          strokeColor: '#FF0000',
          strokeOpacity: 0.8,
          strokeWeight: 2,
          fillColor: '#FF0000',
          fillOpacity: 0.35,
        },
        rectangleOptions: {
          strokeColor: '#FF0000',
          strokeOpacity: 0.8,
          strokeWeight: 2,
          fillColor: '#FF0000',
          fillOpacity: 0.35,
        },
      };

      const drawingManager = new google.maps.drawing.DrawingManager(options);
      drawingManager.setMap(this.map);
      google.maps.event.addListener(
        drawingManager,
        'overlaycomplete',
        (event) => {
          this.isDisabled = false;
          drawingManager.setDrawingMode(null);
          this.overlayShape.properties = JSON.stringify(
            this.getShapeDate(event)
          );
          this.mapShapeService.post(this.overlayShape).subscribe((t) => {
            this.overlayShape = new OverlayShape('', '', '');
            this.mapShapeService.getAll().subscribe((t) => {
              this.overlayShapes = [];
              this.overlayShapes = t;
              this.buildShapes();
            });
          });
        }
      );
    }
  }

  getShapeDate(event: any) {
    if (event.type === google.maps.drawing.OverlayType.CIRCLE) {
      return {
        center: {
          lat: event.overlay.getCenter().lat(),
          lng: event.overlay.getCenter().lng(),
        },
        radius: event.overlay.getRadius(),
      };
    } else if (event.type === google.maps.drawing.OverlayType.RECTANGLE) {
      return {
        bounds: event.overlay.getBounds().toJSON(),
      };
    } else {
      return {
        path: event.overlay
          .getPath()
          .getArray()
          .map((element) => {
            return {
              lat: element.lat(),
              lng: element.lng(),
            };
          }),
      };
    }
  }
}
