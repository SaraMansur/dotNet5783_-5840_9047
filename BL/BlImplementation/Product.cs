using BlApi;
using BO;

namespace BlImplementation;

internal class Product: IProduct
{
    private DalApi.IDal Dal = new Dal.DalList();
    // the function returns list of products to the manager
    public IEnumerable<BO.ProductForList> ProductList() 
    {
        List<BO.ProductForList> productForLists = new List<BO.ProductForList>();//Going through the list of products
        foreach (var item in Dal.Product.Get())
        {
            productForLists.Add(new BO.ProductForList() { m_Category = (BO.Enums.Category?)item.m_Category, m_ID = item.m_ID, m_NameProduct = item.m_Name, m_PriceProduct = item.m_Price });
        }
        return productForLists;
    }


    //public IEnumerable<BO.ProductForList> CatalogList() 
    //{
    //    return ProductList();
    //}

    //the function return a catalog for the customer
    public IEnumerable<BO.ProductItem> CatalogList()
    {
        List<ProductItem> catalogList = new List<ProductItem>();
        foreach (var item in Dal.Product.Get())
        {
            ProductItem p = new ProductItem() { m_Category = (BO.Enums.Category?)item.m_Category, m_ID = item.m_ID, m_NameProduct = item.m_Name, m_PriceProduct = item.m_Price,m_AmountInCart=0 };
            if (item.m_InStock > 0) 
                p.m_InStock = true; 
            else 
                p.m_InStock = false;
            catalogList.Add(p); 
        }
        return catalogList;
    }

    //the function returns details of the requsted  product to the manager
    public BO.Product ProductId(int ID)
    {
        if (ID < 0) throw new BO.incorrectData(); //check if the id is correct
        try
        {
            DO.Product Doproduct = Dal.Product.GetbyID(ID);
            BO.Product Boproduct = new BO.Product()
            { m_Category = (BO.Enums.Category?)Doproduct.m_Category, m_Id = Doproduct.m_ID, m_InStock = Doproduct.m_InStock, m_Name = Doproduct.m_Name, m_Price = Doproduct.m_Price };
            return Boproduct; 
        }
        catch (Exception) { throw new BO.MissingID(); }
    }

    //the function returns details of the requsted product for the customer
    public BO.ProductItem CatalogProductId(int ID)
    {
        if (ID < 0) throw new BO.incorrectData();//check if the id is correct
        try 
        { 
            DO.Product Doproduct = Dal.Product.GetbyID(ID); 
            BO.ProductItem productItem = new BO.ProductItem() 
            { m_Category = (BO.Enums.Category?)Doproduct.m_Category, m_ID = Doproduct.m_ID,m_NameProduct = Doproduct.m_Name, m_PriceProduct = Doproduct.m_Price };
            return productItem; 
        }
        catch (Exception) { throw new BO.MissingID(); }
    }

    //The function adds a product to the data base
    public void AddProduct(BO.Product product) 
    {
        if (product.m_Id < 0 || product.m_Price < 0 || product.m_InStock < 0 || product.m_Name == "")//check if the data is correct
            throw new BO.incorrectData();
        DO.Product Doproduct = new DO.Product() { m_Name = product.m_Name, m_Price = product.m_Price,m_Category = (DO.Enums.Category?)product.m_Category,m_ID= product.m_Id,m_InStock= product.m_InStock};
        try { Dal.Product.Add(Doproduct); }
        catch(Exception) { throw new BO.DuplicateID(); }   
    }

    //the function deletes the requsted product from the data base
    public void DeleteProduct(int ID) 
    {
        bool flag =false;    
        foreach (var item in Dal.Order.Get()) //Going through the list of orders
            foreach (var item2 in Dal.OrderItem.GetOrderItems(item.m_ID)) //for each order check if there is the reqsted product 
                if (item2.m_ProductId == ID)
                {
                    flag = true; 
                    break;
                }
        try { if (!flag) Dal.Product.Delete(ID); }
        catch (Exception) { throw new BO.MissingID(); }
    }

    //the function update the requsted product from the data base
    public void UpdateProduct(BO.Product product) 
    {
        if (product.m_Id < 0 || product.m_Price < 0 || product.m_InStock < 0 || product.m_Name == "")//check if the data is correct
            throw new BO.incorrectData();
        DO.Product Doproduct = new DO.Product() { m_Name = product.m_Name, m_Price = product.m_Price, m_Category = (DO.Enums.Category?)product.m_Category, m_ID = product.m_Id, m_InStock = product.m_InStock };
        try { Dal.Product.Update(Doproduct); }
        catch(Exception) { throw new BO.MissingID(); }  
    }
}
