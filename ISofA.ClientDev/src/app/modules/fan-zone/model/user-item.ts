import { Bid } from './bid';

export interface UserItem {
    TheaterId: number;
    UserItemId: string;
    Name: string;
    Description: string;
    ImageUrl: string;
    HighestBid: number;
    Bids?: Bid[];
}
