import React, { Component } from 'react'
import Container from 'react-bootstrap/Container'
import Alert from 'react-bootstrap/Alert'
import CrowdImage from 'Images/crowd.jpg'

class Home extends Component {
  render() {
    return (
        <Container>
          <Alert variant="light" className='paper'>
            <Alert.Heading>About Us</Alert.Heading>
            <div className='center'><img src={CrowdImage} title='People' className='center paper' /></div>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
            <p>
              Tincidunt id aliquet risus feugiat in.In metus vulputate eu scelerisque felis imperdiet.Hac habitasse platea dictumst vestibulum rhoncus est.Lacus suspendisse faucibus interdum posuere lorem ipsum dolor.Metus aliquam eleifend mi in nulla.Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque.Eleifend mi in nulla posuere sollicitudin aliquam ultrices sagittis orci.At ultrices mi tempus imperdiet nulla malesuada pellentesque elit eget.Purus gravida quis blandit turpis.
            </p>
            <hr />
            <p>
              Whenever you need to, be sure to use margin utilities to keep things nice
              and tidy.
            </p>
          </Alert>
        </Container>
    )
  }
}

export default Home