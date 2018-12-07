import { apiUrl, storageKeys, urlControlers } from '../constants';
import { handleResponse } from '../helpers/helper';
import { headers } from '../helpers/http.helpers';

export const login = (username, password) => {
    const requestOptions = {
        method: "POST",
        headers: headers(true),
        body: JSON.stringify({ username, password })
    };

    const url = apiUrl + `/${urlControlers.users}/login`;
    return fetch(url, requestOptions)
        .then(handleResponse)
        .catch(error => {
            if (error.message) {
                return Promise.reject("Username or password is incorrect");
            }
            return Promise.reject(error);
        });
};

export const saveLoginData = (user) =>
    localStorage.setItem(storageKeys.user, JSON.stringify(user));

export const logout = () =>
    localStorage.removeItem(storageKeys.user);
