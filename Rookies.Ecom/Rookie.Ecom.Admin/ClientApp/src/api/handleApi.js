export const handleApi = apiRequest =>{
    return apiRequest.then(res => {
        return res
    }).catch(err=>{
        return err
    })
}