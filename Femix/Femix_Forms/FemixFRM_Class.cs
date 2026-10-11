using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Femix.Femix_Forms
{
    public class FemixFRM_Class
    {
        
        //public MainForm mainfrm;
        //public Results_class(MainForm form)
        //{
        //    this.mainfrm = form;
        //}
        //public List<IList<string>> GROUP_NAMES_list;
        //public static List<IList<string>> GROUP_NAMES_list_static;

        //public string frm_list_selected_item { get; set; }

        public static string[] Editing_val_Arr;
        public static bool is_new;
        public string kamyar()
        {
            return "Kamyar_ba@yahoo.com";
        }

        public static string[] Run_form(string selected_item, string[] Editing_val)
        {
           
            is_new = false;
            Editing_val_Arr = Editing_val.ToArray();

            Form form1 = new Form();
            form1 = formname1(selected_item);

            if (form1 == null)
                return null;
            form1.ShowDialog();


            return Editing_val_Arr;
        }
        private static Form formname1( string selected_item)
        {
            switch (selected_item)
            {
                case "MAIN_PARAMETERS":
                    break;
                case "GROUP_NAMES":
                   return new group_frm();
                case "POINT_COORDINATES":
                    break;
                case "AUXILIARY_VECTORS":

                    break;

                case "COORDINATE_SYSTEMS":

                    break;

                case "POINTS_WITH_SPECIAL_COORDINATE_SYSTEM":

                    break;

                case "SUPPORTS":

                    break;

                case "INTERNAL_POINT_LINKS":

                    break;

                case "LINEAR_ISOTROPIC_MATERIALS":
                    return new LINEAR_ISOTROPIC_MATERIALS();
                case "LINEAR_ORTHOTROPIC_MATERIALS":

                    break;

                case "LINEAR_INTERFACE_POINT_PROPERTIES":
                    return new LINEAR_INTERFACE_POINT_PROPERTIES();
                case "LINEAR_INTERFACE_LINE_PROPERTIES":
                    return new LINEAR_INTERFACE_LINE_PROPERTIES();
                case "LINEAR_INTERFACE_SURFACE_PROPERTIES":
                    return new LINEAR_INTERFACE_SURFACE_PROPERTIES();
                case "NLMM101":
                    return new NLMM101();
                case "NLMM104":
                    return new NLMM104();
                case "NLMM111":
                    return new NLMM111();
                case "NLMM141":
                    return new NLMM141();
                case "NLMM151":
                    return new NLMM151();
                case "NLMM201":
                    return new NLMM201();
                case "NLMM202":

                    break;
                case "NLMM210":

                    break;
                case "NLMM220":

                    break;
                case "NLMM298":

                    break;
                case "NLMM299":

                    break;
                case "NLMM123":

                    break;
                case "NLMM121":

                    break;
                case "NLMM124":

                    break;
                case "NLMM125":

                    break;
                case "NLMM142":

                    break;
                case "NLMM145":

                    break;
                case "NLMM211":

                    break;
                case "NLMM102":

                    break;
                case "NLMM122":

                    break;
                case "NLMM401":

                    break;
                case "NLMM451":

                    break;
                case "NLMM301":
                    return new NLMM301();
                case "NLMM302":
                    return new NLMM302();
                case "NLMM303":

                    break;
                case "NLMM305":
                    return new NLMM305();
                case "NLMM306":

                    break;
                case "NLMM320":

                    break;

                case "CROSS_SECTION_AREAS":
                    return new CROSS_SECTION_AREAS();
                case "FRAME_2D_CROSS_SECTIONS":

                    break;

                case "FRAME_3D_CROSS_SECTIONS":

                    break;

                case "LAMINATE_THICKNESSES":
                    return new LAMINATE_THICKNESSES_frm();
                case "GEOMETRY_PATTERNS":

                    break;

                case "FIBROUS_PATTERNS":

                    break;

                case "LAYER_PROPERTIES":
                    return new LAYER_PROPERTIES_frm();
                case "LAYER_PATTERNS":
                    return new LAYER_PATTERNS_frm();
                case "NUMERICAL_INTEGRATION":
                    return new NUMERICAL_INTEGRATION();
                case "ELEMENT_NODES":

                    break;

                case "ELEMENT_PROPERTIES":
                    return new element_prop_frm();
                case "LINEAR_SPRING_MATERIALS":

                    break;

                case "POINT_SPRINGS":

                    break;

                case "LINE_SPRINGS":

                    break;

                case "SURFACE_SPRINGS":

                    break;

                case "STIFFENERS":

                    break;

                case "LOAD_CASE_PARAMETERS":


                    break;

                case "GRAVITY_LOAD":


                    break;

                case "POINT_LOADS":


                    break;

                case "INTERNAL_POINT_LOADS":

                    break;

                case "EDGE_LOADS":

                    break;

                case "INTERNAL_EDGE_LOADS":

                    break;

                case "FACE_LOADS":

                    break;

                case "INTERNAL_FACE_LOADS":

                    break;

                case "TEMPERATURE_VARIATION":

                    break;

                case "DIFFERENTIAL_TEMPERATURE_VARIATION":

                    break;

                case "PRESCRIBED_DISPLACEMENTS":

                    break;

                case "INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS":

                    break;

                case "LOAD_CASE_COMBINATIONS":

                    break;

                default:
                   
                    break;
            }
            return null;
        }

        public static List<IList<string>> new_list_after_call(string item)
        {
            switch (item)
            {
                case "GROUP_NAMES":
                    return C_variables.GROUP_NAMES_list.ToList();
                case "LINEAR_ISOTROPIC_MATERIALS":
                    return C_variables.LINEAR_ISOTROPIC_MATERIALS_list.ToList();
                case "LINEAR_ORTHOTROPIC_MATERIALS":
                    return C_variables.LINEAR_ORTHOTROPIC_MATERIALS_list.ToList();
                case "NLMM101":
                    return C_variables.NLMM101_list.ToList();
                case "NLMM104":
                    return C_variables.NLMM104_list.ToList();
                case "NLMM111":
                    return C_variables.NLMM111_list.ToList();
                case "NLMM141":
                    return C_variables.NLMM141_list.ToList();
                case "NLMM151":
                    return C_variables.NLMM151_list.ToList();
                case "NLMM201":
                    return C_variables.NLMM201_list.ToList();
                case "NLMM202":
                    return C_variables.NLMM202_list.ToList();
                case "NLMM210":
                    return C_variables.NLMM210_list.ToList();
                case "NLMM220":
                    return C_variables.NLMM220_list.ToList();
                case "NLMM298":
                    return C_variables.NLMM298_list.ToList();
                case "NLMM299":
                    return C_variables.NLMM299_list.ToList();
                case "NLMM123":
                    return C_variables.NLMM123_list.ToList();
                case "NLMM121":
                    return C_variables.NLMM121_list.ToList();
                case "NLMM124":
                    return C_variables.NLMM124_list.ToList();
                case "NLMM125":
                    return C_variables.NLMM125_list.ToList();
                case "NLMM142":
                    return C_variables.NLMM142_list.ToList();
                case "NLMM145":
                    return C_variables.NLMM145_list.ToList();
                case "NLMM211":
                    return C_variables.NLMM211_list.ToList();
                case "NLMM102":
                    return C_variables.NLMM102_list.ToList();
                case "NLMM122":
                    return C_variables.NLMM122_list.ToList();
                case "NLMM401":
                    return C_variables.NLMM401_list.ToList();
                case "NLMM451":
                    return C_variables.NLMM451_list.ToList();
                case "NLMM301":
                    return C_variables.NLMM301_list.ToList();
                case "NLMM302":
                    return C_variables.NLMM302_list.ToList();
                case "NLMM303":
                    return C_variables.NLMM303_list.ToList();
                case "NLMM305":
                    return C_variables.NLMM305_list.ToList();
                case "NLMM306":
                    return C_variables.NLMM306_list.ToList();
                case "NLMM320":
                    return C_variables.NLMM320_list.ToList();

                case "LAYER_PATTERNS":
                    return C_variables.LAYER_PATTERNS_list.ToList();
                case "LAMINATE_THICKNESSES":
                    return C_variables.LAMINATE_THICKNESSES_list.ToList();
                case "CROSS_SECTION_AREAS":
                    return C_variables.CROSS_SECTION_AREAS_list.ToList();
                case "FRAME_2D_CROSS_SECTIONS":
                    return C_variables.FRAME_2D_CROSS_SECTIONS_list.ToList();
                case "FRAME_3D_CROSS_SECTIONS":
                    return C_variables.FRAME_3D_CROSS_SECTIONS_list.ToList();
                case "NUMERICAL_INTEGRATION":
                    return C_variables.NUMERICAL_INTEGRATION_list.ToList();
                case "LAYER_PROPERTIES":
                    return C_variables.LAYER_PROPERTIES_list.ToList();
                default:
                    break;
            }
            return null;
        }
        public static void generate_combo_item(string item, List<IList<string>> list_item, int column_number, ComboBox outputcombo)
        {
            int old_count, new_count;
            frm_list_items formlist = new frm_list_items();
            formlist.selected_item = item;
            old_count = list_item.Count;
            formlist.ShowDialog();
            if (formlist.is_there_change == true)
            {
                outputcombo.Items.Clear();
                list_item = new_list_after_call(item);
                for (int i = 0; i < list_item.Count; i++)
                    outputcombo.Items.Add(list_item[i][column_number]);
            }
            new_count = list_item.Count;
            if (new_count > old_count)
                outputcombo.SelectedIndex = outputcombo.Items.Count - 1;
        }
    }
}
