
namespace GmshToFemix
{
    partial class frm_gmsh_groups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_gmsh_groups));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listbox_selected_element = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listbox_all_element = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listbox_selected_support = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listbox_all_support = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.listbox_selected_element);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listbox_all_element);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 146);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ELEMENT_NODES";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Selected Groups";
            // 
            // listbox_selected_element
            // 
            this.listbox_selected_element.FormattingEnabled = true;
            this.listbox_selected_element.Location = new System.Drawing.Point(245, 45);
            this.listbox_selected_element.Name = "listbox_selected_element";
            this.listbox_selected_element.Size = new System.Drawing.Size(219, 95);
            this.listbox_selected_element.TabIndex = 2;
            this.listbox_selected_element.DoubleClick += new System.EventHandler(this.listbox_selected_element_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "All Groups";
            // 
            // listbox_all_element
            // 
            this.listbox_all_element.FormattingEnabled = true;
            this.listbox_all_element.Location = new System.Drawing.Point(6, 45);
            this.listbox_all_element.Name = "listbox_all_element";
            this.listbox_all_element.Size = new System.Drawing.Size(233, 95);
            this.listbox_all_element.TabIndex = 0;
            this.listbox_all_element.SelectedIndexChanged += new System.EventHandler(this.listbox_all_element_SelectedIndexChanged);
            this.listbox_all_element.DoubleClick += new System.EventHandler(this.listbox_all_element_DoubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(199, 345);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.listbox_selected_support);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.listbox_all_support);
            this.groupBox2.Location = new System.Drawing.Point(13, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 146);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SUPPORTS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(321, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Selected Groups";
            // 
            // listbox_selected_support
            // 
            this.listbox_selected_support.FormattingEnabled = true;
            this.listbox_selected_support.Location = new System.Drawing.Point(245, 45);
            this.listbox_selected_support.Name = "listbox_selected_support";
            this.listbox_selected_support.Size = new System.Drawing.Size(219, 95);
            this.listbox_selected_support.TabIndex = 2;
            this.listbox_selected_support.DoubleClick += new System.EventHandler(this.listbox_selected_support_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "All Groups";
            // 
            // listbox_all_support
            // 
            this.listbox_all_support.FormattingEnabled = true;
            this.listbox_all_support.Location = new System.Drawing.Point(6, 45);
            this.listbox_all_support.Name = "listbox_all_support";
            this.listbox_all_support.Size = new System.Drawing.Size(233, 95);
            this.listbox_all_support.TabIndex = 0;
            this.listbox_all_support.SelectedIndexChanged += new System.EventHandler(this.listbox_all_support_SelectedIndexChanged);
            this.listbox_all_support.DoubleClick += new System.EventHandler(this.listbox_all_support_DoubleClick);
            // 
            // frm_gmsh_groups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 380);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_gmsh_groups";
            this.Text = "GMSH Groups";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listbox_selected_element;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listbox_all_element;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listbox_selected_support;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listbox_all_support;
    }
}