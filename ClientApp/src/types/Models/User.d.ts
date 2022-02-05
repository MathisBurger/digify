import {UserRole} from "./UserRole";

/**
 * User
 */
export interface User {
    /**
     * Creation date of the user
     */
    created: Date;
    /**
     * The ID of the user
     */
    id: string;
    /**
     * All roles of the user
     */
    roles: UserRole[];
    /**
     * The username of the user
     */
    username: string;
}

/**
 * User that is used in a request
 */
export interface RequestUser {
    /**
     * The name of the user
     */
    name: string;
    /**
     * The password of the user
     */
    password: string;
    /**
     * The roles of the user
     */
    roles: UserRole[];
}