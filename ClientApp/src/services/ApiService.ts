import RestService from "./RestService";
import {RequestUser, User} from "../types/Models/User";

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
    public async getAllClasses(): Promise<any[]> {
        return await this.get<any[]>(`${ORIGIN}/class/getAllClasses`);
    }
}