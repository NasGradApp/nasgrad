import { host } from "./runtimeConfig";

let runtimeHost = host;

export const apiUrl = `${runtimeHost}/api`;
export const dotnetifyUrl = `${runtimeHost}`;
export const urlControlers = {
    users: "users"
};

export const defaultPageSize = 10;

export const localStorageKeys = {
    user: "LS_USER"
};

export const actionTypes = {
    User: {
        // login thunk
        loginStarted: "USER_LOGIN_STARTED",
        loginSucceeded: "USER_LOGIN_SUCCEEDED",
        loginFailed: "USER_LOGIN_FAILED",
        logout: "USER_LOGOUT",
        clearError: "USER_CLEAR_ERROR",
        unauthorized: "USER_UNAUTHORIZED"
    }
};
