using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using EcoMart.Common;

namespace EcoMart.BusinessLayer
{
    public abstract class BaseAccountObject : BaseObject
    {
        #region Declaration       
        //private string _DebitAccount;
        //private string _CreditAccount;
        //private double _DebitAmount;
        //private double _CreditAmount; 
        #endregion

        #region Constructors, Destructors
        public BaseAccountObject()
        {
            Initialise();
        }
        #endregion

        #region Properties
              
        //public string DebitAccount
        //{
        //    get { return _DebitAccount; }
        //    set { _DebitAccount = value; }
        //}
        //public string CreditAccount
        //{
        //    get { return _CreditAccount; }
        //    set { _CreditAccount = value; }
        //}
        //public double DebitAmount
        //{
        //    get { return _DebitAmount; }
        //    set { _DebitAmount = value; }
        //}
        //public double CreditAmount
        //{
        //    get { return _CreditAmount; }
        //    set { _CreditAmount = value; }
        //}
        #endregion

        #region Virtual Methods
        public override void Initialise()
        {
            base.Initialise();
            //_DebitAccount = "";
            //_CreditAccount = "";
            //_DebitAmount = 0;
            //_CreditAmount = 0;
        }      
        #endregion
       
    }
}
