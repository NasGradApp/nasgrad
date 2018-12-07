import React, { Component } from 'react';

class Item extends Component {
    render() {
        const { item, itemMapping } = this.props;
        return (
            <div className="row">
                <div className="col-md-12">
                    {item.title}
                </div>
            </div>
        );
    }
}

export default Item;
