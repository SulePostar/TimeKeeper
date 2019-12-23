import React, { Component } from 'react'
import Container from 'react-bootstrap/Container'
import Card from 'react-bootstrap/Card'
import data from 'Data/services.json'

const styles = {
  card: {
    display: 'table',
    float: 'left',
    width: '30%',
    margin: '18px'

  },
  image: {
    display: 'table',
    width: '72px',
    margin: '0 auto',
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

class Services extends Component {
  render() {
    return (
      <Container>
        <div className='title'>Services</div>
        <div className='center'>
          {data.map((item) => {
            return (
              <Card className='paper' style={styles.card}>
                <div key={item.name}>
                  <img src={item.image} style={styles.image}></img>
                  <div style={styles.title}>{item.title}</div>
                  <div>{item.about}</div>
                </div>
              </Card>
            )
          })}
        </div>
      </Container>
    )
  }
}

export default Services