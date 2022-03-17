import DataList from "../DataList/DataList";
import {GridCellEditCommitParams, GridColDef} from "@mui/x-data-grid";
import {ClassbookDayEntryLesson} from "../../types/Models/Classbook";
import useApiService from "../../hooks/useApiService";
import useSnackbar from "../../hooks/useSnackbar";

interface ClassbookTodayViewProps {
    lessons: ClassbookDayEntryLesson[];
    classbookID: string;
    loading: boolean;
}

const ClassbookTodayView = ({lessons, loading, classbookID}: ClassbookTodayViewProps) => {
    
    const apiService = useApiService();
    const snackbar = useSnackbar();
    const columns: GridColDef[] = [
        {
          field: 'id',
          hide: true  
        },
        {
            field: 'subjectColor',
            headerName: '',
            width: 50,
            renderCell: ({row}) => <div style={{width: '100%', height: '100%', background: row.subjectColor}} />,
            sortable: false
        },
        {
          field: 'teacher',
          headerName: 'Teacher',
          renderCell: ({row}) => row.teacher.username,
          editable: true,
          type: 'singleSelect'  
        },
        {
            field: 'subject',
            headerName: 'Subject',
            flex: 1,
        },
        {
            field: 'approvedByTeacher',
            headerName: 'Approved by Teacher',
            width: 50,
            type: 'boolean',
            editable: true,
        },
        {
            field: 'content',
            headerName: 'content',
            flex: 1,
            editable: true
        },
        {
            field: 'time',
            headerName: 'Time',
            renderCell: ({row}) => `${new Date(row.startTime).getHours()}:${new Date(row.startTime).getMinutes()} 
                        - ${new Date(row.endTime).getHours()}:${new Date(row.endTime).getMinutes()}`
        }
    ];
    
    const updateLesson = async (e: GridCellEditCommitParams) => {
        const lesson: any = lessons.filter(l => l.id === e.id)[0];
        lesson[e.field] = e.value;
        try {
            const classbook = await apiService.updateClassbookLesson(classbookID, lesson);
            if (snackbar.setSnackbar) snackbar.setSnackbar({color: "success", message: "Successfully updated lesson"});
            snackbar.openSnackbar();
            const days = classbook?.dayEntries?.filter(e => (new Date(e.currentDate)).getDate() === (new Date()).getDate());
            if (days && days.length > 0) {
                lessons = days[0].lessons ?? [];
            }
        } catch (e: any) {
            if (snackbar.setSnackbar) snackbar.setSnackbar({color: "success", message: "Error while updating lesson"});
            snackbar.openSnackbar();
        }
        
    }
    
    return (
      <DataList 
          columns={columns} 
          rows={lessons} 
          loading={loading}  
          density="compact" 
          customID 
          onCellEditCommit={(e) => updateLesson(e)}
      />
    );
}

export default ClassbookTodayView;