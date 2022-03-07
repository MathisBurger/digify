import {User} from "./User";

export interface Timetable {
    table_elements: TimeTableElement[];
}

export interface TimeTableElement {
    start_time: Date;
    end_time: Date;
    day: string;
    teacher?: User;
    room: string;
    subject_color: string;
    subject: string;
    id?: string;
}