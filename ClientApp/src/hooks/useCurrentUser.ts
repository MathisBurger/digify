import React, {createContext, useContext} from "react";
import {User} from "../types/Models/User";

type ContextType = {
    /**
     * The current logged in user
     */
    user: null|User;
    /**
     * Method used for setting the user
     */
    setUser:  React.Dispatch<React.SetStateAction<User | null>>|null;
}

/**
 * The general user context
 */
export const CurrentUserContext = createContext<ContextType>({
    user: null,
    setUser: null
});

/**
 * Fetches the current state of the user context
 */
const useCurrentUser = () => useContext(CurrentUserContext);

export default useCurrentUser;