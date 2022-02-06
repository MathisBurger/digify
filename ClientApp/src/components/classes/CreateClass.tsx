import {useEffect, useState} from "react";
import {User} from "../../types/Models/User";
import {RequestClass} from "../../types/Models/Class";
import useApiService from "../../hooks/useApiService";
import useSnackbar from "../../hooks/useSnackbar";
import {Button, FormControl, Grid, InputLabel, MenuItem, Select, TextField} from "@mui/material";
import {UserRole} from "../../types/Models/UserRole";
import FormSelectField from "../form/FormSelectField";


const CreateClass = () => {

    const [newClass, setNewClass] = useState<RequestClass>({name: "", teacher_ids: [], student_ids: []});
    const [teachers, setTeachers] = useState<User[]>([]);
    const [students, setStudents] = useState<User[]>([]);
    const apiService = useApiService();
    const snackbar = useSnackbar();
    
    useEffect(() => {
        const fetcher = async () => {
            const users = await apiService.allUsers();
            for (const user of users) {
                if (user.roles.includes(UserRole.STUDENT) && !students.includes(user)) {
                    setStudents([...students, user]);
                } else if (user.roles.includes(UserRole.TEACHER) && !teachers.includes(user)) {
                    setTeachers([...teachers, user]);
                }
            }
        }
        fetcher();
    }, []);
    
    const onSave = async () => {
        try {
            await apiService.createClass(newClass);
            if (snackbar.setSnackbar) snackbar.setSnackbar({color: "success", message: "Successfully created class"});
            snackbar.openSnackbar();
        } catch (e) {
            if (snackbar.setSnackbar) snackbar.setSnackbar({color: "error", message: "Cannot create class"});
            snackbar.openSnackbar();
        }
    }
    
    return (
        <Grid container xs={12} direction="row" spacing={2}>
            <Grid item xs={6}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    id="username"
                    label="Username"
                    name="username"
                    autoComplete="username"
                    autoFocus
                    value={newClass.name}
                    onChange={(e) => setNewClass({...newClass, name: e.target.value}) }
                />
            </Grid>
            <FormSelectField 
                xs={6} 
                value={newClass.teacher_ids} 
                multiple
                onChange={(e) => setNewClass({...newClass, teacher_ids: e.target.value as string[]})} 
                label="Teachers" 
                options={teachers.map(teacher => ({key: teacher.id, value: teacher.username}))} 
            />
            <FormSelectField
                xs={6}
                value={newClass.student_ids}
                multiple
                onChange={(e) => setNewClass({...newClass, student_ids: e.target.value as string[]})}
                label="Students"
                options={students.map(student => ({key: student.id, value: student.username}))}
            />
            <Grid item container xs={12}>
                <Grid item>
                    <Button
                        color="primary"
                        variant="contained"
                        onClick={onSave}
                    >
                        Create
                    </Button>
                </Grid>
            </Grid>
        </Grid>
    );
}

export default CreateClass;