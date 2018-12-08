import React from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';

const NavMenu = props => (
    <Navbar inverse fixedTop fluid collapseOnSelect>
        <Navbar.Header>
            <Navbar.Brand>
                <Link to={'/'}>NasGrad.Web</Link>
            </Navbar.Brand>
            <Navbar.Toggle />
        </Navbar.Header>
        <Navbar.Collapse>
            <Nav>
                <LinkContainer to={'/'} exact>
                    <NavItem>
                        <Glyphicon glyph='home' /> Home
                    </NavItem>
                </LinkContainer>
                <LinkContainer to={"/issuedetail/1dcfc125-7df9-4d81-af3b-14c8c7b47bfe"} exact>
                    <NavItem>
                        <Glyphicon glyph="issuedetail" /> Issue Detail
                    </NavItem>
				</LinkContainer>
                <LinkContainer to={'/issues'} exact>
                    <NavItem>
                        <Glyphicon glyph='exclamation-sign' /> Issues
                    </NavItem>
                </LinkContainer>
            </Nav>
        </Navbar.Collapse>
    </Navbar>
);

export default NavMenu;
