import { apiHost } from "./apiConfig";

export const apiUrl = `${apiHost}/api`;

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
        setActiveViewType: "ISSUE_SET_ACTIVE_VIEW_TYPE",
		getIssueSucceeded: "ISSUE_GET_SUCCEEDED",
        getIssueFailed: "ISSUE_GET_FAILED",
        getIssueStarted: "ISSUE_GET_STARTED"
    },
    category: {
        getPageSucceededCategory: "CATEGORY_GET_PAGE_SUCCEEDED",
        getPageFailedCategory: "CATEGORY_GET_PAGE_FAILED",
        getPageStartedCategory: "CATEGORY_GET_PAGE_STARTED",
        setActivePageCategory: "CATEGORY_SET_ACTIVE_PAGE"
    },
    types: {
        getPageStartedTypes: "TYPES_GET_PAGE_STARTED",
        getPageSucceededTypes: "TYPES_GET_PAGE_SUCCEEDED",
        getPageFailedTypes: "TYPES_GET_PAGE_FAILED"
    },
    picture: {
        getPageStartedPicture: "PICTURE_GET_PAGE_STARTED",
        getPageSucceededPicture: "PICTURE_GET_PAGE_SUCCEEDED",
        getPageFailedCPicture: "PICTURE_GET_PAGE_FAILED",
        updatePictureStarted: "PICTURE_UPDATE_STARTED",
        updatePictureSucceded: "PICTURE_PDATE_SUCCEDED",
        updatePictureFailed: "PICTURE_UPDATE_FAILED"
    },
    approvalIssues: {
        getPageStarted: "APPROVAL_ISSUES_GET_PAGE_STARTED",
        getPageSucceeded: "APPROVAL_ISSUES_GET_PAGE_SUCCEEDED",
        getPageFailed: "APPROVAL_ISSUES_GET_PAGE_FAILED",
        setActivePage: "APPROVAL_ISSUES_SET_ACTIVE_PAGE",
        setActiveViewType: "APPROVAL_ISSUE_SET_ACTIVE_VIEW_TYPE",
        getIssueSucceeded: "APPROVAL_ISSUE_GET_SUCCEEDED",
        getIssueFailed: "APPROVAL_ISSUE_GET_FAILED",
        getIssueStarted: "APPROVAL_ISSUE_GET_STARTED",
        approveIssueSucceeded: "APPROVAL_ISSUE_APPROVE_SUCCEEDED",
        approveIssueFailed: "APPROVAL_ISSUE_APPROVE_FAILED",
        approveIssueStarted: "APPROVAL_ISSUE_APPROVE_STARTED",
        deleteIssueSucceeded: "APPROVAL_ISSUE_DELETE_SUCCEEDED",
        deleteIssueFailed: "APPROVAL_ISSUE_DELETE_FAILED",
        deleteIssueStarted: "APPROVAL_ISSUE_DELETE_STARTED"
    },
};
