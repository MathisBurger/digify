import {User} from "./User";
import {Class} from "./Class";

export interface Classbook {
    archived: boolean;
    archivedDate: Date|null;
    archivedName: string|null;
    created: Date;
    dayEntries: ClassbookDayEntry[];
    id: string;
    year: string;
    referedClass: Class;
}

export interface ClassbookDayEntry {
    created: string;
    currentDate: string;
    id: string;
    lessons: ClassbookDayEntryLesson[];
    missing: any[];
    notes: string;
}

export interface ClassbookDayEntryLesson {
    approvedByTeacher: boolean;
    content: string;
    created: string;
    endTime: string;
    id: string;
    startTime: string;
    subject: string;
    subjectColor: string;
    teacher: User;
}

export interface RequestClassbookLesson {
    approvedByTeacher: boolean;
    content: string;
    id: string;
}