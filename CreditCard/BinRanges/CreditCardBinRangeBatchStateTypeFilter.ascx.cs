using System;
using CQRPayments.PaymentService.Admin.Presenters.Loaders;
using CQRPayments.PaymentService.DomainModel.CreditCards.BinRanges;
using Framework.Web.UI;

namespace BetAndWin.PaymentService.Web.Controls
{
    /// <summary>
	/// Filter field for filtering by CreditCardBinRangeBatchStateTypeID.
    /// </summary>
	public partial class CreditCardBinRangeBatchStateTypeFilter : System.Web.UI.UserControl
    {
		public event EventHandler SelectedIndexChanged;

        private bool _isAllAllowed = true;
		/// <summary>
		/// gets or sets if "All"-item is shown in the creditCardBinRangeBatchStateTypeComboBox
		/// </summary>
        public bool IsAllAllowed
        {
            get { return _isAllAllowed; }
            set { _isAllAllowed = value; }
        }

    	/// <summary>
    	/// gets or sets if "None"-item is shown in the creditCardBinRangeBatchStateTypeComboBox
    	/// </summary>
    	public bool IsNoneAllowed { get; set; }

    	/// <summary>
		/// Gets or sets the label.
		/// </summary>
		/// <value>The label.</value>
    	public string Label
    	{
			get { return creditCardBinRangeBatchStateTypeLabel.Text; }
			set { creditCardBinRangeBatchStateTypeLabel.Text = value; }
    	}
        
		/// <summary>
		/// Selected Value of the creditCardBinRangeBatchStateTypeComboBox
		/// </summary>
		public CreditCardBinRangeBatchStateTypeID SelectedCreditCardBinRangeBatchStateType
        {
            get
            {
				CreditCardBinRangeBatchStateTypeID creditCardBinRangeBatchStateType = CreditCardBinRangeBatchStateTypeID.Undefined;

            	if (!string.IsNullOrEmpty(creditCardBinRangeBatchStateTypeCombo.SelectedValue))
					creditCardBinRangeBatchStateType = (CreditCardBinRangeBatchStateTypeID)int.Parse(creditCardBinRangeBatchStateTypeCombo.SelectedValue);

				return creditCardBinRangeBatchStateType;
            }
			set
            {
				creditCardBinRangeBatchStateTypeCombo.SelectedValue = ((int)value).ToString();
            }
        }

		/// <summary>
		/// set the AutoPostBack property of the creditCardBinRangeBatchStateTypeComboBox
		/// </summary>
		public bool AutoPostBack
		{
			get { return creditCardBinRangeBatchStateTypeCombo.AutoPostBack; }
			set { creditCardBinRangeBatchStateTypeCombo.AutoPostBack = value; }
		}

		protected void Page_Init(object sender, System.EventArgs e)
		{
			creditCardBinRangeBatchStateTypeCombo.Loader = new DropDownListLoaderAdapter(new CreditCardBinRangeBatchStateTypesLoader());
			creditCardBinRangeBatchStateTypeCombo.SelectedIndexChanged += creditCardBinRangeBatchStateTypeCombo_SelectedIndexChanged;
		}

		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if (IsAllAllowed || IsNoneAllowed)
			{
				creditCardBinRangeBatchStateTypeCombo.IsOptional = true;
				creditCardBinRangeBatchStateTypeCombo.ShowHeaderItem = true;
			}

			if (IsNoneAllowed)
				creditCardBinRangeBatchStateTypeCombo.HeaderItemText = "None";

			if (IsAllAllowed)
				creditCardBinRangeBatchStateTypeCombo.HeaderItemText = "All";
		}

		protected void creditCardBinRangeBatchStateTypeCombo_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			OnSelectedIndexChanged(e);
		}

		protected void OnSelectedIndexChanged(EventArgs e)
		{
			if (SelectedIndexChanged != null)
				SelectedIndexChanged(this, e);
		}
    }
}