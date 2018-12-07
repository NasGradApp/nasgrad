import { localStorageKeys } from "../constants";

export const authHeader = () => {
    const user = JSON.parse(localStorage.getItem(localStorageKeys.user));

    const headers = {};
    if (user && user.auth_token) {
        headers.Authorization = "Bearer " + user.auth_token;
    }

    return headers;
};
