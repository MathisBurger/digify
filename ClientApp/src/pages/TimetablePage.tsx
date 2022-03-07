import PageLayout from "../components/PageLayout";
import useApiService from "../hooks/useApiService";
import {useEffect, useState} from "react";
import {Add, ChevronRight, Edit, ExpandMore} from "@mui/icons-material";
import {Timetable, TimeTableElement} from "../types/Models/Timetable";
import {TreeView} from "@mui/lab";
import {Button, CircularProgress, Grid} from "@mui/material";
import ModifiedTreeItem from "../components/ModifiedTreeItem";
import EditTimetableDialog from "../dialogs/timetable/EditTimetableDialog";
import useSnackbar from "../hooks/useSnackbar";


const defaultTableElement = {
    start_time: new Date(),
    end_time: new Date(),
    teacher: '',
    room: '',
    subject: '',
    subject_color: '#D2D2D2'
} as TimeTableElement;

const TimetablePage = () => {
    
    const params = new URLSearchParams(window.location.search);
    const apiService = useApiService();
    const snackbar = useSnackbar();
    const [timetable, setTimetable] = useState<Timetable | null>(null);
    const [editingDialog, setEditingDialog] = useState<{open: boolean, entry: TimeTableElement|null}>({open: false, entry: null});
    const [creationDialog, setCreationDialog] = useState<{open: boolean, entry: TimeTableElement|null}>({open: false, entry: null});
    const getAllDays = (): string[] => {
        return ['1', '2', '3', '4', '5'];
    }
    
    const getDayLabel = (day: string): string => {
        switch (day) {
            case '1':
                return 'Monday';
            case '2':
                return 'Tuesday';
            case '3':
                return 'Wednesday';
            case '4': 
                return 'Thursday';
            case '5':
                return 'Friday';
            default:
                return 'Freetime';
        }
    }
    
    useEffect(() => {
        const fetcher = async () => {
            setTimetable(await apiService.actionGetTimetable(
                params.get('action') == 'forUser' ? params.get('elementId')! : undefined,
                params.get('action') == 'forClass' ? params.get('elementId')! : undefined
            ));
        };
        fetcher();
    }, []);
    
    const saveTimetable = async () => {
            try {
                if (params.get('action') === 'forUser') {
                    await apiService.updateTimetableForUser(params.get('elementId')!, timetable?.table_elements!);
                } else if (params.get('action') === 'forClass') {
                    await apiService.updateTimetableForClass(params.get('elementId')!, timetable?.table_elements!);
                }
                if (snackbar.setSnackbar) {
                    snackbar.setSnackbar({color: "success", message: "Successfully updated timetable"});
                    snackbar.openSnackbar();
                }
            } catch (e) {
                if (snackbar.setSnackbar) {
                    snackbar.setSnackbar({color: "error", message: "Cannot update timetable"});
                    snackbar.openSnackbar();
                }
            }
    }
    
    return (
        <PageLayout title="Timetable">
            <Grid container justifyContent="flex-start" spacing={2}>
                <Grid item xs={6}>
                    {timetable !== null ? (
                        <>
                            <TreeView
                                defaultCollapseIcon={<ExpandMore />}
                                defaultExpandIcon={<ChevronRight />}
                            >
                                {getAllDays().map((day) => (
                                    <ModifiedTreeItem nodeId={`day-${day}`} label={getDayLabel(day)} key={`day-${day}`}>
                                        {timetable?.table_elements.filter(e => e.day == day).map((element) => (
                                            <ModifiedTreeItem
                                                nodeId={`${day}-${element.start_time}`}
                                                label={
                                                    `${element.subject}`
                                                }
                                                key={`${day}-${element.start_time}`}
                                            >
                                                <ModifiedTreeItem
                                                    nodeId={`${day}-${element.start_time}-color`}
                                                    label={
                                                        <div style={{
                                                            borderRadius: '50%',
                                                            width: '20px',
                                                            height: '20px',
                                                            background: element.subject_color
                                                        }} />
                                                    }
                                                />
                                                <ModifiedTreeItem
                                                    nodeId={`${day}-${element.start_time}-room`}
                                                    label={`Room: ${element.room} ${element.teacher}`}
                                                />
                                                <ModifiedTreeItem
                                                    nodeId={`${day}-${element.start_time}-teacher`}
                                                    label={`Teacher: ${element.teacher}`}
                                                />
                                                <ModifiedTreeItem
                                                    nodeId={`${day}-${element.start_time}-date`}
                                                    label={`Time: ${new Date(element.start_time).getHours()}:${new Date(element.start_time).getMinutes()}
                                            - ${new Date(element.end_time).getHours()}:${new Date(element.end_time).getMinutes()}`}
                                                />
                                                <ModifiedTreeItem
                                                    nodeId={`${day}-${element.start_time}-edit`}
                                                    label="Edit"
                                                    icon={<Edit />}
                                                    onClick={() => setEditingDialog({open: true, entry: element})}
                                                />
                                            </ModifiedTreeItem>
                                        ))}
                                        <ModifiedTreeItem 
                                            nodeId={`day-${day}-create`} 
                                            icon={<Add />} 
                                            label="Add"
                                            onClick={() => setCreationDialog({open: true, entry: {...defaultTableElement, day}})}
                                        />
                                    </ModifiedTreeItem>
                                ))}
                            </TreeView>
                        </>
                    ) : (
                        <CircularProgress />
                    )}
                    <Grid item xs={12}>
                        <Button
                            onClick={saveTimetable}
                            variant="contained"
                            color="primary"
                        >
                            Save
                        </Button>
                    </Grid>
                </Grid>
            </Grid>
            {editingDialog.open && editingDialog.entry !== null ? (
                <EditTimetableDialog 
                    element={editingDialog.entry} 
                    updateElement={(element) => {
                        const index = timetable?.table_elements.indexOf(editingDialog.entry!)!;
                        const copy = [...timetable?.table_elements!];
                        copy[index] = element;
                        setTimetable({...timetable, table_elements: copy});
                    }}
                    onClose={() => setEditingDialog({open: false, entry: null})}
                />
            ) : null}
            {creationDialog.open && creationDialog.entry !== null ? (
                <EditTimetableDialog
                    element={creationDialog.entry}
                    updateElement={(element) => 
                        setTimetable({...timetable, table_elements: [...timetable?.table_elements!, element]})
                    }
                    onClose={() => setCreationDialog({open: false, entry: null})}
                />
            ) : null}
        </PageLayout>
    )
}

export default TimetablePage;