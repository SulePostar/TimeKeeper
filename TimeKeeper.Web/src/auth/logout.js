import React, { Component } from 'react'
import config from '../config'
import { Link } from 'react-router-dom'

class Logout extends Component {
  render() {
    config.currentUser = { id: 0, name: '', role: '' }
    return (
      <Link to='/'>Back to main page</Link>
    )
  }
}

export default Logout