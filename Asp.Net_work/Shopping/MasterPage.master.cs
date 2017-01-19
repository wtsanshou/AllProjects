using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int daysUntil = DaysUntilHalloween();
        if (daysUntil == 0)
            lblMessage.Text = "Happy Halloween!";
        else if (daysUntil == 1)
            lblMessage.Text = "Tomorrow is Halloween!";
        else
            lblMessage.Text = "There are only " + daysUntil
                + " days left until Halloween!";
    }

    private int DaysUntilHalloween()
    {
        DateTime halloween = new DateTime(DateTime.Today.Year, 10, 31);
        if (DateTime.Today > halloween)
            halloween = halloween.AddYears(1);
        TimeSpan ts = halloween - DateTime.Today;
        return ts.Days;
    }

}
