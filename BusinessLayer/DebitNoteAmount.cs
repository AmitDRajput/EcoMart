using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using EcoMart.DataLayer;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public class DebitNoteAmount : BaseObject
    {
        #region Declaration
        private string _CrdbId;
        private string _CrdbAccountId;
        private string _CrdbName;
        private string _CrdbAddress;
        private string _CrdbNarration;
        private string _CrdbVouType;
        private int  _CrdbVouNo;
        private string _CrdbVouDate;
        private string _CrdbVouSeries;
        private int  _CrdbNoOfRows;
        private double _CrdbVat5;
        private double _CrdbVat12point5;
        private double _CrdbAmount;
        private double _CrdbDiscPer;
        private double _CrdbDiscAmt;
        private double _CrdbAmountNet;
        private double _CrdbRoundAmount;
        private double _CrdbTotalAmount;
        private string _Particulars;
        private double _Amount;
        private  int _ClearVouNo;
        private string _ClearVouType;
        private double _BillAmounti;
        private double _BillAmountd;


        private double _CrdbPreSelectedAmount;
        private string _DetailIDForSelected;
        private string _IDForSelected;
        private int _CrdbVouNoForSelected;
        private double _CrdbBillAmountForSelected;
        private string  _CrdbTextForSelected;
        private int _IDForSelectedDNA;
        #endregion

        #region Constructors
        public DebitNoteAmount()
        {
            Initialise();
        }
        #endregion

         # region properties
      
        public int ClearVouNo
        {
            get { return _ClearVouNo; }
            set { _ClearVouNo = value; }
        }
        public string ClearVouType
        {
            get { return _ClearVouType; }
            set { _ClearVouType = value; }
        }
        public string CrdbVouSeries
        {
            get { return _CrdbVouSeries; }
            set { _CrdbVouSeries = value; }
        }
        public string AccountID
        {
            get { return _CrdbAccountId; }
            set { _CrdbAccountId = value; }
        }

        public string Particulars
        {
            get { return _Particulars; }
            set { _Particulars = value; }
        }

        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }


        public string CrdbVouDate
        {
             get { return _CrdbVouDate;}
            set { _CrdbVouDate = value;}
        }

        public string CrdbId
      
        {
            get { return _CrdbId; }
            set { _CrdbId = value; }
        }

        public string CrdbName
        {
            get { return _CrdbName; }
            set { _CrdbName = value; }
        }
        public string CrdbAddress
        {
            get { return _CrdbAddress; }
            set { _CrdbAddress = value; }
        }
        public string CrdbNarration
        {
            get { return _CrdbNarration; }
            set { _CrdbNarration = value; }
        }
        public string CrdbVouType
        {
            get { return _CrdbVouType; }
            set { _CrdbVouType = value; }
        }
        public int CrdbVouNo
        {
            get { return _CrdbVouNo; }
            set { _CrdbVouNo = value; }
        }
        public int CrdbNoOFRows
        {
            get { return _CrdbNoOfRows; }
            set { _CrdbNoOfRows = value; }
        }
        public double CrdbVat5
        {
            get { return _CrdbVat5; }
            set { _CrdbVat5 = value; }
        }
        public double CrdbVat12point5
        {
            get {return _CrdbVat12point5;}
            set { _CrdbVat12point5 = value; }
        }
        public double CrdbAmount
        {
            get { return _CrdbAmount; }
            set { _CrdbAmount = value; }
        }
        public double CrdbDiscPer
        {
            get { return _CrdbDiscPer; }
            set { _CrdbDiscPer = value;}
        }
        public double CrdbDiscAmt
        {
            get { return _CrdbDiscAmt; }
            set { _CrdbDiscAmt = value; }
        }
        public double CrdbAmountNet
        {
            get { return _CrdbAmountNet; }
            set { _CrdbAmountNet = value; }
        }

        public double CrdbRoundAmount
        {
            get { return _CrdbRoundAmount; }
            set { _CrdbRoundAmount = value; }
        }

        public double BillAmounti
        {
            get { return _BillAmounti; }
            set { _BillAmounti = value; }
        }

        public double BillAmountd
        {
            get { return _BillAmountd; }
            set { _BillAmountd = value; }
        }
        public double CrdbTotalAmount
        {
            get { return _CrdbTotalAmount; }
            set { _CrdbTotalAmount = value; }
        }

        public double CrdbBillAmountForSelected
        {
            get { return _CrdbBillAmountForSelected; }
            set { _CrdbBillAmountForSelected = value; }
        }

        public double CrdbPreSelectedAmount
        {
            get { return _CrdbPreSelectedAmount; }
            set { _CrdbPreSelectedAmount = value; }
        }

        public string DetailIDForSelected
        {
            get { return _DetailIDForSelected; }
            set { _DetailIDForSelected = value; }
        }

        public string IDForSelected
        {
            get { return _IDForSelected; }
            set { _IDForSelected = value; }
        }

        public int IDForSelectedDNA
        {
            get { return _IDForSelectedDNA; }
            set { _IDForSelectedDNA = value; }
        }

        public int CrdbVouNoForSelected
        {
            get { return _CrdbVouNoForSelected; }
            set { _CrdbVouNoForSelected = value; }
        }
        public string CrdbTextForSelected
        {
            get { return _CrdbTextForSelected; }
            set { _CrdbTextForSelected = value; }
        }
        #endregion

         #region Internal Methods
        public override void Initialise()
        {
            base.Initialise();
            _CrdbId = "";
            _CrdbName = "";
            _CrdbAccountId = "";
            _CrdbAddress = "";
            _CrdbNarration = "";
            _CrdbVouType = FixAccounts.VoucherTypeForDebitNoteAmount;
            _CrdbVouNo = 0;
            _CrdbVouSeries = General.ShopDetail.ShopVoucherSeries;
            _CrdbVat5 = 0;
            _CrdbVat12point5 = 0;
            _CrdbNoOfRows = 0;
            _CrdbAmount = 0;
            _CrdbDiscPer = 0;
            _CrdbDiscAmt = 0;
            _CrdbAmountNet = 0;
            _CrdbRoundAmount = 0;
            _CrdbVouDate = "";
            _Particulars = "";
            _Amount = 0;
            _ClearVouNo = 0;
            _CrdbPreSelectedAmount = 0;
            _CrdbVouNoForSelected = 0;
            _IDForSelected = "";
            _DetailIDForSelected = "";
            _CrdbBillAmountForSelected = 0;
            _CrdbTextForSelected = "";
        }
         public override void DoValidate()
        {
            try
            {
                if (CrdbId == "")
                    ValidationMessages.Add("Please enter the Account Name.");
                if (CrdbAmountNet == 0)
                    ValidationMessages.Add("Invalid Amount");
                bool retValue = General.CheckDates(CrdbVouDate, CrdbVouDate);
                if (retValue == false)
                {
                    ValidationMessages.Add("Please Check Date...");
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
         
        }

         public override bool CanBeDeleted()
         {
             bool bRetValue = true;
             try
             {
                 if (_ClearVouNo != 0)
                     bRetValue = false;
             }
             catch (Exception ex)
             {
                 Log.WriteException(ex);
             }             
             return bRetValue;
         }


        #endregion

         #region Public Methods
         public DataTable GetOverviewData()
         {
             DBDebitNoteAmount dbnote = new DBDebitNoteAmount();
             return dbnote.GetOverviewData(_CrdbVouType);
         }

         public int GetAndUpdateDNNumber(string voucherseries)
         {
             int vouno = 0;
             try
             {
                 DBGetVouNumbers dbno = new DBGetVouNumbers();
                 vouno = dbno.GetDebitNote(voucherseries);
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
                 DBDebitNoteAmount dbcrdb = new DBDebitNoteAmount();
                 drow = dbcrdb.ReadDetailsByID(Id);

                 if (drow != null)
                 {

                     CrdbId = drow["ID"].ToString();                 
                     if (drow["VoucherNumber"] != DBNull.Value)
                         CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                     if (drow["VoucherDate"] != DBNull.Value)
                         CrdbVouDate = drow["VoucherDate"].ToString();
                     if (drow["Narration"] != DBNull.Value)
                         CrdbNarration = drow["Narration"].ToString();
                     if (drow["AmountNet"] != DBNull.Value)
                         CrdbAmountNet = Convert.ToDouble(drow["AmountNet"].ToString());
                     if (drow["Amount"] != DBNull.Value)
                         CrdbAmount = Convert.ToDouble(drow["Amount"].ToString());
                     if (drow["DiscountAmount"] != DBNull.Value)
                         CrdbDiscAmt = Convert.ToDouble(drow["DiscountAmount"].ToString());
                     if (drow["DiscountPer"] != DBNull.Value)
                         CrdbDiscPer = Convert.ToDouble(drow["DiscountPer"].ToString());
                     if (drow["RoundingAmount"] != DBNull.Value)
                         CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                     if (drow["Vat5"] != DBNull.Value)
                         CrdbVat5 = Convert.ToDouble(drow["Vat5"].ToString());
                     if (drow["Vat12point5"] != DBNull.Value)
                         CrdbVat12point5 = Convert.ToDouble(drow["Vat12point5"].ToString());
                     if (drow["AccountID"] != DBNull.Value)
                         AccountID = drow["AccountID"].ToString();
                     if (drow["ClearedInVoucherNumber"] != DBNull.Value)
                     {
                         ClearVouNo = Convert.ToInt32(drow["ClearedInVoucherNumber"].ToString());
                         ClearVouType = drow["ClearedInVoucherType"].ToString();
                     }

                 }
             }
             catch (Exception ex)
             {
                 Log.WriteException(ex);
             }                
                 return retValue; 
         }

        public DataRow ReadDetailsByVouNumber(int vouno)
        {
           
            DataRow drow = null;
            try
            {
                DBDebitNoteAmount dbcrdb = new DBDebitNoteAmount();
                drow = dbcrdb.ReadDetailsByVouNumber(vouno);

                if (drow != null)
                {

                    CrdbId = drow["CRDBID"].ToString();
                    Id = CrdbId;                 
                    if (drow["VoucherNumber"] != DBNull.Value)
                        CrdbVouNo = Convert.ToInt32(drow["VoucherNumber"].ToString());
                    if (drow["VoucherDate"] != DBNull.Value)
                        CrdbVouDate = drow["VoucherDate"].ToString();
                    if (drow["Narration"] != DBNull.Value)
                        CrdbNarration = drow["Narration"].ToString();
                    if (drow["AmountNet"] != DBNull.Value)
                        CrdbAmountNet = Convert.ToDouble(drow["AmountNet"].ToString());
                    if (drow["Amount"] != DBNull.Value)
                        CrdbAmount = Convert.ToDouble(drow["Amount"].ToString());
                    if (drow["DiscountAmount"] != DBNull.Value)
                        CrdbDiscAmt = Convert.ToDouble(drow["DiscountAmount"].ToString());
                    if (drow["DiscountPer"] != DBNull.Value)
                        CrdbDiscPer = Convert.ToDouble(drow["DiscountPer"].ToString());
                    if (drow["RoundingAmount"] != DBNull.Value)
                        CrdbRoundAmount = Convert.ToDouble(drow["RoundingAmount"].ToString());
                    if (drow["Vat5"] != DBNull.Value)
                        CrdbVat5 = Convert.ToDouble(drow["Vat5"].ToString());
                    if (drow["Vat12point5"] != DBNull.Value)
                        CrdbVat12point5 = Convert.ToDouble(drow["Vat12point5"].ToString());
                    if (drow["AccountID"] != DBNull.Value)
                        AccountID = drow["AccountID"].ToString();
                    if (drow["ClearedInVoucherNumber"] != DBNull.Value)
                    {
                        ClearVouNo = Convert.ToInt32(drow["ClearedInVoucherNumber"].ToString());
                        ClearVouType = drow["ClearedInVoucherType"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

            return drow;


        }

         public  DataTable ReadParticularsByID()
         {
             DataTable dt = null;
             try
             {
                 DBDebitNoteAmount dbcrdb = new DBDebitNoteAmount();
                 dt = dbcrdb.ReadParticularById(_CrdbId);
             }
             catch (Exception ex)
             {
                 Log.WriteException(ex);
             }
             return dt;

         }

         public int AddDetails()
         {
             DBDebitNoteAmount dbcrdb = new DBDebitNoteAmount();
              return dbcrdb.AddDetails(Id,CrdbId,CrdbNarration,CrdbVouType,CrdbVouNo,CrdbVouDate,CrdbAmountNet,
                 CrdbDiscPer,CrdbDiscAmt,CrdbAmount,CrdbVat5,CrdbVat12point5,CrdbRoundAmount,CreatedBy,CreatedDate,CreatedTime);
         }
          public int AddDetailsForSelected()
         {
             DBDebitNoteAmount dbcrdb = new DBDebitNoteAmount();
              return dbcrdb.AddDetails(IDForSelected ,CrdbId,CrdbNarration,CrdbVouType,CrdbVouNoForSelected ,CrdbVouDate,CrdbBillAmountForSelected,
                 0, 0, CrdbBillAmountForSelected, 0,0,0, CreatedBy, CreatedDate, CreatedTime);
         }
         public bool UpdateDetails()
         {
             DBDebitNoteAmount dbcrdb = new DBDebitNoteAmount();
             return dbcrdb.UpdateDetails(Id, CrdbId, CrdbNarration, CrdbVouType, CrdbVouNo, CrdbVouDate, CrdbAmountNet,
                 CrdbDiscPer, CrdbDiscAmt, CrdbAmount, CrdbVat5, CrdbVat12point5, CrdbRoundAmount,ModifiedBy,ModifiedDate,ModifiedTime);
         }

         public bool DeleteDetails()
         {
             DBDebitNoteAmount dbcrdb = new DBDebitNoteAmount();
             return dbcrdb.DeleteDetails(Id);
         }       
         public bool DeletePreviousRecords()
         {
             DBDebitNoteAmount dbcrdb = new DBDebitNoteAmount();
             return dbcrdb.DeleteParticulars(Id);
         }

         #endregion



        public bool AddParticularsDetails()
         {
             DBDebitNoteAmount dbamt = new DBDebitNoteAmount();
             return dbamt.AddParticularsDetails(IntID, Particulars, Amount, DetailId,SerialNumber);
         }
        public bool AddParticularsDetailsForSelected()
        {
            DBDebitNoteAmount dbamt = new DBDebitNoteAmount();
            return dbamt.AddParticularsDetails(IDForSelectedDNA, Particulars, CrdbBillAmountForSelected, DetailId, SerialNumber);
        }
        public bool DeleteParticulars()
         {
             DBDebitNoteAmount dbcrdb = new DBDebitNoteAmount();
             return dbcrdb.DeleteParticulars(Id);
         }
        public void GetLastRecord()
        {
            DataRow dr;
            try
            {
                DBDebitNoteAmount dbs = new DBDebitNoteAmount();
                dr = dbs.GetLastRecord(CrdbVouType, CrdbVouSeries);
                if (dr != null && dr["CRDBID"] != null)
                {

                    Id = dr["CRDBID"].ToString();

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }

        }

        public int GetLastVoucherNumber(string vouType, string vouSeries)
        {
            DataRow dr;
            int lastvouno = 0;
            try
            {
                DBDebitNoteAmount dbs = new DBDebitNoteAmount();
                dr = dbs.GetLastVoucherNumber(vouType, vouSeries);
                if (dr != null)
                {

                    lastvouno = Convert.ToInt32(dr["VoucherNumber"].ToString());

                }

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return lastvouno;
        }

        public DataRow GetFirstRecord()
        {
            DataRow dr = null;
            try
            {
                DBDebitNoteAmount dbs = new DBDebitNoteAmount();
                dr = dbs.GetFirstRecord(CrdbVouType, CrdbVouSeries);
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return dr;
        }
    }
}
