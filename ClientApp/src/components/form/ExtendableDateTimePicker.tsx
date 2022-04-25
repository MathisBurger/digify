import {Button, Grid} from "@mui/material";
import {CalendarPicker} from "@mui/lab";
import {useState} from "react";
import {Add, Remove} from "@mui/icons-material";

interface ExtendableDateTimePickerProps {
    onChange: (date: Date|null) => void;
    date: Date;
}

const ExtendableDateTimePicker = ({onChange, date}: ExtendableDateTimePickerProps) => {
    
    const [extended, setExtended] = useState<boolean>(false);
    
    const change = (input: Date|null) => {
        setExtended(false);
        onChange(input);
    } 
    
    return (
      <Grid container direction="column">
          <Grid item>
              <Button sx={{width: '100%'}} onClick={() => setExtended(!extended)}>
                  {extended ? <Remove /> : <Add />}
              </Button>
          </Grid>
          {extended ? (
              <Grid item>
                  <CalendarPicker 
                      date={date} 
                      onChange={change}
                  />
              </Grid>
          ) : null}
      </Grid>  
    );
}

export default ExtendableDateTimePicker;