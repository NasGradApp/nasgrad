import React, { Component } from 'react';

class IssueItem extends Component {
    render() {
        const { item } = this.props;
        const imgSrc = `data:image/jpeg;base64,${item.picturePreview}`;
        return (
            <div className="card card-bg" style={{ width: "300px", float: "left", marginRight: "30px" }}>
                <img className="card-img-top" src={imgSrc} style={{ width: "100%" }} />
                <div className="card-body" style={{ paddingLeft: "0.5rem", paddingRight: "0.5rem", minHeight: "120px" }}>
                    <h6 className="card-title">{item.title}</h6>
                    <span>{item.description}</span>
                </div>
            </div>
        );
    }
}

export default IssueItem;
