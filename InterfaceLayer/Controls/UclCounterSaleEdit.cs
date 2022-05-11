using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PharmaSYSDistributorPlus.Common;
using PharmaSYSDistributorPlus.BusinessLayer;
using PharmaSYSPlus.CommonLibrary;
using PharmaSYSDistributorPlus.InterfaceLayer.CommonControls;
using PrintDataGrid;
using System.IO;
using PharmaSYSDistributorPlus.InterfaceLayer.Classes;


namespace PharmaSYSDistributorPlus.InterfaceLayer
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
      //  double totamt = 0;

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
                txtTotalAmount.Text = "0.00";
                _SSSale.Initialise();
                //  tsBtnDelete.Visible = true;
                tsBtnAdd.Enabled = false;
                tsBtnEdit.Visible = false;
                tsBtnFifth.Visible = false;
                tsBtnFirst.Enabled = false;
                tsBtnLast.Enabled = false;
                tsBtnCancel.Visible = true;
                tsBtnSearch.Visible = false;
                tsBtnPrevious.Enabled = false;
                tsBtnNext.Enabled = false;
                tsBtnExit.Visible = true;
                headerLabel1.Text = "COUNTER SALE -> EDIT";
                _MGivenDate = GivenDate.Value.ToString("yyyyMMdd");
                tsBtnSave.Enabled = false;
                tsBtnCancel.Enabled = true;
                tsBtnSearch.Enabled = false;
                tsBtnSavenPrint.Enabled = false;

                FillDoctorCombo();
                FillTxtPatientName();
                FillTxtAddress();
                FillDoctorAddress();
                ClearData();
                GetSaleDataProductwise(General.ShopDetail.ShopChangeCounterSaleType);
                SetFocus();
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
                tsBtnView.Visible = false;
                tsBtnAdd.Enabled = false;
                tsBtnFirst.Enabled = false;
                tsBtnLast.Enabled = false;
                tsBtnPrevious.Enabled = false;
                tsBtnNext.Enabled = false;
                tsBtnExit.Visible = true;
                headerLabel1.Text = "COUNTER SALE -> DELETE";
                _MGivenDate = GivenDate.Value.ToString("yyyyMMdd");
                if (General.ShopDetail.ShopChangeCounterSaleType == "Y")
                    tsBtnDelete.Enabled = true;
                else
                    tsBtnDelete.Enabled = false;
                tsBtnCancel.Enabled = false;
                tsBtnSearch.Enabled = false;
                ClearData();
                GetSaleDataProductwise(General.ShopDetail.ShopChangeCounterSaleType);

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
                if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
                    DeleteVoucherSaleProducts();
                else
                    DeleteVoucherSaleProductsForSaleType();
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
                GetSaleDataProductwise(General.ShopDetail.ShopChangeCounterSaleType);

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

                    if (_SSSale.CrdbVouType != FixAccounts.VoucherTypeForVoucherSale || (General.ShopDetail.ShopChangeCounterSaleType != "Y" && _SSSale.CrdbVouType == FixAccounts.VoucherTypeForVoucherSale))
                        Exit();
                    else
                        CheckforSelection();
                }
                if (retValue == false)
                {
                    retValue = base.HandleShortcutAction(keyPressed, modifier);
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
                            // vat 5.5
                            if (mvatper == 6)
                            {
                                mvat5 += mtempamt;
                                mvatamt5 += mamt;
                            }
                            else if (mvatper == 13.5)
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
                            mTotalProfitPercentBySaleRate = Math.Round(((totalsale) - (totalpur + totalvat)) / (totalsale), 4);
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
                            // vat 5.5
                            if (mvatper == 6)
                            {
                                mvat5 += mtempamt;
                                mvatamt5 += mamt;
                            }
                            else if (mvatper == 13.5)
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
        private void DeleteVoucherSaleProductsForSaleType()
        {
            rowCollection = new List<DataGridViewRow>();
            try
            {
                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {

                    _SSSale.Id = dr.Cells["Col_MasterSaleID"].Value.ToString();

                    if (Convert.ToBoolean(dr.Cells["Col_Check"].Value) == true)
                    {
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
            {
                if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
                {
                    retValue = _SSSale.UpdateVoucherSaleDeleteMaster(mmasterSaleID);
                    retValue = _SSSale.DeleteDetailsFromtblTrnac(mmasterSaleID);
                }
                else
                    retValue = true;
            }
            else
            {
                if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
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
                else
                    retValue = true;
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
                        //  PharmaSYSDistributorPlusCache.RefreshProductData(_SSSale.ProductID);
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
                tsBtnFifth.Visible = false;
                ClearData();
                GetSaleDataProductwise(General.ShopDetail.ShopChangeCounterSaleType);
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
                tsBtnAdd.Enabled = true;
                tsBtnFirst.Enabled = true;
                tsBtnLast.Enabled = true;
                tsBtnPrevious.Enabled = true;
                tsBtnNext.Enabled = true;
                tsBtnSave.Enabled = true;
                tsBtnCancel.Enabled = false;
                tsBtnSearch.Enabled = true;
                tsBtnSavenPrint.Enabled = true;
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


        private void DeleteRecordsForSelectedProducts()
        {
            try
            {
                rowCollection = new List<DataGridViewRow>();
                foreach (DataGridViewRow prodrow in dgvReportList.Rows)
                {
                    if (Convert.ToBoolean(prodrow.Cells["Col_Check"].Value) == true)
                    {
                        rowCollection.Add(prodrow);
                    }
                }

                foreach (DataGridViewRow prodrow in rowCollection)
                {
                    string vouID = prodrow.Cells["Col_ID"].Value.ToString();
                    _SSSale.UpdateDetailSaleForNewVoucherTypeAndNumber(_SSSale.Id, vouID, _SSSale.CrdbVouType, _SSSale.CrdbVouNo);
                    dgvReportList.Rows.Remove(prodrow);
                }

                rowCollection = new List<DataGridViewRow>();

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

                if (GivenDate.Value != null)
                    _SSSale.CrdbVouDate = GivenDate.Value.ToString("yyyyMMdd");
                else
                    _SSSale.CrdbVouDate = DateTime.Now.Date.ToString("yyyyMMdd");

                _SSSale.CrdbDiscPer = Convert.ToDouble(txtDiscPercent.Text.ToString());
                _SSSale.CrdbDiscAmt = Convert.ToDouble(txtDiscAmount.Text.ToString());
                _SSSale.CrdbAmountNet = Convert.ToDouble(txtBillAmount2.Text.ToString());
                _SSSale.CrdbRoundAmount = Convert.ToDouble(txtRoundAmount.Text.ToString());
                _SSSale.CrdbAmount = Convert.ToDouble(txtNetAmount.Text.ToString());
                _SSSale.CrdbAmountClear = _SSSale.CrdbAmountNet;
                if (txtAddress.Text != null && txtAddress.Text != "")
                    _SSSale.PatientAddress1 = txtAddress.Text;
                _SSSale.PatientShortAddress = _SSSale.PatientAddress1;
                if (txtPatientName.Text != null && txtPatientName.Text.ToString() != "")
                    _SSSale.CrdbName = txtPatientName.Text;

                if (txtMobileNumber.Text != null && txtMobileNumber.Text.ToString() != string.Empty)
                    _SSSale.Telephone = txtMobileNumber.Text.ToString();


                _SSSale.OperatorID = "";
                _SSSale.OperatorPassword = txtOperator.Text.ToString();
                //if (_Mode == OperationMode.Edit || _Mode == OperationMode.OpenAsChild)
                //{
                LockTable.LocktblVoucherNo();
                _SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(_SSSale.CrdbVouType.Trim(), General.ShopDetail.ShopVoucherSeries); //Amar
                //}

                _SSSale.Validate();


                if (_SSSale.IsValid)
                {
                    LockTable.LockTablesForSale();

                    _SSSale.ModifiedBy = General.CurrentUser.Id;
                    _SSSale.ModifiedDate = DateTime.Now.Date.ToString("yyyyMMdd");
                    _SSSale.ModifiedTime = DateTime.Now.ToString("HH:mm:ss");
                    //_SSSale.CrdbVouNo = _SSSale.GetAndUpdateSaleNumber(_SSSale.CrdbVouType.Trim(), General.ShopDetail.ShopVoucherSeries);

                    if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
                    {
                        retValue = _SSSale.UpdateDetailsEditCounterSale();
                        DeleteRecordsForSelectedNumber();
                        _SSSale.DeleteDetailsFromtblTrnac(_SSSale.Id);
                        SaveIntblTrnac();
                        string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");  //Amar
                        PSDialogResult result;
                        result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                        if (result == PSDialogResult.Print)
                            Print(); //End Amar
                    }
                    else
                    {
                        _SSSale.Id = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                        retValue = _SSSale.AddDetails();
                        UpdateDetailsForEditRateCounterSale();
                        DeleteRecordsForSelectedProducts();
                        SaveIntblTrnac();
                        string msgLine2 = _SSSale.CrdbVouType + "  " + _SSSale.CrdbVouNo.ToString("#0");
                        PSDialogResult result;
                        if (printData)
                        {
                            result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OK, PSMessageBoxIcon.Information, PSMessageBoxButtons.OK);
                            Print();
                        }
                        else
                        {

                            result = PSMessageBox.Show("Information has been saved successfully.", msgLine2, General.ApplicationTitle, PSMessageBoxButtons.OKPrint, PSMessageBoxIcon.Information, PSMessageBoxButtons.Print);
                            if (result == PSDialogResult.Print)
                                Print();

                        }
                    }
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

        private void UpdateDetailsForEditRateCounterSale()
        {
            double mrate = 0;
            bool check = false;
            string detailID = "";
            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {
                check = Convert.ToBoolean(dr.Cells["Col_Check"].Value);
                if (check == true && dr.Cells["Col_ID"].Value != null)
                {
                    detailID = dr.Cells["Col_ID"].Value.ToString();
                    mrate = Convert.ToDouble(dr.Cells["Col_Rate"].Value.ToString());
                    _SSSale.UpdateDetailSaleForRateInCounterSale(detailID, mrate, _SSSale.CrdbVouType, _SSSale.CrdbVouNo);
                }
            }
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

        private void FillPrintGrid()
        {
            int colcount = dgvReportList.Columns.Count;
            double srate = 0;
            double uom = 0;
            double rateperunit = 0;
            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {
                if (dr.Cells[0].Value != null && dr.Cells["Col_Quantity"].Value != null)
                {
                    int printgridindex = PrintGrid.Rows.Add();
                    for (int i = 0; i < colcount; i++)
                    {
                        if (dr.Cells[i].Value != null)
                            PrintGrid.Rows[printgridindex].Cells[i].Value = dr.Cells[i].Value;

                    }
                    srate = Convert.ToDouble(dr.Cells["Col_SaleRate"].Value.ToString());
                    uom = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());
                    if (uom == 0)
                        uom = 1;
                    rateperunit = Math.Round(srate / uom);
                    PrintGrid.Rows[printgridindex].Cells["Col_RatePerUnit"].Value = rateperunit.ToString("#0.00");

                }
            }
        }
        private void PrintSaleBillPlainPaper()
        {
            PharmaSYSDistributorPlus.Printing.PlainPaperPrinter printer = new PharmaSYSDistributorPlus.Printing.PlainPaperPrinter();
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "", _SSSale.MobileNumberForSMS, _SSSale.DoctorName, _SSSale.DoctorAddress, PrintGrid.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount, "");

        }

        private void PrintSaleBillPrePrintedPaper()
        {

            PharmaSYSDistributorPlus.Printing.PrePrintedPaperPrinter printer = new PharmaSYSDistributorPlus.Printing.PrePrintedPaperPrinter();
            //  DataGridViewRowCollection rows = new DataGridViewRowCollection(mpPVC1.datagri);
            printer.Print(_SSSale.CrdbVouType, _SSSale.CrdbVouNo.ToString(), _SSSale.CrdbVouDate, _SSSale.ShortName, "", _SSSale.MobileNumberForSMS, _SSSale.DoctorName, _SSSale.DoctorAddress, PrintGrid.Rows, _SSSale.CrdbNarration, _SSSale.CrdbAmountNet, _SSSale.SaleSubType, _SSSale.CrdbDiscAmt, _SSSale.CrNoteAmount, _SSSale.DbNoteAmount, _SSSale.CrdbAmount, _SSSale.PendingAmount, "");

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
                column.Width = 290;
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
                if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
                    column.ReadOnly = true;
                else
                    column.ReadOnly = false;
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
                column.Name = "Col_IfSaleDisc";
                column.DataPropertyName = "ProdIfSaleDisc";
                column.HeaderText = "IfSaleDisc";
                column.Width = 60;
                column.Visible = false;
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
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MySpecialDiscountAmount";
                //     column.DataPropertyName = "VoucherSubType";
                column.Visible = false;
                column.Width = 50;
                dgvReportList.Columns.Add(column);


                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";
                //     column.DataPropertyName = "VoucherSubType";
                column.Visible = false;
                column.Width = 50;
                dgvReportList.Columns.Add(column);
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";
                //     column.DataPropertyName = "VoucherSubType";
                column.Visible = false;
                column.Width = 50;
                dgvReportList.Columns.Add(column);
                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRupees";
                //     column.DataPropertyName = "VoucherSubType";
                column.Visible = false;
                column.Width = 50;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_CompanyShortName";
                column.HeaderText = "Company Name";
                //column.Visible = false;
                column.Width = 110;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Expiry";
                column.HeaderText = "Expiry";
                column.Width = 110;
                column.ReadOnly = true;
                dgvReportList.Columns.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_Time";
                column.HeaderText = "Time";
                column.Width = 80;
                column.ReadOnly = true;
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

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentBySaleRate";
                column.DataPropertyName = "ProfitPercentBySaleRate";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitPercentByPurchaseRate";
                column.DataPropertyName = "ProfitPercentByPurchaseRate";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProfitInRupees";
                column.DataPropertyName = "ProfitInRupees";
                column.Width = 60;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_ProdScheduleDrugCode";
                column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                ////// added new columns 29/3/2015

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_DiscountAmount";
                column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_MySpecialDiscountAmount";
                column.DataPropertyName = "MySpecialDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_IFTempSale";
                //  column.DataPropertyName = "ItemDiscountAmount";
                column.Width = 40;
                column.Visible = false;
                PrintGrid.ColumnsMain.Add(column);

                column = new DataGridViewTextBoxColumn();
                column.Name = "Col_RatePerUnit";
                // column.DataPropertyName = "ProdScheduleDrugCode";
                column.Width = 40;
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
                DataTable dtable = new DataTable();
                //DataTable dtable = _Party.GetOverviewDataForCounterSale();
                if (General.CurrentSetting.MsetSaleOnlyCashSaleInCounterSale == "Y") //Amar
                    dtable = _Party.GetOverviewDataForCounterSaleForOnlyCashSale();
                else
                    dtable = _Party.GetOverviewDataForCounterSale();
                txtPatientName.FillData(dtable);

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void FillTxtAddress()
        {
            try
            {
                txtAddress.SelectedID = null;
                txtAddress.SourceDataString = new string[2] { "AreaID", "AreaName" };
                txtAddress.ColumnWidth = new string[2] { "0", "300" };
                txtAddress.ValueColumnNo = 0;
                txtAddress.UserControlToShow = new UclArea();
                Area _Area = new Area();
                DataTable dtable = _Area.GetOverViewDataForAddress();
                txtAddress.FillData(dtable);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void FillDoctorAddress()
        {
            try
            {
                txtDoctorAddress.SelectedID = null;
                txtDoctorAddress.SourceDataString = new string[2] { "AreaID", "AreaName" };
                txtDoctorAddress.ColumnWidth = new string[2] { "0", "300" };
                txtDoctorAddress.ValueColumnNo = 0;
                txtDoctorAddress.UserControlToShow = new UclArea();
                Area _Area = new Area();
                DataTable dtable = _Area.GetOverViewDataForAddress();
                txtDoctorAddress.FillData(dtable);
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
                double totamt = 0;
                double mamount = 0;
                foreach (DataRow dr in _BindingSource.Rows)
                {
                    if (Convert.ToInt32(dr["VoucherDate"].ToString()) == Convert.ToInt32(_MGivenDate))
                    {

                        double mamt = 0;
                        mamount = 0;
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
                        mamount = mamt;
                        mamt = Convert.ToDouble(dr["VatPer"].ToString());

                        currentdr.Cells["Col_VatPer"].Value = mamt.ToString("#0.00");
                        mamt = Convert.ToDouble(dr["VatAmount"].ToString());
                        currentdr.Cells["Col_VatAmount"].Value = mamt.ToString("#0.00");
                        if (dr["ProdIfSaleDisc"] != DBNull.Value)
                            currentdr.Cells["Col_IfSaleDisc"].Value = dr["ProdIfSaleDisc"].ToString();
                        currentdr.Cells["Col_StockID"].Value = dr["StockID"].ToString();
                        currentdr.Cells["Col_MasterSaleID"].Value = dr["MasterSaleID"].ToString();
                        currentdr.Cells["Col_PurchaseRate"].Value = dr["PurchaseRate"].ToString();
                        currentdr.Cells["Col_TradeRate"].Value = dr["TradeRate"].ToString();
                        if (dr["MySpecialDiscountAmount"] != DBNull.Value)
                            currentdr.Cells["Col_MySpecialDiscountamount"].Value = dr["MySpecialDiscountAmount"].ToString();
                        else
                            currentdr.Cells["Col_MySpecialDiscountAmount"].Value = string.Empty;


                        currentdr.Cells["Col_CompanyShortName"].Value = dr["ProdCompShortName"].ToString(); //Amar
                        //currentdr.Cells["Col_Expiry"].Value = dr["ExpiryDate"].ToString();
                        currentdr.Cells["Col_Expiry"].Value = dr["Expiry"].ToString(); //Convert.ToDateTime("dd/MM/yyyy").ToString();
                        if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
                        {
                            currentdr.Cells["Col_Time"].Value = dr["CreatedTime"].ToString();
                        }
                        //End

                        totamt += mamount;
                        // mtotamount += Convert.ToDouble(dr["Amount"].ToString());
                        // mtotquantity += Convert.ToInt32(dr["Quantity"].ToString());
                    }
                }
                txtTotalAmount.Text = totamt.ToString("#0.00");
                txtNetAmount.Text = totamt.ToString("#0.00");
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
            //if (_Mode == OperationMode.View)
            //    dgvReportList.Columns["Col_Check"].Visible = false;
            //else
            dgvReportList.Columns["Col_Check"].Visible = true;
            //  dgvReportList.InitializeColumnContextMenu();
        }

        private void GetSaleDataProductwise(string changeCounterSaleType)
        {

            DataTable dtable = new DataTable();
            dtable = _SSSale.GetVoucherSaleDataData(_MGivenDate, changeCounterSaleType);
            _BindingSource = dtable;
            InitializeReportGrid();
            if (dtable != null)
                BindReportGrid();
        }

        private void CheckforSelection()
        {
            _MvouNo = 0;
            if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
            {
                if (Convert.ToBoolean(dgvReportList.CurrentRow.Cells["Col_Check"].Value) == true)
                {
                    if (_Mode == OperationMode.Edit)
                    {
                        pnlFinal.BringToFront();
                        pnlFinal.Dock = DockStyle.Bottom;
                        dgvReportList.Enabled = false;
                        _MvouNo = Convert.ToInt32(dgvReportList.CurrentRow.Cells["Col_VoucherNumber"].Value.ToString());
                        txtsavecustno.Text = _MvouNo.ToString();

                        if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
                        {
                            FillSaleData();
                        }

                        else
                            FillSaleDataforChengeCounterSaleType();
                        filterRows();
                        gbsaletype.Enabled = true;
                        txtPatientName.Focus();
                    }
                    else
                        if (_Mode == OperationMode.Delete)
                        tsBtnDelete.Enabled = true;
                }

            }
            else
            {
                if (_Mode == OperationMode.Edit)
                {
                    pnlFinal.BringToFront();
                    pnlFinal.Dock = DockStyle.Bottom;
                    txtNetAmount.Visible = true;
                    if (General.CurrentSetting.MsetSaleRoundOff == "Y")
                        cbRound.Checked = true;
                    else
                        cbRound.Checked = false;
                    dgvReportList.Enabled = false;
                    _MvouNo = Convert.ToInt32(dgvReportList.CurrentRow.Cells["Col_VoucherNumber"].Value.ToString());
                    txtsavecustno.Text = _MvouNo.ToString();

                    if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
                        FillSaleData();
                    else
                        FillSaleDataforChengeCounterSaleType();
                    gbsaletype.Enabled = true;
                    txtPatientName.Focus();
                }
                else
                    if (_Mode == OperationMode.Delete)
                    tsBtnDelete.Enabled = true;
            }

        }

        private void filterRows()
        {
            foreach (DataGridViewRow dr in dgvReportList.Rows)
            {


                if (Convert.ToBoolean(dr.Cells["Col_Check"].Value) == true || (General.ShopDetail.ShopChangeCounterSaleType != "Y" && _MvouNo == Convert.ToInt32(dr.Cells["Col_VoucherNumber"].Value.ToString())))
                {
                    dr.Visible = true;
                }
                else
                    dr.Visible = false;
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
            txtRoundAmount.Text = _SSSale.CrdbRoundAmount.ToString("#0.00");
            if (Convert.ToDouble(txtRoundAmount.Text.ToString()) != 0 || General.CurrentSetting.MsetSaleRoundOff == "Y")
                cbRound.Checked = true;
            else
                cbRound.Checked = false;
        }

        private void FillSaleDataforChengeCounterSaleType()
        {
            CalculateVAT();

        }

        private void CalculateVAT()
        {
            lblFooterMessage.Text = "";
            double mTotalAmount = 0;
            // double mTotalAmountforDiscount = 0;
            double mTotalAmountVAT5 = 0;
            double mTotalAmountVAT12 = 0;

            double mvatper = 0;
            double mvatamount5 = 0;
            double mvatamount12point5 = 0;
            double mtotamtvat0 = 0;

            double mTvatamount5 = 0;
            double mTvatamount12point5 = 0;
            double mTtotamtvat0 = 0;



            double mrate = 0;
            double mamt = 0;
            double mpakn = 0;
            double mqty = 0;
            int itemCount = 0;
            // 9/12/2014   calculate discount after vat and calculate vat after subtracting vat from amt;
            double mmyspecialDiscountper = 0;
            double mmyspecialdiscountamt5 = 0;
            double mmyspecialdiscountamt12point5 = 0;
            double mmyspecialdiscountamtzero = 0;
            double mdiscamt5 = 0;
            double mdiscamt12point5 = 0;
            double mdiscamtzero = 0;
            double mdiscper = 0;
            double mnewamt = 0;
            double mnewamtwithoutmydiscount = 0;
            double mtotalafterdiscountwithoutmydiscount = 0;
            double mtotaldiscountamount5 = 0;
            double mtotaldiscountamount12point5 = 0;
            double mtotaldiscountamountzero = 0;
            double mtotalmyspecialdiscamt5 = 0;
            double mtotalmyspecialdiscamt12point5 = 0;
            double mtotalmyspecialdiscamtzero = 0;
            double mtotalafterdiscount = 0;

            double mcreditnote = 0;
            double mdebitnote = 0;

            string ifdiscount = "Y";

            //if (txtCreditNote.Text != null && txtCreditNote.Text.ToString() != string.Empty)
            //    mcreditnote = Convert.ToDouble(txtCreditNote.Text.ToString());
            //if (txtDebitNote.Text != null && txtDebitNote.Text.ToString() != string.Empty)
            //    mdebitnote = Convert.ToDouble(txtDebitNote.Text.ToString());

            if (txtPatientName.SelectedID != null && txtPatientName.SelectedID != string.Empty && txtPatientName.SeletedItem.ItemData[1] != null)
                _SSSale.SaleSubType = txtPatientName.SeletedItem.ItemData[1];

            if (txtDiscPercent.Text != null && txtDiscPercent.Text != string.Empty)
                mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());


            //txtMyDiscountPercent.Text = "0.00";
            //if (_SSSale.SaleSubType == FixAccounts.SubTypeForDebtorSale)
            //{
            //    mmyspecialDiscountper = 0;
            //    txtMyDiscountPercent.Text = "0.00";
            //}


            try
            {

                foreach (DataGridViewRow dr in dgvReportList.Rows)
                {
                    mvatamount5 = 0;
                    mvatamount12point5 = 0;
                    mtotamtvat0 = 0;
                    mdiscamt5 = 0;
                    mdiscamt12point5 = 0;
                    mdiscamtzero = 0;
                    mmyspecialdiscountamt5 = 0;
                    mmyspecialdiscountamt12point5 = 0;
                    mmyspecialdiscountamtzero = 0;
                    mnewamtwithoutmydiscount = 0;
                    mnewamt = 0;

                    if (Convert.ToBoolean(dr.Cells["Col_Check"].Value) == true || (General.ShopDetail.ShopChangeCounterSaleType != "Y" && _MvouNo == Convert.ToInt32(dr.Cells["Col_VoucherNumber"].Value.ToString())))
                    {
                        if (dr.Cells["Col_ProductID"].Value != null && dr.Cells["Col_Quantity"].Value != null && dr.Cells["Col_Quantity"].Value.ToString() != "0" && dr.Cells["Col_Quantity"].Value.ToString() != "")
                        {
                            ifdiscount = "Y";
                            mrate = Convert.ToDouble(dr.Cells["Col_Rate"].Value.ToString());
                            mqty = Convert.ToDouble(dr.Cells["Col_Quantity"].Value.ToString());
                            mpakn = Convert.ToDouble(dr.Cells["Col_UOM"].Value.ToString());
                            if (dr.Cells["Col_IfSaleDisc"].Value != null && dr.Cells["Col_IfSaleDisc"].Value.ToString() != "")
                                ifdiscount = dr.Cells["Col_IFSaleDisc"].Value.ToString().ToUpper();

                            if (Math.Truncate(mqty / mpakn) == (mqty / mpakn))
                                mamt = Math.Round((mqty / mpakn) * mrate, 2);
                            else
                            {
                                mamt = Math.Round(Math.Round(mrate / mpakn, 4) * mqty, 2);
                                //if (Math.Round(mamt, 1) - mamt < 0.05)
                                //    mamt = Math.Round(mamt, 1);
                            }

                            dr.Cells["Col_Amount"].Value = (mamt).ToString("#0.00");
                            if (mamt > 0)
                            {
                                mvatamount12point5 = 0;
                                mvatamount5 = 0;
                                mvatper = Convert.ToDouble(dr.Cells["Col_VATPer"].Value.ToString());
                                // vat 5.5
                                if (mvatper == 6)
                                {
                                    mvatamount5 = Math.Round((mamt * mvatper) / (100 + mvatper), 2);
                                    mmyspecialdiscountamt5 = Math.Round((mamt - mvatamount5) * mmyspecialDiscountper / 100, 2);
                                    if (ifdiscount != "N")
                                        mdiscamt5 = Math.Round((mamt - mvatamount5) * mdiscper / 100, 2);
                                    else
                                        mdiscamt5 = 0;
                                }
                                else if (mvatper == 13.5)
                                {
                                    mvatamount12point5 = Math.Round((mamt * mvatper) / (100 + mvatper), 4);
                                    mmyspecialdiscountamt12point5 = Math.Round((mamt - mvatamount5) * mmyspecialDiscountper / 100, 4);
                                    if (ifdiscount != "N")
                                        mdiscamt12point5 = Math.Round((mamt - mvatamount12point5) * mdiscper / 100, 4);
                                    else
                                        mdiscamt12point5 = 0;
                                }
                                else
                                {

                                    mmyspecialdiscountamtzero = Math.Round((mamt) * mmyspecialDiscountper / 100, 2);
                                    if (ifdiscount != "N")
                                        mdiscamtzero = Math.Round(mamt * mdiscper / 100, 2);
                                    else
                                        mdiscamtzero = 0;
                                    mtotamtvat0 += mamt - mmyspecialdiscountamtzero - mdiscamtzero;

                                }
                                mtotaldiscountamount5 += mdiscamt5;
                                mtotaldiscountamount12point5 += mdiscamt12point5;
                                mtotaldiscountamountzero += mdiscamtzero;
                                mtotalmyspecialdiscamt5 += mmyspecialdiscountamt5;
                                mtotalmyspecialdiscamt12point5 += mmyspecialdiscountamt12point5;
                                mtotalmyspecialdiscamtzero += mmyspecialdiscountamtzero;
                                mnewamt = (mamt - mdiscamt5 - mdiscamt12point5 - mdiscamtzero - mmyspecialdiscountamt5 - mmyspecialdiscountamt12point5 - mmyspecialdiscountamtzero);
                                mnewamtwithoutmydiscount = (mamt - mdiscamt5 - mdiscamt12point5 - mdiscamtzero);
                                // vat 5.5
                                if (mvatper == 6)
                                {
                                    mvatamount5 = Math.Round((mnewamt * mvatper) / (100 + mvatper), 2);
                                }
                                else if (mvatper == 13.5)
                                {
                                    mvatamount12point5 = Math.Round((mnewamt * mvatper) / (100 + mvatper), 2);
                                }

                                dr.Cells["Col_VATAmount"].Value = (mvatamount12point5 + mvatamount5).ToString("#0.00");
                                dr.Cells["Col_DiscountAmount"].Value = (mdiscamt5 + mdiscamt12point5 + mdiscamtzero).ToString("#0.00");
                                dr.Cells["Col_MySpecialDiscountAmount"].Value = mmyspecialdiscountamt5 + mmyspecialdiscountamt12point5 + mmyspecialdiscountamtzero;
                                mTotalAmount += mamt;
                                mtotalafterdiscount += mnewamt;
                                // mtotalafterdiscountwithoutmydiscount += 
                                itemCount += 1;
                                //if (ifdiscount != "N")
                                //    mTotalAmountforDiscount += mamt;
                                mTvatamount5 += mvatamount5;
                                mTvatamount12point5 += mvatamount12point5;
                                mTtotamtvat0 += mtotamtvat0;
                                // vat 5.5
                                if (mvatper == 6)
                                    mTotalAmountVAT5 += (mnewamt - mvatamount5);
                                else if (mvatper == 13.5)
                                    mTotalAmountVAT12 += (mnewamt - mvatamount12point5);
                            }
                        }
                    }
                }
                //   NoofRows();
                // txtdiscountAmount5.Text = mtotaldiscountamount5.ToString("#0.00");
                //txtDiscountAmount12point5.Text = mtotaldiscountamount12point5.ToString("#0.00");
                //txtMyDiscountAmount5.Text = mtotalmyspecialdiscamt5.ToString("#0.00");
                //txtMyDiscountAmount12point5.Text = mtotalmyspecialdiscamt12point5.ToString("#0.00");
                txtVatInput5.Text = mTvatamount5.ToString("#0.00");
                txtVatInput12point5.Text = mTvatamount12point5.ToString("#0.00");
                txtAmountfor12VAT.Text = mTotalAmountVAT12.ToString("#0.00");
                txtAmountfor5VAT.Text = mTotalAmountVAT5.ToString("#0.00");
                txtAmountforZeroVAT.Text = mTtotamtvat0.ToString("#0.00");

                double mdblDiscAmount = mtotaldiscountamount5 + mtotaldiscountamount12point5 + mtotaldiscountamountzero;
                double mdblMyDiscAmount = mtotalmyspecialdiscamt12point5 + mtotalmyspecialdiscamt5 + mtotalmyspecialdiscamtzero;
                txtDiscAmount.Text = mdblDiscAmount.ToString("#0.00");
                mtotalafterdiscountwithoutmydiscount = mTotalAmount - mdblDiscAmount - mdblMyDiscAmount;
                //  txtTotalafterdiscount.Text = mtotalafterdiscountwithoutmydiscount.ToString("#0.00");
                //txtMyDiscountAmountTotal.Text = mdblMyDiscAmount.ToString("#0.00");
                double mrndamt = 0;
                mTotalAmount = mTotalAmount - mcreditnote + mdebitnote;
                mtotalafterdiscount = mtotalafterdiscount - mcreditnote + mdebitnote;
                //   mtotalafterdiscount.ToString("#0.00");
                if (cbRound.Checked == true)
                {
                    if (General.CurrentSetting.MsetSaleRoundingToPreviousRupee == "Y")
                    {
                        mrndamt = Math.Floor(Math.Round(mtotalafterdiscount, 2)) - Math.Round(mtotalafterdiscount, 2);
                        txtRoundAmount.Text = mrndamt.ToString("#0.00");
                    }
                    else
                        txtRoundAmount.Text = Math.Round(Math.Round(mtotalafterdiscount, 0) - Math.Round(mtotalafterdiscount, 2), 2).ToString("#0.00");

                    txtNetAmount.Text = mTotalAmount.ToString("#0.00");
                    txtBillAmount2.Text = Math.Round((Convert.ToDouble(mtotalafterdiscount.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString()) + mtotalmyspecialdiscamt12point5 + mtotalmyspecialdiscamt5), 2).ToString("#0.00");
                }
                else
                {
                    txtRoundAmount.Text = "0.00";
                    txtNetAmount.Text = mTotalAmount.ToString("#0.00");
                    txtBillAmount2.Text = Math.Round((Convert.ToDouble(mtotalafterdiscount.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString()) + mdblMyDiscAmount), 2).ToString("#0.00");
                }

                _SSSale.TotalDiscount12point5 = mtotaldiscountamount12point5;
                _SSSale.TotalDiscount5 = mtotaldiscountamount5;
                _SSSale.CrdbVat12point5 = mTvatamount12point5;
                _SSSale.CrdbVat5 = mTvatamount5;
                _SSSale.CrdbAmountVat12point5 = mTotalAmountVAT12;
                _SSSale.CrdbAmountVat5 = mTotalAmountVAT5;
                _SSSale.CrdbAmtForZeroVAT = mTtotamtvat0;
                CalculateProfitPercent();

            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }

        }
        private void CalculateProfitPercent()
        {
            _SSSale.ProfitPercentByPurchaseRate = 0;
            _SSSale.ProfitPercentBySaleRate = 0;
            _SSSale.TotalProfitPercentByPurchaseRate = 0;
            _SSSale.TotalProfitPercentBySaleRate = 0;
            _SSSale.TotalProfitInRupees = 0;

            double mqty = 0;
            double mpurrate = 0;
            double mtraderate = 0;
            double msalerate = 0;
            double mpakn = 0;
            double mprate = 0;
            double mvatper = 0;
            double mvatamt = 0;
            double mamt = 0;
            double mrate = 0;

            double totalsale = 0;
            double totalpur = 0;
            double totalvat = 0;
            // double totaldisc = 0;

            try
            {
                foreach (DataGridViewRow prodrow in dgvReportList.Rows)
                {
                    mqty = 0;
                    mpurrate = 0;
                    mtraderate = 0;
                    msalerate = 0;
                    mpakn = 0;
                    mvatper = 0;
                    mvatamt = 0;
                    mprate = 0;
                    if (Convert.ToBoolean(prodrow.Cells["Col_Check"].Value) == true)
                    {
                        if (prodrow.Cells["Col_ProductName"].Value != null &&
                           Convert.ToDouble(prodrow.Cells["Col_Quantity"].Value) > 0)
                        {
                            if (prodrow.Cells["Col_UOM"].Value != null)
                                double.TryParse(prodrow.Cells["Col_UOM"].Value.ToString(), out mpakn);
                            if (prodrow.Cells["Col_Quantity"].Value != null)
                                double.TryParse(prodrow.Cells["Col_Quantity"].Value.ToString().Trim(), out mqty);
                            if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                                double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString().Trim(), out mpurrate);
                            mrate = Convert.ToDouble(prodrow.Cells["Col_Rate"].Value.ToString());
                            //if (prodrow.Cells["Col_MRP"].Value == null)
                            //    prodrow.Cells["Col_MRP"].Value = mrate.ToString("#0.00");
                            if (mpurrate == 0)
                                mpurrate = mrate - (mrate * 18 / 100);
                            _SSSale.PurchaseRate = mpurrate;
                            if (prodrow.Cells["Col_TradeRate"].Value != null)
                                double.TryParse(prodrow.Cells["Col_TradeRate"].Value.ToString(), out mtraderate);
                            if (mtraderate == 0)
                                mtraderate = mpurrate;
                            if (prodrow.Cells["Col_PurchaseRate"].Value != null)
                                double.TryParse(prodrow.Cells["Col_PurchaseRate"].Value.ToString(), out mprate);
                            if (mprate == 0)
                                mprate = mpurrate;

                            _SSSale.TradeRate = mtraderate;
                            double.TryParse(prodrow.Cells["Col_Rate"].Value.ToString().Trim(), out msalerate);
                            if (prodrow.Cells["Col_VATPer"].Value != null)
                                double.TryParse(prodrow.Cells["Col_VATPer"].Value.ToString(), out mvatper);
                            mvatamt = Math.Round((mtraderate * mvatper) / 100, 2);
                            mamt = Math.Round(Math.Round(mrate / mpakn, 4) * mqty, 2);
                            double mdiscamt = 0;
                            if (prodrow.Cells["Col_DiscountAmount"].Value != null)
                                mdiscamt = Convert.ToDouble(prodrow.Cells["Col_DiscountAmount"].Value.ToString());
                            if (prodrow.Cells["Col_MySpecialDiscountAmount"].Value != null && prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString() != string.Empty)
                                mdiscamt += Convert.ToDouble(prodrow.Cells["Col_MySpecialDiscountAmount"].Value.ToString());
                            _SSSale.SaleRate = msalerate;
                            double newmdiscper = 0;
                            double newmydiscper = 0;
                            double.TryParse(txtDiscPercent.Text, out newmdiscper);
                            //  double.TryParse(txtMyDiscountPercent.Text, out newmydiscper);
                            double newsalerate = msalerate - Math.Round(((msalerate - Math.Round((msalerate * mvatper / 100), 2)) * (newmdiscper + newmydiscper) / 100), 2);
                            double vatontrrate = Math.Round((mtraderate * mvatper) / 100, 2);

                            totalvat += vatontrrate;
                            totalsale += newsalerate;
                            totalpur += mpurrate;

                            _SSSale.ProfitPercentBySaleRate = Math.Round(((msalerate - mdiscamt) - (mpurrate + mvatamt)) / (msalerate - mdiscamt), 4);
                            _SSSale.ProfitPercentByPurchaseRate = Math.Round(((msalerate - mdiscamt) - (mpurrate + mvatamt)) / (mpurrate + mvatamt), 4);
                            //_SSSale.TotalProfitPercentByPurchaseRate += _SSSale.ProfitPercentByPurchaseRate;
                            //_SSSale.TotalProfitPercentBySaleRate += _SSSale.ProfitPercentBySaleRate;
                            _SSSale.ProfitInRupees = Math.Round((((msalerate - mdiscamt) - (mpurrate + mvatamt)) / mpakn) * mqty, 2);
                            _SSSale.TotalProfitInRupees += _SSSale.ProfitInRupees;
                            prodrow.Cells["Col_ProfitPercentBySaleRate"].Value = _SSSale.ProfitPercentBySaleRate.ToString("#0.00");
                            prodrow.Cells["Col_ProfitPercentByPurchaseRate"].Value = _SSSale.ProfitPercentByPurchaseRate.ToString("#0.00");
                            prodrow.Cells["Col_ProfitInRupees"].Value = _SSSale.ProfitInRupees.ToString("#0.00");
                        }
                    }

                }
                _SSSale.TotalProfitPercentBySaleRate = Math.Round(((totalsale) - (totalpur + totalvat)) / (totalsale), 4);
                _SSSale.TotalProfitPercentByPurchaseRate = Math.Round(((totalsale) - (totalpur + totalvat)) / (totalpur + totalvat), 4);
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }
        private void DoRounding()
        {
            //double mdiscamt = 0;
            //double mdiscper = 0;
            //mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());

            mamt = Convert.ToDouble(txtNetAmount.Text.ToString());
            mdiscamt = Convert.ToDouble(txtDiscAmount.Text.ToString());
            //txtDiscAmount.Text = mdiscamt.ToString("#0.00");
            double mtotalafterdiscount = mamt - mdiscamt;
            //CalculateDiscountAfterVat();
            if (cbRound.Checked == true)
            {
                txtRoundAmount.Text = Math.Round(Math.Round(mtotalafterdiscount, 0) - Math.Round(mtotalafterdiscount, 2), 2).ToString("#0.00");
                // txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString()) + mtotalmyspecialdiscamt12point5 + mtotalmyspecialdiscamt5), 2).ToString("#0.00");
                txtBillAmount2.Text = (mtotalafterdiscount + Convert.ToDouble(txtRoundAmount.Text.ToString())).ToString("#0.00");
            }
            else
            {
                txtRoundAmount.Text = "0.00";
                //  txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString()) + mdblMyDiscAmount), 2).ToString("#0.00");
                txtBillAmount2.Text = (mtotalafterdiscount + Convert.ToDouble(txtRoundAmount.Text.ToString())).ToString("#0.00");
            }

        }
        //private void DoRounding()
        //{
        //    double mdiscamt = 0;
        //    double mdiscper = 0;
        //    mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());

        //    mamt = Convert.ToDouble(txtNetAmount.Text.ToString());
        //    mdiscamt = Math.Round((mamt * mdiscper / 100), 2);
        //    txtDiscAmount.Text = mdiscamt.ToString("#0.00");
        //    double mtotalafterdiscount = mamt - mdiscamt;
        //    if (cbRound.Checked == true)
        //    {
        //        txtRoundAmount.Text = Math.Round(Math.Round(mtotalafterdiscount, 0) - Math.Round(mtotalafterdiscount, 2), 2).ToString("#0.00");
        //        // txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString()) + mtotalmyspecialdiscamt12point5 + mtotalmyspecialdiscamt5), 2).ToString("#0.00");
        //        txtBillAmount2.Text = (mtotalafterdiscount + Convert.ToDouble(txtRoundAmount.Text.ToString())).ToString("#0.00");
        //    }
        //    else
        //    {
        //        txtRoundAmount.Text = "0.00";
        //        //  txtBillAmount.Text = Math.Round((Convert.ToDouble(txtTotalafterdiscount.Text.ToString()) + Convert.ToDouble(txtRoundAmount.Text.ToString()) + mdblMyDiscAmount), 2).ToString("#0.00");
        //        txtBillAmount2.Text = (mtotalafterdiscount + Convert.ToDouble(txtRoundAmount.Text.ToString())).ToString("#0.00");
        //    }

        //}
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
            double mdebit = mdebit = Math.Round(mbillamount - Math.Round(mvat5per, 2) - Math.Round(mvat12point5per, 2) + mdiscamount - maddon + mround - mamtforzerovat + mcreditnoteamt - mdebitnoteamt, 2);
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

                if (mvat5per > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput6Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(mvat5per, 2);
                    retValue = _SSSale.AddVoucherIntblTrnac();

                }
                if (mvat12point5per > 0)
                {
                    _SSSale.DebitAccount = FixAccounts.AccountVatOutput13point5Sale;
                    if (_SSSale.CrdbVouType == FixAccounts.VoucherTypeForCashSale)
                        _SSSale.CreditAccount = FixAccounts.AccountCash;
                    else
                        _SSSale.CreditAccount = _SSSale.AccountID;
                    _SSSale.DetailId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
                    _SSSale.DebitAmount = 0;
                    _SSSale.CreditAmount = Math.Round(mvat12point5per, 2);
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

            if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
                CheckforSelection();

        }

        private void GivenDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClearControls();
                _MGivenDate = GivenDate.Value.ToString("yyyyMMdd");
                GetSaleDataProductwise(General.ShopDetail.ShopChangeCounterSaleType);
                if (dgvReportList.Rows.Count > 0)
                {
                    this.ActiveControl = dgvReportList;
                    dgvReportList.Focus();
                }
                else
                {
                    this.ActiveControl = GivenDate;
                    GivenDate.Focus();
                }
            }
        }

        //private void rbtCash_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            if (rbtCash.Checked)
        //            {
        //                txtAddress.Enabled = true;
        //                txtPatientName.Focus();
        //            }
        //        }
        //        if (e.KeyCode == Keys.Right)
        //            rbtCashCredit.Focus();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        //private void rbtCashCredit_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            if (rbtCashCredit.Checked)
        //            {
        //                txtAddress.Enabled = true;
        //                txtPatientName.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        //private void rbtCreditCard_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            if (rbtCreditCard.Checked)
        //            {
        //                txtAddress.Enabled = true;
        //                txtPatientName.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

        //private void rbtCreditStatement_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            if (rbtCreditStatement.Checked)
        //            {
        //                txtAddress.Enabled = true;
        //                txtPatientName.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteException(ex);
        //    }
        //}

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
                        {
                            //mcbDoctor.Enabled = false; 
                            //txtDiscPercent.Focus();
                            txtMobileNumber.Focus();
                        }
                        else
                            //mcbDoctor.Focus();
                            txtMobileNumber.Focus();

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
            if (General.ShopDetail.ShopChangeCounterSaleType != "Y")
            {

                if (txtsavecustno.Text != null && txtsavecustno.Text.ToString() != "")
                    int.TryParse(txtsavecustno.Text.ToString(), out _MvouNo);
                if (_MvouNo > 0)
                {
                    tsBtnSave.Enabled = true;
                    tsBtnSavenPrint.Enabled = true;
                }
                else
                {
                    tsBtnSave.Enabled = true;
                    tsBtnSavenPrint.Enabled = false;
                }

            }
            else
            {
                tsBtnSave.Enabled = true;
                tsBtnSavenPrint.Enabled = true;
            }
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

        private void mcbDoctor_EnterKeyPressed(object sender, EventArgs e)
        {
            if (mcbDoctor.SeletedItem != null && string.IsNullOrEmpty(Convert.ToString(mcbDoctor.SelectedID)) == false && mcbDoctor.SeletedItem.ItemData[2] != null)
            {
                mcbDoctor.Text = mcbDoctor.SeletedItem.ItemData[1];
                txtDoctorAddress.Text = mcbDoctor.SeletedItem.ItemData[2];
                mcbDoctor.ReadOnly = true;
            }
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
            txtDoctorAddress.Enabled = true;
            mcbDoctor.Enabled = true;
            mcbDoctor.ReadOnly = false;
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

        private void dgvReportList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                CheckforSelection();
        }

        private void txtDiscPercent_KeyDown(object sender, KeyEventArgs e)
        {
            double mdiscper = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDiscPercent.Text != null && txtDiscPercent.Text.ToString() != string.Empty)
                {
                    mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());
                    txtDiscPercent.Text = mdiscper.ToString("#0.00");
                }

                CalculateVAT();
                cbRound.Focus();
            }
        }

        private void dgvReportList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                double mrate = 0;
                double mqty = 0;
                double mamt = 0;
                double mpackn = 0;
                mrate = Convert.ToDouble(dgvReportList.CurrentRow.Cells["Col_Rate"].Value.ToString());
                mqty = Convert.ToDouble(dgvReportList.CurrentRow.Cells["Col_Quantity"].Value.ToString());
                mpackn = Convert.ToDouble(dgvReportList.CurrentRow.Cells["Col_UOM"].Value.ToString());
                mamt = Math.Round((mqty * (mrate / mpackn)), 2);
                dgvReportList.CurrentRow.Cells["Col_Rate"].Value = mrate.ToString("#0.00");
                dgvReportList.CurrentRow.Cells["Col_Amount"].Value = mamt.ToString("#0.00");
            }
        }

        private void cbRound_CheckedChanged(object sender, EventArgs e) //amar
        {
            if (cbRound.Checked == true)
            {
                double mdiscper = 0;
                if (txtDiscPercent.Text != null && txtDiscPercent.Text.ToString() != string.Empty)
                {
                    mdiscper = Convert.ToDouble(txtDiscPercent.Text.ToString());
                    txtDiscPercent.Text = mdiscper.ToString("#0.00");
                }

                CalculateVAT();
                cbRound.Focus();
            }
            else if (cbRound.Checked == false)
            {
                txtBillAmount2.Text = (Convert.ToDouble(txtNetAmount.Text) - Convert.ToDouble(txtDiscAmount.Text)).ToString("#0.00");
                txtRoundAmount.Text = "0.00";
            }

        }

        private void txtMobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtPatientName.Text) == true)
                {
                    if (txtMobileNumber.Text != null && txtMobileNumber.Text != string.Empty)
                        _SSSale.MobileNumberForSMS = txtMobileNumber.Text.ToString();
                    if (_SSSale.MobileNumberForSMS != string.Empty)
                    {
                        DataRow dr = null;
                        dr = _SSSale.GetPatientDataByMobileNumber();
                        if (dr != null)
                        {
                            string selectedId = dr["PatientID"].ToString();
                            txtPatientName.SelectedID = selectedId;
                            txtAddress.Text = dr["PatientAddress1"].ToString();
                            mcbDoctor.SelectedID = dr["DoctorID"].ToString();
                            mcbDoctor.Focus();
                        }
                        mcbDoctor.Focus();
                    }
                    else
                    {
                        mcbDoctor.Focus();
                    }

                }
                else
                    mcbDoctor.Focus();
            }
            else if (e.KeyCode == Keys.Up)
                txtAddress.Focus();

        }

        //private void SetDebtorDeatils()
        //{
        //    try
        //    {
        //        if (txtPatientName.Text == "")
        //        {
        //            txtAddress.Text = "";
        //            txtMobileNumber.Text = "";
        //            mcbDoctor.Text = "";
        //            txtDoctorAddress.Text = "";
        //            mcbDoctor.SelectedID = null;
        //        }
        //        else
        //        {
        //            if (General.CurrentUser.Level == 0)
        //            {
        //                cbEditRate.Enabled = true;
        //            }
        //            cbFill.Enabled = true;
        //            cbTransferSale.Enabled = true;
        //            FillCreditDebitNote();
        //            _SSSale.CrdbName = mcbCreditor.SeletedItem.ItemData[2];
        //            txtAddress1.Text = mcbCreditor.SeletedItem.ItemData[3];
        //            txtAddress2.Text = mcbCreditor.SeletedItem.ItemData[4];
        //            txtPatient.Text = mcbCreditor.SeletedItem.ItemData[2];

        //            DataRow[] dr = (txtPatient.DataSource).Select("PatientName = '" + txtPatient.Text + "'");
        //            if (dr.Length > 0 && dr[0] != null)
        //            {
        //                txtPatient.SelectedID = Convert.ToString(dr[0]["PatientID"]);
        //            }

        //            txtPatientAddress.Text = txtAddress1.Text.ToString();
        //            string MobileNumber = Convert.ToString(mcbCreditor.SeletedItem.ItemData[10]);
        //            if (string.IsNullOrEmpty(MobileNumber) == false)
        //                txtMobileNumber.Text = MobileNumber;

        //            txtDiscPercent.Text = mcbCreditor.SeletedItem.ItemData[9];
        //            if (mcbCreditor.SeletedItem.ItemData[6] != "")
        //                _SSSale.TokenNumber = Convert.ToInt32(mcbCreditor.SeletedItem.ItemData[6].ToString());
        //            if (_Mode == OperationMode.Add)
        //            {
        //                _SSSale.DocID = mcbCreditor.SeletedItem.ItemData[7];
        //                mcbDoctor.SelectedID = _SSSale.DocID;
        //            }
        //            _SSSale.TransactionType = mcbCreditor.SeletedItem.ItemData[8];
        //            txtTokenNumber.Text = _SSSale.TokenNumber.ToString();
        //            if (_Mode == OperationMode.Add)
        //            {
        //                if (_SSSale.TransactionType == "CS")
        //                {
        //                    cbTransactionType.Text = FixAccounts.TransactionTypeForCash;
        //                    cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCash);
        //                    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCashSale;
        //                }
        //                else if (_SSSale.TransactionType == "CR")
        //                {
        //                    cbTransactionType.Text = FixAccounts.TransactionTypeForCreditStatement;
        //                    cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCreditStatement);
        //                    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditStatementSale;
        //                }
        //                else
        //                {
        //                    cbTransactionType.Text = FixAccounts.TransactionTypeForCredit;
        //                    cbTransactionType.SelectedIndex = cbTransactionType.Items.IndexOf(FixAccounts.TransactionTypeForCredit);
        //                    _SSSale.CrdbVouType = FixAccounts.VoucherTypeForCreditSale;
        //                }
        //                txtVouType.Text = _SSSale.CrdbVouType;
        //            }

        //            _SSSale.GetPendingAmount(mcbCreditor.SelectedID);
        //            _SSSale.GetOpeningBalance(mcbCreditor.SelectedID);
        //            _SSSale.PendingAmount = _SSSale.OpeningBalance + (_SSSale.TotalDebit - _SSSale.TotalCredit);
        //            txtPendingBalance.Text = Math.Abs(_SSSale.PendingAmount).ToString("#0.00");
        //            txtPatient.Focus();
        //        }
        //    }
        //    catch (Exception Ex)
        //    { Log.WriteException(Ex); }
        //}

        private void txtMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void cbRound_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MainToolStrip.Select();
                tsBtnSave.Select();
            }
        }
    }
}
