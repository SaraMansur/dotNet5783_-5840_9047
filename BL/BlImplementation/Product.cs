﻿using BlApi;
using BO;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal? Dal = DalApi.Factory.Get();

    /// <summary>
    /// the function returns list of products to the manager
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList> ProductList()
    {
        var productForLists = from item in Dal!.Product.Get()
                              let p = new BO.ProductForList() { m_Category = (BO.Enums.Category?)item?.m_Category, m_ID = (int)item?.m_ID, m_NameProduct = item?.m_Name, m_PriceProduct = (double)item?.m_Price }
                              select p;
        return productForLists;
    }

    /// <summary>
    /// the function return a catalog for the customer
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductItem> CatalogList(BO.Cart C = null)
    {

        List<ProductItem> catalogList = new List<ProductItem>();
        foreach (var item in Dal!.Product.Get())
        {
            ProductItem p = new ProductItem() { Category = (BO.Enums.Category?)item?.m_Category, m_ID = (int)item?.m_ID, m_NameProduct = item?.m_Name, m_PriceProduct = (double)item?.m_Price, m_InStock= item?.m_InStock > 0 };
            if(C!= null) 
            {
                foreach (var item2 in C.m_orderItems) {
                    if (item2.m_IdProduct == p.m_ID) { p.m_AmountInCart = item2.m_AmountInCart; break; }
                }
            }
                catalogList.Add(p);
        }
        return catalogList;
    }

    /// <summary>
    /// the function returns details of the requsted  product to the manager
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="FaildGetting"></exception>
    public BO.Product ProductId(int? ID)
    {
        ID = ID ?? throw new ArgumentNull();
        if (ID < 0) throw new FaildGetting(new IlegalInput()); //check if the id is correct
        try
        {
            DO.Product Doproduct = (DO.Product)Dal.Product.GetSingle(x => x?.m_ID == ID);
            BO.Product Boproduct = new BO.Product()
            { m_Category = (BO.Enums.Category?)Doproduct.m_Category, m_Id = Doproduct.m_ID, m_InStock = Doproduct.m_InStock, m_Name = Doproduct.m_Name, m_Price = Doproduct.m_Price };
            return Boproduct;
        }
        catch (Exception inner) { throw new FaildGetting(inner); }
    }

    /// <summary>
    /// the function returns details of the requsted product for the customer
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="FaildGetting"></exception>
    public BO.ProductItem CatalogProductId(int? ID)
    {
        ID = ID ?? throw new ArgumentNull();
        if (ID < 0) throw new FaildGetting(new IlegalInput());//check if the id is correct
        try
        {
            DO.Product Doproduct = (DO.Product)Dal.Product.GetSingle(x => x?.m_ID == ID);
            BO.ProductItem productItem = new BO.ProductItem()
            { Category = (BO.Enums.Category?)Doproduct.m_Category, m_ID = Doproduct.m_ID, m_NameProduct = Doproduct.m_Name, m_PriceProduct = Doproduct.m_Price };
            return productItem;
        }
        catch (Exception inner) { throw new FaildGetting(inner); }
    }

    /// <summary>
    /// The function adds a product to the data base
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="FaildAdding"></exception>
    public void AddProduct(BO.Product? product)
    {
        product = product ?? throw new ArgumentNull();
        for (int i = 0; i < product.m_Name.Length; i++)
        {
            if (!(product.m_Name[i] <= 'z' && product.m_Name[i] >= 'a') && (product.m_Name[i] != ' ') && !(product.m_Name[i] <= 'Z' && product.m_Name[i] >= 'A'))
                throw new BO.IlegalInput();
        }
        if (product.m_Category==Enums.Category.None)
            throw new FaildAdding(new BO.IlegalInput());
        if (product.m_Id < 100000|| product.m_Id > 999999|| product.m_Price <= 0 || product.m_InStock <= 0 || product.m_Name == ""|| product.m_Category==BO.Enums.Category.None)//check if the data is correct
            throw new FaildAdding(new IlegalInput());
        DO.Product Doproduct = new DO.Product() { m_Name = product.m_Name, m_Price = product.m_Price, m_Category = (DO.Enums.Category?)product.m_Category, m_ID = product.m_Id, m_InStock = product.m_InStock };
        try { Dal!.Product.Add(Doproduct); }
        catch (Exception inner) { throw new FaildAdding(inner); }
    }

    /// <summary>
    /// the function deletes the requsted product from the data base
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="FaildDelete"></exception>
    public void DeleteProduct(int? ID)
    {
        ID = ID ?? throw new ArgumentNull();
        bool flag = false;
        foreach (var item in Dal!.Order.Get()) //Going through the list of orders
            foreach (var item2 in Dal.OrderItem.Get(x => x?.m_ID == item?.m_ID)) //for each order check if there is the reqsted product 
                if (item2?.m_ProductId == ID)
                {
                    flag = true;
                    break;
                }
        try { if (!flag) Dal.Product.Delete(ID); }
        catch (Exception inner) { throw new FaildDelete(inner); }
    }

    /// <summary>
    /// the function update the requsted product from the data base
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.IlegalInput"></exception>
    /// <exception cref="FaildUpdating"></exception>
    public void UpdateProduct(BO.Product? product)
    {
        product = product ?? throw new ArgumentNull();
        for (int i = 0; i < product.m_Name.Length; i++)
        {
            if (!(product.m_Name[i] <= 'z' && product.m_Name[i] >= 'a') && (product.m_Name[i] != ' ') && !(product.m_Name[i] <= 'Z' && product.m_Name[i] >= 'A'))
                throw new BO.IlegalInput();
        }
        if (product?.m_Id < 0 || product?.m_Price <= 0 || product?.m_InStock <= 0 || product?.m_Name == "" || product.m_Category == BO.Enums.Category.None)//check if the data is correct
            throw new BO.IlegalInput();
        DO.Product Doproduct = new DO.Product() { m_Name = product?.m_Name, m_Price = (double)product?.m_Price!, m_Category = (DO.Enums.Category?)product.m_Category, m_ID = product.m_Id, m_InStock = product.m_InStock };
        try { Dal!.Product.Update(Doproduct); }
        catch (Exception inner) { throw new FaildUpdating(inner); }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public IEnumerable<ProductForList?> FilterBycategory(BO.Enums.Category c)
    {
        if (c == BO.Enums.Category.None)
            return ProductList();
        return ProductList().Where(x => x.m_Category == c);
}

public IEnumerable<ProductItem> FilterBycategoryCustomer(BO.Enums.Category c)
{
    if (c == BO.Enums.Category.None)
        return CatalogList();
    return CatalogList().Where(x => x.Category == c);
}

}
