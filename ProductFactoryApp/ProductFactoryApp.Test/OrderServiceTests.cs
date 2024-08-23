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
        var productCategory = Category.Book;
        var product = ProductsMock.GetProduct(productCategory);

        return (category, product);
    }

    [Fact]
    public void AddProduct_MultipleProducts_ShouldAddAllProductsToOrder()
    {
        var book = ProductsMock.GetProduct(Category.Book);
        var electronics = ProductsMock.GetProduct(Category.Electronics);
        var furniture = ProductsMock.GetProduct(Category.Furniture);

        _orderService.AddProduct(book);
        _orderService.AddProduct(electronics);
        _orderService.AddProduct(furniture);

        var products = _orderService.GetProducts();

        Assert.Contains(products, p => p.GetName() == book.GetName() && p.GetCategory() == Category.Book);
        Assert.Contains(products, p => p.GetName() == electronics.GetName() && p.GetCategory() == Category.Electronics);
        Assert.Contains(products, p => p.GetName() == furniture.GetName() && p.GetCategory() == Category.Furniture);
    }

    [Fact]
    public void RemoveProduct_MultipleProducts_ShouldRemoveAllProductsFromOrder()
    {
        var book = GetProduct(Category.Book).product;
        var electronics = GetProduct(Category.Electronics).product;
        var furniture = GetProduct(Category.Furniture).product;

        _orderService.AddProduct(book);
        _orderService.AddProduct(electronics);
        _orderService.AddProduct(furniture);

        _orderService.RemoveProduct(book);
        _orderService.RemoveProduct(electronics);
        _orderService.RemoveProduct(furniture);

        Assert.DoesNotContain(_orderService.GetProducts(), product => product.GetCategory() == Category.Book);
        Assert.DoesNotContain(_orderService.GetProducts(), product => product.GetCategory() == Category.Electronics);
        Assert.DoesNotContain(_orderService.GetProducts(), product => product.GetCategory() == Category.Furniture);
    }

}
