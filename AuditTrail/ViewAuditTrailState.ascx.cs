using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CQRPayments.PaymentService.DomainModel.DataAccessInterfaces.Auditing;
using Framework.Compression;
using Framework.ObjectLocation;
using Framework.Web.UI.Helpers;

namespace BetAndWin.PaymentService.Web.Controls.AuditTrail
{
	public partial class ViewAuditTrailState : ContentControl
	{
		#region Private fields
		/// <summary>
		/// 
		/// </summary>
		private CQRPayments.PaymentService.DomainModel.Auditing.AuditTrail _record;
		/// <summary>
		/// 
		/// </summary>
		private long _recordID;
		#endregion

		#region Public property
		/// <summary>
		/// Property returns the Title of the control / functionality.
		/// </summary>
		public override string Title
		{
			get
			{
				return "View Audit Trail Record";
			}
		}
		#endregion

		#region Helping methods
		/// <summary>
		/// 
		/// </summary>
		/// <param name="oldState"></param>
		/// <param name="newState"></param>
		/// <returns></returns>
		protected int CompareText(ref string oldState, ref string newState)
		{
			if ((oldState == null) || (oldState.Length == 0))
				return 1;

			if ((newState == null) || (newState.Length == 0))
				return 1;

			int differences = 0;
			int newIndex = 0;
			int newLength = newState.Length;
			int oldIndex = 0;
			int oldLength = oldState.Length;
			string openText = @"<span class=""text-difference"">*";
			string closeText = @"</span>";
			string skipToText = "&quot; ";
			int newSkip = 0;
			int oldSkip = 0;
			while (true)
			{
				if (newIndex >= newLength)
					break;

				if (oldIndex >= oldLength)
					break;

				if (oldState[oldIndex] != newState[newIndex])
				{
					differences++;
					oldSkip = oldState.IndexOf(skipToText, oldIndex);
					newSkip = newState.IndexOf(skipToText, newIndex);
					if (oldSkip < 0)
					{
						oldIndex = oldLength;
					}
					else
					{
						if (newSkip < 0)
						{
							oldState = oldState.Substring(0, oldIndex) + openText + oldState.Substring(oldIndex) + closeText;
						}
						else
						{
							oldState = oldState.Substring(0, oldIndex) + openText + oldState.Substring(oldIndex);
							oldIndex = oldSkip + openText.Length;
							oldState = oldState.Substring(0, oldIndex) + closeText + oldState.Substring(oldIndex);
							oldLength = oldState.Length;
						}
					}
					if (newSkip < 0)
					{
						newIndex = newLength;
					}
					else
					{
						if (oldSkip < 0)
						{
							newState = newState.Substring(0, newIndex) + openText + newState.Substring(newIndex) + closeText;
						}
						else
						{
							newState = newState.Substring(0, newIndex) + openText + newState.Substring(newIndex);
							newIndex = newSkip + openText.Length;
							newState = newState.Substring(0, newIndex) + closeText + newState.Substring(newIndex);
							newLength = newState.Length;
						}
					}
				}

				newIndex++;
				oldIndex++;
			}
			return differences;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		protected string Decompress(string text)
		{
			if ((text != null) && (text.IndexOf("Compressed") >= 0))
			{
				string open = @"Definition=""";
				int openAt = text.IndexOf(open);
				if (openAt >= 0)
				{
					openAt += open.Length;
					string close = @"""";
					int closeAt = text.IndexOf(close, openAt);
					if (closeAt >= 0)
					{
						text = text.Substring(openAt, closeAt - openAt);
						text = CompressorFactory.GetCompressor(CompressorType.GZip).DecompressText(text);
					}
				}
				text = HttpUtility.HtmlEncode(text).Replace("\t", "&nbsp;").Replace("\r\n", "<br/>");
				return text;
			}
			return null;
		}

		protected bool FillDecompressed(string stateText, HtmlTableRow decompressedTableRow, Literal decompressedLiteral)
		{
			if ((stateText == null) || (stateText.Length == 0))
				return false;

			decompressedTableRow.Visible = true;
			decompressedLiteral.Text = stateText;
			return true;
		}

		/// <summary>
		/// Fills all controls with default values (read from database, etc.)
		/// </summary>
		protected void Fill()
		{
			this.applicationNameLiteral.Text = _record.ApplicationName;
			this.auditTrailIDLiteral.Text = _record.AuditTrailID.ToString();
			this.createdOnLiteral.Text = DateTimeHelper.FormatAsLocal(_record.CreatedOn);//.ToString("G");
			this.dbUserNameLiteral.Text = _record.DbUserName;
			this.entityNameLiteral.Text = _record.EntityName;
			this.hostNameLiteral.Text = _record.HostName;

			if (_record.Identity != null)
				identityNameLiteral.Text = _record.Identity.Name;

			this.operationNameLiteral.Text = _record.OperationTypeName;
			this.reasonLiteral.Text = _record.Reason;
			this.rowIDLiteral.Text = _record.RowID.ToString();

			if (!String.IsNullOrEmpty(_record.NewState) || !String.IsNullOrEmpty(_record.OldState))
			{
				// New Design
				SetLabels(_record.NewState, _record.OldState);
			}
			else
			{
				// XML
				SetLabels(_record.NewStateXml, _record.OldStateXml);
			}
		}

		#region SetLabels

		private void SetLabels(string newState, string oldState)
		{
			bool decompressed = false;

			string newStateDecompressed = Decompress(newState);
			string oldStateDecompressed = Decompress(oldState);

			CompareText(ref newStateDecompressed, ref oldStateDecompressed);

			decompressed = decompressed || FillDecompressed(newStateDecompressed, this.newStateDecompressedRow, this.newStateDecompressedLiteral);
			decompressed = decompressed || FillDecompressed(oldStateDecompressed, this.oldStateDecompressedRow, this.oldStateDecompressedLiteral);

			if (decompressed)
			{
				stateDecompressedRow.Visible = true;
			}
			else
			{
				string newStateHtmlEncoded = HttpUtility.HtmlEncode(HttpUtility.HtmlDecode(newState));
				string oldStateHtmlEncoded = HttpUtility.HtmlEncode(HttpUtility.HtmlDecode(oldState));

				CompareText(ref newStateHtmlEncoded, ref oldStateHtmlEncoded);

				this.newStateLiteral.Text = newStateHtmlEncoded;
				this.oldStateLiteral.Text = oldStateHtmlEncoded;
			}
		}

		#endregion

		/// <summary>
		/// Method reads the data from the data adapter and binds filled ArrayList to the repeater,
		/// not using caching to retrieve actual data only.
		/// </summary>
		protected void ReadData()
		{
			_record = ObjectLocator.Get<IAuditTrailDA>().SelectRecordByID(_recordID);
		}
		#endregion

		protected override void OnPreRender(EventArgs e)
		{
			string recordID = (string)Request.QueryString["id"];
			if (recordID != null)
			{
				_recordID = long.Parse(recordID);
				ReadData();
				Fill();
			}
		}
		#region Event handlers
		/// <summary>
		/// Delegated event handler method is executed when the page is loaded through the PageLoad event.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			//if (Page.IsPostBack)
			//    return;


		}
		#endregion
	}
}
