import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { defaultPageSize, viewType } from '../constants';
import { actionCreators as aprovalActionCreators } from '../store/approval.store';
import List from './List';
import ApprovalItem from './ApprovalItem';

class ApprovalList extends Component {
    componentWillMount() {
        this.props.getAllIssues();
    }
    
    render() {
        const handleApproveIssue = id => {
            var retVal = window.confirm('Da li ste sigurni da zelite da dozvolite ovaj prijavljeni problem ?');
            if (retVal == true) {
                this.props.approveIssue(id);
            } 

           
            
        }

        const handleDeleteIssue = id => {
            var retVal = window.confirm('Da li ste sigurni da zelite da obrisete ovaj prijavljeni problem ?');
            if (retVal == true) {
                this.props.deleteIssue(id);
            }
        }

        const { activePage, setActivePage, activeViewType, setActiveViewType, issues } = this.props;
        const totalPages = (issues === null) ? 0 : Math.floor((issues.length + defaultPageSize - 1) / defaultPageSize);
        const items = (issues === null) ? [] : issues.skip((activePage - 1) * defaultPageSize).take(defaultPageSize);

        const empty = (
            <div className="centar">
                <h3>Nema prijavljenih problema</h3>
            </div>
        );

        return (
            <div>
                <h1>Problemi za approval</h1>
                <div>
                    <List empty={empty} items={items} itemComponent={ApprovalItem} funcApprove={handleApproveIssue} funcDelete={handleDeleteIssue} totalPages={totalPages} activePage={activePage} setActivePage={setActivePage} />
                </div>
            </div>
        );
    }
}

export default connect(
    state => state.approvalIssues,
    dispatch => bindActionCreators(aprovalActionCreators, dispatch)
)(ApprovalList);
