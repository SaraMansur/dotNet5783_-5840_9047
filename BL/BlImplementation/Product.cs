using BlApi;
using BO;
using DalApi;
namespace BlImplementation;

internal class Product:IProduct
{
    private IDal Dal = new Dal.DalList();
    IEnumerable<ProductForList> ProductList() 
    {
        List<ProductForList> productForLists = new List<ProductForList>();//Going through the list of products
        foreach (var item in Dal.Product.Get())
        {
            productForLists.Add(new ProductForList() { m_Category = (BO.Enums.Category?)item.m_Category, m_ID = item.m_ID, m_NameProduct = item.m_Name, m_PriceProduct = item.m_Price });
        }
        return productForLists;
    }

    IEnumerable<ProductForList> CatalogList() 
    {
        return ProductList();
    }
    BO.Product ProductId(int ID)
    {
        if (ID < 0) throw new BO.incorrectData();
        try
        {
            DO.Product Doproduct = Dal.Product.GetbyID(ID);
            BO.Product Boproduct = new BO.Product()
            { m_Category = (BO.Enums.Category?)Doproduct.m_Category, m_Id = Doproduct.m_ID, m_InStok = Doproduct.m_InStock, m_Name = Doproduct.m_Name, m_Price = Doproduct.m_Price };
            return Boproduct; 
        }
        catch (Exception) { throw new BO.MissingID(); }
    }
    ProductItem CatalogProductId(int ID)
    {
        if (ID < 0) throw new BO.incorrectData();
        try 
        { 
            DO.Product Doproduct = Dal.Product.GetbyID(ID); 
            ProductItem productItem = new ProductItem() { m_Category = (BO.Enums.Category?)Doproduct.m_Category, m_ID = Doproduct.m_ID,m_NameProduct = Doproduct.m_Name, m_PriceProduct = Doproduct.m_Price };
            return productItem; 
        }
        catch (Exception) { throw new BO.MissingID(); }
    }
    void AddProduct(BO.Product product) 
    {
        if (product.m_Id < 0 || product.m_Price < 0 || product.m_InStok < 0 || product.m_Name == "")
            throw new incorrectData();
        DO.Product Doproduct = new DO.Product() { m_Name = product.m_Name, m_Price = product.m_Price,m_Category = (DO.Enums.Category?)product.m_Category,m_ID= product.m_Id,m_InStock= product.m_InStok};
        try { Dal.Product.Add(Doproduct); }
        catch(Exception) { throw new BO.DuplicateID(); }   
    }
    void DeleteProduct(int ID) 
    {
        bool flag =false;    
        foreach (var item in Dal.Order.Get())
            foreach (var item2 in Dal.OrderItem.GetOrderItems(item.m_ID))
                if (item2.m_ProductId == ID)
                {
                    flag = true; 
                    break;
                }
        try { if (!flag) Dal.Product.Delete(ID); }
        catch (Exception) { throw new BO.MissingID(); }
    }
    void UpdateProduct(BO.Product product) 
    {
        if (product.m_Id < 0 || product.m_Price < 0 || product.m_InStok < 0 || product.m_Name == "")
            throw new incorrectData();
        DO.Product Doproduct = new DO.Product() { m_Name = product.m_Name, m_Price = product.m_Price, m_Category = (DO.Enums.Category?)product.m_Category, m_ID = product.m_Id, m_InStock = product.m_InStok };
        try { Dal.Product.Update(Doproduct); }
        catch(Exception) { throw new BO.MissingID(); }  
    }
}
//IEnumerable<ProductItem> CatalogList()
//{
//    List<ProductItem> catalogList = new List<ProductItem>();
//    foreach (var item in Dal.Product.Get())
//    {
//        catalogList.Add(new ProductItem() { m_Category = (BO.Enums.Category?)item.m_Category, m_ID = item.m_ID, m_NameProduct = item.m_Name, m_PriceProduct = item.m_Price });
//    }
//    return catalogList;
//}