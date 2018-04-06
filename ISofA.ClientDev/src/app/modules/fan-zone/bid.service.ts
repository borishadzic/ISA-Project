import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { Bid } from './model/bid';
import { environment } from '../../../environments/environment';
import { UserItem } from './model/user-item';

@Injectable()
export class BidService {

    constructor(private http: HttpClient) { }

    public bid(userItemId, bid: Bid): Observable<UserItem> {
        return this.http.post<UserItem>(environment.hostUrl + '/api/useritems/' + userItemId + '/bids', bid);
    }
}
