import { actionTypes } from '../constants';
import * as pictureService from '../service/picture.service';

const actionType = actionTypes.picture;

export const actionCreators = {
    getAllPictures: () => async (dispatch, getState) => {
        if (getState().picture.isLoading) {
            // Prevent duplicate requests to the API
            return;
        }

        dispatch({ type: actionType.getPageStartedPicture });

        pictureService.getAllPictures().then(
            data => {
                dispatch({
                    type: actionType.getPageSucceededPicture,
                    data: data
                });
            },
            error => {
                dispatch({
                    type: actionType.getPageFailedCPicture,
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

    if (action.type === actionType.getPageStartedPicture) {
        return {
            ...state,
            error: null,
            isLoading: true
        };
    }

    if (action.type === actionType.getPageSucceededPicture) {
        return {
            ...state,
            data: action.data,
            error: null,
            isLoading: false
        };
    }

    if (action.type === actionType.getPageFailedCPicture) {
        return {
            ...state,
            data: null,
            error: action.error,
            isLoading: false
        };
    }
    return state;
};
