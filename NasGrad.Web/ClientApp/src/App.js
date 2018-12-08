﻿import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import IssuesView from './components/IssuesView';

const App = () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/issues' component={IssuesView} />
    </Layout>
);


export default App;
