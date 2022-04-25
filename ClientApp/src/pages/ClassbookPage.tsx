import PageLayout from "../components/PageLayout";
import ClassbookDisplay from "../components/classbook/ClassbookDisplay";
import useApiService from "../hooks/useApiService";
import useCurrentUser from "../hooks/useCurrentUser";
import {UserRole} from "../types/Models/UserRole";
import ClassbookList from "../components/classbook/ClassbookList";


const ClassbookPage = () => {
    
    const {user} = useCurrentUser();
    
    return (
        <PageLayout title="Classbook">
            {user?.roles.includes(UserRole.STUDENT) ? <ClassbookDisplay /> : <ClassbookList />}
            
        </PageLayout>
    )
}

export default ClassbookPage;