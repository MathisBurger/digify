import {FormControl, Grid, GridSize, InputLabel, MenuItem, Select, SelectProps} from "@mui/material";


interface FormSelectFieldProps {
    /**
     * The size of the select
     */
    xs: GridSize;
    /**
     * The value of the field
     */
    value: SelectProps['value'];
    /**
     * If multiple values can be selected
     */
    multiple: SelectProps['multiple'];
    /**
     * The change event
     */
    onChange: SelectProps['onChange'];
    /**
     * The label of the select
     */
    label: string;
    /**
     * All possible select options
     */
    options: {value: string, key: string}[];
}

/**
 * FormField that handles simple data selection
 */
const FormSelectField = ({xs, value, label, multiple, onChange, options}: FormSelectFieldProps) => {
    
    const calculateShrink = () => {
        if (Array.isArray(value)) {
            return value.length > 0;
        }
        return value === '';
    }
    
    return (
        <Grid item xs={xs}>
            <FormControl fullWidth required>
                <InputLabel
                    shrink={calculateShrink()}
                    title={label}
                >
                    {label}
                </InputLabel>
                <Select
                    label={value ? label : undefined}
                    name={label}
                    id={label}
                    value={value}
                    multiple={multiple}
                    onChange={onChange}
                >
                    {options.map(option => (
                        <MenuItem value={option.key}>{option.value}</MenuItem>
                    ))}
                </Select>
            </FormControl>
        </Grid>
    )
}

export default FormSelectField;