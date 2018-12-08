import { actionTypes } from '../constants';
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
                    data: data
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

    
};



// initial store state
const initialState = {
    isLoading: false,
    error: null,
    data: null,
    activePage: 1
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
            data: action.data,
            error: null,
            isLoading: false
        };
    }

    if (action.type === actionType.getPageFailed) {
        return {
            ...state,
            data: null,
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
    return state;
};
