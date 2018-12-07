import React, { createRef, Component } from "react";
import { connect } from 'react-redux';
import { render } from 'react-dom';
import { Map, Marker, Popup, TileLayer } from 'react-leaflet';
import { bindActionCreators } from "redux";
import { actionCreators as issuesActionCreators } from '../store/issues.store';
import { Button, ButtonGroup, FormControl, Well, Label, Panel, FormGroup, HelpBlock, Popover, OverlayTrigger } from "react-bootstrap";

import L from "leaflet";
import "../leaflet/leaflet.css";
import "../leaflet/mapstyledevice.css";
import "../style/openStreetMap.css";
import "../style/pictureSettup.css";
import "../style/row.css";
import "../style/column.css";
import "../style/topMargin.css";

class IssueDetail extends Component {

    constructor(props) {
        super(props);
    }
    updatedIssueObject

    mapRef = createRef()

    updateIssue(e) {
        this.updatedIssueObject.pictures.visible === true;
        this.props.updateIssue(this.updateIssue.id, this.updatedIssueObject);
    }

    render() {
        const { id } = this.props.match.params;

        var listOfIssues = this.props.data.issues.filter(function (issue) {
            console.log(issue.issue.id);
            return issue.issue.id === id;
        });

        var issueObject = listOfIssues[0];
        var showForm = true;
        var error = false;

        if (Object.getOwnPropertyNames(issueObject).length === 0) {
            error = true;
            showForm = false;
        }

        this.updatedIssueObject = issueObject.issue;

        const dLat = issueObject.issue.location.langitude;
        const dLng = issueObject.issue.location.longitude;
        const picture = issueObject.issue.pictures.content;
        const isPictureVisible = issueObject.issue.pictures.visible;

        const hasLocation = (dLat && dLng);
        const locationPin = hasLocation ? L.latLng(dLat, dLng) : null;

        const errorHappened = (
            <div className="alert alert-danger col-sm-12">Nothing for update</div>
        );

        const approveButton = (isPictureVisible) ? (
            <Button
                bsStyle="info"
                href="#" type="submit"
                onClick={this.updateIssue(this)}
                disabled
            >Approve
                                            </Button>
        ) : (<Button
            bsStyle="info"
                href="#" type="submit"
                onClick={this.updateIssue(this)}
        >Approve
                                            </Button>);
        

        const clickOnHide = (
            isPictureVisible === false
        );

        const hideButton = (isPictureVisible) ? (
            <Button
                bsStyle="info"
                href="#"
                type="submit"
                onClick={clickOnHide}
            >Hide
             </Button>
        ) : (<Button
            bsStyle="info"
            href="#"
            type="submit"
            onClick={clickOnHide}
            disabled
        >Hide
            </Button>);

        const form = (
            <form>
                <div className="form-group">
                    <label>Naslov</label>
                    <input type="text" className="form-control" placeholder="Problem" readOnly="true" defaultValue={issueObject.issue.title} />
                </div>
                <div className="form-group">
                    <label>Kategorija:</label>
                    <ul>
                        {issueObject.issue.categories.map((item, rowIndex) =>
                            (<li key={item}>
                                {item}
                            </li>)
                        )}
                    </ul>
                </div>
                <div className="form-group">
                    <label>Tip problema:</label>
                    <input type="text" className="form-control" placeholder="Problem" readOnly="true" defaultValue={issueObject.issue["issue-type"]} />
                </div>
                <div className="form-group">
                    <label>Opis problema:</label>
                    <textarea className="form-control" placeholder="Detalji problema" readOnly="true" defaultValue={issueObject.issue.description} />
                </div>
                <div className="form-group">
                    <label>Status:</label>
                    <input type="text" className="form-control" placeholder="Problem" readOnly="true" defaultValue={issueObject.issue.state} />
                </div>

                {hasLocation ?
                    <div className="openStreetMapBlock">
                        <Map
                            center={locationPin}
                            length={4}
                            ref={this.mapRef}
                            zoom={17}>
                            <TileLayer
                                attribution="&amp;copy <a href=&quot;http://osm.org/copyright&quot;>OpenStreetMap</a> contributors"
                                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                            />
                            <Marker position={locationPin}>
                                <Popup>This is a device location</Popup>
                            </Marker>
                        </Map>
                    </div> : null
                }

            </form>
        );

        return (
            <div className="row">
                <div className="column">
                    <div className="panel panel-info">
                        <div className="panel-heading">
                            <div className="panel-title">Problem:</div>
                        </div>
                        <div className="panel-body">
                            {(error) ? errorHappened : ""}
                            {(showForm) ? form : ""}
                        </div>
                    </div>
                </div>
                <div className="column">
                    <div className="panel panel-info">
                        <div className="panel-heading">
                            <div className="panel-title">Slike:</div>
                        </div>
                        <div >
                            <img src={picture} className="pictureSettup" />
                        </div>
                        <div className="topMargin">
                            <ButtonGroup justified >
                                <OverlayTrigger delay={750} trigger={['hover', 'focus']} placement="bottom" >
                                    {approveButton}
                                </OverlayTrigger>
                                {hideButton}
                            </ButtonGroup>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default connect(
    state => state.issues,
    dispatch => bindActionCreators(issuesActionCreators, dispatch)
)(IssueDetail);
