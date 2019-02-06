import React, { Component } from 'react';

class ApprovalItem extends Component {
    render() {


        const { item, funcApprove, funcDelete } = this.props;
        const imgSrc = `data:image/jpeg;base64,${item.picturePreview}`;
        const urlDetails = `/approvalissuedetail/${item.id}`;
        const img = (<img className="card-img-top" src={imgSrc} style={{ width: "100%" }} />);
        return (
            <div className="card card-bg" style={{ width: "300px", float: "left", marginRight: "30px" }}>
                {(item.picturePreview) ? img : ""}
                <div className="card-body" style={{ paddingLeft: "0.5rem", paddingRight: "0.5rem", minHeight: "120px" }}>
                    <h6 className="card-title">{item.title}</h6>
                    <span>{item.description}</span>
                    <br />
                    <a href={urlDetails}>Detalji</a>
                    <br />
                    <br />
                    <div>
                        <button style={{ float: "left" }} type="button" className={"btn btn-success"}
                            onClick={() => funcApprove(item.id)}>
                            <span className="glyphicon glyphicon-ok"></span> Dozvoli
                        </button >
                        
                        <button style={{ float: "right" }} type="button" className="btn btn-danger" onClick={() => funcDelete(item.id)}>
                            <span className="glyphicon glyphicon-remove"></span> Obrisi!
                        </button>
                    </div>
                </div>
            </div>
        );
    }
}

export default ApprovalItem;
