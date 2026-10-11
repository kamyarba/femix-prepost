using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FemixFRM
{
    public class FemixFRM_Class
    {
        //public MainForm mainfrm;
        //public Results_class(MainForm form)
        //{
        //    this.mainfrm = form;
        //}
        public List<IList<string>> GROUP_NAMES_list;
        public static List<IList<string>> GROUP_NAMES_list_static;

        //public string frm_list_selected_item { get; set; }

        public static string[] Editing_val_Arr;
        public static bool is_new;
        public bool Is_new;
        public string kamyar()
        {
            return "Kamyar_ba@yahoo.com";
        }

        public string[] Run_form(string selected_item, string[] Editing_val)
        {
            is_new = false;
            Editing_val_Arr = Editing_val.ToArray();

            GROUP_NAMES_list_static = GROUP_NAMES_list.ToList();

            Form form1 = new Form();
            form1 = formname1(selected_item);

            if (form1 == null)
                return null;
            form1.ShowDialog();
            Is_new = is_new;


            return Editing_val_Arr;
        }
        private Form formname1( string selected_item)
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

                    break;

                case "LINEAR_ORTHOTROPIC_MATERIALS":

                    break;

                case "LINEAR_INTERFACE_POINT_PROPERTIES":

                    break;

                case "LINEAR_INTERFACE_LINE_PROPERTIES":

                    break;

                case "LINEAR_INTERFACE_SURFACE_PROPERTIES":

                    break;

                case "NLMM101":

                    break;

                case "NLMM104":

                    break;

                case "NLMM111":

                    break;

                case "NLMM141":

                    break;

                case "NLMM151":

                    break;

                case "NLMM201":

                    break;

                case "CROSS_SECTION_AREAS":

                    break;

                case "FRAME_2D_CROSS_SECTIONS":

                    break;

                case "FRAME_3D_CROSS_SECTIONS":

                    break;

                case "LAMINATE_THICKNESSES":

                    break;

                case "GEOMETRY_PATTERNS":

                    break;

                case "FIBROUS_PATTERNS":

                    break;

                case "LAYER_PROPERTIES":

                    break;

                case "LAYER_PATTERNS":

                    break;

                case "NUMERICAL_INTEGRATION":

                    break;

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
    }
}
