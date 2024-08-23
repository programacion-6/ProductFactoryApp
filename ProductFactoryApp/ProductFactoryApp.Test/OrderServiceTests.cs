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
        var product = ProductsMock.GetProduct(category);
        return (category, product);
    }

    [Fact]
    public void AddProducts_MultipleProducts_ShouldAddAllProductsToOrder()
    {
        var products = new List<IProduct>
        {
            ProductsMock.GetProduct(Category.Book),
            ProductsMock.GetProduct(Category.Electronics),
            ProductsMock.GetProduct(Category.Furniture)
        };

        // Agregar cada producto al pedido
        foreach (var product in products)
        {
            _orderService.AddProduct(product);
        }

        foreach (var product in products)
        {
            Assert.Contains(_orderService.GetProducts(), p =>
                p.GetCategory() == product.GetCategory() &&
                p.GetName() == product.GetName() &&
                p.GetPrice() == product.GetPrice());
        }
    }

    [Fact]
    public void RemoveProducts_MultipleProducts_ShouldRemoveAllProductsFromOrder()
    {
        var products = new List<IProduct>
        {
            ProductsMock.GetProduct(Category.Book),
            ProductsMock.GetProduct(Category.Electronics),
            ProductsMock.GetProduct(Category.Furniture)
        };

        foreach (var product in products)
        {
            _orderService.AddProduct(product);
        }

        foreach (var product in products)
        {
            _orderService.RemoveProduct(product);
        }

        foreach (var product in products)
        {
            Assert.DoesNotContain(_orderService.GetProducts(), p =>
                p.GetCategory() == product.GetCategory() &&
                p.GetName() == product.GetName() &&
                p.GetPrice() == product.GetPrice());
        }
    }

    [Fact]
    public void GetTotalPrice_MultipleProducts_ShouldReturnCorrectTotal()
    {
        var products = new List<IProduct>
    {
        ProductsMock.GetProduct(Category.Book),
        ProductsMock.GetProduct(Category.Electronics),
        ProductsMock.GetProduct(Category.Furniture)
    };

        decimal expectedTotal = 0;

        foreach (var product in products)
        {
            _orderService.AddProduct(product);
            expectedTotal += product.GetPrice();
        }

        var actualTotal = _orderService.GetTotalPrice();

        Assert.Equal(expectedTotal, actualTotal);
    }

    [Fact]
    public void RemoveNonExistentProduct_ShouldNotThrowException()
    {
        var product = ProductsMock.GetProduct(Category.Book);

        var exception = Record.Exception(() => _orderService.RemoveProduct(product));

        Assert.Null(exception);
        Assert.Empty(_orderService.GetProducts());
    }


}
