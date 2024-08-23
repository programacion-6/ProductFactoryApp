namespace ProductFactoryApp.Test;

using ProductFactoryApp.Core;
using ProductFactoryApp.Enums;
using ProductFactoryApp.Interfaces;
using ProductFactoryApp.Test.Mocks;

public class OrderServiceTests
{
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _orderService = new();
    }

    [Fact]
    public void AddProduct_SingleProduct_ShouldAddProdctToOrder()
    {
        var (productCategory, book) = GetProduct(Category.Book);

        _orderService.AddProduct(book);

        Assert.Contains(_orderService.GetProducts(), product => 
            product.GetCategory() == productCategory &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
    }

    [Fact]
    public void RemoveProduct_SingleProduct_ShouldRemoveProductFromOrder()
    {
        var (productCategory, book) = GetProduct(Category.Book);
        _orderService.AddProduct(book);

        _orderService.RemoveProduct(book);

        Assert.DoesNotContain(_orderService.GetProducts(), product =>
            product.GetCategory() == productCategory &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
    }

    public (Category category, IProduct product) GetProduct(Category category)
    {
        var productCategory = category;
        var product = ProductsMock.GetProduct(productCategory);

        return (category, product);
    }

    [Fact]
    public void AddProduct_MultipleProducts_ShouldAddAllProductsToOrder()
    {
        var (bookCategory, book) = GetProduct(Category.Book);
        var (electronicsCategory, electronics) = GetProduct(Category.Electronics);
        var (furnitureCategory, furniture) = GetProduct(Category.Furniture);

        _orderService.AddProduct(book);
        _orderService.AddProduct(electronics);
        _orderService.AddProduct(furniture);

        var products = _orderService.GetProducts();

        Assert.Contains(products, product =>
            product.GetCategory() == bookCategory &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
        
        Assert.Contains(products, product =>
            product.GetCategory() == electronicsCategory &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
        
        Assert.Contains(products, product =>
            product.GetCategory() == furnitureCategory &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
    }

    [Fact]
    public void RemoveProduct_MultipleProducts_ShouldRemoveSpecifiedProductsFromOrder()
    {
        var (bookCategory, book) = GetProduct(Category.Book);
        var (electronicsCategory, electronics) = GetProduct(Category.Electronics);
        var (furnitureCategory, furniture) = GetProduct(Category.Furniture);

        _orderService.AddProduct(book);
        _orderService.AddProduct(electronics);
        _orderService.AddProduct(furniture);

        _orderService.RemoveProduct(electronics);
        _orderService.RemoveProduct(furniture);

        var products = _orderService.GetProducts();

        Assert.Contains(products, product =>
            product.GetCategory() == bookCategory &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
        
        Assert.DoesNotContain(products, product =>
            product.GetCategory() == electronicsCategory &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
        
        Assert.DoesNotContain(products, product =>
            product.GetCategory() == furnitureCategory &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
    }
}
