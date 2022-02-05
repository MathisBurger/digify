export default class RestService {

    /**
     * The general shaped request function for doing a
     * REST http request.
     *
     * @param method The request method
     * @param path The path to the endpoint
     * @param body The request body if its given
     * @param contentType The content type of the request
     * @return Promise<T> The generic promise response
     * @throws Error If the status code is not 200
     */
    private static async fetchEndpoint<T>(
        method: string,
        path: string,
        body?: any,
        contentType: string | undefined = 'application/json',
        emptyResponse: boolean = false 
    ): Promise<T> {
        const fetchResult = await window.fetch(path, {
            body: body,
            method: method,
            mode: process.env.NODE_ENV === "production" ? "same-origin" : "cors",
            headers: {
                'Content-Type': contentType
            },
            credentials: process.env.NODE_ENV === "production" ? "same-origin" : "include"
        });
        if (fetchResult.status === 401) {
            if (window.location.pathname === "/login") return {} as any;
            window.location.replace("/login");
        }
        if (fetchResult.status !== 200 && fetchResult.status !== 204) {
            // Parse to generic error response
            throw new Error('Something went wrong');
        }
        if (fetchResult.status === 204) {
            return {} as any;
        }
        if (!emptyResponse) {
            return (await fetchResult.json()) as T;
        }
        return {} as any;
    }

    /**
     * The general GET request.
     *
     * @param path The path to the endpoint
     * @return Promise<T> The response as generic promise
     */
    protected async get<T>(path: string): Promise<T> {
        return await RestService.fetchEndpoint<T>("GET", path);
    }

    /**
     * The general POST request.
     *
     * @param path The path to the resp endpoint
     * @param body The http body of the request
     * @param emptyResponse If the response has no json body
     * @return Promise<T> The response as generic promise
     */
    protected async post<T>(path: string, body: any, emptyResponse: boolean = false): Promise<T> {
        return await RestService.fetchEndpoint<T>("POST", path, body, 'application/json', emptyResponse);
    }
}