import useApiService from "../../hooks/useApiService";
import {useEffect} from "react";
import useCurrentUser from "../../hooks/useCurrentUser";
import {UserRole} from "../../types/Models/UserRole";
import {Grid} from "@mui/material";
import ClassbookTodayView from "./ClassbookTodayView";


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
        <Grid container>
            <Grid item xs={6}>
                <ClassbookTodayView />
            </Grid>
        </Grid>
    );
}

export default ClassbookDisplay;