import {UserRole} from "./UserRole";

export interface User {
    created: Date;
    id: string;
    roles: UserRole[];
    username: string;
}

export interface RequestUser {
    name: string;
    password: string;
    roles: UserRole[];
}