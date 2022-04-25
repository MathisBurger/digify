import PageLayout from "../components/PageLayout";
import ClassbookDisplay from "../components/classbook/ClassbookDisplay";
import ExtendableDateTimePicker from "../components/form/ExtendableDateTimePicker";
import {useState} from "react";


const SpecificClassbookPage = () => {

    const params = new URLSearchParams(window.location.search);
    
    const [selectedDate, setSelectedDate] = useState<Date>(new Date());
    
    const Sidebar = () => {
      
        return <ExtendableDateTimePicker 
            date={selectedDate}
            onChange={(date) => setSelectedDate(date ?? new Date())}
        />;
    };
    
    return (
        <PageLayout title="Specific Classbook" sidebarContent={<Sidebar />}>
            <ClassbookDisplay id={params.get('id') ?? undefined} editingMode={true} selectedDate={selectedDate} />
        </PageLayout>
    );
}

export default SpecificClassbookPage;