import * as userService from "../service/user.service";
import { actionTypes, storageKeys } from "../constants";

const userActionType = actionTypes.User;

export const actionCreators = {
    userLogin: (username, password) => async (dispatch, getState) => {
        if (getState().user.isLoading) {
            // Prevent duplicate requests to the API
            return;
        }

        dispatch({ type: userActionType.loginStarted });

        userService.login(username, password).then(
            user => {
                userService.saveLoginData(user);
                dispatch({
                    type: userActionType.loginSucceeded,
                    user: user
                });
                dispatch({
                    type: actionTypes.RESET
                });
            },
            error => {
                dispatch({
                    type: userActionType.loginFailed,
                    error: error
                });
            }
        );
    },
    userLogout: () => async (dispatch, getState) => {
        userService.logout();
        dispatch({
            type: userActionType.logout
        });
    },
    clearError: () => async (dispatch, getState) => {
        dispatch({
            type: userActionType.clearError
        });
    },
    unauthorized: () => async (dispatch, getState) => {
        userService.logout();
        dispatch({
            type: userActionType.unauthorized
        });
    },
    saveUserLogin: (user) => async (dispatch, getState) => {
        userService.saveLoginData(user);
        dispatch({
            type: userActionType.loginSucceeded,
            user: user
        });
        dispatch({
            type: actionTypes.RESET
        });
    }
};

// initial store state
const initialState = {
    user: JSON.parse(localStorage.getItem(storageKeys.user)),
    error: null,
    isLoading: false
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === userActionType.loginStarted) {
        return {
            ...state,
            error: null,
            isLoading: true
        };
    }

    if (action.type === userActionType.loginSucceeded) {
        return {
            ...state,
            user: action.user,
            error: null,
            isLoading: false
        };
    }

    if (action.type === userActionType.loginFailed) {
        return {
            ...state,
            user: null,
            error: action.error,
            isLoading: false
        };
    }

    if (action.type === userActionType.logout) {
        return {
            ...state,
            user: null
        };
    }

    if (action.type === userActionType.clearError) {
        return {
            ...state,
            error: null
        };
    }

    if (action.type === userActionType.unauthorized) {
        return {
            ...state,
            error: "You are not authorized for requested action.",
            user: null
        };
    }

    return state;
};
