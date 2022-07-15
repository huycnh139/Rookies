import EndPoints from '../endpoints';
import {get, post, put, del} from '../httpHelper'

export const getProductRequest = () => {
    return get(EndPoints.product);
}

export const addProductRequest = (payload) => {
    return post(EndPoints.product, payload);
}

export const updateProductRequest = (payload) => {
    return put(EndPoints.product, payload);
}

export const deleteProductRequest = (id) => {
    return del(`${EndPoints.product}/${id}`);
}

export const getCategoryRequest = () => {
    return get(EndPoints.category);
}
export const getImageRequest =()=>{
    return get(`${EndPoints.product}/${EndPoints.Image}`);
}