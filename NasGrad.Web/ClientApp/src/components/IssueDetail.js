import React, { Component } from "react";
import { connect } from 'react-redux';
import { render } from 'react-dom';
import { Map, Marker, Popup, TileLayer } from 'react-leaflet';

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

    render() {
        const showForm = "true";
        const position = [51.505, -0.09];

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
                    <label>Problem:</label>
                    <input type="text" className="form-control" placeholder="Problem" readOnly="true" />
                </div>
                <div className="form-group">
                    <label>Detalji problema:</label>
                    <textarea className="form-control" placeholder="Detalji problema" readOnly="true" />
                </div>
                <div className="form-group">
                    <label>Kategorije:</label>
                    <ul>
                        <li>Prva kategorija</li>
                        <li>Druga kategorija</li>
                    </ul>
                </div>

                <Map center={position} zoom={13}>
                    <TileLayer
                        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        attribution="&copy; <a href=&quot;http://osm.org/copyright&quot;>OpenStreetMap</a> contributors"
                    />
                    <Marker position={position}>
                        <Popup>A pretty CSS3 popup.<br />Easily customizable.</Popup>
                    </Marker>
                </Map>
            </form>
            
        );

        return (
            <div className="mainbox col-md-offset-3 col-md-6" style={topMargin}>
                <div className="panel panel-info">
                    <div className="panel-heading">
                        <div className="panel-title">Opis problema</div>
                    </div>
                    <div className="panel-body">
                        {(showForm) ? form : ""}
                    </div>
                </div>
            </div>
        );
    }
}
/*
const IssueDetail = props => (
    <div>
        <h1>Hello, world!</h1>
        <p>Welcome to your new single-page application, built with:</p>
        <ul>
            <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
            <li><a href='https://facebook.github.io/react/'>React</a> and <a href='https://redux.js.org/'>Redux</a> for client-side code</li>
            <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
        </ul>
        <p>To help you get started, we've also set up:</p>
        <ul>
            <li><strong>Client-side navigation</strong>. For example, click <em>Counter</em> then <em>Back</em> to return here.</li>
            <li><strong>Development server integration</strong>. In development mode, the development server from <code>create-react-app</code> runs in the background automatically, so your client-side resources are dynamically built on demand and the page refreshes when you modify any file.</li>
            <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and your <code>dotnet publish</code> configuration produces minified, efficiently bundled JavaScript files.</li>
        </ul>
        <p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p>
    </div>
);*/

export default connect()(IssueDetail);
