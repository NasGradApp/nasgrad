import React, { Component } from 'react';

class IssueItem extends Component {
    render() {
        const { item } = this.props;
        const imgSrc = `data:image/jpeg;base64,${item.picturePreview}`;
        return (
            <div className="row">
                <div className="col-md-12">
                    <div className="thumbnail" style={{ height: "100px", float: "left", border: "none", boxShadow: "none" }}>
                        <img src={imgSrc} style={{ float: "left", height: "100px" }} />
                    </div>
                    <h3>{item.title}</h3>
                    <div>
                        <span>{item.description}</span>
                    </div>
                    <div style={{ float: "left" }}>
                        <a href="#">Details</a>
                    </div>
                </div>
            </div>
        );
    }
}

export default IssueItem;
