import DataList from "../DataList/DataList";
import {GridColDef} from "@mui/x-data-grid";


const ClassbookTodayView = () => {
    
    const columns: GridColDef[] = [
        {
            field: 'subject'
        }  
    ];
    
    return (
      <DataList columns={[]} rows={[]}  density="compact"/>
    );
}

export default ClassbookTodayView;