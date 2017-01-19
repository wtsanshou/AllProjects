using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Order : System.Web.UI.Page
{
    private Product product;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            NameList.DataBind();
        product = this.GetSelectedProduct();
        //DataView productsTable = (DataView)AccessDataSource1.Select(DataSourceSelectArguments.Empty);
        //productsTable.RowFilter = "ProductID = '" + NameList.SelectedValue + "'";
        //DataRowView row = (DataRowView)productsTable[0];
        Name.Text = product.pName;
        ShortDes.Text = product.pShortDesc;
        LongDes.Text = product.pLongDesc;
        Image.ImageUrl = "~/Images/Products/" + product.pImageFile;
        Price.Text = product.pUnitPrice.ToString("c");

        if (PreviousPage != null)
        {
            TextBox Quantity = (TextBox)PreviousPage.FindControl("Quantity");
            
        }

    }
    private Product GetSelectedProduct()
    {
        DataView productsTable = (DataView)
            AccessDataSource1.Select(DataSourceSelectArguments.Empty);
        productsTable.RowFilter =
            "ProductID = '" + NameList.SelectedValue + "'";
        DataRowView row = (DataRowView)productsTable[0];
        Product p = new Product(row);
        return p;
    } 
    protected void AddCart_Click(object sender, EventArgs e)
    {

    }
}