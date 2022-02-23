import useApiService from "../../hooks/useApiService";
import {useEffect} from "react";
import useCurrentUser from "../../hooks/useCurrentUser";
import {UserRole} from "../../types/Models/UserRole";


const ClassbookDisplay = () => {
    
    const apiService = useApiService();
    const {user} = useCurrentUser();
    
    useEffect(() => {
        const fetcher = async () => {
            if (user?.roles.includes(UserRole.STUDENT)) {
                await apiService.getClassbookForCurrentUser();
            }
        };
        fetcher();
    })
    
    return (
        <div />
    );
}

export default ClassbookDisplay;