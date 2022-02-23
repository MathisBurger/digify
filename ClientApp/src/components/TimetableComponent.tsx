import {
    Timeline,
    TimelineConnector, TimelineContent,
    TimelineDot,
    TimelineItem,
    TimelineOppositeContent,
    TimelineSeparator
} from "@mui/lab";
import {Timetable} from "../types/Models/Timetable";
import {Typography} from "@mui/material";


interface TimetableComponentProps {
    timetable: Timetable;
}

const TimetableComponent = ({timetable}: TimetableComponentProps) => {
    
    const compareTimes = (a: Date, b: Date): boolean => {
        const startSecs = a.getHours() * 3600 + a.getMinutes() * 60;
        const endSecs = b.getHours() * 3600 + b.getMinutes() * 60;
        const currentSecs = new Date().getHours() * 3600 + new Date().getMinutes() * 60;
        return startSecs > currentSecs || currentSecs < endSecs;
    }

    const currentDay = `${new Date().getDay()}`;
    const currentDayEvents = timetable.table_elements.filter(e => e.day === currentDay);
    const reduced = currentDayEvents.filter(u => compareTimes(new Date(u.start_time), new Date(u.end_time)));
    const sorted = reduced.sort(
        (a, b) => new Date(a.start_time).getTime() > new Date(b.start_time).getTime() ? 1 : -1
    );
    
    return (
        <Timeline>
            {sorted.map((e, i) => (
                <TimelineItem key={i}>
                    <TimelineOppositeContent color="text.secondary">
                        {`${new Date(e.start_time).getHours()}:${new Date(e.start_time).getMinutes()} 
                        - ${new Date(e.end_time).getHours()}:${new Date(e.end_time).getMinutes()}`}
                    </TimelineOppositeContent>
                    <TimelineSeparator>
                        <TimelineDot style={{background: e.subject_color}}/>
                        <TimelineConnector />
                    </TimelineSeparator>
                    <TimelineContent>
                        <Typography fontWeight="bold">{e.subject}</Typography>
                        <Typography>{e.room}</Typography>
                        <Typography>{e.teacher}</Typography>
                    </TimelineContent>
                </TimelineItem>
            ))}
        </Timeline>
    );
}


export default TimetableComponent;