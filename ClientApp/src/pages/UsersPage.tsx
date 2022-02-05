import PageLayout, {SidebarAction} from "../components/PageLayout";
import {Add, List} from "@mui/icons-material";
import {useState} from "react";
import UsersList from "../components/users/UsersList";
import CreateUser from "../components/users/CreateUser";

enum Pages {
    ListPage,
    CreatePage,
}

/**
 * Users page
 */
const UsersPage = () => {
    
    const [title, setTitle] = useState<string|null>(null);
    const [page, setPage] = useState<Pages>(Pages.ListPage)
    
    const actions: SidebarAction[] = [
        {
            icon: List,
            text: "List",
            action: () => setPage(Pages.ListPage)
        },
        {
            icon: Add,
            text: "Create",
            action: () => setPage(Pages.CreatePage)
        }
    ];
    
    const getPage = () => {
        switch (page) {
            case Pages.ListPage:
                return <UsersList />;
            case Pages.CreatePage:
                return <CreateUser />;
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