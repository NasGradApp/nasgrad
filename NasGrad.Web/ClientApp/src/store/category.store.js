import { actionTypes } from '../constants';
import * as categoryService from '../service/category.service';

const actionType = actionTypes.category;

export const actionCreators = {
    getAllCategories: () => async (dispatch, getState) => {
        if (getState().category.isLoading) {
            // Prevent duplicate requests to the API
            return;
        }

        dispatch({ type: actionType.getPageStartedCategory });

        categoryService.getAllCategories().then(
            data => {
                dispatch({
                    type: actionType.getPageSucceededCategory,
                    data: data
                });
            },
            error => {
                dispatch({
                    type: actionType.getPageFailedCategory,
                    error: error
                });
            }
        );
    },
    setActivePage: (page) => async (dispatch, getState) => {
        dispatch({
            type: actionType.setActivePageCategory,
            activePage: page
        });
    }
    
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

    if (action.type === actionType.getPageStartedCategory) {
        return {
            ...state,
            error: null,
            isLoading: true
        };
    }

    if (action.type === actionType.getPageSucceededCategory) {
        return {
            ...state,
            data: action.data,
            error: null,
            isLoading: false
        };
    }

    if (action.type === actionType.getPageFailedCategory) {
        return {
            ...state,
            data: null,
            error: action.error,
            isLoading: false
        };
    }

    if (action.type === actionType.setActivePageCategory) {
        return {
            ...state,
            activePage: action.activePage
        };
    }
    
    return state;
};
