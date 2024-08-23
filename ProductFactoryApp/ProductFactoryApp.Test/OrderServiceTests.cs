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
        Assert.Equal(3, products.Count);
        Assert.Contains(products, p => p.GetCategory() == Category.Book);
        Assert.Contains(products, p => p.GetCategory() == Category.Electronics);
        Assert.Contains(products, p => p.GetCategory() == Category.Furniture);
        Assert.All(products, p => Assert.Equal(ProductsMock._productName, p.GetName()));
        Assert.All(products, p => Assert.Equal(ProductsMock._productPrice, p.GetPrice()));
    }

    [Fact]
    public void GetTotalPrice_MultipleProducts_ShouldReturnCorrectTotalPrice()
    {
        var book = ProductsMock.GetProduct(Category.Book);
        var electronics = ProductsMock.GetProduct(Category.Electronics);
        var furniture = ProductsMock.GetProduct(Category.Furniture);

        _orderService.AddProduct(book);
        _orderService.AddProduct(electronics);
        _orderService.AddProduct(furniture);

        var expectedTotalPrice = ProductsMock._productPrice * 3; 
        var actualTotalPrice = _orderService.GetTotalPrice();

        Assert.Equal(expectedTotalPrice, actualTotalPrice);
    }

    [Fact]
    public void GetProductsByCategory_MultipleProducts_ShouldReturnCorrectProductsForCategory()
    {
        var book1 = ProductsMock.GetProduct(Category.Book);
        var book2 = ProductsMock.GetProduct(Category.Book);
        var electronics = ProductsMock.GetProduct(Category.Electronics);

        _orderService.AddProduct(book1);
        _orderService.AddProduct(book2);
        _orderService.AddProduct(electronics);

        var bookProducts = _orderService.GetProductsByCategory(Category.Book);

        Assert.Equal(2, bookProducts.Count);
        Assert.All(bookProducts, p => Assert.Equal(Category.Book, p.GetCategory()));
        Assert.All(bookProducts, p => Assert.Equal(ProductsMock._productName, p.GetName()));
        Assert.All(bookProducts, p => Assert.Equal(ProductsMock._productPrice, p.GetPrice()));
        Assert.DoesNotContain(bookProducts, p => p.GetCategory() == Category.Electronics);
    }
}
