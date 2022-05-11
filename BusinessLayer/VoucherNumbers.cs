using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MitraPlus.DataLayer;
using MitraPlus.Common;

namespace MitraPlus.BusinessLayer
{
    public class VoucherNumbers : BaseObject
    {
        #region Declaration
        private long _PurchaseCredit;
        private long _PurchaseCashCredit;
        private long _PuchaseCash;
        private long _SaleChitNumber;
        private long _SaleCash;
        private long _SaleCredit;
        private long _SaleCashCredit; 
        private string _CreatedUserId;
        private string _ModifyDate;
        private string _ModifyUserId;
        #endregion

        #region Properties
        public long PurchaseCredit
        {
            get { return _PurchaseCredit; }
            set { _PurchaseCredit = value; }
        }

        public long PurchaseCashCredit
        {
            get { return _PurchaseCashCredit; }
            set { _PurchaseCashCredit = value; }
        }

        public long PuchaseCash
        {
            get { return _PuchaseCash; }
            set { _PuchaseCash = value; }
        }

        public long SaleChitNumber
        {
            get { return _SaleChitNumber; }
            set { _SaleChitNumber = value; }
        }

        public long SaleCash
        {
            get { return _SaleCash; }
            set { _SaleCash = value; }
        }

        public long SaleCredit
        {
            get { return _SaleCredit; }
            set { _SaleCredit = value; }
        }

        public long SaleCashCredit
        {
            get { return _SaleCashCredit; }
            set { _SaleCashCredit = value; }
        }

        public string CreatedUserId
        {
            get { return _CreatedUserId; }
            set { _CreatedUserId = value; }
        }

        public string ModifyDate
        {
            get { return _ModifyDate; }
            set { _ModifyDate = value; }
        }

        public string ModifyUserId
        {
        get { return _ModifyUserId; }
        set { _ModifyUserId = value; }
        }

        #endregion

        #region Internal Methods
       
        public override void Initialise()
        {
            try
            {
                base.Initialise();
                _PurchaseCredit = 0;
                _PurchaseCashCredit = 0;
                _PuchaseCash = 0;
                _SaleChitNumber = 0;
                _SaleCashCredit = 0;
                _SaleCash = 0;
                _SaleCredit = 0;    
                _CreatedUserId = "";
                _ModifyDate = "";
                _ModifyUserId = "";
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
        }

        public override void DoValidate()
        {
            try
            {
                if (PurchaseCredit == 0)
                    ValidationMessages.Add("Please enter the  PurchaseCredit");
                if (PurchaseCashCredit == 0)
                    ValidationMessages.Add("Please enter the  PurchaseCashCredit");
                if (PuchaseCash == 0)
                    ValidationMessages.Add("Please enter the  PuchaseCash");
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            } 
        }

        #endregion

        #region Public Methods

        public long GetVouchurNumberForPurchaseType(string purchaseType)
        {
            long voucherNo = 0;
            try
            {
                if (ReadDetails())
                {
                    if (purchaseType.ToUpper() == "CSP") 
                    {
                        voucherNo = this.PuchaseCash + 1;
                        this.PuchaseCash = voucherNo;
                    }
                    else if (purchaseType.ToUpper() == "CRP") 
                    {
                        voucherNo = this.PurchaseCredit + 1;
                        this.PurchaseCredit = voucherNo;
                    }
                    else if (purchaseType.ToUpper() == "CCP") 
                    {
                        voucherNo = this.PurchaseCashCredit + 1;
                        this.PurchaseCashCredit = voucherNo;
                    }
                    else if (purchaseType.ToUpper() == "SVU")
                    {
                        voucherNo = this.SaleChitNumber + 1;
                        this.SaleChitNumber = voucherNo;
                    }
                    else if (purchaseType.ToUpper() == "SCS") 
                    {
                        voucherNo = this.SaleCash + 1;
                        this.SaleCash = voucherNo;
                    }
                    else if (purchaseType.ToUpper() == "SCR") 
                    {
                        voucherNo = this.SaleCredit + 1;
                        this.SaleCredit = voucherNo;
                    }
                    else if (purchaseType.ToUpper() == "SCC") 
                    {
                        voucherNo = this.SaleCashCredit + 1;
                        this.SaleCashCredit = voucherNo;
                    }

                    if (!UpdateDetails())
                    {
                        return 0;
                    }
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }          
            return 1;
        }

        private bool ReadDetails()
        {

            DBVoucherNumbers dbData = new DBVoucherNumbers();
            DataRow drow = dbData.GetVouchernumbers();

            if (drow != null)
            {
                try
                {
                    if (drow["PurchaseCredit"] != DBNull.Value)
                        PurchaseCredit = Convert.ToInt64(drow["PurchaseCredit"]);
                    if (drow["PurchaseCashCredit"] != DBNull.Value)
                        PurchaseCashCredit = Convert.ToInt64(drow["PurchaseCashCredit"]);
                    if (drow["PuchaseCash"] != DBNull.Value)
                        PuchaseCash = Convert.ToInt64(drow["PuchaseCash"]);
                    if (drow["SaleChitNumber"] != DBNull.Value)
                        SaleChitNumber = Convert.ToInt16(drow["SaleChitNumber"]);
                    if (drow["SaleCash"] != DBNull.Value)
                        SaleCash = Convert.ToInt64(drow["SaleCash"]);
                    if (drow["SaleCredit"] != DBNull.Value)
                        SaleCredit = Convert.ToInt64(drow["SaleCredit"]);
                    if (drow["SaleCashCredit"] != DBNull.Value)
                        SaleCashCredit = Convert.ToInt64(drow["SaleCashCredit"]);
                    if (drow["CreatedDate"] != DBNull.Value)
                        CreatedDate = Convert.ToString(drow["CreatedDate"]);
                    if (drow["CreatedUserId"] != DBNull.Value)
                        CreatedUserId = Convert.ToString(drow["CreatedUserId"]);
                    if (drow["ModifiedDate"] != DBNull.Value)
                        ModifyDate = Convert.ToString(drow["ModifiedDate"]);
                    if (drow["ModifiedUserId"] != DBNull.Value)
                        ModifyUserId = Convert.ToString(drow["ModifiedUserId"]);
                }
                catch (Exception Ex)
                {
                    Log.WriteException(Ex);
                }
                return true;
            }
            else
            { return AddDetails(); }
        } 

        private bool AddDetails()
        {
            DBVoucherNumbers dbData = new DBVoucherNumbers();
            return dbData.AddDetails(PurchaseCredit,
                                     PurchaseCashCredit,
                                     PuchaseCash,
                                     SaleChitNumber,
                                     SaleCash,
                                     SaleCredit,
                                     SaleCashCredit,
                                     CreatedDate,
                                     CreatedUserId,
                                     ModifyDate,
                                     ModifyUserId);
        }

        private bool UpdateDetails()
        {
            DBVoucherNumbers dbData = new DBVoucherNumbers();
            return dbData.UpdateDetails(PurchaseCredit,
                                         PurchaseCashCredit,
                                         PuchaseCash,
                                         SaleChitNumber,
                                         SaleCash,
                                         SaleCredit,
                                         SaleCashCredit,
                                         CreatedDate,
                                         CreatedUserId,
                                         ModifyDate,
                                         ModifyUserId);
        }

        #endregion

    }
}
