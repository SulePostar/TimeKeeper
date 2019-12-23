import React, { Component } from 'react'
import { Link, withRouter } from 'react-router-dom'

const styles = {
  butt: {
    marginRight: '3px'
  }
}

class Menu extends Component {
  render() {
    return (
      <div className='menu'>
        <Link to="/"><button className='btn btn-sm btn-info' style={styles.butt}>Home</button></Link>
        <Link to="/services"><button className='btn btn-sm btn-info' style={styles.butt}>Services</button></Link>
        <Link to="/team"><button className='btn btn-sm btn-info' style={styles.butt}>Our Team</button></Link>
        <Link to="/contact"><button className='btn btn-sm btn-info' style={styles.butt}>Contact Us</button></Link>
        <Link to="/contact"><button className='btn btn-sm btn-info' style={styles.butt}>Login</button></Link>
      </div>
    )
  }
}

export default withRouter(Menu)

