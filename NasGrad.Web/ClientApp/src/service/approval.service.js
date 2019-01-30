import { apiUrl } from '../constants';
import { handleResponse } from '../helpers/helper';
import { headers } from '../helpers/http.helpers';

export const getAllIssues = () => {
    const requestOptions = {
        method: "GET",
        headers: headers(true)
    };

    const url = apiUrl + '/approval/allIssues';
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Unable to get approval issues");
            }
            return Promise.reject(error);
        });
}

export const getIssue = (id) => {
    const requestOptions = {
        method: "GET",
        headers: headers(true)
    };

    const url = apiUrl + '/approval/GetIssueDetails/' + id;
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Unable to find issue");
            }
            return Promise.reject(error);
        });
}

export const approveIssue = (id) => {
    const requestOptions = {
        method: "PUT",
        headers: headers(true),
        body: JSON.stringify(id)
    };

    const url = apiUrl + '/approval/approveItem';
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Unable to approve issue");
            }
            return Promise.reject(error);
        });
}

export const deleteIssue = (id) => {
    const requestOptions = {
        method: "DELETE",
        headers: headers(true),
        body: JSON.stringify(id)
    };

    const url = apiUrl + '/approval/deleteItem';
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Unable to delete issue");
            }
            return Promise.reject(error);
        });
}
