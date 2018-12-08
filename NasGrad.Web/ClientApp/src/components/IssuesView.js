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
        let classNameList = "";
        let classNameMap = "";
        if (activeViewType === viewType.list) {
            view = (<List empty={empty} items={items} itemComponent={IssueItem} totalPages={totalPages} activePage={activePage} setActivePage={setActivePage} />);
            classNameList = "active";
        } else if (activeViewType === viewType.map) {
            view = (<Map items={items} />);
            classNameMap = "active";
        }

        return (
            <div>
                <h1>Problemi</h1>
                <div>
                    <ul className="nav nav-tabs">
                        <li role="presentation" className={classNameList}>
                            <a onClick={() => setActiveViewType(viewType.list)}>Lista</a>
                        </li>
                        <li role="presentation" className={classNameMap}>
                            <a onClick={() => setActiveViewType(viewType.map)}>Mapa</a>
                        </li>
                    </ul>
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
