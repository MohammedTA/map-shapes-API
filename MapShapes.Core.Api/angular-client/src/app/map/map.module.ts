import { AgmCoreModule, GoogleMapsAPIWrapper } from "@agm/core";
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MapComponent } from "./map.component";
import { MapRoutingModule } from "./map.routing.module";
import { MapShapeService } from "./services/map-shape.service";

@NgModule({
  declarations: [MapComponent],
  imports: [
    CommonModule,
    FormsModule,
    MapRoutingModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyBgAnmo7oRp5h-2P3leyHh0wsC74UZImaA',
      libraries: ['drawing'],
    }),
  ],
  providers: [GoogleMapsAPIWrapper, MapShapeService],
})
export class MapModule {}