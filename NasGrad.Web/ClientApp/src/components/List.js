import React, { Component } from 'react';
import Pager from './Pager';

class List extends Component {
    render() {
        const { items, activePage, setActivePage, totalPages, empty, itemComponent } = this.props;
        if (!items || items.length === 0) {
            return empty;
        }

        const selectPage = (page) => {
            setActivePage(page);
        };

        return (

            <div>
                <div>
                    {items.map(item => {
                        const data = {
                            item,
                            key: item.id
                        };

                        return React.createElement(itemComponent, data);
                    })}
                </div>
                <div>
                    <Pager selectPage={selectPage} activePage={activePage} totalPages={totalPages} />
                </div>
            </div>
        );
    }
}

export default List;
