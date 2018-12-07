import React, { Component } from 'react';

class Pager extends Component {
    render() {
        const { selectPage, activePage, totalPages } = this.props;
        let pages = (totalPages > 1) ? Array.from(new Array(totalPages), (_, i) => i + 1) : [];
        return (
            <div className="btn-group" role="group">
                {pages.map(page => {
                    const disabled = activePage === page;
                    const onClick = () => {
                        selectPage(page);
                    };
                    return (
                        <button key={page} className='btn btn-default' disabled={disabled} onClick={onClick}>
                            {page}
                        </button>);
                })}
            </div>
        );
    }
}

export default Pager;
