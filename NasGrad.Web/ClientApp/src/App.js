import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import IssueDetail from './components/IssueDetail';
import Home from './components/Home';
import IssueList from './components/IssueList';

const App = () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/issuedetail/:id([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})' component={IssueDetail} />
        <Route exact path='/issues' component={IssueList} />
    </Layout>
);


export default App;
