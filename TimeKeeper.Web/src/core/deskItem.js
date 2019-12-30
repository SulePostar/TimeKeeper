import React, { Component } from 'react'
import Card from 'react-bootstrap/Card'

const styles = {
  card: {
    display: 'table',
    float: 'left',
    width: '30%',
    margin: '18px',
    textAlign: 'center'
  },
  image: {
    width: '33%',
    borderRadius: '33%',
    margin: '12px 0'
  }
}

class DeskItem extends Component {
  render() {
    const props = this.props
    return (
      <Card style={styles.card}>
        <Card.Img variant="top" src={props.image} style={styles.image} />
        <Card.Title>{props.title}</Card.Title>
        <Card.Subtitle>{props.subtitle}</Card.Subtitle>
        <Card.Body>
          <Card.Text>
            {props.description}
          </Card.Text>
        </Card.Body>
      </Card>
    )
  }
}

export default DeskItem