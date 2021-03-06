
import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import useApiService from "../hooks/useApiService";
import {useHistory} from "react-router-dom";
import {Alert, Card, CardContent, Grid, Snackbar} from "@mui/material";
import {useState} from "react";
import useCurrentUser, {CurrentUserContext} from "../hooks/useCurrentUser";

/**
 * Login page
 */
const LoginPage = () => {
    
    const apiService = useApiService();
    const {setUser} = useCurrentUser();
    const [snackbarOpen, setSnackbarOpen] = useState<boolean>(false);
    const history = useHistory();

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        try {
            await apiService.login(data.get('username') as string, data.get('password') as string);
            if (setUser) {
                setUser(await apiService.me());
            }
            history.push('/dashboard');
        } catch (e) {
            setSnackbarOpen(true);
        }
    };
    
    return (
        <>
            <Grid container justifyContent="center" alignItems="center" minHeight="100vh">
                <Grid item>
                    <Card elevation={2}>
                        <CardContent>
                            <Box
                                sx={{
                                    display: 'flex',
                                    flexDirection: 'column',
                                    alignItems: 'center',
                                }}
                            >
                                <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                                    <LockOutlinedIcon />
                                </Avatar>
                                <Typography component="h1" variant="h5">
                                    Sign in
                                </Typography>
                                <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                                    <TextField
                                        margin="normal"
                                        required
                                        fullWidth
                                        id="username"
                                        label="Username"
                                        name="username"
                                        autoComplete="username"
                                        autoFocus
                                    />
                                    <TextField
                                        margin="normal"
                                        required
                                        fullWidth
                                        name="password"
                                        label="Password"
                                        type="password"
                                        id="password"
                                        autoComplete="current-password"
                                    />
                                    <Button
                                        type="submit"
                                        fullWidth
                                        variant="contained"
                                        sx={{ mt: 3, mb: 2 }}
                                    >
                                        Sign In
                                    </Button>
                                </Box>
                            </Box>
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
            <Snackbar open={snackbarOpen} autoHideDuration={6000}>
                <Alert onClose={() => setSnackbarOpen(false)} severity="error" sx={{ width: '100%' }}>
                    Login failed
                </Alert>
            </Snackbar>
        </>
    );
}

export default LoginPage;