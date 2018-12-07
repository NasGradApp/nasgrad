import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators as issuesActionCreators } from '../store/issues.store';
import List from './List';

class IssueList extends Component {
    componentWillMount() {
        this.props.getPage(0);
    }
    render() {
        return (
            <div>
                <List getPage={this.props.getPage} />
            </div>);
    }
}

export default connect(
    state => state.issues,
    dispatch => bindActionCreators(issuesActionCreators, dispatch)
)(IssueList);
