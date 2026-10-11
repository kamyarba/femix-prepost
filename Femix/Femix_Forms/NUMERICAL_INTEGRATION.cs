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
    public partial class NUMERICAL_INTEGRATION : Form
    {
        private string[] Editing_val;
        public NUMERICAL_INTEGRATION()
        {
            InitializeComponent();
            create_controls();
        }

        private void group_frm_Load(object sender, EventArgs e)
        {
           
            combo_new.SelectedIndex = 0;
            Editing_val = FemixFRM_Class.Editing_val_Arr.ToArray();

            this.Text = "Numerical Integration";
            groupBox1.Text = "Numerical Integration";



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



            //comb_term0.Items.Clear();
            //comb_term0.Items.Add("General");
            //comb_term0.Items.Add("Membrane");
            //comb_term0.Items.Add("Shear");
            //comb_term0.Items.Add("Torsional");
            //comb_term0.Items.Add("Bending");

            //comb_dir0.Items.Clear();
            //comb_dir0.Items.Add("S1");
            //comb_dir0.Items.Add("S2");
            //comb_dir0.Items.Add("S3");



            for (int i= 0;i< 20; i++)
            {
                comb_term[i].Items.Clear();
                comb_term[i].Items.Add("General");
                comb_term[i].Items.Add("Membrane");
                comb_term[i].Items.Add("Shear");
                comb_term[i].Items.Add("Torsional");
                comb_term[i].Items.Add("Bending");

                comb_dir[i].Items.Clear();
                comb_dir[i].Items.Add("S1");
                comb_dir[i].Items.Add("S2");
                comb_dir[i].Items.Add("S3");

            }

            if (Editing_val[1] == "") { Editing_val[1] = "0"; }
            for (int i=0;i< int.Parse(Editing_val[1]);i++)
            {
                string[] words = Editing_val[i+2].Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

                if(words.Count()==3)
                {
                    switch (words[0].ToUpper())
                    {
                        case "G":
                            comb_term[i].Text = "General";
                            break;
                        case "M":
                            comb_term[i].Text = "Membrane";
                            break;
                        case "S":
                            comb_term[i].Text = "Shear";
                            break;
                        case "T":
                            comb_term[i].Text = "Torsional";
                            break;
                        case "B":
                            comb_term[i].Text = "Bending";
                            break;
                        default:
                            break;
                    }
                    //comb_term[i].Text = words[0];
                    comb_dir[i].Text = words[1];
                    txt_ndir[i].Value = decimal.Parse(words[2]);
                }
            }

            txt_name.Text = Editing_val[0];
            txt_nkeyword.Text = Editing_val[1];

            //txt_nkeyword.Text = Editing_val[1];

        }

        // private ComboBox[] myComboBox;//= new ComboBox[30];
        private Label[] lbl_count, lbl_term, lbl_dir, lbl_npoint;
        private ComboBox[] comb_term, comb_dir;

        private void txt_nkeyword_ValueChanged(object sender, EventArgs e)
        {
            Editing_val = new string[int.Parse(txt_nkeyword.Value.ToString())+2];
            for (int i=0;i< txt_nkeyword.Value; i++)
            {
                lbl_count[i].Visible = true;
                lbl_term[i].Visible = true;
                lbl_dir[i].Visible = true;
                lbl_npoint[i].Visible = true;
                comb_term[i].Visible = true;
                comb_dir[i].Visible = true;
                txt_ndir[i].Visible = true;
            }
            for (int i = int.Parse((txt_nkeyword.Value ).ToString()); i < 20; i++)
            {
                lbl_count[i].Visible = false;
                lbl_term[i].Visible = false;
                lbl_dir[i].Visible = false;
                lbl_npoint[i].Visible = false;
                comb_term[i].Visible = false;
                comb_dir[i].Visible = false;
                txt_ndir[i].Visible = false;
            }

        }

        private void btn_auto_int_Click(object sender, EventArgs e)
        {
            switch (comb_elem_type.Text)
            {
                case "_TRUSS_2D":
                    txt_nkeyword.Value = 0;
                    break;
                case "_TRUSS_3D":
                    txt_nkeyword.Value = 0;
                    break;
                case "_FRAME_2D":
                    txt_nkeyword.Value = 0;
                    break;
                case "_FRAME_3D":
                    txt_nkeyword.Value = 0;
                    break;
                case "_CABLE_2D":
                    txt_name.Text = "CABLE_2D_Integration";
                    txt_nkeyword.Value = 1;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    break;
                case "_CABLE_3D":
                    txt_name.Text = "CABLE_3D_Integration";
                    txt_nkeyword.Value = 1;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    break;
                case "_TIMOSHENKO_BEAM_2D":
                    txt_name.Text = "TIMOSHENKO_BEAM_2D_Integration";
                    txt_nkeyword.Value = 3;
                    comb_term[0].Text = "Membrane";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "Shear";
                    comb_dir[1].Text = "S1";
                    txt_ndir[1].Value = 2;
                    comb_term[2].Text = "Bending";
                    comb_dir[2].Text = "S1";
                    txt_ndir[2].Value = 2;
                    break;
                case "_TIMOSHENKO_BEAM_3D":
                    txt_name.Text = "TIMOSHENKO_BEAM_3D_Integration";
                    txt_nkeyword.Value = 4;
                    comb_term[0].Text = "Membrane";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "Shear";
                    comb_dir[1].Text = "S1";
                    txt_ndir[1].Value = 2;
                    comb_term[2].Text = "Torsional";
                    comb_dir[2].Text = "S1";
                    txt_ndir[2].Value = 2;
                    comb_term[3].Text = "Bending";
                    comb_dir[3].Text = "S1";
                    txt_ndir[3].Value = 2;
                    break;
                //case "_PLANE_STRESS_TRIANG":
                //    break;
                case "_PLANE_STRESS_QUAD":
                    txt_name.Text = "PLANE_STRESS_QUAD_Integration";
                    txt_nkeyword.Value = 2;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "General";
                    comb_dir[1].Text = "S2";
                    txt_ndir[1].Value = 2;
                    break;
                case "_PLANE_STRESS_QUAD_THERMAL":
                    txt_name.Text = "_PLANE_STRESS_QUAD_THERMAL_Integration";
                    txt_nkeyword.Value = 2;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "General";
                    comb_dir[1].Text = "S2";
                    txt_ndir[1].Value = 2;
                    break;
                //case "_PLANE_STRAIN_TRIANG":
                //    break;
                case "_PLANE_STRAIN_QUAD":
                    txt_name.Text = "PLANE_STRAIN_QUAD_Integration";
                    txt_nkeyword.Value = 2;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "General";
                    comb_dir[1].Text = "S2";
                    txt_ndir[1].Value = 2;
                    break;
                case "_AXIS_LINE":
                    txt_name.Text = "AXIS_LINE_Integration";
                    txt_nkeyword.Value = 1;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    break;
                //case "_AXIS_TRIANG":
                //    break;
                case "_AXIS_QUAD":
                    txt_name.Text = "AXIS_QUAD_Integration";
                    txt_nkeyword.Value = 2;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "General";
                    comb_dir[1].Text = "S2";
                    txt_ndir[1].Value = 2;
                    break;
                //case "_MINDLIN_SLAB_TRIANG":
                //    break;
                case "_MINDLIN_SLAB_QUAD":
                    txt_name.Text = "MINDLIN_SLAB_QUAD_Integration";
                    txt_nkeyword.Value = 4;
                    comb_term[0].Text = "Shear";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "Shear";
                    comb_dir[1].Text = "S2";
                    txt_ndir[1].Value = 2;
                    comb_term[2].Text = "Bending";
                    comb_dir[2].Text = "S1";
                    txt_ndir[2].Value = 2;
                    comb_term[3].Text = "Bending";
                    comb_dir[3].Text = "S2";
                    txt_ndir[3].Value = 2;
                    break;
                //case "_MINDLIN_SHELL_TRIANG":
                //    break;
                case "_MINDLIN_SHELL_QUAD":
                    txt_name.Text = "MINDLIN_SHELL_QUAD_Integration";
                    txt_nkeyword.Value = 6;
                    comb_term[0].Text = "Membrane";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "Membrane";
                    comb_dir[1].Text = "S2";
                    txt_ndir[1].Value = 2;
                    comb_term[2].Text = "Shear";
                    comb_dir[2].Text = "S1";
                    txt_ndir[2].Value = 2;
                    comb_term[3].Text = "Shear";
                    comb_dir[3].Text = "S2";
                    txt_ndir[3].Value = 2;
                    comb_term[4].Text = "Bending";
                    comb_dir[4].Text = "S1";
                    txt_ndir[4].Value = 2;
                    comb_term[5].Text = "Bending";
                    comb_dir[5].Text = "S2";
                    txt_ndir[5].Value = 2;
                    break;
                //case "_AHMAD_SHELL_TRIANG":
                //    break;
                case "_AHMAD_SHELL_QUAD":
                    txt_name.Text = "AHMAD_SHELL_QUAD_Integration";
                    txt_nkeyword.Value = 9;
                    comb_term[0].Text = "Membrane";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "Membrane";
                    comb_dir[1].Text = "S2";
                    txt_ndir[1].Value = 2;
                    comb_term[2].Text = "Membrane";
                    comb_dir[2].Text = "S3";
                    txt_ndir[2].Value = 2;
                    comb_term[3].Text = "Shear";
                    comb_dir[3].Text = "S1";
                    txt_ndir[3].Value = 2;
                    comb_term[4].Text = "Shear";
                    comb_dir[4].Text = "S2";
                    txt_ndir[4].Value = 2;
                    comb_term[5].Text = "Shear";
                    comb_dir[5].Text = "S3";
                    txt_ndir[5].Value = 2;
                    comb_term[6].Text = "Bending";
                    comb_dir[6].Text = "S1";
                    txt_ndir[6].Value = 2;
                    comb_term[7].Text = "Bending";
                    comb_dir[7].Text = "S2";
                    txt_ndir[7].Value = 2;
                    comb_term[8].Text = "Bending";
                    comb_dir[8].Text = "S3";
                    txt_ndir[8].Value = 2;
                    break;
                case "_SOLID_TETRA":
                    txt_name.Text = "SOLID_TETRA_Integration";
                    txt_nkeyword.Value = 3;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "General";
                    comb_dir[1].Text = "S2";
                    txt_ndir[1].Value = 2;
                    comb_term[2].Text = "General";
                    comb_dir[2].Text = "S3";
                    txt_ndir[2].Value = 2;
                    break;
                case "_SOLID_HEXA":
                    txt_name.Text = "SOLID_HEXA_Integration";
                    txt_nkeyword.Value = 3;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "General";
                    comb_dir[1].Text = "S2";
                    txt_ndir[1].Value = 2;
                    comb_term[2].Text = "General";
                    comb_dir[2].Text = "S3";
                    txt_ndir[2].Value = 2;
                    break;
                case "_SOLID_HEXA_THERMAL":
                    txt_name.Text = "SOLID_HEXA_THERMAL_Integration";
                    txt_nkeyword.Value = 3;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "General";
                    comb_dir[1].Text = "S2";
                    txt_ndir[1].Value = 2;
                    comb_term[2].Text = "General";
                    comb_dir[2].Text = "S3";
                    txt_ndir[2].Value = 2;
                    break;
                case "_INTERFACE_POINT":
                    txt_nkeyword.Value = 0;
                    break;
                case "_INTERFACE_LINE_2D":
                    txt_name.Text = "INTERFACE_LINE_2D_Integration";
                    txt_nkeyword.Value = 1;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    break;
                case "_INTERFACE_LINE_3D":
                    txt_name.Text = "INTERFACE_LINE_3D_Integration";
                    txt_nkeyword.Value = 1;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    break;
                //case "_INTERFACE_TRIANG":
                //    break;
                case "_INTERFACE_QUAD":
                    txt_name.Text = "INTERFACE_QUAD_Integration";
                    txt_nkeyword.Value = 2;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    comb_term[1].Text = "General";
                    comb_dir[1].Text = "S2";
                    txt_ndir[1].Value = 2;
                    break;
                case "_EMB_CABLE_2D":
                    txt_name.Text = "EMB_CABLE_2D_Integration";
                    txt_nkeyword.Value = 1;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    break;
                case "_EMB_CABLE_3D":
                    txt_name.Text = "EMB_CABLE_3D_Integration";
                    txt_nkeyword.Value = 1;
                    comb_term[0].Text = "General";
                    comb_dir[0].Text = "S1";
                    txt_ndir[0].Value = 2;
                    break;
                default:
                    MessageBox.Show("Currently, default integration has not been defined for the selected element type!");
                    break;
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {

        }

        private NumericUpDown[] txt_ndir;
        private void create_controls()
        {
            int n_control= 20;

            //myComboBox = new ComboBox[n_control];
            lbl_count = new Label[n_control];
            lbl_term = new Label[n_control];
            comb_term = new ComboBox[n_control];
            comb_dir = new ComboBox[n_control];
            lbl_dir = new Label[n_control];
            lbl_npoint = new Label[n_control];
            txt_ndir = new NumericUpDown[n_control];
            for (int i = 0; i < n_control; i++)
            {
                //label0
                lbl_count[i] = new Label();
                lbl_count[i].Visible = false;
                lbl_count[i].AutoSize = true;
                lbl_count[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
                lbl_count[i].Location = new System.Drawing.Point(10, 13+(30*i));
                lbl_count[i].Size = new System.Drawing.Size(14, 13);
                //lbl_count[i].TabIndex = 0;
                lbl_count[i].Text =(i+1).ToString();
                this.panel1.Controls.Add(lbl_count[i]);


                // lbl_term0
                // 
                lbl_term[i] = new Label();
                lbl_term[i].Visible = false;
                lbl_term[i].AutoSize = true;
                lbl_term[i].Location = new System.Drawing.Point(35, 13 + (30 * i));
                lbl_term[i].Name = "lbl_term0";
                lbl_term[i].Size = new System.Drawing.Size(75, 13);
                //lbl_term[i].TabIndex = 1;
                lbl_term[i].Text = "Type of Terms";
                this.panel1.Controls.Add(lbl_term[i]);

                // 
                // comb_term0
                // 
                comb_term[i] = new ComboBox();
                comb_term[i].Visible = false;
                comb_term[i].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                comb_term[i].FormattingEnabled = true;
                comb_term[i].Location = new System.Drawing.Point(116, 10 + (30 * i));
                comb_term[i].Name = "comb_term0";
                comb_term[i].Size = new System.Drawing.Size(95, 21);
                //comb_term[i].TabIndex = 14;
                comb_term[i].Tag = "";
                toolTip1.SetToolTip(comb_term[i], "General use (stiffness matrix, springs and load vector)\r\nMembrane terms (stiffnes" +
            "s matrix)\r\nShear terms (stiffness matrix)\r\nTorsional terms (stiffness matrix)\r\nB" +
            "ending terms (stiffness matrix)");
                this.panel1.Controls.Add(comb_term[i]);
                // 
                // comb_dir0
                // 
                comb_dir[i] = new ComboBox();
                comb_dir[i].Visible = false;
                comb_dir[i].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                comb_dir[i].FormattingEnabled = true;
                comb_dir[i].Location = new System.Drawing.Point(284, 10 + (30 * i));
                comb_dir[i].Name = "comb_dir0";
                comb_dir[i].Size = new System.Drawing.Size(53, 21);
                //comb_dir[i].TabIndex = 16;
                comb_dir[i].Tag = "";
                toolTip1.SetToolTip(comb_dir[i], "Direction s1\r\nDirection s2\r\nDirection s3");
                this.panel1.Controls.Add(comb_dir[i]);
                // 
                // lbl_dir0
                // 
                lbl_dir[i] = new Label();
                lbl_dir[i].Visible = false;
                lbl_dir[i].AutoSize = true;
                lbl_dir[i].Location = new System.Drawing.Point(229, 13 + (30 * i));
                lbl_dir[i].Name = "lbl_dir0";
                lbl_dir[i].Size = new System.Drawing.Size(49, 13);
                //lbl_dir[i].TabIndex = 15;
                lbl_dir[i].Text = "Direction";
                this.panel1.Controls.Add(lbl_dir[i]);

                // 
                // lbl_npoint0
                // 
                lbl_npoint[i] = new Label();
                lbl_npoint[i].Visible = false;
                lbl_npoint[i].AutoSize = true;
                lbl_npoint[i].Location = new System.Drawing.Point(349, 13 + (30 * i));
                lbl_npoint[i].Name = "lbl_npoint0";
                lbl_npoint[i].Size = new System.Drawing.Size(87, 13);
                //lbl_npoint[i].TabIndex = 17;
                lbl_npoint[i].Text = "Number of points";
                this.panel1.Controls.Add(lbl_npoint[i]);

                // 
                // txt_ndir0
                // 
                txt_ndir[i] = new NumericUpDown();
                txt_ndir[i].Visible = false;
                txt_ndir[i].Location = new System.Drawing.Point(442, 10 + (30 * i));
                txt_ndir[i].Maximum = new decimal(new int[] {10,0,0,0});
                txt_ndir[i].Minimum = new decimal(new int[] {1,0,0,0});
                txt_ndir[i].Name = "txt_ndir0";
                txt_ndir[i].Size = new System.Drawing.Size(66, 20);
                //txt_ndir[i].TabIndex = 18;
                txt_ndir[i].Value = new decimal(new int[] { 2,0, 0, 0});
                this.panel1.Controls.Add(txt_ndir[i]);


                //myComboBox[i] = new ComboBox();
                //myComboBox[i].Text = "New Combo Created";
                //myComboBox[i].Left = 20;
                //myComboBox[i].Top = 22 * i;
                //this.panel1.Controls.Add(myComboBox[i]);
            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

            Editing_val[0] = txt_name.Text;
            Editing_val[1] = txt_nkeyword.Text;


            
            for (int i = 0; i < int.Parse( Editing_val[1]); i++)
            {
                Editing_val[i + 2] = "_";
                switch (comb_term[i].Text)
                {
                    case "General":
                        Editing_val[i + 2] += "G";
                        break;
                    case "Membrane":
                        Editing_val[i + 2] += "M";
                        break;
                    case "Shear":
                        Editing_val[i + 2] += "S";
                        break;
                    case "Torsional":
                        Editing_val[i + 2] += "T";
                        break;
                    case "Bending":
                        Editing_val[i + 2] += "B";
                        break;
                    default:
                        break;
                }
                Editing_val[i + 2] += "_" + comb_dir[i].Text;
                Editing_val[i + 2] += "_" + txt_ndir[i].Text;
            }



            FemixFRM_Class.Editing_val_Arr = Editing_val.ToArray();
            if (combo_new.SelectedIndex == 1)
                FemixFRM_Class.is_new = true;
            this.Close();
        }


    }
}
