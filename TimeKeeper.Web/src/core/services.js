import React, { Component } from 'react'
import Container from 'react-bootstrap/Container'
import DeskItem from './deskItem'
import data from 'Data/services.json'

class Services extends Component {
  render() {
    return (
      <Container>
        <div className='title'>Services</div>
        {data.map((item) => {
          return (
            <div key={item.title}>
              <DeskItem title={item.title} image={item.image} description={item.about} />
            </div>
          )
        })}
      </Container>
    )
  }
}

export default Services