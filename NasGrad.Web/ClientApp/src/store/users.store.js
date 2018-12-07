import * as usersService from "../service/users.service";
import { actionTypes, storageKeys } from "../constants";

const actionType = actionTypes.users;

export const actionCreators = {
    userLogin: (username, password) => async (dispatch, getState) => {
        if (getState().user.isLoading) {
            // Prevent duplicate requests to the API
            return;
        }

        dispatch({ type: actionType.loginStarted });

        usersService.login(username, password).then(
            user => {
                usersService.saveLoginData(user);
                dispatch({
                    type: actionType.loginSucceeded,
                    user: user
                });
            },
            error => {
                dispatch({
                    type: actionType.loginFailed,
                    error: error
                });
            }
        );
    },
    userLogout: () => async (dispatch, getState) => {
        usersService.logout();
        dispatch({
            type: actionType.logout
        });
    },
    clearError: () => async (dispatch, getState) => {
        dispatch({
            type: actionType.clearError
        });
    },
    unauthorized: () => async (dispatch, getState) => {
        usersService.logout();
        dispatch({
            type: actionType.unauthorized
        });
    },
    saveUserLogin: (user) => async (dispatch, getState) => {
        usersService.saveLoginData(user);
        dispatch({
            type: actionType.loginSucceeded,
            user: user
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

    if (action.type === actionType.loginStarted) {
        return {
            ...state,
            error: null,
            isLoading: true
        };
    }

    if (action.type === actionType.loginSucceeded) {
        return {
            ...state,
            user: action.user,
            error: null,
            isLoading: false
        };
    }

    if (action.type === actionType.loginFailed) {
        return {
            ...state,
            user: null,
            error: action.error,
            isLoading: false
        };
    }

    if (action.type === actionType.logout) {
        return {
            ...state,
            user: null
        };
    }

    if (action.type === actionType.clearError) {
        return {
            ...state,
            error: null
        };
    }

    if (action.type === actionType.unauthorized) {
        return {
            ...state,
            error: "You are not authorized for requested action.",
            user: null
        };
    }

    return state;
};
