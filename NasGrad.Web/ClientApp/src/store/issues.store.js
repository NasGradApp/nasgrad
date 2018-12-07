import { actionTypes } from '../constants';
import * as issuesService from '../service/issues.service';

const actionType = actionTypes.issues;

export const actionCreators = {
    getPage: (page, pageSize) => async (dispatch, getState) => {
        if (getState().issues.isLoading) {
            // Prevent duplicate requests to the API
            return;
        }

        dispatch({ type: actionType.getPageStarted });

        issuesService.getPage(page, pageSize).then(
            data => {
                dispatch({
                    type: actionType.getPageSucceeded,
                    page: data,
                    activePage: page
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

    getItem: () => { }
};

// initial store state
const initialState = {
    isLoading: false,
    error: null,
    page: {
        "count": 2,
        "issues": [
            {
                "issue": {
                    "id": "95410c0d-5d14-4407-b2b2-2bc1891014e5",
                    "owner-id": "4abd7bff-8f4f-4fe3-8f01-40e742eff59a",
                    "title": "Rupa u putu na Trifkovic Trgu",
                    "description": "to je to nemam sta drugo da dodam",
                    "issue-type": "Rupa u putu",
                    "state": "submited",
                    "pictures": [
                        "pic-1",
                        "pic-2"
                    ],
                    "categories": [
                        "JKP Put",
                        "JKP Zelenilo"
                    ],
                    "location": {
                        "langitude": 45.25695,
                        "longitude": 19.844151
                    }
                },
                "picture-preview": "AAEC"
            },
            {
                "issue": {
                    "id": "4eec31f1-3424-47e2-8fa1-f61a94453677",
                    "owner-id": "781071cd-16c2-4bb5-a978-5086b8a822d7",
                    "title": "Rupa koja nije na Trifkovic Trgu",
                    "description": "to je to nemam sta drugo da dodam",
                    "issue-type": "Rupa u putu",
                    "state": "submited",
                    "pictures": [
                        "pic-3",
                        "pic-4"
                    ],
                    "categories": [
                        "JKP Put",
                        "JKP Zelenilo"
                    ],
                    "location": {
                        "langitude": 46.25695,
                        "longitude": 20.844151
                    }
                },
                "picture-preview": "AAEC"
            }
        ]
    },
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
            page: action.page,
            error: null,
            isLoading: false
        };
    }

    if (action.type === actionType.getPageFailed) {
        return {
            ...state,
            page: null,
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

    return state;
};
