import { apiUrl, apiControllerName, defaultPageSize } from '../constants';
import { handleResponse } from '../helpers/helper';
import { headers } from '../helpers/http.helpers';

export const getPage = (page, pageSize) => {
    const requestOptions = {
        method: "GET",
        headers: headers(true)
    };

    if (!page) {
        page = 1;
    }
    
    if (!pageSize) {
        pageSize = defaultPageSize;
    }
    // http://68.183.223.223:8080/api/getissuelist
    // const url = apiUrl + `/${apiControllerName.issues}?page=${page}&pageSize=${pageSize}`;
    const url = apiUrl + '/getissuelist';
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Unable to get issues");
            }
            return Promise.reject(error);
        });
};
