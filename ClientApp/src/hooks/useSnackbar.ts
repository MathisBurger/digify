import {createContext, Dispatch, SetStateAction, useContext} from "react";
import {AlertProps, SnackbarProps} from "@mui/material";

export type SnackbarValues = {
    /**
     * The color of the snackbar
     */
    color: AlertProps['severity'],
    /**
     * The message that is displayed
     */
    message: SnackbarProps['message']
}

type ContextType = {
    /**
     * All values of the context
     */
    snackbarValues: SnackbarValues|null,
    /**
     * Method used to update the snackbar values
     */
    setSnackbar: Dispatch<SetStateAction<SnackbarValues|null>>|null;
    /**
     * Opens the snackbar
     */
    openSnackbar: () => void;
}

/**
 * The context of the snackbar
 */
export const SnackbarContext = createContext<ContextType>({
    snackbarValues: {
        color: "success",
        message: "message"
    },
    setSnackbar: null,
    openSnackbar: () => {}
});

/**
 * The data of the context
 */
const useSnackbar = () => useContext(SnackbarContext);

export default useSnackbar;