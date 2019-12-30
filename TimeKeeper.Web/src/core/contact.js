import React, { Component } from 'react'
import Container from 'react-bootstrap/Container'
import { send } from '../services/apiServices'
import Button from 'react-bootstrap/Button'
import Form from 'react-bootstrap/Form'

const styles = {
  card: {
    width: '60%',
    margin: '16px auto',
    padding: '16px',
    boxShadow: '10px 10px 16px 4px #888'
  },
  table: {
    width: '100%',
    padding: '8px'
  }, 
  detail: {
    width: '100%',
    padding: '8px'
  },
  half: {
    width: '50%',
    padding: '12px',
    verticalAlign: 'top'
  },
  title: {
    display: 'table',
    margin: '0 auto',
    fontSize: '2.5em',
    color: '#69c',
    marginBottom: '12px',
    marginTop: '-24px'
  },
  center: {
    textAlign: 'center'
  },
  box: {
    border: '1px solid black'
  }
}

class Contact extends Component {
  constructor({ match }) {
    super()
    this.state = {
      name: '',
      email: '',
      phone: '',
      message: ''
    }
  }

  componentDidMount = () => {
    // this.setState({
    //   name: 'haj',
    //   email: 'nehaj@dot.com',
    //   phone: 'jah',
    //   message: 'jahen'
    // })
  }

  handleChange = name => event => {
    const value = event.target.value
    this.setState({ [name]: value })
  }

  handleSubmit = () => {
    send(this.state, res => console.log(res))
  }

  render() {
    return (
      <Container>
        <div className='title'>Contact</div>
        <Form>
          <Form.Group controlId='name'>
            <Form.Label>Full name:</Form.Label>
            <Form.Control type='text' placeholder='your name here' value={this.state.name} onChange={this.handleChange('name')} />
          </Form.Group>
          <Form.Group controlId='email'>
            <Form.Label>E-mail address:</Form.Label>
            <Form.Control type='email' placeholder='your email here' value={this.state.email} onChange={this.handleChange('email')} />
          </Form.Group>
          <Form.Group controlId='phone'>
            <Form.Label>Phone number:</Form.Label>
            <Form.Control type='text' placeholder='your phone here'value={this.state.phone} onChange={this.handleChange('phone')}  />
          </Form.Group>
          <Form.Group controlId='message'>
            <Form.Label>Your message:</Form.Label>
            <Form.Control as='textarea' rows='3' value={this.state.message} onChange={this.handleChange('message')} />
          </Form.Group>
          <Button variant="primary" type="submit" onClick={this.handleSubmit}>Send Message</Button>
        </Form>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
                <p>Id faucibus nisl tincidunt eget nullam non.Eros donec ac odio tempor orci dapibus.</p>
                <p>Aenean sed adipiscing diam donec adipiscing tristique risus nec.</p>
                <Button onClick={this.handleSubmit}>Send Message</Button>
      </Container>
    )
  }
}

export default Contact