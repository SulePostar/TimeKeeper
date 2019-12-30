import React, { Component } from 'react'

const footer = {
  position: 'fixed',
  bottom: 0,
  fontSize: '0.8em',
  height: '32px',
  color: 'white',
  backgroundColor: '#036',
  width: '100%',
  zIndex: 999,
  paddingTop: '6px',
  textAlign: 'center'
}

class Footer extends Component {
  render() {
    return (
      <div style={footer}>
        Copyright &copy; Gigi School of Coding 2019. All rights reserved.
      </div>
    )
  }
}

export default Footer