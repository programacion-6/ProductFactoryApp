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
        var name = "Test Electronic";
        var price = 39.99m;

        var electronic = new Electronics(name, price);

        Assert.NotNull(electronic);
        Assert.Equal(name, electronic.Name);
        Assert.Equal(price, electronic.Price);
        Assert.Equal(Category.Electronics, electronic.Category);
    }

    [Fact]
    public void Furniture_Initialized_ShouldBeInitializedCorrectly()
    {
        var name = "Test Furniture";
        var price = 294.99m;

        var furniture = new Furniture(name, price);

        Assert.NotNull(furniture);
        Assert.Equal(name, furniture.Name);
        Assert.Equal(price, furniture.Price);
        Assert.Equal(Category.Furniture, furniture.Category);
    }
}
