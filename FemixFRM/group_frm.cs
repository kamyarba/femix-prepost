using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FemixFRM
{
    public partial class group_frm : Form
    {
        private  string[] Editing_val;
        private  bool is_new;
        public group_frm()
        {
            InitializeComponent();
        }

        private void group_frm_Load(object sender, EventArgs e)
        {
            is_new = false;
            combo_new.SelectedIndex = 0;
            Editing_val = FemixFRM_Class.Editing_val_Arr.ToArray();

            this.Text = "Group Name";
            groupBox1.Text = "Group Name";

            textBox1.Text = Editing_val[0];

            //MessageBox.Show(FemixFRM_Class.GROUP_NAMES_list_static[0][2]);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

            Editing_val[0]=textBox1.Text;



            FemixFRM_Class.Editing_val_Arr = Editing_val.ToArray();
            if (combo_new.SelectedIndex == 1)
                FemixFRM_Class.is_new = true;
            this.Close();
        }
    }
}
