import React, { createRef, Component } from "react";
import { connect } from 'react-redux';
import { render } from 'react-dom';
import { Map, Marker, Popup, TileLayer } from 'react-leaflet';
import { bindActionCreators } from "redux";
import { actionCreators as issuesActionCreators } from '../store/issues.store';
import { actionCreators as categoryActionCreators } from '../store/category.store';
import { actionCreators as typeActionCreators } from '../store/type.store';
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

    mapRef = createRef()

    componentWillMount() {
        const { id } = this.props.match.params;
        this.props.issuesActionCreators.getIssue(id);
        this.props.categoryActionCreators.getAllCategories();
        this.props.typeActionCreators.getAllCategories();
    }

    updateIssue(e) {
        // this.updatedIssueObject.pictures.visible === true;
        // this.props.updateIssue(this.updateIssue.id, this.updatedIssueObject);
    }

    getCategoryName(id) {
        var allCategories = this.props.category.data;
        for (var i = 0; i < allCategories.length; ++i) {
            if (allCategories[i].id === id.item) {
                return allCategories[i].name;
            }
        }
        return null;
    }

    getProblemType(id) {
        var allTypes = this.props.types.data;
        for (var i = 0; i < allTypes.length; ++i) {
            if (allTypes[i].id === id) {
                return allTypes[i].name;
            }
        }
        return null;
    }
    
    getStatusIssue(id) {
        const statusIssue = {
            "1": 'Submitted',
            "2": 'Reported',
            "3": 'Done'
        };

        for (let [key, value] of Object.entries(statusIssue)) {
            if (key === id.toString()) {
                return value;
            }
        }
        return null;
    };

    render() {

        const issueObject = this.props.issues.issue;
        const allCategories = this.props.category.data;
        const allTypes = this.props.types.data;

        if ((issueObject === null || issueObject === undefined) || (allCategories === null || allCategories === undefined) || (allTypes === null || allTypes === undefined)) {
            return "";
        }

        var showForm = true;
        var error = false;

        if (Object.getOwnPropertyNames(issueObject).length === 0 || Object.getOwnPropertyNames(allCategories).length === 0) {
            error = true;
            showForm = false;
        }

        const dLat = issueObject.location.langitude;
        const dLng = issueObject.location.longitude;
        const picture = issueObject.pictures.content;
        const isPictureVisible = true;

        const hasLocation = (dLat && dLng);
        const locationPin = hasLocation ? L.latLng(dLat, dLng) : null;

        const errorHappened = (
            <div className="alert alert-danger col-sm-12">Nothing for update</div>
        );



        const approveButton = (isPictureVisible) ? (
            <Button
                bsStyle="info"
                href="#" type="submit"
            >Approve
                                            </Button>
        ) : (<Button
            bsStyle="info"
            href="#" type="submit"
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
            >Hide
             </Button>
        ) : (<Button
            bsStyle="info"
            href="#"
            type="submit"
            disabled
        >Hide
            </Button>);

        const form = (
            <form>
                <div className="form-group">
                    <label>Naslov</label>
                    <input type="text" className="form-control" placeholder="Naslov" readOnly="true" defaultValue={issueObject.title} />
                </div>
                <div className="form-group">
                    <label>Kategorija:</label>
                    <ul>
                        {issueObject.categories.map((item, rowIndex) =>
                            (<li key={item}>
                                {this.getCategoryName({ item })}
                            </li>)
                        )}
                    </ul>
                </div>
                <div className="form-group">
                    <label>Tip problema:</label>
                    <input type="text" className="form-control" placeholder="Tip problema" readOnly="true" defaultValue={this.getProblemType(issueObject.issueType)} />
                </div>
                <div className="form-group">
                    <label>Opis problema:</label>
                    <textarea className="form-control" placeholder="Detalji problema" readOnly="true" defaultValue={issueObject.description} />
                </div>
                <div className="form-group">
                    <label>Status:</label>
                    <input type="text" className="form-control" placeholder="Status" readOnly="true" defaultValue={this.getStatusIssue(issueObject.state)} />
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
                            {(this.props.issues.isLoading || this.props.category.isLoading || this.props.types.isLoading) ? <p>Loading...</p> : null}
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
    state => {
        return {
            issues: state.issues,
            category: state.category,
            types: state.types
        };
    },
    dispatch => {
        return {
            issuesActionCreators: bindActionCreators(issuesActionCreators, dispatch),
            categoryActionCreators: bindActionCreators(categoryActionCreators, dispatch),
            typeActionCreators: bindActionCreators(typeActionCreators, dispatch)
        };
    }
)(IssueDetail);
