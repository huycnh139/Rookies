import Endpoints from "../endpoints";
import {get,post,del,put} from '../httpHelper'

export const addCategoryRequest = (load) => {
    return post(Endpoints.category, load)
};
export const updateCategoryRequest = (load) => {
    return put(Endpoints.category, load)
};
export const deleteCategoryRequest =(id) => {
    return del(Endpoints.category+`?categoryId=${id}`)
};
export const getCategoryRequest = () => {
    return get(Endpoints.category);
};
