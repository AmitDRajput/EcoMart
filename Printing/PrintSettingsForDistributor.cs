using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EcoMart.Common;
using System.Xml.Linq;
using System.Drawing;

namespace EcoMart.Printing
{

    public enum ColumnDataTypeForDistributor
    {
        None = 100,
        Text = 101,
        Integer = 102,
        Decimal = 103,
    }

    public enum PrintSettingsTypeForDistributor
    {
        None = 100,
        SaleBillPrintSettingsPlainPaperForDistributor = 101,
        SaleBillPrintSettingsPrePrintedPaperForDistributor = 102,
        DebiNotePrintSettingsPlainPaperForDistributor = 103,
        DebitNotePrintSettingsPrePrintedPaperForDistributor = 104,
    }
    public class PrintSettingsForDistributor
    {

        private const string SETTINGFILENAME = "PrintSettingsForDistributor.xml";
        private SaleBillSettingsForDistributor _SaleBillPrintSettingsPlainPaperForDistributor;
        public SaleBillSettingsForDistributor SaleBillPrintSettingsPlainPaperForDistributor
        {
            get { return _SaleBillPrintSettingsPlainPaperForDistributor; }
        }

        //private SaleBillSettingsForDistributor _SaleBillPrintSettingsPrePrintedPaperForDistributor;
        //public SaleBillSettingsForDistributor SaleBillPrintSettingsPrePrintedPaperForDistributor
        //{
        //    get { return _SaleBillPrintSettingsPrePrintedPaperForDistributor; }
        //}
        //private SaleBillSettingsForDistributor _DebitNotePrintSettingsPlainPaperForDistributor;
        //public SaleBillSettingsForDistributor DebitNotePrintSettingsPlainPaperForDistributor 
        //{
        //    get { return _DebitNotePrintSettingsPlainPaperForDistributor; }
        //}
        //private SaleBillSettingsForDistributor _DebitNotePrintSettingsPrePrintedPaperForDistributor;
        //public SaleBillSettingsForDistributor DebitNotePrintSettingsPrePrintedPaperForDistributor
        //{
        //    get { return _DebitNotePrintSettingsPrePrintedPaperForDistributor; }
        //}
        public PrintSettingsForDistributor()
        {           
            _SaleBillPrintSettingsPlainPaperForDistributor = new SaleBillSettingsForDistributor(PrintSettingsTypeForDistributor.SaleBillPrintSettingsPlainPaperForDistributor);
            //_SaleBillPrintSettingsPrePrintedPaperForDistributor = new SaleBillSettingsForDistributor(PrintSettingsTypeForDistributor.SaleBillPrintSettingsPrePrintedPaperForDistributor);
            //_DebitNotePrintSettingsPlainPaperForDistributor = new SaleBillSettingsForDistributor(PrintSettingsTypeForDistributor.DebiNotePrintSettingsPlainPaperForDistributor);
            //_DebitNotePrintSettingsPrePrintedPaperForDistributor = new SaleBillSettingsForDistributor(PrintSettingsTypeForDistributor.DebitNotePrintSettingsPrePrintedPaperForDistributor);
            ReadPrintSettingsForDistributor();
        }

        private void ReadPrintSettingsForDistributor()
        {
            try
            {
                string file = AppDomain.CurrentDomain.BaseDirectory.ToString() + SETTINGFILENAME;
                XDocument docd = XDocument.Load(file);
                _SaleBillPrintSettingsPlainPaperForDistributor.ReadSettingsForDistributor(docd);
                //_SaleBillPrintSettingsPrePrintedPaperForDistributor.ReadSettingsForDistributor(docd);
                //_DebitNotePrintSettingsPlainPaperForDistributor.ReadSettingsForDistributor(docd);
                //_DebitNotePrintSettingsPrePrintedPaperForDistributor.ReadSettingsForDistributor(docd);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public static string GetRootElementNameForDistributor(PrintSettingsTypeForDistributor settingsType)
        {
            string retValue = string.Empty;
            switch (settingsType)
            {
                case PrintSettingsTypeForDistributor.None:
                    retValue = "None";
                    break;
                case PrintSettingsTypeForDistributor.SaleBillPrintSettingsPlainPaperForDistributor:
                    retValue = "SaleBillPrintSettingsPlainPaperForDistributor";
                    break;
                case PrintSettingsTypeForDistributor.SaleBillPrintSettingsPrePrintedPaperForDistributor:
                    retValue = "SaleBillPrintSettingsPrePrintedPaperForDistributor";
                    break;
                case PrintSettingsTypeForDistributor.DebiNotePrintSettingsPlainPaperForDistributor:
                    retValue = "DebitNotePrintSettingsPlainPaperForDistributor";
                    break;
                case PrintSettingsTypeForDistributor.DebitNotePrintSettingsPrePrintedPaperForDistributor:
                    retValue = "DebitNotePrintSettingsPrePrintedPaperForDistributor";
                    break;

            }
            return retValue;
        }
    }

    public class SaleBillSettingsForDistributor
    {
        private PrintSettingsTypeForDistributor _PrintSettingsTypeForDistributor = PrintSettingsTypeForDistributor.None;
        private GeneralSettingsForDistributor _GeneralSettingsForDistributor;
        private PageHeaderForDistributor _PageHeaderForDistributor;
        private PageContentForDistributor _PageContentForDistributor;
        private PageFooterForDistributor _PageFooterForDistributor;

        public GeneralSettingsForDistributor GeneralSettingsForDistributor
        {
            get { return _GeneralSettingsForDistributor; }
        }

        public PageHeaderForDistributor PageHeaderForDistributor
        {
            get { return _PageHeaderForDistributor; }
        }

        public PageContentForDistributor PageContentForDistributor
        {
            get { return _PageContentForDistributor; }
        }

        public PageFooterForDistributor PageFooterForDistributor
        {
            get { return _PageFooterForDistributor; }
        }

        public SaleBillSettingsForDistributor(PrintSettingsTypeForDistributor printSettingsTypeForDistributor)
        {
             _PrintSettingsTypeForDistributor = printSettingsTypeForDistributor;
            _GeneralSettingsForDistributor = new GeneralSettingsForDistributor(_PrintSettingsTypeForDistributor);
            _PageHeaderForDistributor = new PageHeaderForDistributor(_PrintSettingsTypeForDistributor);
            _PageContentForDistributor = new PageContentForDistributor(_PrintSettingsTypeForDistributor);
            _PageFooterForDistributor = new PageFooterForDistributor(_PrintSettingsTypeForDistributor);
        }

        public void ReadSettingsForDistributor(XDocument doc)
        {
            try
            {
                _GeneralSettingsForDistributor.ReadSettingsForDistributor(doc);
                _PageHeaderForDistributor.ReadSettingsForDistributor(doc);
                _PageContentForDistributor.ReadSettingsForDistributor(doc);
                _PageFooterForDistributor.ReadSettingsForDistributor(doc);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }

    public class GeneralSettingsForDistributor
    {
        private PrintSettingsTypeForDistributor _PrintSettingsTypeForDistributor = PrintSettingsTypeForDistributor.None;
        private int _PageWidth;
        private int _PageHeight;
        private double _ReverseLineFeed;
        private double _LineFeed;
        private int _ContentStartRow;

        public GeneralSettingsForDistributor(PrintSettingsTypeForDistributor printSettingsTypeForDistributor)
        {
           _PrintSettingsTypeForDistributor = printSettingsTypeForDistributor;
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

        public void ReadSettingsForDistributor(XDocument doc)
        {
            try
            {
                string rootElement = PrintSettingsForDistributor.GetRootElementNameForDistributor(_PrintSettingsTypeForDistributor);
                foreach (XElement element in doc.Descendants(rootElement).Descendants("GeneralSettingsForDistributor"))
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
    public class PageHeaderForDistributor
    {
        private PrintSettingsTypeForDistributor _PrintSettingsTypeForDistributor = PrintSettingsTypeForDistributor.None;

        private PageItemForDistributor _ShopName;
        public PageItemForDistributor ShopName
        {
            get { return _ShopName; }
            set { _ShopName = value; }
        }
        private PageItemForDistributor _ShopAddress1;
        public PageItemForDistributor ShopAddress1
        {
            get { return _ShopAddress1; }
            set { _ShopAddress1 = value; }
        }

        private PageItemForDistributor _ShopAddress2;
        public PageItemForDistributor ShopAddress2
        {
            get { return _ShopAddress2; }
            set { _ShopAddress2 = value; }
        }

        private PageItemForDistributor _ShopTelephone;
        public PageItemForDistributor ShopTelephone
        {
            get { return _ShopTelephone; }
            set { _ShopTelephone = value; }
        }

        private PageItemForDistributor _PatientName;
        public PageItemForDistributor PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }

        private PageItemForDistributor _PatientAddress1;
        public PageItemForDistributor PatientAddress1
        {
            get { return _PatientAddress1; }
            set { _PatientAddress1 = value; }
        }
        private PageItemForDistributor _PatientAddress2;
        public PageItemForDistributor PatientAddress2
        {
            get { return _PatientAddress2; }
            set { _PatientAddress2 = value; }
        }
        private PageItemForDistributor _PatientTelephone;
        public PageItemForDistributor PatientTelephone
        {
            get { return _PatientTelephone; }
            set { _PatientTelephone = value; }
        }
        private PageItemForDistributor _PatientVATTINV;
        public PageItemForDistributor PatientVATTINV
        {
            get { return _PatientVATTINV; }
            set { _PatientVATTINV = value; }
        }
        private PageItemForDistributor _PartyDLN;
        public PageItemForDistributor PartyDLN
        {
            get { return _PartyDLN; }
            set { _PartyDLN = value; }
        }
        private PageItemForDistributor _PartyLBT;
        public PageItemForDistributor PartyLBT
        {
            get { return _PartyLBT; }
            set { _PartyLBT = value; }
        }
        private PageItemForDistributor _DoctorName;
        public PageItemForDistributor DoctorName
        {
            get { return _DoctorName; }
            set { _DoctorName = value; }
        }

        private PageItemForDistributor _DoctorAddress;
        public PageItemForDistributor DoctorAddress
        {
            get { return _DoctorAddress; }
            set { _DoctorAddress = value; }
        }

        private PageItemForDistributor _Time;
        public PageItemForDistributor Time
        {
            get { return _Time; }
            set { _Time = value; }
        }

        private PageItemForDistributor _VoucherTypeSCA;
        public PageItemForDistributor VoucherTypeSCA
        {
            get { return _VoucherTypeSCA; }
            set { _VoucherTypeSCA = value; }
        }

        private PageItemForDistributor _VoucherTypeSCR;
        public PageItemForDistributor VoucherTypeSCR
        {
            get { return _VoucherTypeSCR; }
            set { _VoucherTypeSCR = value; }
        }

        private PageItemForDistributor _VoucherTypeSCS;
        public PageItemForDistributor VoucherTypeSCS
        {
            get { return _VoucherTypeSCS; }
            set { _VoucherTypeSCS = value; }
        }

        private PageItemForDistributor _VoucherTypeSVU;
        public PageItemForDistributor VoucherTypeSVU
        {
            get { return _VoucherTypeSVU; }
            set { _VoucherTypeSVU = value; }
        }

        private PageItemForDistributor _VoucherTypeCreditNote;
        public PageItemForDistributor VoucherTypeCreditNote
        {
            get { return _VoucherTypeCreditNote; }
            set { _VoucherTypeCreditNote = value; }
        }

        private PageItemForDistributor _VoucherTypeDebitNote;
        public PageItemForDistributor VoucherTypeDebitNote
        {
            get { return _VoucherTypeDebitNote; }
            set { _VoucherTypeDebitNote = value; }
        }

        private PageItemForDistributor _Date;
        public PageItemForDistributor Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        private PageItemForDistributor _PageNo;
        public PageItemForDistributor PageNo
        {
            get { return _PageNo; }
            set { _PageNo = value; }
        }

        public PageHeaderForDistributor(PrintSettingsTypeForDistributor printSettingsTypeForDistributor)
        {
            _PrintSettingsTypeForDistributor = printSettingsTypeForDistributor;
            _ShopName = new PageItemForDistributor();
            _ShopAddress1 = new PageItemForDistributor();
            _ShopAddress2 = new PageItemForDistributor();
            _ShopTelephone = new PageItemForDistributor();
            _PatientName = new PageItemForDistributor();
            _PatientAddress1 = new PageItemForDistributor();
            _PatientAddress2 = new PageItemForDistributor();
            _PatientTelephone = new PageItemForDistributor();
            _PatientVATTINV = new PageItemForDistributor();
            _PartyDLN = new PageItemForDistributor();
            _PartyLBT = new PageItemForDistributor();
            _DoctorName = new PageItemForDistributor();
            _DoctorAddress = new PageItemForDistributor();
            _Time = new PageItemForDistributor();
            _VoucherTypeSCA = new PageItemForDistributor();
            _VoucherTypeSCR = new PageItemForDistributor();
            _VoucherTypeSCS = new PageItemForDistributor();
            _VoucherTypeSVU = new PageItemForDistributor();
            _VoucherTypeCreditNote = new PageItemForDistributor();
            _VoucherTypeDebitNote = new PageItemForDistributor();
            _Date = new PageItemForDistributor();
            _PageNo = new PageItemForDistributor();
        }

        public void ReadSettingsForDistributor(XDocument doc)
        {
            try
            {
                string rootElement = PrintSettingsForDistributor.GetRootElementNameForDistributor(_PrintSettingsTypeForDistributor);
                foreach (XElement element in doc.Descendants(rootElement).Descendants("PageHeaderForDistributor"))
                {
                    XElement item = element.Element("ShopNameItem");
                    _ShopName.ReadSettingsForDistributor(item);

                    item = element.Element("ShopAddress1Item");
                    _ShopAddress1.ReadSettingsForDistributor(item);

                    item = element.Element("ShopAddress2Item");
                    _ShopAddress2.ReadSettingsForDistributor(item);

                    item = element.Element("ShopTelephoneItem");
                    _ShopTelephone.ReadSettingsForDistributor(item);

                    item = element.Element("PatientName");
                    _PatientName.ReadSettingsForDistributor(item);

                    item = element.Element("PatientAddress1");
                    _PatientAddress1.ReadSettingsForDistributor(item);

                    item = element.Element("PatientAddress2");
                    _PatientAddress2.ReadSettingsForDistributor(item);

                    item = element.Element("PatientTelephone");
                    _PatientTelephone.ReadSettingsForDistributor(item);

                    item = element.Element("PatientVATTINV");
                    _PatientVATTINV.ReadSettingsForDistributor(item);

                    item = element.Element("PatientDLN");
                    _PartyDLN.ReadSettingsForDistributor(item);

                    item = element.Element("PatientLBT");
                    _PartyLBT.ReadSettingsForDistributor(item);

                    item = element.Element("DoctorName");
                    _DoctorName.ReadSettingsForDistributor(item);

                    item = element.Element("DoctorAddress");
                    _DoctorAddress.ReadSettingsForDistributor(item);

                    item = element.Element("TimeItem");
                    _Time.ReadSettingsForDistributor(item);

                    item = element.Element("VoucherTypeSCAItem");
                    _VoucherTypeSCA.ReadSettingsForDistributor(item);

                    item = element.Element("VoucherTypeSCRItem");
                    _VoucherTypeSCR.ReadSettingsForDistributor(item);

                    item = element.Element("VoucherTypeSCSItem");
                    _VoucherTypeSCS.ReadSettingsForDistributor(item);

                    item = element.Element("VoucherTypeSVUItem");
                    _VoucherTypeSVU.ReadSettingsForDistributor(item);

                    item = element.Element("VoucherTypeCreditNoteItem");
                    _VoucherTypeCreditNote.ReadSettingsForDistributor(item);

                    item = element.Element("VoucherTypeDebitNoteItem");
                    _VoucherTypeDebitNote.ReadSettingsForDistributor(item);

                    item = element.Element("DateItem");
                    _Date.ReadSettingsForDistributor(item);

                    item = element.Element("PageNoItem");
                    _PageNo.ReadSettingsForDistributor(item);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

       
    }

    
    public class PageContentForDistributor
    {
        private PrintSettingsTypeForDistributor _PrintSettingsTypeForDistributor = PrintSettingsTypeForDistributor.None;
        private int _ColumnCount;
        public int ColumnCount
        {
            get { return _ColumnCount; }
            set { _ColumnCount = value; }
        }

        public List<PageColumnForDistributor> Columns;

        public PageContentForDistributor(PrintSettingsTypeForDistributor printSettingsTypeForDistributor)
        {
            _PrintSettingsTypeForDistributor = printSettingsTypeForDistributor;
            Columns = new List<PageColumnForDistributor>();
        }
        public void ReadSettingsForDistributor(XDocument doc)
        {
            try
            {
               
                string rootElement = PrintSettingsForDistributor.GetRootElementNameForDistributor(_PrintSettingsTypeForDistributor);
                foreach (XElement element in doc.Descendants(rootElement).Descendants("PageContentForDistributor"))
                {
                    XElement item = element.Element("ColumnCount");
                    int.TryParse(item.Value, out _ColumnCount);
                    for (int i = 1; i <= _ColumnCount; i++)
                    {
                        string elementName = "Column" + i.ToString();
                        foreach (XElement column in element.Descendants(elementName))
                        {
                            PageColumnForDistributor pColumn = new PageColumnForDistributor();
                            pColumn.ReadSettingsForDistributor(column);
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
    
    public class PageFooterForDistributor
    {
        private PrintSettingsTypeForDistributor _PrintSettingsTypeForDistributor = PrintSettingsTypeForDistributor.None;
        private PageItemForDistributor _NarrationItem;
        public PageItemForDistributor NarrationItem
        {
            get { return _NarrationItem; }
            set { _NarrationItem = value; }
        }
        private PageItemForDistributor _GrossAmountItem;
        public PageItemForDistributor GrossAmountItem
        {
            get { return _GrossAmountItem; }
            set { _GrossAmountItem = value; }
        }
        private PageItemForDistributor _DiscountItem;
        public PageItemForDistributor DiscountItem
        {
            get { return _DiscountItem; }
            set { _DiscountItem = value; }
        }
        private PageItemForDistributor _Vat12point5Item;
        public PageItemForDistributor Vat12point5Item
        {
            get { return _Vat12point5Item; }
            set { _Vat12point5Item = value; }
        }
        private PageItemForDistributor _Vat5Item;
        public PageItemForDistributor Vat5Item
        {
            get { return _Vat5Item; }
            set { _Vat5Item = value; }
        }
        private PageItemForDistributor _ADDItem;
        public PageItemForDistributor ADDItem
        {
            get { return _ADDItem; }
            set { _ADDItem = value; }
        }
        private PageItemForDistributor _LESSItem;
        public PageItemForDistributor LESSItem
        {
            get { return _LESSItem; }
            set { _LESSItem = value; }
        }
        private PageItemForDistributor _NetAmountItem;
        public PageItemForDistributor NetAmountItem
        {
            get { return _NetAmountItem; }
            set { _NetAmountItem = value; }
        }
        private PageItemForDistributor _CreditNoteItem;
        public PageItemForDistributor CreditNoteItem
        {
            get { return _CreditNoteItem; }
            set { _CreditNoteItem = value; }
        }
        private PageItemForDistributor _DebitNoteItem;
        public PageItemForDistributor DebitNoteItem
        {
            get { return _DebitNoteItem; }
            set { _DebitNoteItem = value; }
        }
        private PageItemForDistributor _BalanceAmountItem;
        public PageItemForDistributor BalanceAmountItem
        {
            get { return _BalanceAmountItem; }
            set { _BalanceAmountItem = value; }
        }

        private PageItemForDistributor _SubjectItem;
        public PageItemForDistributor SubjectItem
        {
            get { return _SubjectItem; }
            set { _SubjectItem = value; }
        }
        private PageItemForDistributor _DLNItem;
        public PageItemForDistributor DLNItem
        {
            get { return _DLNItem; }
            set { _DLNItem = value; }
        }
        private PageItemForDistributor _LBTItem;
        public PageItemForDistributor LBTItem
        {
            get { return _LBTItem; }
            set { _LBTItem = value; }
        }
        private PageItemForDistributor _VATTINVItem;
        public PageItemForDistributor VATTINVItem
        {
            get { return _VATTINVItem; }
            set { _VATTINVItem = value; }
        }
        private PageItemForDistributor _VATTINCItem;
        public PageItemForDistributor VATTINCItem
        {
            get { return _VATTINCItem; }
            set { _VATTINCItem = value; }
        }
        private PageItemForDistributor _DeclarationItem1;
        public PageItemForDistributor DeclarationItem1
        {
            get { return _DeclarationItem1; }
            set { _DeclarationItem1 = value; }
        }
        private PageItemForDistributor _DeclarationItem2;
        public PageItemForDistributor DeclarationItem2
        {
            get { return _DeclarationItem2; }
            set { _DeclarationItem2 = value; }
        }
        private PageItemForDistributor _DeclarationItem3;
        public PageItemForDistributor DeclarationItem3
        {
            get { return _DeclarationItem3; }
            set { _DeclarationItem3 = value; }
        }
        private PageItemForDistributor _ShopNameItem;
        public PageItemForDistributor ShopNameItem
        {
            get { return _ShopNameItem; }
            set { _ShopNameItem = value; }
        }
        private PageItemForDistributor _SignatureItem;
        public PageItemForDistributor SignatureItem
        {
            get { return _SignatureItem; }
            set { _SignatureItem = value; }
        }

        private PageItemForDistributor _ContinueItem;
        public PageItemForDistributor ContinueItem
        {
            get { return _ContinueItem; }
            set { _ContinueItem = value; }
        }

        private PageItemForDistributor _SchemeDiscountItem;
        public PageItemForDistributor SchemeDiscountItem
        {
            get { return _SchemeDiscountItem; }
            set { _SchemeDiscountItem = value; }
        }
        private PageItemForDistributor _ItemDiscountItem;
        public PageItemForDistributor ItemDiscountItem
        {
            get { return _ItemDiscountItem; }
            set { _ItemDiscountItem = value; }
        }
        private PageItemForDistributor _RoundupItem;
        public PageItemForDistributor RoundupItem
        {
            get { return _RoundupItem; }
            set { _RoundupItem = value; }
        }
        public PageFooterForDistributor(PrintSettingsTypeForDistributor printSettingsTypeForDistributor)
        {
            _PrintSettingsTypeForDistributor = printSettingsTypeForDistributor;
            _DiscountItem = new PageItemForDistributor();
            _GrossAmountItem = new PageItemForDistributor();
            _NarrationItem = new PageItemForDistributor();
            _Vat12point5Item = new PageItemForDistributor();
            _Vat5Item = new PageItemForDistributor();
            _ADDItem = new PageItemForDistributor();
            _LESSItem = new PageItemForDistributor();
            _CreditNoteItem = new PageItemForDistributor();
            _DebitNoteItem = new PageItemForDistributor();
            _BalanceAmountItem = new PageItemForDistributor();
            _NetAmountItem = new PageItemForDistributor();
            _SubjectItem = new PageItemForDistributor();
            _DLNItem = new PageItemForDistributor();
            _LBTItem = new PageItemForDistributor();
            _VATTINVItem = new PageItemForDistributor();
            _VATTINCItem = new PageItemForDistributor();
            _ShopNameItem = new PageItemForDistributor();
            _DeclarationItem1 = new PageItemForDistributor();
            _DeclarationItem2 = new PageItemForDistributor();
            _DeclarationItem3 = new PageItemForDistributor();
            _SignatureItem = new PageItemForDistributor();
            _ContinueItem = new PageItemForDistributor();          
            _SchemeDiscountItem = new PageItemForDistributor();
            _RoundupItem = new PageItemForDistributor();
            _ItemDiscountItem = new PageItemForDistributor();
        }

        public void ReadSettingsForDistributor(XDocument doc)
        {
            try
            {
                string rootElementForDistribuor = PrintSettingsForDistributor.GetRootElementNameForDistributor(_PrintSettingsTypeForDistributor);
                foreach (XElement element in doc.Descendants(rootElementForDistribuor).Descendants("PageFooterForDistributor"))
                {
                    XElement item = element.Element("DiscountItem");
                    _DiscountItem.ReadSettingsForDistributor(item);

                    item = element.Element("GrossAmountItem");
                    _GrossAmountItem.ReadSettingsForDistributor(item);

                    item = element.Element("NarrationItem");
                    _NarrationItem.ReadSettingsForDistributor(item);

                    item = element.Element("VAT12Point5Item");
                    _Vat12point5Item.ReadSettingsForDistributor(item);

                    item = element.Element("VAT5Item");
                    _Vat5Item.ReadSettingsForDistributor(item);

                    item = element.Element("ADDItem");
                    _ADDItem.ReadSettingsForDistributor(item);

                    item = element.Element("LESSItem");
                    _LESSItem.ReadSettingsForDistributor(item);

                    item = element.Element("CreditNoteItem");
                    _CreditNoteItem.ReadSettingsForDistributor(item);

                    item = element.Element("DebitNoteItem");
                    _DebitNoteItem.ReadSettingsForDistributor(item);

                    item = element.Element("BalanceAmountItem");
                    _BalanceAmountItem.ReadSettingsForDistributor(item);

                    item = element.Element("NetAmountItem");
                    _NetAmountItem.ReadSettingsForDistributor(item);

                    item = element.Element("SubjectItem");
                    _SubjectItem.ReadSettingsForDistributor(item);

                    item = element.Element("DLNItem");
                    _DLNItem.ReadSettingsForDistributor(item);

                    item = element.Element("LBTItem");
                    _LBTItem.ReadSettingsForDistributor(item);

                    item = element.Element("VATTINVItem");
                    _VATTINVItem.ReadSettingsForDistributor(item);

                    item = element.Element("VATTINCItem");
                    _VATTINCItem.ReadSettingsForDistributor(item);

                    item = element.Element("ShopNameItem");
                    _ShopNameItem.ReadSettingsForDistributor(item);

                    item = element.Element("DeclarationItem1");
                    _DeclarationItem1.ReadSettingsForDistributor(item);

                    item = element.Element("DeclarationItem2");
                    _DeclarationItem2.ReadSettingsForDistributor(item);

                    item = element.Element("DeclarationItem3");
                    _DeclarationItem3.ReadSettingsForDistributor(item);

                    item = element.Element("SignatureItem");
                    _SignatureItem.ReadSettingsForDistributor(item);

                    item = element.Element("ContinueItem");
                    _ContinueItem.ReadSettingsForDistributor(item);

                    item = element.Element("SchemeDiscountItem");
                    _SchemeDiscountItem.ReadSettingsForDistributor(item);

                    item = element.Element("ItemDiscountItem");
                    _ItemDiscountItem.ReadSettingsForDistributor(item);

                    item = element.Element("RoundupItem");
                    _RoundupItem.ReadSettingsForDistributor(item);
                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
    }

    public class PageItemForDistributor
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

       public void ReadSettingsForDistributor(XElement element)
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


    public class PageColumnForDistributor
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

        public void ReadSettingsForDistributor(XElement element)
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
