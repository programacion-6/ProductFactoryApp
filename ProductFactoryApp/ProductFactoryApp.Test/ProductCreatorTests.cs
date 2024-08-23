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
    public void CreateProduct_Product_ShouldReturnCorrectProductType(string name, decimal price, Category category,
        Type expectedType)
    {
        var creator = new ProductCreator();

        var product = creator.CreateProduct(name, price, category);

        Assert.IsType(expectedType, product);
        Assert.Equal(name, product.GetName());
        Assert.Equal(price, product.GetPrice());
        Assert.Equal(category, product.GetCategory());
    }

    [Theory]
    [InlineData(null, 29.99, Category.Book)]
    [InlineData("", 29.99, Category.Book)]
    [InlineData("Test Book", -10, Category.Book)]
    public void CreateProduct_InvalidNameOrPrice_ShouldNotThrowException(string name, decimal price, Category category)
    {
        var creator = new ProductCreator();

        var product = creator.CreateProduct(name, price, category);

        Assert.NotNull(product);
        Assert.Equal(name, product.GetName());
        Assert.Equal(price, product.GetPrice());
        Assert.Equal(category, product.GetCategory());
    }

    [Fact]
    public void CreateProduct_InvalidCategory_ShouldThrowArgumentException()
    {
        var creator = new ProductCreator();

        var invalidCategory = (Category)999;

        Assert.Throws<ArgumentException>(() => creator.CreateProduct("Test Invalid", 29.99m, invalidCategory));
    }
    
    [Theory]
    [InlineData(null, -1, (Category)0)]
    [InlineData("", 0, (Category)0)]
    public void CreateProduct_AllInvalidParameters_ShouldNotThrowException(string name, decimal price,
        Category category)
    {
        var creator = new ProductCreator();

        var product = creator.CreateProduct(name, price, category);

        Assert.NotNull(product);
        Assert.Equal(name, product.GetName());
        Assert.Equal(price, product.GetPrice());
        Assert.Equal(category, product.GetCategory());
    }

    [Theory]
    [InlineData("!@#$%^&*()", 19.99, Category.Book)]
    [InlineData("1234567890", 19.99, Category.Electronics)]
    public void CreateProduct_NameWithSpecialCharacters_ShouldNotThrowException(string name, decimal price,
        Category category)
    {
        var creator = new ProductCreator();

        var product = creator.CreateProduct(name, price, category);

        Assert.NotNull(product);
        Assert.Equal(name, product.GetName());
        Assert.Equal(price, product.GetPrice());
        Assert.Equal(category, product.GetCategory());
    }
    
    [Fact]
    public void CreateProduct_WithExtremelyLongName_ShouldNotThrowException()
    {
        var creator = new ProductCreator();

        var longName = new string('A', 1000);

        var product = creator.CreateProduct(longName, 29.99m, Category.Book);

        Assert.NotNull(product);
        Assert.Equal(longName, product.GetName());
        Assert.Equal(29.99m, product.GetPrice());
        Assert.Equal(Category.Book, product.GetCategory());
    }

}
