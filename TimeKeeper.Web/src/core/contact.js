import React, { Component } from 'react'
import Container from 'react-bootstrap/Container'
import { list, send } from '../services/apiServices'
import Button from 'react-bootstrap/Button'
import FormText from 'react-bootstrap/FormText'
import Text from 'Styles/text'
import Area from 'Styles/area'

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
      message: ''
    }
  }

  componentDidMount = () => {
    this.setState({
      name: 'abc',
      email: 'abc',
      phone: '1234567890',
      message: 'abc'
    })
  }

  handleChange = name => event => {
    const value = event.target.value
    this.setState({ [name]: value })
  }

  handleSubmit = () => {
    const mail = {
      name: this.state.name,
      email: this.state.email,
      phone: this.state.phone,
      message: this.state.message
    }
    // list(res => { console.log(res.data) })
    send(mail, res => console.log(res))
  }

  render() {
    return (
      <Container>
        <div style={styles.title}>Contact</div>
        <div style={styles.card}>
          <table style={styles.table}><tbody>
            <tr>
              <td style={styles.half} rowSpan='2'>
                <table style={styles.table}><tbody>
                  <tr>
                    <td style={styles.detail}>
                      <label>Name: <Text name='name' value={this.state.name} onChange={this.handleChange('name')} /></label>
                    </td>
                  </tr>
                  <tr><td>&nbsp;</td></tr>
                  <tr>
                    <td style={styles.detail}>
                      <label>Email address: <Text name='email' value={this.state.email} onChange={this.handleChange('email')} /></label>
                    </td>
                  </tr>
                  <tr>
                    <td style={styles.detail}>
                      <label>Phone number: <Text name='phone' value={this.state.email} onChange={this.handleChange('phone')} /></label>
                    </td>
                  </tr>
                  <tr>
                    <td style={styles.detail}>
                      <label>Message:<br /><Area name='message' rows='5' value={this.state.message} onChange={this.handleChange('message')}></Area></label>
                    </td>
                  </tr>
                </tbody></table>
              </td>
              <td style={styles.half}>
                &nbsp;<br />
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
                <p>Id faucibus nisl tincidunt eget nullam non.Eros donec ac odio tempor orci dapibus.</p>
                <p>Aenean sed adipiscing diam donec adipiscing tristique risus nec.</p>
              </td>
            </tr>
            <tr>
              <td style={styles.center}>
                <Button onClick={this.handleSubmit}>Send Message</Button>
              </td>
            </tr>
          </tbody></table>
        </div>
      </Container>
    )
  }
}

export default Contact