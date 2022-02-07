import {User} from "./User";

export interface RequestClass {
    name: string;
    teacher_ids: string[];
    student_ids: string[];
}

export interface Class {
    /**
     * The creation date
     */
    created: Date;
    /**
     * The name of the class
     */
    name: string;
    /**
     * All teachers of the class
     */
    teachers: User[];
    /**
     * All students of the class
     */
    students: User[];
}