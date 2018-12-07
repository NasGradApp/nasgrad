import { apiUrl, localStorageKeys, urlControlers } from "../constants";
import { defaultHeaders, handleResponse } from "../helpers/fetch.helper";

export const login = (username, password) => {
    const requestOptions = {
        method: "POST",
        headers: defaultHeaders(),
        body: JSON.stringify({ username, password })
    };

    // login API allow anonymous call
    delete requestOptions.headers.Authorization;

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

export const saveLoginData = (user, username) => {
    // store user details and jwt token in local storage to keep user logged in between page refreshes
    localStorage.setItem(localStorageKeys.user, JSON.stringify(user));
    localStorage.setItem(localStorageKeys.username, username);
};

export const logout = () => {
    // remove user from local storage to logout user
    localStorage.removeItem(localStorageKeys.user);
    localStorage.removeItem(localStorageKeys.username);
};
