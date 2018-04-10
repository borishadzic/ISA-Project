import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { UserItem } from './model/user-item';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class UserItemService {

    constructor(private http: HttpClient) { }

    public addUserItem(theaterId, userItem): Observable<UserItem> {
        return this.http.post<UserItem>(environment.hostUrl + '/api/theaters/' + theaterId + '/useritems', userItem);
    }

    public uploadImage(theaterId, userItemId, image: File): Observable<UserItem> {
        const formData = new FormData();
        formData.append('image', image, image.name);

        return this.http.post<UserItem>(environment.hostUrl + '/api/theaters/' +  theaterId + '/useritems/' + userItemId, formData);
    }

    public getItems(theaterId): Observable<UserItem[]> {
        return this.http.get<UserItem[]>(environment.hostUrl + '/api/theaters/' + theaterId + '/useritems');
    }

    public getAwaitingItems(theaterId): Observable<UserItem[]> {
        return this.http.get<UserItem[]>(environment.hostUrl + '/api/theaters/' + theaterId + '/useritems', {
            params: { status: 'awaiting' }
        });
    }

    public getItem(theaterId, userItemId): Observable<UserItem> {
        return this.http.get<UserItem>(environment.hostUrl + '/api/theaters/' +  theaterId + '/useritems/' + userItemId);
    }

    public approveItem(theaterId, userItemId): Observable<any> {
        return this.http.put<any>(environment.hostUrl + '/api/theaters/' + theaterId + '/useritems/' + userItemId, null);
    }

    public declineItem(theaterId, userItemId): Observable<any> {
        return this.http.delete<any>(environment.hostUrl + '/api/theaters/' + theaterId + '/useritems/' + userItemId);
    }
}
