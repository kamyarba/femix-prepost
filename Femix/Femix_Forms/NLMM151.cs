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
    public partial class NLMM151 : Form
    {
        private string[] Editing_val;
        private string mat_lib_folder;
        public NLMM151()
        {
            InitializeComponent();
        }

        private void group_frm_Load(object sender, EventArgs e)
        {
            combo_new.SelectedIndex = 0;
            Editing_val = FemixFRM_Class.Editing_val_Arr.ToArray();

            this.Text = "NLMM 151";
            groupBox1.Text = "NLMM 151";
            mat_lib_folder = "NLMM151";

            txt_B.Text = Editing_val[0];
            txt_C.Text = Editing_val[1];
            txt_D.Text = Editing_val[2];
            txt_E.Text = Editing_val[3];
            txt_F.Text = Editing_val[4];
            txt_G.Text = Editing_val[5];
            txt_H.Text = Editing_val[6];
            txt_I.Text = Editing_val[7];
            txt_J.Text = Editing_val[8];
            com_K.Text = Editing_val[9];
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
            txt_W.Text = Editing_val[21];
            txt_X.Text = Editing_val[22];
            txt_Y.Text = Editing_val[23];
            txt_Z.Text = Editing_val[24];
            com_AA.Text = Editing_val[25];
            txt_AB.Text = Editing_val[26];
            txt_AC.Text = Editing_val[27];
            txt_AD.Text = Editing_val[28];
            txt_AE.Text = Editing_val[29];
            com_AF.Text = Editing_val[30];
            com_AG.Text = Editing_val[31];
            txt_AI.Text = Editing_val[32];
            com_AJ.Text = Editing_val[33];
            com_AK.Text = Editing_val[34];
            txt_AL.Text = Editing_val[35];
            txt_AM.Text = Editing_val[36];


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
            Editing_val[7] = txt_I.Text;
            Editing_val[8] = txt_J.Text;
            Editing_val[9] = com_K.Text;
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
            Editing_val[21] = txt_W.Text;
            Editing_val[22] = txt_X.Text;
            Editing_val[23] = txt_Y.Text;
            Editing_val[24] = txt_Z.Text;
            Editing_val[25] = com_AA.Text;
            Editing_val[26] = txt_AB.Text;
            Editing_val[27] = txt_AC.Text;
            Editing_val[28] = txt_AD.Text;
            Editing_val[29] = txt_AE.Text;
            Editing_val[30] = com_AF.Text;
            Editing_val[31] = com_AG.Text;
            Editing_val[32] = txt_AI.Text;
            Editing_val[33] = com_AJ.Text;
            Editing_val[34] = com_AK.Text;
            Editing_val[35] = txt_AL.Text;
            Editing_val[36] = txt_AM.Text;

            for (int i = 0; i <= 36; i++)
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
            txt_I.Text = lines[7];
            txt_J.Text = lines[8];
            com_K.Text = lines[9];
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
            txt_W.Text = lines[21];
            txt_X.Text = lines[22];
            txt_Y.Text = lines[23];
            txt_Z.Text = lines[24];
            com_AA.Text = lines[25];
            txt_AB.Text = lines[26];
            txt_AC.Text = lines[27];
            txt_AD.Text = lines[28];
            txt_AE.Text = lines[29];
            com_AF.Text = lines[30];
            com_AG.Text = lines[31];
            txt_AI.Text = lines[32];
            com_AJ.Text = lines[33];
            com_AK.Text = lines[34];
            txt_AL.Text = lines[35];
            txt_AM.Text = lines[36];

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_B.Text == "")
                return;

            List<string> filedata = new List<string>();

            filedata.Add(txt_B.Text);
            filedata.Add(txt_C.Text);
            filedata.Add(txt_D.Text);
            filedata.Add(txt_E.Text);
            filedata.Add(txt_F.Text);
            filedata.Add(txt_G.Text);
            filedata.Add(txt_H.Text);
            filedata.Add(txt_I.Text);
            filedata.Add(txt_J.Text);
            filedata.Add(com_K.Text);
            filedata.Add(txt_L.Text);
            filedata.Add(txt_M.Text);
            filedata.Add(txt_N.Text);
            filedata.Add(txt_O.Text);
            filedata.Add(txt_P.Text);
            filedata.Add(txt_Q.Text);
            filedata.Add(com_R.Text);
            filedata.Add(txt_S.Text);
            filedata.Add(txt_T.Text);
            filedata.Add(txt_U.Text);
            filedata.Add(txt_V.Text);
            filedata.Add(txt_W.Text);
            filedata.Add(txt_X.Text);
            filedata.Add(txt_Y.Text);
            filedata.Add(txt_Z.Text);
            filedata.Add(com_AA.Text);
            filedata.Add(txt_AB.Text);
            filedata.Add(txt_AC.Text);
            filedata.Add(txt_AD.Text);
            filedata.Add(txt_AE.Text);
            filedata.Add(com_AF.Text);
            filedata.Add(com_AG.Text);
            filedata.Add(txt_AI.Text);
            filedata.Add(com_AJ.Text);
            filedata.Add(com_AK.Text);
            filedata.Add(txt_AL.Text);
            filedata.Add(txt_AM.Text);


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

            txt_S.Enabled = true; lbl_S.Enabled = true;
            txt_T.Enabled = true; lbl_T.Enabled = true;
            txt_U.Enabled = true; lbl_U.Enabled = true;
            txt_V.Enabled = true; lbl_V.Enabled = true;
            txt_W.Enabled = true; lbl_W.Enabled = true;
            txt_X.Enabled = true; lbl_X.Enabled = true;
            switch (com_R.Text)
            {
                case "_TRILINEAR":
                    pic_frac1.Image = Image.FromFile("image/" + "NLMM104-2-TRI.png");
                    if (txt_W.Text == "") txt_W.Text = "0";
                    if (txt_X.Text == "") txt_X.Text = "0";
                    txt_W.Enabled = false; lbl_W.Enabled = false;
                    txt_X.Enabled = false; lbl_X.Enabled = false;
                    break;
                case "_QUADRILINEAR":
                    pic_frac1.Image = Image.FromFile("image/" + "NLMM104-2-QUAD.png");
                    break;
                case "_CORNELISSEN":
                    pic_frac1.Image = Image.FromFile("image/" + "NLMM104-2-CORN.png");
                    if (txt_S.Text == "") txt_S.Text = "0";
                    if (txt_T.Text == "") txt_T.Text = "0";
                    if (txt_U.Text == "") txt_U.Text = "0";
                    if (txt_V.Text == "") txt_V.Text = "0";
                    if (txt_W.Text == "") txt_W.Text = "0";
                    if (txt_X.Text == "") txt_X.Text = "0";
                    txt_S.Enabled = false; lbl_S.Enabled = false;
                    txt_T.Enabled = false; lbl_T.Enabled = false;
                    txt_U.Enabled = false; lbl_U.Enabled = false;
                    txt_V.Enabled = false; lbl_V.Enabled = false;
                    txt_W.Enabled = false; lbl_W.Enabled = false;
                    txt_X.Enabled = false; lbl_X.Enabled = false;
                    break;
            }



        }




        private void comb_U_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


     

        private void com_AA_SelectedIndexChanged(object sender, EventArgs e)
        {
            com_AF.Items.Clear();
            txt_AH.Enabled = true; lbl_AH.Enabled = true;
            txt_AI.Enabled = true; lbl_AI.Enabled = true;

            txt_AB.Enabled = false; lbl_AB.Enabled = false;
            txt_AC.Enabled = false; lbl_AC.Enabled = false;
            txt_AD.Enabled = false; lbl_AD.Enabled = false;
            txt_AE.Enabled = false; lbl_AE.Enabled = false;

            switch (com_AA.Text)
            {
                case "_NONE":
                    pic_frac2.Image = null;
                    com_AF.Items.Add("_LINEAR");
                    com_AF.Items.Add("_QUADRATIC");
                    com_AF.Items.Add("_CUBIC");
                    txt_AH.Enabled = false; lbl_AH.Enabled = false;
                    txt_AI.Enabled = false; lbl_AI.Enabled = false;
                    break;
                case "_LINEAR":
                    pic_frac2.Image = Image.FromFile("image/" + "NLMM104-3.png");
                    com_AF.Text = "0";

                    break;
                case "_TRILINEAR":
                    pic_frac2.Image = Image.FromFile("image/" + "NLMM104-5-TRI.png");
                    txt_AB.Enabled = true; lbl_AB.Enabled = true;
                    txt_AC.Enabled = true; lbl_AC.Enabled = true;
                    txt_AD.Enabled = true; lbl_AD.Enabled = true;
                    txt_AE.Enabled = true; lbl_AE.Enabled = true;
                    break;
                case "_CORNELISSEN":
                    pic_frac2.Image = Image.FromFile("image/" + "NLMM104-5-CORN.png");
                    txt_AB.Enabled = true; lbl_AB.Enabled = true;
                    txt_AC.Enabled = true; lbl_AC.Enabled = true;
                    txt_AD.Enabled = true; lbl_AD.Enabled = true;
                    txt_AE.Enabled = true; lbl_AE.Enabled = true;
                    break;
            }
        }

        private void txt_G_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txt_L_TextChanged(object sender, EventArgs e)
        {
            if (txt_L.Text == "0")
            {
                txt_M.Enabled = false;lbl_M.Enabled = false;
                txt_N.Enabled = false;lbl_N.Enabled = false;
                txt_O.Enabled = false;lbl_O.Enabled = false;
                txt_P.Enabled = false; lbl_P.Enabled = false;
            }
            else
            {
                txt_M.Enabled = true; lbl_M.Enabled = true;
                txt_N.Enabled = true; lbl_N.Enabled = true;
                txt_O.Enabled = true; lbl_O.Enabled = true;
                txt_P.Enabled = true; lbl_P.Enabled = true;
            }
        }
    }
}
