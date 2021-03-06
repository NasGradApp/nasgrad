﻿import { applyMiddleware, combineReducers, compose, createStore } from 'redux';
import thunk from 'redux-thunk';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import * as usersStore from './users.store';
import * as issuesStore from './issues.store';
import * as categoryStore from './category.store';
import * as typesStore from './type.store';
import * as pictureStore from './picture.store';
import * as approvalStore from './approval.store';

export default function configureStore(initialState) {
    const reducers = {
        users: usersStore.reducer,
        issues: issuesStore.reducer,
        category: categoryStore.reducer,
        types: typesStore.reducer,
        picture: pictureStore.reducer,
        approvalIssues: approvalStore.reducer
};

    const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
    const history = createBrowserHistory({ basename: baseUrl });

    const middleware = [
        thunk,
        routerMiddleware(history)
    ];

    // In development, use the browser's Redux dev tools extension if installed
    const enhancers = [];
    const isDevelopment = process.env.NODE_ENV === 'development';
    if (isDevelopment && typeof window !== 'undefined' && window.devToolsExtension) {
        enhancers.push(window.devToolsExtension());
    }

    const rootReducer = combineReducers({
        ...reducers,
        routing: routerReducer
    });

    return createStore(
        rootReducer,
        initialState,
        compose(applyMiddleware(...middleware), ...enhancers)
    );
}
