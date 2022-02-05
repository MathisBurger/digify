import {PropsWithChildren, useEffect, useState} from "react";
import {CurrentUserContext} from "../hooks/useCurrentUser";
import {User} from "../types/Models/User";
import useApiService from "../hooks/useApiService";

/**
 * wrapping component that provides multiple contexts 
 * for handling app scoped states.
 */
const DataProvider = ({children}: PropsWithChildren<any>) => {
    
    const apiService = useApiService();
    const [currentUser, setCurrentUser] = useState<null|User>(null);
    
    useEffect( () => {
        const fetcher = async () => {
            const user = await apiService.me();
            setCurrentUser(user);
        }
        fetcher();
    }, []);
    
    return (
        <CurrentUserContext.Provider value={{
            user: currentUser,
            setUser: setCurrentUser
        }}>
            {children}
        </CurrentUserContext.Provider>
    )
}

export default DataProvider;