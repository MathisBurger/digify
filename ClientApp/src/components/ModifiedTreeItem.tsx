import {TreeItem, TreeItemProps} from "@mui/lab";
import {makeStyles} from "@mui/styles";

const ModifiedTreeItem = (props: TreeItemProps & {color?: string}) => {
    
    const useStyles = makeStyles({
        content: {
            borderRadius: '10px',
            padding: '10px',
            background: props.color
        }
    });
    const style = useStyles();
    
    return (
        <TreeItem
            {...props}
            classes={{
                content: style.content
            }}
        />
    )
}

export default ModifiedTreeItem;