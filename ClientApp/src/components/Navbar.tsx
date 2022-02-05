import {AppBar, Button, Link, Toolbar, Typography} from "@mui/material";
import useCurrentUser from "../hooks/useCurrentUser";
import {UserRole} from "../types/Models/UserRole";


const Navbar = () => {
    
    const currentUser = useCurrentUser();
    
    return (
        <AppBar
            position="static"
            color="default"
            elevation={0}
            sx={{ borderBottom: (theme) => `1px solid ${theme.palette.divider}` }}
        >
            <Toolbar sx={{ flexWrap: 'wrap' }}>
                <Typography variant="h6" color="inherit" noWrap sx={{ flexGrow: 1 }}>
                    Digify
                </Typography>
                <nav>
                    <Link
                        variant="button"
                        color="text.primary"
                        href="/dashboard"
                        sx={{ my: 1, mx: 1.5 }}
                    >
                        Home
                    </Link>
                    {currentUser?.roles.includes(UserRole.ADMIN) ? (
                        <Link
                            variant="button"
                            color="text.primary"
                            href="/users"
                            sx={{ my: 1, mx: 1.5 }}
                        >
                            Users
                        </Link>
                    ) : null}
                </nav>
                <Button href="/login" variant="outlined" sx={{ my: 1, mx: 1.5 }}>
                    Login
                </Button>
            </Toolbar>
        </AppBar>
    );
}

export default Navbar;