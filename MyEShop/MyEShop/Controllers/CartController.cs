using EF_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class CartController : Controller
{
    private readonly CartManager _cartManager;

    public CartController(CartManager cartManager)
    {
        _cartManager = cartManager;
    }

    public IActionResult Index()
    {
        var cart = _cartManager.GetCartItems();
        return View(cart);
    }

    public IActionResult AddToCart(int id, string name, decimal price)
    {
        var item = new CartItem
        {
            Id = id,
            Name = name,
            Price = price,
            Quantity = 1
        };

        _cartManager.AddToCart(item);
        return RedirectToAction("Index");
    }

    public IActionResult RemoveFromCart(int id)
    {
        _cartManager.RemoveFromCart(id);
        return RedirectToAction("Index");
    }

    public IActionResult ClearCart()
    {
        _cartManager.ClearCart();
        return RedirectToAction("Index");
    }
}
