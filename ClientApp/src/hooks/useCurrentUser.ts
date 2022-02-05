import React, {createContext, useContext} from "react";
import {User} from "../types/Models/User";

type ContextType = {
    user: null|User;
    setUser:  React.Dispatch<React.SetStateAction<User | null>>|null;
}

export const CurrentUserContext = createContext<ContextType>({
    user: null,
    setUser: null
});

const useCurrentUser = () => useContext(CurrentUserContext);

export default useCurrentUser;