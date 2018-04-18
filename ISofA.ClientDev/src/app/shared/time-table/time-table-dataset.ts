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
    $class: any;
    startMins: number;
    durationMins: number; 
}