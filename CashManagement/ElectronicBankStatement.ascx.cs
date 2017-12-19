using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BetAndWin.Payments.DomainModel;
using CQRPayments.PaymentService.DomainModel;
using CQRPayments.PaymentService.DomainModel.DataAccessInterfaces.ExternalPaymentBatching.ElectronicBankStatement;
using Framework.ObjectLocation;
using Microsoft.Security.Application;


namespace BetAndWin.PaymentService.Web.Controls.CashManagement
{
    /// <summary>
    /// Displays transactions of the bank statement.
    /// </summary>
    public partial class ElectronicBankStatement : ContentControl
    {
        private int _transactionsCount;
        private int _externalPaymentBatchID;
        private PaymentDirection _direction;

        public override string Title
        {
            get
            {
                return "BankStatement";
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            _externalPaymentBatchID = int.Parse(Request.QueryString["ID"]);
            bankStatementDetails.ExternalPaymentBatchID = _externalPaymentBatchID;
            
            string subViewType = Request.QueryString["Direction"];
            _direction = PaymentDirection.Unspecified;
            if (!String.IsNullOrEmpty(subViewType))
            {
				if (subViewType == "Credit")
				{
					_direction = PaymentDirection.Deposit;
				}
				else if (subViewType == "Debit")
				{
					_direction = PaymentDirection.Withdrawal;
				}
				else
				{
					_direction = PaymentDirection.Unspecified;
				}
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (_externalPaymentBatchID == 0)
                return;
            
            using (IDataReader dr =
				ObjectLocator.Get<IElectronicBankStatementTransactionDA>().
                    SelectTransactionsByElectronicBankStatementRaw(
                        _externalPaymentBatchID, 
                        _direction
                )
            )
            {
                bankStatementTransactionsRepeater.DataSource = dr;
                bankStatementTransactionsRepeater.DataBind();
            }


            if (_transactionsCount > 0)
            {
                Label itemCountLabel = bankStatementTransactionsRepeater.Controls[0].Controls[0].FindControl("transactionCountLabel") as Label;
                if (itemCountLabel != null)
                    itemCountLabel.Text = _transactionsCount + " Items";
            }
        }

        protected void bankStatementTransactionsRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item && e.Item.ItemIndex % 2 == 0)
            {
                HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("bankStatementTransactionRow");
                row.Attributes["class"] = "alternate";
            }
        }

        protected void bankStatementTransactionsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _transactionsCount++;

                if (((IDataRecord)e.Item.DataItem)["ExternalTransactionDate"] != DBNull.Value)
                {
					string externalTransactionDate = ((DateTime)((IDataRecord)e.Item.DataItem)["ExternalTransactionDate"]).ToString("dd.MM.yyyy");
					((Label)e.Item.FindControl("valueDateLabel")).Text = Microsoft.Security.Application.Encoder.HtmlEncode(externalTransactionDate);
                }

                if (((IDataRecord)e.Item.DataItem)["BookingDate"] != DBNull.Value)
                {
					string bookingDate = ((DateTime)((IDataRecord)e.Item.DataItem)["BookingDate"]).ToString("dd.MM.yyyy");
					((Label)e.Item.FindControl("entryDateLabel")).Text = Microsoft.Security.Application.Encoder.HtmlEncode(bookingDate);
                }

                string creditDebitMark;
                bool isReversal = false;
                int paymentDirection = ((int)((IDataRecord)e.Item.DataItem)["ExternalPaymentDirectionID"]);
                if (((IDataRecord)e.Item.DataItem)["IsReversal"] != DBNull.Value)
                {
                    isReversal = ((bool)((IDataRecord)e.Item.DataItem)["IsReversal"]);
                }

				if (paymentDirection == 1)
				{
					creditDebitMark = isReversal ? "RD" : "C";
				}
				else
				{
					creditDebitMark = isReversal ? "RC" : "D";
				}

            	((Label)e.Item.FindControl("paymentDirectionLabel")).Text = creditDebitMark;

                
                if (((IDataRecord)e.Item.DataItem)["Amount"] != DBNull.Value)
                {
                    string amount = ((decimal)((IDataRecord)e.Item.DataItem)["Amount"]).ToString("N2");
					((Label)e.Item.FindControl("transactionAmountLabel")).Text = Microsoft.Security.Application.Encoder.HtmlEncode(amount);
                }
            }
        }
    }
}