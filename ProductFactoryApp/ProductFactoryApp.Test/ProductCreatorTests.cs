namespace ProductFactoryApp.Test;

using ProductFactoryApp.Core;
using ProductFactoryApp.Core.Products;
using ProductFactoryApp.Enums;

public class ProductCreatorTests
{
    [Theory]
    [InlineData("Test Book", 29.99, Category.Book, typeof(Book))]
    [InlineData("Test Electronics", 999.99, Category.Electronics, typeof(Electronics))]
    [InlineData("Test Furniture", 99.99, Category.Furniture, typeof(Furniture))]
    public void CreateProduct_Product_ShouldReturnCorrectProductType(string name, decimal price, Category category, Type expectedType)
    {
        var creator = new ProductCreator();

        var product = creator.CreateProduct(name, price, category);

        Assert.IsType(expectedType, product);
        Assert.Equal(name, product.GetName());
        Assert.Equal(price, product.GetPrice());
        Assert.Equal(category, product.GetCategory());
    }

    [Theory]
    [InlineData("Test Invalid", 29.99, (Category)999)] 
    public void CreateProduct_InvalidCategory_ShouldThrowArgumentException(string name, decimal price, Category category)
    {
        var creator = new ProductCreator();

        Assert.Throws<ArgumentException>(() => creator.CreateProduct(name, price, category));
    }

    [Fact]
    public void CreateProduct_NoProductCreated_ShouldNotContainInCollection()
    {
        var creator = new ProductCreator();
        var orderService = new OrderService();

        Assert.DoesNotContain(orderService.GetProducts(), p => p.GetName() == "Book");
    }

    [Fact]
    public void RemoveProduct_ProductNotInOrder_ShouldNotContain()
    {
        var creator = new ProductCreator();
        var orderService = new OrderService();

        var product = creator.CreateProduct("Non-Existent Product", 99.99m, Category.Furniture);
        
        orderService.RemoveProduct(product);

        Assert.DoesNotContain(orderService.GetProducts(), p => p.GetName() == "Non-Existent Product");
    }

}
