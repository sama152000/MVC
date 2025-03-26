using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using EF_Core.Models;
using Newtonsoft.Json;

public class CartManager
{
    private const string CartSessionKey = "Cart";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public List<CartItem> GetCartItems()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var sessionCart = session.GetString(CartSessionKey);

        return sessionCart != null ? JsonConvert.DeserializeObject<List<CartItem>>(sessionCart) : new List<CartItem>();
    }

    public void AddToCart(CartItem item)
    {
        var cart = GetCartItems();
        var existingItem = cart.FirstOrDefault(p => p.Id == item.Id);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            cart.Add(item);
        }

        SaveCartItems(cart);
    }

    public void RemoveFromCart(int productId)
    {
        var cart = GetCartItems();
        cart.RemoveAll(p => p.Id == productId);
        SaveCartItems(cart);
    }

    public void ClearCart()
    {
        _httpContextAccessor.HttpContext.Session.Remove(CartSessionKey);
    }

    private void SaveCartItems(List<CartItem> cart)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        session.SetString(CartSessionKey, JsonConvert.SerializeObject(cart));
    }
}
