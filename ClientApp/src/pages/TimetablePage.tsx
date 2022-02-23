import PageLayout from "../components/PageLayout";
import useApiService from "../hooks/useApiService";
import {useEffect, useState} from "react";
import {ChevronRight, Edit, ExpandMore} from "@mui/icons-material";
import {Timetable, TimeTableElement} from "../types/Models/Timetable";
import {TreeView} from "@mui/lab";
import {CircularProgress, Grid} from "@mui/material";
import ModifiedTreeItem from "../components/ModifiedTreeItem";
import EditTimetableDialog from "../dialogs/timetable/EditTimetableDialog";


const TimetablePage = () => {
    
    const params = new URLSearchParams(window.location.search);
    const apiService = useApiService();
    const [timetable, setTimetable] = useState<Timetable | null>(null);
    const [editingDialog, setEditingDialog] = useState<{open: boolean, entry: TimeTableElement|null}>({open: false, entry: null});
    
    const getAllDays = (): string[] => {
        const days: string[] = [];
        for (const element of timetable?.table_elements!) {
            if (!days.includes(element.day)) days.push(element.day);
        }
        return days;
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
    
    return (
        <PageLayout title="Timetable">
            <Grid container justifyContent="flex-start">
                <Grid item xs={6}>
                    {timetable !== null ? (
                        <TreeView
                            defaultCollapseIcon={<ExpandMore />}
                            defaultExpandIcon={<ChevronRight />}
                        >
                            {getAllDays().map((day) => (
                                <ModifiedTreeItem nodeId={`day-${day}`} label={getDayLabel(day)}>
                                    {timetable?.table_elements.filter(e => e.day == day).map((element) => (
                                        <ModifiedTreeItem 
                                            nodeId={`${day}-${element.start_time}`}
                                            label={
                                            `${element.subject}`
                                            }
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
                                </ModifiedTreeItem>
                            ))}
                        </TreeView>
                    ) : (
                        <CircularProgress />
                    )}
                </Grid>
            </Grid>
            {editingDialog.open && editingDialog.entry !== null ? (
                <EditTimetableDialog 
                    element={editingDialog.entry} 
                    updateElement={(element) => {}}
                />
            ) : null}
        </PageLayout>
    )
}

export default TimetablePage;