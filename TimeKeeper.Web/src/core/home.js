import React, { Component, useState } from 'react'
import { Container, Button, Nav, Accordion, Card, Carousel } from 'react-bootstrap'
import CrowdImage from 'Images/crowd.jpg'
import c1 from 'Images/c1.jpg'
import c2 from 'Images/c2.jpg'
import c3 from 'Images/c3.jpg'

class Home extends Component {
  render() {
    return (
      <Container>
        <Card bg='light' text='dark' border='dark'>
          <Card.Header>
            <div className='title'>About Us</div>
          </Card.Header>
          <Card.Img variant='top' src={CrowdImage} style={{width:'770px', margin:'0 auto'}}/>
          <Card.Body>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
            <p>
              Tincidunt id aliquet risus feugiat in.In metus vulputate eu scelerisque felis imperdiet.Hac habitasse platea dictumst vestibulum rhoncus est.Lacus suspendisse faucibus interdum posuere lorem ipsum dolor.Metus aliquam eleifend mi in nulla.Rhoncus aenean vel elit scelerisque mauris pellentesque pulvinar pellentesque.Eleifend mi in nulla posuere sollicitudin aliquam ultrices sagittis orci.At ultrices mi tempus imperdiet nulla malesuada pellentesque elit eget.Purus gravida quis blandit turpis.
                </p>
          </Card.Body>
          <Card.Footer>
          <p>
              Whenever you need to, be sure to use margin utilities to keep things nice
              and tidy.
                </p>
          </Card.Footer>
        </Card>
      </Container >
    )
  }
}

export default Home