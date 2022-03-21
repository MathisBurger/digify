import useApiService from "../../hooks/useApiService";
import {useEffect, useState} from "react";
import useCurrentUser from "../../hooks/useCurrentUser";
import {UserRole} from "../../types/Models/UserRole";
import {Grid} from "@mui/material";
import ClassbookTodayView from "./ClassbookTodayView";
import {Classbook} from "../../types/Models/Classbook";
import ClassbookMissingView from "./ClassbookMissingView";
import ClassbookDayEntryNotes from "./ClassbookDayEntryNotes";


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
    
    const getCurrentDayMissing = () => {
        const days = classbook?.dayEntries?.filter(e => (new Date(e.currentDate)).getDate() === (new Date()).getDate());
        if (days && days.length > 0) {
            return days[0].missing ?? [];
        }
        return [];
    }

    const getCurrentDayNotes = () => {
        const days = classbook?.dayEntries?.filter(e => (new Date(e.currentDate)).getDate() === (new Date()).getDate());
        if (days && days.length > 0) {
            return days[0].notes ?? '';
        }
        return '';
    }
    
    return (
        <Grid container direction="row" spacing={2}>
            <Grid item xs={6}>
                <ClassbookTodayView 
                    lessons={getCurrentDayLessons()} 
                    loading={classbook === null} 
                    classbookID={classbook?.id ?? ""}
                />
            </Grid>
            <Grid item xs={3}>
                <ClassbookMissingView 
                    missingStudents={getCurrentDayMissing()}
                    students={classbook?.referedClass.students ?? []}
                    classbookID={classbook?.id ?? ''}
                    setClassbook={(cb: Classbook) => setClassbook(cb)}
                />
            </Grid>
            <Grid item xs={3}>
                <ClassbookDayEntryNotes notes={getCurrentDayNotes()}  classbookID={classbook?.id ?? ''} />
            </Grid>
        </Grid>
    );
}

export default ClassbookDisplay;