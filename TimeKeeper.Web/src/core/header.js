import React, { Component } from 'react'
import Container from 'react-bootstrap/Container'
import Menu from './menu'
import Logo from 'Images/logo.png'

const styles = {
    logo: {
        width: '56px',
        float: 'left',
        marginTop: '12px'
    },
    title: {
        float: 'left',
        fontSize: '2em',
        marginTop: '12px',
        marginLeft: '12px'
    },
}

class Header extends Component {
    render() {
        return (
            <header>
                <Container>
                    <img src={Logo} style={styles.logo} />
                    <div style={styles.title}>TimeKeeper Application</div>
                    <Menu />
                </Container>
            </header>
        )
    }
}

export default Header