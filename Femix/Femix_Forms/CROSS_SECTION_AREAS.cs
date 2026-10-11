using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Femix.Femix_Forms
{
    public partial class CROSS_SECTION_AREAS : Form
    {

        private string[] Editing_val;
        private bool is_new;
        public CROSS_SECTION_AREAS()
        {
            InitializeComponent();
        }

        private void group_frm_Load(object sender, EventArgs e)
        {
            is_new = false;
            combo_new.SelectedIndex = 0;
            Editing_val = FemixFRM_Class.Editing_val_Arr.ToArray();

            this.Text = "Cross section areas";
            groupBox1.Text = "Cross section areas";

            txt_name.Text = Editing_val[0];
            txt_thickness.Text = Editing_val[1];

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

            Editing_val[0] = txt_name.Text;
            Editing_val[1] = txt_thickness.Text;


            FemixFRM_Class.Editing_val_Arr = Editing_val.ToArray();
            if (combo_new.SelectedIndex == 1)
                FemixFRM_Class.is_new = true;
            this.Close();
        }

    }
}
