using BO;

namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// The function builds a list of products and returns it (for an admin screen)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ProductForList?> ProductList();

    /// <summary>
    /// The function builds a product catalog and returns it (for a customer's screen)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductItem?> CatalogList();

    /// <summary>
    /// The function receives a product ID and returns a built product (for an admin screen)
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public BO.Product ProductId(int? ID);

    /// <summary>
    /// The function receives a product ID and returns a built product (for a customer screen - from the catalog)
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public ProductItem CatalogProductId(int? ID);

    /// <summary>
    /// The function compares a product if it is correct, to the data overlay (for the manager screen)
    /// </summary>
    /// <param name="product"></param>
    public void AddProduct(BO.Product? product);

    /// <summary>
    /// The function deletes a product if it is possible (for an administrator screen)
    /// </summary>
    /// <param name="product"></param>
    public void DeleteProduct(int? ID);

    /// <summary>
    /// The function updates a product if it is possible (for an administrator screen)
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProduct(BO.Product? product);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public IEnumerable<ProductForList?> FilterBycategory (BO.Enums.Category c);

}
