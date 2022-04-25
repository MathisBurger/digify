import useApiService from "../../hooks/useApiService";
import {GridColDef, GridRowModel} from "@mui/x-data-grid";
import DataList from "../DataList/DataList";
import {useEffect, useState} from "react";
import {CalendarViewMonth, Folder, Remove} from "@mui/icons-material";
import {useHistory} from "react-router-dom";


const ClassesList = () => {
    
    const apiService = useApiService();
    const history = useHistory();
    
    const [classes, setClasses] = useState<any[]>([]);
    
    useEffect(() => {
        const fetcher = async () => setClasses(await apiService.getAllClasses());
        fetcher();
    }, []);
    
    const columns: GridColDef[] = [
        {
            field: 'name',
            headerName: 'Name',
            width: 100,
            flex: 1
        },
        {
            field: 'created',
            headerName: 'Created',
            type: 'dateTime',
            flex: 1,
            width: 300
        },
        {
            field: 'students',
            headerName: 'Students',
            width: 100,
            flex: 1,
            renderCell: ({value}) => `${value ? value.length : 0}`
        },
        {
            field: 'teachers',
            headerName: 'Teachers',
            width: 100,
            flex: 1,
            renderCell: ({value}) => `${value ? value.length : 0}`
        }
    ];
    
    return (
        <DataList 
            columns={columns} 
            rows={classes}
            singleActions={[
                {
                    icon: Remove,
                    onClick: async (row: GridRowModel) => {
                        await apiService.deleteClass(row.id);
                    }
                },
                {
                    icon: CalendarViewMonth,
                    onClick: (row: GridRowModel) =>
                        history.push(`/timetable?action=forClass&elementId=${row.id}`)
                },
                {
                    icon: Folder,
                    onClick: (row: GridRowModel) =>
                        history.push(`/classbook-specific?id=${row.classbook.id}`)
                }
            ]}
        />
    )
}

export default ClassesList;