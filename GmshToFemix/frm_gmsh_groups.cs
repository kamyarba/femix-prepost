using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GmshToFemix
{
    public partial class frm_gmsh_groups : Form
    {
        public frm_gmsh_groups()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listbox_all_element.Items.Clear();
            listbox_all_support.Items.Clear();
            listbox_selected_element.Items.Clear();
            listbox_selected_support.Items.Clear();
            foreach (string item in list_all_group)
            {
                listbox_all_element.Items.Add(item);
                listbox_all_support.Items.Add(item);
            }
            if (listbox_all_element.Items.Count > 0) { listbox_all_element.SelectedIndex = 0; }
            if (listbox_all_support.Items.Count > 0) { listbox_all_support.SelectedIndex = 0; }
        }

        private void listbox_all_element_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listbox_all_element_DoubleClick(object sender, EventArgs e)
        {
            string clicked_item = listbox_all_element.SelectedItem.ToString();
            listbox_selected_element.Items.Add(clicked_item);
            listbox_all_element.Items.Remove(clicked_item);
        }

        private void listbox_selected_element_DoubleClick(object sender, EventArgs e)
        {
            string clicked_item = listbox_selected_element.SelectedItem.ToString();
            listbox_all_element.Items.Add(clicked_item);
            listbox_selected_element.Items.Remove(clicked_item);
        }

        private void listbox_all_support_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listbox_all_support_DoubleClick(object sender, EventArgs e)
        {
            string clicked_item = listbox_all_support.SelectedItem.ToString();
            listbox_selected_support.Items.Add(clicked_item);
            listbox_all_support.Items.Remove(clicked_item);
        }

        private void listbox_selected_support_DoubleClick(object sender, EventArgs e)
        {
            string clicked_item = listbox_selected_support.SelectedItem.ToString();
            listbox_all_support.Items.Add(clicked_item);
            listbox_selected_support.Items.Remove(clicked_item);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var listBoxItem in listbox_selected_element.Items)
            {
                list_element_group.Add(listBoxItem.ToString());
            }
            foreach (var listBoxItem in listbox_selected_support.Items)
            {
                list_support_group.Add(listBoxItem.ToString());
            }
            this.Close();
        }
    }
}
