using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PharmaSYSRetailPlus.DataLayer;
using PharmaSYSRetailPlus.Common;
using System.Drawing;


namespace PharmaSYSRetailPlus.BusinessLayer
{
       
    class PrintingVariables : BaseObject
    {
        private int _PrintRowCount;
        private int _PrintPageNumber;
        private int _PrintRowPixel;
        private int _PrintColumnPixel;
        private string _PrintReportHead;
        private string _PrintReportHead2;
        private int _PrintTotalPages;
        private Font _PrintFont;
        private double _PrintTotalAmount;
        private double _totpg;
        private string _PrintIfFirstRow;

        #region Properties
        public int PrintRowCount
        {
            get { return _PrintRowCount; }
            set { _PrintRowCount = value; }
        }
        public int PrintPageNumber
        {
            get { return _PrintPageNumber; }
            set { _PrintPageNumber = value; }
        }
        public int PrintRowPixel
        {
            get { return _PrintRowPixel; }
            set { _PrintRowPixel = value; }
        }
        public int PrintColumnPixel
        {
            get { return _PrintColumnPixel; }
            set { _PrintColumnPixel = value; }
        }
        public string PrintReportHead
        {
            get { return _PrintReportHead; }
            set { _PrintReportHead = value; }
        }
        public string PrintReportHead2
        {
            get { return _PrintReportHead2; }
            set { _PrintReportHead2 = value; }
        }
        public int PrintTotalPages
        {
            get { return _PrintTotalPages; }
            set { _PrintTotalPages = value; }
        }
        public Font PrintFont
        {
            get { return _PrintFont; }
            set { _PrintFont = value; }
        }
        public double PrintTotalAmount
        {
            get { return _PrintTotalAmount; }
            set { _PrintTotalAmount = value; }
        }
        public double totpg
        {
            get { return _totpg; }
            set { _totpg = value; }
        }
        public string PrintIfFirstRow
        {
            get { return _PrintIfFirstRow; }
            set { _PrintIfFirstRow = value; }
        }
        private OperationMode _Mode;
        public OperationMode Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
       
        #endregion
    }
}
