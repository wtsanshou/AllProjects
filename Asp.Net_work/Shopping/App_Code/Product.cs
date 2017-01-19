using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
///Product 的摘要说明
/// </summary>
public class Product
{
    private string ProductID;
    private string Name;
    private string ShortDescription;
    private string LongDescription;
    private decimal UnitPrice;
    private string ImageFile;

    public Product(DataRowView tableRow)
    {
        DataRowView row = tableRow;
        ProductID = row["ProductID"].ToString();
        Name = row["Name"].ToString();
        ShortDescription = row["ShortDescription"].ToString();
        LongDescription = row["LongDescription"].ToString();
        UnitPrice = (decimal)row["UnitPrice"];
        ImageFile = row["ImageFile"].ToString();
    }

    public string pProductID
    {
        get
        {
            return ProductID;
        }
    }
    public string pName
    {
        get
        {
            return Name;
        }
    }

    public string pShortDesc
    {
        get
        {
            return ShortDescription;
        }
    }

    public string pLongDesc
    {
        get
        {
            return LongDescription;
        }
    }

    public decimal pUnitPrice
    {
        get
        {
            return UnitPrice;
        }
    }
    public string pImageFile
    {
        get
        {
            return ImageFile;
        }
    }
}