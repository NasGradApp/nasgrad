import React, { Component } from 'react';

class IssueItem extends Component {
    render() {
        const { item } = this.props;
        return (
            <div className="row">
                <div className="col-md-12">
                    {item.issue.title}
                </div>
            </div>
        );
    }
}

export default IssueItem;
