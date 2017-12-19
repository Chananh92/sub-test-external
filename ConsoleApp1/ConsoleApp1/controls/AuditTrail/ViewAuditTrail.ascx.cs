using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BetAndWin.Payments.Security;
using BetAndWin.Security;
using CQRPayments.PaymentService.DomainModel.Auditing;
using CQRPayments.PaymentService.DomainModel.DataAccessInterfaces.Auditing;
using Framework.ObjectLocation;
using Framework.ScheduledTasks.DataAccess.Interfaces;
using System.Data;
using System.Linq;

namespace BetAndWin.PaymentService.Web.Controls.AuditTrail
{
	public partial class ViewAuditTrail : ContentControl
	{
		#region Private fields
		/// <summary>
		/// Field contains the number of records bound through the repeater.
		/// </summary>
		private int _recordCount = 0;

		private IScheduledTaskDA ScheduledTaskDA
		{
			get
			{
				return ObjectLocator.Get<IScheduledTaskDA>();
			}
		}
		#endregion

		#region Public property

		// Keys used for values retrived from Session. 
		/// <summary>
		/// The EntityID.
		/// </summary>
		public const string ENTITYID = "AuditTrailEntityID";
		/// <summary>
		/// The RowID.
		/// </summary>
		public const string ROWID = "AuditTrailRowID";

		/// <summary>
		/// Property returns the Title of the control / functionality.
		/// </summary>
		public override string Title
		{
			get
			{
				return "View Audit Trail";
			}
		}
		#endregion

		#region Helping methods
		/// <summary>
		/// Method fills existing ListControl with a list of DictionaryEntry items by removing all records first.
		/// </summary>
		/// <param name="listControl">The ListControl to fill.</param>
		/// <param name="collection">The collection of DictionaryEntry items.</param>
		protected void FillControl(ListControl listControl, IList<DictionaryEntry> collection)
		{
			if (listControl == null)
				return;

			listControl.Items.Clear();

			if (collection == null)
				return;
			string id;
			string text;
			listControl.Items.Add(new ListItem("Any", "0"));
			foreach (DictionaryEntry entry in collection)
			{
				id = entry.Key.ToString();
				text = entry.Value.ToString();
				listControl.Items.Add(new ListItem(text, id));
			}
		}

		/// <summary>
		/// Fills all controls with default values (read from database, etc.)
		/// </summary>
		protected void FillFilterControls()
		{
			FillControl(entityDropDownList, ObjectLocator.Get<IAuditTrailDA>().SelectEntities());
			FillControl(operationDropDownList, ObjectLocator.Get<IAuditTrailDA>().SelectOperationTypes());
		}

		/// <summary>
		/// Method returns int value if text can be parsed, defaultValue otherwise.
		/// </summary>
		/// <param name="text">The text to parse.</param>
		/// <param name="defaultValue">The default value to return if text can't be parsed.</param>
		/// <returns>Parsed int value or defaultValue.</returns>
		protected int GetInt(object text, int defaultValue)
		{
			if (text != null)
			{
				try
				{
					return int.Parse(text.ToString());
				}
				catch (FormatException)
				{
				}
			}
			return defaultValue;
		}

		/// <summary>
		/// Method returns long value if text can be parsed, defaultValue otherwise.
		/// </summary>
		/// <param name="text">The text to parse.</param>
		/// <param name="defaultValue">The default value to return if text can't be parsed.</param>
		/// <returns>Parsed int value or defaultValue.</returns>
		protected long GetLong(object text, out bool bindSource)
		{
			bindSource = true;
			if (!String.IsNullOrEmpty(text.ToString()))
			{
				var st = GetInt(entityDropDownList.SelectedValue, 0).Equals((int) AuditTrailEntityID.ScheduledTasks) &&
				         !String.IsNullOrEmpty(text.ToString())
					         ? ScheduledTaskDA.SelectByExternalID(text.ToString(), 1, false)
					         : null;
				long value;
				//select the ST internal id
				if (st != null)
					long.TryParse(st.ID.ToString(), out value);
				else
					long.TryParse(text.ToString(), out value);

				if (value <= 0)
					bindSource = false;

				return value;

			}
			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		protected string GetString(string text)
		{
			if ((text == null) || (text.Length == 0))
				return null;
			if ((text.IndexOf("%") == -1) && (text.IndexOf("_") == -1))
			{
				text = "%" + text + "%";
			}
			return text;
		}
		/// <summary>
		/// Select the audit trail records by parameters.
		/// </summary>
		/// <param name="countOfItems">The count of items.</param>
		/// <param name="auditTrailID">The audit trail ID.</param>
		/// <param name="fromCreatedOn">From created on.</param>
		/// <param name="toCreatedOn">To created on.</param>
		/// <param name="entityID">The entity ID.</param>
		/// <param name="rowID">The row ID.</param>
		/// <param name="operationType">Type of the operation.</param>
		/// <param name="reason">The reason.</param>
		/// <param name="applicationName">Name of the application.</param>
		/// <param name="hostName">Name of the host.</param>
		/// <param name="userName">Name of the user.</param>
		/// <param name="dbUserName">Name of the db user.</param>
		/// <param name="statePattern">The state pattern.</param>
		/// <returns></returns>
		protected IDataReader GetRecords(int countOfItems, long auditTrailID, DateTime fromCreatedOn, DateTime toCreatedOn, AuditTrailEntityID entityID,
			long rowID, AuditTrailOperationType operationType, string reason, string applicationName, string hostName, string userName, string dbUserName, string statePattern)
		{
			return ObjectLocator.Get<IAuditTrailDA>().SelectRecords(countOfItems, auditTrailID, fromCreatedOn, toCreatedOn, entityID,
													   rowID, operationType, reason, applicationName, hostName, userName,
													   dbUserName, statePattern, 0);
		}

		/// <summary>
		/// Method reads the data from the data adapter and binds filled ArrayList to the repeater,
		/// not using caching to retrieve actual data only.
		/// </summary>
		protected void SelectRecordsByFilter()
		{
			// Has to be checked here bacuse even if some users are allowed to see specific audit trail records
			// (e.g. bank account changes if user has ViewBankAccountAuditTrailRight), they shouldn't be able to 
			// select audit trail records themselves but can see only selection made by parameters from Session
			SecurityContext.Demand(new ViewAuditTrailRight(), true, false);
			bool useBinding;
			long rowID = GetLong(rowIDTextBox.Text, out useBinding);
			if (!useBinding)
			{
				BindDataSource(Enumerable.Empty<IDataRecord>());
				return;
			}

			using (IDataReader listOfRecords = GetRecords(
				GetInt(countOfItemsTextBox.Text, 100),
				0L,
				fromCreatedOnControl.DateTime,
				toCreatedOnControl.DateTime,
				(AuditTrailEntityID)GetInt(entityDropDownList.SelectedValue, 0),
				rowID,
				(AuditTrailOperationType)GetInt(operationDropDownList.SelectedValue, 0),
				GetString(reasonTextBox.Text),
				GetString(applicationNameTextBox.Text),
				GetString(computerNameTextBox.Text),
				GetString(identityTextBox.Text),
				GetString(dbUserNameTextBox.Text),
				GetString(statePatternTextBox.Text)))
			{
				BindDataSource((IEnumerable) listOfRecords);
			}
		}

		/// <summary>
		/// Method selects the audit trail records by values retreived from Session
		/// and binds it to repeater.
		/// </summary>
		protected void SelectRecordsBySessionValues()
		{
			int entityID = (int)Session[ENTITYID];
			long rowID = (long)Session[ROWID];

			using (IDataReader listOfRecords = GetRecords(
				100,
				0L,
				DateTime.MinValue,
				DateTime.MinValue,
				(AuditTrailEntityID)entityID,
				rowID,
				(AuditTrailOperationType)0,
				null, null, null, null, null, null))
			{
				BindDataSource((IEnumerable) listOfRecords);
			}
		}

		/// <summary>
		/// Binds the data source to the repeater.
		/// </summary>
		/// <param name="dr"></param>
		protected void BindDataSource(IEnumerable dr)
		{
			viewAuditTrailRepeater.DataSource = dr;
			viewAuditTrailRepeater.DataBind();
		}

		#endregion

		#region Event handlers
		/// <summary>
		/// Delegated event handler method is executed when the page is loaded through the PageLoad event.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Page.IsPostBack)
				return;

			string filter = Request.QueryString["filter"];
			bool selectByFilter = true; // by default filter is used
			if (!String.IsNullOrEmpty(filter))
			{
				if (!bool.TryParse(filter, out selectByFilter))
					selectByFilter = true;
			}

			if (selectByFilter)
			{
				FillFilterControls();
			}
			else
			{
				// Select audit trail records by values retrieved from Session.
				// This is used to see changes for specific entity (e.g. specific user bank account,...)
				SelectRecordsBySessionValues();
			}

			auditTrailFilter.Visible = selectByFilter;
		}

		/// <summary>
		/// Delegated event handler method is executed when the page is just about to be rendered.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			try
			{
				Label resultsLabel = (Label)viewAuditTrailRepeater.Controls[0].Controls[0].FindControl("resultsLabel");
				if (resultsLabel != null)
					resultsLabel.Text = string.Format("AuditTrailRecords {0} items", _recordCount);
			}
			catch (ArgumentOutOfRangeException)
			{
			}
		}

		/// <summary>
		/// Delegated event handler method is executed when the item is being created to set the
		/// css class of the row for alternating purposes.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void viewAuditTrailRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
					if (e.Item.ItemIndex % 2 == 0)
					{
						HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("paymentMethodRow");
						row.Attributes["class"] = "alternate";
					}
					break;
			}
		}

		/// <summary>
		/// Delegated event handler method is executed when the item is being bouns through the data repeater
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void viewAuditTrailRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.AlternatingItem:
				case ListItemType.Item:
					_recordCount++;
					break;
			}
		}

		/// <summary>
		/// Delegated event handler method is executed when view button is clicked
		/// to search for and display the appropriate data.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void viewButton_Click(object sender, EventArgs e)
		{
			Page.Validate();
			if (Page.IsValid)
				SelectRecordsByFilter();
		}

		/// <summary>
		/// Delegated event handler method is used for determining if the
		/// searching by StatePattern is eligible as it takes a lot of time.
		/// </summary>
		/// <param name="source">The object that sent the event.</param>
		/// <param name="args">The event arguments.</param>
		protected void statePatternCV_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid =
				(entityDropDownList.SelectedValue != "0") ||
				((fromCreatedOnControl.DateTime > DateTime.MinValue) && (toCreatedOnControl.DateTime > DateTime.MinValue)) ||
				(rowIDTextBox.Text.Length > 0) ||
				(reasonTextBox.Text.Length > 0) ||
				(identityTextBox.Text.Length > 0);
		}
		#endregion
	}
}
