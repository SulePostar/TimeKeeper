import React, { Component } from 'react'
import { Route, Switch } from 'react-router-dom'

import Home from './core/home'
import Services from './core/services'
import Team from './core/team'
import Contact from './core/contact'
import Login from './auth/login'
import Logout from './auth/logout'

class Router extends Component {
    render() {
        return (
            <div>
                <Switch>
                    <Route exact path="/" component={Home} />
                    <Route path="/services" component={Services} />
                    <Route path="/team" component={Team} />
                    <Route path="/contact" component={Contact} />
                    <Route path="/login" component={Login} />
                    <Route path="/logout" component={Logout} />
                </Switch>
            </div>
        )
    }
}

export default Router