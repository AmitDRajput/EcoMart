using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    class CashExpenseSalary : BaseObject 
    {

          #region Declaration


        private string _CBId;
        private string _CBAccountId;
        private string _CBName;
        private string _CBAddress;
        private string _CBNarration;
        private string _CBVouType;
        private int _CBVouNo;
        private string _CBVouDate;
        private int _CBNoOfRows;
        private double _CBAmount;
        private bool _DuplicateAccount;

        private string _RAccountID;
        private double _RDebit;
        private double _RCredit;


        # endregion

        #region Constructors
        public CashExpenseSalary()
        {
            Initialise();
        }
        #endregion

        # region properties
        public string RAccountID
        {
            get { return _RAccountID; }
            set { _RAccountID = value; }
        }
        public double Rdebit
        {
            get { return _RDebit; }
            set { _RDebit = value; }
        }
        public double Rcredit
        {
            get { return _RCredit; }
            set { _RCredit = value; }
        }
        public bool DuplicateAccount
        {
            get { return _DuplicateAccount; }
            set { _DuplicateAccount = value; }
        }

        public string CBAccountID
        {
            get { return _CBAccountId; }
            set { _CBAccountId = value; }
        }


        public string CBVouDate
        {
            get { return _CBVouDate; }
            set { _CBVouDate = value; }
        }

        public string CBId
        {
            get { return _CBId; }
            set { _CBId = value; }
        }

        public string CBName
        {
            get { return _CBName; }
            set { _CBName = value; }
        }
        public string CBAddress
        {
            get { return _CBAddress; }
            set { _CBAddress = value; }
        }
        public string CBNarration
        {
            get { return _CBNarration; }
            set { _CBNarration = value; }
        }
        public string CBVouType
        {
            get { return _CBVouType; }
            set { _CBVouType = value; }
        }
        public int CBVouNo
        {
            get { return _CBVouNo; }
            set { _CBVouNo = value; }
        }
        public int CBNoOFRows
        {
            get { return _CBNoOfRows; }
            set { _CBNoOfRows = value; }
        }

        public double CBAmount
        {
            get { return _CBAmount; }
            set { _CBAmount = value; }
        }

        #endregion

        #region Internal Methods

        public override void Initialise()
        {
            base.Initialise();



            _CBId = "";
            _CBName = "";
            _CBAccountId = "";
            _CBAddress = "";
            _CBNarration = "";
         //   _CBVouType = FixAccounts.VoucherTypeForCashExpensesSalary
            _CBVouNo = 0;
            _CBNoOfRows = 0;
            _CBAmount = 0;
            _CBVouDate = "";

            _DuplicateAccount = false;
            _RAccountID = null;
            _RCredit = 0;
            _RDebit = 0;
        }
        public override void DoValidate()
        {
            
            if (CBAmount <= 0  )
                ValidationMessages.Add("Invalid Amount");
        }
        public override bool CanBeDeleted()
        {
            bool bRetValue = true;
            return bRetValue;
        }


        #endregion

        #region Public Methods
        public DataTable GetOverviewData(string CBType)
        {
            DBCashExpenseSalary CBtran = new DBCashExpenseSalary();        
            return CBtran.GetOverviewData(CBType);
        }
        public int GetAndUpdateCPENumber(string voucherseries)
        {
            int vouno = 0;
            try
            {
                DBGetVouNumbers dbno = new DBGetVouNumbers();
                vouno = dbno.GetCashExpenses(voucherseries);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return vouno;
        }
        
        public bool ReadDetailsByID()
        {
            bool retValue = false;
            DataRow drow = null;
            try
            {
                DBCashExpenseSalary CBtran = new DBCashExpenseSalary();
                drow = CBtran.ReadDetailsByID(Id);

                if (drow != null)
                {
                    CBVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    CBNarration = Convert.ToString(drow["Narration"]);
                    CBAmount = Convert.ToDouble(drow["AmountNet"].ToString());
                    CBVouDate = drow["VoucherDate"].ToString();
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        public DataTable ReadBillDetailsByID(string ID)
        {
            DBCashExpenseSalary CBtran = new DBCashExpenseSalary();
            return CBtran.GetExpensesDetailsByID(ID);
        }



        public bool AddDetails()
        {
            DBCashExpenseSalary CBtran = new DBCashExpenseSalary();
            return CBtran.AddDetails(Id, CBAccountID, CBNarration, CBVouType, CBVouNo, CBVouDate, CBAmount,CreatedBy,CreatedDate,CreatedTime);
        }
        public bool AddParticularsDetails(string id,string acid,double mdebit,double mcredit)
        {
            DBCashExpenseSalary CBtran = new DBCashExpenseSalary();
            return CBtran.AddDetailsP(id, acid, mdebit, mcredit);
        }


        public bool UpdateDetails()
        {
            DBCashExpenseSalary CBtran = new DBCashExpenseSalary();
            return CBtran.UpdateDetails(Id, CBNarration, CBVouType, CBVouNo, CBVouDate,
                CBAmount, ModifiedBy,ModifiedDate,ModifiedTime);
        }

        public bool DeleteDetails()
        {
            DBCashExpenseSalary CBtran = new DBCashExpenseSalary();
            return CBtran.DeleteDetails(Id);
        }

        public bool DeletePreviousRecords()
        {
            DBCashExpenseSalary CBtran = new DBCashExpenseSalary();
            return CBtran.DeletePreviosRowsByID(Id);
        }


        #endregion

    }
}
