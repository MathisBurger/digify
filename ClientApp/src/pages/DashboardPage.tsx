import Navbar from "../components/Navbar";
import useApiService from "../hooks/useApiService";


const DashboardPage = () => {
    
    const apiService = useApiService();
    console.log(apiService.me());
    
    return (
      <Navbar /> 
    );
}

export default DashboardPage;