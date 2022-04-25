import PageLayout from "../components/PageLayout";
import ClassbookDisplay from "../components/classbook/ClassbookDisplay";


const SpecificClassbookPage = () => {

    const params = new URLSearchParams(window.location.search);
    
    return (
        <PageLayout title="Specific Classbook">
            <ClassbookDisplay id={params.get('id') ?? undefined} editingMode={true} />
        </PageLayout>
    );
}

export default SpecificClassbookPage;