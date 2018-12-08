import 'bootstrap/dist/css/bootstrap.css';
import './index.css';
import React from 'react';
import { render } from 'react-dom';
import { BrowserRouter as Router } from 'react-router-dom';
import App from './App';
import { push } from 'react-router-redux';
import configureStore from './store/configureStore';
import registerServiceWorker from './registerServiceWorker';
import { actionCreators as userActionCreators } from './store/users.store';
import { arrayHelper } from './helpers/array.helper';

arrayHelper();

// Create browser history to use in the Redux store
//const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
//const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window.initialReduxState;
const store = configureStore(initialState);

const rootElement = document.getElementById('root');

render(
    <Router>
        <App store={store} />
    </Router>, rootElement);

registerServiceWorker();

// need to be global function because of circular referencing
window.handleHttp401 = function () {
    userActionCreators.unauthorized()(store.dispatch);
    store.dispatch(push("/login"));
};
