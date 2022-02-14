import PageLayout from "../components/PageLayout";
import {Grid} from "@mui/material";
import {Timeline} from "@mui/lab";
import useApiService from "../hooks/useApiService";

/**
 * Dashboard page
 */
const DashboardPage = () => {
    
    const apiService = useApiService();
    
    return (
      <PageLayout title="Dashboard">
          <Grid container>
              <Grid item xs={3}>
                  <Timeline position="alternate">
                      
                  </Timeline>
              </Grid>
          </Grid>
      </PageLayout>
    );
}

export default DashboardPage;