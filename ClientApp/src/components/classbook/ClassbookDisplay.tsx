import useApiService from "../../hooks/useApiService";
import {useEffect, useState} from "react";
import useCurrentUser from "../../hooks/useCurrentUser";
import {UserRole} from "../../types/Models/UserRole";
import {Grid} from "@mui/material";
import ClassbookTodayView from "./ClassbookTodayView";
import {Classbook} from "../../types/Models/Classbook";


const ClassbookDisplay = () => {
    
    const apiService = useApiService();
    const {user} = useCurrentUser();
    const [classbook, setClassbook] = useState<Classbook|null>(null);
    
    useEffect(() => {
        const fetcher = async () => {
            if (user?.roles.includes(UserRole.STUDENT)) {
                setClassbook(await apiService.getClassbookForCurrentUser());
            }
        };
        fetcher();
    }, [classbook !== null]);
    
    const getCurrentDayLessons = () => {
        const days = classbook?.dayEntries?.filter(e => (new Date(e.currentDate)).getDate() === (new Date()).getDate());
        if (days && days.length > 0) {
            return days[0].lessons ?? []; 
        }  
        return [];
    } 
    
    return (
        <Grid container>
            <Grid item xs={6}>
                <ClassbookTodayView lessons={getCurrentDayLessons()} loading={classbook === null} />
            </Grid>
        </Grid>
    );
}

export default ClassbookDisplay;