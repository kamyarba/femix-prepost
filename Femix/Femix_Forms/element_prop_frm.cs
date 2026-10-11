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
            for (int i = 0; i < C_variables.GROUP_NAMES_list.Count; i++)
                comb_group.Items.Add(C_variables.GROUP_NAMES_list[i][2]);


            comb_elem_type.Items.Clear();
            comb_elem_type.Items.Add("_TRUSS_2D");
            comb_elem_type.Items.Add("_TRUSS_3D");
            comb_elem_type.Items.Add("_FRAME_2D");
            comb_elem_type.Items.Add("_FRAME_3D");
            comb_elem_type.Items.Add("_CABLE_2D");
            comb_elem_type.Items.Add("_CABLE_3D");
            comb_elem_type.Items.Add("_TIMOSHENKO_BEAM_2D");
            comb_elem_type.Items.Add("_TIMOSHENKO_BEAM_3D");
            comb_elem_type.Items.Add("_PLANE_STRESS_TRIANG");
            comb_elem_type.Items.Add("_PLANE_STRESS_QUAD");
            comb_elem_type.Items.Add("_PLANE_STRESS_QUAD_THERMAL");
            comb_elem_type.Items.Add("_PLANE_STRAIN_TRIANG");
            comb_elem_type.Items.Add("_PLANE_STRAIN_QUAD");
            comb_elem_type.Items.Add("_AXIS_LINE");
            comb_elem_type.Items.Add("_AXIS_TRIANG");
            comb_elem_type.Items.Add("_AXIS_QUAD");
            comb_elem_type.Items.Add("_MINDLIN_SLAB_TRIANG");
            comb_elem_type.Items.Add("_MINDLIN_SLAB_QUAD");
            comb_elem_type.Items.Add("_MINDLIN_SHELL_TRIANG");
            comb_elem_type.Items.Add("_MINDLIN_SHELL_QUAD");
            comb_elem_type.Items.Add("_AHMAD_SHELL_TRIANG");
            comb_elem_type.Items.Add("_AHMAD_SHELL_QUAD");
            comb_elem_type.Items.Add("_SOLID_TETRA");
            comb_elem_type.Items.Add("_SOLID_HEXA");
            comb_elem_type.Items.Add("_SOLID_HEXA_THERMAL");
            comb_elem_type.Items.Add("_INTERFACE_POINT");
            comb_elem_type.Items.Add("_INTERFACE_LINE_2D");
            comb_elem_type.Items.Add("_INTERFACE_LINE_3D");
            comb_elem_type.Items.Add("_INTERFACE_TRIANG");
            comb_elem_type.Items.Add("_INTERFACE_QUAD");
            comb_elem_type.Items.Add("_EMB_CABLE_2D");
            comb_elem_type.Items.Add("_EMB_CABLE_3D");




            comb_group.Text = Editing_val[0];
            txt_phase.Text = Editing_val[1];
            txt_range.Text = Editing_val[2];
            comb_elem_type.Text = Editing_val[3];



            comb_int_type.Items.Clear();
            comb_int_type.Items.Add("_NONE");
            comb_int_type.Items.Add("_GLEG");
            comb_int_type.Items.Add("_GLOB");
            comb_int_type.Items.Add("_NCOTES");
            comb_int_type.SelectedIndex = 1;
            comb_int_type.Text = Editing_val[8];


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

        private void fill_combo_mat_type()
        {
            comb_mat_type.Items.Clear();
            switch (comb_elem_type.Text)
            {
                case "_TRUSS_2D":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    break;
                case "_TRUSS_3D":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    break;
                case "_FRAME_2D":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    break;
                case "_FRAME_3D":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    break;
                case "_CABLE_2D":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    comb_mat_type.Items.Add("_NLMM201");
                    comb_mat_type.Items.Add("_NLMM202");
                    break;
                case "_CABLE_3D":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    comb_mat_type.Items.Add("_NLMM201");
                    comb_mat_type.Items.Add("_NLMM202");
                    break;
                case "_TIMOSHENKO_BEAM_2D": 
                    break;
                case "_TIMOSHENKO_BEAM_3D":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    comb_mat_type.Items.Add("_NLMM201");
                    comb_mat_type.Items.Add("_NLMM202");
                    comb_mat_type.Items.Add("_NLMM210");
                    comb_mat_type.Items.Add("_NLMM220");
                    comb_mat_type.Items.Add("_NLMM298");
                    comb_mat_type.Items.Add("_NLMM299");
                    break;
                case "_PLANE_STRESS_TRIANG":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    break;
                case "_PLANE_STRESS_QUAD":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    comb_mat_type.Items.Add("_NLMM101");
                    comb_mat_type.Items.Add("_NLMM104");
                    comb_mat_type.Items.Add("_NLMM121");
                    comb_mat_type.Items.Add("_NLMM123");
                    comb_mat_type.Items.Add("_NLMM124");
                    comb_mat_type.Items.Add("_NLMM125");
                    comb_mat_type.Items.Add("_NLMM141");
                    comb_mat_type.Items.Add("_NLMM142");
                    comb_mat_type.Items.Add("_NLMM145");
                    comb_mat_type.Items.Add("_NLMM211");

                    break;
                case "_PLANE_STRESS_QUAD_THERMAL":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    break;
                case "_PLANE_STRAIN_TRIANG":
                    comb_mat_type.Items.Add("_NONE");
                    break;
                case "_PLANE_STRAIN_QUAD":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    comb_mat_type.Items.Add("_NLMM101");
                    comb_mat_type.Items.Add("_NLMM104");
                    comb_mat_type.Items.Add("_NLMM121");
                    comb_mat_type.Items.Add("_NLMM123");
                    comb_mat_type.Items.Add("_NLMM124");
                    comb_mat_type.Items.Add("_NLMM125");
                    comb_mat_type.Items.Add("_NLMM141");
                    comb_mat_type.Items.Add("_NLMM142");
                    break;
                case "_AXIS_LINE":
                    comb_mat_type.Items.Add("_NONE");
                    break;
                case "_AXIS_TRIANG":
                    comb_mat_type.Items.Add("_NONE");
                    break;
                case "_AXIS_QUAD":
                    comb_mat_type.Items.Add("_NONE");
                    break;
                case "_MINDLIN_SLAB_TRIANG":
                    comb_mat_type.Items.Add("_NONE");
                    break;
                case "_MINDLIN_SLAB_QUAD":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    break;
                case "_MINDLIN_SHELL_TRIANG":
                    comb_mat_type.Items.Add("_NONE");
                    break;
                case "_MINDLIN_SHELL_QUAD":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    comb_mat_type.Items.Add("_LIN_ORTHO");
                    comb_mat_type.Items.Add("_NLMM101");
                    comb_mat_type.Items.Add("_NLMM104");
                    comb_mat_type.Items.Add("_NLMM121");
                    comb_mat_type.Items.Add("_NLMM141");
                    comb_mat_type.Items.Add("_NLMM142");
                    comb_mat_type.Items.Add("_NLMM211");
                    break;
                case "_AHMAD_SHELL_TRIANG":
                    comb_mat_type.Items.Add("_NONE");
                    break;
                case "_AHMAD_SHELL_QUAD":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    comb_mat_type.Items.Add("_LIN_ORTHO");
                    comb_mat_type.Items.Add("_NLMM101");
                    comb_mat_type.Items.Add("_NLMM104");
                    comb_mat_type.Items.Add("_NLMM121");
                    comb_mat_type.Items.Add("_NLMM141");
                    comb_mat_type.Items.Add("_NLMM142");                              
                    break;
                case "_SOLID_TETRA":
                    comb_mat_type.Items.Add("_NONE");
                    break;
                case "_SOLID_HEXA":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    comb_mat_type.Items.Add("_NLMM102");
                    comb_mat_type.Items.Add("_NLMM111");
                    comb_mat_type.Items.Add("_NLMM121");
                    comb_mat_type.Items.Add("_NLMM122");
                    comb_mat_type.Items.Add("_NLMM123");
                    comb_mat_type.Items.Add("_NLMM124");
                    comb_mat_type.Items.Add("_NLMM125");
                    comb_mat_type.Items.Add("_NLMM151");
                    break;
                case "_SOLID_HEXA_THERMAL":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    comb_mat_type.Items.Add("_NLMM401");
                    comb_mat_type.Items.Add("_NLMM451");
                    break;
                case "_INTERFACE_POINT":
                    comb_mat_type.Items.Add("_LIN_INT_POINT");
                    break;
                case "_INTERFACE_LINE_2D":
                    comb_mat_type.Items.Add("_LIN_INT_LINE");
                    comb_mat_type.Items.Add("_NLMM301");
                    comb_mat_type.Items.Add("_NLMM302");
                    comb_mat_type.Items.Add("_NLMM303");
                    comb_mat_type.Items.Add("_NLMM305");
                    comb_mat_type.Items.Add("_NLMM306");
                    comb_mat_type.Items.Add("_NLMM320");

                    break;
                case "_INTERFACE_LINE_3D":
                    comb_mat_type.Items.Add("_LIN_INT_LINE");
                    comb_mat_type.Items.Add("_NLMM303");
                    comb_mat_type.Items.Add("_NLMM320");
                    break;
                case "_INTERFACE_TRIANG":
                    comb_mat_type.Items.Add("_NONE");
                    break;
                case "_INTERFACE_QUAD":
                    comb_mat_type.Items.Add("_LIN_INT_SURF");
                    comb_mat_type.Items.Add("_NLMM305");
                    comb_mat_type.Items.Add("_NLMM306");
                    comb_mat_type.Items.Add("_NLMM320");
                    break;
                case "_EMB_CABLE_2D":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    comb_mat_type.Items.Add("_NLMM201");
                    comb_mat_type.Items.Add("_NLMM202");
                    break;
                case "_EMB_CABLE_3D":
                    comb_mat_type.Items.Add("_LIN_ISO");
                    comb_mat_type.Items.Add("_NLMM201");
                    comb_mat_type.Items.Add("_NLMM202");
                    break;
                default:
                    break;
            }

            //comb_mat_type.Items.Add("_LIN_ISO");
            //comb_mat_type.Items.Add("_LIN_ORTHO");
            //comb_mat_type.Items.Add("_NLMM101");
            //comb_mat_type.Items.Add("_NLMM104");
            //comb_mat_type.Items.Add("_NLMM111");
            //comb_mat_type.Items.Add("_NLMM141");
            //comb_mat_type.Items.Add("_NLMM151");
            //comb_mat_type.Items.Add("_NLMM201");
            //comb_mat_type.Items.Add("_NLMM202");
            //comb_mat_type.Items.Add("_NLMM210");
            //comb_mat_type.Items.Add("_NLMM220");
            //comb_mat_type.Items.Add("_NLMM298");
            //comb_mat_type.Items.Add("_NLMM299");
            //comb_mat_type.Items.Add("_NLMM123");
            //comb_mat_type.Items.Add("_NLMM121");
            //comb_mat_type.Items.Add("_NLMM124");
            //comb_mat_type.Items.Add("_NLMM125");
            //comb_mat_type.Items.Add("_NLMM142");
            //comb_mat_type.Items.Add("_NLMM145");
            //comb_mat_type.Items.Add("_NLMM211");
            //comb_mat_type.Items.Add("_NLMM102");
            //comb_mat_type.Items.Add("_NLMM122");
            //comb_mat_type.Items.Add("_NLMM401");
            //comb_mat_type.Items.Add("_NLMM451");
            //comb_mat_type.Items.Add("_NLMM301");
            //comb_mat_type.Items.Add("_NLMM302");
            //comb_mat_type.Items.Add("_NLMM303");
            //comb_mat_type.Items.Add("_NLMM305");
            //comb_mat_type.Items.Add("_NLMM306");
            //comb_mat_type.Items.Add("_NLMM320");
        }
        private void comb_elem_type_SelectedIndexChanged(object sender, EventArgs e)
        {

            fill_combo_mat_type();

            chk_layer.Checked = false;
            if (comb_elem_type.Text == "_MINDLIN_SHELL_QUAD" || comb_elem_type.Text == "_AHMAD_SHELL_QUAD")
            {
                chk_layer.Visible = true;
                //comb_mat_type.Items.Add("_LAY_PATT");
            }
            else
            {
                chk_layer.Visible = false;
            }


            if ((comb_elem_type.Text == "_MINDLIN_SHELL_QUAD" || comb_elem_type.Text == "_AHMAD_SHELL_QUAD" ) && Editing_val[4]== "_LAY_PATT")
                chk_layer.Checked = true;


            comb_mat_type.Text = Editing_val[4];


            comb_geo_type.Items.Clear();
            switch (comb_elem_type.Text)
            {
                case "_TRUSS_2D":
                    comb_geo_type.Items.Add("_CS_AREA");
                    break;
                case "_TRUSS_3D":
                    comb_geo_type.Items.Add("_CS_AREA");
                    break;
                case "_FRAME_2D":
                    comb_geo_type.Items.Add("_CS_2D");
                    break;
                case "_FRAME_3D":
                    comb_geo_type.Items.Add("_CS_3D");
                    break;
                case "_CABLE_2D":
                    comb_geo_type.Items.Add("_CS_AREA");
                    break;
                case "_CABLE_3D":
                    comb_geo_type.Items.Add("_CS_AREA");
                    break;
                case "_TIMOSHENKO_BEAM_2D":
                    comb_geo_type.Items.Add("_CS_2D");
                    break;
                case "_TIMOSHENKO_BEAM_3D":
                    comb_geo_type.Items.Add("_CS_3D");
                    break;
                case "_PLANE_STRESS_TRIANG":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_PLANE_STRESS_QUAD":
                    comb_geo_type.Items.Add("_LAM_THICK");
                    comb_geo_type.Items.Add("_GEOM_PATT");
                    break;
                case "_PLANE_STRESS_QUAD_THERMAL":
                    break;
                case "_PLANE_STRAIN_TRIANG":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_PLANE_STRAIN_QUAD":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_AXIS_LINE":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_AXIS_TRIANG":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_AXIS_QUAD":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_MINDLIN_SLAB_TRIANG":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_MINDLIN_SLAB_QUAD":
                    comb_geo_type.Items.Add("_LAM_THICK");
                    comb_geo_type.Items.Add("_GEOM_PATT");
                    break;
                case "_MINDLIN_SHELL_TRIANG":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_MINDLIN_SHELL_QUAD":
                    if (chk_layer.Checked == true)
                        comb_geo_type.Items.Add("_NONE");
                    else
                        comb_geo_type.Items.Add("_LAM_THICK");
                        comb_geo_type.Items.Add("_GEOM_PATT");
                    break;
                case "_AHMAD_SHELL_TRIANG":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_AHMAD_SHELL_QUAD":
                    if (chk_layer.Checked == true)
                        comb_geo_type.Items.Add("_NONE");
                    else
                        comb_geo_type.Items.Add("_LAM_THICK");
                        comb_geo_type.Items.Add("_GEOM_PATT");
                    break;
                case "_SOLID_TETRA":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_SOLID_HEXA":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_SOLID_HEXA_THERMAL":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_INTERFACE_POINT":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_INTERFACE_LINE_2D":
                    comb_geo_type.Items.Add("_LAM_THICK");
                    break;
                case "_INTERFACE_LINE_3D":
                    comb_geo_type.Items.Add("_LAM_THICK");
                    break;
                case "_INTERFACE_TRIANG":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_INTERFACE_QUAD":
                    comb_geo_type.Items.Add("_NONE");
                    break;
                case "_EMB_CABLE_2D":
                    comb_geo_type.Items.Add("_CS_AREA");
                    break;
                case "_EMB_CABLE_3D":
                    comb_geo_type.Items.Add("_CS_AREA");
                    break;
                default:
                    break;
            }



            comb_geo_type.Text = Editing_val[6];
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void add_img_MouseHover(object sender, EventArgs e)
        {
            //  add_img.BackgroundImage = Femix.Properties.Resources.icons8_add_new_black_fill;
        }
        private void add_img_MouseLeave(object sender, EventArgs e)
        {
            // add_img.BackgroundImage = Femix.Properties.Resources.icons8_add_new_black;
        }

        private void add_img_Click(object sender, EventArgs e)
        {
           FemixFRM_Class.generate_combo_item("GROUP_NAMES", C_variables.GROUP_NAMES_list,2, comb_group);
        }

     
        private void comb_mat_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            comb_mat_name.Items.Clear();
            switch (comb_mat_type.Text)
            {
                case "_LIN_ISO":
                    //LINEAR_ISOTROPIC_MATERIALS
                 for (int i = 0; i < C_variables.LINEAR_ISOTROPIC_MATERIALS_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.LINEAR_ISOTROPIC_MATERIALS_list[i][2]);
                    break;
                case "_LIN_ORTHO":
                    //LINEAR_ORTHOTROPIC_MATERIALS
                    for (int i = 0; i < C_variables.LINEAR_ORTHOTROPIC_MATERIALS_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.LINEAR_ORTHOTROPIC_MATERIALS_list[i][2]);
                    break;
                case "_NLMM101":
                    //NLMM101_list
                    for (int i = 0; i < C_variables.NLMM101_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM101_list[i][2]);
                    break;
                case "_NLMM104":
                    //NLMM104_list
                    for (int i = 0; i < C_variables.NLMM104_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM104_list[i][2]);
                    break;
                case "_NLMM111":
                    //NLMM111_list
                    for (int i = 0; i < C_variables.NLMM111_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM111_list[i][2]);
                    break;
                case "_NLMM141":
                    //NLMM141_list
                    for (int i = 0; i < C_variables.NLMM141_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM141_list[i][2]);
                    break;
                case "_NLMM151":
                    //NLMM151_list
                    for (int i = 0; i < C_variables.NLMM151_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM151_list[i][2]);
                    break;
                case "_NLMM201":
                    //NLMM201_list
                    for (int i = 0; i < C_variables.NLMM201_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM201_list[i][2]);
                    break;
                case "_NLMM202":
                    for (int i = 0; i < C_variables.NLMM202_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM202_list[i][2]);
                    break;
                case "_NLMM210":
                    for (int i = 0; i < C_variables.NLMM210_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM210_list[i][2]);
                    break;
                case "_NLMM220":
                    for (int i = 0; i < C_variables.NLMM220_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM220_list[i][2]);
                    break;
                case "_NLMM298":
                    for (int i = 0; i < C_variables.NLMM298_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM298_list[i][2]);
                    break;
                case "_NLMM299":
                    for (int i = 0; i < C_variables.NLMM299_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM299_list[i][2]);
                    break;
                case "_NLMM123":
                    for (int i = 0; i < C_variables.NLMM123_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM123_list[i][2]);
                    break;
                case "_NLMM121":
                    for (int i = 0; i < C_variables.NLMM121_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM121_list[i][2]);
                    break;
                case "_NLMM124":
                    for (int i = 0; i < C_variables.NLMM124_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM124_list[i][2]);
                    break;
                case "_NLMM125":
                    for (int i = 0; i < C_variables.NLMM125_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM125_list[i][2]);
                    break;
                case "_NLMM142":
                    for (int i = 0; i < C_variables.NLMM142_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM142_list[i][2]);
                    break;
                case "_NLMM145":
                    for (int i = 0; i < C_variables.NLMM145_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM145_list[i][2]);
                    break;
                case "_NLMM211":
                    for (int i = 0; i < C_variables.NLMM211_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM211_list[i][2]);
                    break;
                case "_NLMM102":
                    for (int i = 0; i < C_variables.NLMM102_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM102_list[i][2]);
                    break;
                case "_NLMM122":
                    for (int i = 0; i < C_variables.NLMM122_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM122_list[i][2]);
                    break;
                case "_NLMM401":
                    for (int i = 0; i < C_variables.NLMM401_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM401_list[i][2]);
                    break;
                case "_NLMM451":
                    for (int i = 0; i < C_variables.NLMM451_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM451_list[i][2]);
                    break;
                case "_NLMM301":
                    for (int i = 0; i < C_variables.NLMM301_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM301_list[i][2]);
                    break;
                case "_NLMM302":
                    for (int i = 0; i < C_variables.NLMM302_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM302_list[i][2]);
                    break;
                case "_NLMM303":
                    for (int i = 0; i < C_variables.NLMM303_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM303_list[i][2]);
                    break;
                case "_NLMM305":
                    for (int i = 0; i < C_variables.NLMM305_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM305_list[i][2]);
                    break;
                case "_NLMM306":
                    for (int i = 0; i < C_variables.NLMM306_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM306_list[i][2]);
                    break;
                case "_NLMM320":
                    for (int i = 0; i < C_variables.NLMM320_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.NLMM320_list[i][2]);
                    break;
                case "_LAY_PATT":
                    for (int i = 0; i < C_variables.LAYER_PATTERNS_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.LAYER_PATTERNS_list[i][2]);
                    break;

                case "_LIN_INT_POINT":
                    for (int i = 0; i < C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_list[i][2]);
                    break;
                case "_LIN_INT_LINE":
                    for (int i = 0; i < C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_list[i][2]);
                    break;
                case "_LIN_INT_SURF":
                    for (int i = 0; i < C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_list.Count; i++)
                        comb_mat_name.Items.Add(C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_list[i][2]);
                    break;
                case "_NONE":
                        comb_mat_name.Items.Add("_NONE");
                    break;
                default:
                    break;
            }


            //comb_geo_type.Items.Clear();
            //if (comb_mat_type.Text == "_LAY_PATT")
            //{
            //    if (comb_elem_type.Text == "_AHMAD_SHELL_QUAD" || comb_elem_type.Text == "_MINDLIN_SHELL_QUAD")
            //    {
            //        comb_geo_type.Items.Clear();
            //        comb_geo_type.Items.Add("_NONE");
            //        comb_geo_type.SelectedIndex = 0;
            //    }
            //    else
            //    {
            //        comb_geo_type.Items.Clear();
            //        comb_geo_type.Items.Add("_LAM_THICK");
            //    }
            //}



            comb_mat_name.Text = Editing_val[5];


        }

        private void add_mat_Click(object sender, EventArgs e)
        {
            switch (comb_mat_type.Text)
            {
                case "_LIN_ISO":
                    //LINEAR_ISOTROPIC_MATERIALS
                    FemixFRM_Class.generate_combo_item("LINEAR_ISOTROPIC_MATERIALS", C_variables.LINEAR_ISOTROPIC_MATERIALS_list, 2, comb_mat_name);
                    break;
                case "_LIN_ORTHO":
                    //LINEAR_ORTHOTROPIC_MATERIALS
                    FemixFRM_Class.generate_combo_item("LINEAR_ORTHOTROPIC_MATERIALS", C_variables.LINEAR_ORTHOTROPIC_MATERIALS_list, 2, comb_mat_name);
                    break;
                case "_NLMM101":
                    //NLMM101
                    FemixFRM_Class.generate_combo_item("NLMM101", C_variables.NLMM101_list, 2, comb_mat_name);
                    break;
                case "_NLMM104":
                    //NLMM104
                    FemixFRM_Class.generate_combo_item("NLMM104", C_variables.NLMM104_list, 2, comb_mat_name);
                    break;
                case "_NLMM111":
                    //NLMM111
                    FemixFRM_Class.generate_combo_item("NLMM111", C_variables.NLMM111_list, 2, comb_mat_name);
                    break;
                case "_NLMM141":
                    //NLMM141
                    FemixFRM_Class.generate_combo_item("NLMM141", C_variables.NLMM141_list, 2, comb_mat_name);
                    break;
                case "_NLMM151":
                    //NLMM151
                    FemixFRM_Class.generate_combo_item("NLMM151", C_variables.NLMM151_list, 2, comb_mat_name);
                    break;
                case "_NLMM201":
                    FemixFRM_Class.generate_combo_item("NLMM201", C_variables.NLMM201_list, 2, comb_mat_name);
                    break;
                case "_NLMM202":
                    FemixFRM_Class.generate_combo_item("NLMM202", C_variables.NLMM202_list, 2, comb_mat_name);
                    break;
                case "_NLMM210":
                    FemixFRM_Class.generate_combo_item("NLMM210", C_variables.NLMM210_list, 2, comb_mat_name);
                    break;
                case "_NLMM220":
                    FemixFRM_Class.generate_combo_item("NLMM220", C_variables.NLMM220_list, 2, comb_mat_name);
                    break;
                case "_NLMM298":
                    FemixFRM_Class.generate_combo_item("NLMM298", C_variables.NLMM298_list, 2, comb_mat_name);
                    break;
                case "_NLMM299":
                    FemixFRM_Class.generate_combo_item("NLMM299", C_variables.NLMM299_list, 2, comb_mat_name);
                    break;
                case "_NLMM123":
                    FemixFRM_Class.generate_combo_item("NLMM123", C_variables.NLMM123_list, 2, comb_mat_name);
                    break;
                case "_NLMM121":
                    FemixFRM_Class.generate_combo_item("NLMM121", C_variables.NLMM121_list, 2, comb_mat_name);
                    break;
                case "_NLMM124":
                    FemixFRM_Class.generate_combo_item("NLMM124", C_variables.NLMM124_list, 2, comb_mat_name);
                    break;
                case "_NLMM125":
                    FemixFRM_Class.generate_combo_item("NLMM125", C_variables.NLMM125_list, 2, comb_mat_name);
                    break;
                case "_NLMM142":
                    FemixFRM_Class.generate_combo_item("NLMM142", C_variables.NLMM142_list, 2, comb_mat_name);
                    break;
                case "_NLMM145":
                    FemixFRM_Class.generate_combo_item("NLMM145", C_variables.NLMM145_list, 2, comb_mat_name);
                    break;
                case "_NLMM211":
                    FemixFRM_Class.generate_combo_item("NLMM211", C_variables.NLMM211_list, 2, comb_mat_name);
                    break;
                case "_NLMM102":
                    FemixFRM_Class.generate_combo_item("NLMM102", C_variables.NLMM102_list, 2, comb_mat_name);
                    break;
                case "_NLMM122":
                    FemixFRM_Class.generate_combo_item("NLMM122", C_variables.NLMM122_list, 2, comb_mat_name);
                    break;
                case "_NLMM401":
                    FemixFRM_Class.generate_combo_item("NLMM401", C_variables.NLMM401_list, 2, comb_mat_name);
                    break;
                case "_NLMM451":
                    FemixFRM_Class.generate_combo_item("NLMM451", C_variables.NLMM451_list, 2, comb_mat_name);
                    break;
                case "_NLMM301":
                    FemixFRM_Class.generate_combo_item("NLMM301", C_variables.NLMM301_list, 2, comb_mat_name);
                    break;
                case "_NLMM302":
                    FemixFRM_Class.generate_combo_item("NLMM302", C_variables.NLMM302_list, 2, comb_mat_name);
                    break;
                case "_NLMM303":
                    FemixFRM_Class.generate_combo_item("NLMM303", C_variables.NLMM303_list, 2, comb_mat_name);
                    break;
                case "_NLMM305":
                    FemixFRM_Class.generate_combo_item("NLMM305", C_variables.NLMM305_list, 2, comb_mat_name);
                    break;
                case "_NLMM306":
                    FemixFRM_Class.generate_combo_item("NLMM306", C_variables.NLMM306_list, 2, comb_mat_name);
                    break;
                case "_NLMM320":
                    FemixFRM_Class.generate_combo_item("NLMM320", C_variables.NLMM320_list, 2, comb_mat_name);
                    break;
                    
                case "_LIN_INT_POINT":
                    FemixFRM_Class.generate_combo_item("LINEAR_INTERFACE_POINT_PROPERTIES", C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_list, 2, comb_mat_name);
                    break;
                case "_LIN_INT_LINE":
                    FemixFRM_Class.generate_combo_item("LINEAR_INTERFACE_LINE_PROPERTIES", C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_list, 2, comb_mat_name);
                    break;
                case "_LIN_INT_SURF":
                    FemixFRM_Class.generate_combo_item("LINEAR_INTERFACE_SURFACE_PROPERTIES", C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_list, 2, comb_mat_name);
                    break;


                case "_LAY_PATT":
                    //LAYER_PATTERNS
                    FemixFRM_Class.generate_combo_item("LAYER_PATTERNS", C_variables.LAYER_PATTERNS_list, 2, comb_mat_name);
                    break;
                default:
                    break;
            }
        }

        private void comb_geo_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            comb_geo_name.Items.Clear();

            switch (comb_geo_type.Text)
            {
                case "_NONE":
                    comb_geo_name.Items.Add("_NONE");
                    comb_geo_name.SelectedIndex = 0;
                    lbl_geo_type.Text = "";
                    break;
                case "_LAM_THICK":
                    comb_geo_name.Items.Clear();
                    for (int i = 0; i < C_variables.LAMINATE_THICKNESSES_list.Count; i++)
                        comb_geo_name.Items.Add(C_variables.LAMINATE_THICKNESSES_list[i][2]);
                    lbl_geo_type.Text = "LAMINATE THICKNESSES";
                    break;
                case "_CS_AREA":
                    comb_geo_name.Items.Clear();
                    for (int i = 0; i < C_variables.CROSS_SECTION_AREAS_list.Count; i++)
                        comb_geo_name.Items.Add(C_variables.CROSS_SECTION_AREAS_list[i][2]);
                    lbl_geo_type.Text = "CROSS SECTION AREAS";
                    break;
                case "_CS_2D":
                    comb_geo_name.Items.Clear();
                    for (int i = 0; i < C_variables.FRAME_2D_CROSS_SECTIONS_list.Count; i++)
                        comb_geo_name.Items.Add(C_variables.FRAME_2D_CROSS_SECTIONS_list[i][2]);
                    lbl_geo_type.Text = "FRAME 2D CROSS SECTIONS";
                    break;
                case "_CS_3D":
                    comb_geo_name.Items.Clear();
                    for (int i = 0; i < C_variables.FRAME_3D_CROSS_SECTIONS_list.Count; i++)
                        comb_geo_name.Items.Add(C_variables.FRAME_3D_CROSS_SECTIONS_list[i][2]);
                    lbl_geo_type.Text = "FRAME 3D CROSS SECTIONS";
                    break;
                case "_GEOM_PATT":
                    comb_geo_name.Items.Clear();
                    for (int i = 0; i < C_variables.GEOMETRY_PATTERNS_list.Count; i++)
                        comb_geo_name.Items.Add(C_variables.GEOMETRY_PATTERNS_list[i][2]);
                    lbl_geo_type.Text = "GEOMETRY PATTERNS";
                    break;
                default:
                    break;
            }

            comb_geo_name.Text = Editing_val[7];
        }

        private void comb_geo_name_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chk_layer_CheckedChanged(object sender, EventArgs e)
        {
             comb_mat_type.Items.Clear();
            comb_geo_type.Items.Clear();
            if (chk_layer.Checked == false)
            {
                fill_combo_mat_type();

                comb_geo_type.Items.Add("_LAM_THICK");
                comb_geo_type.Items.Add("_GEOM_PATT");

                label5.Text = "Material type";
                label6.Text = "Material name";
            }
            else
            {
                comb_mat_type.Items.Add("_LAY_PATT");
                label5.Text = "Layer pattern";
                label6.Text = "Pattern name";

                comb_geo_type.Items.Add("_NONE");
                comb_geo_type.SelectedIndex = 0;

            }
            comb_mat_type.Text = Editing_val[4];
            comb_geo_type.Text= Editing_val[6];
        }
        private void add_geo_Click(object sender, EventArgs e)
        {
            switch (comb_geo_type.Text)
            {
                case "_NONE":
                    break;
                case "_LAM_THICK":
                    FemixFRM_Class.generate_combo_item("LAMINATE_THICKNESSES", C_variables.LAMINATE_THICKNESSES_list, 2, comb_geo_name);
                    break;
                case "_CS_AREA":
                    FemixFRM_Class.generate_combo_item("CROSS_SECTION_AREAS", C_variables.CROSS_SECTION_AREAS_list, 2, comb_geo_name);
                    break;
                case "_CS_2D":
                    FemixFRM_Class.generate_combo_item("FRAME_2D_CROSS_SECTIONS", C_variables.FRAME_2D_CROSS_SECTIONS_list, 2, comb_geo_name);
                    break;
                case "_CS_3D":
                    FemixFRM_Class.generate_combo_item("FRAME_3D_CROSS_SECTIONS", C_variables.FRAME_3D_CROSS_SECTIONS_list, 2, comb_geo_name);
                    break;
                case "_GEOM_PATT":
                    FemixFRM_Class.generate_combo_item("GEOMETRY_PATTERNS", C_variables.GEOMETRY_PATTERNS_list, 2, comb_geo_name);
                    break;
                default:
                    break;
            }
        }
        private void comb_mat_name_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comb_int_type_SelectedIndexChanged(object sender, EventArgs e)
        {

            comb_int_name.Items.Clear();
            switch (comb_int_type.Text)
            {
                case "_NONE":
                    lbl_int_type.Text = "";
                    comb_int_name.Items.Add("_NONE");
                    break;
                case "_GLEG":
                    lbl_int_type.Text = "Gauss-Legendre";
                    break;
                case "_GLOB":
                    lbl_int_type.Text = "Gauss-Lobatto";
                    break;
                case "_NCOTES":
                    lbl_int_type.Text = "Newton-Cotes";
                    break;
                default:
                    break;
            }


            for (int i = 0; i < C_variables.NUMERICAL_INTEGRATION_list.Count; i++)
                comb_int_name.Items.Add(C_variables.NUMERICAL_INTEGRATION_list[i][2]);

               

            comb_int_name.Text = Editing_val[9];
        }

       

        private void add_int_Click(object sender, EventArgs e)
        {
            FemixFRM_Class.generate_combo_item("NUMERICAL_INTEGRATION", C_variables.NUMERICAL_INTEGRATION_list, 2, comb_int_name);

        }
    }
}
