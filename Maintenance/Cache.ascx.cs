using System;
using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Framework.Caching;
using log4net;

using BetAndWin.Security;
using BetAndWin.Payments.Security;
using Microsoft.Security.Application;

namespace BetAndWin.PaymentService.Web.Controls.Maintenance
{
	public partial class CacheControl : ContentControl
	{
		#region Private fields
		/// <summary>
		/// Private field contains name of cache to select.
		/// </summary>
		private string _cacheName;
		/// <summary>
		/// Private field contains item key name of cache's Hashtable _data to show.
		/// </summary>
		private string _keyName;
		/// <summary>
		/// Private field contains switch if item key name was entered manually and _keyName from QueryString should be ignored.
		/// </summary>
		private bool _keyNameEntered;
		/// <summary>
		/// Private field contains logger for this CacheControl.
		/// </summary>
		private static ILog _log = LogManager.GetLogger(typeof(CacheControl));
		/// <summary>
		/// Private field contains name of cache scope to select.
		/// </summary>
		private string _scopeName;
		/// <summary>
		/// Private field contains selected cache based on _scopeName and _cacheName.
		/// </summary>
		private ICache _selectedCache = null;
		/// <summary>
		/// Private constant contains text for text if can't be set.
		/// </summary>
		private const string _unknown = "(unknown)";
		#endregion

		#region Event handlers
		/// <summary>
		/// Delegated event handler method is executed when the page is loaded through the PageLoad event
		/// to collect input data as set in query string and find appropriate cache item.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				SecurityContext.Demand(new UseCacheViewerRight(), true);
				if (!Page.IsPostBack)
				{
					scopeDropDownList.Items.Add(new ListItem("AppDomain"));
					scopeDropDownList.Items.Add(new ListItem("Thread"));
				}

				_cacheName = Request.QueryString["name"];
				_keyName = Request.QueryString["ItemKey"];
				_scopeName = Request.QueryString["scope"];
				if ((_scopeName != null) && (_cacheName != null))
				{
					IList scopeCaches = CacheFactory.GetCaches(CacheScope.AppDomain);
					foreach (ICache scopeCache in scopeCaches)
					{
						if (scopeCache.Name.Equals(_cacheName))
						{
							_selectedCache = scopeCache;
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				_log.Error(string.Format("CacheControl: Error getting cache {0} of scope {1}: {2}",
					_cacheName, _scopeName, ex));
				throw ex;
			}
		}

		/// <summary>
		/// Delegated event handler method is executed when the page is about to be displayed
		/// to set control values to show the user appropriate data.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			try
			{
				if (_selectedCache != null)
				{
					nameLiteral.Text = _selectedCache.Name;
					itemCountLiteral.Text = _selectedCache.ItemCount.ToString();
					scopeDropDownList.SelectedValue = _selectedCache.Scope.ToString();
					if ((_keyName != null) && !_keyNameEntered)
					{
						itemKeyInput.Text = _keyName;
						getItemButton_Click(this, null);
					}

					cacheKeysRepeater.DataSource = GetKeys();
					cacheKeysRepeater.DataBind();
				}
			}
			catch (Exception ex)
			{
				_log.Error(string.Format("CacheControl: Error pre-rendering page: {0}",
					ex));
				throw ex;
			}
		}

		/// <summary>
		/// Delegated event handler method is executed when Add Item button is clicked
		/// to add a new item to the cache.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void addItemButton_Click(object sender, EventArgs e)
		{
			string key = _unknown;
			string value = _unknown;
			try
			{
				key = itemKeyInput.Text;
				value = itemValueInput.Text;
				if (_selectedCache != null)
				{
					_selectedCache.Add(key, value);
				}
			}
			catch (Exception ex)
			{
				_log.Error(string.Format("CacheControl: Error adding item by key {0} and value {1} to cache {2}: {3}",
					key, value, _cacheName, ex));
				throw ex;
			}
		}

		protected string GetItemValue(object itemValue)
		{
			if (itemValue is IDictionary)
			{
				IDictionary dictionaryValue = (IDictionary)itemValue;
				ICollection keyList = dictionaryValue.Keys;
				StringBuilder stringValue = new StringBuilder();
				stringValue.Append(dictionaryValue.ToString());
				stringValue.Append("\n{");
				string comma = string.Empty;
				if (dictionaryValue.Count > 0)
				{
					stringValue.AppendFormat("Count: {0}; Pairs:\n", dictionaryValue.Count);
					foreach (object key in keyList)
					{
						stringValue.AppendFormat("{0}{1}={2}",
							comma,
							key,
							GetItemValue(dictionaryValue[key]));
						comma = ",\n";
					}
				}
				stringValue.Append("}");
				return stringValue.ToString();
			}
			if (itemValue is IList)
			{
				IList listValue = (IList)itemValue;
				StringBuilder stringValue = new StringBuilder();
				stringValue.Append(listValue.ToString());
				stringValue.Append(" {");
				if (listValue.Count > 0)
				{
					stringValue.AppendFormat("Count: {0}; Items: ", listValue.Count);
					ArrayList sortedList = new ArrayList(listValue);
					//                    sortedList.Sort();
					bool comma = false;
					foreach (object item in sortedList)
					{
						if (comma)
							stringValue.Append(", ");
						else
							comma = true;
						stringValue.Append(item);
					}
				}
				stringValue.Append("}");
				return stringValue.ToString();
			}
			return itemValue.ToString();
		}

		/// <summary>
		/// Delegated event handler method is executed when Get Item button is clicked
		/// to get contents of specific item in cache.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void getItemButton_Click(object sender, EventArgs e)
		{
			string key = _unknown;
			try
			{
				key = itemKeyInput.Text;
				itemValueInput.Text = string.Empty;
				_keyNameEntered = true;
				if (_selectedCache != null)
				{
					object itemValue = _selectedCache.Get(key);
					if (itemValue != null)
						itemValueInput.Text = GetItemValue(itemValue);
				}
			}
			catch (Exception ex)
			{
				_log.Error(string.Format("CacheControl: Error getting item by key {0} from cache {1}: {2}",
					key, _cacheName, ex));
				throw ex;
			}
		}

		/// <summary>
		/// Delegated event handler method is executed when Remove Item button is clicked
		/// to remove item and its contents from cache.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void removeItemButton_Click(object sender, EventArgs e)
		{
			string key = _unknown;
			try
			{
				key = itemKeyInput.Text;
				if (_selectedCache != null)
				{
					_selectedCache.Remove(key);
				}
			}
			catch (Exception ex)
			{
				_log.Error(string.Format("CacheControl: Error removing key {0} from cache {1}: {2}",
					key, _cacheName, ex));
				throw ex;
			}
		}

		/// <summary>
		/// Delegated event handler method is executed when Expire Whole Cache button is clicked
		/// to remove all contents from cache.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void expireCacheButton_Click(object sender, EventArgs e)
		{
			try
			{
				if (_selectedCache != null)
				{
					_selectedCache.Clear();
				}
			}
			catch (Exception ex)
			{
				_log.Error(string.Format("CacheControl: Error exipring cache {0}: {1}", _cacheName, ex));
				throw ex;
			}
		}

		/// <summary>
		/// Delegated event handler method is executed when the item is being created to set the
		/// item link to contain existing Url and cache item key.
		/// </summary>
		/// <param name="sender">The object that sent the event.</param>
		/// <param name="e">The event arguments.</param>
		protected void cacheKeysRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
		{
			try
			{
				HtmlAnchor anchor = (HtmlAnchor)e.Item.FindControl("cacheKeyLink");
				if (anchor != null)
				{
					string url = Request.Url.PathAndQuery;
					int itemKeyAt = url.IndexOf("&ItemKey=");
					if (itemKeyAt != -1)
						url = url.Substring(0, itemKeyAt);

					anchor.HRef = string.Concat(url, "&ItemKey=", Microsoft.Security.Application.Encoder.UrlEncode(e.Item.DataItem.ToString()));
				}
			}
			catch (Exception ex)
			{
				_log.Error(string.Format("CacheControl: Error creating item's Url of cacheKeysRepeater: {0}", ex));
				throw ex;
			}
		}

		/// <summary>
		/// Method gets and returns collection of Hashtable keys of selected cache through Reflection.
		/// </summary>
		/// <returns>Collection of Hashtable keys of selected cache.</returns>
		protected ICollection GetKeys()
		{
			if (_selectedCache == null)
				return null;

			ArrayList sortedKeys = new ArrayList(_selectedCache.Keys);
			sortedKeys.Sort();

			return sortedKeys;
		}
		#endregion

		public override string Title
		{
			get
			{
				return "CACHE";
			}
		}
	}
}