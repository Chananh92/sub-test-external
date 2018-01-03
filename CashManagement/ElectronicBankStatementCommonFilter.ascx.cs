using System;
using System.Web.UI;
using BetAndWin.Payments.DataLayer;
using BetAndWin.Payments.DomainModel;
using CQRPayments.PaymentService.DomainModel;
using System.Collections.Generic;
using Framework.ObjectLocation;

namespace BetAndWin.PaymentService.Web.Controls.CashManagement
{
	/// <summary>
	/// Filter for bank statements.
	/// </summary>
	public partial class ElectronicBankStatementCommonFilter : UserControl
	{
		#region Properties

		/// <summary>
		/// Gets the merchant filter.
		/// </summary>
		/// <value>The merchant filter.</value>
		public MerchantFilter MerchantFilter
		{
			get
			{
				return merchantFilter;
			}
		}

		/// <summary>
		/// Gets the bank account filter.
		/// </summary>
		/// <value>The bank account filter.</value>
		public BankAccountFilter BankAccountFilter
		{
			get
			{
				return bankAccountFilter;
			}
		}

		/// <summary>
		/// Gets the bank statement date filter.
		/// </summary>
		/// <value>The bank statement date filter.</value>
		public DateTimeFilter BankStatementDateFilter
		{
			get
			{
				return bankStatementDateFilter;
			}
		}

		#endregion


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				merchantFilter.LoadCombo();
				bankAccountFilter.MerchantID = merchantFilter.SelectedMerchantID;
			}
		}

		protected void merchantFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			bankAccountFilter.MerchantID = merchantFilter.SelectedMerchantID;
		}
	}
}