import DataList from "../DataList/DataList";
import {GridColDef} from "@mui/x-data-grid";
import {ClassbookDayEntryLesson} from "../../types/Models/Classbook";

interface ClassbookTodayViewProps {
    lessons: ClassbookDayEntryLesson[];
    loading: boolean;
}

const ClassbookTodayView = ({lessons, loading}: ClassbookTodayViewProps) => {
    
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
    console.log(lessons);
    
    return (
      <DataList columns={columns} rows={lessons} loading={loading}  density="compact"/>
    );
}

export default ClassbookTodayView;