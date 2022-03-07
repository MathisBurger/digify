import {TimeTableElement} from "../../types/Models/Timetable";
import {Button, Dialog, DialogActions, DialogContent, DialogTitle, Grid, TextField} from "@mui/material";
import {useEffect, useState} from "react";
import FormSelectField from "../../components/form/FormSelectField";
import {User} from "../../types/Models/User";
import useApiService from "../../hooks/useApiService";
import {UserRole} from "../../types/Models/UserRole";
import {TimePicker} from "@mui/lab";


interface EditTimetableDialogProps {
    element: TimeTableElement;
    updateElement: (element: TimeTableElement) => void;
    onClose: () => void;
}

const EditTimetableDialog = ({element, updateElement, onClose}: EditTimetableDialogProps) => {
    
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
        <Dialog open={true} onClose={onClose}>
            <DialogTitle>Edit Entry</DialogTitle>
            <DialogContent>
                <Grid container spacing={2}>
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
                    <Grid item xs={6}>
                        <TimePicker 
                            onChange={(date) => {
                                if (date) {
                                    setEntry({...entry, start_time: date});
                                }
                            }}
                            value={new Date(entry.start_time)}
                            //openPicker={}
                            //rawValue={}
                            renderInput={(props) => <TextField {...props} />}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <TimePicker
                            onChange={(date) => {
                                if (date) {
                                    setEntry({...entry, end_time: date});
                                }
                            }}
                            value={new Date(entry.end_time)}
                            //openPicker={}
                            //rawValue={}
                            renderInput={(props) => <TextField {...props} />}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <input type="color" onChange={(e) => setEntry({...entry, subject_color: e.target.value}) } />
                    </Grid>
                </Grid>
            </DialogContent>
            <DialogActions>
                <Button
                    onClick={onClose}
                    variant="outlined"
                    color="primary"
                >
                    Close
                </Button>
                <Button
                    onClick={() => {
                        updateElement(entry);
                        onClose();
                    }}
                    variant="contained"
                    color="primary"
                >
                    Save
                </Button>
            </DialogActions>
        </Dialog>
    )
}

export default EditTimetableDialog;