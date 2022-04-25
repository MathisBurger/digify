import useApiService from "../../hooks/useApiService";
import {useEffect, useState} from "react";
import useCurrentUser from "../../hooks/useCurrentUser";
import {UserRole} from "../../types/Models/UserRole";
import {Grid} from "@mui/material";
import ClassbookTodayView from "./ClassbookTodayView";
import {Classbook} from "../../types/Models/Classbook";
import ClassbookMissingView from "./ClassbookMissingView";
import ClassbookDayEntryNotes from "./ClassbookDayEntryNotes";


interface ClassbookDisplayProps {
    id?: string;
    editingMode?: boolean;
    selectedDate?: Date;
}

const ClassbookDisplay = ({id, editingMode, selectedDate}: ClassbookDisplayProps) => {
    
    const apiService = useApiService();
    const {user} = useCurrentUser();
    const [classbook, setClassbook] = useState<Classbook|null>(null);
    
    useEffect(() => {
        const fetcher = async () => {
            if (!user?.roles.includes(UserRole.STUDENT) && id) {
                setClassbook(await apiService.getSpecificClassbook(id));
            } else {
                setClassbook(await apiService.getClassbookForCurrentUser());
            }
        };
        fetcher();
    }, [classbook !== null]);
    
    const getSelectedDayLessons = () => {
        const requiredDate = selectedDate ?? new Date();
        const days = classbook?.dayEntries?.filter(e => (new Date(e.currentDate)).getDate() === (requiredDate).getDate());
        if (days && days.length > 0) {
            return days[0].lessons ?? []; 
        }  
        return [];
    }
    
    const getSelectedDayMissing = () => {
        const requiredDate = selectedDate ?? new Date();
        const days = classbook?.dayEntries?.filter(e => (new Date(e.currentDate)).getDate() === (requiredDate).getDate());
        if (days && days.length > 0) {
            return days[0].missing ?? [];
        }
        return [];
    }

    const getSelectedDayNotes = () => {
        const requiredDate = selectedDate ?? new Date();
        const days = classbook?.dayEntries?.filter(e => (new Date(e.currentDate)).getDate() === (requiredDate).getDate());
        if (days && days.length > 0) {
            return days[0].notes ?? '';
        }
        return '';
    }
    
    return (
        <Grid container direction="row" spacing={2}>
            <Grid item xs={6}>
                <ClassbookTodayView 
                    lessons={getSelectedDayLessons()} 
                    loading={classbook === null} 
                    classbookID={classbook?.id ?? ""}
                    editorMode={editingMode ?? false}
                />
            </Grid>
            <Grid item xs={3}>
                <ClassbookMissingView 
                    missingStudents={getSelectedDayMissing()}
                    students={classbook?.referedClass.students ?? []}
                    classbookID={classbook?.id ?? ''}
                    setClassbook={(cb: Classbook) => setClassbook(cb)}
                    editorMode={editingMode ?? false}
                />
            </Grid>
            <Grid item xs={3}>
                <ClassbookDayEntryNotes 
                    notes={getSelectedDayNotes()}
                    classbookID={classbook?.id ?? ''}
                    editorMode={editingMode ?? false} 
                />
            </Grid>
        </Grid>
    );
}

export default ClassbookDisplay;