import RestService from "./RestService";
import {RequestUser, User} from "../types/Models/User";
import {Class, RequestClass} from "../types/Models/Class";
import {Timetable, TimeTableElement} from "../types/Models/Timetable";

const ORIGIN = process.env.NODE_ENV === "production" ? '/api' : 'https://localhost:5001';

/**
 * General API service that wraps all methods into functions
 */
export default class APIService extends RestService {

    /**
     * Logs in the user.
     * 
     * @param username The username of the logged in user
     * @param password The password of the logged in user
     */
    public async login(username: string, password: string): Promise<any> {
        return await this.post<any>(`${ORIGIN}/auth/login`, JSON.stringify({
            login_name: username,
            password: password
        }), true);
    }

    /**
     * Fetches the current user
     */
    public async me(): Promise<User> {
        return await this.get<User>(`${ORIGIN}/user/me`);
    }

    /**
     * Fetches all users the user has access to
     */
    public async allUsers(): Promise<User[]> {
        return await this.get<User[]>(`${ORIGIN}/user/allUsers`);
    }

    /**
     * Creates a new user in the system.
     * 
     * @param user The new user
     */
    public async createUser(user: RequestUser): Promise<User> {
        return await this.post<User>(`${ORIGIN}/user/create`, JSON.stringify(user));
    }

    /**
     * Fetches all classes.
     */
    public async getAllClasses(): Promise<Class[]> {
        return await this.get<Class[]>(`${ORIGIN}/class/getAllClasses`);
    }

    /**
     * Creates a new class.
     *
     * @param newClass The class that should be created
     */
    public async createClass(newClass: RequestClass): Promise<Class> {
        return await this.post<Class>(`${ORIGIN}/class/createClass`, JSON.stringify(newClass));
    }

    /**
     * Deletes a class from the system
     * 
     * @param id The ID of the class that should be deleted
     */
    public async deleteClass(id: string): Promise<any> {
        return await this.delete<any>(`${ORIGIN}/class/deleteClass/${id}`);
    }

    /**
     * Deletes a user from the system
     * 
     * @param id The ID of the user that should be deleted
     */
    public async deleteUser(id: string): Promise<any> {
        return await this.delete<any>(`${ORIGIN}/user/delete/${id}`);
    }

    /**
     * Gets the current timetable of the user
     */
    public async getTimetable(): Promise<Timetable> {
        return await this.get<Timetable>(`${ORIGIN}/timetable/get`);
    }

    /**
     * Gets a timetable by action.
     * 
     * @param userId The ID of the user 
     * @param classId The ID of the class that should be used
     */
    public async actionGetTimetable(userId?: string, classId?: string): Promise<Timetable> {
        let queryParam = '';
        queryParam = userId ? 'user_id' : queryParam;
        queryParam = classId ? 'class_id' : queryParam;
        return await this.get<Timetable>(`${ORIGIN}/timetable/actionGet?${queryParam}=${userId ?? classId}`);
    }

    /**
     * Updates the timetable of a user
     * 
     * @param userId The user thats timetable should be updated
     * @param elements All elements of the timetable
     */
    public async updateTimetableForUser(userId: string, elements: TimeTableElement[]): Promise<Timetable> {
        return await this.post<Timetable>(
            `${ORIGIN}/timetable/update/forUser`, 
            JSON.stringify({user_id: userId, request_table_elements: elements})
        );
    }
}