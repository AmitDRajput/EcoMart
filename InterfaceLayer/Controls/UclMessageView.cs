using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using EcoMart.Common;
using EcoMart.BusinessLayer;

namespace EcoMart.InterfaceLayer
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
        private float textoffset = 0;
        System.Drawing.Graphics graphics = null;

        public UclMessageView()
        {
            InitializeComponent();
            _ViewType = MessageViewType.None;
            lblMessage.AutoEllipsis = true;
        }

        private void LoadMessages()
        {
            try
            {
                Messages _msg = new Messages();
                DataTable dtable = _msg.GetOverviewData();
                if (dtable != null && dtable.Rows.Count > 0)
                {
                    StringBuilder msgstring = new StringBuilder("");

                    if (_MsgList == null)
                        _MsgList = new ArrayList();

                    foreach (DataRow dr in dtable.Rows)
                    {
                        
                        msgstring.Append(dr["Message"].ToString() + "||");

                    }

                    
                    string outputMsg = msgstring.ToString();
                    _MsgList.Add(outputMsg.Remove(outputMsg.LastIndexOf("||")));
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
                StringBuilder msgstring = new StringBuilder("");
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
                        if (_MsgList == null)
                            _MsgList = new ArrayList();
                        foreach (DataRow dr in dtable.Rows)
                        {
                            msgstring.Append(dr["ProdName"].ToString() + "-" + dr["ProdLoosePack"].ToString() + "-" + dr["ProdPack"].ToString() + "-" + dr["BatchNumber"].ToString() + "-" + dr["Expiry"].ToString() + "=>" + dr["ClosingStock"].ToString() + "||");

                        }

                        string outputMsg = msgstring.ToString();
                        _MsgList.Add(outputMsg.Remove(outputMsg.LastIndexOf("||")));
                        //_MsgList.Add(msgstring);
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
                StringBuilder msgstring = new StringBuilder("");
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
                        if (_MsgList == null)
                            _MsgList = new ArrayList();
                        foreach (DataRow dr in dtable.Rows)
                        {
                            msgstring.Append(dr["ProdName"].ToString() + "-" + dr["ProdLoosePack"].ToString() + "-" + dr["ProdPack"].ToString() + "-" + dr["BatchNumber"].ToString() + "-" + dr["Expiry"].ToString() + "=>" + dr["ClosingStock"].ToString() + "||");

                        }

                        string outputMsg = msgstring.ToString();
                        _MsgList.Add(outputMsg.Remove(outputMsg.LastIndexOf("||")));
                        //_MsgList.Add(msgstring);
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
                StringBuilder msgstring = new StringBuilder("");
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
                        if (_MsgList == null)
                            _MsgList = new ArrayList();
                        foreach (DataRow dr in dtable.Rows)
                        {
                            msgstring.Append(dr["ProdName"].ToString() + "-" + dr["ProdLoosePack"].ToString() + "-" + dr["ProdPack"].ToString() + "-" + dr["BatchNumber"].ToString() + "-" + dr["Expiry"].ToString() + "=>" + dr["ClosingStock"].ToString() + "||");

                        }
                        string outputMsg = msgstring.ToString();
                        _MsgList.Add(outputMsg.Remove(outputMsg.LastIndexOf("||")));
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
                    //Commented by Amit 14/10/2016

                    //if (cnt != _MsgList.Count - 1)
                    //    lblMessage.Text += _MsgList[cnt].ToString() + " || ";
                    //else
                    //    lblMessage.Text += _MsgList[cnt].ToString();

                    textoffset = (float)pbMessage.Width; // Text starts off the right edge of the window
                    pbMessage.Image = new Bitmap(pbMessage.Width, pbMessage.Height);
                    graphics = Graphics.FromImage(pbMessage.Image);
                }
                // lblMessage.Left = this.Width;

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
                graphics.Clear(BackColor);
                graphics.DrawString(_MsgList[0].ToString(), new Font("Microsoft Sans Serif", 9F, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(textoffset, 0));
                //graphics.DrawString(_MsgList[0].ToString(), this.Font, Brushes.Black, ClientRectangle);
                pbMessage.Refresh();
                textoffset = textoffset - 5;

                //if (lblMessage.Right <= 0)
                //{
                //    lblMessage.Left = this.Width;
                //}
                //else
                //    lblMessage.Left = lblMessage.Left - 5;

                //this.Refresh();
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
            this.Cursor = Cursors.WaitCursor;
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
            this.Cursor = Cursors.Default;
        }

        private void menuItemExpiry_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
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
            this.Cursor = Cursors.Default;
        }

        private void expiryTwoMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
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
            this.Cursor = Cursors.Default;
        }

        private void expiryThreeMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
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
            this.Cursor = Cursors.Default;
        }
    }
}
