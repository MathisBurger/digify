import {createContext, useContext} from "react";
import {User} from "../types/Models/User";

export const CurrentUserContext = createContext<null|User>(null);

const useCurrentUser = () => useContext(CurrentUserContext);

export default useCurrentUser;