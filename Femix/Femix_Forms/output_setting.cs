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
    public partial class output_setting : Form
    {
        public static string[] input_param = new string[10];
        public static string[] output_param = new string[10];
        public output_setting()
        {
            InitializeComponent();
        }

        private void output_setting_Load(object sender, EventArgs e)
        {

            foreach (var item in C_variables.GROUP_NAMES_list)
            {
                combo_group.Items.Add(item[2]);
            }
            if (combo_group.Items.Count > 0)
                combo_group.SelectedIndex = 0;



            if (input_param[0] == "false")//haslayer
            {
                lbl_layer.Enabled = false;
                numericUpDown_layer.Enabled = false;
                lbl_layer_help.Enabled = false;
            }
            else
            {
            numericUpDown_layer.Minimum = 1;
            numericUpDown_layer.Maximum =decimal.Parse(input_param[1]);
            numericUpDown_layer.Value = 1;
                lbl_layer_help.Text = "<" + input_param[1];
            }

            numericUpDown_combo.Minimum = 1;
            numericUpDown_combo.Maximum = decimal.Parse(input_param[2]);
            numericUpDown_combo.Value = decimal.Parse(input_param[2]);
            lbl_combo_help .Text = "<" + input_param[2];
            chk_com.Checked = true;

            chk_gr.Checked = true;



        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            output_param[0] = "false";//runposfemix
            this.Close();
        }
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            output_param[0] = "true";//runposfemix
            output_param[1] = numericUpDown_layer.Value.ToString();//nlayer
            output_param[2] = numericUpDown_combo.Value.ToString();//ncombo
            if (chk_com.Checked == true)
                output_param[2] = "all";
            output_param[3] = (combo_group.SelectedIndex + 1).ToString();//ngroup
            if (chk_gr.Checked == true)
                output_param[3] = "all";
            output_param[4] = chk_di.CheckState.ToString();//dis
            output_param[5] = chk_stress.CheckState.ToString();//stress
            output_param[6] = chk_strain.CheckState.ToString();//strain
            output_param[7] = chk_cr.CheckState.ToString();//crack
            this.Close();
        }

        private void chk_com_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_com.Checked)
            {
                lbl_combo.Enabled = false;
                numericUpDown_combo.Enabled = false;
            }
            else
            {
                lbl_combo.Enabled = true;
                numericUpDown_combo.Enabled = true;
            }
        }

        private void chk_gr_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_gr.Checked)
            {
                lbl_group.Enabled = false;
                combo_group.Enabled = false;
            }
            else
            {
                lbl_group.Enabled = true;
                combo_group.Enabled = true;
            }
        }
    }
}
