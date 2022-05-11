using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PharmaSYSRetailPlus.Common;
using System.Xml.Linq;
using System.Drawing;

namespace PharmaSYSRetailPlus.Printing
{
    public enum ColumnDataType
    {
        None = 0,
        Text = 1,
        Integer = 2,
        Decimal = 3,
    }

    public enum PrintSettingsType
    {
        None = 0,
        SaleBillPrintSettingsPlainPaper = 1,
        SaleBillPrintSettingsPrePrintedPaper = 2,
        DebiNotePrintSettingsPlainPaper = 3,
        DebitNotePrintSettingsPrePrintedPaper = 4,
    }

    public class PrintSettings
    {
        private const string SETTINGFILENAME = "PrintSettings.xml";
        private SaleBillSettings _SaleBillPrintSettingsPlainPaper;
        public SaleBillSettings SaleBillPrintSettingsPlainPaper
        {
            get { return _SaleBillPrintSettingsPlainPaper; }
        }

        private SaleBillSettings _SaleBillPrintSettingsPrePrintedPaper;
        public SaleBillSettings SaleBillPrintSettingsPrePrintedPaper
        {
            get { return _SaleBillPrintSettingsPrePrintedPaper; }
        }
        private SaleBillSettings _DebitNotePrintSettingsPlainPaper;
        public SaleBillSettings DebitNotePrintSettingsPlainPaper 
        {
            get { return _DebitNotePrintSettingsPlainPaper; }
        }
        private SaleBillSettings _DebitNotePrintSettingsPrePrintedPaper;
        public SaleBillSettings DebitNotePrintSettingsPrePrintedPaper
        {
            get { return _DebitNotePrintSettingsPrePrintedPaper; }
        }
        public PrintSettings()
        {
            _SaleBillPrintSettingsPlainPaper = new SaleBillSettings(PrintSettingsType.SaleBillPrintSettingsPlainPaper);
            _SaleBillPrintSettingsPrePrintedPaper = new SaleBillSettings(PrintSettingsType.SaleBillPrintSettingsPrePrintedPaper);
            _DebitNotePrintSettingsPlainPaper = new SaleBillSettings(PrintSettingsType.DebiNotePrintSettingsPlainPaper);
            _DebitNotePrintSettingsPrePrintedPaper = new SaleBillSettings(PrintSettingsType.DebitNotePrintSettingsPrePrintedPaper);
            ReadPrintSettings();
        }

        private void ReadPrintSettings()
        {
            try
            {
                string file = AppDomain.CurrentDomain.BaseDirectory.ToString() + SETTINGFILENAME;
                XDocument doc = XDocument.Load(file);
                _SaleBillPrintSettingsPlainPaper.ReadSettings(doc);
                _SaleBillPrintSettingsPrePrintedPaper.ReadSettings(doc);
                _DebitNotePrintSettingsPlainPaper.ReadSettings(doc);
                _DebitNotePrintSettingsPrePrintedPaper.ReadSettings(doc);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public static string GetRootElementName(PrintSettingsType settingsType)
        {
            string retValue = string.Empty;
            switch (settingsType)
            {
                case PrintSettingsType.None:
                    retValue = "None";
                    break;
                case PrintSettingsType.SaleBillPrintSettingsPlainPaper:
                    retValue = "SaleBillPrintSettingsPlainPaper";
                    break;
                case PrintSettingsType.SaleBillPrintSettingsPrePrintedPaper:
                    retValue = "SaleBillPrintSettingsPrePrintedPaper";
                    break;
                case PrintSettingsType.DebiNotePrintSettingsPlainPaper:
                    retValue = "DebitNotePrintSettingsPlainPaper";
                    break;
                case PrintSettingsType.DebitNotePrintSettingsPrePrintedPaper:
                    retValue = "DebitNotePrintSettingsPrePrintedPaper";
                    break;

            }
            return retValue;
        }
    }

    public class SaleBillSettings
    {
        private PrintSettingsType _PrintSettingsType = PrintSettingsType.None;
        private GeneralSettings _GeneralSettings;
        private PageHeader _PageHeader;
        private PageContent _PageContent;
        private PageFooter _PageFooter;

        public GeneralSettings GeneralSettings
        {
            get { return _GeneralSettings; }
        }

        public PageHeader PageHeader
        {
            get { return _PageHeader; }
        }

        public PageContent PageContent
        {
            get { return _PageContent; }
        }

        public PageFooter PageFooter
        {
            get { return _PageFooter; }
        }

        public SaleBillSettings(PrintSettingsType printSettingsType)
        {
            _PrintSettingsType = printSettingsType;
            _GeneralSettings = new GeneralSettings(_PrintSettingsType);
            _PageHeader = new PageHeader(_PrintSettingsType);
            _PageContent = new PageContent(_PrintSettingsType);
            _PageFooter = new PageFooter(_PrintSettingsType);
        }

        public void ReadSettings(XDocument doc)
        {
            try
            {
                _GeneralSettings.ReadSettings(doc);
                _PageHeader.ReadSettings(doc);
                _PageContent.ReadSettings(doc);
                _PageFooter.ReadSettings(doc);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }

    public class GeneralSettings
    {
        private PrintSettingsType _PrintSettingsType = PrintSettingsType.None;
        private int _PageWidth;
        private int _PageHeight;
        private double _ReverseLineFeed;
        private double _LineFeed;
        private int _ContentStartRow;      

        public GeneralSettings(PrintSettingsType printSettingsType)
        {
            _PrintSettingsType = printSettingsType;
        }

        public int PageWidth
        {
            get { return _PageWidth; }
            set { _PageWidth = value; }
        }
        public int PageHeight
        {
            get { return _PageHeight; }
            set { _PageHeight = value; }
        }

        public double ReverseLineFeed
        {
            get { return _ReverseLineFeed; }
            set { _ReverseLineFeed = value; }
        }
        public double LineFeed
        {
            get { return _LineFeed; }
            set { _LineFeed = value; }
        }
        public int ContentStartRow
        {
            get { return _ContentStartRow; }
            set { _ContentStartRow = value; }
        }       

        public void ReadSettings(XDocument doc)
        {
            try
            {
                string rootElement = PrintSettings.GetRootElementName(_PrintSettingsType);
                foreach (XElement element in doc.Descendants(rootElement).Descendants("GeneralSettings"))
                {
                    XElement item = element.Element("PageWidth");
                    int.TryParse(item.Value, out _PageWidth);

                    item = element.Element("PageHeight");
                    int.TryParse(item.Value, out _PageHeight);

                    item = element.Element("LineFeed");
                    double.TryParse(item.Value, out _LineFeed);

                    item = element.Element("ReverseLineFeed");
                    double.TryParse(item.Value, out _ReverseLineFeed);

                    item = element.Element("ContentStartRow");
                    int.TryParse(item.Value, out _ContentStartRow);

                   
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }
    public class PageHeader
    {
        private PrintSettingsType _PrintSettingsType = PrintSettingsType.None;

        private PageItem _ShopName;
        public PageItem ShopName
        {
            get { return _ShopName; }
            set { _ShopName = value; }
        }
        private PageItem _ShopAddress1;
        public PageItem ShopAddress1
        {
            get { return _ShopAddress1; }
            set { _ShopAddress1 = value; }
        }

        private PageItem _ShopAddress2;
        public PageItem ShopAddress2
        {
            get { return _ShopAddress2; }
            set { _ShopAddress2 = value; }
        }

        private PageItem _ShopTelephone;
        public PageItem ShopTelephone
        {
            get { return _ShopTelephone; }
            set { _ShopTelephone = value; }
        }

        private PageItem _PatientName;
        public PageItem PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }

        private PageItem _PatientAddress;
        public PageItem PatientAddress
        {
            get { return _PatientAddress; }
            set { _PatientAddress = value; }
        }

        private PageItem _PatientTelephone;
        public PageItem PatientTelephone
        {
            get { return _PatientTelephone; }
            set { _PatientTelephone = value; }
        }

        private PageItem _DoctorName;
        public PageItem DoctorName
        {
            get { return _DoctorName; }
            set { _DoctorName = value; }
        }

        private PageItem _DoctorAddress;
        public PageItem DoctorAddress
        {
            get { return _DoctorAddress; }
            set { _DoctorAddress = value; }
        }

        private PageItem _Time;
        public PageItem Time
        {
            get { return _Time; }
            set { _Time = value; }
        }

        private PageItem _VoucherTypeSCA;
        public PageItem VoucherTypeSCA
        {
            get { return _VoucherTypeSCA; }
            set { _VoucherTypeSCA = value; }
        }

        private PageItem _VoucherTypeSCR;
        public PageItem VoucherTypeSCR
        {
            get { return _VoucherTypeSCR; }
            set { _VoucherTypeSCR = value; }
        }

        private PageItem _VoucherTypeSCS;
        public PageItem VoucherTypeSCS
        {
            get { return _VoucherTypeSCS; }
            set { _VoucherTypeSCS = value; }
        }

        private PageItem _VoucherTypeSVU;
        public PageItem VoucherTypeSVU
        {
            get { return _VoucherTypeSVU; }
            set { _VoucherTypeSVU = value; }
        }

        private PageItem _VoucherTypeCreditNote;
        public PageItem VoucherTypeCreditNote
        {
            get { return _VoucherTypeCreditNote; }
            set { _VoucherTypeCreditNote = value; }
        }

        private PageItem _VoucherTypeDebitNote;
        public PageItem VoucherTypeDebitNote
        {
            get { return _VoucherTypeDebitNote; }
            set { _VoucherTypeDebitNote = value; }
        }

        private PageItem _Date;
        public PageItem Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        private PageItem _PageNo;
        public PageItem PageNo
        {
            get { return _PageNo; }
            set { _PageNo = value; }
        }

        public PageHeader(PrintSettingsType printSettingsType)
        {
            _PrintSettingsType = printSettingsType;
            _ShopName = new PageItem();
            _ShopAddress1 = new PageItem();
            _ShopAddress2 = new PageItem();
            _ShopTelephone = new PageItem();
            _PatientName = new PageItem();
            _PatientAddress = new PageItem();
            _PatientTelephone = new PageItem();
            _DoctorName = new PageItem();
            _DoctorAddress = new PageItem();
            _Time = new PageItem();
            _VoucherTypeSCA = new PageItem();
            _VoucherTypeSCR = new PageItem();
            _VoucherTypeSCS = new PageItem();
            _VoucherTypeSVU = new PageItem();
            _VoucherTypeCreditNote = new PageItem();
            _VoucherTypeDebitNote = new PageItem();
            _Date = new PageItem();
            _PageNo = new PageItem();
        }

        public void ReadSettings(XDocument doc)
        {
            try
            {
                string rootElement = PrintSettings.GetRootElementName(_PrintSettingsType);
                foreach (XElement element in doc.Descendants(rootElement).Descendants("PageHeader"))
                {
                    XElement item = element.Element("ShopNameItem");
                    _ShopName.ReadSettings(item);

                    item = element.Element("ShopAddress1Item");
                    _ShopAddress1.ReadSettings(item);

                    item = element.Element("ShopAddress2Item");
                    _ShopAddress2.ReadSettings(item);

                    item = element.Element("ShopTelephoneItem");
                    _ShopTelephone.ReadSettings(item);

                    item = element.Element("PatientName");
                    _PatientName.ReadSettings(item);

                    item = element.Element("PatientAddress");
                    _PatientAddress.ReadSettings(item);

                    item = element.Element("PatientTelephone");
                    _PatientTelephone.ReadSettings(item);

                    item = element.Element("DoctorName");
                    _DoctorName.ReadSettings(item);

                    item = element.Element("DoctorAddress");
                    _DoctorAddress.ReadSettings(item);

                    item = element.Element("TimeItem");
                    _Time.ReadSettings(item);

                    item = element.Element("VoucherTypeSCAItem");
                    _VoucherTypeSCA.ReadSettings(item);

                    item = element.Element("VoucherTypeSCRItem");
                    _VoucherTypeSCR.ReadSettings(item);

                    item = element.Element("VoucherTypeSCSItem");
                    _VoucherTypeSCS.ReadSettings(item);

                    item = element.Element("VoucherTypeSVUItem");
                    _VoucherTypeSVU.ReadSettings(item);

                    item = element.Element("VoucherTypeCreditNoteItem");
                    _VoucherTypeCreditNote.ReadSettings(item);

                    item = element.Element("VoucherTypeDebitNoteItem");
                    _VoucherTypeDebitNote.ReadSettings(item);

                    item = element.Element("DateItem");
                    _Date.ReadSettings(item);

                    item = element.Element("PageNoItem");
                    _PageNo.ReadSettings(item);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

       
    }

    
    public class PageContent
    {
        private PrintSettingsType _PrintSettingsType = PrintSettingsType.None;
        private int _ColumnCount;
        public int ColumnCount
        {
            get { return _ColumnCount; }
            set { _ColumnCount = value; }
        }

        public List<PageColumn> Columns;

        public PageContent(PrintSettingsType printSettingsType)
        {
            _PrintSettingsType = printSettingsType;
            Columns = new List<PageColumn>();
        }
        public void ReadSettings(XDocument doc)
        {
            try
            {
                string rootElement = PrintSettings.GetRootElementName(_PrintSettingsType);
                foreach (XElement element in doc.Descendants(rootElement).Descendants("PageContent"))
                {
                    XElement item = element.Element("ColumnCount");
                    int.TryParse(item.Value, out _ColumnCount);
                    for (int i = 1; i <= _ColumnCount; i++)
                    {
                        string elementName = "Column" + i.ToString();
                        foreach (XElement column in element.Descendants(elementName))
                        {
                            PageColumn pColumn = new PageColumn();
                            pColumn.ReadSettings(column);
                            Columns.Add(pColumn);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }
    
    public class PageFooter
    {
        private PrintSettingsType _PrintSettingsType = PrintSettingsType.None;
        private PageItem _DiscountItem;
        public PageItem DiscountItem
        {
            get { return _DiscountItem; }
            set { _DiscountItem = value; }
        }

        private PageItem _GrossAmountItem;
        public PageItem GrossAmountItem
        {
            get { return _GrossAmountItem; }
            set { _GrossAmountItem = value; }
        }

        private PageItem _NarrationItem;
        public PageItem NarrationItem
        {
            get { return _NarrationItem; }
            set { _NarrationItem = value; }
        }
        private PageItem _CreditNoteItem;
        public PageItem CreditNoteItem
        {
            get { return _CreditNoteItem; }
            set { _CreditNoteItem = value; }
        }
        private PageItem _DebitNoteItem;
        public PageItem DebitNoteItem
        {
            get { return _DebitNoteItem; }
            set { _DebitNoteItem = value; }
        }
        private PageItem _BalanceAmountItem;
        public PageItem BalanceAmountItem
        {
            get { return _BalanceAmountItem; }
            set { _BalanceAmountItem = value; }
        }
        private PageItem _NetAmountItem;
        public PageItem NetAmountItem
        {
            get { return _NetAmountItem; }
            set { _NetAmountItem = value; }
        }
        private PageItem _SubjectItem;
        public PageItem SubjectItem
        {
            get { return _SubjectItem; }
            set { _SubjectItem = value; }
        }
        private PageItem _DLNItem;
        public PageItem DLNItem
        {
            get { return _DLNItem; }
            set { _DLNItem = value; }
        }
        private PageItem _VATTINItem;
        public PageItem VATTINItem
        {
            get { return _VATTINItem; }
            set { _VATTINItem = value; }
        }
        private PageItem _ShopNameItem;
        public PageItem ShopNameItem
        {
            get { return _ShopNameItem; }
            set { _ShopNameItem = value; }
        }
        private PageItem _SignatureItem;
        public PageItem SignatureItem
        {
            get { return _SignatureItem; }
            set { _SignatureItem = value; }
        }

        private PageItem _ContinueItem;
        public PageItem ContinueItem
        {
            get { return _ContinueItem; }
            set { _ContinueItem = value; }
        }

        public PageFooter(PrintSettingsType printSettingsType)
        {
            _PrintSettingsType = printSettingsType;
            _DiscountItem = new PageItem();
            _GrossAmountItem = new PageItem();
            _NarrationItem = new PageItem();
            _CreditNoteItem = new PageItem();
            _DebitNoteItem = new PageItem();
            _BalanceAmountItem = new PageItem();
            _NetAmountItem = new PageItem();
            _SubjectItem = new PageItem();
            _DLNItem = new PageItem();
            _VATTINItem = new PageItem();
            _ShopNameItem = new PageItem();
            _SignatureItem = new PageItem();
            _ContinueItem = new PageItem();
        }

        public void ReadSettings(XDocument doc)
        {
            try
            {
                string rootElement = PrintSettings.GetRootElementName(_PrintSettingsType);
                foreach (XElement element in doc.Descendants(rootElement).Descendants("PageFooter"))
                {
                    XElement item = element.Element("DiscountItem");
                    _DiscountItem.ReadSettings(item);

                    item = element.Element("GrossAmountItem");
                    _GrossAmountItem.ReadSettings(item);

                    item = element.Element("NarrationItem");
                    _NarrationItem.ReadSettings(item);

                    item = element.Element("CreditNoteItem");
                    _CreditNoteItem.ReadSettings(item);

                    item = element.Element("DebitNoteItem");
                    _DebitNoteItem.ReadSettings(item);

                    item = element.Element("BalanceAmountItem");
                    _BalanceAmountItem.ReadSettings(item);

                    item = element.Element("NetAmountItem");
                    _NetAmountItem.ReadSettings(item);

                    item = element.Element("SubjectItem");
                    _SubjectItem.ReadSettings(item);

                    item = element.Element("DLNItem");
                    _DLNItem.ReadSettings(item);

                    item = element.Element("VATTINItem");
                    _VATTINItem.ReadSettings(item);

                    item = element.Element("ShopNameItem");
                    _ShopNameItem.ReadSettings(item);

                    item = element.Element("SignatureItem");
                    _SignatureItem.ReadSettings(item);

                    item = element.Element("ContinueItem");
                    _ContinueItem.ReadSettings(item);    
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }

    public class PageItem
    {
        private string _Text;
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private int _Row;
        public int Row
        {
            get { return _Row; }
            set { _Row = value; }
        }

        private int _Column;
        public int Column
        {
            get { return _Column; }
            set { _Column = value; }
        }

        private int _FontSize;
        public int FontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
        }

        private string _FontName;
        public string FontName
        {
            get { return _FontName; }
            set { _FontName = value; }
        }
        
        private bool _FontBold;
        public bool FontBold
        {
            get { return _FontBold; }
            set { _FontBold = value; }
        }

        private bool _Show;
        public bool Show
        {
            get { return _Show; }
            set { _Show = value; }
        }
        private bool _ShowText;
        public bool ShowText
        {
            get { return _ShowText; }
            set { _ShowText = value; }
        }

        public Font Font
        {
            get
            {
                Font newFont;
                try
                {
                    FontStyle style = FontStyle.Regular;
                    if (_FontBold)
                        style = FontStyle.Bold;
                    if (string.IsNullOrEmpty(_FontName))
                        newFont = new Font("Arial", _FontSize, style);
                    else
                        newFont = new Font(_FontName, _FontSize, style);
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                    newFont = new Font("Arial", _FontSize, FontStyle.Regular);
                }
                return newFont;
            }

        }

        public void ReadSettings(XElement element)
        {
            try
            {
                if (element == null)
                {
                    Log.WriteError("NULL Element: PageItem.ReadSettings() ");
                    return;
                }
                XElement item = element.Element("Text");
                _Text = item.Value;

                item = element.Element("Row");
                int.TryParse(item.Value, out _Row);

                item = element.Element("Column");
                int.TryParse(item.Value, out _Column);

                item = element.Element("FontSize");
                int.TryParse(item.Value, out _FontSize);

                item = element.Element("FontName");
                if (item != null)
                {
                    _FontName = item.Value;
                }

                item = element.Element("FontBold");
                bool.TryParse(item.Value, out _FontBold);

                item = element.Element("Show");
                bool.TryParse(item.Value, out _Show);

                item = element.Element("ShowText");
                bool.TryParse(item.Value, out _ShowText);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }


    }


    public class PageColumn
    {
        private string _ColumnHeader;
        public string ColumnHeader
        {
            get { return _ColumnHeader; }
            set { _ColumnHeader = value; }
        }
        private int _FontSize;
        public int FontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
        }

        private string _FontName;
        public string FontName
        {
            get { return _FontName; }
            set { _FontName = value; }
        }

        private bool _FontBold;
        public bool FontBold
        {
            get { return _FontBold; }
            set { _FontBold = value; }
        }

        private string _ColumnDataField;
        public string ColumnDataField
        {
            get { return _ColumnDataField; }
            set { _ColumnDataField = value; }
        }

        private ColumnDataType _ColumnDataType;
        public ColumnDataType ColumnDataType
        {
            get { return _ColumnDataType; }
            set { _ColumnDataType = value; }
        }
        private int _Column;
        public int Column
        {
            get { return _Column; }
            set { _Column = value; }
        }
        private bool _Show;
        public bool Show
        {
            get { return _Show; }
            set { _Show = value; }
        }

        public Font Font
        {
            get
            {
                Font newFont;
                try
                {
                    FontStyle style = FontStyle.Regular;
                    if (_FontBold)
                        style = FontStyle.Bold;
                    if (string.IsNullOrEmpty(_FontName))
                        newFont = new Font("Arial", _FontSize, style);
                    else
                        newFont = new Font(_FontName, _FontSize, style);
                }
                catch (Exception ex)
                {
                    Log.WriteException(ex);
                    newFont = new Font("Arial", _FontSize, FontStyle.Regular);
                }
                return newFont;
            }

        }

        public void ReadSettings(XElement element)
        {
            try
            {
                XElement item = element.Element("ColumnHeader");
                _ColumnHeader = item.Value;

                item = element.Element("FontSize");
                int.TryParse(item.Value, out _FontSize);

                item = element.Element("FontName");
                if (item != null)
                {
                    _FontName = item.Value;
                }
                item = element.Element("FontBold");
                bool.TryParse(item.Value, out _FontBold);

                item = element.Element("ColumnDataField");
                _ColumnDataField = item.Value;

                item = element.Element("ColumnDataType");
                if (item.Value.ToString() == "Decimal")
                    _ColumnDataType = ColumnDataType.Decimal;
                else if (item.Value.ToString() == "Integer")
                    _ColumnDataType = ColumnDataType.Integer;
                else
                    _ColumnDataType = ColumnDataType.Text;

                item = element.Element("Column");
                int.TryParse(item.Value, out _Column);

                item = element.Element("Show");
                bool.TryParse(item.Value, out _Show);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

    }
}
