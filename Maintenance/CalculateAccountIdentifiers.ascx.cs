using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BetAndWin.Payments.DataLayer;

namespace BetAndWin.PaymentService.Web.Controls.Maintenance
{
    public partial class CalculateAccountIdentifiers : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCalculateAccountIdentifiers_Click(object sender, EventArgs e)
        {
            string paymentAccountType = paymentAccountTypeInput.Text;
        }
    }
}