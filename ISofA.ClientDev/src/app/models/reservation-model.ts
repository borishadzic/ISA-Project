export class ReservationModel{
    constructor(row:string, column:string) {
        this.SeatRow=row;
        this.SeatColumn=column;
        this.TheaterId = "";
        this.PlayId="";
        this.ProjectionId="";
        this.StageId="";
        this.State="-1";
        this.UserId="";
    }
    TheaterId : string;
    PlayId : string;
    StageId : string;
    ProjectionId : string;
    SeatRow : string;
    SeatColumn : string;
    State : string;
    Discount : string;
    UserId : string;
}