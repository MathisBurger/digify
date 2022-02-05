import APIService from "../services/ApiService";

/**
 * Wraps the API service into a simple hook
 */
const useApiService = () => new APIService();

export default useApiService;