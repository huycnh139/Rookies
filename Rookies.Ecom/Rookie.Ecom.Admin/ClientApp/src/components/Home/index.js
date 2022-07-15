import {useEffect, useState} from 'react'
import {
  BrowserRouter,
  Route,
  Routes,
  Link
} from "react-router-dom";
import { Menu } from 'primereact/menu';
function Home(){
  const [isProductOn, setIsProductOn] = useState(false);
  const [isOnCategory, setIsOnCategory] = useState(false);
    let items = [
      {
          label: 'Options',
          items: [,
                  {
                    label: 'Home', 
                    icon: 'pi pi-fw pia-trash',
                    url : 'http://www.google.com'
                  },
                  {
                    label: 'Product', 
                    icon: 'pi pi-fw pi-plus',
                    url : <Link to ="/category"></Link>
                  },
                  {
                    label: 'Category', 
                    icon: 'pi pi-fw pi-trash',
                    url : <Link to ="/product"></Link>
                  }]
      },
      {
          label: 'Account',
          items: [
                  {label: 'Options', icon: 'pi pi-fw pi-cog'},
                  {label: 'Log in', icon: 'pi pi-fw pi-upload'},
                  {label: 'Sign Out', icon: 'pi pi-fw pi-power-off'} 
                ]
      }
  ]
  return (
    <div>     
        <Menu model={items} />
        
    </div>
  );
}
export default Home;


