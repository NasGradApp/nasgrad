import { apiHost } from "./apiConfig";

export const apiUrl = `${apiHost}/api`;

export const urlControlers = {
    users: "users"
};

export const defaultPageSize = 20;

export const storageKeys = {
    user: "SK_USER"
};

export const apiControllerName = {
    users: "users",
    issues: "issues"
};

export const viewType = {
    list: "LIST",
    map: "MAP"
}

export const actionTypes = {
    users: {
        loginStarted: "USER_LOGIN_STARTED",
        loginSucceeded: "USER_LOGIN_SUCCEEDED",
        loginFailed: "USER_LOGIN_FAILED",
        logout: "USER_LOGOUT",
        clearError: "USER_CLEAR_ERROR",
        unauthorized: "USER_UNAUTHORIZED"
    },
    issues: {
        getPageStarted: "ISSUES_GET_PAGE_STARTED",
        getPageSucceeded: "ISSUES_GET_PAGE_SUCCEEDED",
        getPageFailed: "ISSUES_GET_PAGE_FAILED",
        setActivePage: "ISSUES_SET_ACTIVE_PAGE",
        setActiveViewType: "ISSUE_SET_ACTIVE_VIEW_TYPE"
    }
};
