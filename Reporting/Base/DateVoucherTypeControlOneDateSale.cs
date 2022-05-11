using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EcoMart.Common;

namespace EcoMart.Reporting.Base
{
    public partial class DateVoucherTypeControlOneDateSale : UserControl
    {
        public delegate void GoClicked(DateTime FromDate, VoucherTypes vType);
        public event GoClicked OnGoClicked;

        private bool _ShowVoucher = true;
        private bool _ShowCreditStatement = true;

        private const int GBOXVOUCHERNORMALHEIGHT = 60;
        private const int GBOXVOUCHERWITHOUTCSHEIGHT = 38;

        public DateVoucherTypeControlOneDateSale()
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

        public void Initialize(string fromDate)
        {
            fromDate1.Value = General.ConvertStringToDateyyyyMMdd(fromDate);
            rbtnAll.Checked = true;
            fromDate1.Focus();

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
                    VoucherTypes vType = GetVoucherType();
                    OnGoClicked(fromDate1.Value,vType);
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

        private VoucherTypes GetVoucherType()
        {
            VoucherTypes retValue = VoucherTypes.None;
            try
            {
                if (rbtnAll.Checked)
                    retValue = VoucherTypes.All;
                else if (rbtnCash.Checked)
                    retValue = VoucherTypes.Cash;
                else if (rbtnCreditStatement.Checked)
                    retValue = VoucherTypes.CreditStatement;
                else if (rbtnCredit.Checked)
                    retValue = VoucherTypes.Credit;
                else if (rbtnVoucher.Checked)
                    retValue = VoucherTypes.Voucher;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        private void fromDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ProcessGoClick();
        }
    }
}
