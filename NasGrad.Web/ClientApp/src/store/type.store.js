import { actionTypes } from '../constants';
import * as typeService from '../service/type.service';

const actionType = actionTypes.types;

export const actionCreators = {
    getAllCategories: () => async (dispatch, getState) => {
        if (getState().types.isLoading) {
            // Prevent duplicate requests to the API
            return;
        }

        dispatch({ type: actionType.getPageStartedTypes });

        typeService.getAllTypes().then(
            data => {
                dispatch({
                    type: actionType.getPageSucceededTypes,
                    data: data
                });
            },
            error => {
                dispatch({
                    type: actionType.getPageFailedTypes,
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

    if (action.type === actionType.getPageStartedTypes) {
        return {
            ...state,
            error: null,
            isLoading: true
        };
    }

    if (action.type === actionType.getPageSucceededTypes) {
        return {
            ...state,
            data: action.data,
            error: null,
            isLoading: false
        };
    }

    if (action.type === actionType.getPageFailedTypes) {
        return {
            ...state,
            data: null,
            error: action.error,
            isLoading: false
        };
    }

    return state;
};
