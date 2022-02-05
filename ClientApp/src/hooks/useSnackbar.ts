import {createContext, Dispatch, SetStateAction, useContext} from "react";
import {AlertProps, SnackbarProps} from "@mui/material";

export type SnackbarValues = {
    color: AlertProps['severity'],
    message: SnackbarProps['message']
}

type ContextType = {
    snackbarValues: SnackbarValues|null,
    setSnackbar: Dispatch<SetStateAction<SnackbarValues|null>>|null;
    openSnackbar: () => void;
}


export const SnackbarContext = createContext<ContextType>({
    snackbarValues: {
        color: "success",
        message: "message"
    },
    setSnackbar: null,
    openSnackbar: () => {}
});

const useSnackbar = () => useContext(SnackbarContext);

export default useSnackbar;