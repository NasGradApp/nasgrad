import React from 'react';
import { Route } from 'react-router';
import { Provider } from 'react-redux'
import Layout from './components/Layout';
import IssueDetail from './components/IssueDetail';
import Home from './components/Home';
import IssuesView from './components/IssuesView';

const App = ({ store }) => (
    <Provider store={store}>
        <Layout>
            <Route exact path='/' component={Home} />
            <Route exact path='/issues' component={IssuesView} />
			<Route exact path='/issuedetail/:id([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})' component={IssueDetail} />
		</Layout>
    </Provider>
);


export default App;
