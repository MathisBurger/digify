import {Card, IconButton, List, ListItem, ListItemIcon, ListItemText, Menu, MenuItem} from "@mui/material";
import {User} from "../../types/Models/User";
import {Add, Remove} from "@mui/icons-material";
import {useMemo, useState} from "react";
import UseApiService from "../../hooks/useApiService";
import {Classbook} from "../../types/Models/Classbook";
import MissingUserClickedDialog from "../../dialogs/classbook/MissingUserClickedDialog";

interface ClassbookMissingViewProps {
    missingStudents: User[];
    students: User[];
    classbookID: string;
    setClassbook: (classbook: Classbook) => void;
    editorMode: boolean;
}

const ClassbookMissingView = ({missingStudents, students, classbookID, setClassbook, editorMode}: ClassbookMissingViewProps) => {
    
    const [anchorEl, setAnchorEl] = useState<any>(null);
    const dropdownOpen = Boolean(anchorEl);
    const apiService = UseApiService();
    const [removeUser, setRemoveUser] = useState<string|null>(null);
    
    const unaddedStudents = useMemo(() => {
        
        const containedInMissing = (id: string) => missingStudents.filter(u => u.id === id).length > 0;
        
        return students.filter(s => !containedInMissing(s.id));
    }, [students])
    
    const addUsertoMissingList = async (missingID: string) => {
        const classbook = await apiService.addMissingPersonToClassbook(classbookID, missingID);
        setClassbook(classbook);
    }
    
    console.log(removeUser);
    
    return (
        <>
            <Card elevation={1}>
                <List>
                    {missingStudents.map(student => (
                        <ListItem key={student.id} button onClick={() => setRemoveUser(student.id)}>
                            <ListItemText>{student.username}</ListItemText>
                        </ListItem>
                    ))}
                    {editorMode ? (
                        <ListItem button onClick={(e) => setAnchorEl(e.currentTarget)}>
                            <ListItemIcon>
                                <Add />
                            </ListItemIcon>
                        </ListItem>
                    ) : null}
                </List>
            </Card>
            {unaddedStudents.length > 0 ? (
                <Menu
                    anchorEl={anchorEl}
                    open={dropdownOpen}
                    onClose={() => setAnchorEl(null)}
                >
                    {unaddedStudents.map(s => (
                        <MenuItem onClick={() => addUsertoMissingList(s.id)}>{s.username}</MenuItem>
                    ))}
                </Menu>
            ) : null}
            {removeUser !== null ? (
                <MissingUserClickedDialog
                    close={() => setRemoveUser(null)}
                    userID={'' + removeUser}
                    classbookID={classbookID}
                    setClassbook={setClassbook}
                    editorMode={editorMode}
                />
            ) : null}
        </>
    );
}

export default ClassbookMissingView;