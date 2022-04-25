import {AppBar, Button, Link, Toolbar, Typography} from "@mui/material";
import useCurrentUser from "../hooks/useCurrentUser";
import {UserRole} from "../types/Models/UserRole";
import {useHistory} from "react-router-dom";


/**
 * The app navbar
 */
const Navbar = () => {
    
    const {user} = useCurrentUser();
    const history = useHistory();
    
    return (
        <AppBar
            position="static"
            color="default"
            elevation={1}
            sx={{ borderBottom: (theme) => `1px solid ${theme.palette.divider}`, borderRadius: '10px' }}
        >
            <Toolbar sx={{ flexWrap: 'wrap' }}>
                <Typography variant="h6" color="inherit" noWrap sx={{ flexGrow: 1 }}>
                    Digify
                </Typography>
                <nav>
                    <Link
                        variant="button"
                        color="text.primary"
                        onClick={() => history.push('/dashboard')}
                        sx={{ my: 1, mx: 1.5 }}
                    >
                        Home
                    </Link>
                    {!user?.roles.includes(UserRole.STUDENT) ? (
                        <Link
                            variant="button"
                            color="text.primary"
                            onClick={() => history.push('/classes')}
                            sx={{ my: 1, mx: 1.5 }}
                        >
                            Classes
                        </Link>
                    ) : null}
                    <Link
                        variant="button"
                        color="text.primary"
                        onClick={() => history.push('/classbook')}
                        sx={{ my: 1, mx: 1.5 }}
                    >
                        Classbook
                    </Link>
                    {user?.roles && user?.roles.includes(UserRole.ADMIN) ? (
                        <Link
                            variant="button"
                            color="text.primary"
                            onClick={() => history.push('/user-management')}
                            sx={{ my: 1, mx: 1.5 }}
                        >
                            Users
                        </Link>
                    ) : null}
                </nav>
                <Button href="/login" variant="outlined" sx={{ my: 1, mx: 1.5 }}>
                    {user === null ? "Login" : user.username}
                </Button>
            </Toolbar>
        </AppBar>
    );
}

export default Navbar;