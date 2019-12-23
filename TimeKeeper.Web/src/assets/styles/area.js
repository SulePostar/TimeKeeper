import React, { Component } from 'react'

const textStyle = {
    width: '130%',
    margin: '2px',
    padding: '4px',
    backgroundColor: '#fff',
    color: '#369',
    border: '1px solid #036',
    fontSize: '1.2em',
    boxShadow: '4px 4px 6px #888'
}

class Area extends Component {
    render() {
        return (
            <textarea style={textStyle} name={this.props.name} value={this.props.value} onChange={this.props.onChange} rows={this.props.rows}></textarea>
        )
    }
}

export default Area