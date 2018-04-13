export class TimeTableDataset
{
    startDate: Date;
    groupedData: TimeTableGroupData[];
}

export class TimeTableGroupData
{
    name: string;
    data: TimeTableData[];
}

export class TimeTableData
{
    name: string;
    startMins: number;
    durationMins: number; 
}