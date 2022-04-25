import {Box, Grid, IconButton, SvgIcon} from "@mui/material";
import {
    DataGrid,
    DataGridProps, GridColDef, GridRowModel,
    GridToolbarColumnsButton,
    GridToolbarContainer, GridToolbarDensitySelector, GridToolbarExport,
    GridToolbarFilterButton
} from "@mui/x-data-grid";
import React, {useMemo} from "react";
import {MuiIcon} from "../PageLayout";


export interface SingleAction {
    /**
     * The icon of the action
     */
    icon: MuiIcon;
    /**
     * Method that takes the active row model as parameter
     */
    onClick: (item: GridRowModel) => void;
}

/**
 * All props of a data list
 */
type DataListProps = Pick<
    DataGridProps,
    'columns'
    | 'rows'
    | 'loading'
    | 'density'
    | 'onCellEditCommit'
    >
& {
  singleActions?: SingleAction[];  
  customID?: boolean;
};

/**
 * A wrapper of the MUI data grid to make working 
 * with the data grids more easier.
 */
const DataList = ({
    columns,
    rows,
    loading,
    density,
    singleActions = undefined,
    customID = false,
    onCellEditCommit = undefined
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
    const prepareColumns = () => {
        const cols: GridColDef[] = singleActions ? [
            {
                field: 'actions',
                headerName: 'Actions',
                renderCell: ({row}) => (
                    <Grid container direction="row">
                        {singleActions.map(item => (
                            <Grid item>
                                <IconButton 
                                    color="primary"
                                    onClick={() => item.onClick(row)}
                                ><SvgIcon component={item.icon} /></IconButton>
                                
                            </Grid>
                        ))}
                    </Grid>
                )
            }
        ] : [];
        return cols.concat(columns);
    }
    
    return (
      <Box width="100%" height={500}>
          <DataGrid 
              columns={prepareColumns()} 
              rows={prepareContent} 
              loading={loading}
              density={density}
              checkboxSelection
              disableSelectionOnClick={true}
              onCellEditCommit={onCellEditCommit}
              getRowId={(row) => customID ? row.id : row.ghostID}
              components={{
                  Toolbar: CustomToolbar
              }}
          />
      </Box>  
    );
}

export default DataList;