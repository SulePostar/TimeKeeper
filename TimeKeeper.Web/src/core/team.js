import React, { Component } from 'react'
import Container from 'react-bootstrap/Container'
import DeskItem  from './deskItem'
import data from 'Data/team.json'

class Team extends Component {
  render() {
    return (
      <Container>
        <div className='title'>Our Team</div>
        {data.map((item) => {
          return (
            <div key={item.name}>
              <DeskItem title={item.name} subtitle={item.role} image={item.image} description={item.desc} />
            </div>
          )
        })}
      </Container>
    )
  }
}

export default Team