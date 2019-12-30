import React, { Component } from 'react'
import { Link, withRouter } from 'react-router-dom'
import { Button, ButtonGroup, Dropdown, DropdownButton } from 'react-bootstrap'
import config from '../config'

class Menu extends Component {
  render() {
    return (
      <div className='menu'>
        {config.currentUser.id === 0 &&
          <ButtonGroup size='sm'>
            <Button href='/'>Home</Button>
            <Button href='/services'>Services</Button>
            <Button href='/team'>Our Team</Button>
            <Button href='/contact'>Contact Us</Button>
            <Button href='/login'>Login</Button>
          </ButtonGroup>
        }
        {config.currentUser.id !== 0 &&
          <ButtonGroup size='sm'>
            <DropdownButton title='Home' size='sm'>
            <Dropdown.Header>HEADER</Dropdown.Header>
              <Dropdown.Item href='/'>About us</Dropdown.Item>
              <Dropdown.Item href='/services'>Services</Dropdown.Item>
              <Dropdown.Divider />
              <Dropdown.Header>HEADER II</Dropdown.Header>
              <Dropdown.Item href='/team'>Our Team</Dropdown.Item>
              <Dropdown.Item href='/contact'>Contact Us</Dropdown.Item>
            </DropdownButton>
            <DropdownButton title='Database' size='sm'>
            <Dropdown.Item href='/'>Home</Dropdown.Item>
              <Dropdown.Item href='/services'>Services</Dropdown.Item>
              <Dropdown.Item href='/team'>Our Team</Dropdown.Item>
              <Dropdown.Item href='/contact'>Contact Us</Dropdown.Item>
            </DropdownButton>
            <Button href='/time'>Time Tracking</Button>
            <DropdownButton title='Reports' size='sm'>
            <Dropdown.Item href='/'>Home</Dropdown.Item>
              <Dropdown.Item href='/services'>Services</Dropdown.Item>
              <Dropdown.Item href='/team'>Our Team</Dropdown.Item>
              <Dropdown.Item href='/contact'>Contact Us</Dropdown.Item>
            </DropdownButton>
            <Button href='/logout'>Logout</Button>
          </ButtonGroup>
        }
      </div>
    )
  }
}

export default withRouter(Menu)