namespace ProductFactoryApp.Test;

using ProductFactoryApp.Core.Products;
using ProductFactoryApp.Enums;

public class ProductTests
{
    [Fact]
    public void Book_Initialized_ShouldBeInitializedCorrectly()
    {
        var name = "Test Book";
        var price = 29.99m;

        var book = new Book(name, price);

        Assert.NotNull(book);
        Assert.Equal(name, book.Name);
        Assert.Equal(price, book.Price);
        Assert.Equal(Category.Book, book.Category);
    }

    [Fact]
    public void Electronics_Initialized_ShouldBeInitializedCorrectly()
    {
        var name = "Test Phone";
        var price = 499.99m;

        var electronics = new Electronics(name, price);

        Assert.NotNull(electronics);
        Assert.Equal(name, electronics.Name);
        Assert.Equal(price, electronics.Price);
        Assert.Equal(Category.Electronics, electronics.Category);
    }

    [Fact]
    public void Furniture_Initialized_ShouldBeInitializedCorrectly()
    {
        var name = "Test Sofa";
        var price = 899.99m;

        var furniture = new Furniture(name, price);

        Assert.NotNull(furniture);
        Assert.Equal(name, furniture.Name);
        Assert.Equal(price, furniture.Price);
        Assert.Equal(Category.Furniture, furniture.Category);
    }
}
