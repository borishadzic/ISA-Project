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

  public getItem(itemId): Observable<Item> {
    return this.http.get<Item>(environment.hostUrl + '/api/items/' + itemId);
  }

  public addItem(theaterId, item: Item): Observable<Item> {
    return this.http.post<Item>(environment.hostUrl + '/api/theaters/' + theaterId + '/items', item);
  }

  public uploadImage(itemId, formData: FormData): Observable<Item> {
    return this.http.post<Item>(environment.hostUrl + '/api/items/' + itemId, formData);
  }

}
