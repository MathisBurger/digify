import {UserRole} from "./UserRole";

export interface User {
    created: Date;
    id: string;
    roles: UserRole[];
    username: string;
}