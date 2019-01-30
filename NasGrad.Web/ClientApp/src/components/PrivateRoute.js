import React, { Component } from "react";
import { Route, Redirect } from "react-router";
import { withRouter } from 'react-router-dom';
import { connect } from "react-redux";

class PrivateRoute extends Component {
    isAuthorized(user) {
        if (this.props.authorizedIfAdmin && !user.is_user_admin) {
            return false;
        }

        return true;
    }

    render() {
        const { component: Component, user, ...rest } = this.props;

        const renderRoute = props => {
            if (user && this.isAuthorized(user)) {
                return (
                    <Component {...props} />
                );
            }

            const to = {
                pathname: '/login',
                state: {
                    from: props.location
                }
            };

            return (
                <Redirect to={to} />
            );
        }

        return (
            <Route {...rest} render={renderRoute} />
        );
    }
}

export default withRouter(connect(
    state => state.users
)(PrivateRoute));