import React, { Component } from "react";
import { Redirect } from "react-router";
import { bindActionCreators } from "redux";
import { connect } from "react-redux";
import { actionCreators as userActionCreators } from "../store/users.store";

class Login extends Component {
    txtUsername
    txtPassword
    btnLogin
    constructor(props) {
        super(props);
        this.state = {
            hasUsername: true,
            hasPassword: true
        }
    }

    componentWillUnmount() {
        this.props.clearError();
    }

    onClickLogin(e) {
        e.preventDefault();
        const username = this.txtUsername.value;
        const password = this.txtPassword.value;
        const newState = {
            hasUsername: username !== "",
            hasPassword: password !== ""
        };
        this.setState(newState);
        this.props.clearError();
        if (newState.hasUsername && newState.hasPassword) {
            this.props.userLogin(username, password);
        }
        this.btnLogin.blur();
    }

    render() {
        const { user, error, isLoading } = this.props;
        const { hasUsername, hasPassword } = this.state;
        if (user) {
            return (
                <Redirect to="/approval/allItems" />
            );
        }

        const usernameClassName = "form-group" + ((!hasUsername) ? " has-error has-danger" : "");
        const passwordClassName = "form-group" + ((!hasPassword) ? " has-error has-danger" : "");

        return (
            <div className="mainbox col-md-offset-3 col-md-6" style={{ marginTop: "20px" }}>
                <div className="panel panel-info" >

                    <div className="panel-heading">
                        <div className="panel-title">Login</div>
                    </div>

                    <div className="panel-body">
                        <form onSubmit={this.onClickLogin.bind(this)}>
                            {(error) ? <div className="alert alert-danger col-sm-12">{error}</div> : ""}
                            <div className={usernameClassName}>
                                <div className="input-group">
                                    <span className="input-group-addon">
                                        <i className="glyphicon glyphicon-user"></i>
                                    </span>
                                    <input type="text" className="form-control" placeholder="Username" ref={el => this.txtUsername = el} />
                                </div>
                                {(!hasUsername) ? <div className="help-block with-errors">Please provide username</div> : ""}
                            </div>

                            <div className={passwordClassName}>
                                <div className="input-group">
                                    <span className="input-group-addon">
                                        <i className="glyphicon glyphicon-lock"></i>
                                    </span>
                                    <input type="password" className="form-control" placeholder="Password" ref={el => this.txtPassword = el} />
                                </div>
                                {(!hasPassword) ? <div className="help-block with-errors">Please provide password</div> : ""}
                            </div>
                            <div className="input-group">
                                <button className="btn btn-success" disabled={isLoading} ref={el => this.btnLogin = el}
                                    type="submit" onClick={this.onClickLogin.bind(this)}>
                                    <span className="glyphicon glyphicon-log-in"></span> Login</button>
                                {(isLoading) ? <span style={{ marginLeft: "15px" }}>Loading...</span> : ""}
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        );
    }
}

export default connect(
    state => state.users,
    dispatch => bindActionCreators(userActionCreators, dispatch)
)(Login);
