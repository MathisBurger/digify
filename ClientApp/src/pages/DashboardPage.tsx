import PageLayout from "../components/PageLayout";
import {CardContent, Card, Grid, CircularProgress} from "@mui/material";
import useApiService from "../hooks/useApiService";
import TimetableComponent from "../components/TimetableComponent";
import {useEffect, useState} from "react";
import {Timetable} from "../types/Models/Timetable";
import useCurrentUser from "../hooks/useCurrentUser";
import {UserRole} from "../types/Models/UserRole";

/**
 * Dashboard page
 */
const DashboardPage = () => {
    
    const apiService = useApiService();
    const {user} = useCurrentUser();
    const [timetable, setTimetable] = useState<Timetable|null>(null);
    
    useEffect(() => {
        const fetcher = async () => {
            if (user?.roles && user.roles.includes(UserRole.STUDENT)) {
                setTimetable(await apiService.getTimetable());
            }
        }
        fetcher();
    }, [user]);
    
    return (
      <PageLayout title="Dashboard">
          <Grid container>
              <Grid item xs={3}>
                  <Card elevation={0} style={{
                      border: "1px solid #9A9A9A"
                  }}>
                      <CardContent>
                          {timetable !== null  ? (
                              <TimetableComponent timetable={timetable} />
                          ) : (
                              <CircularProgress />
                          )}
                      </CardContent>
                  </Card>
              </Grid>
          </Grid>
      </PageLayout>
    );
}

export default DashboardPage;