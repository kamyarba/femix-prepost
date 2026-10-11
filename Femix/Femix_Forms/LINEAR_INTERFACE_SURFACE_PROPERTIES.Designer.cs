
namespace Femix.Femix_Forms
{
    partial class LINEAR_INTERFACE_SURFACE_PROPERTIES
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LINEAR_INTERFACE_SURFACE_PROPERTIES));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_E = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_D = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_C = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_B = new System.Windows.Forms.TextBox();
            this.combo_new = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.comb_mat_lib = new System.Windows.Forms.ComboBox();
            this.btn_load = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(215, 214);
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
            this.btn_cancel.Location = new System.Drawing.Point(307, 214);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Dismiss";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_E);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_D);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_C);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_B);
            this.groupBox1.Location = new System.Drawing.Point(13, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 138);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GroupBox1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tangential stiffness - s2";
            // 
            // txt_E
            // 
            this.txt_E.Location = new System.Drawing.Point(249, 104);
            this.txt_E.Name = "txt_E";
            this.txt_E.Size = new System.Drawing.Size(167, 20);
            this.txt_E.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tangential stiffness - s1 ";
            // 
            // txt_D
            // 
            this.txt_D.Location = new System.Drawing.Point(249, 78);
            this.txt_D.Name = "txt_D";
            this.txt_D.Size = new System.Drawing.Size(167, 20);
            this.txt_D.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Normal stiffness";
            // 
            // txt_C
            // 
            this.txt_C.Location = new System.Drawing.Point(249, 52);
            this.txt_C.Name = "txt_C";
            this.txt_C.Size = new System.Drawing.Size(167, 20);
            this.txt_C.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name of the type of surface interface element";
            // 
            // txt_B
            // 
            this.txt_B.Location = new System.Drawing.Point(249, 26);
            this.txt_B.Name = "txt_B";
            this.txt_B.Size = new System.Drawing.Size(167, 20);
            this.txt_B.TabIndex = 0;
            // 
            // combo_new
            // 
            this.combo_new.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_new.FormattingEnabled = true;
            this.combo_new.Items.AddRange(new object[] {
            "Editing Item",
            "Add as new"});
            this.combo_new.Location = new System.Drawing.Point(13, 216);
            this.combo_new.Name = "combo_new";
            this.combo_new.Size = new System.Drawing.Size(121, 21);
            this.combo_new.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_save);
            this.groupBox2.Controls.Add(this.comb_mat_lib);
            this.groupBox2.Controls.Add(this.btn_load);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(13, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(516, 53);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Material Library";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(448, 17);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(61, 23);
            this.btn_save.TabIndex = 5;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // comb_mat_lib
            // 
            this.comb_mat_lib.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_mat_lib.FormattingEnabled = true;
            this.comb_mat_lib.Location = new System.Drawing.Point(111, 19);
            this.comb_mat_lib.Name = "comb_mat_lib";
            this.comb_mat_lib.Size = new System.Drawing.Size(225, 21);
            this.comb_mat_lib.TabIndex = 4;
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(380, 17);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(62, 23);
            this.btn_load.TabIndex = 3;
            this.btn_load.Text = "Load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Material name";
            // 
            // LINEAR_INTERFACE_SURFACE_PROPERTIES
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(543, 247);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.combo_new);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LINEAR_INTERFACE_SURFACE_PROPERTIES";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LINEAR_INTERFACE_SURFACE_PROPERTIES";
            this.Load += new System.EventHandler(this.group_frm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_B;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combo_new;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_E;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_D;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_C;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.ComboBox comb_mat_lib;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Label label10;
    }
}