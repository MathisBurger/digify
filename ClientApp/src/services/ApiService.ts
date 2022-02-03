import RestService from "./RestService";

const ORIGIN = process.env.NODE_ENV === "production" ? '/api' : 'http://localhost:5000';

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
}