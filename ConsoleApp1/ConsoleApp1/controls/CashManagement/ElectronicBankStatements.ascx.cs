using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using BetAndWin.Payments.DataLayer;
using CQRPayments.PaymentService.DomainModel.BankTransfers;
using CQRPayments.PaymentService.DomainModel.DataAccessInterfaces.BankTransfers;
using CQRPayments.PaymentService.DomainModel.DataAccessInterfaces.ExternalPaymentBatching.ElectronicBankStatement;
using Framework.ObjectLocation;
using Framework.Web.UI;
using Microsoft.Security.Application;
using Framework.Web.UI.Helpers;


namespace BetAndWin.PaymentService.Web.Controls.CashManagement
{
    /// <summary>
    /// Displays bank statements.
    /// </summary>
    public partial class ElectronicBankStatements : ContentControl
    {
        private int _itemCount;
        
        public override string Title
        {
            get
            {
                return "BankStatements";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Page newly loaded. Set BankStatementDate to the previous day as default.
				ebscFilter.BankStatementDateFilter.DateTime = DateTimeHelper.ConvertToUTC(DateTimeHelper.LocalDateTime.Date.AddDays(-1));
            }
            else
            {
                if (IsViewAccountNumberButtonPostBack())
                {
                    DoQuery(false);
                }
            }
        }

        private bool IsViewAccountNumberButtonPostBack()
        {
            // TODO - check if it is there any other way to check which control fired event from Page_Load
            bool result = false;
            string eventTarget = Request.Form["__EVENTTARGET"];
            if (!String.IsNullOrEmpty(eventTarget) && eventTarget.Contains("viewAccountNumberButton"))
            {
                result = true;
            }
            return result;
        }

        protected void viewButton_Click(object sender, EventArgs e)
        {
            DoQuery(true);
        }

        /// <summary>
        /// Gets data.
        /// </summary>
        /// <param name="checkValidation">Check validation.</param>
        private void DoQuery(bool checkValidation)
        {
            if (checkValidation && Page.IsPostBack && !Page.IsValid)
                return;

            using (IDataReader dr =
                ObjectLocator.Get<IElectronicBankStatementDA>().SelectRaw(
                    ebscFilter.MerchantFilter.SelectedMerchantID,
                    ebscFilter.BankAccountFilter.SelectedBankAccountID,
					DateTimeHelper.ConvertToLocal(ebscFilter.BankStatementDateFilter.DateTime) // As only Date part is important, don't consider Timezones 
                )
            )
            {
                this.bankStatementsRepeater.DataSource = dr;
                this.bankStatementsRepeater.DataBind();
            }
        }
        
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (_itemCount > 0)
            {
                {
                    LocalizableLabel resultLabel = this.bankStatementsRepeater.Controls[0].Controls[0].FindControl("resultsLabel") as LocalizableLabel;
                    if (resultLabel != null)
                        resultLabel.Visible = true;
                    Label itemCountLabel = this.bankStatementsRepeater.Controls[0].Controls[0].FindControl("itemCountLabel") as Label;
                    if (itemCountLabel != null)
                    {
                        itemCountLabel.Visible = true;
                        itemCountLabel.Text = this._itemCount.ToString() + " Items";
                    }
                }
            }
        }

        protected void bankStatementsRepeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewAccountNumber")
            {
                int bankAccountID = Convert.ToInt32(e.CommandArgument);
                BankAccount bankAccount = ObjectLocator.Get<IBankAccountDA>().Select(bankAccountID, true);

                Label label = e.Item.FindControl("accountNumberLabel") as Label;
                if (label != null)
                {
                    label.Text = bankAccount.DisplayAccountNumber;
                    label.Visible = true;
                }

                LocalizableLabel headerLabel = 
                    this.bankStatementsRepeater.Controls[0].Controls[0].FindControl("accountNumberHeaderLabel") as LocalizableLabel;
                if (headerLabel != null)
                {
                    headerLabel.Visible = true;
                }
            }
        }
        
        protected void bankStatementsRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item && e.Item.ItemIndex % 2 == 0)
            {
                HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("bankStatementsRow");
                row.Attributes["class"] = "alternate";
            }
        }

        protected void bankStatementsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				_itemCount++;

				if (((IDataRecord) e.Item.DataItem)["BankName"] != DBNull.Value)
				{
					((Label) e.Item.FindControl("bankNameLabel")).Text =
						Encoder.HtmlEncode((((IDataRecord) e.Item.DataItem)["BankName"]).ToString());
				}

				if (((IDataRecord) e.Item.DataItem)["BankStatementDate"] != DBNull.Value)
				{
					string date = ((DateTime) ((IDataRecord) e.Item.DataItem)["BankStatementDate"]).ToString("dd.MM.yyyy");
					((Label) e.Item.FindControl("bankStatementDateLabel")).Text = Microsoft.Security.Application.Encoder.HtmlEncode(date);
				}

				if (((IDataRecord) e.Item.DataItem)["StatementNumber"] != DBNull.Value)
				{
					((Label) e.Item.FindControl("statementNumberLabel")).Text =
						Encoder.HtmlEncode((((IDataRecord) e.Item.DataItem)["StatementNumber"]).ToString());
				}

				if (((IDataRecord) e.Item.DataItem)["Currency"] != DBNull.Value)
				{
					((Label) e.Item.FindControl("currencyLabel")).Text =
						Encoder.HtmlEncode((((IDataRecord) e.Item.DataItem)["Currency"]).ToString());
				}

				if (((IDataRecord) e.Item.DataItem)["OpenBalanceAmount"] != DBNull.Value)
				{
					string amount = ((decimal) ((IDataRecord) e.Item.DataItem)["OpenBalanceAmount"]).ToString("N2");

					if (((bool) ((IDataRecord) e.Item.DataItem)["OpenBalanceIsNegative"]))
					{
						amount = "-" + amount;
					}

					((Label) e.Item.FindControl("openingBalanceLabel")).Text = Microsoft.Security.Application.Encoder.HtmlEncode(amount);
				}

				if (((IDataRecord) e.Item.DataItem)["CreditAmount"] != DBNull.Value)
				{
					string amount = ((decimal) ((IDataRecord) e.Item.DataItem)["CreditAmount"]).ToString("N2");
					((Label) e.Item.FindControl("creditAmountLabel")).Text = Microsoft.Security.Application.Encoder.HtmlEncode(amount);
				}

				if (((IDataRecord) e.Item.DataItem)["DebitAmount"] != DBNull.Value)
				{
					string amount = ((decimal) ((IDataRecord) e.Item.DataItem)["DebitAmount"]).ToString("N2");
					((Label) e.Item.FindControl("debitAmountLabel")).Text = Microsoft.Security.Application.Encoder.HtmlEncode(amount);
				}

				if (((IDataRecord) e.Item.DataItem)["CloseBalanceAmount"] != DBNull.Value)
				{
					string amount = ((decimal) ((IDataRecord) e.Item.DataItem)["CloseBalanceAmount"]).ToString("N2");

					if (((bool) ((IDataRecord) e.Item.DataItem)["CloseBalanceIsNegative"]))
					{
						amount = "-" + amount;
					}

					((Label) e.Item.FindControl("closingBalanceLabel")).Text = Microsoft.Security.Application.Encoder.HtmlEncode(amount);
				}

				if (((IDataRecord) e.Item.DataItem)["AvailBalanceAmount"] != DBNull.Value)
				{
					string amount = ((decimal) ((IDataRecord) e.Item.DataItem)["AvailBalanceAmount"]).ToString("N2");

					if (((bool) ((IDataRecord) e.Item.DataItem)["AvailBalanceIsNegative"]))
					{
						amount = "-" + amount;
					}

					((Label) e.Item.FindControl("availBalanceLabel")).Text = Microsoft.Security.Application.Encoder.HtmlEncode(amount);
				}
			}
        }
    }
}