import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { OverlayShape } from 'src/app/models/overlay-shape';
import { ApiService } from './api.service';

@Injectable()
export class MapShapeService {
  Api = '/overlay-shapes/';
  constructor(public apiService: ApiService) {}

  getAll(): Observable<OverlayShape[]> {
    return this.apiService
      .get<OverlayShape>(this.Api)
      .pipe(map((t) => t.results));
  }

  get(id: number): Observable<OverlayShape> {
    return this.apiService.get<OverlayShape>(this.Api + id);
  }

  post(entity: OverlayShape): Observable<OverlayShape> {
    return this.apiService.post(this.Api, entity);
  }
  put(id: number, entity: OverlayShape): Observable<OverlayShape> {
    return this.apiService.put(this.Api + id, entity);
  }

  delete(id: number): Observable<OverlayShape> {
    return this.apiService.delete(this.Api + id);
  }
}
