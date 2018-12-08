import { apiUrl, apiControllerName, defaultPageSize } from '../constants';
import { handleResponse } from '../helpers/helper';
import { headers } from '../helpers/http.helpers';

export const getAllTypes = () => {
    const requestOptions = {
        method: "GET",
        headers: headers(true)
    };

    const url = apiUrl + '/Configuration';
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Unable to get types");
            }
            return Promise.reject(error);
        });
}
