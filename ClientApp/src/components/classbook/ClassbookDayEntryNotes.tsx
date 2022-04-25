import {Button, Grid, TextField} from "@mui/material";
import {useState} from "react";
import useApiService from "../../hooks/useApiService";
import useSnackbar from "../../hooks/useSnackbar";

interface ClassbookDayEntryNotesProps {
    notes: string;
    classbookID: string;
    editorMode: boolean;
}

const ClassbookDayEntryNotes = ({notes, classbookID, editorMode}: ClassbookDayEntryNotesProps) => {

    console.log(notes);
    
    const [note, setNote] = useState<string|null>(null);
    const apiService = useApiService();
    const snackbar = useSnackbar();
    
    const updateNotes = async () => {
        try {
            await apiService.updateNotes(classbookID, note ?? notes);
            if (snackbar.setSnackbar) snackbar.setSnackbar({color: "success", message: "Successfully updated notes"});
            snackbar.openSnackbar();
        } catch (e) {
            if (snackbar.setSnackbar) snackbar.setSnackbar({color: "error", message: "Failed while saving notes"});
            snackbar.openSnackbar();
        }
    }
    
    return (
        <Grid container direction="column" spacing={2}>
            <Grid item>
                <TextField
                    multiline
                    onChange={(e) => setNote(e.target.value)}
                    value={note ?? notes}
                    sx={{width: '100%', height: '100%'}}
                    disabled={!editorMode}
                />
            </Grid>
            {editorMode ? (
                <Grid item>
                    <Button variant="contained" color="primary" onClick={() => updateNotes()}>Speichern</Button>
                </Grid>
            ) : null}
        </Grid>
    );
}

export default ClassbookDayEntryNotes;