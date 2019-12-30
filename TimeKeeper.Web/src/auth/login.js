import React, { Component } from 'react'
import config from '../config'
import { Link } from 'react-router-dom'

class Login extends Component {
  render() {
    config.currentUser = { id: 1, name: 'Alexander Papadopulos', role: 'admin' }
    return (
      <Link to='/'>Back to main page</Link>
    )
  }
}

export default Login
