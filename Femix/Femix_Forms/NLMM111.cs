using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Femix.Femix_Forms
{
    public partial class NLMM111 : Form
    {
        private string[] Editing_val;
        private string mat_lib_folder;
        public NLMM111()
        {
            InitializeComponent();
        }

        private void group_frm_Load(object sender, EventArgs e)
        {
            combo_new.SelectedIndex = 0;
            Editing_val = FemixFRM_Class.Editing_val_Arr.ToArray();

            this.Text = "NLMM 111";
            groupBox1.Text = "NLMM 111";
            mat_lib_folder = "NLMM111";

            txt_B.Text = Editing_val[0];
            txt_C.Text = Editing_val[1];
            txt_D.Text = Editing_val[2];
            txt_E.Text = Editing_val[3];
            txt_F.Text = Editing_val[4];

            txt_G.Text = Editing_val[5];
            txt_H.Text = Editing_val[6];
            com_I.Text = Editing_val[7];
            txt_J.Text = Editing_val[8];
            txt_K.Text = Editing_val[9];
            txt_L.Text = Editing_val[10];
            txt_M.Text = Editing_val[11];
            txt_N.Text = Editing_val[12];
            txt_O.Text = Editing_val[13];
            txt_P.Text = Editing_val[14];
            txt_Q.Text = Editing_val[15];
            com_R.Text = Editing_val[16];
            txt_S.Text = Editing_val[17];

            txt_T.Text = Editing_val[18];
            txt_U.Text = Editing_val[19];
            txt_V.Text = Editing_val[20];
            com_W.Text = Editing_val[21];
            com_X.Text = Editing_val[22];
            txt_Y.Text = Editing_val[23];
            txt_Z.Text = Editing_val[24];

            com_AA.Text = Editing_val[25];
            com_AB.Text = Editing_val[26];
            txt_AC.Text = Editing_val[27];
            txt_AD.Text = Editing_val[28];


           



            //scan for material library
            comb_mat_lib.Items.Clear();

            string lib_directory = "Material_Library/" + mat_lib_folder;
            if (!Directory.Exists(lib_directory))
                Directory.CreateDirectory(lib_directory);
            string[] files = Directory.GetFiles(lib_directory, "*.mat");
            foreach (var file in files)
            {
                comb_mat_lib.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

            Editing_val[0] = txt_B.Text;
            Editing_val[1] = txt_C.Text;
            Editing_val[2] = txt_D.Text;
            Editing_val[3] = txt_E.Text;
            Editing_val[4] = txt_F.Text;
            Editing_val[5] = txt_G.Text;
            Editing_val[6] = txt_H.Text;
            Editing_val[7] = com_I.Text;
            Editing_val[8] = txt_J.Text;
            Editing_val[9] = txt_K.Text;
            Editing_val[10] = txt_L.Text;
            Editing_val[11] = txt_M.Text;
            Editing_val[12] = txt_N.Text;
            Editing_val[13] = txt_O.Text;
            Editing_val[14] = txt_P.Text;
            Editing_val[15] = txt_Q.Text;
            Editing_val[16] = com_R.Text;
            Editing_val[17] = txt_S.Text;
            Editing_val[18] = txt_T.Text;
            Editing_val[19] = txt_U.Text;
            Editing_val[20] = txt_V.Text;
            Editing_val[21] = com_W.Text;
            Editing_val[22] = com_X.Text;
            Editing_val[23] = txt_Y.Text;
            Editing_val[24] = txt_Z.Text;
            Editing_val[25] = com_AA.Text;
            Editing_val[26] = com_AB.Text;
            Editing_val[27] = txt_AC.Text;
            Editing_val[28] = txt_AD.Text;


            for (int i = 0; i <= 28; i++)
                if (Editing_val[i] == "")
                    Editing_val[i] = "0";

            FemixFRM_Class.Editing_val_Arr = Editing_val.ToArray();
            if (combo_new.SelectedIndex == 1)
                FemixFRM_Class.is_new = true;
            this.Close();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            if (comb_mat_lib.SelectedIndex < 0)
                return;
            string[] lines = File.ReadAllLines("Material_Library/" + mat_lib_folder + "/" + comb_mat_lib.Text + ".mat");

            txt_B.Text = lines[0];
            txt_C.Text = lines[1];
            txt_D.Text = lines[2];
            txt_E.Text = lines[3];
            txt_F.Text = lines[4];

            txt_G.Text = lines[5];
            txt_H.Text = lines[6];
            com_I.Text = lines[7];
            txt_J.Text = lines[8];
            txt_K.Text = lines[9];
            txt_L.Text = lines[10];
            txt_M.Text = lines[11];
            txt_N.Text = lines[12];
            txt_O.Text = lines[13];
            txt_P.Text = lines[14];
            txt_Q.Text = lines[15];
            com_R.Text = lines[16];
            txt_S.Text = lines[17];

            txt_T.Text = lines[18];
            txt_U.Text = lines[19];
            txt_V.Text = lines[20];
            com_W.Text = lines[21];
            com_X.Text = lines[22];
            txt_Y.Text = lines[23];
            txt_Z.Text = lines[24];

            com_AA.Text = lines[25];
            com_AB.Text = lines[26];
            txt_AC.Text = lines[27];
            txt_AD.Text = lines[28];


        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_B.Text == "")
                return;

            List<string> filedata = new List<string>();

           filedata.Add( txt_B.Text );
           filedata.Add( txt_C.Text );
           filedata.Add( txt_D.Text );
           filedata.Add( txt_E.Text );
           filedata.Add( txt_F.Text );
           filedata.Add( txt_G.Text );
           filedata.Add( txt_H.Text );
           filedata.Add( com_I.Text );
           filedata.Add( txt_J.Text );
           filedata.Add( txt_K.Text );
           filedata.Add( txt_L.Text );
           filedata.Add( txt_M.Text );
           filedata.Add( txt_N.Text );
           filedata.Add( txt_O.Text );
           filedata.Add( txt_P.Text );
           filedata.Add( txt_Q.Text );
           filedata.Add( com_R.Text );
           filedata.Add( txt_S.Text );
           filedata.Add( txt_T.Text );
           filedata.Add( txt_U.Text );
           filedata.Add( txt_V.Text );
           filedata.Add( com_W.Text );
           filedata.Add( com_X.Text );
           filedata.Add( txt_Y.Text );
           filedata.Add( txt_Z.Text );
           filedata.Add( com_AA.Text);
           filedata.Add( com_AB.Text);
           filedata.Add( txt_AC.Text);
            filedata.Add(txt_AD.Text);


            File.WriteAllLines("Material_Library/" + mat_lib_folder + "/" + txt_B.Text + ".mat", filedata);
            comb_mat_lib.Items.Clear();
            string[] files = Directory.GetFiles("Material_Library/" + mat_lib_folder, "*.mat");
            foreach (var file in files)
                comb_mat_lib.Items.Add(Path.GetFileNameWithoutExtension(file));
            comb_mat_lib.Text = txt_B.Text;

            //System.IO.File.Delete("Material_Library/"+ mat_lib_folder+"/"+comb_mat_lib.Text+".mat");
        }

        private void comb_I_SelectedIndexChanged(object sender, EventArgs e)
        {

            txt_J.Enabled = true; lbl_J.Enabled = true;
            txt_K.Enabled = true; lbl_K.Enabled = true;
            txt_L.Enabled = true; lbl_L.Enabled = true;
            txt_M.Enabled = true; lbl_M.Enabled = true;
            txt_N.Enabled = true; lbl_N.Enabled = true;
            txt_O.Enabled = true; lbl_O.Enabled = true;
            switch (com_I.Text)
            {
                case "_TRILINEAR":
                    pic_frac1.Image = Image.FromFile("image/" + "NLMM104-2-TRI.png");
                    if (txt_N.Text == "") txt_N.Text = "0";
                    if (txt_O.Text == "") txt_O.Text = "0";
                    txt_N.Enabled = false; lbl_N.Enabled = false;
                    txt_O.Enabled = false; lbl_O.Enabled = false;
                    break;
                case "_QUADRILINEAR":
                    pic_frac1.Image = Image.FromFile("image/" + "NLMM104-2-QUAD.png");
                    break;
                case "_CORNELISSEN":
                    pic_frac1.Image = Image.FromFile("image/" + "NLMM104-2-CORN.png");
                    if (txt_J.Text == "") txt_J.Text = "0";
                    if (txt_K.Text == "") txt_K.Text = "0";
                    if (txt_L.Text == "") txt_L.Text = "0";
                    if (txt_M.Text == "") txt_M.Text = "0";
                    if (txt_N.Text == "") txt_N.Text = "0";
                    if (txt_O.Text == "") txt_O.Text = "0";
                    txt_J.Enabled = false; lbl_J.Enabled = false;
                    txt_K.Enabled = false; lbl_K.Enabled = false;
                    txt_L.Enabled = false; lbl_L.Enabled = false;
                    txt_M.Enabled = false; lbl_M.Enabled = false;
                    txt_N.Enabled = false; lbl_N.Enabled = false;
                    txt_O.Enabled = false; lbl_O.Enabled = false;
                    break;
            }



        }

        private void comb_Q_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comb_T_SelectedIndexChanged(object sender, EventArgs e)
        {
            com_W.Items.Clear();
            txt_Y.Enabled = true; lbl_Y.Enabled = true;
            txt_Z.Enabled = true; lbl_Z.Enabled = true;

            txt_S.Enabled = false; lbl_S.Enabled = false;
            txt_T.Enabled = false; lbl_T.Enabled = false;
            txt_U.Enabled = false; lbl_U.Enabled = false;
            txt_V.Enabled = false; lbl_V.Enabled = false;

            switch (com_R.Text)
            {
                case "_NONE":
                    pic_frac2.Image = null;
                    com_W.Items.Add("_LINEAR");
                    com_W.Items.Add("_QUADRATIC");
                    com_W.Items.Add("_CUBIC");
                    if (txt_Y.Text == "") txt_Y.Text = "0";
                    if (txt_Z.Text == "") txt_Z.Text = "0";
                    txt_Y.Enabled = false; lbl_Y.Enabled = false;
                    txt_Z.Enabled = false; lbl_Z.Enabled = false;
                    break;
                case "_LINEAR":
                    pic_frac2.Image = Image.FromFile("image/" + "NLMM104-3.png");
                    com_W.Text = "0";

                    break;
                case "_TRILINEAR":
                    pic_frac2.Image = Image.FromFile("image/" + "NLMM104-5-TRI.png");
                    txt_S.Enabled = true; lbl_S.Enabled = true;
                    txt_T.Enabled = true; lbl_T.Enabled = true;
                    txt_U.Enabled = true; lbl_U.Enabled = true;
                    txt_V.Enabled = true; lbl_V.Enabled = true;
                    break;
                case "_CORNELISSEN":
                    pic_frac2.Image = Image.FromFile("image/" + "NLMM104-5-CORN.png");
                    txt_S.Enabled = true; lbl_S.Enabled = true;
                    txt_T.Enabled = true; lbl_T.Enabled = true;
                    txt_U.Enabled = true; lbl_U.Enabled = true;
                    txt_V.Enabled = true; lbl_V.Enabled = true;
                    break;
            }
        }

        private void comb_U_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comb_U_TextChanged(object sender, EventArgs e)
        {
            if (com_W.Text == "_LINEAR" || com_W.Text == "_QUADRATIC" || com_W.Text == "_CUBIC")
            {
                com_X.Enabled = true; lbl_X.Enabled = true;
            }
            else
            {
                if (com_X.SelectedIndex == -1)
                    com_X.SelectedIndex = 0;
                com_X.Enabled = false; lbl_X.Enabled = false;
            }
        }

        private void com_X_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
