import { Bid } from './bid';

export interface UserItem {
    UserItemId: string;
    Name: string;
    Description: string;
    ImageUrl: string;
    HighestBid: number;
    Bids: Bid[];
}
