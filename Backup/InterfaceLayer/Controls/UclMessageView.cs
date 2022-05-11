using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using PharmaSYSRetailPlus.Common;
using PharmaSYSRetailPlus.BusinessLayer;

namespace PharmaSYSRetailPlus.InterfaceLayer
{
    [System.ComponentModel.ToolboxItem(false)]
    public enum MessageViewType
    {
        None,
        Messages,
        ExpiryOneMonth,
        ExpiryTwoMonth,
        ExpiryThreeMonth,
    }

    public partial class UclMessageView : UserControl
    {
        MessageViewType _ViewType;
        public UclMessageView()
        {
            InitializeComponent();
            _ViewType = MessageViewType.Messages;
        }

        private void LoadMessages()
        {
            try
            {
                Messages _msg = new Messages();
                DataTable dtable = _msg.GetOverviewData();
                if (dtable != null && dtable.Rows.Count > 0)
                {
                    _MsgList = new ArrayList();
                    foreach (DataRow dr in dtable.Rows)
                    {
                        _MsgList.Add(dr["Message"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void LoadExpiryOneMonth()
        {
            try
            {
                int mmonth = DateTime.Now.Month;
                int myear = DateTime.Now.Year;
                string msgstring = "";
                mmonth = mmonth + 1;
                if (mmonth == 13)
                {
                    mmonth = 1;
                    myear = myear + 1;
                }                
                string mdate = "";
                string cmonth = "";           
                if (mmonth > 0 && myear > 2000)
                {
                    cmonth = "00" + Convert.ToString(mmonth).Trim();
                    int mlen = 0;
                    mlen = cmonth.Length;
                    if (mlen == 3)
                        cmonth = cmonth.Substring(1, 2);
                    else
                        cmonth = cmonth.Substring(2, 2);
                    mdate = Convert.ToString(myear).Trim() + cmonth + "01";
                    DebitNoteExpiry _DNExpiry = new DebitNoteExpiry();
                    DataTable dtable = _DNExpiry.ReadExpiredStockForMessage(mdate);
                    if (dtable != null && dtable.Rows.Count > 0)
                    {
                        _MsgList = new ArrayList();
                        foreach (DataRow dr in dtable.Rows)
                        {
                            msgstring = dr["ProdName"].ToString() + "-" + dr["ProdLoosePack"].ToString() + "-" + dr["ProdPack"].ToString() + "-" + dr["BatchNumber"].ToString() + "-" + dr["Expiry"].ToString() + "=>" + dr["ClosingStock"].ToString();
                            _MsgList.Add(msgstring);
                        }
                    }          
                }              
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void LoadExpiryTwoMonth()
        {
            try
            {
                int mmonth = DateTime.Now.Month;
                int myear = DateTime.Now.Year;
                string msgstring = "";
                mmonth = mmonth + 2;
                if (mmonth == 13)
                {
                    mmonth = 1;
                    myear = myear + 1;
                }
                else if (mmonth == 14)
                {
                    mmonth = 2;
                    myear = myear + 1;
                }

                string mdate = "";
                string cmonth = "";
                if (mmonth > 0 && myear > 2000)
                {
                    cmonth = "00" + Convert.ToString(mmonth).Trim();
                    int mlen = 0;
                    mlen = cmonth.Length;
                    if (mlen == 3)
                        cmonth = cmonth.Substring(1, 2);
                    else
                        cmonth = cmonth.Substring(2, 2);
                    mdate = Convert.ToString(myear).Trim() + cmonth + "01";
                    DebitNoteExpiry _DNExpiry = new DebitNoteExpiry();
                    DataTable dtable = _DNExpiry.ReadExpiredStockForMessage(mdate);
                    if (dtable != null && dtable.Rows.Count > 0)
                    {
                        _MsgList = new ArrayList();
                        foreach (DataRow dr in dtable.Rows)
                        {
                            msgstring = dr["ProdName"].ToString() + "-" + dr["ProdLoosePack"].ToString() + "-" + dr["ProdPack"].ToString() + "-" + dr["BatchNumber"].ToString() + "-" + dr["Expiry"].ToString() + "=>" + dr["ClosingStock"].ToString();
                            _MsgList.Add(msgstring);
                        }
                    }
                }              
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void LoadExpiryThreeMonth()
        {
            try
            {
                int mmonth = DateTime.Now.Month;
                int myear = DateTime.Now.Year;
                string msgstring = "";
                mmonth = mmonth + 3;
                if (mmonth == 13)
                {
                    mmonth = 1;
                    myear = myear + 1;
                }
                else if (mmonth == 14)
                {
                    mmonth = 2;
                    myear = myear + 1;
                }
                else if (mmonth == 15)
                {
                    mmonth = 3;
                    myear = myear + 1;
                }

                string mdate = "";
                string cmonth = "";
                if (mmonth > 0 && myear > 2000)
                {
                    cmonth = "00" + Convert.ToString(mmonth).Trim();
                    int mlen = 0;
                    mlen = cmonth.Length;
                    if (mlen == 3)
                        cmonth = cmonth.Substring(1, 2);
                    else
                        cmonth = cmonth.Substring(2, 2);
                    mdate = Convert.ToString(myear).Trim() + cmonth + "01";
                    DebitNoteExpiry _DNExpiry = new DebitNoteExpiry();
                    DataTable dtable = _DNExpiry.ReadExpiredStockForMessage(mdate);
                    if (dtable != null && dtable.Rows.Count > 0)
                    {
                        _MsgList = new ArrayList();
                        foreach (DataRow dr in dtable.Rows)
                        {
                            msgstring = dr["ProdName"].ToString() + "-" + dr["ProdLoosePack"].ToString() + "-" + dr["ProdPack"].ToString() + "-" + dr["BatchNumber"].ToString() + "-" + dr["Expiry"].ToString() + "=>" + dr["ClosingStock"].ToString();
                            _MsgList.Add(msgstring);
                        }
                    }
                }              
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        public void LoadData()
        {
            try
            {
                _MsgList = new ArrayList();
                if (_ViewType == MessageViewType.Messages)
                {
                    LoadMessages();
                }
                else if (_ViewType == MessageViewType.ExpiryOneMonth)
                {
                    LoadExpiryOneMonth();
                }
                else if (_ViewType == MessageViewType.ExpiryTwoMonth)
                {
                    LoadExpiryTwoMonth();
                }
                else if (_ViewType == MessageViewType.ExpiryThreeMonth)
                {
                    LoadExpiryThreeMonth();
                }
                lblMessage.Text = "";
                for (int cnt = 0; cnt < _MsgList.Count; cnt++)
                {
                    if (cnt != _MsgList.Count-1)
                        lblMessage.Text += _MsgList[cnt].ToString() + " || ";
                    else
                        lblMessage.Text += _MsgList[cnt].ToString();
                }
                lblMessage.Left = this.Width;
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }           
        }

        private ArrayList _MsgList;
        public ArrayList MsgList
        {
            get { return _MsgList; }
            set { _MsgList = value; }
        }

        public void Start()
        {
            try
            {
                tmScroller.Interval = 150;
                tmScroller.Start();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            } 
        }

        public void Stop()
        {
            try
            {
                tmScroller.Stop();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }            
        }

        private void UncheckOtherMenuItems(ToolStripMenuItem itemCurrent)
        {
            try
            {
                foreach (ToolStripMenuItem item in cmMessageView.Items)
                {
                    if (item != itemCurrent)
                    {
                        item.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void tmScroller_Tick(object sender, EventArgs e)
        {
            try
            {
                if (lblMessage.Right <= 0)
                {
                    lblMessage.Left = this.Width;
                }
                else
                    lblMessage.Left = lblMessage.Left - 5;

                this.Refresh();
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            } 
        }

        private void UclMessageView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    cmMessageView.Show(System.Windows.Forms.Cursor.Position);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void lblMessage_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    cmMessageView.Show(System.Windows.Forms.Cursor.Position);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void menuItemStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ViewType != MessageViewType.None)
                {
                    _ViewType = MessageViewType.None;
                    lblMessage.Text = "";
                    Stop();
                    cmMessageView.Hide();
                    this.Refresh();
                    UncheckOtherMenuItems(menuItemStop);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void menuItemMessages_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ViewType != MessageViewType.Messages)
                {
                    Stop();
                    _ViewType = MessageViewType.Messages;
                    cmMessageView.Hide();
                    this.Refresh();
                    LoadData();
                    Start();
                    UncheckOtherMenuItems(menuItemMessages);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void menuItemExpiry_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ViewType != MessageViewType.ExpiryOneMonth)
                {
                    Stop();
                    _ViewType = MessageViewType.ExpiryOneMonth;
                    cmMessageView.Hide();
                    this.Refresh();
                    LoadData();
                    Start();
                    UncheckOtherMenuItems(menuItemExpiry);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void expiryTwoMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ViewType != MessageViewType.ExpiryTwoMonth)
                {
                    Stop();
                    _ViewType = MessageViewType.ExpiryTwoMonth;
                    cmMessageView.Hide();
                    this.Refresh();
                    LoadData();
                    Start();
                    UncheckOtherMenuItems(expiryTwoMonthToolStripMenuItem);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void expiryThreeMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ViewType != MessageViewType.ExpiryThreeMonth)
                {
                    Stop();
                    _ViewType = MessageViewType.ExpiryThreeMonth;
                    cmMessageView.Hide();
                    this.Refresh();
                    LoadData();
                    Start();
                    UncheckOtherMenuItems(expiryThreeMonthToolStripMenuItem);
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }        
    }
}
