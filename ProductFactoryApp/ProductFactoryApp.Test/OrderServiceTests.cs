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

    // TODO: Add for multiple
    [Fact]
    public void AddProduct_MultipleProducts_ShouldAddProductsToOrder()
    {
        var (productCategory1, book1) = GetProduct(Category.Book);
        var (productCategory2, book2) = GetProduct(Category.Book);
        var (productCategory3, book3) = GetProduct(Category.Book);

        _orderService.AddProduct(book1);
        _orderService.AddProduct(book2);
        _orderService.AddProduct(book3);

        Assert.Contains(_orderService.GetProducts(), product =>
            product.GetCategory() == productCategory1 &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);

        Assert.Contains(_orderService.GetProducts(), product =>
            product.GetCategory() == productCategory2 &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);

        Assert.Contains(_orderService.GetProducts(), product =>
            product.GetCategory() == productCategory3 &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
    }

        [Fact]
        public void RemoveProduct_MultipleProducts_ShouldRemoveProductsFromOrder()
        {
            var (productCategory1, book1) = GetProduct(Category.Book);
            var (productCategory2, book2) = GetProduct(Category.Book);
            _orderService.AddProduct(book1);
            _orderService.AddProduct(book2);

            Assert.Contains(_orderService.GetProducts(), product =>
            product.GetCategory() == productCategory1 &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);

            Assert.Contains(_orderService.GetProducts(), product =>
            product.GetCategory() == productCategory2 &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
            
            _orderService.RemoveProduct(book1);
            _orderService.RemoveProduct(book2);

            Assert.DoesNotContain(_orderService.GetProducts(), product =>
            product.GetCategory() == productCategory1 &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);

            Assert.DoesNotContain(_orderService.GetProducts(), product =>
            product.GetCategory() == productCategory2 &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);

            var (productCategory3, book3) = GetProduct(Category.Book);
            _orderService.AddProduct(book3);

            Assert.Contains(_orderService.GetProducts(), product =>
            product.GetCategory() == productCategory3 &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
            _orderService.RemoveProduct(book3); 

            Assert.DoesNotContain(_orderService.GetProducts(), product =>
            product.GetCategory() == productCategory3 &&
            product.GetName() == ProductsMock._productName &&
            product.GetPrice() == ProductsMock._productPrice);
        }
}
