using System;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Framework.Caching;
using log4net;

using BetAndWin.Security;
using BetAndWin.Payments.Security;

namespace BetAndWin.PaymentService.Web.Controls.Maintenance
{
    public partial class Caches : ContentControl
    {
        #region Private fields
        /// <summary>
        /// 
        /// </summary>
        private string _reason;
        /// <summary>
        /// 
        /// </summary>
        private static ILog _log = LogManager.GetLogger(typeof(Caches));
        #endregion

        /// <summary>
        /// Delegated event handler method is executed when the page is loaded through the PageLoad event.
        /// </summary>
        /// <param name="sender">The object that sent the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SecurityContext.Demand(new UseCacheViewerRight(), true);
        }

        /// <summary>
        /// Delegated event handler method is executed when the page is about to be displayed
        /// to set control values to show the user appropriate data.
        /// </summary>
        /// <param name="sender">The object that sent the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ReadData();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void ReadData()
        {
            ArrayList allCaches = new ArrayList();
            allCaches.AddRange(CacheFactory.GetCaches(CacheScope.AppDomain));
            allCaches.AddRange(CacheFactory.GetCaches(CacheScope.Thread));
            allCaches.Sort(new CacheComparer());
            cachesRepeater.DataSource = allCaches;
            cachesRepeater.DataBind();
        }

        /// <summary>
        /// Delegated event handler method is executed when the item is being created to set the
        /// css class of the row for alternating purposes.
        /// </summary>
        /// <param name="sender">The object that sent the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void cachesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                    if (e.Item.ItemIndex % 2 == 0)
                    {
                        HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("cacheRow");
                        row.Attributes["class"] = "alternate";
                    }
                    break;
            }
        }

        /// <summary>
        /// Delegated event handler method is executed when text of reasonTextBox in the footer
        /// of the repeater is changed. It sets the private field and sets the length
        /// to the maximum available length.
        /// </summary>
        /// <param name="sender">The object that sent the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void reasonTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox reasonTextBox = (TextBox)sender;
            _reason = reasonTextBox.Text;
            if (_reason.Length > reasonTextBox.MaxLength)
                _reason = _reason.Substring(0, reasonTextBox.MaxLength);
        }

        /// <summary>
        /// Delegated event handler method is executed when expire button is clicked to expire caches.
        /// </summary>
        /// <param name="sender">The object that sent the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void expireButton_Click(object sender, EventArgs e)
        {
            UpdateReason();
            UpdateCaches();
        }

        /// <summary>
        /// Method updates the Reason thread property if control found.
        /// </summary>
        protected void UpdateReason()
        {
        }

        /// <summary>
        /// Method expires selected caches.
        /// </summary>
        protected void UpdateCaches()
        {
            try
            {
                CheckBox expireCheckBox;
                Label cacheName;
                foreach (RepeaterItem recordItem in cachesRepeater.Items)
                {
                    expireCheckBox = (recordItem.FindControl("expireCheckBox") as CheckBox);
                    if ((expireCheckBox != null) && (expireCheckBox.Checked))
                    {
                        cacheName = (recordItem.FindControl("cacheName") as Label);
                        if (cacheName != null)
                        {

                            CacheFactory.Get(cacheName.Text).Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Caches: Error exipring caches {0}", ex));
            }
        }

        public override string Title
        {
            get
            {
                return "Caches";
            }
        }
    }
}