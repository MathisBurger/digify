﻿import {PropsWithChildren, useEffect, useState} from "react";
import {CurrentUserContext} from "../hooks/useCurrentUser";
import {User} from "../types/Models/User";
import useApiService from "../hooks/useApiService";

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
        <CurrentUserContext.Provider value={currentUser}>
            {children}
        </CurrentUserContext.Provider>
    )
}

export default DataProvider;