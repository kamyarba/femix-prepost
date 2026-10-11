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
    public partial class element_prop_frm : Form
    {
        private string[] Editing_val;
        private bool is_new;
        public element_prop_frm()
        {
            InitializeComponent();
        }

        private void element_prop_frm_Load(object sender, EventArgs e)
        {
            is_new = false;
            combo_new.SelectedIndex = 0;
            Editing_val = FemixFRM_Class.Editing_val_Arr.ToArray();

            this.Text = "Element properties";
            groupBox1.Text = "Element properties";

            comb_group.Items.Clear();
            for (int i = 0; i < FemixFRM_Class.GROUP_NAMES_list_static.Count;i++)
                comb_group.Items.Add(FemixFRM_Class.GROUP_NAMES_list_static[i][2]);


            comb_elem_type.Items.Clear();
            comb_elem_type.Items.Add("_CABLE_2D");
            comb_elem_type.Items.Add("_CABLE_3D");
            comb_elem_type.Items.Add("_EMB_CABLE_2D");
            comb_elem_type.Items.Add("_EMB_CABLE_3D");
            comb_elem_type.Items.Add("_TRUSS_2D");
            comb_elem_type.Items.Add("_TRUSS_3D");
            comb_elem_type.Items.Add("_FRAME_2D");
            comb_elem_type.Items.Add("_FRAME_3D");
            comb_elem_type.Items.Add("_PLANE_STRESS_QUAD");
            comb_elem_type.Items.Add("_SOLID_HEXA");
            comb_elem_type.Items.Add("_TIMOSHENKO_BEAM_2D");
            comb_elem_type.Items.Add("_TIMOSHENKO_BEAM_3D");
            comb_elem_type.Items.Add("_MINDLIN_SHELL_QUAD");
            comb_elem_type.Items.Add("_MINDLIN_SLAB_QUAD");
            comb_elem_type.Items.Add("_AHMAD_SHELL_QUAD");




            comb_group.Text = Editing_val[0];
            txt_phase.Text = Editing_val[1];
            txt_range.Text = Editing_val[2];
            comb_elem_type.Text = Editing_val[3];

            comb_mat_type.Text = Editing_val[4];
            comb_mat_name.Text = Editing_val[5];
            comb_geo_type.Text = Editing_val[6];
            comb_geo_name.Text = Editing_val[7];
            comb_int_type.Text = Editing_val[8];
            comb_int_name.Text = Editing_val[9];

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

            Editing_val[0] = comb_group.Text;
            Editing_val[1] = txt_phase.Text;
            Editing_val[2] = txt_range.Text;

            Editing_val[3] = comb_elem_type.Text;

            Editing_val[4] = comb_mat_type.Text;
            Editing_val[5] = comb_mat_name.Text;
            Editing_val[6] = comb_geo_type.Text;
            Editing_val[7] = comb_geo_name.Text;
            Editing_val[8] = comb_int_type.Text;
            Editing_val[9] = comb_int_name.Text;


            FemixFRM_Class.Editing_val_Arr = Editing_val.ToArray();
            if (combo_new.SelectedIndex == 1)
                FemixFRM_Class.is_new = true;
            this.Close();
        }

        private void comb_elem_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comb_elem_type.Text == "")
            {

            }
            else
            {

            }
        }
    }
}
