import {Button, Grid, MenuItem, Select, TextField} from "@mui/material";
import {UserRole} from "../../types/Models/UserRole";
import {useState} from "react";
import {RequestUser} from "../../types/Models/User";
import useApiService from "../../hooks/useApiService";
import useSnackbar from "../../hooks/useSnackbar";


/**
 * Window that is used for creating a new user in the system
 */
const CreateUser = () => {
    
    const [user, setUser] = useState<RequestUser>({name: "", password: "", roles: []});
    const apiService = useApiService();
    const snackbar = useSnackbar();
    
    const onSave = async () => {
        try {
            await apiService.createUser(user);
            if (snackbar.setSnackbar) snackbar.setSnackbar({color: "success", message: "Successfully created user"});
            snackbar.openSnackbar();
        } catch (e: any) {
            if (snackbar.setSnackbar) snackbar.setSnackbar({color: "error", message: "Cannot create user"});
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
                    value={user.name}
                    onChange={(e) => setUser({...user, name: e.target.value}) }
                />
            </Grid>
            <Grid item xs={6}>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    id="password"
                    label="Password"
                    name="password"
                    type="password"
                    autoFocus
                    value={user.password}
                    onChange={(e) => setUser({...user, password: e.target.value}) }
                />
            </Grid>
            <Grid item xs={6}>
                <Select
                    label="Roles"
                    name="roles"
                    id="roles"
                    value={user.roles}
                    multiple
                    onChange={(e) => setUser({...user, roles: e.target.value as UserRole[]})}
                >
                    {Object.values(UserRole).map((value, index) => (
                        <MenuItem value={value} key={index}>{value}</MenuItem>
                    ))}   
                </Select>
            </Grid>
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

export default CreateUser;