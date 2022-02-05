import PageLayout, {SidebarAction} from "../components/PageLayout";
import {List} from "@mui/icons-material";
import {useState} from "react";
import UsersList from "../components/users/UsersList";

enum Pages {
    ListPage
}

const UsersPage = () => {
    
    const [title, setTitle] = useState<string|null>(null);
    const [page, setPage] = useState<Pages>(Pages.ListPage)
    
    const actions: SidebarAction[] = [
        {
            icon: List,
            text: "List",
            action: () => console.log("Interessant")
        }
    ];
    
    const getPage = () => {
        switch (page) {
            case Pages.ListPage:
                return <UsersList />;
            default:
                return <UsersList />;
        }
    }
    
    return (
        <PageLayout sidebarActions={actions} title={title ?? actions[0].text}>
            {getPage()}            
        </PageLayout>        
    );
}

export default UsersPage;