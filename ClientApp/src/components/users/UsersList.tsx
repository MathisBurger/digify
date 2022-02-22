import {useEffect, useState} from "react";
import {User} from "../../types/Models/User";
import useApiService from "../../hooks/useApiService";
import {DataGrid, GridCellParams, GridColDef, GridRenderCellParams, GridRowModel} from "@mui/x-data-grid";
import {UserRole} from "../../types/Models/UserRole";
import {Chip, Grid} from "@mui/material";
import DataList from "../DataList/DataList";
import {CalendarViewMonth, Remove} from "@mui/icons-material";
import {useHistory} from "react-router-dom";

/**
 * A list of all users that are existing in the system
 */
const UsersList = () => {
    
    const [users, setUsers] = useState<User[]>([]);
    const apiService = useApiService();
    const history = useHistory();
    
    useEffect(() => {
        const fetcher = async () => setUsers(await apiService.allUsers());
        fetcher();
    }, []);

    const columns: GridColDef[] = [
        {
           field: 'username',
           headerName: 'Username', 
           width: 200,
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
            field: 'roles',
            headerName: 'Roles',
            renderCell: ({value}: GridRenderCellParams) => (
                <Grid container direction="row">
                    {Object.values(value as UserRole[]).map((role, i) => (
                        <Chip color="primary" variant="outlined" label={role} key={i} />
                    ))}
                </Grid>
            ),
            width: 150,
            flex: 1
        }
    ];
    
    return (
        <DataList 
            columns={columns}
            rows={users}
            singleActions={[
                {
                    icon: Remove,
                    onClick: async (row: GridRowModel) => {
                        await apiService.deleteUser(row.id);
                    }
                },
                {
                    icon: CalendarViewMonth,
                    onClick: (row: GridRowModel) => 
                        history.push(`/timetable?action=forUser&elementId=${row.id}`)
                }
            ]}
        />
    );
}

export default UsersList;