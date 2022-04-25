import {Button, Dialog, DialogActions, DialogContent, DialogTitle} from "@mui/material";
import {useState} from "react";
import {Classbook} from "../../types/Models/Classbook";
import useApiService from "../../hooks/useApiService";

interface MissingUserClickedDialogProps {
    close: () => void;
    userID: string;
    classbookID: string;
    setClassbook: (classbook: Classbook) => void;
    editorMode: boolean;
}

const MissingUserClickedDialog = ({close, userID, setClassbook, classbookID, editorMode}: MissingUserClickedDialogProps) => {
    
    const apiService = useApiService();
    
    const removeMissing = async () => {
        const classbook = await apiService.removeMissingPersonFromClassbook(classbookID, userID);
        setClassbook(classbook);
        close();
    }
    
    return (
                <Dialog open fullWidth onClose={close}>
                    <DialogTitle>Missing User</DialogTitle>
                    <DialogContent>
                        Following actions are possible for selected user.
                    </DialogContent>
                    <DialogActions>
                        <Button color="primary" variant="contained" disabled>User profile</Button>
                        {editorMode ? <Button color="primary" variant="contained" onClick={removeMissing}>Remove Missing</Button> : null}
                    </DialogActions>
                </Dialog>
    );
}

export default MissingUserClickedDialog;