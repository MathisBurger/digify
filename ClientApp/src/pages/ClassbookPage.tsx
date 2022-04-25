import PageLayout from "../components/PageLayout";
import ClassbookDisplay from "../components/classbook/ClassbookDisplay";
import useCurrentUser from "../hooks/useCurrentUser";
import {UserRole} from "../types/Models/UserRole";
import ClassbookList from "../components/classbook/ClassbookList";
import {useState} from "react";
import ExtendableDateTimePicker from "../components/form/ExtendableDateTimePicker";


const ClassbookPage = () => {
    
    const {user} = useCurrentUser();
    const isStudent = user?.roles.includes(UserRole.STUDENT);

    const [selectedDate, setSelectedDate] = useState<Date>(new Date());

    const Sidebar = () => {

        return <ExtendableDateTimePicker
            date={selectedDate}
            onChange={(date) => setSelectedDate(date ?? new Date())}
        />;
    };
    
    
    return (
        <PageLayout title="Classbook" sidebarContent={isStudent ? <Sidebar /> : undefined}>
            {isStudent ? <ClassbookDisplay selectedDate={selectedDate} /> : <ClassbookList />}
            
        </PageLayout>
    )
}

export default ClassbookPage;