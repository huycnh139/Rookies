import {useEffect, useState} from 'react'
import Category  from './components/Category'
import Product  from './components/Product'
import { Menu } from 'primereact/menu';
function App(){
  const [onCatgory,setOnCatgory] = useState(false);
  const [onProduct,setOnProduct] = useState(false);
  let items = [
    {
        label: 'Options',
        items: [{
                  label: 'User', 
                  icon: 'pi pi-fw pi-user-edit'
                },
                {
                  label: 'Category', 
                  icon: 'pi pi-fw pi-shopping-cart',
                  command:()=>{
                    setOnCatgory(true);
                    setOnProduct(false); 
                  }
                },
                {
                  label: 'Product', 
                  icon: 'pi pi-fw pi-wallet', 
                  command:()=>{ 
                    setOnCatgory(false);
                    setOnProduct(true);
                  } 
                }]
    },
    {
        label: 'Account',
        items: [{
                  label: 'Options', 
                  icon: 'pi pi-fw pi-cog'
                },
                {
                  label: 'Sign Out', 
                  icon: 'pi pi-fw pi-sign-out'
                },
                {
                  label: 'Log In', 
                  icon: 'pi pi-fw pi-sign-in'
                } ]
    }
]
 
  return (
    <div id ="root" style={{display:'flex'}}>
      <div>
          <Menu model={items} />
          </div>
      <div>
          {onCatgory&&<Category/>}
          {onProduct&&<Product/>}
      </div>
    </div>
  );
}
export default App;


