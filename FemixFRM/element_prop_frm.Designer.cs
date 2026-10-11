
namespace FemixFRM
{
    partial class element_prop_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(element_prop_frm));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comb_int_name = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comb_int_type = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comb_geo_name = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comb_geo_type = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comb_mat_name = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comb_mat_type = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comb_elem_type = new System.Windows.Forms.ComboBox();
            this.txt_range = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_phase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comb_group = new System.Windows.Forms.ComboBox();
            this.combo_new = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(233, 388);
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
            this.btn_cancel.Location = new System.Drawing.Point(325, 388);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Dismiss";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.comb_int_name);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.comb_int_type);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comb_geo_name);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comb_geo_type);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comb_mat_name);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comb_mat_type);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comb_elem_type);
            this.groupBox1.Controls.Add(this.txt_range);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_phase);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comb_group);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(625, 349);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group Box1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(368, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "1 or [1-100]";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 271);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(166, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Integration name (stiffness matrix) ";
            // 
            // comb_int_name
            // 
            this.comb_int_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_int_name.FormattingEnabled = true;
            this.comb_int_name.Location = new System.Drawing.Point(174, 271);
            this.comb_int_name.Name = "comb_int_name";
            this.comb_int_name.Size = new System.Drawing.Size(188, 21);
            this.comb_int_name.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 244);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(160, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Integration type (stiffness matrix) ";
            // 
            // comb_int_type
            // 
            this.comb_int_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_int_type.FormattingEnabled = true;
            this.comb_int_type.Location = new System.Drawing.Point(174, 244);
            this.comb_int_type.Name = "comb_int_type";
            this.comb_int_type.Size = new System.Drawing.Size(188, 21);
            this.comb_int_type.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Geometry name";
            // 
            // comb_geo_name
            // 
            this.comb_geo_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_geo_name.FormattingEnabled = true;
            this.comb_geo_name.Location = new System.Drawing.Point(174, 217);
            this.comb_geo_name.Name = "comb_geo_name";
            this.comb_geo_name.Size = new System.Drawing.Size(188, 21);
            this.comb_geo_name.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Geometry type";
            // 
            // comb_geo_type
            // 
            this.comb_geo_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_geo_type.FormattingEnabled = true;
            this.comb_geo_type.Location = new System.Drawing.Point(174, 190);
            this.comb_geo_type.Name = "comb_geo_type";
            this.comb_geo_type.Size = new System.Drawing.Size(188, 21);
            this.comb_geo_type.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Material name";
            // 
            // comb_mat_name
            // 
            this.comb_mat_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_mat_name.FormattingEnabled = true;
            this.comb_mat_name.Location = new System.Drawing.Point(174, 163);
            this.comb_mat_name.Name = "comb_mat_name";
            this.comb_mat_name.Size = new System.Drawing.Size(188, 21);
            this.comb_mat_name.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Material type";
            // 
            // comb_mat_type
            // 
            this.comb_mat_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_mat_type.FormattingEnabled = true;
            this.comb_mat_type.Location = new System.Drawing.Point(174, 136);
            this.comb_mat_type.Name = "comb_mat_type";
            this.comb_mat_type.Size = new System.Drawing.Size(188, 21);
            this.comb_mat_type.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Element type";
            // 
            // comb_elem_type
            // 
            this.comb_elem_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_elem_type.FormattingEnabled = true;
            this.comb_elem_type.Location = new System.Drawing.Point(174, 109);
            this.comb_elem_type.Name = "comb_elem_type";
            this.comb_elem_type.Size = new System.Drawing.Size(188, 21);
            this.comb_elem_type.TabIndex = 9;
            this.comb_elem_type.SelectedIndexChanged += new System.EventHandler(this.comb_elem_type_SelectedIndexChanged);
            // 
            // txt_range
            // 
            this.txt_range.Location = new System.Drawing.Point(174, 83);
            this.txt_range.Name = "txt_range";
            this.txt_range.Size = new System.Drawing.Size(188, 20);
            this.txt_range.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Element (or element range) ";
            // 
            // txt_phase
            // 
            this.txt_phase.Location = new System.Drawing.Point(174, 57);
            this.txt_phase.Name = "txt_phase";
            this.txt_phase.Size = new System.Drawing.Size(188, 20);
            this.txt_phase.TabIndex = 6;
            this.txt_phase.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Phase (or phase range)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Group Name";
            // 
            // comb_group
            // 
            this.comb_group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_group.FormattingEnabled = true;
            this.comb_group.Location = new System.Drawing.Point(174, 30);
            this.comb_group.Name = "comb_group";
            this.comb_group.Size = new System.Drawing.Size(188, 21);
            this.comb_group.TabIndex = 3;
            // 
            // combo_new
            // 
            this.combo_new.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_new.FormattingEnabled = true;
            this.combo_new.Items.AddRange(new object[] {
            "Editing Item",
            "Add as new"});
            this.combo_new.Location = new System.Drawing.Point(12, 388);
            this.combo_new.Name = "combo_new";
            this.combo_new.Size = new System.Drawing.Size(121, 21);
            this.combo_new.TabIndex = 2;
            // 
            // element_prop_frm
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(815, 421);
            this.Controls.Add(this.combo_new);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "element_prop_frm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "group_frm";
            this.Load += new System.EventHandler(this.element_prop_frm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox combo_new;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comb_int_type;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comb_geo_name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comb_geo_type;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comb_mat_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comb_mat_type;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comb_elem_type;
        private System.Windows.Forms.TextBox txt_range;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_phase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comb_group;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comb_int_name;
    }
}