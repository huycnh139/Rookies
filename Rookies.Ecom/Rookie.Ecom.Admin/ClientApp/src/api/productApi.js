import axiosClient from "./axiosClient";

// api/productApi.js
const productApi = {
    getAll:() =>{
        const url= '/product';
        return axiosClient.get(url);
    },
    get: (id)=>{
        const url=`/product/${id}`;
        return axiosClient.get(url);
    }
}
export default productApi;