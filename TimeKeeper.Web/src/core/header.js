import React, { Component } from 'react'
import Container from 'react-bootstrap/Container'
import config from '../config'
import Menu from './menu'
import Logo from 'Images/logo.png'

const styles = {
  header: {
    position: 'fixed',
    top: 0,
    height: '84px',
    color: 'white',
    backgroundColor: '#036',
    width: '100%',
    zIndex: 999,
    boxShadow: '12px 12px 12px 12px #ccc'
  },
  logo: {
    width: '56px',
    float: 'left',
    marginTop: '12px'
  },
  title: {
    float: 'left',
    fontSize: '2em',
    marginTop: '0px',
    marginLeft: '12px'
  }
}

class Header extends Component {
  render() {
    return (
      <div style={styles.header}>
        <Container>
          <img src={Logo} style={styles.logo} />
          <div style={styles.title}>
            TimeKeeper Application<br />
            <small>{config.currentUser.name}</small>
          </div>
          <Menu />
        </Container>
      </div>
    )
  }
}

export default Header