export interface Timetable {
    table_elements: TimeTableElement[];
}

export interface TimeTableElement {
    start_time: Date;
    end_time: Date;
    day: string;
    teacher: string;
    room: string;
    subject_color: string;
    subject: string;
}