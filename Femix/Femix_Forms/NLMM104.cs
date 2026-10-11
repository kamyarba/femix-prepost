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
    public partial class NLMM104 : Form
    {

        private string[] Editing_val;
        private string mat_lib_folder;
        public NLMM104()
        {
            InitializeComponent();
        }

        private void group_frm_Load(object sender, EventArgs e)
        {
            combo_new.SelectedIndex = 0;
            Editing_val = FemixFRM_Class.Editing_val_Arr.ToArray();

            this.Text = "NLMM 104";
            groupBox1.Text = "NLMM 104";
            mat_lib_folder = "NLMM104";

            txt_B.Text = Editing_val[0];
            txt_C.Text = Editing_val[1];
            txt_D.Text = Editing_val[2];
            txt_E.Text = Editing_val[3];
            txt_F.Text = Editing_val[4];

            txt_G.Text = Editing_val[5];
            txt_H.Text = Editing_val[6];
            comb_I.Text = Editing_val[7];
            txt_J.Text = Editing_val[8];
            txt_K.Text = Editing_val[9];
            txt_L.Text = Editing_val[10];
            txt_M.Text = Editing_val[11];
            txt_N.Text = Editing_val[12];
            txt_O.Text = Editing_val[13];
            txt_P.Text = Editing_val[14];
            comb_Q.Text = Editing_val[15];
            comb_R.Text = Editing_val[16];
            comb_S.Text = Editing_val[17];

            comb_T.Text = Editing_val[18];
            comb_U.Text = Editing_val[19];
            comb_V.Text = Editing_val[20];
            txt_W.Text = Editing_val[21];
            txt_X.Text = Editing_val[22];

            comb_Y.Text = Editing_val[23];
            comb_Z.Text = Editing_val[24];
            txt_AA.Text = Editing_val[25];
            txt_AB.Text = Editing_val[26];

            comb_AC.Text = Editing_val[27];
            txt_AD.Text = Editing_val[28];
            txt_AE.Text = Editing_val[29];
            txt_AF.Text = Editing_val[30];
            txt_AG.Text = Editing_val[31];
            txt_AH.Text = Editing_val[32];
            txt_AI.Text = Editing_val[33];


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
            Editing_val[7] = comb_I.Text;
            Editing_val[8] = txt_J.Text;
            Editing_val[9] = txt_K.Text;
            Editing_val[10] = txt_L.Text;
            Editing_val[11] = txt_M.Text;
            Editing_val[12] = txt_N.Text;
            Editing_val[13] = txt_O.Text;
            Editing_val[14] = txt_P.Text;
            Editing_val[15] = comb_Q.Text;
            Editing_val[16] = comb_R.Text;
            Editing_val[17] = comb_S.Text;

            Editing_val[18] = comb_T.Text;
            Editing_val[19] = comb_U.Text;
            Editing_val[20] = comb_V.Text;
            Editing_val[21] = txt_W.Text;
            Editing_val[22] = txt_X.Text;

            Editing_val[23] = comb_Y.Text;
            Editing_val[24] = comb_Z.Text;
            Editing_val[25] = txt_AA.Text;
            Editing_val[26] = txt_AB.Text;

            Editing_val[27] = comb_AC.Text;
            Editing_val[28] = txt_AD.Text;
            Editing_val[29] = txt_AE.Text;
            Editing_val[30] = txt_AF.Text;
            Editing_val[31] = txt_AG.Text;
            Editing_val[32] = txt_AH.Text;
            Editing_val[33] = txt_AI.Text;

            for (int i = 0; i <= 33; i++)
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
            comb_I.Text = lines[7];
            txt_J.Text = lines[8];
            txt_K.Text = lines[9];
            txt_L.Text = lines[10];
            txt_M.Text = lines[11];
            txt_N.Text = lines[12];
            txt_O.Text = lines[13];
            txt_P.Text = lines[14];
            comb_Q.Text = lines[15];
            comb_R.Text = lines[16];
            comb_S.Text = lines[17];

            comb_T.Text = lines[18];
            comb_U.Text = lines[19];
            comb_V.Text = lines[20];
            txt_W.Text = lines[21];
            txt_X.Text = lines[22];

            comb_Y.Text = lines[23];
            comb_Z.Text = lines[24];
            txt_AA.Text = lines[25];
            txt_AB.Text = lines[26];

            comb_AC.Text = lines[27];
            txt_AD.Text = lines[28];
            txt_AE.Text = lines[29];
            txt_AF.Text = lines[30];
            txt_AG.Text = lines[31];
            txt_AH.Text = lines[32];
            txt_AI.Text = lines[33];

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
           filedata.Add(comb_I.Text);
           filedata.Add(txt_J.Text);
           filedata.Add(txt_K.Text);
           filedata.Add( txt_L.Text);
           filedata.Add( txt_M.Text);
           filedata.Add( txt_N.Text);
           filedata.Add( txt_O.Text);
           filedata.Add( txt_P.Text);
           filedata.Add( comb_Q.Text);
           filedata.Add( comb_R.Text);
           filedata.Add( comb_S.Text);
           filedata.Add( comb_T.Text);
           filedata.Add( comb_U.Text);
           filedata.Add( comb_V.Text);
           filedata.Add( txt_W.Text);
           filedata.Add( txt_X.Text);
           filedata.Add( comb_Y.Text);
           filedata.Add( comb_Z.Text);
           filedata.Add( txt_AA.Text);
           filedata.Add( txt_AB.Text);
           filedata.Add( comb_AC.Text);
           filedata.Add( txt_AD.Text);
           filedata.Add( txt_AE.Text);
           filedata.Add( txt_AF.Text);
           filedata.Add( txt_AG.Text);
           filedata.Add( txt_AH.Text);
           filedata.Add( txt_AI.Text);




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

            txt_J.Enabled = true;    lbl_J.Enabled = true;
            txt_K.Enabled = true;    lbl_K.Enabled = true;
            txt_L.Enabled = true;    lbl_L.Enabled = true;
            txt_M.Enabled = true;    lbl_M.Enabled = true;
            txt_N.Enabled = true;    lbl_N.Enabled = true;
            txt_O.Enabled = true;    lbl_O.Enabled = true;
            switch (comb_I.Text)
            {
                case "_TRILINEAR":
                    pic_frac1.Image = Image.FromFile("image/"+ "NLMM104-2-TRI.png");
                    txt_N.Enabled = false; lbl_N.Enabled = false;
                    txt_O.Enabled = false; lbl_O.Enabled = false;
                    break;
                case "_QUADRILINEAR":
                    pic_frac1.Image = Image.FromFile("image/" + "NLMM104-2-QUAD.png");
                    break;
                case "_CORNELISSEN":
                    pic_frac1.Image = Image.FromFile("image/" + "NLMM104-2-CORN.png");
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
            if (comb_Q.Text== "_NONE")
            {
                comb_R.Enabled = false;
                lbl_R.Enabled = false;
            }
            else
            {
                comb_R.Enabled = true;
                lbl_R.Enabled = true;
            }
        }

        private void comb_T_SelectedIndexChanged(object sender, EventArgs e)
        {
            comb_U.Items.Clear();
            txt_W.Enabled = true; lbl_W.Enabled = true;
            txt_X.Enabled = true; lbl_X.Enabled = true;
            switch (comb_T.Text)
            {
                case "_NONE":
                    pic_frac2.Image = null;
                    comb_U.Items.Add("_LINEAR");
                    comb_U.Items.Add("_QUADRATIC");
                    comb_U.Items.Add("_CUBIC");
                    txt_W.Enabled = false; lbl_W.Enabled = false;
                    txt_X.Enabled = false; lbl_X.Enabled = false;
                    break;
                case "_LINEAR":
                    pic_frac2.Image = Image.FromFile("image/" + "NLMM104-3.png");
                    comb_U.Text = "0";

                    break;
            }
        }

        private void comb_U_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void comb_U_TextChanged(object sender, EventArgs e)
        {
            if (comb_U.Text == "_LINEAR" || comb_U.Text == "_QUADRATIC" || comb_U.Text == "_CUBIC")
            {
                comb_V.Enabled = true; lbl_V.Enabled = true;
            }
            else
            {
                if (comb_V.SelectedIndex == -1)
                    comb_V.SelectedIndex = 0;
                comb_V.Enabled = false; lbl_V.Enabled = false;
            }
        }

        private void comb_AC_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_AD.Enabled = true;lbl_AD.Enabled = true;
            txt_AE.Enabled = true;lbl_AE.Enabled = true;
            txt_AF.Enabled = true;lbl_AF.Enabled = true;
            txt_AG.Enabled = true;lbl_AG.Enabled = true;
            txt_AH.Enabled = true;lbl_AH.Enabled = true;
            txt_AI.Enabled = true;lbl_AI.Enabled = true;

            switch (comb_AC.Text)
            {
                case "_NONE":
                    pic_op.Image = null;
                    txt_AD.Enabled = false; lbl_AD.Enabled = false;
                    txt_AE.Enabled = false; lbl_AE.Enabled = false;
                    txt_AF.Enabled = false; lbl_AF.Enabled = false;
                    txt_AG.Enabled = false; lbl_AG.Enabled = false;
                    txt_AH.Enabled = false; lbl_AH.Enabled = false;
                    txt_AI.Enabled = false; lbl_AI.Enabled = false;
                    break;
                case "_LINEAR":
                    pic_op.Image = Image.FromFile("image/" + "NLMM104-5-LIN.png");
                    txt_AF.Enabled = false; lbl_AF.Enabled = false;
                    txt_AG.Enabled = false; lbl_AG.Enabled = false;
                    txt_AH.Enabled = false; lbl_AH.Enabled = false;
                    txt_AI.Enabled = false; lbl_AI.Enabled = false;
                    break;
                case "_TRILINEAR":
                    pic_op.Image = Image.FromFile("image/" + "NLMM104-5-TRI.png");
                    break;
                case "_CORNELISSEN":
                    pic_op.Image = Image.FromFile("image/" + "NLMM104-5-CORN.png");
                    txt_AF.Enabled = false; lbl_AF.Enabled = false;
                    txt_AG.Enabled = false; lbl_AG.Enabled = false;
                    txt_AH.Enabled = false; lbl_AH.Enabled = false;
                    txt_AI.Enabled = false; lbl_AI.Enabled = false;
                    break;
            }
        }
    }
}
