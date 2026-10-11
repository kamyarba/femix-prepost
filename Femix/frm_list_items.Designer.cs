
namespace Femix
{
    partial class frm_list_items
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_list_items));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer_bot = new System.Windows.Forms.SplitContainer();
            this.splitContainer_top = new System.Windows.Forms.SplitContainer();
            this.label_item = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.DG1 = new System.Windows.Forms.DataGridView();
            this.splitContainer_info_ver = new System.Windows.Forms.SplitContainer();
            this.txtinfo = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.Edit_context_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.frm_edit_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.frm_new_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateSelectedRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_bot)).BeginInit();
            this.splitContainer_bot.Panel1.SuspendLayout();
            this.splitContainer_bot.Panel2.SuspendLayout();
            this.splitContainer_bot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_top)).BeginInit();
            this.splitContainer_top.Panel1.SuspendLayout();
            this.splitContainer_top.Panel2.SuspendLayout();
            this.splitContainer_top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_info_ver)).BeginInit();
            this.splitContainer_info_ver.Panel1.SuspendLayout();
            this.splitContainer_info_ver.Panel2.SuspendLayout();
            this.splitContainer_info_ver.SuspendLayout();
            this.Edit_context_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer_bot);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1094, 527);
            this.panel1.TabIndex = 3;
            // 
            // splitContainer_bot
            // 
            this.splitContainer_bot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_bot.IsSplitterFixed = true;
            this.splitContainer_bot.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_bot.Name = "splitContainer_bot";
            this.splitContainer_bot.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_bot.Panel1
            // 
            this.splitContainer_bot.Panel1.Controls.Add(this.splitContainer_top);
            // 
            // splitContainer_bot.Panel2
            // 
            this.splitContainer_bot.Panel2.Controls.Add(this.btn_Ok);
            this.splitContainer_bot.Panel2.Controls.Add(this.btn_cancel);
            this.splitContainer_bot.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer_bot.Size = new System.Drawing.Size(1094, 527);
            this.splitContainer_bot.SplitterDistance = 483;
            this.splitContainer_bot.TabIndex = 0;
            // 
            // splitContainer_top
            // 
            this.splitContainer_top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_top.IsSplitterFixed = true;
            this.splitContainer_top.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_top.Name = "splitContainer_top";
            this.splitContainer_top.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_top.Panel1
            // 
            this.splitContainer_top.Panel1.Controls.Add(this.label_item);
            // 
            // splitContainer_top.Panel2
            // 
            this.splitContainer_top.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer_top.Size = new System.Drawing.Size(1094, 483);
            this.splitContainer_top.SplitterDistance = 26;
            this.splitContainer_top.TabIndex = 0;
            // 
            // label_item
            // 
            this.label_item.AutoSize = true;
            this.label_item.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_item.Location = new System.Drawing.Point(12, 6);
            this.label_item.Name = "label_item";
            this.label_item.Size = new System.Drawing.Size(28, 13);
            this.label_item.TabIndex = 1;
            this.label_item.Text = "Log";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.DG1);
            this.splitContainer3.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer3_Panel1_Paint);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer_info_ver);
            this.splitContainer3.Size = new System.Drawing.Size(1094, 453);
            this.splitContainer3.SplitterDistance = 253;
            this.splitContainer3.TabIndex = 0;
            // 
            // DG1
            // 
            this.DG1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DG1.Location = new System.Drawing.Point(0, 0);
            this.DG1.Name = "DG1";
            this.DG1.RowHeadersWidth = 102;
            this.DG1.Size = new System.Drawing.Size(1094, 253);
            this.DG1.TabIndex = 0;
            this.DG1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DG1_CellContentClick_1);
            this.DG1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DG1_CellFormatting);
            this.DG1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DG1_CellValueChanged);
            this.DG1.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.DG1_CellValueNeeded);
            this.DG1.ColumnStateChanged += new System.Windows.Forms.DataGridViewColumnStateChangedEventHandler(this.DG1_ColumnStateChanged);
            this.DG1.CurrentCellDirtyStateChanged += new System.EventHandler(this.DG1_CurrentCellDirtyStateChanged);
            this.DG1.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.DG1_RowStateChanged);
            // 
            // splitContainer_info_ver
            // 
            this.splitContainer_info_ver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_info_ver.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_info_ver.Name = "splitContainer_info_ver";
            // 
            // splitContainer_info_ver.Panel1
            // 
            this.splitContainer_info_ver.Panel1.Controls.Add(this.txtinfo);
            // 
            // splitContainer_info_ver.Panel2
            // 
            this.splitContainer_info_ver.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer_info_ver.Size = new System.Drawing.Size(1094, 196);
            this.splitContainer_info_ver.SplitterDistance = 714;
            this.splitContainer_info_ver.TabIndex = 0;
            // 
            // txtinfo
            // 
            this.txtinfo.BackColor = System.Drawing.SystemColors.Info;
            this.txtinfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtinfo.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.txtinfo.Location = new System.Drawing.Point(0, 0);
            this.txtinfo.Name = "txtinfo";
            this.txtinfo.ReadOnly = true;
            this.txtinfo.Size = new System.Drawing.Size(714, 196);
            this.txtinfo.TabIndex = 1;
            this.txtinfo.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(376, 196);
            this.tabControl1.TabIndex = 0;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(459, 8);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 1;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(551, 8);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 0;
            this.btn_cancel.Text = "Dismiss";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // Edit_context_menu
            // 
            this.Edit_context_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frm_edit_menu,
            this.frm_new_menu,
            this.toolStripMenuItem1,
            this.copyToClipboardToolStripMenuItem,
            this.duplicateSelectedRowToolStripMenuItem,
            this.deleteRowsToolStripMenuItem});
            this.Edit_context_menu.Name = "Edit_context_menu";
            this.Edit_context_menu.Size = new System.Drawing.Size(194, 120);
            // 
            // frm_edit_menu
            // 
            this.frm_edit_menu.Name = "frm_edit_menu";
            this.frm_edit_menu.Size = new System.Drawing.Size(193, 22);
            this.frm_edit_menu.Text = "Edit selected row";
            this.frm_edit_menu.Click += new System.EventHandler(this.frm_edit_menu_Click);
            // 
            // frm_new_menu
            // 
            this.frm_new_menu.Name = "frm_new_menu";
            this.frm_new_menu.Size = new System.Drawing.Size(193, 22);
            this.frm_new_menu.Text = "Add a new row";
            this.frm_new_menu.Click += new System.EventHandler(this.frm_new_menu_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 6);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // duplicateSelectedRowToolStripMenuItem
            // 
            this.duplicateSelectedRowToolStripMenuItem.Name = "duplicateSelectedRowToolStripMenuItem";
            this.duplicateSelectedRowToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.duplicateSelectedRowToolStripMenuItem.Text = "Duplicate selected row";
            this.duplicateSelectedRowToolStripMenuItem.Click += new System.EventHandler(this.duplicateSelectedRowToolStripMenuItem_Click);
            // 
            // deleteRowsToolStripMenuItem
            // 
            this.deleteRowsToolStripMenuItem.Name = "deleteRowsToolStripMenuItem";
            this.deleteRowsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.deleteRowsToolStripMenuItem.Text = "Delete selected row(s)";
            this.deleteRowsToolStripMenuItem.Click += new System.EventHandler(this.deleteRowsToolStripMenuItem_Click);
            // 
            // frm_list_items
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(1094, 527);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_list_items";
            this.Text = "frm_list_items";
            this.Load += new System.EventHandler(this.frm_list_items_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer_bot.Panel1.ResumeLayout(false);
            this.splitContainer_bot.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_bot)).EndInit();
            this.splitContainer_bot.ResumeLayout(false);
            this.splitContainer_top.Panel1.ResumeLayout(false);
            this.splitContainer_top.Panel1.PerformLayout();
            this.splitContainer_top.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_top)).EndInit();
            this.splitContainer_top.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DG1)).EndInit();
            this.splitContainer_info_ver.Panel1.ResumeLayout(false);
            this.splitContainer_info_ver.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_info_ver)).EndInit();
            this.splitContainer_info_ver.ResumeLayout(false);
            this.Edit_context_menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer_bot;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.SplitContainer splitContainer_top;
        private System.Windows.Forms.Label label_item;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer_info_ver;
        private System.Windows.Forms.DataGridView DG1;
        private System.Windows.Forms.RichTextBox txtinfo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ContextMenuStrip Edit_context_menu;
        private System.Windows.Forms.ToolStripMenuItem frm_edit_menu;
        private System.Windows.Forms.ToolStripMenuItem frm_new_menu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteRowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateSelectedRowToolStripMenuItem;
    }
}