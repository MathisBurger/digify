import RestService from "./RestService";
import {RequestUser, User} from "../types/Models/User";
import {Class, RequestClass} from "../types/Models/Class";
import {Timetable, TimeTableElement} from "../types/Models/Timetable";
import {Classbook, RequestClassbookLesson} from "../types/Models/Classbook";

const ORIGIN = process.env.NODE_ENV === "production" ? '/api' : 'https://localhost:5001/api';

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
            JSON.stringify({user_id: userId, request_table_elements: elements.map(e => ({
                    ...e, teacher: e.teacher ? e.teacher.id: ''}
            ))}), true
        );
    }

    /**
     * Updates the timetable of a user
     *
     * @param classId The class thats timetable should be updated
     * @param elements All elements of the timetable
     */
    public async updateTimetableForClass(classId: string, elements: TimeTableElement[]): Promise<any> {
        return await this.post<any>(
            `${ORIGIN}/timetable/update/forClass`,
            JSON.stringify({class_id: classId, request_table_elements: elements.map(e => ({
                        ...e, teacher: e.teacher ? e.teacher.id: ''}
                ))}), true
        );
    }

    /**
     * Gets the classbook for the currently logged in student
     */
    public async getClassbookForCurrentUser(): Promise<Classbook> {
        return await this.get<Classbook>(`${ORIGIN}/classbook`);
    }

    /**
     * Updates an existing classbook lesson
     * 
     * @param classID The ID of the class
     * @param lesson The content of the lesson that should be updated
     */
    public async updateClassbookLesson(classID: string, lesson: RequestClassbookLesson): Promise<Classbook> {
        return await this.post<Classbook>(`${ORIGIN}/classbook/updateLesson`, JSON.stringify({
            id: classID,
            lesson_to_update: lesson
        }));
    }

    /**
     * Adds a missing person to the current day entry
     * 
     * @param classbookID The ID of the classbook the missing user should be added to
     * @param missingID The missing user that should be added
     */
    public async addMissingPersonToClassbook(classbookID: string, missingID: string): Promise<Classbook> {
        return await this.post<Classbook>(`${ORIGIN}/classbook/addMissing`, JSON.stringify({
            classbook_id: classbookID,
            missing_id: missingID
        }));
    }

    /**
     * Removes a missing person from the current day entry in classbook
     * 
     * @param classbookID The ID of the classbook that the user should be added to
     * @param missingID The ID of the missing student that should be removed.
     */
    public async removeMissingPersonFromClassbook(classbookID: string, missingID: string): Promise<Classbook> {
        return await this.delete<Classbook>(`${ORIGIN}/classbook/removeMissing`, JSON.stringify({
            classbook_id: classbookID,
            missing_id: missingID
        }));
    }

    /**
     * Updates the notes of the current day entry
     * 
     * @param classbookID The ID of the classbook, that contains the day entry
     * @param notes
     */
    public async updateNotes(classbookID: string, notes: string): Promise<Classbook> {
        return await this.post<Classbook>(`${ORIGIN}/classbook/updateNotes`, JSON.stringify({
            id: classbookID,
            notes: notes
        }));
    }

    /**
     * Gets a list of all classbooks a teacher has access to.
     */
    public async getClassbooksForTeacher(): Promise<Classbook[]> {
        return await this.get<Classbook[]>(`${ORIGIN}/classbook/getForTeacher`);
    }

    /**
     * Gets a specific classbook from the API
     * 
     * @param id The ID of the classbook that should be fetched
     */
    public async getSpecificClassbook(id: string): Promise<Classbook> {
        return await this.get<Classbook>(`${ORIGIN}/classbook/getSpecific/${id}`);
    }
}