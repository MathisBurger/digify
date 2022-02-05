import {useEffect, useState} from "react";
import {SnackbarValues} from "../hooks/useSnackbar";
import {SnackbarContext} from "../hooks/useSnackbar";
import {Alert} from "@mui/material";
import * as React from "react";
import {Snackbar as MuiSnackbar} from "@mui/material";


const Snackbar = ({children}: React.PropsWithChildren<any>) => {
    
    const [snackbarOpen, setSnackbarOpen] = useState<boolean>(true);
    const [snackbarValues, setSnackbarValues] = useState<SnackbarValues|null>(null);
    
    useEffect(() => {
        setTimeout(() => setSnackbarOpen(false), 2000);
    }, [snackbarOpen]);
    
    return (
        <SnackbarContext.Provider value={{snackbarValues, setSnackbar: setSnackbarValues, openSnackbar: () => setSnackbarOpen(true)}}>
            {children}
            <MuiSnackbar open={snackbarOpen}>
                <Alert onClose={() => setSnackbarOpen(false)} severity={snackbarValues?.color} sx={{ width: '100%' }}>
                    {snackbarValues?.message}
                </Alert>
            </MuiSnackbar>
        </SnackbarContext.Provider>
    )
}

export default Snackbar;