import {Box} from "@mui/material";
import {
    DataGrid,
    DataGridProps,
    GridToolbarColumnsButton,
    GridToolbarContainer, GridToolbarDensitySelector, GridToolbarExport,
    GridToolbarFilterButton
} from "@mui/x-data-grid";
import React, {useMemo} from "react";

type DataListProps = Pick<
    DataGridProps,
    'columns'
    | 'rows'
    | 'loading'
    >;

const DataList = ({
    columns,
    rows,
    loading
}: DataListProps) => {

    const prepareContent = useMemo(() => {
        return rows.map((data, index) => ({...data, ghostID: index}));
    }, [rows]);
    
    const CustomToolbar = () => (
        <GridToolbarContainer>
            <GridToolbarColumnsButton />
            <GridToolbarFilterButton />
            <GridToolbarDensitySelector />
            <GridToolbarExport />
        </GridToolbarContainer>
    );
    
    return (
      <Box width="100%" height={500}>
          <DataGrid 
              columns={columns} 
              rows={prepareContent} 
              loading={loading}
              checkboxSelection
              getRowId={(row) => row.ghostID}
              components={{
                  Toolbar: CustomToolbar
              }}
          />
      </Box>  
    );
}

export default DataList;