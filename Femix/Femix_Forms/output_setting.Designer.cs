
namespace Femix.Femix_Forms
{
    partial class output_setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(output_setting));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_combo_help = new System.Windows.Forms.Label();
            this.chk_gr = new System.Windows.Forms.CheckBox();
            this.combo_group = new System.Windows.Forms.ComboBox();
            this.lbl_group = new System.Windows.Forms.Label();
            this.chk_cr = new System.Windows.Forms.CheckBox();
            this.chk_strain = new System.Windows.Forms.CheckBox();
            this.chk_stress = new System.Windows.Forms.CheckBox();
            this.chk_di = new System.Windows.Forms.CheckBox();
            this.chk_com = new System.Windows.Forms.CheckBox();
            this.numericUpDown_combo = new System.Windows.Forms.NumericUpDown();
            this.lbl_combo = new System.Windows.Forms.Label();
            this.lbl_layer_help = new System.Windows.Forms.Label();
            this.numericUpDown_layer = new System.Windows.Forms.NumericUpDown();
            this.lbl_layer = new System.Windows.Forms.Label();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_combo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_layer)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_combo_help);
            this.groupBox1.Controls.Add(this.chk_gr);
            this.groupBox1.Controls.Add(this.combo_group);
            this.groupBox1.Controls.Add(this.lbl_group);
            this.groupBox1.Controls.Add(this.chk_cr);
            this.groupBox1.Controls.Add(this.chk_strain);
            this.groupBox1.Controls.Add(this.chk_stress);
            this.groupBox1.Controls.Add(this.chk_di);
            this.groupBox1.Controls.Add(this.chk_com);
            this.groupBox1.Controls.Add(this.numericUpDown_combo);
            this.groupBox1.Controls.Add(this.lbl_combo);
            this.groupBox1.Controls.Add(this.lbl_layer_help);
            this.groupBox1.Controls.Add(this.numericUpDown_layer);
            this.groupBox1.Controls.Add(this.lbl_layer);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Output Settings";
            // 
            // lbl_combo_help
            // 
            this.lbl_combo_help.AutoSize = true;
            this.lbl_combo_help.Location = new System.Drawing.Point(344, 61);
            this.lbl_combo_help.Name = "lbl_combo_help";
            this.lbl_combo_help.Size = new System.Drawing.Size(46, 13);
            this.lbl_combo_help.TabIndex = 14;
            this.lbl_combo_help.Text = "(1-1000)";
            // 
            // chk_gr
            // 
            this.chk_gr.AutoSize = true;
            this.chk_gr.Location = new System.Drawing.Point(250, 83);
            this.chk_gr.Name = "chk_gr";
            this.chk_gr.Size = new System.Drawing.Size(74, 17);
            this.chk_gr.TabIndex = 13;
            this.chk_gr.Text = "All Groups";
            this.chk_gr.UseVisualStyleBackColor = true;
            this.chk_gr.CheckedChanged += new System.EventHandler(this.chk_gr_CheckedChanged);
            // 
            // combo_group
            // 
            this.combo_group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_group.FormattingEnabled = true;
            this.combo_group.Location = new System.Drawing.Point(110, 83);
            this.combo_group.Name = "combo_group";
            this.combo_group.Size = new System.Drawing.Size(121, 21);
            this.combo_group.TabIndex = 12;
            // 
            // lbl_group
            // 
            this.lbl_group.AutoSize = true;
            this.lbl_group.Location = new System.Drawing.Point(16, 86);
            this.lbl_group.Name = "lbl_group";
            this.lbl_group.Size = new System.Drawing.Size(36, 13);
            this.lbl_group.TabIndex = 11;
            this.lbl_group.Text = "Group";
            // 
            // chk_cr
            // 
            this.chk_cr.AutoSize = true;
            this.chk_cr.Checked = true;
            this.chk_cr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_cr.Location = new System.Drawing.Point(19, 144);
            this.chk_cr.Name = "chk_cr";
            this.chk_cr.Size = new System.Drawing.Size(90, 17);
            this.chk_cr.TabIndex = 10;
            this.chk_cr.Text = "Crack pattern";
            this.chk_cr.UseVisualStyleBackColor = true;
            // 
            // chk_strain
            // 
            this.chk_strain.AutoSize = true;
            this.chk_strain.Checked = true;
            this.chk_strain.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_strain.Location = new System.Drawing.Point(294, 121);
            this.chk_strain.Name = "chk_strain";
            this.chk_strain.Size = new System.Drawing.Size(58, 17);
            this.chk_strain.TabIndex = 9;
            this.chk_strain.Text = "Strains";
            this.chk_strain.UseVisualStyleBackColor = true;
            // 
            // chk_stress
            // 
            this.chk_stress.AutoSize = true;
            this.chk_stress.Checked = true;
            this.chk_stress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_stress.Location = new System.Drawing.Point(176, 121);
            this.chk_stress.Name = "chk_stress";
            this.chk_stress.Size = new System.Drawing.Size(55, 17);
            this.chk_stress.TabIndex = 8;
            this.chk_stress.Text = "Stress";
            this.chk_stress.UseVisualStyleBackColor = true;
            // 
            // chk_di
            // 
            this.chk_di.AutoSize = true;
            this.chk_di.Checked = true;
            this.chk_di.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_di.Location = new System.Drawing.Point(19, 121);
            this.chk_di.Name = "chk_di";
            this.chk_di.Size = new System.Drawing.Size(90, 17);
            this.chk_di.TabIndex = 7;
            this.chk_di.Text = "Displacement";
            this.chk_di.UseVisualStyleBackColor = true;
            // 
            // chk_com
            // 
            this.chk_com.AutoSize = true;
            this.chk_com.Location = new System.Drawing.Point(250, 60);
            this.chk_com.Name = "chk_com";
            this.chk_com.Size = new System.Drawing.Size(102, 17);
            this.chk_com.TabIndex = 6;
            this.chk_com.Text = "All combinations";
            this.chk_com.UseVisualStyleBackColor = true;
            this.chk_com.CheckedChanged += new System.EventHandler(this.chk_com_CheckedChanged);
            // 
            // numericUpDown_combo
            // 
            this.numericUpDown_combo.Location = new System.Drawing.Point(111, 57);
            this.numericUpDown_combo.Name = "numericUpDown_combo";
            this.numericUpDown_combo.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_combo.TabIndex = 4;
            // 
            // lbl_combo
            // 
            this.lbl_combo.AutoSize = true;
            this.lbl_combo.Location = new System.Drawing.Point(16, 59);
            this.lbl_combo.Name = "lbl_combo";
            this.lbl_combo.Size = new System.Drawing.Size(65, 13);
            this.lbl_combo.TabIndex = 3;
            this.lbl_combo.Text = "Combination";
            // 
            // lbl_layer_help
            // 
            this.lbl_layer_help.AutoSize = true;
            this.lbl_layer_help.Location = new System.Drawing.Point(247, 31);
            this.lbl_layer_help.Name = "lbl_layer_help";
            this.lbl_layer_help.Size = new System.Drawing.Size(19, 13);
            this.lbl_layer_help.TabIndex = 2;
            this.lbl_layer_help.Text = "<0";
            // 
            // numericUpDown_layer
            // 
            this.numericUpDown_layer.Location = new System.Drawing.Point(111, 31);
            this.numericUpDown_layer.Name = "numericUpDown_layer";
            this.numericUpDown_layer.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_layer.TabIndex = 1;
            // 
            // lbl_layer
            // 
            this.lbl_layer.AutoSize = true;
            this.lbl_layer.Location = new System.Drawing.Point(16, 33);
            this.lbl_layer.Name = "lbl_layer";
            this.lbl_layer.Size = new System.Drawing.Size(70, 13);
            this.lbl_layer.TabIndex = 0;
            this.lbl_layer.Text = "Element layer";
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(144, 188);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 5;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(236, 188);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "Dismiss";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // output_setting
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(447, 226);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "output_setting";
            this.Text = "Output Settings";
            this.Load += new System.EventHandler(this.output_setting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_combo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_layer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label lbl_layer;
        private System.Windows.Forms.NumericUpDown numericUpDown_layer;
        private System.Windows.Forms.Label lbl_layer_help;
        private System.Windows.Forms.CheckBox chk_di;
        private System.Windows.Forms.CheckBox chk_com;
        private System.Windows.Forms.NumericUpDown numericUpDown_combo;
        private System.Windows.Forms.Label lbl_combo;
        private System.Windows.Forms.CheckBox chk_stress;
        private System.Windows.Forms.CheckBox chk_cr;
        private System.Windows.Forms.CheckBox chk_strain;
        private System.Windows.Forms.Label lbl_combo_help;
        private System.Windows.Forms.CheckBox chk_gr;
        private System.Windows.Forms.ComboBox combo_group;
        private System.Windows.Forms.Label lbl_group;
    }
}