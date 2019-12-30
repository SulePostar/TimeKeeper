import axios from 'axios'
import config from '../config'

const send = (mail, callback) => {
  let axiosConfig = {
    method: 'post',
    url: config.apiUrl + 'contact',
    headers: { 'Content-Type': 'application/json' },
    body: mail
  }
  // axios(axiosConfig)
  //     .then(res => callback(res))
  //     .catch(reason => {
  //         console.log(reason)
  //         callback(false)
  //     })

  axios.post(config.apiUrl + 'contact', mail)
    .then(res => {
      window.alert(JSON.stringify(res))
      callback(res)
    })
    .catch(err => {
      window.alert(JSON.stringify(err))
      callback(false)
    })
}

const list = (callback) => {
  let axiosConfig = { headers: { 'Content-Type': 'application/json' } }
  axios.get(config.apiUrl + 'users', axiosConfig)
    .then(res => callback(res))
    .catch(reason => {
      console.log(reason)
      callback(false)
    })
}

export { list, send }