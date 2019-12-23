import 'bootstrap/dist/css/bootstrap.css'
import 'Styles/style.css'
import React from 'react'
import { hydrate } from 'react-dom'
import App from './app'

hydrate(<App />, document.getElementById('root'))