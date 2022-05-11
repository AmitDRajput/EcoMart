using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;

namespace PharmaSYSRetailPlus.Reporting.Base
{
    public partial class DateVoucherTypeControl : UserControl
    {
        public delegate void GoClicked(DateTime FromDate, DateTime ToDate, VoucherType vType);
        public event GoClicked OnGoClicked;

        private bool _ShowVoucher = true;
        private bool _ShowCreditStatement = true;

        private const int GBOXVOUCHERNORMALHEIGHT = 60;
        private const int GBOXVOUCHERWITHOUTCSHEIGHT = 38;

        public DateVoucherTypeControl()
        {
            InitializeComponent();
        }

        public bool ShowVoucher
        {
            get
            {
                return _ShowVoucher;
            }
            set
            {
                _ShowVoucher = value;
                rbtnVoucher.Visible = value;
            }
        }

        public bool ShowCreditStatement
        {
            get
            {
                return _ShowCreditStatement;
            }
            set
            {
                _ShowCreditStatement = value;
                rbtnCreditStatement.Visible = value;
                if (rbtnCreditStatement.Visible)
                {
                    gboxVoucherType.Height = GBOXVOUCHERNORMALHEIGHT;
                }
                else
                {
                    gboxVoucherType.Height = GBOXVOUCHERWITHOUTCSHEIGHT;
                }
            }
        }

        public void Initialize(string fromDate, string toDate)
        {
            fromDate1.Value = General.ConvertStringToDateyyyyMMdd(fromDate);
            toDate1.Value = General.ConvertStringToDateyyyyMMdd(toDate);
        }

        public void SetFocus()
        {
            fromDate1.Focus();
        }

        public void ProcessGoClick()
        {
            try
            {
                if (OnGoClicked != null)
                {
                    string MFromDate = fromDate1.Value.Date.ToString("yyyyMMdd");
                    string MToDate = toDate1.Value.Date.ToString("yyyyMMdd");
                    VoucherType vType = GetVoucherType();
                    OnGoClicked(fromDate1.Value, toDate1.Value, vType);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            ProcessGoClick();
        }

        private VoucherType GetVoucherType()
        {
            VoucherType retValue = VoucherType.None;
            try
            {
                if (rbtnAll.Checked)
                    retValue = VoucherType.All;
                else if (rbtnCash.Checked)
                    retValue = VoucherType.Cash;
                else if (rbtnCreditStatement.Checked)
                    retValue = VoucherType.CreditStatement;
                else if (rbtnCredit.Checked)
                    retValue = VoucherType.Credit;
                else if (rbtnVoucher.Checked)
                    retValue = VoucherType.Voucher;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
    }
}
