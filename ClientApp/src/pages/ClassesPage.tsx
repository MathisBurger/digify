import PageLayout, {SidebarAction} from "../components/PageLayout";
import ClassesList from "../components/classes/ClassesList";
import {useState} from "react";
import {Add, List} from "@mui/icons-material";
import UsersList from "../components/users/UsersList";
import CreateUser from "../components/users/CreateUser";
import CreateClass from "../components/classes/CreateClass";

enum Pages {
    ListPage,
    CreatePage,
}

const ClassesPage = () => {

    const [title, setTitle] = useState<string|null>(null);
    const [page, setPage] = useState<Pages>(Pages.ListPage)

    const actions: SidebarAction[] = [
        {
            icon: List,
            text: "List",
            action: () => {
                setPage(Pages.ListPage);
                setTitle("List");
            }
        },
        {
            icon: Add,
            text: "Create",
            action: () => {
                setPage(Pages.CreatePage);
                setTitle("Create");
            }
        }
    ];

    const getPage = () => {
        switch (page) {
            case Pages.ListPage:
                return <ClassesList />;
            case Pages.CreatePage:
                return <CreateClass />;
            default:
                return <ClassesList />;
        }
    }
    
    return (
        <PageLayout title="List" sidebarActions={actions}>
            {getPage()}
        </PageLayout>
    )
}

export default ClassesPage;