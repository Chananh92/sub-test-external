using System;
using System.Web.UI;
using CQRPayments.PaymentService.DomainModel.DataAccessInterfaces.ExternalPaymentBatching.ElectronicBankStatement;
using CQRPayments.PaymentService.DomainModel.ExternalPaymentBatching.ElectronicBankStatement;
using Framework.ObjectLocation;

namespace BetAndWin.PaymentService.Web.Controls.CashManagement
{
    /// <summary>
    /// Control for showing the details of the bank statement.
    /// </summary>
    public partial class ElectronicBankStatementDetails : UserControl
    {
        #region Properties

        private CQRPayments.PaymentService.DomainModel.ExternalPaymentBatching.ElectronicBankStatement.ElectronicBankStatement statement = null;
        
        private int _externalPaymentBatchID;
        /// <summary>
        /// An ID of the external payment batch.
        /// </summary>
        public int ExternalPaymentBatchID
        {
            get
            {
                return _externalPaymentBatchID;
            }
            set
            {
                _externalPaymentBatchID = value;
            }
        }

        #endregion

        /// <summary>
        /// Load the control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            IElectronicBankStatementDA electronicBankStatementDA = ObjectLocator.Get<IElectronicBankStatementDA>();
            statement =
                (CQRPayments.PaymentService.DomainModel.ExternalPaymentBatching.ElectronicBankStatement.ElectronicBankStatement)electronicBankStatementDA.SelectExternalPaymentBatchByID(_externalPaymentBatchID, true);
        }

        /// <summary>
        /// Event handler for viewClearTextAccountNumberButton link button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void viewClearTextAccountNumberButton_Click(object sender, EventArgs e)
        {
            if (statement.BankAccount != null)
            {
                accountNumberLiteral.Text = statement.BankAccount.DisplayAccountNumber;
            }
        }

        /// <summary>
        /// PreRender event handler.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            if (statement == null)
                return;

            if (statement.BankAccount != null)
            {
                bankNameLiteral.Text = statement.BankAccount.BankName;
                accountNameLiteral.Text = statement.BankAccount.Name;
            }
            if (statement.ExternalDate != null)
            {
                bankStatementDateLiteral.Text = ((DateTime)statement.ExternalDate).ToString("dd.MM.yyyy");
            }
            bankStatementNumberLiteral.Text = statement.StatementNumber;
            if (statement.GrossAmountCurrency != null)
            {
                currencyLiteral.Text = statement.GrossAmountCurrency.Code;
            }
            creditsSumAmountLiteral.Text = statement.CreditAmount.ToString("N2");
            debitsSumAmountLiteral.Text = statement.DebitAmount.ToString("N2");

            if (statement.OpeningBalance != null)
            {
                startingAccountBalanceLiteral.Text = statement.OpeningBalance.Amount.Amount.ToString("N2");
            }

            if (statement.ClosingBalance != null)
            {
                endingAccountBalanceLiteral.Text = statement.ClosingBalance.Amount.Amount.ToString("N2");
            }

            foreach (ElectronicBankStatementBalance balance in statement.Balances)
            {
                if (balance.BalanceType == ElectronicBankStatementBalanceType.ClosingAvailableBalance)
                {
                    closingAvailableBalanceLiteral.Text = balance.Amount.Amount.ToString("N2");
                    break;
                }
            }
        }
    }
}