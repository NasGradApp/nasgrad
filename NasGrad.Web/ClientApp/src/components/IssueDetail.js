import React, { createRef, Component } from "react";
import { connect } from 'react-redux';
import { render } from 'react-dom';
import { Map, Marker, Popup, TileLayer } from 'react-leaflet';
import { bindActionCreators } from "redux";
import { actionCreators as issuesActionCreators } from '../store/issues.store';

import L from "leaflet";
import "../leaflet/leaflet.css";
import "../leaflet/mapstyledevice.css";
import "../style/openStreetMap.css";

class IssueDetail extends Component {
    issueId
    issueTitle
    issueDescription
    issueLng
    issueLat
    issueStatus

    constructor(props) {
        super(props);
    }

    mapRef = createRef()

    render() {
        const { id } = this.props.match.params;

        var listOfIssues = this.props.page.issues.filter(function (issue) {
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
        const dLat = issueObject.issue.location.langitude;
        const dLng = issueObject.issue.location.longitude;

        const hasLocation = (dLat && dLng);
        const locationPin = hasLocation ? L.latLng(dLat, dLng) : null;

        const openStreetMapBlock = {
            clear: "both",
            display: "block",
            marginTop: "20px"
        };

        const topMargin = {
            marginTop: "20px"
        };

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
                            {marker}
                        </Map>
                    </div> : null
                }
            </form>
        );

        const marker = (hasLocation && locationPin) ? (
            <Marker position={locationPin}>
                <Popup>This is a device location</Popup>
            </Marker>
        ) : null

        const errorHappened = (
            <div className="alert alert-danger col-sm-12">Nothing for update</div>
        );

        return (
            <div className="mainbox col-md-offset-3 col-md-6" style={topMargin}>
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
        );
    }
}

export default connect(
    state => state.issues,
    dispatch => bindActionCreators(issuesActionCreators, dispatch)
)(IssueDetail);
