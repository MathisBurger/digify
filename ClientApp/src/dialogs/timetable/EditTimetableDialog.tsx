import {TimeTableElement} from "../../types/Models/Timetable";
import {Dialog, DialogContent, DialogTitle, Grid, TextField} from "@mui/material";
import {useEffect, useState} from "react";
import FormSelectField from "../../components/form/FormSelectField";
import {User} from "../../types/Models/User";
import useApiService from "../../hooks/useApiService";
import {UserRole} from "../../types/Models/UserRole";
import {TimePicker} from "@mui/lab";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import AdapterDateFns from "@mui/lab/LocalizationProvider";


interface EditTimetableDialogProps {
    element: TimeTableElement;
    updateElement: (element: TimeTableElement) => void;
}

const EditTimetableDialog = ({element, updateElement}: EditTimetableDialogProps) => {
    
    const [entry, setEntry] = useState<TimeTableElement>(element);
    const [teachers, setTeachers] = useState<User[]>([]);
    const apiService = useApiService();
    
    useEffect(() => {
        const fetcher = async () => {
            const users = await apiService.allUsers();
            for (const user of users) {
                if (user.roles.includes(UserRole.TEACHER) && !teachers.includes(user)) {
                    setTeachers([...teachers, user]);
                }
            }
        }
        fetcher();
    }, []);
    
    return (
        <Dialog open={true}>
            <DialogTitle>Edit Entry</DialogTitle>
            <DialogContent>
                <Grid container>
                    <Grid item xs={12}>
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="subject"
                            label="Subject"
                            name="subject"
                            autoComplete="subject"
                            autoFocus
                            value={entry.subject}
                            onChange={(e) => setEntry({...entry, subject: e.target.value}) }
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="room"
                            label="Room"
                            name="room"
                            autoComplete="room"
                            autoFocus
                            value={entry.room}
                            onChange={(e) => setEntry({...entry, room: e.target.value}) }
                        />
                    </Grid>
                    <FormSelectField 
                        xs={12} 
                        value={entry.teacher} 
                        multiple={false} 
                        onChange={(e) => setEntry({...entry, teacher: '' + e.target.value})} 
                        label="Teacher"
                        options={teachers.map(teacher => ({key: teacher.username, value: teacher.username}))}
                    />
                </Grid>
            </DialogContent>
        </Dialog>
    )
}

export default EditTimetableDialog;