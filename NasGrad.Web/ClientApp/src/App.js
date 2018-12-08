import React from 'react';
import { Route } from 'react-router';
import { Provider } from 'react-redux'
import Layout from './components/Layout';
import Home from './components/Home';
import IssuesView from './components/IssuesView';

const App = ({ store }) => (
    <Provider store={store}>
        <Layout>
            <Route exact path='/' component={Home} />
            <Route exact path='/issues' component={IssuesView} />
        </Layout>
    </Provider>
);


export default App;
