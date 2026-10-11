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
    public partial class LAYER_PATTERNS_frm : Form
    {

        private string[] Editing_val;
        private bool is_new;
        public LAYER_PATTERNS_frm()
        {
            InitializeComponent();
        }

        private void group_frm_Load(object sender, EventArgs e)
        {
            is_new = false;
            combo_new.SelectedIndex = 0;
            Editing_val = FemixFRM_Class.Editing_val_Arr.ToArray();

            this.Text = "Layer Patterns";
            groupBox1.Text = "Layer Patterns";


            txt_name.Text = Editing_val[0];
            txt_n_layer.Text = Editing_val[1];
            txt_layer.Text = Editing_val[2];

            comb_layer_pro.Items.Clear();
            for (int i = 0; i < C_variables.LAYER_PROPERTIES_list.Count; i++)
                comb_layer_pro.Items.Add(C_variables.LAYER_PROPERTIES_list[i][2]);

            comb_layer_pro.Text = Editing_val[3];



        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            Editing_val[0] = txt_name.Text;
            Editing_val[1] = txt_n_layer.Text;
            Editing_val[2] = txt_layer.Text;
            Editing_val[3] = comb_layer_pro.Text;


            FemixFRM_Class.Editing_val_Arr = Editing_val.ToArray();
            if (combo_new.SelectedIndex == 1)
                FemixFRM_Class.is_new = true;
            this.Close();
        }

        private void add_layer_Click(object sender, EventArgs e)
        {
            FemixFRM_Class.generate_combo_item("LAYER_PROPERTIES", C_variables.LAYER_PROPERTIES_list, 2, comb_layer_pro);
        }
    }
}
