import { storageKeys } from '../constants';

export const headers = (withAuth) => {
    const user = JSON.parse(localStorage.getItem(storageKeys.user));

    const headers = {
        "Content-Type": "application/json"
    };

    if (withAuth && user && user.jwt) {
        headers.Authorization = "Bearer " + user.jwt;
    }

    return headers;
};
