import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { defaultPageSize } from '../constants';
import { actionCreators as issuesActionCreators } from '../store/issues.store';
import List from './List';
import IssueItem from './IssueItem';

class IssueList extends Component {
    componentWillMount() {
        // this.props.getPage(0);
    }
    render() {
        const { activePage, setActivePage } = this.props;
        const { issues } = this.props.data;
        const totalPages = Math.floor((issues.length + defaultPageSize - 1) / defaultPageSize);
        const items = issues.skip((activePage - 1) * defaultPageSize).take(defaultPageSize);

        const empty = (
            <div>
                Empty list
            </div>
        );

        return (
            <div>
                <List empty={empty} items={items} itemComponent={IssueItem} totalPages={totalPages}
                    activePage={activePage} setActivePage={setActivePage}
                />
            </div>
        );
    }
}

export default connect(
    state => state.issues,
    dispatch => bindActionCreators(issuesActionCreators, dispatch)
)(IssueList);
