import {useEffect, useState} from "react";
import {User} from "../../types/Models/User";
import useApiService from "../../hooks/useApiService";
import {DataGrid} from "@mui/x-data-grid";

const UsersList = () => {
    
    const [users, setUsers] = useState<User[]>([]);
    const apiService = useApiService();
    
    useEffect(() => {
        const fetcher = async () => setUsers(await apiService.allUsers());
        fetcher();
    }, []);

    
    
    return (
        <DataGrid columns={[]} rows={[]} />
    );
}