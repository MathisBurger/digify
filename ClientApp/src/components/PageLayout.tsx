import {
    Card, CardContent,
    Grid,
    List,
    ListItem,
    ListItemIcon,
    ListItemText,
    SvgIcon,
    SvgIconTypeMap,
    Toolbar, Typography
} from "@mui/material";
import Navbar from "./Navbar";
import {OverridableComponent} from "@mui/material/OverridableComponent";
import React from "react";

type MuiIcon = OverridableComponent<SvgIconTypeMap> & {muiName: string};

/**
 * A action that can be provided in the sidebar
 */
export interface SidebarAction {
    /**
     * The icon that can be provided 
     */
    icon?: MuiIcon;
    /**
     * The text that is displayed in the option
     */
    text: string;
    /**
     * The action that is performed on click
     */
    action: () => void;
}

interface PageLayoutProps {
    /**
     * All sidebar actions
     */
    sidebarActions?: SidebarAction[];
    /**
     * The title of the page
     */
    title: string;
}

/**
 * Wrapper to wrap the page into a general purpose design
 */
const PageLayout = ({
    sidebarActions = [],
    title,
    children
}: React.PropsWithChildren<PageLayoutProps>) => {
    
    return (
        <>
            <Navbar />
            <Grid container direction="row" spacing={2} alignItems="center" minHeight="80vh" justifyContent="center">
                {sidebarActions.length > 0 ? (
                    <Grid item xs={2}>
                            <Toolbar />
                            <Card>
                                <List>
                                    {sidebarActions?.map((action, index) => (
                                        <ListItem button key={index} onClick={action.action}>
                                            {action.icon ? (
                                                <ListItemIcon>
                                                    <SvgIcon component={action.icon} />
                                                </ListItemIcon>
                                            ) : null}
                                            <ListItemText primary={action.text} />
                                        </ListItem>
                                    ))}
                                </List>
                            </Card>
                    </Grid>
                ) : null}
                <Grid item xs={10}>
                    <Card>
                        <CardContent>
                            <Typography variant="h4">{title}</Typography>
                            <hr />
                            {children}
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
        </>
    );
}

PageLayout.defaultProps = {
    sidebarActions: [],
}

export default PageLayout;