using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using PharmaSYSRetailPlus.InterfaceLayer.CommonControls;
using PrintDataGrid;
using System.IO;
using PharmaSYSRetailPlus.InterfaceLayer.Classes;


namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class UclCounterSaleEdit : BaseControl
    {
        #region Declaration
        private SSSale _SSSale;
        public string _MGivenDate;
        public DataTable _BindingSource;
        List<DataGridViewRow> rowCollection;
        public int _MvouNo;
        public double mamt = 0;
        public double mvat5 = 0;
        public double mvatamt5 = 0;
        public double mvat12point5 = 0;
        public double mvatamt12point5 = 0;
        public double mvatper = 0;
        public double mvatamtforZero = 0;
        public int mqty = 0;
       
        public double mpurrate = 0;
        public double mtraderate = 0;
        public double msalerate = 0;
        public double mpakn = 0;
        public double mprate = 0;      
        public double mvatamt = 0;        
        public double mrate = 0;

        public double mTotalProfitInRupees = 0;
        public double mTotalProfitPercentBySaleRate = 0;
        public double mTotalProfitPercentByPurchaseRate = 0;
        public double mdiscamt = 0;

        #endregion

        #region Constructor
        public UclCounterSaleEdit()
        {
            InitializeComponent();
            _SSSale = new SSSale();
            _MGivenDate = DateTime.Now.Date.ToString("yyyyMMdd");
            GivenDate.Value = General.ConvertStringToDateyyyyMMdd(_MGivenDate);
        }
        #endregion

        #region IOverView Members
        public override bool Edit()
        {
            bool retValue = base.Edit();
            try
            {
                _SSSale.Initialise();
                headerLabel1.Text = "COUNTER SALE -> EDIT";
                _MGivenDate = GivenDate.Value.ToString("yyyyMMdd");
                tsBtnSave.Enabled = false;
                tsBtnCancel.Enabled = true;
                tsBtnSearch.Enabled = false;
                tsBtnSavenPrint.Enabled = false;
             
                FillDoctorCombo();
                FillTxtPatientName();
                FillTxtAddress();
                ClearData();
                GetSaleDataProductwise();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }
        public override bool Cancel()
        {
            bool retValue = base.Cancel();
            ClearData();
            return retValue;
        }

        public override bool Delete()
        {
            bool retValue = base.Delete();
            try
            {

                _SSSale.Initialise();
                headerLabel1.Text = "COUNTER SALE -> DELETE";
                _MGivenDate = GivenDate.Value.ToString("yyyyMMdd");
                tsBtnDelete.Enabled = false;
                tsBtnCancel.Enabled = false;
                tsBtnSearch.Enabled = false;
                ClearData();
                GetSaleDataProductwise();

            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return true;
        }
        public override bool ProcessDelete()
        {
            bool retValue = false;

            if (MessageBox.Show("Are you sure you want to delete Sale Information?", General.ApplicationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeleteVoucherSaleProducts();
                retValue = true;
                MessageBox.Show("Successfully Deleted", General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    
                        dr.Cells["Col_Check"].Value = false;
                        dgvReportList.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    
                }
               
              //  dgvReportList.Refresh();
                GetSaleDataProductwise();
                
            }
            ClearData();          
            return retValue;
        }
        public override bool HandleShortcutAction(Keys keyPressed, Keys modifier)
        {
            bool retValue = false;

            try
            {

                if (keyPressed == Keys.Escape)
                {
                    Exit();                    
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
        }

        private void RemoveDeletedRows(List<DataGridViewRow> rowCollection)
        {
            try
            {
                foreach (DataGridViewRow dr in rowCollection)
                {
                    if (Convert.ToBoolean(dr.Cells["Col_Check"].Value) == true)
                        dgvReportList.Rows.Remove(dr);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);                
            }
        }

        private void DeleteVoucherSaleProducts()
        {
            string ifProductDeleted = "N";

            _SSSale.ProfitPercentByPurchaseRate = 0;
            _SSSale.ProfitPercentBySaleRate = 0;
            mTotalProfitPercentByPurchaseRate = 0;
            mTotalProfitPercentBySaleRate = 0;
            mTotalProfitInRupees = 0;

            mamt = 0;
            mvat5 = 0;
            mvatamt5 = 0;
            mvat12point5 = 0;
            mvatamt12point5 = 0;
            mqty = 0;
            mvatper = 0;
            mvatamtforZero = 0;

            double totalsale = 0;
            double totalpur = 0;
            double totalvat = 0;
            double totaldisc = 0;

            mpurrate = 0;
            mtraderate = 0;
            msalerate = 0;
            mpakn = 0;
            mprate = 0;           
            mvatamt = 0;           
            mrate = 0;

            double mtempamt = 0;
            rowCollection = new List<DataGridViewRow>();
            try
            {
                int.TryParse(dgvReportList.Rows[0].Cells["Col_VoucherNumber"].Value.ToString(), out _MvouNo);
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    if (Convert.ToInt32(dr.Cells["Col_VoucherNumber"].Value.ToString()) == _MvouNo)
                    {
                        _SSSale.Id = dr.Cells["Col_MasterSaleID"].Value.ToString();

                        if (Convert.ToBoolean(dr.Cells["Col_Check"].Value) == true)
                            ifProductDeleted = "Y";
                        else
                        {
                            mtempamt = 0;
                            if (dr.Cells["Col_Amount"] != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                                mtempamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mamt = mtempamt;

                            mtempamt = 0;
                            if (dr.Cells["Col_Vatper"] != null && dr.Cells["Col_Vatper"].Value.ToString() != "")
                                mtempamt = Convert.ToDouble(dr.Cells["Col_Vatper"].Value.ToString());
                            mvatper = mtempamt;

                            mtempamt = 0;
                            if (dr.Cells["Col_VatAmount"] != null && dr.Cells["Col_VatAmount"].Value.ToString() != "")
                                mtempamt = Convert.ToDouble(dr.Cells["Col_VatAmount"].Value.ToString());
                            if (mvatper == 5)
                            {
                                mvat5 += mtempamt;
                                mvatamt5 += mamt;
                            }
                            else if (mvatper == 12.5)
                            {
                                mvat12point5 += mtempamt;
                                mvatamt12point5 += mamt;
                            }
                            else mvatamtforZero += mamt;

                            if (dr.Cells["Col_UOM"].Value != null)
                                double.TryParse(dr.Cells["Col_UOM"].Value.ToString(), out mpakn);                            
                            if (dr.Cells["Col_PurchaseRate"].Value != null)
                                double.TryParse(dr.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            _SSSale.PurchaseRate = mpurrate;
                            totalpur += mpurrate;
                           
                            if (dr.Cells["Col_TradeRate"].Value != null)
                                double.TryParse(dr.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                            
                            _SSSale.TradeRate = mtraderate;
                            double.TryParse(dr.Cells["Col_Rate"].Value.ToString().Trim(), out msalerate);
                            double vatontrrate = Math.Round((mtraderate * mvatper) / 100, 2);
                            totalvat += vatontrrate;
                            totalsale += msalerate;
                            mdiscamt = 0;
                            if (dr.Cells["Col_MySpecialDiscountAmount"].Value != null && dr.Cells["Col_MySpecialDiscountAmount"].Value.ToString() != string.Empty)
                                mdiscamt = Convert.ToDouble(dr.Cells["Col_MySpecialDiscountAmount"].Value.ToString());
                            totaldisc += mdiscamt;
                            mqty = Convert.ToInt32(dr.Cells["Col_Quantity"].Value.ToString());
                            
                            //_SSSale.ProfitPercentBySaleRate = Math.Round(((msalerate - mdiscamt ) - (mpurrate + mvatamt)) / (msalerate - mdiscamt), 4);
                            //_SSSale.ProfitPercentByPurchaseRate = Math.Round(((msalerate - mdiscamt) - (mpurrate + mvatamt)) / (mpurrate + mvatamt), 4);
                            //mTotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
                            //mTotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
                            _SSSale.ProfitInRupees = Math.Round((((msalerate) - (mpurrate + mvatamt)) / mpakn) * mqty, 2);
                            mTotalProfitInRupees += _SSSale.ProfitInRupees;
                        }
                        rowCollection.Add(dr);
                    }
                    else
                    {
                        if (ifProductDeleted == "Y" || ifProductDeleted == "N")
                        {
                            mTotalProfitPercentBySaleRate = Math.Round(((totalsale ) - (totalpur + totalvat)) / (totalsale ), 4);
                            mTotalProfitPercentByPurchaseRate = Math.Round(((totalsale) - (totalpur + totalvat)) / (totalpur + totalvat), 4);
                            UpdateData(rowCollection);
                        }
                        int.TryParse(dr.Cells["Col_VoucherNumber"].Value.ToString(), out _MvouNo);
                        ifProductDeleted = "N";
                        mamt = 0;
                        mvat5 = 0;
                        mvatamt5 = 0;
                        mvat12point5 = 0;
                        mvatamt12point5 = 0;
                        mqty = 0;
                        mvatper = 0;
                        mvatamtforZero = 0;
                        totalsale = 0;
                        totalpur = 0;
                        totalvat = 0;
                        totaldisc = 0;
                        mTotalProfitPercentByPurchaseRate = 0;
                        mTotalProfitPercentBySaleRate = 0;
                        mTotalProfitInRupees = 0;
                        //if (Convert.ToBoolean(dr.Cells["Col_Check"].Value) == true)
                        //    ifProductDeleted = "Y";
                        //else
                        //    ifProductDeleted = "N";



                        _SSSale.Id = dr.Cells["Col_ID"].Value.ToString();

                        if (Convert.ToBoolean(dr.Cells["Col_Check"].Value) == true)
                            ifProductDeleted = "Y";
                        else
                        {
                            mtempamt = 0;
                            if (dr.Cells["Col_Amount"] != null && dr.Cells["Col_Amount"].Value.ToString() != "")
                                mtempamt = Convert.ToDouble(dr.Cells["Col_Amount"].Value.ToString());
                            mamt = mtempamt;

                            mtempamt = 0;
                            if (dr.Cells["Col_Vatper"] != null && dr.Cells["Col_Vatper"].Value.ToString() != "")
                                mtempamt = Convert.ToDouble(dr.Cells["Col_Vatper"].Value.ToString());
                            mvatper = mtempamt;

                            mtempamt = 0;
                            if (dr.Cells["Col_VatAmount"] != null && dr.Cells["Col_VatAmount"].Value.ToString() != "")
                                mtempamt = Convert.ToDouble(dr.Cells["Col_VatAmount"].Value.ToString());
                            if (mvatper == 5)
                            {
                                mvat5 += mtempamt;
                                mvatamt5 += mamt;
                            }
                            else if (mvatper == 12.5)
                            {
                                mvat12point5 += mtempamt;
                                mvatamt12point5 += mamt;
                            }
                            else mvatamtforZero += mamt;

                            if (dr.Cells["Col_UOM"].Value != null)
                                double.TryParse(dr.Cells["Col_UOM"].Value.ToString(), out mpakn);
                            if (dr.Cells["Col_PurchaseRate"].Value != null)
                                double.TryParse(dr.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            _SSSale.PurchaseRate = mpurrate;
                            totalpur += mpurrate;

                            if (dr.Cells["Col_TradeRate"].Value != null)
                                double.TryParse(dr.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);

                            _SSSale.TradeRate = mtraderate;
                            double.TryParse(dr.Cells["Col_Rate"].Value.ToString().Trim(), out msalerate);
                            double vatontrrate = Math.Round((mtraderate * mvatper) / 100, 2);
                            totalvat += vatontrrate;
                            totalsale += msalerate;
                            mdiscamt = 0;
                            if (dr.Cells["Col_MySpecialDiscountAmount"].Value != null && dr.Cells["Col_MySpecialDiscountAmount"].Value.ToString() != string.Empty)
                                mdiscamt = Convert.ToDouble(dr.Cells["Col_MySpecialDiscountAmount"].Value.ToString());
                            totaldisc += mdiscamt;
                            mqty = Convert.ToInt32(dr.Cells["Col_Quantity"].Value.ToString());

                            //_SSSale.ProfitPercentBySaleRate = Math.Round(((msalerate - mdiscamt ) - (mpurrate + mvatamt)) / (msalerate - mdiscamt), 4);
                            //_SSSale.ProfitPercentByPurchaseRate = Math.Round(((msalerate - mdiscamt) - (mpurrate + mvatamt)) / (mpurrate + mvatamt), 4);
                            //mTotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
                            //mTotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
                            _SSSale.ProfitInRupees = Math.Round((((msalerate) - (mpurrate + mvatamt)) / mpakn) * mqty, 2);
                            mTotalProfitInRupees += _SSSale.ProfitInRupees;


                        }

                        rowCollection = new List<DataGridViewRow>();
                        rowCollection.Add(dr);
                    }
                }
                if (rowCollection.Count > 0)
                    UpdateData(rowCollection);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);               
            }
        }

        private void UpdateData(List<DataGridViewRow> rowCollection)
        {
            bool retValue = false;
            string mstockID = "";
            string mproductID = "";
            string mdetailSaleID = "";
            string mmasterSaleID = "";
            bool ifchecked = false;

            LockTable.LockTablesForSale();
            General.BeginTransaction();

            foreach (DataGridViewRow prodrow in rowCollection)
            {
                if (prodrow.Cells["Col_ID"] != null)
                    mdetailSaleID = prodrow.Cells["Col_ID"].Value.ToString();
                if (prodrow.Cells["Col_ProductID"] != null)
                    mproductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                if (prodrow.Cells["Col_StockId"] != null)
                    mstockID = prodrow.Cells["Col_StockId"].Value.ToString();
                if (prodrow.Cells["Col_MasterSaleID"] != null)
                    mmasterSaleID = prodrow.Cells["Col_MasterSaleID"].Value.ToString();
                if (prodrow.Cells["Col_Quantity"] != null)
                    mqty = Convert.ToInt32(prodrow.Cells["Col_Quantity"].Value.ToString());
                ifchecked = Convert.ToBoolean(prodrow.Cells["Col_Check"].Value.ToString());
                if (ifchecked)
                {
                    retValue = _SSSale.UpdateVoucherSaleDeleteDetails(mdetailSaleID);
                    if (retValue)
                        retValue = _SSSale.UpdateVoucherSaleUpdateStock(mstockID, mqty);
                    else
                        break;
                    if (retValue)
                        retValue = _SSSale.UpdateVoucherSaleUpdateMasterProduct(mproductID, mqty);
                    else
                        break;
                }

            }
            //if (retValue)
            //{
            if (mamt <= 0)
                retValue = _SSSale.UpdateVoucherSaleDeleteMaster(_SSSale.Id);
            else
            {
                 retValue = _SSSale.UpdateVoucherSaleUpdateMaster(_SSSale.Id, mvatamt5 + mvatamt12point5 + mvatamtforZero, mvatamt5, mvatamt12point5, mvatamtforZero, mvat5, mvat12point5, mTotalProfitInRupees, mTotalProfitPercentBySaleRate, mTotalProfitPercentByPurchaseRate);
                _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
                _SSSale.CrdbAmtForZeroVAT = mvatamtforZero;
                _SSSale.CrdbVat5 = mvat5;
                _SSSale.CrdbVat12point5 = mvat12point5;
                _SSSale.CrdbAddOn = 0;
                _SSSale.CrdbRoundAmount = 0;
                _SSSale.CrdbDiscAmt = 0;
                _SSSale.CrNoteAmount = 0;
                _SSSale.DbNoteAmount = 0;
                _SSSale.CrdbAmountNet = mvatamt5 + mvatamt12point5 + mvatamtforZero;
                SaveIntblTrnac();
            }
            //}
            if (retValue)
                General.CommitTransaction();
            else
                General.RollbackTransaction();
            LockTable.UnLockTables();
            if (retValue)
            {
                UpdateClosingStockinCache(rowCollection);
                RemoveDeletedRows(rowCollection);
                dgvReportList.Refresh();
                retValue = true;
            }

        }
        private bool UpdateClosingStockinCache(List<DataGridViewRow> rowCollection)
        {
            bool returnVal = true;
            try
            {
                foreach (DataGridViewRow prodrow in rowCollection)
                {
                    if (prodrow.Cells["Col_ProductName"].Value != null &&
                       Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                    {
                        _SSSale.ProductID = prodrow.Cells["Col_ProductID"].Value.ToString();
                      //  PharmaSysRetailPlusCache.RefreshProductData(_SSSale.ProductID);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
                returnVal = false;
            }
            return returnVal;
        }       
        public override bool View()
        {
            bool retValue = base.View();
            try
            {
                _SSSale.Initialise();
                headerLabel1.Text = "COUNTER SALE -> VIEW";
                _MGivenDate = GivenDate.Value.ToString("yyyyMMdd");              
                tsBtnCancel.Enabled = false;
                tsBtnSearch.Enabled = false;
                ClearData();
                GetSaleDataProductwise();  
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        public override bool ClearData()
        {
            ClearControls();
            ClearCheckBoxes();
            return true;
        }
        public override bool Exit()
        {
      
            bool retValue = false;
           
            try
            {               
                retValue = base.Exit();

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            return retValue;
           
        }

        public void ClearCheckBoxes()
        {
            foreach (DataGridViewRow prodrow in dgvReportList.Rows)
            {
                prodrow.Cells["Col_Check"].Value = false;

            }
        }

        private void DeleteRecordsForSelectedNumber()
        {
            try
            {
                if (txtsavecustno.Text != "")
                {
                    rowCollection = new List<DataGridViewRow>();
                    foreach (DataGridViewRow prodrow in dgvReportList.Rows)
                    {
                        if (prodrow.Cells["Col_VoucherNumber"].Value.ToString() == txtsavecustno.Text.ToString())
                        {
                            rowCollection.Add(prodrow);
                        }
                    }

                    foreach (DataGridViewRow prodrow in rowCollection)
                    {
                        dgvReportList.Rows.Remove(prodrow);
                    }

                    rowCollection = new List<DataGridViewRow>();
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void ClearControls()
        {
            rbtCash.Checked = true;
            txtBillAmount2.Text = "0.00";
            txtNetAmount.Text = "0.00";
            txtDiscAmount.Text = "0.00";
            txtDiscPercent.Text = "0.00";
            txtPatientName.SelectedID = "";
            txtPatientName.Text = "";
            txtAddress.SelectedID = "";
            txtAddress.Text = "";          
            mcbDoctor.SelectedID = "";
            txtMobileNumber.Text = "";
            mcbDoctor.Text = "";
            txtDoctorAddress.Text = "";
            txtsavecustno.Text = "";
            pnlFinal.SendToBack();
            pnlFinal.Dock = DockStyle.None;
            dgvReportList.Enabled = true;
            txtOperator.Text = "";

            txtPatientName.Enabled = true;
            txtAddress.Enabled = true;
            mcbDoctor.Enabled = true;         

            if (General.CurrentSetting.MsetSaleCreditSale == "Y")
                rbtCreditStatement.Visible = false;
            else
                rbtCreditStatement.Visible = false;
            if (General.CurrentSetting.MsetSaleAskDiscountinCounterSale == "Y")
            {
                txtDiscPercent.Visible = true;
                txtDiscAmount.Visible = true;
                lblDiscPer.Visible = true;
            }
            else
            {
                txtDiscPercent.Visible = false;
                txtDiscAmount.Visible = false;
                lblDiscPer.Visible = false;
            }
            if (General.CurrentSetting.MsetAskOperatorOtherThanVoucherSale == "Y")
            {
                txtOperator.Visible = true;
                lblOperator.Visible = true;
            }
            else
            {
                txtOperator.Visible = false;
                lblOperator.Visible = false;
            }            

        }
        public override bool IsDetailChanged()
        {            
                return true;
        }
        public override void SetFocus()
        {
            base.SetFocus();
            GivenDate.Focus();
        }
        public override bool Save()
        {
            return SaveData(false);
        }

        public override bool SaveAndPrint()
        {
            return SaveData(true);
        }
        private bool SaveData(bool printData)
        {
            bool retValue = false;
            try
            {
                if (txtPatientName.SelectedID != null && txtPatientName.SelectedID != "")
                {
                    if (_SSSale.AccCode == FixAccounts.AccCodeForDebtor)
                    {
                        _SSSale.AccountID = txtPatientName.SelectedID;
                        _SSSale.PatientID = "";
                        _SSSale.SaleSubType = FixAccounts.SubTypeForDebtorSale;
                    }

                    else if (_SSSale.AccCode == FixAccounts.AccCodeForPatient)
                    {
                        _SSSale.PatientID = txtPatientName.SelectedID;
                        _SSSale.AccountID = "";
                        _SSSale.SaleSubType = FixAccounts.SubTypeForPatientSale;
                    }
                    else
                    {
                        _SSSale.PatientID = "";
                        _SSSale.AccountID = "";
                        _SSSale.SaleSubType = FixAccounts.SubTypeForVoucherSale;
                    }
                }
                else
                {
                    _SSSale.SaleSubType = FixAccounts.SubTypeForPatientSale;
                    _SSSale.AccountID = FixAccounts.AccountCash;
                }
            
                    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
            

                System.Text.StringBuilder _errorMessage;

                if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != string.Empty)
                {
                    _SSSale.DocID = mcbDoctor.SelectedID.Trim();
                    _SSSale.DoctorName = mcbDoctor.SeletedItem.ItemData[1];
                    if (mcbDoctor.SeletedItem.ItemData[2] != null && mcbDoctor.SeletedItem.ItemData[2].ToString() != string.Empty)
                        _SSSale.DoctorAddress = mcbDoctor.SeletedItem.ItemData[2];
                }
                else
                {
                    _SSSale.DoctorName = mcbDoctor.Text.ToString();
                    _SSSale.DoctorAddress = txtDoctorAddress.Text.ToString();
                }                

                _SSSale.CrdbVouDate = DateTime.Now.Date.ToString("yyyyMMdd");

                if (txtAddress.Text != null && txtAddress.Text != "")
                    _SSSale.PatientAddress1 = txtAddress.Text;
                _SSSale.PatientShortAddress = _SSSale.PatientAddress1;
                if (txtPatientName.Text != null && txtPatientName.Text.ToString() != "")
                    _SSSale.CrdbName = txtPatientName.Text;
              
                if (txtMobileNumber.Text != null && txtMobileNumber.Text.ToString() != string.Empty)
                    _SSSale.Telephone = txtMobileNumber.Text.ToString();
                

                _SSSale.OperatorID = "";
                _SSSale.OperatorPassword = txtOperator.Text.ToString();


                _SSSale.Validate();


                if (_SSSale.IsValid)
                {
                    LockTable.LockTablesForSale();

                    _SSSale.ModifiedBy = General.CurrentUser.Id;
                    _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(_SSSale.CrdbVouType.Trim(), General.ShopDetail.ShopVoucherSeries);
                    retValue = _SSSale.UpdateDetailsEditCounterSale();
                    DeleteRecordsForSelectedNumber();
                    _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
                    SaveIntblTrnac();
                }
                else
                {
                    LockTable.UnLockTables();
                    _errorMessage = new System.Text.StringBuilder();
                    _errorMessage.Append("Error while validating the input" + Environment.NewLine);
                    foreach (string _message in _SSSale.ValidationMessages)
                    {
                        _errorMessage.AppendFormat("- {0}" + Environment.NewLine, _message);
                    }
                    MessageBox.Show(_errorMessage.ToString(), General.ApplicationTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
            LockTable.UnLockTables();
            return retValue;
        }
        public override bool Print()
        {
            bool retValue = true;
            try
            {
                ConstructPrintGridColumns();
                PrintGrid.Rows.Clear();
                FillPrintGrid();
                if (General.CurrentSetting.MsetPrintSaleBill == "Y")
                    PrintSaleBillPrePrintedPaper();
                else
                    PrintSaleBillPlainPaper();

                ClearData();
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        private void PrintSaleBillPlainPaper()
        {
            PharmaSYSRetailPlus.Printing.PlainPaperPrinter printer = new PharmaSYSRetailPlus.Printing.PlainPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "", _SSSale.Telephone, _SSSale.DoctorName, _SSSale.DoctorAddress, PrintGrid.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount);

        }

        private void PrintSaleBillPrePrintedPaper()
        {

            PharmaSYSRetailPlus.Printing.PrePrintedPaperPrinter printer = new PharmaSYSRetailPlus.Printing.PrePrintedPaperPrinter();
            //  DataGridViewRowCollection rows = new DataGridViewRowCollection(mpPVC1.datagri);
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "",_SSSale.Telephone,  _SSSale.DoctorName, _SSSale.DoctorAddress, PrintGrid.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount);

        }
       
        #endregion IOverView Members

        #region OtherPrivate Methods

        private void ConstructReportColumns()
        {
            try
            {
                dgvReportList.Columns.Clear();
                DataGridViewTextBoxColumn column;

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ID";
                //   column.DataPropertyName = "ID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                DataGridViewCheckBoxColumn columnCheck = new DataGridViewCheckBoxColumn();
                columnCheck.Name = "Col_Check";
                columnCheck.HeaderText = "Check";
                columnCheck.Width = 50;
                columnCheck.Visible = true;
                columnCheck.ReadOnly = false;
                dgvReportList.Columns.Add(columnCheck);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherType";
                //  column.DataPropertyName = "VoucherType";
                column.HeaderText = "TYPE";
                column.Width = 50;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherNumber";
                //   column.DataPropertyName = "VoucherNumber";
                column.HeaderText = "NUMBER";
                column.Width = 60;
                column.ReadOnly = true;
                column.ValueType = typeof(int);
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                //    column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                //column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 170;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);
                //2
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                //column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.Width = 50;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                //  column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 60;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Batch";
                //  column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 90;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                //    column.DataPropertyName = "Quantity";
                column.HeaderText = "Quantity";
                column.Width = 70;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Rate";
                //     column.DataPropertyName = "SaleRate";
                column.HeaderText = "SaleRate";
                column.Width = 90;
                column.ReadOnly = true;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                //   column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 100;
                column.ReadOnly = true;
                column.DefaultCellStyle.Format = "N2";
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;     
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VatPer";
                //  column.DataPropertyName = "Amount";
                column.HeaderText = "VatPer";
                column.Width = 100;
                column.ReadOnly = true;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VatAmount";
                //  column.DataPropertyName = "Amount";
                column.HeaderText = "VatAmount";
                column.Width = 100;
                column.ReadOnly = true;
                column.Visible = false;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockId";
                //  column.DataPropertyName = "AccCode";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MasterSaleID";
                //  column.DataPropertyName = "AccCode";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_AccCode";
                //  column.DataPropertyName = "AccCode";
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VoucherSubType";
                //     column.DataPropertyName = "VoucherSubType";
                column.Visible = false;
                column.Width = 50;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                //     column.DataPropertyName = "VoucherSubType";
                column.Visible = false;
                column.Width = 50;
                dgvReportList.Columns.Add(column);
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                //     column.DataPropertyName = "VoucherSubType";
                column.Visible = false;
                column.Width = 50;
                dgvReportList.Columns.Add(column);
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MySpecialDiscountAmount";
                //     column.DataPropertyName = "VoucherSubType";
                column.Visible = false;
                column.Width = 50;
                dgvReportList.Columns.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void ConstructPrintGridColumns()
        {
            DataGridViewTextBoxColumn column;
            PrintGrid.ColumnsMain.Clear();
            //0
            try
            {
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductID";
                column.DataPropertyName = "ProductID";
                column.HeaderText = "ProductID";
                column.Width = 0;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //1
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProductName";
                column.DataPropertyName = "ProdName";
                column.HeaderText = "ProductName";
                column.Width = 220;
                column.ReadOnly = false;
                PrintGrid.ColumnsMain.Add(column);
                //2 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_UOM";
                column.DataPropertyName = "ProdLoosePack";
                column.HeaderText = "UOM";
                column.ReadOnly = true;
                column.Width = 50;
                PrintGrid.ColumnsMain.Add(column);
                //3
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Pack";
                column.DataPropertyName = "ProdPack";
                column.HeaderText = "Pack";
                column.Width = 70;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //4
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdCompShortName";
                column.DataPropertyName = "ProdCompShortName";
                column.HeaderText = "CO";
                column.Width = 50;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //5
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Shelf";
                column.DataPropertyName = "ShelfCode";
                column.HeaderText = "Shelf";
                column.Width = 60;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //6
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATPer";
                column.DataPropertyName = "ProdVATPercent";
                column.HeaderText = "VAT%";
                column.Width = 60;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = true;
                PrintGrid.ColumnsMain.Add(column);
                //7
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchNumber";
                column.DataPropertyName = "BatchNumber";
                column.HeaderText = "Batch";
                column.Width = 100;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //8          
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.DataPropertyName = "Expiry";
                column.HeaderText = "EXP";
                column.Width = 70;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //9
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MRP";
                column.DataPropertyName = "MRP";
                column.HeaderText = "MRP";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //10
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_SaleRate";
                column.DataPropertyName = "SaleRate";
                column.HeaderText = "S.Rate";
                column.Width = 80;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //11
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Quantity";
                column.DataPropertyName = "Quantity";
                column.HeaderText = "QTY";
                column.Width = 50;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = false;
                PrintGrid.ColumnsMain.Add(column);
                //12
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Amount";
                column.DataPropertyName = "Amount";
                column.HeaderText = "Amount";
                column.Width = 110;
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.ReadOnly = true;
                PrintGrid.ColumnsMain.Add(column);
                //13            // temp storage columns 
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ClosingStock";
                column.DataPropertyName = "ProdClosingStock";
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //14         
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_PurchaseRate";
                column.DataPropertyName = "PurchaseRate";
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //15
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ExpiryDate";
                column.DataPropertyName = "ExpiryDate";
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //16
                column = new DataGridViewTextBoxColumn();
                column.Name = "Old_Quantity";
                column.DataPropertyName = "OldQuantity";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //17
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_TradeRate";
                column.DataPropertyName = "TradeRate";
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //18
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_BatchStock";
                column.DataPropertyName = "ClosingStock";
                column.HeaderText = "Qty";
                column.Width = 50;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //19
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_VATAmount";
                column.DataPropertyName = "VATAmount";
                column.HeaderText = "VAT Amount";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
                //20
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "IfSaleDisc";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_StockID";
                column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CustID";
                //  column.DataPropertyName = "StockID";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_LastStockID";
                column.DataPropertyName = "LastStockID";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }

        private void FillDoctorCombo()
        {
            try
            {
                mcbDoctor.SelectedID = null;
                mcbDoctor.SourceDataString = new string[4] { "DocID", "DocName", "DocAddress", "DocShortNameAddress" };
                mcbDoctor.ColumnWidth = new string[4] { "0", "200", "300", "0" };
                mcbDoctor.ValueColumnNo = 0;
                mcbDoctor.UserControlToShow = new UclDoctor();
                Doctor _Doctor = new Doctor();
                DataTable dtabled = _Doctor.GetSSDoctorsList();
                mcbDoctor.FillData(dtabled);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillTxtPatientName()
        {
            try
            {
                txtPatientName.SelectedID = null;
                txtPatientName.SourceDataString = new string[8] { "PatientID", "AccCode", "PatientName", "PatientAddress1", "PatientAddress2", "ShortNameAddress", "DoctorID", "AccTransactionType" };
                txtPatientName.ColumnWidth = new string[8] { "0", "50", "200", "200", "200", "0", "0", "0" };
                txtPatientName.DisplayColumnNo = 2;
                txtPatientName.ValueColumnNo = 0;
                txtPatientName.UserControlToShow = new UclPatient();
                Patient _Party = new Patient();
                DataTable dtable = _Party.GetOverviewDataForCounterSale();
                txtPatientName.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillPrintGrid()
        {
            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {
                if (dr.Cells[0].Value != null && dr.Cells["Col_Quantity"].Value != null)
                {
                    int printgridindex = PrintGrid.Rows.Add();
                    for (int i = 0; i < 20; i++)
                    {
                        if (dr.Cells[i].Value != null)
                            PrintGrid.Rows[printgridindex].Cells[i].Value = dr.Cells[i].Value;

                    }
                }
            }
        }         
        private void FillTxtAddress()
        {
            try
            {
                txtAddress.SelectedID = null;
                txtAddress.SourceDataString = new string[2] { "AreaID", "AreaName" };
                txtAddress.ColumnWidth = new string[2] { "0", "200" };
                txtAddress.ValueColumnNo = 0;
                txtAddress.UserControlToShow = new UclArea();
                Area _Area = new Area();
                DataTable dtable = _Area.GetOverviewData();
                txtAddress.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void BindReportGrid()
        {
            try
            {
                int _RowIndex;
                DataGridViewRow currentdr;

                //double mtotamount = 0;
                //int mtotquantity = 0;              

                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (Convert.ToInt32(dr["VoucherDate"].ToString()) == Convert.ToInt32(_MGivenDate))
                    {

                        double mamt = 0;

                        _RowIndex = dgvReportList.Rows.Add();
                        currentdr = dgvReportList.Rows[_RowIndex];
                        currentdr.Cells["Col_ID"].Value = dr["DetailSaleID"].ToString();
                        currentdr.Cells["Col_Check"].Value = false;
                        currentdr.Cells["Col_VoucherType"].Value = dr["VoucherType"].ToString();
                        // currentdr.Cells["Col_VoucherSubType"].Value = dr["VoucherSubType"].ToString();
                        currentdr.Cells["Col_VoucherNumber"].Value = dr["VoucherNumber"].ToString();
                        //   currentdr.Cells["Col_VoucherDate"].Value = General.GetDateInShortDateFormat(dr["VoucherDate"].ToString());
                        currentdr.Cells["Col_ProductID"].Value = dr["ProductID"].ToString();
                        currentdr.Cells["Col_ProductName"].Value = dr["ProdName"].ToString();
                        currentdr.Cells["Col_UOM"].Value = dr["ProdLoosePack"].ToString();
                        currentdr.Cells["Col_Pack"].Value = dr["ProdPack"].ToString();
                        currentdr.Cells["Col_Quantity"].Value = dr["Quantity"].ToString();
                        currentdr.Cells["Col_Batch"].Value = dr["BatchNumber"].ToString();
                        mamt = Convert.ToDouble(dr["SaleRate"].ToString());
                        currentdr.Cells["Col_Rate"].Value = mamt.ToString("#0.00");
                        mamt = Convert.ToDouble(dr["Amount"].ToString());
                        currentdr.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
                        mamt = Convert.ToDouble(dr["VatPer"].ToString());
                        currentdr.Cells["Col_VatPer"].Value = mamt.ToString("#0.00");
                        mamt = Convert.ToDouble(dr["VatAmount"].ToString());
                        currentdr.Cells["Col_VatAmount"].Value = mamt.ToString("#0.00");
                        currentdr.Cells["Col_StockID"].Value = dr["StockID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        currentdr.Cells["Col_PurchaseRate"].Value = dr["PurchaseRate"].ToString();
                        currentdr.Cells["Col_TradeRate"].Value = dr["TradeRate"].ToString();
                        if (dr["MySpecialDiscountAmount"] != DBNull.Value)
                            currentdr.Cells["Col_MySpecialDiscountamount"].Value = dr["MySpecialDiscountAmount"].ToString();
                        else
                            currentdr.Cells["Col_MySpecialDiscountAmount"].Value = string.Empty;
                        // mtotamount += Convert.ToDouble(dr["Amount"].ToString());
                        // mtotquantity += Convert.ToInt32(dr["Quantity"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                Log.WriteException(ex);
            }


        }

        private void InitializeReportGrid()
        {
            ConstructReportColumns();
            dgvReportList.Columns["Col_ID"].Visible = false;
            if (_Mode == OperationMode.View)
                dgvReportList.Columns["Col_Check"].Visible = false;
            else
                dgvReportList.Columns["Col_Check"].Visible = true;
            //  dgvReportList.InitializeColumnContextMenu();
        }

        private void GetSaleDataProductwise()
        {

            DataTable dtable = new DataTable();
            dtable = _SSSale.GetVoucherSaleDataData(_MGivenDate);
            _BindingSource = dtable;
            InitializeReportGrid();
            if (dtable != null)
                BindReportGrid();
        }

        private void CheckforSelection()
        {
            _MvouNo = 0;
            if (Convert.ToBoolean(dgvReportList.CurrentRow.Cells["Col_Check"].Value) == true)
            {
                if (_Mode == OperationMode.Edit)
                {
                    pnlFinal.BringToFront();
                    pnlFinal.Dock = DockStyle.Bottom;
                    dgvReportList.Enabled = false;
                    _MvouNo = Convert.ToInt32(dgvReportList.CurrentRow.Cells["Col_VoucherNumber"].Value.ToString());
                    txtsavecustno.Text = _MvouNo.ToString();
                    FillSaleData();
                    gbsaletype.Enabled = true;
                    txtPatientName.Focus();
                }
                else
                    if (_Mode == OperationMode.Delete)
                        tsBtnDelete.Enabled = true;

            }
        }

        private void FillSaleData()
        {
            _SSSale.CrdbVouType = FixAccounts.VoucherTypeForVoucherSale;
            _SSSale.CrdbVouNo = _MvouNo;
            _SSSale.SaleSubType = FixAccounts.SubTypeForVoucherSale;
            _SSSale.ReadDetailsByVouNumberCounterSale();
            txtNetAmount.Text = _SSSale.CrdbAmountNet.ToString("#0.00");
            txtDiscPercent.Text = _SSSale.CrdbDiscPer.ToString("#0.00");
            txtDiscAmount.Text = _SSSale.CrdbDiscAmt.ToString("#0.00");
            txtBillAmount2.Text = _SSSale.CrdbBillAmount.ToString("#0.00");
        }

        private bool SaveIntblTrnac()
        {

            bool retValue = false;
            double mamtforzerovat = _SSSale.CrdbAmtForZeroVAT;
            double mvat5per = _SSSale.CrdbVat5;
            double mvat12point5per = _SSSale.CrdbVat12point5;
            double maddon = _SSSale.CrdbAddOn;
            double mround = _SSSale.CrdbRoundAmount;
            double mdiscamount = _SSSale.CrdbDiscAmt;
            double mcreditnoteamt = _SSSale.CrNoteAmount;
            double mdebitnoteamt = _SSSale.DbNoteAmount;
            double mbillamount = _SSSale.CrdbAmountNet;
            double mdebit = mdebit = Math.Round(mbillamount - Math.Round(mvat5per,2) - Math.Round(mvat12point5per,2) + mdiscamount - maddon + mround - mamtforzerovat + mcreditnoteamt - mdebitnoteamt, 2);
            try
            {               
                _SSSale.CrdbAmountNet = mbillamount;  

                if (mamtforzerovat > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVATZeroSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mamtforzerovat;
                    retValue = _SSSale.AddVoucherIntblTrnac();

                }

                if ( mvat5per > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput5Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(mvat5per,2);
                    retValue = _SSSale.AddVoucherIntblTrnac();

                }
                if (mvat12point5per > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput12point5Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(mvat12point5per,2);
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (maddon > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountAddonSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = maddon;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (mround < 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = (mround * -1);
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (mround > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountRoundoffSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mround;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }

                if (mdiscamount > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountCashDiscountSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mdiscamount;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }

                if (mcreditnoteamt > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountSalesReturn;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mcreditnoteamt;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (mdebitnoteamt > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountDebitNoteSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mdebitnoteamt;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (mdebit > 0)
                {

                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.DebitAccount = FixAccounts.AccountCashSale;
                    else
                        _SSSale.DebitAccount = FixAccounts.AccountCashCreditSale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = mdebit;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
                if (mbillamount > 0)
                {
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.DebitAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.DebitAccount = _SSSale.AccountID;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCashSale;
                    else
                        _SSSale.CreditAccount = FixAccounts.AccountCashCreditSale;                   
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = mbillamount;
                    _SSSale.CreditAmount = 0;
                    retValue = _SSSale.AddVoucherIntblTrnac();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
            return retValue;
        }
        #endregion Other Private Methods

        #region Events

        private void dgvReportList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvReportList.CommitEdit(DataGridViewDataErrorContexts.Commit);

            CheckforSelection();
        }

        private void GivenDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _MGivenDate = GivenDate.Value.ToString("yyyyMMdd");
                GetSaleDataProductwise();
                dgvReportList.Focus();

            }
        }

        private void rbtCash_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (rbtCash.Checked)
                    {
                        txtAddress.Enabled = true;
                        txtPatientName.Focus();
                    }
                }
                if (e.KeyCode == Keys.Right)
                    rbtCashCredit.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void rbtCashCredit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (rbtCashCredit.Checked)
                    {
                        txtAddress.Enabled = true;
                        txtPatientName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void rbtCreditCard_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (rbtCreditCard.Checked)
                    {
                        txtAddress.Enabled = true;
                        txtPatientName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void rbtCreditStatement_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (rbtCreditStatement.Checked)
                    {
                        txtAddress.Enabled = true;
                        txtPatientName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void txtPatientName_EnterKeyPressed(object sender, EventArgs e)
        {
            //lblMessage.Text = "";
            try
            {

                if (txtPatientName.SelectedID != null && txtPatientName.SelectedID != "" && txtPatientName.Text.ToString() != txtPatientName.SeletedItem.ItemData[2])
                {
                    txtPatientName.SelectedID = "";                   
                    txtAddress.Focus();
                }
                else
                {
                    if (txtPatientName.SelectedID == null || txtPatientName.SelectedID == "")
                    {
                        txtAddress.Enabled = true;
                        txtAddress.Focus();
                    }
                    else
                    {
                        txtAddress.Text = txtPatientName.SeletedItem.ItemData[3];                       
                        mcbDoctor.SelectedID = txtPatientName.SeletedItem.ItemData[6];
                        _SSSale.AccCode = txtPatientName.SeletedItem.ItemData[1];
                        txtPatientName.Enabled = false;
                        txtAddress.Enabled = false;
                        if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "")
                            mcbDoctor.Enabled = false;
                       
                        txtDiscPercent.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void txtAddress_EnterKeyPressed(object sender, EventArgs e)
        {
            try
            {
                txtMobileNumber.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void txtsavecustno_TextChanged(object sender, EventArgs e)
        {
            _MvouNo = 0;
            if (txtsavecustno.Text != null && txtsavecustno.Text.ToString() != "")
                int.TryParse(txtsavecustno.Text.ToString(), out _MvouNo);
            if (_MvouNo > 0)
                tsBtnSavenPrint.Enabled = true;
            else
                tsBtnSavenPrint.Enabled = false;
        }
        private void txtPatientNameAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbDoctor.Focus();
        }
        #endregion Events

        private void mcbDoctor_ItemAddedEdited(object sender, EventArgs e)
        {
            try
            {
                string selectedId = mcbDoctor.SelectedID;
                FillDoctorCombo();
                mcbDoctor.SelectedID = selectedId;
                if (mcbDoctor.SelectedID != null && mcbDoctor.SelectedID != "" && mcbDoctor.SeletedItem.ItemData[2] != null)
                {
                   // txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2].ToString();
                  //  txtDoctorShortName.Text = mcbDoctor.SeletedItem.ItemData[3].ToString();
                }
            }
            catch (Exception Ex)
            {
                Log.WriteException(Ex);
            }
        }

        private void txtMobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                mcbDoctor.Focus();
            else if (e.KeyCode == Keys.Up)
                txtAddress.Focus();
        }

        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {
            txtDoctorAddress.Focus();
        }

        private void mcbDoctor_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtMobileNumber.Focus();
        }

        private void txtDoctorAddress_UpArrowKeyPressed(object sender, EventArgs e)
        {
            mcbDoctor.Focus();
        }

        private void txtDoctorAddress_EnterKeyPressed(object sender, EventArgs e)
        {
            txtDiscPercent.Focus();
        }

        private void txtAddress_UpArrowKeyPressed(object sender, EventArgs e)
        {
            txtPatientName.Focus();
        }


        private void btnClearDoctor_Click(object sender, EventArgs e)
        {
            mcbDoctor.SelectedID = "";
            mcbDoctor.Text = "";
            txtDoctorAddress.Text = "";
            mcbDoctor.Focus();

        }

        private void btnClearPatient_Click(object sender, EventArgs e)
        {
            txtPatientName.Text = "";
            txtPatientName.SelectedID = "";
            txtAddress.Text = "";
            txtMobileNumber.Text = "";
            txtPatientName.Enabled = true;
            this.txtPatientName.Focus();
        }
    }
}
