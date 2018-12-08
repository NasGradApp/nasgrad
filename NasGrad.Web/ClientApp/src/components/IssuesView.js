import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { defaultPageSize, viewType } from '../constants';
import { actionCreators as issuesActionCreators } from '../store/issues.store';
import List from './List';
import IssueItem from './IssueItem';
import Map from './Map';

class IssuesView extends Component {
    componentWillMount() {
        this.props.getAllIssues();
    }

    render() {
        const { activePage, setActivePage, activeViewType, setActiveViewType, issues } = this.props;
        const totalPages = (issues === null) ? 0 : Math.floor((issues.length + defaultPageSize - 1) / defaultPageSize);
        const items = (issues === null) ? [] : issues.skip((activePage - 1) * defaultPageSize).take(defaultPageSize);

        const empty = (
            <div className="centar">
                <h3>Nema prijavljenih problem</h3>
            </div>
        );

        let view = "";
        let classNameList = "btn btn-warning";
        let classNameMap = "btn btn-warning";
        if (activeViewType === viewType.list) {
            view = (<List empty={empty} items={items} itemComponent={IssueItem} totalPages={totalPages} activePage={activePage} setActivePage={setActivePage} />);
            classNameList += " disabled";
        } else if (activeViewType === viewType.map) {
            view = (<Map items={items} />);
            classNameMap += " disabled";
        }

        return (
            <div>
                <h1>Problemi</h1>
                <div className="btn-toolbar" role="toolbar" aria-label="Toolbar with button groups" style={{ float: "right", marginTop: "-50px" }}>
                    <div className="btn-group mr-2" role="group" aria-label="First group">
                        <button type="button" className={classNameList} onClick={() => setActiveViewType(viewType.list)}>List</button>
                        <button type="button" className={classNameMap} onClick={() => setActiveViewType(viewType.map)}>Mapa</button>
                    </div>
                </div>
                <div>
                    {view}
                </div>
            </div>
        );
    }
}

export default connect(
    state => state.issues,
    dispatch => bindActionCreators(issuesActionCreators, dispatch)
)(IssuesView);
