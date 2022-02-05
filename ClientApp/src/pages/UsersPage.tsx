import PageLayout, {SidebarAction} from "../components/PageLayout";
import {List} from "@mui/icons-material";

const UsersPage = () => {
    
    const actions: SidebarAction[] = [
        {
            icon: List,
            text: "List",
            action: () => console.log("Interessant")
        }
    ]
    
    return (
        <PageLayout sidebarActions={actions} title="User list" />        
    );
}

export default UsersPage;