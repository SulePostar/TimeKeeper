import React, { Component } from 'react'

const textStyle = {
    width: '100%',
    margin: '2px',
    padding: '4px',
    backgroundColor: '#fff',
    border: '1px solid #036',
    color: '#036',
    fontSize: '1.2em',
    boxShadow: '4px 4px 6px #888'
}

class Text extends Component {
    render() {
        return (
            <input style={textStyle} name={this.props.name} value={this.props.value} onChange={this.props.onChange} />
        )
    }
}

export default Text