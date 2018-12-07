import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import IssueList from './components/IssueList';

const App = () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/issues' component={IssueList} />
    </Layout>
);


export default App;
