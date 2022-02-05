import {useEffect, useState} from "react";
import {User} from "../../types/Models/User";
import useApiService from "../../hooks/useApiService";
import {DataGrid, GridCellParams, GridColDef, GridRenderCellParams} from "@mui/x-data-grid";
import {UserRole} from "../../types/Models/UserRole";
import {Chip, Grid} from "@mui/material";

const UsersList = () => {
    
    const [users, setUsers] = useState<User[]>([]);
    const apiService = useApiService();
    
    useEffect(() => {
        const fetcher = async () => setUsers(await apiService.allUsers());
        fetcher();
    }, [apiService]);

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
                    {Object.values(value as UserRole[]).map(role => (
                        <Chip color="primary" variant="outlined" label={role} />
                    ))}
                </Grid>
            ),
            width: 150,
            flex: 1
        }
    ];
    
    const prepareContent = () => {
        return users.map((user, index) => ({...user, ghostID: index}));
    }
    
    return (
        <div style={{width: '100%', height: 300}}>
            <DataGrid
                columns={columns}
                rows={prepareContent()}
                getRowId={(row) => row.ghostID}
                checkboxSelection
            />
        </div>
    );
}

export default UsersList;