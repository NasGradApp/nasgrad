import { apiUrl, apiControllerName, defaultPageSize } from '../constants';
import { handleResponse } from '../helpers/helper';
import { headers } from '../helpers/http.helpers';

export const getAllIssues = () => {
    const requestOptions = {
        method: "GET",
        headers: headers(true)
    };

    const url = apiUrl + '/getissuelist';
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Unable to get issues");
            }
            return Promise.reject(error);
        });
}

export const getIssue = (id) => {
    const requestOptions = {
        method: "GET",
        headers: headers(true)
    };

    const url = apiUrl + '/GetIssueList/GetIssueDetails/' + id;
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Unable to find issue");
            }
            return Promise.reject(error);
        });
}
