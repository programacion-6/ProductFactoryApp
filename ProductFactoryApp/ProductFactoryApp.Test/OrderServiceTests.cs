﻿namespace ProductFactoryApp.Test;

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
        var products = GetMultipleProducts();

        foreach (var product in products)
        {
            _orderService.AddProduct(product.product);
        }

        foreach (var (productCategory, product) in products)
        {
            Assert.Contains(_orderService.GetProducts(), p =>
                p.GetCategory() == productCategory &&
                p.GetName() == product.GetName() &&
                p.GetPrice() == product.GetPrice());
        }
    }

    [Fact]
    public void RemoveProduct_MultipleProducts_ShouldRemoveAllProductsFromOrder()
    {
        var products = GetMultipleProducts();

        foreach (var product in products)
        {
            _orderService.AddProduct(product.product);
        }

        foreach (var (productCategory, product) in products)
        {
            _orderService.RemoveProduct(product);
        }

        foreach (var (productCategory, product) in products)
        {
            Assert.DoesNotContain(_orderService.GetProducts(), p =>
                p.GetCategory() == productCategory &&
                p.GetName() == product.GetName() &&
                p.GetPrice() == product.GetPrice());
        }
    }

    public List<(Category category, IProduct product)> GetMultipleProducts()
    {
        return new List<(Category category, IProduct product)>
        {
            (Category.Book, ProductsMock.GetProduct(Category.Book)),
            (Category.Electronics, ProductsMock.GetProduct(Category.Electronics)),
            (Category.Furniture, ProductsMock.GetProduct(Category.Furniture))
        };
    }
}
