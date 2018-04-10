import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

import { Item } from './model/item';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ItemService {

  constructor(private http: HttpClient) { }

  public getItems(theaterId): Observable<Item[]> {
    return this.http.get<Item[]>(environment.hostUrl + '/api/theaters/' + theaterId + '/items');
  }

  public getItem(theaterId, itemId): Observable<Item> {
    return this.http.get<Item>(environment.hostUrl + '/api/theaters/' + theaterId + '/items/' + itemId);
  }

  public addItem(theaterId, item: Item): Observable<Item> {
    return this.http.post<Item>(environment.hostUrl + '/api/theaters/' + theaterId + '/items', item);
  }

  public uploadImage(theaterId, itemId, image: File): Observable<Item> {
    const formData = new FormData();
    formData.append('image', image, image.name);

    return this.http.post<Item>(environment.hostUrl + '/api/theaters/' + theaterId + '/items/' + itemId, formData);
  }

  public updateItem() {

  }

  public deleteItem() {

  }

}
