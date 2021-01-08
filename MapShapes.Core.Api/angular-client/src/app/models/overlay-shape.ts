export interface OverlayShape {
  id: number;
  title: string;
  properties: string;
  type: string;
}

export class OverlayShape implements OverlayShape {
  constructor(type: string, title: string, properties: string, id: number = 0) {
    this.title = title;
    this.properties = properties;
    this.id = id;
    this.type = type;
  }
}