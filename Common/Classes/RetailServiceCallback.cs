using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace EcoMart.Common.Classes
{
  
    //public class RetailServiceCallback:ServiceReference1.IRetailPlusServiceCallback
    //{
    //    BackgroundWorker m_oWorker;        
    //    #region IRetailPlusServiceCallback Members

    //    //public void Notify(EcoMart.ServiceReference1.NotificationTypes notificationType, string productIdsToRefresh)
    //    //{
    //    //    try
    //    //    {
    //    //        m_oWorker = new BackgroundWorker();
    //    //        m_oWorker.DoWork += new DoWorkEventHandler(m_oWorker_DoWork);
    //    //        m_oWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(m_oWorker_RunWorkerCompleted);
    //    //        m_oWorker.WorkerReportsProgress = true;
    //    //        m_oWorker.WorkerSupportsCancellation = true;
    //    //        m_oWorker.RunWorkerAsync(productIdsToRefresh);                
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        Log.WriteException(ex);
    //    //    }
    //    //}

    //    //private void m_oWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    //    //{
    //    //    try
    //    //    {
    //    //        if (e.Result != null)
    //    //        {
    //    //            string productIdsToRefresh = (string)e.Result;
    //    //            if (productIdsToRefresh == string.Empty)
    //    //                General.RefillClientProductListForCallBack();
    //    //            else
    //    //                General.RefreshClientProductListForCallBack(productIdsToRefresh);
    //    //            General.FormMainInstance.WindowMenuItem_RunProductRefresh();
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        Log.WriteException(ex);
    //    //    }
    //    //}       

    //    private void m_oWorker_DoWork(object sender, DoWorkEventArgs e)
    //    {
    //        try
    //        {                
    //            try
    //            {
    //                System.Threading.Thread.Sleep(1);
    //                e.Result = e.Argument;
    //            }
    //            catch (Exception ex)
    //            {
    //                Log.WriteException(ex);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Log.WriteException(ex);
    //        }
    //    }
    //    #endregion        
    //}
}
