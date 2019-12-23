import React, { Component } from 'react'
import Container from 'react-bootstrap/Container'
import Card from 'react-bootstrap/Card'
import data from '../assets/data/team.json'

const styles = {
  card: {
    display: 'table',
    float: 'left',
    width: '30%',
    margin: '18px'

  },
  image: {
    display: 'table',
    width: '128px',
    margin: '0 auto',
    borderRadius: '50%',
    marginBottom: '24px'
  },
  title: {
    display: 'table',
    margin: '0 auto',
    fontSize: '2.5em',
    color: '#69c',
    marginBottom: '12px',
    marginTop: '-24px'
  },
  role: {
    display: 'table',
    margin: '0 auto',
    fontSize: '1.5em',
    color: '#69c',
    marginBottom: '12px',
    marginTop: '-12px'
  }
}

class Team extends Component {
  render() {
    return (
      <Container>
        <div className='title'>Our Team</div>
        <div className='center'>
          {data.map((item) => {
            return (
              <Card className='paper' style={styles.card}>
                <div key={item.name}>
                  <img src={item.image} style={styles.image}></img>
                  <div style={styles.title}>{item.name}</div>
                  <div style={styles.role}>{item.role}</div>
                  <div>{item.desc}</div>
                </div>
              </Card>
            )
          })}
        </div>
      </Container>
    )
  }
}

export default Team