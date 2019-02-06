import React, { Component } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import { actionCreators as userActionCreators } from "../store/users.store";

class NavMenu extends Component {
    onClickLogout(e) {
        e.preventDefault();
        this.props.userLogout();
    }
    render() {
        const { user } = this.props;
        const message = (
            <li className="nav-item">
                <a className="nav-link js-scroll-trigger" onClick={this.onClickLogout.bind(this)} href="#">Logout</a>
            </li>
        );

return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark fixed-top nav-bg" id="mainNav">
        <div className="container">
            <a className="navbar-brand js-scroll-trigger" href="#page-top">
                <img src="/logo.png" />
            </a>
            <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarResponsive" aria-controls="navbarResponsive" aria-expanded="false" aria-label="Toggle navigation">
                <span className="navbar-toggler-icon" />
            </button>
            <div className="collapse navbar-collapse" id="navbarResponsive">
                <ul className="navbar-nav ml-auto">
                    <li className="nav-item">
                        <a className="nav-link js-scroll-trigger" href="/issues">Problemi</a>
                    </li>
                    {(user) ? message : ""}
                </ul>
            </div>
        </div>
    </nav>
);
}
}

export default connect(
    state => state.users,
    dispatch => bindActionCreators(userActionCreators, dispatch)
)(NavMenu);

