import React, { useState, useEffect, useRef } from 'react';
import { classNames } from 'primereact/utils';
import {getProductRequest, addProductRequest, updateProductRequest, deleteProductRequest, getCategoryRequest, getImageRequest} from '../../api/services/productServices'
import {handleApi} from '../../api/handleApi'
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Toast } from 'primereact/toast';
import { Button } from 'primereact/button';
import { FileUpload } from 'primereact/fileupload';
import { Toolbar } from 'primereact/toolbar';
import { InputTextarea } from 'primereact/inputtextarea';
import { RadioButton } from 'primereact/radiobutton';
import { InputNumber } from 'primereact/inputnumber';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import './index.css';

function Product() {
  let emptyProduct = {
    name: 'New Product',
    categoryId: 1,
    description: '',
    price: 199000,
    cost: 30000,
    viewCount:0,
    stock: 20,
    imageFile:{},
  };
  const [products, setProducts] = useState([]);
  const [categories, setCategory] = useState([]);
  const [images, setImage] = useState([]);
  const [deleteProductDialog, setDeleteProductDialog] = useState(false);
  const [globalFilter, setGlobalFilter] = useState(null);
  const [productDialog, setProductDialog] = useState(false);
  const [product, setProduct] = useState(emptyProduct);
  const [submitted, setSubmitted] = useState(false);
  const toast = useRef(null);
  // const [totalSize, setTotalSize] = useState(0);

  useEffect(() => {
    const fetchProductAsync = async() => {
      let result = await handleApi(getProductRequest());
      setProducts(result.data)
      // let uniqueCate = [... new Set(result.data.map(x => x.category.name))]
      // setCategory(uniqueCate)
    }
    const fetchCategoryAsync = async() => {
        let result = await handleApi(getCategoryRequest());
        // let categoryName = result.data.map(c => c.name);
        setCategory(result.data)
      }
      const fetchImageAsync = async() => {
          let result = await handleApi(getImageRequest());
          setImage(result.data)
      }

      fetchImageAsync();
      fetchCategoryAsync();
    fetchProductAsync();

  }, [])
  const openNew = () => {
    setProduct(emptyProduct);
    setSubmitted(false);
    setProductDialog(true);
  }

  const hideDialog = () => {
    setSubmitted(false);
    setProductDialog(false);
  }

  const hideDeleteProductDialog = () => {
    setDeleteProductDialog(false);
  }
  const productImage =(id) => {
    let _image = images.filter(val => val.productId === id && val.isDefualt == true)[0];
    if(_image == null)
    {
      _image = {
        id: id,
        name :'#',
      }
    }
    return _image;
  }
  const categoryName =(productId) => {
    let _cateogry = categories.filter(val => val.id === productId)[0];
    return _cateogry
  }
  const saveProduct = () => {
    setSubmitted(true);
    //clone 
    let _products = [...products];
    let _product = {...product};

    if (product.id) {
      //update view
      const index = _products.findIndex(p => p.id === product.id) 
      _products[index] = _product;
      toast.current.show({ severity: 'success', summary: 'Successful', detail: 'Product Updated', life: 3000 });
     
      (async(p) => {
        await handleApi(updateProductRequest(p))
      })(_product)
    }
    else {
      const addProductAsync = async (payload) => {
        let result = await handleApi(addProductRequest(payload));
        console.log(result);
        setProduct(result.data)
        _products.push(result.data);
      }
      addProductAsync(_product);
      toast.current.show({ severity: 'success', summary: 'Successful', detail: 'Product Created', life: 3000 });
    }
    
    setProducts(_products);
    setProductDialog(false);
    setProduct(emptyProduct);
  }

  const editProduct = (product) => {
    setProduct({...product});
    setProductDialog(true);
  }

  const confirmDeleteProduct = (product) => {
    setProduct(product);
    setDeleteProductDialog(true);
  }

  const deleteProduct = () => {
    let _products = products.filter(val => val.id !== product.id);
    let id = product.id;
    setProducts(_products);
    setDeleteProductDialog(false);
    setProduct(emptyProduct);
    toast.current.show({ severity: 'success', summary: 'Successful', detail: 'Product Deleted', life: 3000 });

    (async(id) => {
      await handleApi(deleteProductRequest(id))
    })(id)
}

  //templates
  const header = (
    <div className="table-header">
        <h5 className="mx-0 my-1">Manage Products</h5>
          <span className="p-input-icon-left">
              <i className="pi pi-search" />
              <InputText type="search" onInput={(e) => setGlobalFilter(e.target.value)} placeholder="Search..." />
          </span>
    </div>
  );

  const productDialogFooter = (
    <React.Fragment>
        <Button label="Cancel" icon="pi pi-times" className="p-button-text" onClick={hideDialog} />
        <Button label="Save" icon="pi pi-check" className="p-button-text" onClick={saveProduct} />
    </React.Fragment>
  );

  const leftToolbarTemplate = () => {
    return (
      <React.Fragment>
        <Button label="Add" icon="pi pi-plus" className="p-button-success mr-2" onClick={openNew} />
      </React.Fragment>
    )
  }

  const actionBodyTemplate = (rowData) => {
    return (
        <React.Fragment>
            <Button icon="pi pi-pencil" className="p-button-rounded p-button-success mr-2" onClick={() => editProduct(rowData)} />
            <Button icon="pi pi-trash" className="p-button-rounded p-button-warning" onClick={() => confirmDeleteProduct(rowData)} />
        </React.Fragment>
    );
  }

  const deleteProductDialogFooter = (
    <React.Fragment>
        <Button label="No" icon="pi pi-times" className="p-button-text" onClick={hideDeleteProductDialog} />
        <Button label="Yes" icon="pi pi-check" className="p-button-text" onClick={deleteProduct} />
    </React.Fragment>
  );


  //methods
  const onCategoryChange = e => {
    const val = (e.target && e.target.value) || '';
    let _product = {...product}
    _product.categoryid = val;
    console.log(val)
    setProduct(_product);
  }

  const onInputChange = (e, name) => {
    const val = (e.target && e.target.value) || '';
    let _product = {...product};
    _product[`${name}`] = val;

    setProduct(_product);
  }

  const onInputNumberChange = (e, name) => {
    const val = (e.target && e.target.value) || 0;
    let _product = {...product};
    _product[`${name}`] = val;

    setProduct(_product);
  }

  const imageBodyTemplate = (rowData) => {
    let image = productImage(rowData.id);
    return <img src={image.name+image.id} onError={(e) => e.target.src='https://www.primefaces.org/wp-content/uploads/2020/05/placeholder.png'} alt={image.id} className="product-image" />;
  }
  const formatCurrency = (value) => {
    return value.toLocaleString('en-US', { style: 'currency', currency: 'VND' });
  }
  const categoryBodyTemplate = (rowData) =>{
    let category = categoryName(rowData.categoryId);
    return <p>{category.name}</p>
  }
  const flagStock = stock => {
    return stock <= 30 ? "LOWSTOCK" : "INSTOCK";
  }

  const priceBodyTemplate = (rowData) => {
    return formatCurrency(rowData.price);
  }

  const statusBodyTemplate = (rowData) => {
    return <span className={`product-badge status-${flagStock(rowData.stock).toLowerCase()}`}>{flagStock(rowData.stock)}</span>;
  }


  const uploadImage = async (event) => {
    const file = event.files[0];
    console.log(file)
    let _product = {...product};

    _product.imageFile = (file);
    console.log(_product.imageFile)
    setProduct(_product);
    console.log(_product);
    toast.current.show({severity: 'info', summary: 'Success', detail: 'File Uploaded with Auto Mode'});
    // }
  }

  
  return (
    <div className="datatable-crud-demo">
        <Toast ref={toast} />
        <div className="card">
          <Toolbar className="mb-4" left={leftToolbarTemplate}></Toolbar>
          <DataTable globalFilter={globalFilter} value={products} header={header} responsiveLayout="scroll">
            <Column field="id" header="Id" sortable style={{ minWidth: '16rem' }}></Column>
            <Column field="name" header="Name" sortable style={{ minWidth: '16rem' }}></Column>
            <Column header="Image" body={imageBodyTemplate}></Column>
            <Column field="price" header="Price" body={priceBodyTemplate} sortable style={{ minWidth: '8rem' }}></Column>
            <Column header="Category" body={categoryBodyTemplate} style={{ minWidth: '10rem' }}></Column>
            <Column header="Status" body={statusBodyTemplate}></Column>
            <Column header="Action" body={actionBodyTemplate} exportable={false} style={{ minWidth: '8rem' }}></Column>
          </DataTable>
        </div>

      <Dialog visible={productDialog} style={{ width: '450px' }} header="Product Details" modal className="p-fluid" footer={productDialogFooter} onHide={hideDialog}>
                <div className="field">
                    <div>Upload Image</div>
                    <FileUpload mode="basic" name="demo[]"  accept="image/*" customUpload auto uploadHandler={uploadImage} />
                </div>
               
                 <div className="field">
                     <label htmlFor="name">Name</label>
                     <InputText id="name" value={product.name} onChange={(e) => onInputChange(e, 'name')} required autoFocus className={classNames({ 'p-invalid': submitted && !product.name })} />
                     {submitted && !product.name && <small className="p-error">Name is required.</small>}
                 </div>
                 <div className="field">
                     <label htmlFor="description">Description</label>
                     <InputTextarea id="description" value={product.description} onChange={(e) => onInputChange(e, 'description')} required rows={3} cols={20} />
                 </div>

                 <div className="field">
                     {/* <Button label="Add Category" icon="pi pi-upload" className="p-button-help" onClick={addCategory} /> */}
                     <label className="mb-3">Category</label>
                     <div className="formgrid grid">
                     <InputNumber id="stock" value={product.categoryId} onValueChange={(e) => onInputNumberChange(e, 'categoryId')} integeronly="true" />
                     </div>
                </div>

                 <div className="formgrid grid">
                     <div className="field col">
                         <label htmlFor="price">Price</label>
                         <InputNumber id="price" value={product.price} onValueChange={(e) => onInputNumberChange(e, 'price')} mode="currency" currency="VND" locale="en-US" />
                     </div>
                     <div className="field col">
                         <label htmlFor="stock">Stock</label>
                         <InputNumber id="stock" value={product.stock} onValueChange={(e) => onInputNumberChange(e, 'stock')} integeronly="true" />
                     </div>
                 </div>
        </Dialog>
        <Dialog visible={deleteProductDialog} style={{ width: '450px' }} header="Confirm" modal footer={deleteProductDialogFooter} onHide={hideDeleteProductDialog}>
            <div className="confirmation-content">
                <i className="pi pi-exclamation-triangle mr-3" style={{ fontSize: '2rem'}} />
                {product && <span>Are you sure you want to delete <b>{product.name}</b>?</span>}
            </div>
          </Dialog>
      </div>
  )
}


export default Product;