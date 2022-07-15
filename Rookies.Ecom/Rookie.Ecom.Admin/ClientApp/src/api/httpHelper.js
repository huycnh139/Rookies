import axios from 'axios'
import {BASE_URL} from './config'

export  const get = endpoint =>{
    return axios.get(BASE_URL +"/" + endpoint);
}
export function post(endpoint,body){
    return axios({
        method:"post",
        url: BASE_URL +'/'+endpoint,
        data: body,
        headers :{"Content-Type":"multipart/form-data"}
    })
}
export function put(endpoint,body){
    return axios.put(BASE_URL+'/'+ endpoint, body);
}

export function del(endpoint,body){
    return axios.delete(BASE_URL+'/'+ endpoint, body);
}