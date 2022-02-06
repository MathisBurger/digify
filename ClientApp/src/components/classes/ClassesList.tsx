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
        
    ];
    
    return (
        <DataList columns={columns} rows={classes} />
    )
}

export default ClassesList;