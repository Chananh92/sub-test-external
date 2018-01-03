using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

using Framework.Compression;

using BetAndWin.Payments.Security;
using BetAndWin.Security;
using log4net;

namespace BetAndWin.PaymentService.Web.Controls.Maintenance
{
    /// <summary>
    ///		Summary description for CryptoTool.
    /// </summary>
    public partial class ZipHelper : ContentControl
    {
        private enum ConversionType
        {
            TextToText = 0x22,
            DataToText = 0x12,
            TextToData = 0x21,
            DataToData = 0x11
        }

        private static ILog _log = LogManager.GetLogger(typeof(ZipHelper));

        #region Private fields
        /// <summary>
        /// 
        /// </summary>
        private ICompressor _base64Compressor = CompressorFactory.GetCompressor(CompressorType.Base64);
        /// <summary>
        /// Private field contains compression level as integer value (0-store only, 1-minimum, 9-maximum).
        /// </summary>
        private int _compressionLevel = 9;
        /// <summary>
        /// Private field contains compressor.
        /// </summary>
        private ICompressor _compressor = null;
        /// <summary>
        /// Private field contains conversion type.
        /// </summary>
        private ConversionType _conversionType;
        /// <summary>
        /// Private field contains the input data.
        /// </summary>
        private byte[] _inputData = null;
        /// <summary>
        /// Private field contains the input text.
        /// </summary>
        private string _inputText = null;
        /// <summary>
        /// Private field contains the password used for packing or unpacking zipped data.
        /// </summary>
        private string _password = null;
        /// <summary>
        /// 
        /// </summary>
        private string _plainText = "Plain Text";
        /// <summary>
        /// Private field contains the output data.
        /// </summary>
        private byte[] _outputData = null;
        /// <summary>
        /// Private field contains the output text.
        /// </summary>
        private string _outputText = null;
        /// <summary>
        /// 
        /// </summary>
        private ZipCompressor _zipCompressor = null;
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.packButton.Click += new EventHandler(packButton_Click);
            this.unpackButton.Click += new EventHandler(unpackButton_Click);
            this.clearButton.Click += new EventHandler(clearButton_Click);
        }
        #endregion

        public override string Title
        {
            get
            {
                return "ZipHelper";
            }
        }

        #region Helper methods
        /// <summary>
        /// Method converts passed array of bytes to a string representation.
        /// </summary>
        /// <param name="data">The input array of bytes.</param>
        /// <returns>String representation of passed array of bytes.</returns>
        private string GetHexString(byte[] data)
        {
            StringBuilder hexDump = new StringBuilder();
            int index, count = data.Length;
            for (index = 0; index < count; index++)
            {
                if (index > 0)
                    hexDump.AppendFormat(" {0:X2}", data[index]);
                else
                    hexDump.AppendFormat("{0:X2}", data[index]);
            }
            return hexDump.ToString();
        }

        /// <summary>
        /// Method converts string representation of array of bytes to actual array of bytes.
        /// </summary>
        /// <param name="text">String representation of array of bytes.</param>
        /// <returns>Actual array of bytes of parsed string representation.</returns>
        private byte[] GetByteArray(string text)
        {
            text = text.ToUpper();
            if (text.StartsWith("0X"))
                text = text.Substring(2);

            text = Regex.Replace(text, "[^A-F0-9]+", string.Empty);
            string byteHex;
            int index, count = text.Length / 2;
            IList<byte> dataList = new List<byte>();
            for (index = 0; index < count; index++)
            {
                byteHex = text.Substring(index * 2, 2);
                dataList.Add(byte.Parse(byteHex, NumberStyles.HexNumber));
            }
            count = dataList.Count;
            byte[] data = new byte[count];
            for (index = 0; index < count; index++)
            {
                data[index] = dataList[index];
            }
            return data;
        }

        /// <summary>
        /// Method reads setting as set by the user and assigns them to proper private fields.
        /// </summary>
        private void ReadSettings()
        {
            string algorythmValue = algorythmDropDownList.Value;
            if (algorythmValue == _plainText)
            {
                _compressor = null;
            }
            else
            {
                CompressorType compressorType = (CompressorType)Enum.Parse(typeof(CompressorType), algorythmValue, true);
                _compressor = CompressorFactory.GetCompressor(compressorType);
                if (compressorType == CompressorType.Zip)
                {
                    _zipCompressor = (ZipCompressor)_compressor;
                }
            }
            _compressionLevel = int.Parse(compressionLevelDropDownList.Items[compressionLevelDropDownList.SelectedIndex].Value);
            _conversionType = (ConversionType)Enum.Parse(typeof(ConversionType), dataTypeDropDownList.Value, true);
            _inputText = inputArea.Value;
            _password = passwordTextBox.Value;
        }
        #endregion

        #region Compressing and decompressing
        /// <summary>
        /// 
        /// </summary>
        private void Compress()
        {
            PrepareInputData();
            if (_zipCompressor != null)
            {
                _outputData = _zipCompressor.CompressData(_inputData, _compressionLevel, _password);
            }
            else if (_compressor != null)
            {
                _outputData = _compressor.CompressData(_inputData);
            }
            else
            {
                _outputData = _inputData;
            }
            PrepareOutputData();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Decompress()
        {
            PrepareInputData();
            if (_zipCompressor != null)
            {
                _outputData = _zipCompressor.DecompressData(_inputData, _password);
            }
            else if (_compressor != null)
            {
                _outputData = _compressor.DecompressData(_inputData);
            }
            else
            {
                _outputData = _inputData;
            }
            PrepareOutputData();
        }
        #endregion

        #region Preparing data
        /// <summary>
        /// 
        /// </summary>
        private void PrepareInputData()
        {
            ReadSettings();
            switch (_conversionType)
            {
                case ConversionType.DataToData:
                case ConversionType.DataToText:
                    _inputData = GetByteArray(_inputText);
                    _inputText = null;
                    break;
                case ConversionType.TextToData:
                case ConversionType.TextToText:
                    _inputData = Encoding.UTF8.GetBytes(_inputText);
                    _inputText = null;
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PrepareOutputData()
        {
            switch (_conversionType)
            {
                case ConversionType.DataToData:
                case ConversionType.TextToData:
                    _outputText = GetHexString(_outputData);
                    _outputData = null;
                    break;
                case ConversionType.DataToText:
                case ConversionType.TextToText:
                    _outputText = Encoding.UTF8.GetString(_outputData);
                    _outputData = null;
                    break;
            }
            outputArea.Value = _outputText;
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Method is executed when the page loads.
        /// Used for checking security credentials of the user.
        /// </summary>
        /// <param name="sender">The object that sent the event.</param>
        /// <param name="e">The event arguments.</param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                SecurityContext.Demand(new UseZipHelperRight(), true);
                if (!Page.IsPostBack)
                {
                    ListItem listItem;
                    bool defaultItemSet = false;
                    algorythmDropDownList.Items.Add(_plainText);
                    foreach (CompressorType value in Enum.GetValues(typeof(CompressorType)))
                    {
                        if ((defaultItemSet) && (value == CompressorType.Default)) continue;
                        listItem = new ListItem(value.ToString());
                        if (value == CompressorType.Default)
                        {
                            listItem.Selected = (value == CompressorType.Default);
                            defaultItemSet = true;
                        }
                        algorythmDropDownList.Items.Add(listItem);
                    }
                    foreach (ConversionType value in Enum.GetValues(typeof(ConversionType)))
                    {
                        string itemText = value.ToString();
                        itemText = itemText.Substring(0, 4) + " " + itemText.Substring(4, 2) + " " + itemText.Substring(6);
                        listItem = new ListItem(itemText, value.ToString());
                        dataTypeDropDownList.Items.Add(listItem);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error("ZipHelper: error occured!", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Method is executed when a user clicks packButton to pack the input data
        /// base on user input (data, password, level).
        /// </summary>
        /// <param name="sender">The object that sent the event.</param>
        /// <param name="e">The event arguments.</param>
        private void packButton_Click(object sender, EventArgs e)
        {
            if (this.inputArea.InnerText == string.Empty)
                return;

            try
            {
                Compress();
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("ZipHelper: Error zipping data '{0}'", this.inputArea.InnerText), ex);
                this.outputArea.InnerText = ex.ToString();
            }
        }

        /// <summary>
        /// Method is executed when a user clicks unpackButton to unpack the input data
        /// base on user input (data, password).
        /// </summary>
        /// <param name="sender">The object that sent the event.</param>
        /// <param name="e">The event arguments.</param>
        private void unpackButton_Click(object sender, EventArgs e)
        {
            if (this.inputArea.InnerText == string.Empty)
                return;

            try
            {
                Decompress();
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("ZipHelper: error decrypting data '{0}'", this.inputArea.InnerText), ex);
                this.outputArea.InnerText = ex.ToString();
            }
        }

        /// <summary>
        /// Method is executed when user clicks clearButton to clear all input.
        /// </summary>
        /// <param name="sender">The object that sent the event.</param>
        /// <param name="e">The event arguments.</param>
        private void clearButton_Click(object sender, EventArgs e)
        {
            this.inputArea.InnerText = string.Empty;
            this.outputArea.InnerText = string.Empty;
            this.passwordTextBox.Value = string.Empty;
        }
        #endregion
    }
}
