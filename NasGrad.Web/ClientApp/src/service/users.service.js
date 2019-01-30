import { apiUrl, storageKeys } from "../constants";
import { handleResponse } from "../helpers/helper";
import { headers } from "../helpers/http.helpers";


export const login = (username, password) => {
    const requestOptions = {
        method: "POST",
        headers: headers(false),
        body: JSON.stringify({ username, password })
    };
    
    const url = apiUrl + '/login';
    return fetch(url, requestOptions)
        .then(handleResponse)
        .then(user => {
            if (user) {
                localStorage.setItem(storageKeys.user, JSON.stringify(user));
            }

            return user;
        }).catch(error => {
            if (error.message) {
                return Promise.reject("Username or password is incorrect");
            }
            return Promise.reject(error);
        });
}

export const logout = () => {
    localStorage.removeItem(storageKeys.user);
}
