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

export interface SidebarAction {
    icon?: MuiIcon;
    text: string;
    action: () => void;
}

interface PageLayoutProps {
    sidebarActions?: SidebarAction[];
    title: string;
}

const PageLayout = ({
    sidebarActions = [],
    title,
    children
}: React.PropsWithChildren<PageLayoutProps>) => {
    
    return (
        <>
            <Navbar />
            <Grid container direction="row" spacing={2} alignItems="center" minHeight="80vh">
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