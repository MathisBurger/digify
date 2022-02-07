import useApiService from "../../hooks/useApiService";
import {GridColDef} from "@mui/x-data-grid";
import DataList from "../DataList/DataList";
import {useEffect, useState} from "react";


const ClassesList = () => {
    
    const apiService = useApiService();
    
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
        <DataList columns={columns} rows={classes} />
    )
}

export default ClassesList;