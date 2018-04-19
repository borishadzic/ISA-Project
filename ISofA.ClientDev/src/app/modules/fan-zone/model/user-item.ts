import { Bid } from './bid';

export interface UserItem {
    TheaterId: number;
    UserItemId: string;
    UserId: string;
    Name: string;
    Description: string;
    ImageUrl: string;
    HighestBid: number;
    Sold: boolean;
    ExpirationDate: string;
    Approved: boolean;
    Bids?: Bid[];
}
