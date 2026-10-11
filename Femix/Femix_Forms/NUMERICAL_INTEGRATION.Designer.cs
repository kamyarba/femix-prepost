
namespace Femix.Femix_Forms
{
    partial class NUMERICAL_INTEGRATION
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NUMERICAL_INTEGRATION));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_auto_int = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comb_elem_type = new System.Windows.Forms.ComboBox();
            this.txt_nkeyword = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_ndir0 = new System.Windows.Forms.NumericUpDown();
            this.lbl_npoint0 = new System.Windows.Forms.Label();
            this.comb_dir0 = new System.Windows.Forms.ComboBox();
            this.lbl_dir0 = new System.Windows.Forms.Label();
            this.comb_term0 = new System.Windows.Forms.ComboBox();
            this.lbl_term0 = new System.Windows.Forms.Label();
            this.lbl_count0 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.combo_new = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_nkeyword)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ndir0)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(226, 383);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 3;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(318, 383);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Dismiss";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txt_nkeyword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_name);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 364);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GroupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_auto_int);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.comb_elem_type);
            this.groupBox2.Location = new System.Drawing.Point(9, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(539, 52);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Auto Generation";
            // 
            // btn_auto_int
            // 
            this.btn_auto_int.Location = new System.Drawing.Point(365, 18);
            this.btn_auto_int.Name = "btn_auto_int";
            this.btn_auto_int.Size = new System.Drawing.Size(168, 23);
            this.btn_auto_int.TabIndex = 13;
            this.btn_auto_int.Text = "Generate numerical integration";
            this.btn_auto_int.UseVisualStyleBackColor = true;
            this.btn_auto_int.Click += new System.EventHandler(this.btn_auto_int_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Element type";
            // 
            // comb_elem_type
            // 
            this.comb_elem_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_elem_type.FormattingEnabled = true;
            this.comb_elem_type.Location = new System.Drawing.Point(91, 19);
            this.comb_elem_type.Name = "comb_elem_type";
            this.comb_elem_type.Size = new System.Drawing.Size(265, 21);
            this.comb_elem_type.TabIndex = 11;
            // 
            // txt_nkeyword
            // 
            this.txt_nkeyword.Location = new System.Drawing.Point(481, 90);
            this.txt_nkeyword.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txt_nkeyword.Name = "txt_nkeyword";
            this.txt_nkeyword.Size = new System.Drawing.Size(67, 20);
            this.txt_nkeyword.TabIndex = 4;
            this.txt_nkeyword.ValueChanged += new System.EventHandler(this.txt_nkeyword_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(371, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of keywords";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txt_ndir0);
            this.panel1.Controls.Add(this.lbl_npoint0);
            this.panel1.Controls.Add(this.comb_dir0);
            this.panel1.Controls.Add(this.lbl_dir0);
            this.panel1.Controls.Add(this.comb_term0);
            this.panel1.Controls.Add(this.lbl_term0);
            this.panel1.Controls.Add(this.lbl_count0);
            this.panel1.Location = new System.Drawing.Point(6, 123);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 235);
            this.panel1.TabIndex = 2;
            // 
            // txt_ndir0
            // 
            this.txt_ndir0.Location = new System.Drawing.Point(442, 10);
            this.txt_ndir0.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txt_ndir0.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_ndir0.Name = "txt_ndir0";
            this.txt_ndir0.Size = new System.Drawing.Size(66, 20);
            this.txt_ndir0.TabIndex = 18;
            this.txt_ndir0.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.txt_ndir0.Visible = false;
            // 
            // lbl_npoint0
            // 
            this.lbl_npoint0.AutoSize = true;
            this.lbl_npoint0.Location = new System.Drawing.Point(349, 13);
            this.lbl_npoint0.Name = "lbl_npoint0";
            this.lbl_npoint0.Size = new System.Drawing.Size(87, 13);
            this.lbl_npoint0.TabIndex = 17;
            this.lbl_npoint0.Text = "Number of points";
            this.lbl_npoint0.Visible = false;
            // 
            // comb_dir0
            // 
            this.comb_dir0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_dir0.FormattingEnabled = true;
            this.comb_dir0.Location = new System.Drawing.Point(284, 10);
            this.comb_dir0.Name = "comb_dir0";
            this.comb_dir0.Size = new System.Drawing.Size(53, 21);
            this.comb_dir0.TabIndex = 16;
            this.comb_dir0.Tag = "";
            this.toolTip1.SetToolTip(this.comb_dir0, "Direction s1\r\nDirection s2\r\nDirection s3");
            this.comb_dir0.Visible = false;
            // 
            // lbl_dir0
            // 
            this.lbl_dir0.AutoSize = true;
            this.lbl_dir0.Location = new System.Drawing.Point(229, 13);
            this.lbl_dir0.Name = "lbl_dir0";
            this.lbl_dir0.Size = new System.Drawing.Size(49, 13);
            this.lbl_dir0.TabIndex = 15;
            this.lbl_dir0.Text = "Direction";
            this.lbl_dir0.Visible = false;
            // 
            // comb_term0
            // 
            this.comb_term0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_term0.FormattingEnabled = true;
            this.comb_term0.Location = new System.Drawing.Point(116, 10);
            this.comb_term0.Name = "comb_term0";
            this.comb_term0.Size = new System.Drawing.Size(95, 21);
            this.comb_term0.TabIndex = 14;
            this.comb_term0.Tag = "";
            this.toolTip1.SetToolTip(this.comb_term0, "General use (stiffness matrix, springs and load vector)\r\nMembrane terms (stiffnes" +
        "s matrix)\r\nShear terms (stiffness matrix)\r\nTorsional terms (stiffness matrix)\r\nB" +
        "ending terms (stiffness matrix)");
            this.comb_term0.Visible = false;
            // 
            // lbl_term0
            // 
            this.lbl_term0.AutoSize = true;
            this.lbl_term0.Location = new System.Drawing.Point(35, 13);
            this.lbl_term0.Name = "lbl_term0";
            this.lbl_term0.Size = new System.Drawing.Size(75, 13);
            this.lbl_term0.TabIndex = 1;
            this.lbl_term0.Text = "Type of Terms";
            this.lbl_term0.Visible = false;
            // 
            // lbl_count0
            // 
            this.lbl_count0.AutoSize = true;
            this.lbl_count0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.lbl_count0.Location = new System.Drawing.Point(10, 13);
            this.lbl_count0.Name = "lbl_count0";
            this.lbl_count0.Size = new System.Drawing.Size(14, 13);
            this.lbl_count0.TabIndex = 0;
            this.lbl_count0.Text = "1";
            this.lbl_count0.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Integration Name";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(100, 90);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(265, 20);
            this.txt_name.TabIndex = 0;
            // 
            // combo_new
            // 
            this.combo_new.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_new.FormattingEnabled = true;
            this.combo_new.Items.AddRange(new object[] {
            "Editing Item",
            "Add as new"});
            this.combo_new.Location = new System.Drawing.Point(11, 385);
            this.combo_new.Name = "combo_new";
            this.combo_new.Size = new System.Drawing.Size(121, 21);
            this.combo_new.TabIndex = 2;
            // 
            // NUMERICAL_INTEGRATION
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(588, 420);
            this.Controls.Add(this.combo_new);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NUMERICAL_INTEGRATION";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NUMERICAL_INTEGRATION";
            this.Load += new System.EventHandler(this.group_frm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_nkeyword)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ndir0)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combo_new;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown txt_nkeyword;
        private System.Windows.Forms.Button btn_auto_int;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comb_elem_type;
        private System.Windows.Forms.NumericUpDown txt_ndir0;
        private System.Windows.Forms.Label lbl_npoint0;
        private System.Windows.Forms.ComboBox comb_dir0;
        private System.Windows.Forms.Label lbl_dir0;
        private System.Windows.Forms.ComboBox comb_term0;
        private System.Windows.Forms.Label lbl_term0;
        private System.Windows.Forms.Label lbl_count0;
    }
}