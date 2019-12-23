import React, { Component } from 'react'
import { BrowserRouter } from 'react-router-dom'
import Header from './core/header'
import Footer from './core/footer'
import Router from './router'

class App extends Component {
    render() {
        return (
            <div>
                <BrowserRouter>
                    <Header />
                    <Router />
                    <Footer />
                </BrowserRouter>
            </div>
        )
    }
}

export default App
