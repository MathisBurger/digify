import {TreeItem, TreeItemProps} from "@mui/lab";
import {makeStyles} from "@mui/styles";

const ModifiedTreeItem = (props: TreeItemProps & {color?: string, whiteFont?: boolean}) => {
    
    const useStyles = makeStyles({
        content: {
            borderRadius: '10px',
            padding: '10px',
            background: props.color,
            color: props.whiteFont ? '#fff' : undefined
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