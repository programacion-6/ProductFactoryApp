namespace ProductFactoryApp.Test;

using ProductFactoryApp.Core;
using ProductFactoryApp.Core.Products;
using ProductFactoryApp.Enums;

public class ProductCreatorTests
{
    private readonly ProductCreator _creator;

    public ProductCreatorTests()
    {
        _creator = new ProductCreator();
    }

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

    [Fact]
    public void CreateProduct_UnknownCategory_ShouldThrowArgumentException()
    {
        var unknownCategory = (Category)999; // Una categoría que no existe

        Assert.Throws<ArgumentException>(() => _creator.CreateProduct("Test Product", 10.00m, unknownCategory));
    }

    [Fact]
    public void CreateProduct_NullName_ShouldNotThrowException()
    {
        var product = _creator.CreateProduct(null, 10.00m, Category.Book);

        Assert.NotNull(product);
        Assert.Null(product.GetName());
    }

    [Fact]
    public void CreateProduct_EmptyName_ShouldNotThrowException()
    {
        var product = _creator.CreateProduct("", 10.00m, Category.Electronics);

        Assert.NotNull(product);
        Assert.Equal("", product.GetName());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-0.01)]
    public void CreateProduct_NegativePrice_ShouldNotThrowException(decimal price)
    {
        var product = _creator.CreateProduct("Test Product", price, Category.Furniture);

        Assert.NotNull(product);
        Assert.Equal(price, product.GetPrice());
    }

    [Fact]
    public void CreateProduct_ZeroPrice_ShouldNotThrowException()
    {
        var product = _creator.CreateProduct("Free Product", 0m, Category.Book);

        Assert.NotNull(product);
        Assert.Equal(0m, product.GetPrice());
    }
}
