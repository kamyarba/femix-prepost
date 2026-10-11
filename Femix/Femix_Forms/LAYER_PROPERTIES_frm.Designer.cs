
namespace Femix.Femix_Forms
{
    partial class LAYER_PROPERTIES_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LAYER_PROPERTIES_frm));
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_geo_type = new System.Windows.Forms.Label();
            this.add_geo = new System.Windows.Forms.PictureBox();
            this.add_mat = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comb_geo_name = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comb_geo_type = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comb_mat_name = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comb_mat_type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_layer_name = new System.Windows.Forms.TextBox();
            this.combo_new = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.add_geo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_mat)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(206, 206);
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
            this.btn_cancel.Location = new System.Drawing.Point(298, 206);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Dismiss";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_geo_type);
            this.groupBox1.Controls.Add(this.add_geo);
            this.groupBox1.Controls.Add(this.add_mat);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.comb_geo_name);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comb_geo_type);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.comb_mat_name);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comb_mat_type);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_layer_name);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(550, 187);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GroupBox1";
            // 
            // lbl_geo_type
            // 
            this.lbl_geo_type.AutoSize = true;
            this.lbl_geo_type.Location = new System.Drawing.Point(384, 115);
            this.lbl_geo_type.Name = "lbl_geo_type";
            this.lbl_geo_type.Size = new System.Drawing.Size(11, 13);
            this.lbl_geo_type.TabIndex = 39;
            this.lbl_geo_type.Text = "*";
            // 
            // add_geo
            // 
            this.add_geo.BackgroundImage = global::Femix.Properties.Resources.icons8_add_new_black;
            this.add_geo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.add_geo.Location = new System.Drawing.Point(387, 137);
            this.add_geo.Name = "add_geo";
            this.add_geo.Size = new System.Drawing.Size(23, 23);
            this.add_geo.TabIndex = 38;
            this.add_geo.TabStop = false;
            this.toolTip1.SetToolTip(this.add_geo, "Add a new item");
            this.add_geo.Click += new System.EventHandler(this.add_geo_Click);
            // 
            // add_mat
            // 
            this.add_mat.BackgroundImage = global::Femix.Properties.Resources.icons8_add_new_black;
            this.add_mat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.add_mat.Location = new System.Drawing.Point(387, 83);
            this.add_mat.Name = "add_mat";
            this.add_mat.Size = new System.Drawing.Size(23, 23);
            this.add_mat.TabIndex = 37;
            this.add_mat.TabStop = false;
            this.toolTip1.SetToolTip(this.add_mat, "Add a new item");
            this.add_mat.Click += new System.EventHandler(this.add_mat_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 36;
            this.label8.Text = "Geometry name";
            // 
            // comb_geo_name
            // 
            this.comb_geo_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_geo_name.FormattingEnabled = true;
            this.comb_geo_name.Location = new System.Drawing.Point(190, 139);
            this.comb_geo_name.Name = "comb_geo_name";
            this.comb_geo_name.Size = new System.Drawing.Size(188, 21);
            this.comb_geo_name.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Geometry type";
            // 
            // comb_geo_type
            // 
            this.comb_geo_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_geo_type.FormattingEnabled = true;
            this.comb_geo_type.Location = new System.Drawing.Point(190, 112);
            this.comb_geo_type.Name = "comb_geo_type";
            this.comb_geo_type.Size = new System.Drawing.Size(188, 21);
            this.comb_geo_type.TabIndex = 33;
            this.comb_geo_type.SelectedIndexChanged += new System.EventHandler(this.comb_geo_type_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Material name";
            // 
            // comb_mat_name
            // 
            this.comb_mat_name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_mat_name.FormattingEnabled = true;
            this.comb_mat_name.Location = new System.Drawing.Point(190, 85);
            this.comb_mat_name.Name = "comb_mat_name";
            this.comb_mat_name.Size = new System.Drawing.Size(188, 21);
            this.comb_mat_name.TabIndex = 31;
            this.comb_mat_name.SelectedIndexChanged += new System.EventHandler(this.comb_mat_name_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Material type";
            // 
            // comb_mat_type
            // 
            this.comb_mat_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_mat_type.FormattingEnabled = true;
            this.comb_mat_type.Location = new System.Drawing.Point(190, 58);
            this.comb_mat_type.Name = "comb_mat_type";
            this.comb_mat_type.Size = new System.Drawing.Size(188, 21);
            this.comb_mat_type.TabIndex = 29;
            this.comb_mat_type.SelectedIndexChanged += new System.EventHandler(this.comb_mat_type_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Layer name";
            // 
            // txt_layer_name
            // 
            this.txt_layer_name.Location = new System.Drawing.Point(190, 32);
            this.txt_layer_name.Name = "txt_layer_name";
            this.txt_layer_name.Size = new System.Drawing.Size(188, 20);
            this.txt_layer_name.TabIndex = 0;
            // 
            // combo_new
            // 
            this.combo_new.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_new.FormattingEnabled = true;
            this.combo_new.Items.AddRange(new object[] {
            "Editing Item",
            "Add as new"});
            this.combo_new.Location = new System.Drawing.Point(13, 206);
            this.combo_new.Name = "combo_new";
            this.combo_new.Size = new System.Drawing.Size(121, 21);
            this.combo_new.TabIndex = 2;
            // 
            // LAYER_PROPERTIES_frm
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(575, 239);
            this.Controls.Add(this.combo_new);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LAYER_PROPERTIES_frm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LAYER_PROPERTIES_frm";
            this.Load += new System.EventHandler(this.group_frm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.add_geo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_mat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_layer_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combo_new;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbl_geo_type;
        private System.Windows.Forms.PictureBox add_geo;
        private System.Windows.Forms.PictureBox add_mat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comb_geo_name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comb_geo_type;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comb_mat_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comb_mat_type;
    }
}