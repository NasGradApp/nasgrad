import React from 'react';

const NavMenu = props => (
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
                </ul>
            </div>
        </div>
    </nav>
);

export default NavMenu;
