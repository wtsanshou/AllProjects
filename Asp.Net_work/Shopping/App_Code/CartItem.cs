using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CartItem
/// </summary>
public class CartItem
{
    private Product Product;
    private int Quantity;

    public CartItem(Product selectedProduct, int quantity)
    {
        this.Product = selectedProduct;
        this.Quantity = quantity;
    }

    public Product cProduct
    {
        get
        {
            return Product;
        }
    }
    public int cQuantity
    {
        get
        {
            return Quantity;
        }
        set
        {
            Quantity = value;
        }
    }
    public string Display()
    {
        string displayString = Product.pName + " (" + Quantity.ToString()
            + " at " + Product.pUnitPrice.ToString("c") + " each)";
        return displayString;
    }
}