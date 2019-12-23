import axios from 'axios'
import config from '../config'

const send = (mail, callback) => {
    console.log(mail)
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
    
    axios.post(config.apiUrl + 'contact', mail).then(res => callback(res))
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