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
    public partial class NLMM141 : Form
    {
        private string[] Editing_val;
        private string mat_lib_folder;
        public NLMM141()
        {
            InitializeComponent();
        }

        private void group_frm_Load(object sender, EventArgs e)
        {
            combo_new.SelectedIndex = 0;
            Editing_val = FemixFRM_Class.Editing_val_Arr.ToArray();

            this.Text = "NLMM 141";
            groupBox1.Text = "NLMM 141";
            mat_lib_folder = "NLMM141";


            pic141_1.Image = Image.FromFile("image/" + "NLMM141-1.png");
            pic141_2.Image = Image.FromFile("image/" + "NLMM141-2.png");

            txt_B.Text = Editing_val[0];
            txt_C.Text = Editing_val[1];
            txt_D.Text = Editing_val[2];
            txt_E.Text = Editing_val[3];
            txt_F.Text = Editing_val[4];
            txt_G.Text = Editing_val[5];
            txt_H.Text = Editing_val[6];
            txt_I.Text = Editing_val[7];
            txt_J.Text = Editing_val[8];
            txt_K.Text = Editing_val[9];
            com_L.Text = Editing_val[10];
            txt_M.Text = Editing_val[11];
            txt_N.Text = Editing_val[12];
            txt_O.Text = Editing_val[13];
            txt_P.Text = Editing_val[14];
            txt_Q.Text = Editing_val[15];
            txt_R.Text = Editing_val[16];
            com_S.Text = Editing_val[17];
            com_T.Text = Editing_val[18];
            com_U.Text = Editing_val[19];
            txt_V.Text = Editing_val[20];
            txt_X.Text = Editing_val[21];


            //scan for material library
            comb_mat_lib.Items.Clear();

            string lib_directory = "Material_Library/" + mat_lib_folder;
            if (!Directory.Exists(lib_directory))
                Directory.CreateDirectory(lib_directory);
            string[] files = Directory.GetFiles(lib_directory, "*.mat"); foreach (var file in files)
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
            Editing_val[9] = txt_K.Text;
            Editing_val[10] = com_L.Text;
            Editing_val[11] = txt_M.Text;
            Editing_val[12] = txt_N.Text;
            Editing_val[13] = txt_O.Text;
            Editing_val[14] = txt_P.Text;
            Editing_val[15] = txt_Q.Text;
            Editing_val[16] = txt_R.Text;
            Editing_val[17] = com_S.Text;
            Editing_val[18] = com_T.Text;
            Editing_val[19] = com_U.Text;
            Editing_val[20] = txt_V.Text;
            Editing_val[21] = txt_X.Text;

            for (int i = 0; i <= 21; i++)
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
            txt_K.Text = lines[9];
            com_L.Text = lines[10];
            txt_M.Text = lines[11];
            txt_N.Text = lines[12];
            txt_O.Text = lines[13];
            txt_P.Text = lines[14];
            txt_Q.Text = lines[15];
            txt_R.Text = lines[16];
            com_S.Text = lines[17];
            com_T.Text = lines[18];
            com_U.Text = lines[19];
            txt_V.Text = lines[20];
            txt_X.Text = lines[21];

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
            filedata.Add(txt_K.Text);
            filedata.Add(com_L.Text);
            filedata.Add(txt_M.Text);
            filedata.Add(txt_N.Text);
            filedata.Add(txt_O.Text);
            filedata.Add(txt_P.Text);
            filedata.Add(txt_Q.Text);
            filedata.Add(txt_R.Text);
            filedata.Add(com_S.Text);
            filedata.Add(com_T.Text);
            filedata.Add(com_U.Text);
            filedata.Add(txt_V.Text);
            filedata.Add(txt_X.Text);

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

            txt_M.Enabled = true; lbl_M.Enabled = true;
            txt_N.Enabled = true; lbl_N.Enabled = true;
            txt_O.Enabled = true; lbl_O.Enabled = true;
            txt_P.Enabled = true; lbl_P.Enabled = true;
            txt_N.Enabled = true; lbl_N.Enabled = true;
            txt_O.Enabled = true; lbl_O.Enabled = true;
            switch (com_L.Text)
            {
                case "_TRILINEAR":
                    pic_frac1.Image = Image.FromFile("image/" + "NLMM104-2-TRI.png");
                    break;
                case "_CORNELISSEN":
                    pic_frac1.Image = Image.FromFile("image/" + "NLMM104-2-CORN.png");
                    txt_M.Enabled = false; lbl_M.Enabled = false;
                    txt_N.Enabled = false; lbl_N.Enabled = false;
                    txt_O.Enabled = false; lbl_O.Enabled = false;
                    txt_P.Enabled = false; lbl_P.Enabled = false;
                    txt_N.Enabled = false; lbl_N.Enabled = false;
                    txt_O.Enabled = false; lbl_O.Enabled = false;
                    break;
            }
        }

     
    }
}
