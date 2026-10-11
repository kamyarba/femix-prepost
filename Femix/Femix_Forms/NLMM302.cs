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
    public partial class NLMM302 : Form
    {

        private string[] Editing_val;
        private string mat_lib_folder;
        public NLMM302()
        {
            InitializeComponent();
        }

        private void group_frm_Load(object sender, EventArgs e)
        {
            combo_new.SelectedIndex = 0;
            Editing_val = FemixFRM_Class.Editing_val_Arr.ToArray();

            this.Text = "NLMM302";
            groupBox1.Text = "NLMM302";
            mat_lib_folder = "NLMM302";

            txt_B.Text = Editing_val[0];
            txt_C.Text = Editing_val[1];
            txt_D.Text = Editing_val[2];
            txt_E.Text = Editing_val[3];
            txt_F.Text = Editing_val[4];
            txt_G.Text = Editing_val[5];
            txt_H.Text = Editing_val[6];
            txt_I.Text = Editing_val[7];
            txt_J.Text = Editing_val[8];

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

            for (int i = 0; i <= 8; i++)
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

            File.WriteAllLines("Material_Library/" + mat_lib_folder + "/" + txt_B.Text + ".mat", filedata);
            //comb_mat_lib.Items.Add(txt_B.Text);
            comb_mat_lib.Items.Clear();
            string[] files = Directory.GetFiles("Material_Library/" + mat_lib_folder, "*.mat");
            foreach (var file in files)
                comb_mat_lib.Items.Add(Path.GetFileNameWithoutExtension(file));

            comb_mat_lib.Text = txt_B.Text;

            //System.IO.File.Delete("Material_Library/"+ mat_lib_folder+"/"+comb_mat_lib.Text+".mat");
        }
    }
}
