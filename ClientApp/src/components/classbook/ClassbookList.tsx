import DataList, {SingleAction} from "../DataList/DataList";
import {GridColDef, GridRenderCellParams, GridRowModel} from "@mui/x-data-grid";
import useApiService from "../../hooks/useApiService";
import {useEffect, useState} from "react";
import {Classbook} from "../../types/Models/Classbook";
import {Folder} from "@mui/icons-material";
import {useHistory} from "react-router-dom";


const ClassbookList = () => {
    
    const apiService = useApiService();
    const [classbooks, setClassbooks] = useState<Classbook[]>([]);
    const history = useHistory();
    
    useEffect(() => {
        const fetcher = async () => setClassbooks(await apiService.getClassbooksForTeacher());
        fetcher();
    }, []);
    
    const columns: GridColDef[] = [
        {
            field: 'name',
            headerName: 'Name',
            width: 100,
            flex: 1,
            renderCell: ({row}: GridRenderCellParams) => row.referedClass.name
        },
        {
            field: 'year',
            headerName: 'Year',
            width: 100,
            flex: 1
        },
    ];
    
    const actions: SingleAction[] = [
        {
            icon: Folder,
            onClick: (row: GridRowModel) => history.push(`/classbook-specific?id=${row.id}`)
        }
    ];
    
    return (
      <DataList columns={columns} rows={classbooks} singleActions={actions}  />
    );
}


export default ClassbookList;