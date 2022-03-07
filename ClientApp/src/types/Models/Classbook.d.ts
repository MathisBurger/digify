export interface Classbook {
    archived: boolean;
    archivedDate: Date|null;
    archivedName: string|null;
    created: Date;
    dayEntries: any[];
    id: string;
    year: string;
}