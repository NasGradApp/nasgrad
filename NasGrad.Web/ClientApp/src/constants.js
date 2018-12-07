import { apiHost } from "./apiConfig";

export const apiUrl = `${apiHost}/api`;

export const urlControlers = {
    users: "users"
};

export const defaultPageSize = 50;

export const storageKeys = {
    user: "SK_USER"
};

export const actionTypes = {
    User: {
        loginStarted: "USER_LOGIN_STARTED",
        loginSucceeded: "USER_LOGIN_SUCCEEDED",
        loginFailed: "USER_LOGIN_FAILED",
        logout: "USER_LOGOUT",
        clearError: "USER_CLEAR_ERROR",
        unauthorized: "USER_UNAUTHORIZED"
    }
};
