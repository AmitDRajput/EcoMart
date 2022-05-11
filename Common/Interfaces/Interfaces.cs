using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace EcoMart.Common
{
    public enum OperationMode
    {
        None = 0,
        Add = 1,
        Edit = 2,
        View = 3,
        Delete = 4,
        OpenAsChild = 5,
        Report = 6,
        ReportView = 7,
        Fifth = 8,
    }

    public enum ViewMode
    {
        None = 0,
        Current = 1,
        Changed = 2,
        Deleted = 3,
    }

    public enum ReturnCode
    {
        Success = 0,
        Fail = 1
    }

    public enum OperationButton
    {
        None = 0,
        Delete = 1,
        Save = 2,
        Cancel = 3,
        SavenPrint = 4,
        Print = 5,
        Exit = 6,
        Search = 7,
    }

    public enum PSMessageBoxIcon
    {
        None = 0,
        Information = 1,
        Warning = 2,
        Error = 3,
    }

    public enum PSMessageBoxButtons
    {
        None = 0,
        OK = 1,
        Print = 2,
        OKPrint = 3,
    }

    public enum PSDialogResult
    {
        None = 0,
        OK = 1,
        Print = 2,
    }

    public enum VoucherTypes
    {
        None = 0,
        Cash = 1,
        CreditStatement = 2,
        Credit = 3,
        All = 4,
        Voucher = 5,
        CreditCard = 6,
    }

    public enum ModuleNumber
    {
        None = 0,
        CounterSale = 1,
        PatientSale = 2,
        DebtorSale = 3,
        HospitalSale = 4,
        InstitutionalSale = 5,
    }

    public interface IDetailControl
    {
        bool Add();
        bool Edit();
        bool Delete();
        bool View();
        bool Save();
        bool Cancel();
        bool Print();
        bool Search();
        bool Exit();
        void ReFillData();
        bool Fifth();
    }

    public interface IValidation
    {
        bool IsValid
        {
            get;
        }

        ArrayList ValidationMessages
        {
            get;
        }
        void Validate();
    }

    public interface IReportControl
    {
        void ShowOverview();
        void Print();
    }
}
