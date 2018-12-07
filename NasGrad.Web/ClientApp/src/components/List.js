import React, { Component } from 'react';
import Item from './Item';
import Pager from './Pager';

class List extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const selectPage = page => {
            this.props.getPage(page);
        };

        return (
            <div>
                <div>
                    <Item />
                    <Item />
                    <Item />
                    <Item />
                </div>
                <div>
                    <Pager selectPage={selectPage} totalPages={5} activePage={2} />
                </div>
            </div>
        );
    }
}

export default List;
