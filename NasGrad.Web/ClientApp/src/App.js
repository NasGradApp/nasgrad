import React from 'react';
import { Route } from 'react-router';
import { Provider } from 'react-redux'
import Layout from './components/Layout';
import IssueDetail from './components/IssueDetail';
import Home from './components/Home';
import IssuesView from './components/IssuesView';
import Login from "./components/Login";
import PrivateRoute from "./components/PrivateRoute";
import ApprovalList from "./components/ApprovalList";
import ApprovalIssueDetail from "./components/ApprovalIssueDetail";

const App = ({ store }) => (
    <Provider store={store}>
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path="/login" component={Login} />
            <PrivateRoute exact path="/approval/allItems" component={ApprovalList} authorizedIfAdmin />
            <PrivateRoute exact path='/approvalissuedetail/:id([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})' component={ApprovalIssueDetail} authorizedIfAdmin />
            <Route exact path='/issues' component={IssuesView} />
			<Route exact path='/issuedetail/:id([0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12})' component={IssueDetail} />
		</Layout>
    </Provider>
);


export default App;
