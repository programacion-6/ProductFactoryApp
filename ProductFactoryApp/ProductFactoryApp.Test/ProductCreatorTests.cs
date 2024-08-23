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

    [Fact]
    public void CreateProduct_InvalidCategory_ShouldThrowArgumentException()
    {
        var creator = new ProductCreator();
        
        Assert.Throws<ArgumentException>(() => creator.CreateProduct("Test Invalid", 50.0m, (Category)999));
    }

    [Fact]
    public void CreateProduct_NullName_ShouldThrowArgumentNullException()
    {
        var creator = new ProductCreator();

        Assert.Throws<ArgumentNullException>(() => creator.CreateProduct(null, 29.99m, Category.Book));
    }

    [Fact]
    public void CreateProduct_NegativePrice_ShouldThrowArgumentOutOfRangeException()
    {
        var creator = new ProductCreator();

        Assert.Throws<ArgumentOutOfRangeException>(() => creator.CreateProduct("Test Negative Price", -10.0m, Category.Book));
    }

    [Fact]
    public void CreateProduct_ZeroPrice_ShouldThrowArgumentOutOfRangeException()
    {
        var creator = new ProductCreator();

        Assert.Throws<ArgumentOutOfRangeException>(() => creator.CreateProduct("Test Zero Price", 0.0m, Category.Book));
    }
}
