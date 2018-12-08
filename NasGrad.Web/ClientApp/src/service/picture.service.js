import { apiUrl, apiControllerName, defaultPageSize } from '../constants';
import { handleResponse } from '../helpers/helper';
import { headers } from '../helpers/http.helpers';

export const getAllPictures = () => {
    const requestOptions = {
        method: "GET",
        headers: headers(true)
    };

    const url = apiUrl + '/Picture';
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Unable to get pictures");
            }
            return Promise.reject(error);
        });
}

export const updatePicture = (id, visible) => {
    const headers = {
        "Content-Type": "application/json"
    };
    const requestOptions = {
        method: "POST",
        headers: headers,
        body: JSON.stringify(id, visible)
    };

    const url = apiUrl + `/Picture?id=` + id + '&visible=' + visible;
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Unable to update picture");
            }
            return Promise.reject(error);
        });
}