import { apiUrl, apiControllerName, defaultPageSize } from '../constants';
import { handleResponse } from '../helpers/helper';
import { headers } from '../helpers/http.helpers';

export const getAllCategories = () => {
    const requestOptions = {
        method: "GET",
        headers: headers(true)
    };

    const url = apiUrl + '/Category';
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Unable to get categories");
            }
            return Promise.reject(error);
        });
}
