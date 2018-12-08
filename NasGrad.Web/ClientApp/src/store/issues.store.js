import { actionTypes, viewType } from '../constants';
import * as issuesService from '../service/issues.service';

const actionType = actionTypes.issues;

export const actionCreators = {
    getAllIssues: () => async (dispatch, getState) => {
        if (getState().issues.isLoading) {
            // Prevent duplicate requests to the API
            return;
        }

        dispatch({ type: actionType.getPageStarted });

        issuesService.getAllIssues().then(
            data => {
                dispatch({
                    type: actionType.getPageSucceeded,
                    issues: data
                });
            },
            error => {
                dispatch({
                    type: actionType.getPageFailed,
                    error: error
                });
            }
        );
    },
    setActivePage: (page) => async (dispatch, getState) => {
        dispatch({
            type: actionType.setActivePage,
            activePage: page
        });
    },
    getIssue: (id) => async (dispatch, getState) => {
        if (getState().issues.isLoading) {
            // Prevent duplicate requests to the API
            return;
        }

        dispatch({ type: actionType.getIssueStarted });

        issuesService.getIssue(id).then(
            data => {
                dispatch({
                    type: actionType.getIssueSucceeded,
                    data: data
                });
            },
            error => {
                dispatch({
                    type: actionType.getIssueFailed,
                    error: error
                });
            }
        );
    },
    setActiveViewType: (type) => async (dispatch, getState) => {
        dispatch({
            type: actionType.setActiveViewType,
            activeViewType: type
        });
    }
};



// initial store state
const initialState = {
    isLoading: false,
    error: null,
    data: null,
    issues: null,
    activePage: 1,
    activeViewType: viewType.list
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === actionType.getPageStarted) {
        return {
            ...state,
            error: null,
            isLoading: true
        };
    }

    if (action.type === actionType.getPageSucceeded) {
        return {
            ...state,
            issues: action.issues,
            error: null,
            isLoading: false
        };
    }

    if (action.type === actionType.getPageFailed) {
        return {
            ...state,
            issues: null,
            error: action.error,
            isLoading: false
        };
    }

    if (action.type === actionType.setActivePage) {
        return {
            ...state,
            activePage: action.activePage
        };
    }
    
    if (action.type === actionType.getIssueStarted) {
        return {
            ...state,
            error: null,
            isLoading: true
        };
    }

    if (action.type === actionType.getIssueSucceeded) {
        return {
            ...state,
            issue: action.data,
            isLoading: false
        };
    }

    if (action.type === actionType.getIssueFailed) {
        return {
            ...state,
            issue: null,
            error: action.error,
            isLoading: false
        };
    }
    if (action.type === actionType.setActiveViewType) {
        return {
            ...state,
            activeViewType: action.activeViewType
        };
    }

    return state;
};
