import React, { Component, createRef } from 'react';
import { Map, Marker, Popup, TileLayer } from "react-leaflet";

import L from "leaflet";
import "../leaflet/leaflet.css";
import "../leaflet/mapstyledevice.css";
import "../leaflet/openStreetMap.css";

L.Icon.Default.imagePath = "//cdnjs.cloudflare.com/ajax/libs/leaflet/1.0.0/images/";

const defaultLat = 45.257169;
const defaultLng = 19.844719;

class List extends Component {
    mapRef = createRef()

    render() {
        const { items } = this.props;

        let mapCenter = {
            lat: defaultLat,
            lng: defaultLng
        };

        return (
            <div>
                <div className="openStreetMapBlock">
                    <Map center={mapCenter}
                        length={4} ref={this.mapRef} zoom={15}>
                        <TileLayer
                            attribution="&amp;copy <a href=&quot;http://osm.org/copyright&quot;>OpenStreetMap</a> contributors"
                            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        />
                    </Map>
                </div>
            </div>
        );
    }
}

export default List;
