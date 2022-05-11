using System;
using System.Windows.Forms;
namespace EcoMart
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    partial class AddSMSsettings : System.Windows.Forms.Form
    {
        //Form overrides dispose to clean up the component list.
        [System.Diagnostics.DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        //Required by the Windows Form Designer

        private System.ComponentModel.IContainer components;
        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblFrmName = new System.Windows.Forms.Label();
            this.DGVSMSsettings = new System.Windows.Forms.DataGridView();
            this.SMS_Name = new System.Windows.Forms.DataGridViewLinkColumn();
            this.URL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Label3 = new System.Windows.Forms.Label();
            this.GroupBoxSMSsettings = new System.Windows.Forms.GroupBox();
            this.Btn_AddNew = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TxtPriority = new System.Windows.Forms.TextBox();
            this.LabelPriority = new System.Windows.Forms.Label();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Btn_Delete = new System.Windows.Forms.Button();
            this.Btn_Update = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.LabelName = new System.Windows.Forms.Label();
            this.LabelURL = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.LabelError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGVSMSsettings)).BeginInit();
            this.GroupBoxSMSsettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFrmName
            // 
            this.lblFrmName.AutoSize = true;
            this.lblFrmName.BackColor = System.Drawing.Color.Transparent;
            this.lblFrmName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFrmName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrmName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.lblFrmName.Location = new System.Drawing.Point(14, 11);
            this.lblFrmName.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblFrmName.Name = "lblFrmName";
            this.lblFrmName.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lblFrmName.Size = new System.Drawing.Size(206, 19);
            this.lblFrmName.TabIndex = 55;
            this.lblFrmName.Text = "Add/View  SMS Settings";
            this.lblFrmName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DGVSMSsettings
            // 
            this.DGVSMSsettings.AllowUserToAddRows = false;
            this.DGVSMSsettings.AllowUserToDeleteRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(229)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(229)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.DGVSMSsettings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.DGVSMSsettings.BackgroundColor = System.Drawing.Color.White;
            this.DGVSMSsettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(188)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(188)))), ((int)(((byte)(188)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVSMSsettings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.DGVSMSsettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVSMSsettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SMS_Name,
            this.URL,
            this.priority});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVSMSsettings.DefaultCellStyle = dataGridViewCellStyle8;
            this.DGVSMSsettings.GridColor = System.Drawing.Color.White;
            this.DGVSMSsettings.Location = new System.Drawing.Point(13, 45);
            this.DGVSMSsettings.Name = "DGVSMSsettings";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVSMSsettings.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.DGVSMSsettings.RowHeadersVisible = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.DGVSMSsettings.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.DGVSMSsettings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGVSMSsettings.Size = new System.Drawing.Size(420, 335);
            this.DGVSMSsettings.TabIndex = 56;
            this.DGVSMSsettings.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVSMSsettings_CellContentClick);
            // 
            // SMS_Name
            // 
            this.SMS_Name.DataPropertyName = "name";
            this.SMS_Name.HeaderText = "Name";
            this.SMS_Name.Name = "SMS_Name";
            this.SMS_Name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SMS_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // URL
            // 
            this.URL.DataPropertyName = "URL";
            this.URL.HeaderText = "URL";
            this.URL.Name = "URL";
            this.URL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.URL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.URL.Width = 250;
            // 
            // priority
            // 
            this.priority.DataPropertyName = "Priority";
            this.priority.HeaderText = "Priority";
            this.priority.Name = "priority";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Location = new System.Drawing.Point(445, 45);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(132, 15);
            this.Label3.TabIndex = 60;
            this.Label3.Text = "* fields are mandatory";
            // 
            // GroupBoxSMSsettings
            // 
            this.GroupBoxSMSsettings.BackColor = System.Drawing.Color.Transparent;
            this.GroupBoxSMSsettings.Controls.Add(this.Btn_AddNew);
            this.GroupBoxSMSsettings.Controls.Add(this.Label2);
            this.GroupBoxSMSsettings.Controls.Add(this.Label1);
            this.GroupBoxSMSsettings.Controls.Add(this.TxtPriority);
            this.GroupBoxSMSsettings.Controls.Add(this.LabelPriority);
            this.GroupBoxSMSsettings.Controls.Add(this.TxtName);
            this.GroupBoxSMSsettings.Controls.Add(this.Label4);
            this.GroupBoxSMSsettings.Controls.Add(this.Btn_Delete);
            this.GroupBoxSMSsettings.Controls.Add(this.Btn_Update);
            this.GroupBoxSMSsettings.Controls.Add(this.ButtonCancel);
            this.GroupBoxSMSsettings.Controls.Add(this.LabelName);
            this.GroupBoxSMSsettings.Controls.Add(this.LabelURL);
            this.GroupBoxSMSsettings.Controls.Add(this.txtURL);
            this.GroupBoxSMSsettings.Controls.Add(this.btnSave);
            this.GroupBoxSMSsettings.Location = new System.Drawing.Point(439, 64);
            this.GroupBoxSMSsettings.Name = "GroupBoxSMSsettings";
            this.GroupBoxSMSsettings.Size = new System.Drawing.Size(409, 301);
            this.GroupBoxSMSsettings.TabIndex = 61;
            this.GroupBoxSMSsettings.TabStop = false;
            this.GroupBoxSMSsettings.Text = "Add /Edit  SMS Settings";
            // 
            // Btn_AddNew
            // 
            this.Btn_AddNew.BackColor = System.Drawing.Color.Turquoise;
            this.Btn_AddNew.FlatAppearance.BorderSize = 0;
            this.Btn_AddNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.Btn_AddNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_AddNew.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_AddNew.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_AddNew.Location = new System.Drawing.Point(101, 191);
            this.Btn_AddNew.Name = "Btn_AddNew";
            this.Btn_AddNew.Size = new System.Drawing.Size(106, 27);
            this.Btn_AddNew.TabIndex = 6;
            this.Btn_AddNew.Text = "&Add New ";
            this.Btn_AddNew.UseVisualStyleBackColor = false;
            this.Btn_AddNew.Click += new System.EventHandler(this.Btn_AddNew_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.ForeColor = System.Drawing.Color.Red;
            this.Label2.Location = new System.Drawing.Point(391, 120);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(15, 15);
            this.Label2.TabIndex = 70;
            this.Label2.Text = "* ";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.ForeColor = System.Drawing.Color.Red;
            this.Label1.Location = new System.Drawing.Point(392, 96);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(15, 15);
            this.Label1.TabIndex = 69;
            this.Label1.Text = "* ";
            // 
            // TxtPriority
            // 
            this.TxtPriority.Location = new System.Drawing.Point(57, 125);
            this.TxtPriority.MaxLength = 100;
            this.TxtPriority.Name = "TxtPriority";
            this.TxtPriority.Size = new System.Drawing.Size(156, 21);
            this.TxtPriority.TabIndex = 3;
            // 
            // LabelPriority
            // 
            this.LabelPriority.AutoSize = true;
            this.LabelPriority.Location = new System.Drawing.Point(6, 128);
            this.LabelPriority.Name = "LabelPriority";
            this.LabelPriority.Size = new System.Drawing.Size(48, 15);
            this.LabelPriority.TabIndex = 67;
            this.LabelPriority.Text = "Priority";
            // 
            // TxtName
            // 
            this.TxtName.Location = new System.Drawing.Point(57, 96);
            this.TxtName.MaxLength = 100;
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(332, 21);
            this.TxtName.TabIndex = 2;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.ForeColor = System.Drawing.Color.Red;
            this.Label4.Location = new System.Drawing.Point(395, 26);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(15, 15);
            this.Label4.TabIndex = 65;
            this.Label4.Text = "* ";
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.BackColor = System.Drawing.Color.Turquoise;
            this.Btn_Delete.FlatAppearance.BorderSize = 0;
            this.Btn_Delete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Btn_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Delete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Delete.Location = new System.Drawing.Point(158, 156);
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Size = new System.Drawing.Size(60, 27);
            this.Btn_Delete.TabIndex = 5;
            this.Btn_Delete.Text = "&Delete";
            this.Btn_Delete.UseVisualStyleBackColor = false;
            this.Btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // Btn_Update
            // 
            this.Btn_Update.BackColor = System.Drawing.Color.Turquoise;
            this.Btn_Update.FlatAppearance.BorderSize = 0;
            this.Btn_Update.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Btn_Update.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Btn_Update.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Update.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Update.Location = new System.Drawing.Point(92, 156);
            this.Btn_Update.Name = "Btn_Update";
            this.Btn_Update.Size = new System.Drawing.Size(60, 27);
            this.Btn_Update.TabIndex = 4;
            this.Btn_Update.Text = "&Update";
            this.Btn_Update.UseVisualStyleBackColor = false;
            this.Btn_Update.Click += new System.EventHandler(this.Btn_Update_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.BackColor = System.Drawing.Color.Turquoise;
            this.ButtonCancel.FlatAppearance.BorderSize = 0;
            this.ButtonCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.ButtonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ButtonCancel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonCancel.Location = new System.Drawing.Point(158, 156);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(60, 27);
            this.ButtonCancel.TabIndex = 5;
            this.ButtonCancel.Text = "&Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = false;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Location = new System.Drawing.Point(6, 99);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(40, 15);
            this.LabelName.TabIndex = 58;
            this.LabelName.Text = "Name";
            // 
            // LabelURL
            // 
            this.LabelURL.AutoSize = true;
            this.LabelURL.Location = new System.Drawing.Point(5, 23);
            this.LabelURL.Name = "LabelURL";
            this.LabelURL.Size = new System.Drawing.Size(30, 15);
            this.LabelURL.TabIndex = 57;
            this.LabelURL.Text = "URL";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(57, 22);
            this.txtURL.MaxLength = 100;
            this.txtURL.Multiline = true;
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(332, 71);
            this.txtURL.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Turquoise;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSave.Location = new System.Drawing.Point(92, 156);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 27);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // LabelError
            // 
            this.LabelError.AutoSize = true;
            this.LabelError.BackColor = System.Drawing.Color.Transparent;
            this.LabelError.ForeColor = System.Drawing.Color.Red;
            this.LabelError.Location = new System.Drawing.Point(487, 43);
            this.LabelError.Name = "LabelError";
            this.LabelError.Size = new System.Drawing.Size(0, 15);
            this.LabelError.TabIndex = 62;
            // 
            // AddSMSsettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(862, 395);
            this.Controls.Add(this.LabelError);
            this.Controls.Add(this.GroupBoxSMSsettings);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.DGVSMSsettings);
            this.Controls.Add(this.lblFrmName);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "AddSMSsettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddSMSsettings";
            this.Load += new System.EventHandler(this.AddSMSsettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVSMSsettings)).EndInit();
            this.GroupBoxSMSsettings.ResumeLayout(false);
            this.GroupBoxSMSsettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal System.Windows.Forms.Label lblFrmName;
        internal System.Windows.Forms.DataGridView DGVSMSsettings;
        //internal System.Windows.Forms.DataGridViewTextBoxColumn SMSName;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.GroupBox GroupBoxSMSsettings;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Button Btn_AddNew;
        internal System.Windows.Forms.Button Btn_Delete;
        internal System.Windows.Forms.Button Btn_Update;
        internal System.Windows.Forms.Button ButtonCancel;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Label LabelName;
        internal System.Windows.Forms.Label LabelURL;
        internal System.Windows.Forms.TextBox txtURL;
        internal System.Windows.Forms.TextBox TxtName;
        internal System.Windows.Forms.TextBox TxtPriority;
        internal System.Windows.Forms.Label LabelPriority;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label LabelError;
        internal System.Windows.Forms.DataGridViewLinkColumn SMS_Name;
        internal System.Windows.Forms.DataGridViewTextBoxColumn URL;
        internal System.Windows.Forms.DataGridViewTextBoxColumn priority;
    }

    //=======================================================
    //Service provided by Telerik (www.telerik.com)
    //Conversion powered by NRefactory.
    //Twitter: @telerik
    //Facebook: facebook.com/telerik
    //=======================================================

}
