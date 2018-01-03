using System;

namespace BetAndWin.PaymentService.Web.Controls
{
	/// <summary>
	/// Shipping details control
	/// </summary>
	public partial class PostepayWithdrawalDetailsControl : System.Web.UI.UserControl
	{
		private const string _POSTEPAY_WITHDRAWAL_HOLDER_IS_NOT_SPECIFIED_TEXT = "Not Specified";

		/// <summary>
		/// Sets the first name of the cardholder.
		/// </summary>
		/// <value>The name of the shipping receiver.</value>
		public string HolderFirstname
		{
			set
			{
				firstnameLiteral.Text = value;
			}
		}
		/// <summary>
		/// Sets the first name of the cardholder.
		/// </summary>
		/// <value>The name of the shipping receiver.</value>
		public string HolderLastname
		{
			set
			{
				lastnameLiteral.Text = value;
			}
		}
		/// <summary>
		/// Width of table
		/// </summary>
		public string Width = "30%";

		protected void Page_Load(object sender, EventArgs e)
		{
			//firstnameLiteral.Text = lastnameLiteral.Text = _POSTEPAY_WITHDRAWAL_HOLDER_IS_NOT_SPECIFIED_TEXT;
		}
	}
}