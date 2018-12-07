import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import IssueDetail from './components/IssueDetail';
import Home from './components/Home';

const App = () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/issuedetail' component={IssueDetail} />
    </Layout>
);


export default App;
