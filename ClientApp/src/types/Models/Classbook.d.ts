import {User} from "./User";

export interface Classbook {
    archived: boolean;
    archivedDate: Date|null;
    archivedName: string|null;
    created: Date;
    dayEntries: any[];
    id: string;
    year: string;
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