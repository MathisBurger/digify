import {Box} from "@mui/material";
import {
    DataGrid,
    DataGridProps,
    GridToolbarColumnsButton,
    GridToolbarContainer, GridToolbarDensitySelector, GridToolbarExport,
    GridToolbarFilterButton
} from "@mui/x-data-grid";
import React, {useMemo} from "react";

/**
 * All props of a data list
 */
type DataListProps = Pick<
    DataGridProps,
    'columns'
    | 'rows'
    | 'loading'
    >;

/**
 * A wrapper of the MUI data grid to make working 
 * with the data grids more easier.
 */
const DataList = ({
    columns,
    rows,
    loading
}: DataListProps) => {

    /**
     * Prepares the content for being consumed by the data grid
     */
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