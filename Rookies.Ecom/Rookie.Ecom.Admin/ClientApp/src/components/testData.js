import { useEffect, useState } from "react";
import productApi from "../api/productApi";

function TestDate(){
    useEffect(()=>{
        cont[product, setProduct] = useState([]);
  
        const fetchproduct = async () => {
            try {
                const response = await productApi.get(3);
                console.log(response);
            } catch (error) {
                console.log('failed to fetch product');
            }
        }
        fetchproduct();
    },  []);
    return <h3>fest data</h3>
  }
  export default TestDate;
