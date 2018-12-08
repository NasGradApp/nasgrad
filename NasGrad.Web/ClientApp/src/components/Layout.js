import React from 'react';
import NavMenu from './NavMenu';

const Layout = props => (
    <div>
        <nav>
            <NavMenu />
        </nav>
        <div className="container" style={{ marginTop: "85px" }}>
            {props.children}
        </div>
    </div>
);

export default Layout; 
