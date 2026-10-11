using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;
using System.IO;
using System.Windows.Forms.Design;
using System.Diagnostics;
using System.Threading;
using Kitware.VTK;
using GmshToFemix;
using intersection;

namespace Femix
{
    public partial class MainForm : Form
    {
        string femix_filepath;
        //RenderWindowControl RenderWindow1;
        RenderInterface KbRen;
        Graph_class ZedGraph;
        Results_class Result_Fem;

        femix_log femix_log = new femix_log();
        ClassGMSHConvert GMSHconvert = new ClassGMSHConvert();

        kamyar_intersection intersection = new kamyar_intersection();//intersection class
        Point3D Point3d = new Point3D(); //intersection class
        Vector3D Vector3D = new Vector3D(); //intersection class
        
        
        public MainForm()
        {
            InitializeComponent();
            //toolStrip1.AutoSize = false;toolStrip1.Height = 24;
            //New_femix_toolstrip.AutoSize = false; New_femix_toolstrip.Width = 24;
            //open_femix_toolstrip.AutoSize = false; open_femix_toolstrip.Width = 24;
            //save_femix_toolstrip.AutoSize = false; save_femix_toolstrip.Width = 24;

            KbRen = new RenderInterface(this);

            ZedGraph = new Graph_class();
            Result_Fem = new Results_class(this);
            /*
            RenderWindow1 = new RenderWindowControl();
            RenderWindow1.Dock = System.Windows.Forms.DockStyle.Fill;
            RenderWindow1.Load += new System.EventHandler(this.RenderWindow1_Load);
            //this.Controls.Add(this.RenderWindow1);
            splitContainer3.Panel1.Controls.Add(this.RenderWindow1);
            */
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = saveFileDialog_femix.ShowDialog();
            if (result == DialogResult.OK)
            {
               //splitContainer3.Panel1.Controls.Clear();
                //KbRen.CreateViewport(splitContainer3);

                KbRen.Renderer.RemoveAllViewProps();
                KbRen.Refresh();


                femix_filepath = saveFileDialog_femix.FileName.Replace("_","-");
                femix_filepath = Path.GetDirectoryName(femix_filepath) + @"\" + Path.GetFileNameWithoutExtension(femix_filepath) + "_gl.dat";

                this.Text = "Femix PrePost (" + femix_filepath + ")";

                C_variables.Femix_string = "<FEMIX_DATA_FILE_V4.0>\n\n\n<MAIN_PARAMETERS>\n\n</MAIN_PARAMETERS>\n\n<MESH>\n";
                C_variables.Femix_string += "\n<GROUP_NAMES>\nCOUNT = 1 ;\n1   Femix_Group ;\n</GROUP_NAMES>";
                //C_variables.Femix_string += "\n<POINT_COORDINATES>\n\n</POINT_COORDINATES>\n<SUPPORTS>\n</SUPPORTS>\n";
                //C_variables.Femix_string += "\n<NUMERICAL_INTEGRATION>\n## Keywords: _GLEG, _GLOB or _NCOTES \n## Numerical integration definition \n# N. of layouts of integration points \n";
                //C_variables.Femix_string += "\nCOUNT = 5 ;";
                //C_variables.Femix_string += "\n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the layout \n#  C -> N. of keywords defining the layout \n#  D -> Keywords defining the type of term and the n. of points in each direction \n# \n#  Valid syntax for D is the following: \n#     _i_Sj_k \n# \n#     i: type of terms \n#       G -> General use (stiffness matrix, springs and load vector) \n#       M -> Membrane terms (stiffness matrix) \n#       S -> Shear terms (stiffness matrix) \n#       T -> Torsional terms (stiffness matrix) \n#       B -> Bending terms (stiffness matrix) \n# \n#     j: direction \n#       1 -> direction s1 \n#       2 -> direction s2 \n#       3 -> direction s3 \n# \n#     k: n. of points in the specified direction \n# \n#     Note: k must be in the range [1,10] \n# \n#     Examples: _G_S1_5, _M_S2_1, _S_S3_2, _T_S1_7, _B_S2_10 \n# \n#		A	B				C  D\n";
                //C_variables.Femix_string += "         1  PLANE_STRESS_QUAD_Integration  2  _G_S1_2  _G_S2_2  ;";
                //C_variables.Femix_string += "\n       2  EMB_CABLE_2D_Integration  1  _G_S1_2  ;";
                //C_variables.Femix_string += "\n	      3  EMB_CABLE_3D_Integration  1  _G_S1_2  ;";
                //C_variables.Femix_string += "\n	      4  SOLID_HEXA_Integration  3  _G_S1_2  _G_S2_2  _G_S3_2  ;";
                //C_variables.Femix_string += "\n       5  MINDLIN_SHELL_QUAD_Integration  6  _M_S1_2  _M_S2_2  _S_S1_2  _S_S2_2  _B_S1_2  _B_S2_2  ;";
                //C_variables.Femix_string += "\n</NUMERICAL_INTEGRATION>\n";
                C_variables.Femix_string += "\n<ELEMENT_NODES>\n\n</ELEMENT_NODES>\n\n<ELEMENT_PROPERTIES>\n\n</ELEMENT_PROPERTIES>\n\n\n</MESH>\n\n<LOAD>\n\n<LOAD_CASE>\n";
                //C_variables.Femix_string += "\n<LOAD_CASE_PARAMETERS>\nLOAD_CASE_NUMBER = 1 ;\nLOAD_CASE_TITLE = Load_Case_1 ;\nLOAD_CASE_GROUP = Femix_Group ;\nINTEGRATION_TYPE = _DEFAULT ; # Keywords: _GLEG, _GLOB, _NCOTES or _DEFAULT\nINTEGRATION_NAME = _DEFAULT ; # Defined in the -NUMERICAL_INTEGRATION- block or _DEFAULT\n</LOAD_CASE_PARAMETERS>\n";
                C_variables.Femix_string += "\n</LOAD_CASE>\n</LOAD>\n";
                C_variables.Femix_string += "\n</FEMIX_DATA_FILE_V4.0 >\n";

                Femixbox1.Text = C_variables.Femix_string;

                System.IO.File.WriteAllText(femix_filepath, C_variables.Femix_string);

                //load_femix_reader_and_draw();
                Class_femix_reader.extract_femix(femix_filepath, C_variables.Femix_string);
                
                femix_toolstip.Enabled = false;
                postfemix_toolstrip.Enabled = false;
                toolStripclean.Enabled = false;

                combo_part_names.Items.Clear();

                combo_sections_fill();
                tree_femix_fill();


                save_femix_toolstrip.Enabled = true;
                save_as_toolstrip.Enabled = true;
                tabControl_left.Enabled = true;
            }



        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            //FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(@"Kitware.VTK.dll");
            //MessageBox.Show(myFileVersionInfo.FileMajorPart.ToString());


            tabControl_left.Enabled = false;
            KbRen.CreateViewport(splitContainer3);
            //KbRen.RenWinControl1.Visible = false;

            ZedGraph.CreateGraphview(splitContainer3);
            ZedGraph.zg1.Visible = false;
            //ZedGraph.CreateGraph();

            //MessageBox.Show(GMSHconvert.kamyar());

            //GMSHconvert.frm();
            //tree_femix_fill();
            //combo_sections_fill();

            femix_toolstip.Enabled = false;
            postfemix_toolstrip.Enabled = false;
            toolStripclean.Enabled = false;

            save_femix_toolstrip.Enabled = false;
            save_as_toolstrip.Enabled = false;

            combo_xaxis.SelectedIndex = 0;
            combo_x_direction.SelectedIndex = 2;
            combo_yaxis.SelectedIndex = 0;
            combo_y_direction.SelectedIndex = 2;


            combo_results.Items.Clear();
            List<string> result_item = new List<string> {"Displacement","Stress","Strains","Crack pattern" };
            foreach (string item in result_item)
                combo_results.Items.Add(item);
            combo_results.SelectedIndex = 0;

            C_variables.Layer_items = new Dictionary<string, List<int>>();

        }

        private void tree_femix_fill()
        {
            Font boldFont = new Font(tree_femix.Font, FontStyle.Bold);
            tree_femix.Nodes.Clear();

            tree_femix.BeginUpdate();

            tree_femix.Nodes.Add("FEMIX_DATA_FILE");//0
            tree_femix.Nodes[0].NodeFont = boldFont;

            tree_femix.Nodes[0].Nodes.Add("MAIN_PARAMETERS ");//0-0


            if (C_variables.MAIN_PARAMETERS_n > 0)
            {
                foreach (var item in C_variables.MAIN_PARAMETERS_list)
                {
                    if (item[1] == "ANALYSIS_TYPE")
                        tree_femix.Nodes[0].Nodes[0].Text = "MAIN_PARAMETERS (" + item[2] + ")";
                    if (item[1] == "MAIN_TITLE")
                        tree_femix.Nodes[0].Text = item[2];

                }

            }


            tree_femix.Nodes[0].Nodes.Add("MESH");//0-1
            if (C_variables.GROUP_NAMES_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("GROUP_NAMES (" + C_variables.GROUP_NAMES_n + ")");//0-1-0
            }
            if (C_variables.POINT_COORDINATES_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("POINT_COORDINATES (" + C_variables.POINT_COORDINATES_n + ")");//0-1-1
            }
            if (C_variables.AUXILIARY_VECTORS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("AUXILIARY_VECTORS (" + C_variables.AUXILIARY_VECTORS_n + ")");//0-1-2
            }
            if (C_variables.COORDINATE_SYSTEMS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("COORDINATE_SYSTEMS (" + C_variables.COORDINATE_SYSTEMS_n + ")");//0-1-3
            }
            if (C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("POINTS_WITH_SPECIAL_COORDINATE_SYSTEM (" + C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_n + ")");//0-1-4
            }
            if (C_variables.SUPPORTS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("SUPPORTS (" + C_variables.SUPPORTS_n + ")");//0-1-5
            }
            if (C_variables.INTERNAL_POINT_LINKS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("INTERNAL_POINT_LINKS (" + C_variables.INTERNAL_POINT_LINKS_n + ")");//0-1-6
            }
            if (C_variables.LINEAR_ISOTROPIC_MATERIALS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("LINEAR_ISOTROPIC_MATERIALS (" + C_variables.LINEAR_ISOTROPIC_MATERIALS_n + ")");//0-1-7
            }
            if (C_variables.LINEAR_ORTHOTROPIC_MATERIALS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("LINEAR_ORTHOTROPIC_MATERIALS (" + C_variables.LINEAR_ORTHOTROPIC_MATERIALS_n + ")");//0-1-8
            }
            if (C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("LINEAR_INTERFACE_POINT_PROPERTIES (" + C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_n + ")");//0-1-9
            }
            if (C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("LINEAR_INTERFACE_LINE_PROPERTIES (" + C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_n + ")");//0-1-10
            }
            if (C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("LINEAR_INTERFACE_SURFACE_PROPERTIES (" + C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_n + ")");//0-1-11
            }
            if (C_variables.NLMM101_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM101 (" + C_variables.NLMM101_n + ")");//0-1-12
            }
            if (C_variables.NLMM104_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM104 (" + C_variables.NLMM104_n + ")");//0-1-12
            }
            if (C_variables.NLMM111_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM111 (" + C_variables.NLMM111_n + ")");//0-1-12
            }
            if (C_variables.NLMM141_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM141 (" + C_variables.NLMM141_n + ")");//0-1-12
            }
            if (C_variables.NLMM151_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM151 (" + C_variables.NLMM151_n + ")");//0-1-12
            }
            if (C_variables.NLMM201_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM201 (" + C_variables.NLMM201_n + ")");//0-1-12
            if (C_variables.NLMM202_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM202 (" + C_variables.NLMM202_n + ")");//0-1-12
            if (C_variables.NLMM210_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM210 (" + C_variables.NLMM210_n + ")");//0-1-12
            if (C_variables.NLMM220_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM220 (" + C_variables.NLMM220_n + ")");//0-1-12
            if (C_variables.NLMM298_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM298 (" + C_variables.NLMM298_n + ")");//0-1-12
            if (C_variables.NLMM299_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM299 (" + C_variables.NLMM299_n + ")");//0-1-12
            if (C_variables.NLMM123_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM123 (" + C_variables.NLMM123_n + ")");//0-1-12
            if (C_variables.NLMM121_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM121 (" + C_variables.NLMM121_n + ")");//0-1-12
            if (C_variables.NLMM124_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM124 (" + C_variables.NLMM124_n + ")");//0-1-12
            if (C_variables.NLMM125_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM125 (" + C_variables.NLMM125_n + ")");//0-1-12
            if (C_variables.NLMM142_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM142 (" + C_variables.NLMM142_n + ")");//0-1-12
            if (C_variables.NLMM145_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM145 (" + C_variables.NLMM145_n + ")");//0-1-12
            if (C_variables.NLMM211_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM211 (" + C_variables.NLMM211_n + ")");//0-1-12
            if (C_variables.NLMM102_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM102 (" + C_variables.NLMM102_n + ")");//0-1-12
            if (C_variables.NLMM122_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM122 (" + C_variables.NLMM122_n + ")");//0-1-12
            if (C_variables.NLMM401_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM401 (" + C_variables.NLMM401_n + ")");//0-1-12
            if (C_variables.NLMM451_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM451 (" + C_variables.NLMM451_n + ")");//0-1-12
            if (C_variables.NLMM301_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM301 (" + C_variables.NLMM301_n + ")");//0-1-12
            if (C_variables.NLMM302_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM302 (" + C_variables.NLMM302_n + ")");//0-1-12
            if (C_variables.NLMM303_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM303 (" + C_variables.NLMM303_n + ")");//0-1-12
            if (C_variables.NLMM305_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM305 (" + C_variables.NLMM305_n + ")");//0-1-12
            if (C_variables.NLMM306_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM306 (" + C_variables.NLMM306_n + ")");//0-1-12
            if (C_variables.NLMM320_n > 0)
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NLMM320 (" + C_variables.NLMM320_n + ")");//0-1-12

            if (C_variables.CROSS_SECTION_AREAS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("CROSS_SECTION_AREAS (" + C_variables.CROSS_SECTION_AREAS_n + ")");//0-1-13
            }
            if (C_variables.FRAME_2D_CROSS_SECTIONS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("FRAME_2D_CROSS_SECTIONS (" + C_variables.FRAME_2D_CROSS_SECTIONS_n + ")");//0-1-14
            }
            if (C_variables.FRAME_3D_CROSS_SECTIONS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("FRAME_3D_CROSS_SECTIONS (" + C_variables.FRAME_3D_CROSS_SECTIONS_n + ")");//0-1-15
            }
            if (C_variables.LAMINATE_THICKNESSES_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("LAMINATE_THICKNESSES (" + C_variables.LAMINATE_THICKNESSES_n + ")");//0-1-16
            }
            if (C_variables.GEOMETRY_PATTERNS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("GEOMETRY_PATTERNS (" + C_variables.GEOMETRY_PATTERNS_n + ")");//0-1-17
            }
            if (C_variables.FIBROUS_PATTERNS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("FIBROUS_PATTERNS (" + C_variables.FIBROUS_PATTERNS_n + ")");//0-1-18
            }
            if (C_variables.LAYER_PROPERTIES_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("LAYER_PROPERTIES (" + C_variables.LAYER_PROPERTIES_n + ")");//0-1-19
            }
            if (C_variables.LAYER_PATTERNS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("LAYER_PATTERNS (" + C_variables.LAYER_PATTERNS_n + ")");//0-1-20
            }
            if (C_variables.NUMERICAL_INTEGRATION_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("NUMERICAL_INTEGRATION (" + C_variables.NUMERICAL_INTEGRATION_n + ")");//0-1-21
            }
            if (C_variables.ELEMENT_NODES_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("ELEMENT_NODES (" + C_variables.ELEMENT_NODES_n + ")");//0-1-22
            }
            if (C_variables.ELEMENT_PROPERTIES_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("ELEMENT_PROPERTIES (" + C_variables.ELEMENT_PROPERTIES_n + ")");//0-1-23
            }
            if (C_variables.LINEAR_SPRING_MATERIALS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("LINEAR_SPRING_MATERIALS (" + C_variables.LINEAR_SPRING_MATERIALS_n + ")");//0-1-24
            }
            if (C_variables.POINT_SPRINGS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("POINT_SPRINGS (" + C_variables.POINT_SPRINGS_n + ")");//0-1-25
            }
            if (C_variables.LINE_SPRINGS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("LINE_SPRINGS (" + C_variables.LINE_SPRINGS_n + ")");//0-1-26
            }
            if (C_variables.SURFACE_SPRINGS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("SURFACE_SPRINGS (" + C_variables.SURFACE_SPRINGS_n + ")");//0-1-27
            }
            if (C_variables.STIFFENERS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[1].Nodes.Add("STIFFENERS (" + C_variables.STIFFENERS_n + ")");//0-1-28
            }

            tree_femix.Nodes[0].Nodes.Add("LOAD");//0-2
            tree_femix.Nodes[0].Nodes[2].Nodes.Add("LOAD_CASE");//0-2-0
            if (C_variables.LOAD_CASE_PARAMETERS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("LOAD_CASE_PARAMETERS (" + C_variables.LOAD_CASE_PARAMETERS_n + ")");//0-2-0-0
            }
            if (C_variables.GRAVITY_LOAD_n > 0)
            {
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("GRAVITY_LOAD (" + C_variables.GRAVITY_LOAD_n + ")");//0-2-0-1
            }
            if (C_variables.POINT_LOADS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("POINT_LOADS (" + C_variables.POINT_LOADS_n + ")");//0-2-0-2
            }
            if (C_variables.INTERNAL_POINT_LOADS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("INTERNAL_POINT_LOADS (" + C_variables.INTERNAL_POINT_LOADS_n + ")");//0-2-0-3
            }
            if (C_variables.EDGE_LOADS_n > 0)
            {
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("EDGE_LOADS (" + C_variables.EDGE_LOADS_n + ")");//0-2-0-4
            }
            if (C_variables.INTERNAL_EDGE_LOADS_n > 0)
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("INTERNAL_EDGE_LOADS (" + C_variables.INTERNAL_EDGE_LOADS_n + ")");//0-2-0-5

            if (C_variables.FACE_LOADS_n > 0)
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("FACE_LOADS (" + C_variables.FACE_LOADS_n + ")");//0-2-0-6

            if (C_variables.INTERNAL_FACE_LOADS_n > 0)
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("INTERNAL_FACE_LOADS (" + C_variables.INTERNAL_FACE_LOADS_n + ")");//0-2-0-7

            if (C_variables.TEMPERATURE_VARIATION_n > 0)
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("TEMPERATURE_VARIATION (" + C_variables.TEMPERATURE_VARIATION_n + ")");//0-2-0-8

            if (C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_n > 0)
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("DIFFERENTIAL_TEMPERATURE_VARIATION (" + C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_n + ")");//0-2-0-9

            if (C_variables.PRESCRIBED_DISPLACEMENTS_n > 0)
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("PRESCRIBED_DISPLACEMENTS (" + C_variables.PRESCRIBED_DISPLACEMENTS_n + ")");//0-2-0-10

            if (C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_n > 0)
                tree_femix.Nodes[0].Nodes[2].Nodes[0].Nodes.Add("INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS (" + C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_n + ")");//0-2-0-11


            if (C_variables.LOAD_CASE_COMBINATIONS_n > 0)
                tree_femix.Nodes[0].Nodes.Add("LOAD_CASE_COMBINATIONS (" + C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_n + ")");//0-3

            //tree_femix.Nodes[0].Nodes[3].Nodes.Add("COMBINATION");//0-3-0
            //tree_femix.Nodes[0].Nodes[3].Nodes[0].Nodes.Add("COMBINATION_PARAMETERS (0)");//0-3-0-0
            //tree_femix.Nodes[0].Nodes[3].Nodes[0].Nodes.Add("LOAD_CASE_FACTORS (0)");//0-3-0-1

            tree_femix.EndUpdate();

            //tree_femix.Nodes.Clear();
            //tree_femix.Nodes[0].Text = "cc";
            //tree_femix.Nodes[0].Nodes[1].Remove();
            tree_femix.ExpandAll();
            tree_femix.Nodes[0].EnsureVisible();
            //tree_femix.CollapseAll();
        }

        private void combo_sections_fill()
        {
            //Font font = combo_sections.Font;
            //font = new Font(font, FontStyle.Bold);

            combo_sections.Items.Clear();
            combo_sections.Items.Add("      <FEMIX_DATA_FILE>");
            combo_sections.Items.Add("MAIN_PARAMETERS");
            combo_sections.Items.Add("      <MESH>");
            combo_sections.Items.Add("GROUP_NAMES");
            combo_sections.Items.Add("POINT_COORDINATES");
            combo_sections.Items.Add("AUXILIARY_VECTORS");
            combo_sections.Items.Add("COORDINATE_SYSTEMS");
            combo_sections.Items.Add("POINTS_WITH_SPECIAL_COORDINATE_SYSTEM");
            combo_sections.Items.Add("SUPPORTS");
            combo_sections.Items.Add("INTERNAL_POINT_LINKS");
            combo_sections.Items.Add("LINEAR_ISOTROPIC_MATERIALS");
            combo_sections.Items.Add("LINEAR_ORTHOTROPIC_MATERIALS");
            combo_sections.Items.Add("LINEAR_INTERFACE_POINT_PROPERTIES");
            combo_sections.Items.Add("LINEAR_INTERFACE_LINE_PROPERTIES");
            combo_sections.Items.Add("LINEAR_INTERFACE_SURFACE_PROPERTIES");
            combo_sections.Items.Add("NLMM101");
            combo_sections.Items.Add("NLMM104");
            combo_sections.Items.Add("NLMM111");
            combo_sections.Items.Add("NLMM141");
            combo_sections.Items.Add("NLMM151");
            combo_sections.Items.Add("NLMM201");
            combo_sections.Items.Add("NLMM202");
            combo_sections.Items.Add("NLMM210");
            combo_sections.Items.Add("NLMM220");
            combo_sections.Items.Add("NLMM298");
            combo_sections.Items.Add("NLMM299");
            combo_sections.Items.Add("NLMM123");
            combo_sections.Items.Add("NLMM121");
            combo_sections.Items.Add("NLMM124");
            combo_sections.Items.Add("NLMM125");
            combo_sections.Items.Add("NLMM142");
            combo_sections.Items.Add("NLMM145");
            combo_sections.Items.Add("NLMM211");
            combo_sections.Items.Add("NLMM102");
            combo_sections.Items.Add("NLMM122");
            combo_sections.Items.Add("NLMM401");
            combo_sections.Items.Add("NLMM451");
            combo_sections.Items.Add("NLMM301");
            combo_sections.Items.Add("NLMM302");
            combo_sections.Items.Add("NLMM303");
            combo_sections.Items.Add("NLMM303");
            combo_sections.Items.Add("NLMM305");
            combo_sections.Items.Add("NLMM306");
            combo_sections.Items.Add("NLMM320");

            combo_sections.Items.Add("CROSS_SECTION_AREAS");
            combo_sections.Items.Add("FRAME_2D_CROSS_SECTIONS");
            combo_sections.Items.Add("FRAME_3D_CROSS_SECTIONS");
            combo_sections.Items.Add("LAMINATE_THICKNESSES");
            combo_sections.Items.Add("GEOMETRY_PATTERNS");
            combo_sections.Items.Add("FIBROUS_PATTERNS");
            combo_sections.Items.Add("LAYER_PROPERTIES");
            combo_sections.Items.Add("LAYER_PATTERNS");
            combo_sections.Items.Add("NUMERICAL_INTEGRATION");
            combo_sections.Items.Add("ELEMENT_NODES");
            combo_sections.Items.Add("ELEMENT_PROPERTIES");
            combo_sections.Items.Add("LINEAR_SPRING_MATERIALS");
            combo_sections.Items.Add("POINT_SPRINGS");
            combo_sections.Items.Add("LINE_SPRINGS");
            combo_sections.Items.Add("SURFACE_SPRINGS");
            combo_sections.Items.Add("STIFFENERS");

            combo_sections.Items.Add("      <LOAD>");
            combo_sections.Items.Add("      <LOAD_CASE>");
            combo_sections.Items.Add("LOAD_CASE_PARAMETERS");
            combo_sections.Items.Add("GRAVITY_LOAD");
            combo_sections.Items.Add("POINT_LOADS");
            combo_sections.Items.Add("INTERNAL_POINT_LOADS");
            combo_sections.Items.Add("EDGE_LOADS");
            combo_sections.Items.Add("INTERNAL_EDGE_LOADS");
            combo_sections.Items.Add("FACE_LOADS");
            combo_sections.Items.Add("INTERNAL_FACE_LOADS");
            combo_sections.Items.Add("TEMPERATURE_VARIATION");
            combo_sections.Items.Add("DIFFERENTIAL_TEMPERATURE_VARIATION");
            combo_sections.Items.Add("PRESCRIBED_DISPLACEMENTS");
            combo_sections.Items.Add("INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS");

            combo_sections.Items.Add("LOAD_CASE_COMBINATIONS");
            //combo_sections.Items.Add("      <COMBINATION>");
            //combo_sections.Items.Add("COMBINATION_PARAMETERS");
            //combo_sections.Items.Add("LOAD_CASE_FACTORS");

            combo_sections.SelectedIndex = 0;
        }


#region femix_text_editor
        //Create style for highlighting
        //TextStyle brownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);


        ///light color
        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);

        TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);

        //static SolidBrush yellow2 = new SolidBrush(Color.FromArgb(255, 255, 255, 128));
        TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);

        TextStyle errorred = new TextStyle(Brushes.White, Brushes.OrangeRed, FontStyle.Italic);

        TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Bold);
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        ///// Dark
        //TextStyle BlueStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 0, 128, 192)) , null, FontStyle.Regular);
        //TextStyle BoldStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 255, 183, 183)), null, FontStyle.Bold | FontStyle.Underline);
        //TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);

        //TextStyle MagentaStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 255, 255, 128)), null, FontStyle.Regular);

        //TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        //TextStyle BrownStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 255, 128, 64)), null, FontStyle.Italic);
        //TextStyle MaroonStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 255, 183, 183)), null, FontStyle.Regular);
        //MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));


        FastColoredTextBoxNS.AutocompleteMenu popupMenu2;
        string[] keywords = {"COUNT","MAIN_TITLE","ANALYSIS_TYPE","MATERIALLY_NONLINEAR_PROBLEM","GEOMETRICALLY_NONLINEAR_PROBLEM","NUMBER_OF_PHASES","NUMBER_OF_LOAD_CASES","NUMBER_OF_COMBINATIONS","STIFFNESS_MATRIX_STORAGE_TECHNIQUE","SYSTEM_LIN_EQ_ALGORITHM","LOAD_CASE_NUMBER","INTEGRATION_TYPE","LOAD_CASE_TITLE","LOAD_CASE_GROUP","INTEGRATION_NAME",
       "_D1","_D2","_D3","_R1","_R2","_R3","_Y","_N","_NONE","PRECONDITIONED_RESIDUAL_DECAY",
            "_STATIC","_SYMMETRIC_NONCONSTANT_SEMIBAND","_GAUSS_ELIM","ITERATIVE_ALGORITHM","_NEWTON_RAPHSON_STD","CONVERGENCE_CRITERION","FORCE_NORM","TOLERANCE_IN_EACH_COMBINATION","MAXIMUM_NUMBER_OF_ITERATIONS_IN_EACH_COMBINATION","NUMBER_OF_COMBINATIONS_BEFORE_RESTART","ADD_THE_UNBALANCED_FORCES_IN_EACH_COMBINATION","ARC_LENGTH","LINE_SEARCH","PATH_BEHAVIOR","INDEPENDENT"};
        string[] snippets = {"COUNT = ^ ;" , "_Y ;^", "_N ;^" ,
        "<FEMIX_DATA_FILE_V4.0>\n^\n</FEMIX_DATA_FILE_V4.0>\n","<MAIN_PARAMETERS>\n^\n</MAIN_PARAMETERS>\n","<MESH>\n^\n</MESH>\n","<GROUP_NAMES>\n^\n</GROUP_NAMES>\n","<POINT_COORDINATES>\n^\n</POINT_COORDINATES>\n","<AUXILIARY_VECTORS>\n^\n</AUXILIARY_VECTORS>\n","<COORDINATE_SYSTEMS>\n^\n</COORDINATE_SYSTEMS>\n","<POINTS_WITH_SPECIAL_COORDINATE_SYSTEM>\n^\n</POINTS_WITH_SPECIAL_COORDINATE_SYSTEM>\n","<SUPPORTS>\n^\n</SUPPORTS>\n","<INTERNAL_POINT_LINKS>\n^\n</INTERNAL_POINT_LINKS>\n","<LINEAR_ISOTROPIC_MATERIALS>\n^\n</LINEAR_ISOTROPIC_MATERIALS>\n","<LINEAR_ORTHOTROPIC_MATERIALS>\n^\n</LINEAR_ORTHOTROPIC_MATERIALS>\n","<LINEAR_INTERFACE_POINT_PROPERTIES>\n^\n</LINEAR_INTERFACE_POINT_PROPERTIES>\n","<LINEAR_INTERFACE_LINE_PROPERTIES>\n^\n</LINEAR_INTERFACE_LINE_PROPERTIES>\n","<LINEAR_INTERFACE_SURFACE_PROPERTIES>\n^\n</LINEAR_INTERFACE_SURFACE_PROPERTIES>\n","<NLMM101>\n^\n</NLMM101>\n","<NLMM104>\n^\n</NLMM104>\n","<NLMM111>\n^\n</NLMM111>\n","<NLMM141>\n^\n</NLMM141>\n","<NLMM151>\n^\n</NLMM151>\n","<NLMM201>\n^\n</NLMM201>\n","<NLMM202>\n^\n</NLMM202>\n","<NLMM210>\n^\n</NLMM210>\n","<NLMM220>\n^\n</NLMM220>\n","<NLMM298>\n^\n</NLMM298>\n","<NLMM299>\n^\n</NLMM299>\n","<NLMM123>\n^\n</NLMM123>\n","<NLMM121>\n^\n</NLMM121>\n","<NLMM124>\n^\n</NLMM124>\n","<NLMM125>\n^\n</NLMM125>\n","<NLMM142>\n^\n</NLMM142>\n","<NLMM145>\n^\n</NLMM145>\n","<NLMM211>\n^\n</NLMM211>\n","<NLMM102>\n^\n</NLMM102>\n","<NLMM122>\n^\n</NLMM122>\n","<NLMM401>\n^\n</NLMM401>\n","<NLMM451>\n^\n</NLMM451>\n","<NLMM301>\n^\n</NLMM301>\n","<NLMM302>\n^\n</NLMM302>\n","<NLMM303>\n^\n</NLMM303>\n","<NLMM305>\n^\n</NLMM305>\n","<NLMM306>\n^\n</NLMM306>\n","<NLMM320>\n^\n</NLMM320>\n","<CROSS_SECTION_AREAS>\n^\n</CROSS_SECTION_AREAS>\n","<FRAME_2D_CROSS_SECTIONS>\n^\n</FRAME_2D_CROSS_SECTIONS>\n","<FRAME_3D_CROSS_SECTIONS>\n^\n</FRAME_3D_CROSS_SECTIONS>\n","<LAMINATE_THICKNESSES>\n^\n</LAMINATE_THICKNESSES>\n","<GEOMETRY_PATTERNS>\n^\n</GEOMETRY_PATTERNS>\n","<FIBROUS_PATTERNS>\n^\n</FIBROUS_PATTERNS>\n","<LAYER_PROPERTIES>\n^\n</LAYER_PROPERTIES>\n","<LAYER_PATTERNS>\n^\n</LAYER_PATTERNS>\n","<NUMERICAL_INTEGRATION>\n^\n</NUMERICAL_INTEGRATION>\n","<ELEMENT_NODES>\n^\n</ELEMENT_NODES>\n","<ELEMENT_PROPERTIES>\n^\n</ELEMENT_PROPERTIES>\n","<LINEAR_SPRING_MATERIALS>\n^\n</LINEAR_SPRING_MATERIALS>\n","<POINT_SPRINGS>\n^\n</POINT_SPRINGS>\n","<LINE_SPRINGS>\n^\n</LINE_SPRINGS>\n","<SURFACE_SPRINGS>\n^\n</SURFACE_SPRINGS>\n","<STIFFENERS>\n^\n</STIFFENERS>\n","<LOAD>\n^\n</LOAD>\n","<LOAD_CASE>\n^\n</LOAD_CASE>\n","<LOAD_CASE_PARAMETERS>\n^\n</LOAD_CASE_PARAMETERS>\n","<GRAVITY_LOAD>\n^\n</GRAVITY_LOAD>\n","<POINT_LOADS>\n^\n</POINT_LOADS>\n","<INTERNAL_POINT_LOADS>\n^\n</INTERNAL_POINT_LOADS>\n","<EDGE_LOADS>\n^\n</EDGE_LOADS>\n","<INTERNAL_EDGE_LOADS>\n^\n</INTERNAL_EDGE_LOADS>\n","<FACE_LOADS>\n^\n</FACE_LOADS>\n","<INTERNAL_FACE_LOADS>\n^\n</INTERNAL_FACE_LOADS>\n","<TEMPERATURE_VARIATION>\n^\n</TEMPERATURE_VARIATION>\n","<DIFFERENTIAL_TEMPERATURE_VARIATION>\n^\n</DIFFERENTIAL_TEMPERATURE_VARIATION>\n","<PRESCRIBED_DISPLACEMENTS>\n^\n</PRESCRIBED_DISPLACEMENTS>\n","<INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS>\n^\n</INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS>\n","<LOAD_CASE_COMBINATIONS>\n^\n</LOAD_CASE_COMBINATIONS>\n","<COMBINATION>\n^\n</COMBINATION>\n","<COMBINATION_PARAMETERS>\n^\n</COMBINATION_PARAMETERS>\n","<LOAD_CASE_FACTORS>\n^\n</LOAD_CASE_FACTORS>\n",
        "<GROUP_NAMES>\n## Declaration of the names of the groups\n# N. of groups\n\n\n## Content of each column:\n#  A -> Counter\n#  B -> Name\n#			A		B\n^\n</GROUP_NAMES>\n","<POINT_COORDINATES>\n## Point coordinates (global coordinate system) \n# N. of points \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Coordinate - XG1 \n#  C -> Coordinate - XG2 \n#  D -> Coordinate - XG3 \n#			A		B		C		D\n^\n</POINT_COORDINATES>\n","<AUXILIARY_VECTORS>\n## Auxiliary vectors (global coordinate system) \n## All vectors are normalized immediately after input \n# N. of auxiliary vectors \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the auxiliary vector \n#  C -> Component of the auxiliary vector - 1 \n#  D -> Component of the auxiliary vector - 2 \n#  E -> Component of the auxiliary vector - 3 \n#			A	B		C		D      E\n^\n</AUXILIARY_VECTORS>\n","<COORDINATE_SYSTEMS>\n## Coordinate systems defined with 3 vectors \n# N. of coordinate systems \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Coordinate system name \n#  C -> Name of the auxiliary vector - 1 \n#  D -> Name of the auxiliary vector - 2 \n#  E -> Name of the auxiliary vector - 3 \n#			A	B		C		D		E\n^\n</COORDINATE_SYSTEMS>\n","<POINTS_WITH_SPECIAL_COORDINATE_SYSTEM>\n## Points with special coordinate system \n# N. of points with special coordinate system \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number (or point number range) \n#  E -> Name of the coordinate system \n#			A	B				C			D   E\n^\n</POINTS_WITH_SPECIAL_COORDINATE_SYSTEM>\n","<SUPPORTS>\n## Points with fixed degrees of freedom \n# N. of points with fixed degrees of freedom \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number (or point number range) \n#  E -> N. of fixed degrees of freedom in the current point(s) \n#  F -> Fixed degrees of freedom: \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#		A	B			C		D	E		F\n^\n</SUPPORTS>\n","<INTERNAL_POINT_LINKS>\n## Points with linked degrees of freedom \n# N. of pairs of points with linked degrees of freedom \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number - M (Master) (or point number range) \n#  E -> Point number - S (Slave) (or point number range) \n#  F -> N. of linked degrees of freedom in the current pair of points \n#  G -> Linked degrees of freedom: \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#		A	B			C		D		E	F		G\n^\n</INTERNAL_POINT_LINKS>\n","<LINEAR_ISOTROPIC_MATERIALS>\n## Keyword: _LIN_ISO \n## Properties of the linear isotropic materials \n# N. of linear isotropic materials \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Material name \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Young's modulus \n#  F -> Poisson's coefficient \n#		A	B		C		D		E		F\n^\n</LINEAR_ISOTROPIC_MATERIALS>\n","<LINEAR_ORTHOTROPIC_MATERIALS>\n## Keyword: _LIN_ORTHO \n## Properties of the linear orthotropic materials \n# N. of linear orthotropic materials \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Material name \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient - l1 \n#  E -> Temperature coefficient - l2 \n#  F -> Temperature coefficient - l3 \n#  G -> Young's modulus - l1 \n#  H -> Young's modulus - l2 \n#  I -> Young's modulus - l3 \n#  J -> Shear modulus - l2/l3 \n#  K -> Shear modulus - l3/l1 \n#  L -> Shear modulus - l1/l2 \n#  M -> Poisson's coefficient - l2/l3 \n#  N -> Poisson's coefficient - l3/l1 \n#  O -> Poisson's coefficient - l1/l2 \n#  P -> Name of the coordinate system \n#	A	B		C		D		E		F	G	H	I	J	K	L		M		N		O	P\n^\n</LINEAR_ORTHOTROPIC_MATERIALS>\n","<LINEAR_INTERFACE_POINT_PROPERTIES>\n## Keyword: _LIN_INT_POINT \n## Properties of linear interface elements that link a pair of points \n# N. of types of point interface elements \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the type of point interface element \n#  C -> Degree of freedom with linking spring \n#  D -> Stiffness of the spring \n#		A	B			C			D\n^\n</LINEAR_INTERFACE_POINT_PROPERTIES>\n","<LINEAR_INTERFACE_LINE_PROPERTIES>\n## Keyword: _LIN_INT_LINE \n## Properties of linear interface elements that link a pair of edges (2D only) \n# N. of types of line interface elements \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the type of line interface element \n#  C -> Normal stiffness \n#  D -> Tangential stiffness \n#		A  B				C				D\n^\n</LINEAR_INTERFACE_LINE_PROPERTIES>\n","<LINEAR_INTERFACE_SURFACE_PROPERTIES>\n## Keyword: _LIN_INT_SURF \n## Properties of linear interface elements that link a pair of faces (solid/shell only) \n# N. of types of surface interface elements \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the type of surface interface element \n#  C -> Normal stiffness \n#  D -> Tangential stiffness - s1 \n#  E -> Tangential stiffness - s2 \n#		A	B				C				D				E\n^\n</LINEAR_INTERFACE_SURFACE_PROPERTIES>\n",
        "<NLMM101>\n## Keyword: _NLMM101 \n## Properties defining the material constitutive law: \n##    CEB-FIP 1993 law for compression behavior \n##    Linear-parabola diagram for tensile up-to-peak behaviour \n##    Trilinear diagram for tensile post-peak behavior \n##    Kinematic hardening rule; associated flow rule; strain hardening; Owen yield criteria \n##    Multi-fixed smeared crack model \n \n# N. of materials modeled by the NLMM101 law \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the constitutive law \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n \n#  G -> Compression peak stress \n#  H -> Strain at compression peak stress \n#  I -> Parameter for defining the compressive strength and rigidity \n#       degradation according to Vecchio and Collins law (0=no degradation) \n#  J -> Unloading and reloading law in compression (_ELASTIC, _SECANT) \n \n#  K -> Tensile peak stress \n#  L -> Strain at tensile peak stress \n#  M -> Up limit tensile stress of the linear branch \n#  N -> Unloading and reloading law for the up-to-peak tensile stress (_ELASTIC, _SECANT) \n \n#  O -> Stress at the first post-peak point of the tensile-softening diagram (fracture mode I) \n#  P -> Crack strain at the first post-peak point of the tensile-softening diagram (fracture mode I) \n#  Q -> Stress at the second post-peak point of the tensile-softening diagram (fracture mode I) \n#  R -> Crack strain at the second post-peak point of the tensile-softening diagram (fracture mode I) \n#  S -> Mode I fracture energy (Gf) \n#  T -> Unloading and reloading law for the tensile post-peak strain softening diagram \n#       (_ELASTIC, _SECANT) (fracture mode I) \n \n#  U -> Parameter to define the shear retention factor for the fracture mode II \n#  V -> Crack band width for the fracture mode I (_SQRT_ELE; _SQRT_GP; _CONST_VALUE) \n#  X -> Criteria for opening a new crack [1: principal tensile stress greater than uniaxial tensile strength (_SI_GT_FCT); \n#                                         2: angle between the principal tensile stress and the existing cracks \n#                                            exceed the value of a threshold angle (_ANG_GT_THRANG); \n#                                         3: 1 AND 2 (_SIGTFCT_AND_ANGGTTHRANG); \n#                                         4: 1 OR 2 (_SIGTFCT_OR_ANGGTTHRANG); \n#  Y -> Threshold angle (zero for the first criteria of the new crack opening); \n#  W -> Parameter to define the mode I fracture energy available for the new crack (zero for the initial Gf) \n#		A	B		C		D		E		F		G		H		I		J		K		L		M		N		O	P		Q	R		S	T		U	V		X			Y		W	\n^\n</NLMM101>\n",
        "<NLMM104> \n \n## Keyword: _NLMM104 \n## Properties of the NLMM104 material model \n# N. of NLMM104 materials \n   COUNT = ^ ; \n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Tensile strength \n#  I -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN \n#  J -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  K -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  L -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  M -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  N -> Ratio between the strain at the third post-peak point and the \n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I) \n#  O -> Ratio between the stress at the third post-peak point and the \n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I) \n#  P -> Mode I fracture energy (Gf) \n#  Q -> Type of mode I fracture energy update law \n#       - Available keywords: _NONE, _LINEAR, _QUADRATIC or _CUBIC \n#  R -> Ultimate crack shear sliding (keyword or constant value) \n#       - Available keywords: _MAXIMUM_CRACKWIDTH \n#  S -> Parameter to define the mode I fracture energy available to the new crack \n#  T -> Type of shear-softening diagram \n#       - Available keywords: _NONE or _LINEAR \n#  U -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n#  V -> Type of crack shear modelling appoach (used in shear retention model) \n#       - Available keywords: _INCREMENTAL or _TOTAL \n#  W -> Crack shear strength \n#  X -> Shear fracture energy (Gfs=GfII) (in nonlinear Mindlin shell: In-plane shear fracture energy) \n#  Y -> Type of unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n#  Z -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n# AA -> Maximum n. of cracks \n# AB -> Threshold angle (degress) \n# \n# Out-of-plane shear data: \n#    -> Used in nonlinear Mindlin shell \n# \n# AC -> Type of out-of-plane shear-softening diagram \n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN \n# AD -> Minimum out-of-plane shear stress for softening behavior \n# AE -> Out-of-plane shear fracture energy \n# AF -> Ratio between the out-of-plane shear strain at the first \n#       post-peak point and the ultimate out-of-plane shear strain \n#       trilinear out-of-plane shear-softening diagram \n# AG -> Ratio between the out-of-plane shear stress at the first \n#       post-peak point and the out-of-plane shear strength of the \n#       trilinear out-of-plane shear-softening diagram \n# AH -> Ratio between the out-of-plane shear strain at the second \n#       post-peak point and the ultimate out-of-plane shear strain \n#       of the trilinear out-of-plane shear-softening diagram \n# AI -> Ratio between the out-of-plane shear stress at the second \n#       post-peak point and the out-of-plane shear strength of the \n#       trilinear out-of-plane shear-softening diagram \n \n \n#     A     B     C     D     E     F     G		 H     I     J     K     L     M     N     O	    P     Q     R     S     T     U		   Y     Z     AA     AB     AC     AD     AE	   V     W     X	   AF     AG     AH     AI \n \n \n</NLMM104>",
        "<NLMM111> \n \n## Keyword: _NLMM111 \n## Properties of the NLMM111 material model \n# N. of NLMM111 materials \n   COUNT = ^ ; \n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Tensile strength \n#  I -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN \n#  J -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  K -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  L -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  M -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  N -> Ratio between the strain at the third post-peak point and the \n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I) \n#  O -> Ratio between the stress at the third post-peak point and the \n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I) \n#  P -> Mode I fracture energy (Gf) \n#  Q -> Parameter to define the mode I fracture energy available to the new crack \n#  R -> Type of shear-softening diagram \n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN \n#  S -> Ratio between the (shear strain minus peak shear strain) at the first post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n#  T -> Ratio between the the shear stress at the first post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n#  U -> Ratio between the (shear strain minus peak shear strain) at the second post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n#  V -> Ratio between the the shear stress at the second post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n#  W -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n#  X -> Type of crack shear modelling appoach (used in shear retention model) \n#       - Available keywords: _INCREMENTAL or _TOTAL \n#  Y -> Crack shear strength \n#  Z -> Shear fracture energy (Gfs=GfII=GfIII) \n# AA -> Type of unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n# AB -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n# AC -> Maximum n. of cracks \n# AD -> Threshold angle (degress) \n \n#     A     B     C     D     E     F     G		   H     I     J     K     L     M     N     O	   P     Q     R     S     T     U     V	   W     X     Y     Z	  AA     AB     AC     AD \n \n \n</NLMM111> ",
        "<NLMM141> \n \n## Keyword: _NLMM141 \n## Properties of the NLMM141 material model \n# N. of NLMM141 materials \n   COUNT = ^ ; \n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Strain at compression peak stress \n#  I -> Parameter defining the initial yield surface \n#  J -> Compression/stiffness degradation parameter due to concrete cracking \n#  K -> Tensile strength \n#  L -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR or _CORNELISSEN \n#  M -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear tensile-softening diagram (fracture mode I) \n#  N -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear tensile-softening diagram (fracture mode I) \n#  O -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear tensile-softening diagram (fracture mode I) \n#  P -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear tensile-softening diagram (fracture mode I) \n#  Q -> Mode I fracture energy (Gf) \n#  R -> Parameter to define the mode I fracture energy available to the new crack \n#  S -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n#  T -> Type of tensile unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n#  U -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n#  V -> Maximum n. of cracks \n#  X -> Threshold angle (degrees) \n \n#     A     B     C     D     E     F     G     H	  I     J     K     L     M     N     O     P	   Q     R     S     T     U     V     X \n \n \n</NLMM141> ",
        "<NLMM151> \n \n## Keyword: _NLMM151 \n## Properties of the NLMM151 material model \n   COUNT = ^ ; # N. of NLMM151 materials \n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Strain at compressive strength \n#  I -> Parameter defining the linear part of the compressive strength \n#  J -> Compressive fracture energy \n#  K -> Type of hardening function after peak load \n#       - Available keywords: _CEB_FIP93 or _PERFECT_PLASTIC \n#  L -> Volume fraction of fibers (Vf) \n# \n#       Note: for plane concrete a Value of zero should be set for this parameter \n# \n#  M -> Aspect ratio of fibers (lf/Df) \n#  N -> Length efficiency factor of fibers \n#  O -> Orientation factor of fibers \n#  P -> Ultimate bond strength \n#  Q -> Tensile strength \n#  R -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN \n#  S -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  T -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  U -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  V -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  W -> Ratio between the strain at the third post-peak point and the \n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I) \n#  X -> Ratio between the stress at the third post-peak point and the \n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I) \n#  Y -> Mode I fracture energy (Gf) \n#  Z -> Parameter to define the mode I fracture energy available to the new crack \n# AA -> Type of shear-softening diagram \n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN \n# AB -> Ratio between the (shear strain minus peak shear strain) at the first post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n# AC -> Ratio between the the shear stress at the first post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n# AD -> Ratio between the (shear strain minus peak shear strain) at the second post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n# AE -> Ratio between the the shear stress at the second post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n# AF -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n# AG -> Type of crack shear modelling approach (used in shear retention model) \n#       - Available keywords: _INCREMENTAL or _TOTAL \n# AH -> Crack shear strength \n# AI -> Shear fracture energy (Gfs=GfII=GfIII) \n# AJ -> Type of unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n# AK -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n# AL -> Maximum n. of cracks \n# AM -> Threshold angle (degrees) \n \n#     A     B     C     D     E     F     G     H     I     J     K     L     M     N     O     P     Q     R     S     T     U     V     W     X     Y     Z     AA     AB     AC      AD     AE     AF     AG     AH     AI	     AJ     AK     AL     AM \n \n \n</NLMM151> ",
        "<NLMM201> \n \n## Properties of the NLMM201 material model \n## Keyword: _NLMM201 \n   COUNT = ^ ; # N. of NLMM201 materials \n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Strain at the end of the first branch \n#  F -> Stress at the end of the first branch \n#  G -> Strain at the end of the second branch \n#  H -> Stress at the end of the second branch \n#  I -> Strain at the end of the third branch \n#  J -> Stress at the end of the third branch \n#  K -> Third branch exponent \n \n#			A			B			C			D			E			F			G			H			I			J			K \n \n \n</NLMM201>",
        "<NLMM142>\n## Properties of the NLMM142 material model\n## Keyword: _NLMM142\n   COUNT = ^ ;  # N. of NLMM142 materials\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Mass per unit volume\n#  D -> Temperature coefficient\n#  E -> Poisson's coefficient\n#  F -> Young's modulus\n#  G -> Compressive strength\n#  H -> Strain at compression peak stress\n#  I -> Parameter defining the initial yield surface\n#  J -> Type of hardeding function after peak load\n#       - Available keywords: _CEB_FIP93 or _PERFECT_PLASTIC\n#  K -> Volume fraction of fibers (Vf)\n#\n#       Note: for plane concrete a Value of zero should be set for this parameter\n#\n#  L -> Aspect ratio of fibers (lf/Df)\n#  M -> Length efficiency factor of fibers\n#  N -> Orientation factor of fibers\n#  O -> Ultimate bond strength\n#  P -> Tensile strength\n#  Q -> Type of tensile-softening diagram\n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN\n#  R -> Ratio between the strain at the first post-peak point and the\n#      ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  S -> Ratio between the stress at the first post-peak point and the\n#     tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  T -> Ratio between the strain at the second post-peak point and the\n#      ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  U -> Ratio between the stress at the second post-peak point and the\n#     tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  V -> Ratio between the strain at the third post-peak point and the\n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I)\n#  W -> Ratio between the stress at the third post-peak point and the\n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I)\n#  X -> Mode I fracture energy (Gf)\n#  Y -> Type of mode I fracture energy update law\n#       - Available keywords: _NONE, _LINEAR, _QUADRATIC or _CUBIC\n#  Z -> Ultimate crack shear sliding (keyword or constant value)\n#       - Available keywords: _MAXIMUM_CRACKWIDTH\n#  AA -> Parameter to define the mode I fracture energy available to the new crack\n#  AB -> Type of shear-softening diagram\n#       - Available keywords: _NONE or _LINEAR\n#  AC -> Type of shear retention factor law (keyword or constant value)\n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC\n#  AD -> Type of crack shear modelling appoach (used in shear retention model)\n#       - Available keywords: _INCREMENTAL or _TOTAL\n#  AE -> Crack shear strength\n#  AF -> Shear fracture energy (Gfs=GfII) (in nonlinear Mindlin shell: In-plane shear fracture energy)\n#  AG -> Type of unloading/reloading diagram\n#       - Available keywords: _SECANT or _ELASTIC\n#  AH -> Crack band width for the fracture mode I (keyword or constant value)\n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP\n# AI -> Maximum n. of cracks\n# AJ -> Threshold angle (degress)\n#\n# Out-of-plane shear data:\n#    -> Used in nonlinear Mindlin shell\n#\n# AK -> Type of out-of-plane shear-softening diagram\n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN\n# AL -> Minimum out-of-plane shear stress for softening behavior\n# AM -> Out-of-plane shear fracture energy\n# AN -> Ratio between the out-of-plane shear strain at the first\n#       post-peak point and the ultimate out-of-plane shear strain\n#       trilinear out-of-plane shear-softening diagram\n# AO -> Ratio between the out-of-plane shear stress at the first\n#       post-peak point and the out-of-plane shear strength of the\n#       trilinear out-of-plane shear-softening diagram\n# AP -> Ratio between the out-of-plane shear strain at the second\n#       post-peak point and the ultimate out-of-plane shear strain\n#       of the trilinear out-of-plane shear-softening diagram\n# AQ -> Ratio between the out-of-plane shear stress at the second\n#       post-peak point and the out-of-plane shear strength of the\n#       trilinear out-of-plane shear-softening diagram\n\n\n<NLMM142>",
         "<NLMM202>\n## Properties of the NLMM202 material model\n## Keyword: _NLMM202\n# N. of NLMM202 materials\nCOUNT = ^ ; \n\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Mass per unit volume\n#  D -> Temperature coefficient\n#  E -> Elastic modulus\n#  F -> Tangent modulus at strain hardening\n#  G -> Yielding strain\n#  H -> Yielding stress\n#  I -> Hardening strain\n#  J -> Hardening stress\n#  K -> Strain at the ultimate stress\n#  L -> Ultimate stress\n#  A  B        C        D       E         F 		 G        H      I      J      K      L\n\n\n</NLMM202>","<NLMM210>\n## Properties of the NLMM210 material model\n## Keyword: _NLMM210\n# N. of NLMM210 materials\nCOUNT = ^ ; \n\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Mass per unit volume\n#  D -> Temperature coefficient\n#  E -> Concrete initial Young modulus\n#  F -> Concrete strain at peak confined stress\n#  G -> Confined concrete stress\n#  H -> Non-dimensional critical strain on the compression envelope\n#  I -> Strain at peak tension stress\n#  J -> Concrete tension strength\n#  K -> Critical strain on the tension envelope curve\n#  A  B       C        D        E        F 			G      H        I         J        K\n\n\n</NLMM210>",
         "<NLMM301>\n\n## Properties of the NLMM301 material model\n## Keyword: _NLMM30\n# N. of NLMM301 materials\n  COUNT = ^ ; \n\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Slip at the end of the linear bond stress-slip relationship\n#  D -> Slip at peak bond stress\n#  E -> Peak bond stress\n#  F -> Parameter used in the definition of the pre-peak bond stress-slip relationship\n#  G -> Parameter used in the definition of the post-peak bond stress-slip relationship\n#  H -> Normal stiffness\n#      A  B                                         C                 D                 E                 F                 G                 H\n\n\n</NLMM301>","<NLMM302>\n\n## Keyword: _NLMM302\n## Properties of the NLMM302 material model\n# N. of NLMM302 materials\n   COUNT = ^ ;\n\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Constitutive value defining fracture mode II\n#  D -> Initial constitutive value defining fracture mode I\n#  E -> Tensile strength\n#  F -> First post-peak point of the crack opening\n#  G -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the tensile-softening diagram\n#  H -> Second post-peak point of the crack opening\n#  I -> Ratio between the stress at the second post-peak point\n#       and the tensile strength of the tensile-softening diagram\n#  J -> Ultimate post-peak point of the crack opnening\n#      A  B                                         C                 D                 E                 F                 G                 H                 I                 J\n\n\n</NLMM302>","<NLMM305>\n\n## Properties of the NLMM305 material model\n## Keyword: _NLMM305\n# N. of NLMM305 materials\n   COUNT = ^ ;\n\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Slip at the end of the linear bond stress-slip relationship\n#  D -> Slip at peak bond stress\n#  E -> Material cohesion\n#  F -> Friction angle between materials in contact\n#  G -> Parameter used in the definition of the pre-peak bond stress-slip relationship\n#  H -> Parameter used in the definition of the post-peak bond stress-slip relationship\n#  I -> Normal stiffness\n#      A  B                 C                 D                 E                 F                 G                 H                 I\n\n\n</NLMM305>",
        "<CROSS_SECTION_AREAS>\n## Keyword: _CS_AREA \n## Cross section areas \n# N. of cross section areas \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the cross section \n#  C -> Area \n#		A	B		C\n^\n</CROSS_SECTION_AREAS>\n","<FRAME_2D_CROSS_SECTIONS>\n## Keyword: _CS_2D \n## Cross section properties of the 2D frame elements \n# N. of 2D frame cross sections \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the cross section \n#  C -> Area \n#  D -> Shear factor \n#  E -> Moment of inertia \n#  F -> Cross section height (used with differential temperature only) \n#		A	B			C			D			E			F\n^\n</FRAME_2D_CROSS_SECTIONS>\n","<FRAME_3D_CROSS_SECTIONS>\n## Keyword: _CS_3D \n## Cross section properties of the 3D frame elements \n# N. of 3D frame cross sections \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the cross section \n#  C -> Area \n#  D -> Shear factor (l2) \n#  E -> Shear factor (l3) \n#  F -> Torsional constant \n#  G -> Moment of inertia (l2) \n#  H -> Moment of inertia (l3) \n#  I -> Local coordinate of the shear center (l2) \n#  J -> Local coordinate of the shear center (l3) \n#  K -> Cross section height (l2) (used with differential temperature only) \n#  L -> Cross section height (l3) (used with differential temperature only) \n#  M -> Angle (degrees) \n#	A	B		C		D		E		F		G		H		I		J		K		L		M\n^\n</FRAME_3D_CROSS_SECTIONS>\n","<LAMINATE_THICKNESSES>\n## Keyword: _LAM_THICK \n## Thickness of the laminate elements \n# N. of nodal thicknesses \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the thickness \n#  C -> Thickness \n#		A		B		C\n^\n</LAMINATE_THICKNESSES>\n","<GEOMETRY_PATTERNS>\n## Keyword: _GEOM_PATT \n## Geometry patterns of several types of properties \n# N. of geometry patterns \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Pattern name \n#  C -> Type of geometry description \n#       - Available keywords: _CS_AREA, _CS_2D, _CS_3D or _LAM_THICK \n#  D -> N. of nodes of the pattern \n#  E -> Names of geometry descriptions \n#		A	B		C		D	E\n^\n</GEOMETRY_PATTERNS>\n","<FIBROUS_PATTERNS>\n## Keyword: _FIB_PATT \n## Lists of files describing fibrous cross sections \n# N. of lists of files describing fibrous cross sections \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Material/geometry name \n#  C -> N. of nodes of the fibrous element \n#  D -> File names defining the properties of the fibrous cross section \n# \n#  Note: when C is equal to 1 the cross section is constant \n# \n#		A	B		C	D\n^\n</FIBROUS_PATTERNS>\n","<LAYER_PROPERTIES>\n## Layer properties \n# N. of layer properties \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Layer name \n#  C -> Material type \n#  D -> Material name \n#  E -> Geometry type \n#  F -> Geometry name \n#	A	B		C		D		E		F\n^\n</LAYER_PROPERTIES>\n","<LAYER_PATTERNS>\n## Keyword: _LAY_PATT \n## Layer patterns \n# N. of layer patterns \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Pattern name \n#  C -> N. of layers \n#  E -> Layer (or layer range) \n#  D -> Name of the layer properties in the [LAYER_PROPERTIES] block \n# \n#	A	B			C		D	E	\n^\n</LAYER_PATTERNS>\n","<NUMERICAL_INTEGRATION>\n## Keywords: _GLEG, _GLOB or _NCOTES \n## Numerical integration definition \n# N. of layouts of integration points \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the layout \n#  C -> N. of keywords defining the layout \n#  D -> Keywords defining the type of term and the n. of points in each direction \n# \n#  Valid syntax for D is the following: \n#     _i_Sj_k \n# \n#     i: type of terms \n#       G -> General use (stiffness matrix, springs and load vector) \n#       M -> Membrane terms (stiffness matrix) \n#       S -> Shear terms (stiffness matrix) \n#       T -> Torsional terms (stiffness matrix) \n#       B -> Bending terms (stiffness matrix) \n# \n#     j: direction \n#       1 -> direction s1 \n#       2 -> direction s2 \n#       3 -> direction s3 \n# \n#     k: n. of points in the specified direction \n# \n#     Note: k must be in the range [1,10] \n# \n#     Examples: _G_S1_5, _M_S2_1, _S_S3_2, _T_S1_7, _B_S2_10 \n# \n#		A	B				C  D\n^\n</NUMERICAL_INTEGRATION>\n","<ELEMENT_NODES>\n## Nodes defining the elements \n# N. of elements \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> N. of nodes of the element \n#  C -> Nodes of the element \n#	A	B	C\n^\n</ELEMENT_NODES>\n","<ELEMENT_PROPERTIES>\n## Specification of the element properties \n# N. of specifications \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Element (or element range) \n#  E -> Element type \n#  F -> Material type \n#  G -> Material name \n#  H -> Geometry type \n#  I -> Geometry name \n#  J -> Integration type (stiffness matrix) \n#  K -> Integration name (stiffness matrix) \n#	A	B		C		D	E			F			G			H			I			J			K\n^\n</ELEMENT_PROPERTIES>\n","<LINEAR_SPRING_MATERIALS>\n## Keyword: _LIN_SPRING \n## Properties of the linear spring materials \n# N. of linear spring materials \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Material name \n#  C -> Stiffness \n#		A	B		C\n^\n</LINEAR_SPRING_MATERIALS>\n","<POINT_SPRINGS>\n## Points with springs \n# N. of point springs \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number (or point number range) \n#  E -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  F -> Spring location: \n#       - Direction vector from the structure (S) to the ground (G): _SG \n#       - Direction vector from the ground (G) to the structure (S): _GS \n#         Example: \n#           Direction vector: -----> _XG1 \n#                                            E     F \n#           Structure - Spring - Ground  =>  _XG1  _SG \n#           Ground - Spring - Structure  =>  _XG1  _GS \n#  G -> Dof type (_TRANS - Translational; _ROT - Rotational) \n#  H -> Spring material type \n#  I -> Spring material name(s) \n#		A		B			C			D	E		F		G		H		I\n^\n</POINT_SPRINGS>\n","<LINE_SPRINGS>\n## Lines with springs \n# N. of line springs \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Element number (or element number range) \n#  E -> Face number \n#  F -> Edge number \n#  G -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Timoshenko beam: _L1, _L2 or _L3 \n#       - Solid/Shell: _T, _NN or _NT \n#       - Plane stress/Plane strain/Axisymmetry: _T or _NT \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  H -> Spring location: \n#       - Direction vector from the structure (S) to the ground (G): _SG \n#       - Direction vector from the ground (G) to the structure (S): _GS \n#         Example: \n#           Direction vector: -----> _XG1 \n#                                            G     H \n#           Structure - Spring - Ground  =>  _XG1  _SG \n#           Ground - Spring - Structure  =>  _XG1  _GS \n#  I -> Dof type (_TRANS - Translational; _ROT - Rotational) \n#  J -> Integration type (spring stiffness matrix) \n#  K -> Integration name (spring stiffness matrix) \n#  L -> Spring material type \n#  M -> N. of nodes of the edge of the element \n#  N -> Spring material name(s) \n# \n#  Note: when M is equal to 1 the spring is constant \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the bar \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _T  - tangential to the edge \n#     _NN - normal to the edge and normal to the element face \n#     _NT - normal to the edge and tangential to the element face \n# \n#		A	B			C			D		E	F	G		H		I			J			K			L			M	N\n^\n</LINE_SPRINGS>\n","<SURFACE_SPRINGS>\n## Surface with springs \n# N. of surface springs \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Element number (or element number range) \n#  E -> Face number \n#  F -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Solid/Shell: _L1, _L2 or _N \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  G -> Spring location: \n#       - Direction vector from the structure (S) to the ground (G): _SG \n#       - Direction vector from the ground (G) to the structure (S): _GS \n#         Example: \n#           Direction vector: -----> _XG1 \n#                                            F     G \n#           Structure - Spring - Ground  =>  _XG1  _SG \n#           Ground - Spring - Structure  =>  _XG1  _GS \n#  H -> Integration type (spring stiffness matrix) \n#  I -> Integration name (spring stiffness matrix) \n#  J -> Spring material type \n#  K -> N. of nodes of the face of the element \n#  L -> Spring material name(s) \n# \n#  Notes: \n#         when K is equal to 1 the spring is constant \n#         Only translational stiffness is available \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the face and following the l1 local axis \n#     _L2 - tangential to the face and following the l2 local axis \n#     _N  - normal to the face \n# \n#		A	B			C			D	E	F		G	H		I		J			K	L\n^\n</SURFACE_SPRINGS>\n","<STIFFENERS>\n## Stiffener elements \n# N. of stiffeners \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Stiffener element (or stiffener element range) \n#  C -> Mother element (or mother element range) \n# \n#		A		B		C\n^\n</STIFFENERS>\n","<LOAD_CASE_PARAMETERS>\n^\n</LOAD_CASE_PARAMETERS>\n","<GRAVITY_LOAD>\n## Gravity acceleration (global coordinate system) \n## Content of each column: \n#  A -> Acceleration - XG1 \n#  B -> Acceleration - XG2 \n#  C -> Acceleration - XG3 \n#		A		B		C\n^\n</GRAVITY_LOAD>\n","<POINT_LOADS>\n## Point loads (global coordinate system) \n# N. of point loads \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded point (or loaded point range) \n#  C -> Force - XG1 \n#  D -> Force - XG2 \n#  E -> Force - XG3 \n#  F -> Moment - XG1 \n#  G -> Moment - XG2 \n#  H -> Moment - XG3 \n#		A		B		C		D		E		F		G		H\n^\n</POINT_LOADS>\n","<INTERNAL_POINT_LOADS>\n## Load applied to a point inside an element (global coordinate system) \n# N. of internal point loads \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element \n#  C -> Auxiliary point with the load (or auxiliary point range) \n#  D -> Force - XG1 \n#  E -> Force - XG2 \n#  F -> Force - XG3 \n#  G -> Moment - XG1 \n#  H -> Moment - XG2 \n#  I -> Moment - XG3 \n#		A		B		C		D		E		F		G		H		I\n^\n</INTERNAL_POINT_LOADS>\n","<EDGE_LOADS>\n## Load on the edge of an element \n# N. of edge loads \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Loaded face \n#  D -> Loaded edge \n#  E -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Frame: _L1, _L2 or _L3 \n#       - Solid/Shell: _T, _NN or _NT \n#       - Plane stress/Plane strain/Axisymmetry: _T or _NT \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  F -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  G -> N. of nodes of the edge of the element \n#  H -> Load values \n# \n#  Note: when G is equal to 1 the load is constant \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the bar \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _T  - tangential to the edge \n#     _NN - normal to the edge and normal to the element face \n#     _NT - normal to the edge and tangential to the element face \n# \n#		A		B		C	D	E		F		G		H\n^\n</EDGE_LOADS>\n","<INTERNAL_EDGE_LOADS>\n## Line load inside an element \n# N. of internal edge loads \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Loaded element \n#  C -> Auxiliary line element with the load \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Frame: _L1, _L2 or _L3 \n#       - Solid surface/Shell: _T, _NN or _NT \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the auxiliary line element \n#  G -> Load values \n# \n#  Notes: \n#  - when F is equal to 1 the load is constant; \n#  - the n. of nodes of the auxiliary line element must match the n. of nodes of the edge of the loaded element. \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the bar \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _T  - tangential to the edge \n#     _NN - normal to the edge and normal to the element face \n#     _NT - normal to the edge and tangential to the element face \n# \n#		A		B		C	D		E		F		G\n^\n</INTERNAL_EDGE_LOADS>\n","<FACE_LOADS>\n## Load on the face of an element \n# N. of face loads \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Loaded face \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Solid/Shell: _L1, _L2 or _N \n#       - Plane stress/Plane strain/Axisymmetry: _L1 or _L2 \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the face of the element \n#  G -> Load values \n# \n#  Note: when F is equal to 1 the load is constant \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the face and following the l1 local axis \n#     _L2 - tangential to the face and following the l2 local axis \n#     _N  - normal to the face \n# \n#		A		B		C	D		E		F		G\n^\n</FACE_LOADS>\n","<INTERNAL_FACE_LOADS>\n## Surface load inside an element \n# N. of internal face loads \n\n \n## Content of each column: \n#  A -> Counter \n#  B -> Loaded element \n#  C -> Auxiliary surface element with the load \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Solid surface/Shell: _L1, _L2 or _N \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the auxiliary surface element \n#  G -> Load values \n# \n#  Notes: \n#  - when F is equal to 1 the load is constant; \n#  - the n. of nodes of the auxiliary surface element must match the n. of nodes of the face of the loaded element. \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the face and following the l1 local axis \n#     _L2 - tangential to the face and following the l2 local axis \n#     _N  - normal to the face \n# \n#		A		B		C	D			E			F			G\n^\n</INTERNAL_FACE_LOADS>\n","<TEMPERATURE_VARIATION>\n## Elements with temperature variation \n# N. of elements with temperature variation \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> N. of nodes of the element \n#  D -> Temperature variation values \n# \n#  Note: when C is equal to 1 the temperature variation is the same for all the element nodes \n# \n#		A		B		C		D\n^\n</TEMPERATURE_VARIATION>\n","<DIFFERENTIAL_TEMPERATURE_VARIATION>\n## Elements with differential temperature variation \n# N. of elements with differential temperature variation \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Direction vector: \n#       - Frame: _L2 or _L3 \n#       - Shell: _N \n#  D -> N. of nodes of the element \n#  E -> Differential temperature variation values \n# \n#  Note: when D is equal to 1 the diff. temp. var. is the same for all the element nodes \n# \n#  Direction vector keywords: \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _N  - normal to the shell mean surface \n# \n#		A		B	C		D		E\n^\n</DIFFERENTIAL_TEMPERATURE_VARIATION>\n","<PRESCRIBED_DISPLACEMENTS>\n## Points with prescribed displacement \n# N. of prescribed displacements \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Point number (or point number range) \n#  C -> Prescribed degree of freedom \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#  D -> Value of the prescribed displacement \n#		A		B	C		D\n^\n</PRESCRIBED_DISPLACEMENTS>\n","<INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS>\n## Points with internal prescribed relative displacement \n# N. of internal prescribed relative displacements \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Point number - M (Master) (or point number range) \n#  C -> Point number - S (Slave) (or point number range) \n#  D -> Prescribed degree of freedom \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#  E -> Value of the prescribed displacement \n#		A		B		C	D		E\n^\n</INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS>\n","<LOAD_CASE_FACTORS>\n## Load case factors defining a combination \n# N. of load cases in the combination \n\n \n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Load case number (or load case range) \n#  C -> Load case factor \n#		A		B		C\n^\n</LOAD_CASE_FACTORS>\n",};
        private void BuildAutocompleteMenu()
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();
            foreach (var item in keywords)
                items.Add(new AutocompleteItem(item));
            foreach (var item in snippets)
                items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            //set as autocomplete source
            popupMenu2.Items.SetAutocompleteItems(items);
        }

        private void DarkMode()
        {
            Femixbox1.Font = new System.Drawing.Font("Consolas", 12.5F);

            Femixbox1.BackColor = System.Drawing.Color.FromArgb(255, 30, 30, 30);
            //Femixbox1.BookmarkColor = System.Drawing.Color.LightBlue;
            Femixbox1.FoldingIndicatorColor = System.Drawing.Color.GreenYellow;
            Femixbox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            Femixbox1.IndentBackColor = System.Drawing.Color.FromArgb(255, 30, 30, 30);
            Femixbox1.SelectionColor = System.Drawing.Color.Cyan;

        }
        private void Femixbox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            //fctb.GoEnd();
            //fctb.Focus();
            //Help.ShowPopup(textBox1, "Enter your first name", new Point(textBox1.Right, this.textBox1.Bottom));                     

            Femixbox1.LeftBracket = '(';
            Femixbox1.RightBracket = ')';
            Femixbox1.LeftBracket2 = '[';
            Femixbox1.RightBracket2 = ']';

            //Femixbox1.Language = Language.Custom;

            //Femixbox1.AutoIndentCharsPatterns= @"^\s*[\w\.\(\)]+\s*(?<range>=)\s*(?<range>.+)";
            //Femixbox1.AutoIndentCharsPatterns= @"^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>[^;=]+);";
            //Femixbox1.AutoIndentCharsPatterns = @"^\s*[\w\.]+(\s\w+)?\s*([=;])\s*(?<range>[^=]+)[;]";
            // Femixbox1.AutoIndentCharsPatterns = @"^\s*[\w\.]+(\s\w+)?\s*(?<range>[=;])\s*(?<range>[^=]+)[;]";
            //Femixbox1.AutoIndentCharsPatterns = @"^\s*[\w\.]+(\s\w+)?\s*(?<range>[;])\s*(?<range>[^;=]+)";


            //clear style of changed range
            e.ChangedRange.ClearStyle(BlueStyle, BoldStyle, GrayStyle, MagentaStyle, GreenStyle, BrownStyle);
            // between < >
            e.ChangedRange.SetStyle(BlueStyle, "<[^>]+>");

            //string highlighting
            e.ChangedRange.SetStyle(BrownStyle, @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            //comment highlighting
            e.ChangedRange.SetStyle(GreenStyle, @"#.*$", RegexOptions.Multiline);
            //e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            //e.ChangedRange.SetStyle(GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            //number highlighting
            e.ChangedRange.SetStyle(MagentaStyle, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            //attribute highlighting
            e.ChangedRange.SetStyle(GrayStyle, @"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            //class name highlighting
            e.ChangedRange.SetStyle(BoldStyle, @"\b(COUNT)\b");
            //keyword highlighting
            e.ChangedRange.SetStyle(MaroonStyle, @"\b(MAIN_TITLE|ANALYSIS_TYPE|MATERIALLY_NONLINEAR_PROBLEM|GEOMETRICALLY_NONLINEAR_PROBLEM|NUMBER_OF_PHASES|NUMBER_OF_LOAD_CASES|NUMBER_OF_COMBINATIONS|STIFFNESS_MATRIX_STORAGE_TECHNIQUE|SYSTEM_LIN_EQ_ALGORITHM|COUNT|LOAD_CASE_NUMBER|INTEGRATION_TYPE|LOAD_CASE_TITLE|LOAD_CASE_GROUP|INTEGRATION_NAME|MAIN_TITLE|ANALYSIS_TYPE|MATERIALLY_NONLINEAR_PROBLEM|GEOMETRICALLY_NONLINEAR_PROBLEM|NUMBER_OF_PHASES|NUMBER_OF_LOAD_CASES|NUMBER_OF_COMBINATIONS|STIFFNESS_MATRIX_STORAGE_TECHNIQUE|SYSTEM_LIN_EQ_ALGORITHM|COUNT|LOAD_CASE_NUMBER|INTEGRATION_TYPE|LOAD_CASE_TITLE|LOAD_CASE_GROUP|INTEGRATION_NAME|PRECONDITIONED_RESIDUAL_DECAY|ITERATIVE_ALGORITHM|CONVERGENCE_CRITERION|TOLERANCE_IN_EACH_COMBINATION|MAXIMUM_NUMBER_OF_ITERATIONS_IN_EACH_COMBINATION|NUMBER_OF_COMBINATIONS_BEFORE_RESTART|ADD_THE_UNBALANCED_FORCES_IN_EACH_COMBINATION|ARC_LENGTH|LINE_SEARCH|PATH_BEHAVIOR)\b");
            e.ChangedRange.SetStyle(MagentaStyle, @"\b(_D1|_D2|_D3|_R1|_R2|_R3|_Y|_N|_NONE|_STATIC|_SYMMETRIC_NONCONSTANT_SEMIBAND|_GAUSS_ELIM|_NEWTON_RAPHSON_STD|_FORCE_NORM|_INDEPENDENT)\b");

            //some charachters
            e.ChangedRange.SetStyle(BrownStyle, ";");
            //e.ChangedRange.SetStyle(errorred, @";|", RegexOptions.Multiline);
            //e.ChangedRange.SetStyle(BrownStyle, @"=|\s;|", RegexOptions.Multiline);

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //Femixbox1.Range.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers(@"<FEMIX_DATA_FILE_V4.0>", @"</FEMIX_DATA_FILE_V4.0>");
            e.ChangedRange.SetFoldingMarkers(@"<MAIN_PARAMETERS>", @"</MAIN_PARAMETERS>");
            e.ChangedRange.SetFoldingMarkers(@"<MESH>", @"</MESH>");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"<GROUP_NAMES>", @"</GROUP_NAMES>");
            e.ChangedRange.SetFoldingMarkers(@"<POINT_COORDINATES>", @"</POINT_COORDINATES>");
            e.ChangedRange.SetFoldingMarkers(@"<AUXILIARY_VECTORS>", @"</AUXILIARY_VECTORS>");
            e.ChangedRange.SetFoldingMarkers(@"<COORDINATE_SYSTEMS>", @"</COORDINATE_SYSTEMS>");
            e.ChangedRange.SetFoldingMarkers(@"<POINTS_WITH_SPECIAL_COORDINATE_SYSTEM>", @"</POINTS_WITH_SPECIAL_COORDINATE_SYSTEM>");
            e.ChangedRange.SetFoldingMarkers(@"<SUPPORTS>", @"</SUPPORTS>");
            e.ChangedRange.SetFoldingMarkers(@"<INTERNAL_POINT_LINKS>", @"</INTERNAL_POINT_LINKS>");
            e.ChangedRange.SetFoldingMarkers(@"<LINEAR_ISOTROPIC_MATERIALS>", @"</LINEAR_ISOTROPIC_MATERIALS>");
            e.ChangedRange.SetFoldingMarkers(@"<LINEAR_ORTHOTROPIC_MATERIALS>", @"</LINEAR_ORTHOTROPIC_MATERIALS>");
            e.ChangedRange.SetFoldingMarkers(@"<LINEAR_INTERFACE_POINT_PROPERTIES>", @"</LINEAR_INTERFACE_POINT_PROPERTIES>");
            e.ChangedRange.SetFoldingMarkers(@"<LINEAR_INTERFACE_LINE_PROPERTIES>", @"</LINEAR_INTERFACE_LINE_PROPERTIES>");
            e.ChangedRange.SetFoldingMarkers(@"<LINEAR_INTERFACE_SURFACE_PROPERTIES>", @"</LINEAR_INTERFACE_SURFACE_PROPERTIES>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM101>", @"</NLMM101>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM104>", @"</NLMM104>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM111>", @"</NLMM111>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM141>", @"</NLMM141>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM151>", @"</NLMM151>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM201>", @"</NLMM201>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM202>", @"</NLMM202>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM210>", @"</NLMM210>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM220>", @"</NLMM220>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM298>", @"</NLMM298>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM299>", @"</NLMM299>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM123>", @"</NLMM123>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM121>", @"</NLMM121>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM124>", @"</NLMM124>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM125>", @"</NLMM125>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM142>", @"</NLMM142>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM145>", @"</NLMM145>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM211>", @"</NLMM211>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM102>", @"</NLMM102>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM122>", @"</NLMM122>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM401>", @"</NLMM401>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM451>", @"</NLMM451>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM301>", @"</NLMM301>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM302>", @"</NLMM302>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM303>", @"</NLMM303>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM305>", @"</NLMM305>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM306>", @"</NLMM306>");
            e.ChangedRange.SetFoldingMarkers(@"<NLMM320>", @"</NLMM320>");
            e.ChangedRange.SetFoldingMarkers(@"<CROSS_SECTION_AREAS>", @"</CROSS_SECTION_AREAS>");
            e.ChangedRange.SetFoldingMarkers(@"<FRAME_2D_CROSS_SECTIONS>", @"</FRAME_2D_CROSS_SECTIONS>");
            e.ChangedRange.SetFoldingMarkers(@"<FRAME_3D_CROSS_SECTIONS>", @"</FRAME_3D_CROSS_SECTIONS>");
            e.ChangedRange.SetFoldingMarkers(@"<LAMINATE_THICKNESSES>", @"</LAMINATE_THICKNESSES>");
            e.ChangedRange.SetFoldingMarkers(@"<GEOMETRY_PATTERNS>", @"</GEOMETRY_PATTERNS>");
            e.ChangedRange.SetFoldingMarkers(@"<FIBROUS_PATTERNS>", @"</FIBROUS_PATTERNS>");
            e.ChangedRange.SetFoldingMarkers(@"<LAYER_PROPERTIES>", @"</LAYER_PROPERTIES>");
            e.ChangedRange.SetFoldingMarkers(@"<LAYER_PATTERNS>", @"</LAYER_PATTERNS>");
            e.ChangedRange.SetFoldingMarkers(@"<NUMERICAL_INTEGRATION>", @"</NUMERICAL_INTEGRATION>");
            e.ChangedRange.SetFoldingMarkers(@"<ELEMENT_NODES>", @"</ELEMENT_NODES>");
            e.ChangedRange.SetFoldingMarkers(@"<ELEMENT_PROPERTIES>", @"</ELEMENT_PROPERTIES>");
            e.ChangedRange.SetFoldingMarkers(@"<LINEAR_SPRING_MATERIALS>", @"</LINEAR_SPRING_MATERIALS>");
            e.ChangedRange.SetFoldingMarkers(@"<POINT_SPRINGS>", @"</POINT_SPRINGS>");
            e.ChangedRange.SetFoldingMarkers(@"<LINE_SPRINGS>", @"</LINE_SPRINGS>");
            e.ChangedRange.SetFoldingMarkers(@"<SURFACE_SPRINGS>", @"</SURFACE_SPRINGS>");
            e.ChangedRange.SetFoldingMarkers(@"<STIFFENERS>", @"</STIFFENERS>");
            e.ChangedRange.SetFoldingMarkers(@"<LOAD>", @"</LOAD>");
            e.ChangedRange.SetFoldingMarkers(@"<LOAD_CASE>", @"</LOAD_CASE>");
            e.ChangedRange.SetFoldingMarkers(@"<LOAD_CASE_PARAMETERS>", @"</LOAD_CASE_PARAMETERS>");
            e.ChangedRange.SetFoldingMarkers(@"<GRAVITY_LOAD>", @"</GRAVITY_LOAD>");
            e.ChangedRange.SetFoldingMarkers(@"<POINT_LOADS>", @"</POINT_LOADS>");
            e.ChangedRange.SetFoldingMarkers(@"<INTERNAL_POINT_LOADS>", @"</INTERNAL_POINT_LOADS>");
            e.ChangedRange.SetFoldingMarkers(@"<EDGE_LOADS>", @"</EDGE_LOADS>");
            e.ChangedRange.SetFoldingMarkers(@"<INTERNAL_EDGE_LOADS>", @"</INTERNAL_EDGE_LOADS>");
            e.ChangedRange.SetFoldingMarkers(@"<FACE_LOADS>", @"</FACE_LOADS>");
            e.ChangedRange.SetFoldingMarkers(@"<INTERNAL_FACE_LOADS>", @"</INTERNAL_FACE_LOADS>");
            e.ChangedRange.SetFoldingMarkers(@"<TEMPERATURE_VARIATION>", @"</TEMPERATURE_VARIATION>");
            e.ChangedRange.SetFoldingMarkers(@"<DIFFERENTIAL_TEMPERATURE_VARIATION>", @"</DIFFERENTIAL_TEMPERATURE_VARIATION>");
            e.ChangedRange.SetFoldingMarkers(@"<PRESCRIBED_DISPLACEMENTS>", @"</PRESCRIBED_DISPLACEMENTS>");
            e.ChangedRange.SetFoldingMarkers(@"<INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS>", @"</INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS>");
            e.ChangedRange.SetFoldingMarkers(@"<LOAD_CASE_COMBINATIONS>", @"</LOAD_CASE_COMBINATIONS>");
            e.ChangedRange.SetFoldingMarkers(@"<COMBINATION>", @"</COMBINATION>");
            e.ChangedRange.SetFoldingMarkers(@"<COMBINATION_PARAMETERS>", @"</COMBINATION_PARAMETERS>");
            e.ChangedRange.SetFoldingMarkers(@"<LOAD_CASE_FACTORS>", @"</LOAD_CASE_FACTORS>");



            //e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            //e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }

        private void Femixbox1_Load(object sender, EventArgs e)
        {
            //create autocomplete popup menu
            popupMenu2 = new FastColoredTextBoxNS.AutocompleteMenu(Femixbox1);
            popupMenu2.MinFragmentLength = 1;
            popupMenu2.Items.ImageList = imageList1_editor;
            popupMenu2.SearchPattern = @"[\w\.:=!<>/]";
            popupMenu2.AllowTabKey = true;
            popupMenu2.Items.MaximumSize = new System.Drawing.Size(300, 400);
            popupMenu2.Items.Width = 400;
            //
            BuildAutocompleteMenu();

            //DarkMode();
        }

#endregion femix_text_editor



        private void save_femix_toolstrip_Click(object sender, EventArgs e)
        {
            //Class1 ddd = new Class1();
            //MessageBox.Show(ddd.kkkkkk(textBox1));

            System.IO.File.WriteAllText(femix_filepath, C_variables.Femix_string);

            femix_toolstip.Enabled = true;
            postfemix_toolstrip.Enabled = true;
            toolStripclean.Enabled = true;
            LogBox_add("Femix file saved successfully.");
        }

        private void open_femix_toolstrip_Click(object sender, EventArgs e)
        {
            
            //Form2 form2 = new Form2();
            //form2.Show(textBox1);
            //new Form2().ShowDialog();

            openFileDialog_femix.Filter = "Femix|*_gl.dat";
            openFileDialog_femix.Title = "Select Femix input file (*_gl.dat)";

            DialogResult result = openFileDialog_femix.ShowDialog();
            if (result == DialogResult.OK)
            // openFileDialog_femix.FileName = @"C:\Users\Kamyar\Desktop\Femixinput\T3-18-2_gl.dat";
            {


                femix_filepath = openFileDialog_femix.FileName;
                this.Text = "Femix PrePost(" + femix_filepath + ")";

                LogBox.AppendText("Femix input file:");
                LogBox.AppendText(Environment.NewLine);
                LogBox.AppendText(femix_filepath + Environment.NewLine);



                //int TotalLines = File.ReadLines(femix_filepath).Count(); // Count the number of rows in file with lazy load
                //if (TotalLines > 300000)
                //{
                //    List<string> lines = new List<string>();
                //    StringBuilder resultAsString = new StringBuilder();
                //    int Limit = 300000; // 100000 rows per batch
                //    for (int Offset = 0; Offset < TotalLines; Offset += Limit)
                //    {
                //        Application.DoEvents();
                //        Console.WriteLine(Offset);
                //        //resultAsString = new StringBuilder();
                //        lines = File.ReadLines(femix_filepath).Skip(Offset).Take(Limit).ToList();
                //        foreach (string line in lines)
                //            resultAsString.AppendLine(line);
                //    }
                //    C_variables.Femix_string = resultAsString.ToString();
                //}
                //else
                //{
                //    C_variables.Femix_string = System.IO.File.ReadAllText(femix_filepath);
                //}

                C_variables.Femix_string = System.IO.File.ReadAllText(femix_filepath);

                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                //sw.Stop();
                //Console.WriteLine("Elapsed={0}", sw.Elapsed);

                Class_femix_reader.extract_femix(femix_filepath, C_variables.Femix_string);
                femix_toolstip.Enabled = false;
                postfemix_toolstrip.Enabled = false;
                toolStripclean.Enabled = false;

                combo_sections_fill();
                tree_femix_fill();
                load_parts();

                //splitContainer3.Panel1.Controls.Clear();
                //splitContainer1.Panel2.Controls.Remove(Femixbox1);
                //TextBox Femixbox2 = new TextBox();
                //Femixbox2.Name = "Femixbox1";
                //Femixbox2.Text = "helloo";
                //splitContainer1.Panel2.Controls.Add(Femixbox2);



                Femixbox1.Text = C_variables.Femix_string;


                if (tabControl_left.SelectedIndex != 2)
                {
                    tabControl_left.SelectedIndex = 2;
                }
                else
                {
                    draw_objects();
                }


                //load_femix_draw();
                femix_toolstip.Enabled = true;
                postfemix_toolstrip.Enabled = true;
                toolStripclean.Enabled = true;

                save_femix_toolstrip.Enabled = true;
                save_as_toolstrip.Enabled = true;
                tabControl_left.Enabled = true;
            }
        }

        private void tree_femix_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("ddd");
        }

        private void tree_femix_DoubleClick(object sender, EventArgs e)
        {
            string _target_item = "";
            combo_sections.SelectedIndex = 0;
            TreeViewHitTestInfo info = tree_femix.HitTest(tree_femix.PointToClient(Cursor.Position));
            if (info != null)
            {
                _target_item = info.Node.FullPath;
                if (_target_item.Contains("MAIN_PARAMETERS"))
                    combo_sections.SelectedItem = "MAIN_PARAMETERS";
                if (_target_item.Contains("GROUP_NAMES"))
                    combo_sections.SelectedItem = "GROUP_NAMES";
                if (_target_item.Contains("POINT_COORDINATES"))
                    combo_sections.SelectedItem = "POINT_COORDINATES";
                if (_target_item.Contains("AUXILIARY_VECTORS"))
                    combo_sections.SelectedItem = "AUXILIARY_VECTORS";
                if (_target_item.Contains("COORDINATE_SYSTEMS"))
                    combo_sections.SelectedItem = "COORDINATE_SYSTEMS";
                if (_target_item.Contains("POINTS_WITH_SPECIAL_COORDINATE_SYSTEM"))
                    combo_sections.SelectedItem = "POINTS_WITH_SPECIAL_COORDINATE_SYSTEM";
                if (_target_item.Contains("SUPPORTS"))
                    combo_sections.SelectedItem = "SUPPORTS";
                if (_target_item.Contains("INTERNAL_POINT_LINKS"))
                    combo_sections.SelectedItem = "INTERNAL_POINT_LINKS";
                if (_target_item.Contains("LINEAR_ISOTROPIC_MATERIALS"))
                    combo_sections.SelectedItem = "LINEAR_ISOTROPIC_MATERIALS";
                if (_target_item.Contains("LINEAR_ORTHOTROPIC_MATERIALS"))
                    combo_sections.SelectedItem = "LINEAR_ORTHOTROPIC_MATERIALS";
                if (_target_item.Contains("LINEAR_INTERFACE_POINT_PROPERTIES"))
                    combo_sections.SelectedItem = "LINEAR_INTERFACE_POINT_PROPERTIES";
                if (_target_item.Contains("LINEAR_INTERFACE_LINE_PROPERTIES"))
                    combo_sections.SelectedItem = "LINEAR_INTERFACE_LINE_PROPERTIES";
                if (_target_item.Contains("LINEAR_INTERFACE_SURFACE_PROPERTIES"))
                    combo_sections.SelectedItem = "LINEAR_INTERFACE_SURFACE_PROPERTIES";
                if (_target_item.Contains("NLMM101"))
                    combo_sections.SelectedItem = "NLMM101";
                if (_target_item.Contains("NLMM104"))
                    combo_sections.SelectedItem = "NLMM104";
                if (_target_item.Contains("NLMM111"))
                    combo_sections.SelectedItem = "NLMM111";
                if (_target_item.Contains("NLMM141"))
                    combo_sections.SelectedItem = "NLMM141";
                if (_target_item.Contains("NLMM151"))
                    combo_sections.SelectedItem = "NLMM151";
                if (_target_item.Contains("NLMM201"))
                    combo_sections.SelectedItem = "NLMM201";
                if (_target_item.Contains("NLMM202"))
                    combo_sections.SelectedItem = "NLMM202";
                if (_target_item.Contains("NLMM210"))
                    combo_sections.SelectedItem = "NLMM210";
                if (_target_item.Contains("NLMM220"))
                    combo_sections.SelectedItem = "NLMM220";
                if (_target_item.Contains("NLMM298"))
                    combo_sections.SelectedItem = "NLMM298";
                if (_target_item.Contains("NLMM299"))
                    combo_sections.SelectedItem = "NLMM299";
                if (_target_item.Contains("NLMM123"))
                    combo_sections.SelectedItem = "NLMM123";
                if (_target_item.Contains("NLMM121"))
                    combo_sections.SelectedItem = "NLMM121";
                if (_target_item.Contains("NLMM124"))
                    combo_sections.SelectedItem = "NLMM124";
                if (_target_item.Contains("NLMM125"))
                    combo_sections.SelectedItem = "NLMM125";
                if (_target_item.Contains("NLMM142"))
                    combo_sections.SelectedItem = "NLMM142";
                if (_target_item.Contains("NLMM145"))
                    combo_sections.SelectedItem = "NLMM145";
                if (_target_item.Contains("NLMM211"))
                    combo_sections.SelectedItem = "NLMM211";
                if (_target_item.Contains("NLMM102"))
                    combo_sections.SelectedItem = "NLMM102";
                if (_target_item.Contains("NLMM122"))
                    combo_sections.SelectedItem = "NLMM122";
                if (_target_item.Contains("NLMM401"))
                    combo_sections.SelectedItem = "NLMM401";
                if (_target_item.Contains("NLMM451"))
                    combo_sections.SelectedItem = "NLMM451";
                if (_target_item.Contains("NLMM301"))
                    combo_sections.SelectedItem = "NLMM301";
                if (_target_item.Contains("NLMM302"))
                    combo_sections.SelectedItem = "NLMM302";
                if (_target_item.Contains("NLMM303"))
                    combo_sections.SelectedItem = "NLMM303";
                if (_target_item.Contains("NLMM305"))
                    combo_sections.SelectedItem = "NLMM305";
                if (_target_item.Contains("NLMM306"))
                    combo_sections.SelectedItem = "NLMM306";
                if (_target_item.Contains("NLMM320"))
                    combo_sections.SelectedItem = "NLMM320";
                if (_target_item.Contains("CROSS_SECTION_AREAS"))
                    combo_sections.SelectedItem = "CROSS_SECTION_AREAS";
                if (_target_item.Contains("FRAME_2D_CROSS_SECTIONS"))
                    combo_sections.SelectedItem = "FRAME_2D_CROSS_SECTIONS";
                if (_target_item.Contains("FRAME_3D_CROSS_SECTIONS"))
                    combo_sections.SelectedItem = "FRAME_3D_CROSS_SECTIONS";
                if (_target_item.Contains("LAMINATE_THICKNESSES"))
                    combo_sections.SelectedItem = "LAMINATE_THICKNESSES";
                if (_target_item.Contains("GEOMETRY_PATTERNS"))
                    combo_sections.SelectedItem = "GEOMETRY_PATTERNS";
                if (_target_item.Contains("FIBROUS_PATTERNS"))
                    combo_sections.SelectedItem = "FIBROUS_PATTERNS";
                if (_target_item.Contains("LAYER_PROPERTIES"))
                    combo_sections.SelectedItem = "LAYER_PROPERTIES";
                if (_target_item.Contains("LAYER_PATTERNS"))
                    combo_sections.SelectedItem = "LAYER_PATTERNS";
                if (_target_item.Contains("NUMERICAL_INTEGRATION"))
                    combo_sections.SelectedItem = "NUMERICAL_INTEGRATION";
                if (_target_item.Contains("ELEMENT_NODES"))
                    combo_sections.SelectedItem = "ELEMENT_NODES";
                if (_target_item.Contains("ELEMENT_PROPERTIES"))
                    combo_sections.SelectedItem = "ELEMENT_PROPERTIES";
                if (_target_item.Contains("LINEAR_SPRING_MATERIALS"))
                    combo_sections.SelectedItem = "LINEAR_SPRING_MATERIALS";
                if (_target_item.Contains("POINT_SPRINGS"))
                    combo_sections.SelectedItem = "POINT_SPRINGS";
                if (_target_item.Contains("LINE_SPRINGS"))
                    combo_sections.SelectedItem = "LINE_SPRINGS";
                if (_target_item.Contains("SURFACE_SPRINGS"))
                    combo_sections.SelectedItem = "SURFACE_SPRINGS";
                if (_target_item.Contains("STIFFENERS"))
                    combo_sections.SelectedItem = "STIFFENERS";
                if (_target_item.Contains("LOAD_CASE_PARAMETERS"))
                    combo_sections.SelectedItem = "LOAD_CASE_PARAMETERS";
                if (_target_item.Contains("GRAVITY_LOAD"))
                    combo_sections.SelectedItem = "GRAVITY_LOAD";
                if (_target_item.Contains("POINT_LOADS"))
                    combo_sections.SelectedItem = "POINT_LOADS";
                if (_target_item.Contains("INTERNAL_POINT_LOADS"))
                    combo_sections.SelectedItem = "INTERNAL_POINT_LOADS";
                if (_target_item.Contains("EDGE_LOADS"))
                    combo_sections.SelectedItem = "EDGE_LOADS";
                if (_target_item.Contains("INTERNAL_EDGE_LOADS"))
                    combo_sections.SelectedItem = "INTERNAL_EDGE_LOADS";
                if (_target_item.Contains("FACE_LOADS"))
                    combo_sections.SelectedItem = "FACE_LOADS";
                if (_target_item.Contains("INTERNAL_FACE_LOADS"))
                    combo_sections.SelectedItem = "INTERNAL_FACE_LOADS";
                if (_target_item.Contains("TEMPERATURE_VARIATION"))
                    combo_sections.SelectedItem = "TEMPERATURE_VARIATION";
                if (_target_item.Contains("DIFFERENTIAL_TEMPERATURE_VARIATION"))
                    combo_sections.SelectedItem = "DIFFERENTIAL_TEMPERATURE_VARIATION";
                if (_target_item.Contains("PRESCRIBED_DISPLACEMENTS"))
                    combo_sections.SelectedItem = "PRESCRIBED_DISPLACEMENTS";
                if (_target_item.Contains("INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS"))
                    combo_sections.SelectedItem = "INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS";
                if (_target_item.Contains("LOAD_CASE_COMBINATIONS"))
                    combo_sections.SelectedItem = "LOAD_CASE_COMBINATIONS";


            }

            //info.Node.FullPath
            ////info.Node.Index       info.Node.Parent        
            ////MessageBox.Show(tree_femix.SelectedNode.Text);
        }

        private void save_as_toolstrip_Click(object sender, EventArgs e)
        {
            //string femix_filepath_old = femix_filepath;
            //saveFileDialog_femix.Filter = "Femix|*_gl.dat";
            //saveFileDialog_femix.Title = "Save Femix file";
            //DialogResult result = saveFileDialog_femix.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    femix_filepath = saveFileDialog_femix.FileName.Replace("_", "-"); ;
            //    femix_filepath = Path.GetDirectoryName(femix_filepath) + @"\" + Path.GetFileNameWithoutExtension(femix_filepath) + "_gl.dat";
            //    this.Text = "Femix PrePost (" + femix_filepath + ")";

            //    System.IO.File.WriteAllText(femix_filepath, C_variables.Femix_string);

            //    femix_toolstip.Enabled = true;
            //    postfemix_toolstrip.Enabled = true;
            //    toolStripclean.Enabled = true;

            //    string path_new = Path.GetDirectoryName(femix_filepath) + @"\";
            //    string file_name_new = Path.GetFileNameWithoutExtension(femix_filepath);
            //    file_name_new = file_name_new.Remove(file_name_new.Length - 3)+"_" ;
            //    string path_old = Path.GetDirectoryName(femix_filepath_old) + @"\";
            //    string file_name_old = Path.GetFileNameWithoutExtension(femix_filepath_old);
            //    file_name_old =   file_name_old.Remove(file_name_old.Length - 3)+"_";
            //    string extensions = "_femix.part";
            //    foreach (string item in combo_part_names.Items)
            //    {
            //        System.IO.File.Copy(path_old + file_name_old + item + extensions, path_new +file_name_new  + item + extensions);
            //    }

            //    LogBox_add("Femix file saved successfully as:");
            //    LogBox_add(femix_filepath);
            //    }

            string femixFilepath = this.femix_filepath;
            this.saveFileDialog_femix.Filter = "Femix|*_gl.dat";
            this.saveFileDialog_femix.Title = "Save Femix file";
            if (this.saveFileDialog_femix.ShowDialog() != DialogResult.OK)
                return;
            this.femix_filepath = this.saveFileDialog_femix.FileName.Replace("_", "-");
            this.femix_filepath = Path.GetDirectoryName(this.femix_filepath) + "\\" + Path.GetFileNameWithoutExtension(this.femix_filepath) + "_gl.dat";
            this.Text = "Femix PrePost (" + this.femix_filepath + ")";
            File.WriteAllText(this.femix_filepath, C_variables.Femix_string);
            this.femix_toolstip.Enabled = true;
            this.postfemix_toolstrip.Enabled = true;
            this.toolStripclean.Enabled = true;
            this.runPreFEMIXToolStripMenuItem.Enabled = true;
            this.runFEMIXToolStripMenuItem.Enabled = true;
            this.runPosFEMIXToolStripMenuItem.Enabled = true;
            string str1 = Path.GetDirectoryName(this.femix_filepath) + "\\";
            string withoutExtension1 = Path.GetFileNameWithoutExtension(this.femix_filepath);
            string str2 = withoutExtension1.Remove(withoutExtension1.Length - 3) + "_";
            string path = str1 + str2 + "parts\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string str3 = Path.GetDirectoryName(femixFilepath) + "\\";
            string withoutExtension2 = Path.GetFileNameWithoutExtension(femixFilepath);
            string str4 = withoutExtension2.Remove(withoutExtension2.Length - 3) + "_";
            string str5 = str3 + str4 + "parts\\";
            string str6 = "_femix.part";
            foreach (string str7 in this.combo_part_names.Items)
                File.Copy(str5 + str7 + str6, path + str7 + str6);
            this.LogBox_add("Femix file saved successfully as:");
            this.LogBox_add(this.femix_filepath);

        }

        public void RenderWindow1_Load(object sender, EventArgs e)
        {
            try
            {
                //KbRen.ini_vtk_class(RenderWindow1);
                //KbRen.background(RenderWindow1);
                //drawcells();
                //point_cell_data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
            }
        }

        private void prefemix_toolstrip_Click(object sender, EventArgs e)
        {


        }

        private void femix_toolstip_Click(object sender, EventArgs e)
        {

            ////clear all previous analysis data
            string path = Path.GetDirectoryName(femix_filepath) + @"\";
            string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            file_name = file_name.Remove(file_name.Length - 3);


            if (File.Exists(path + file_name + "_ctrl.dat")) 
            {
                DialogResult dialogResult = MessageBox.Show($"This folder contains analysis data.\n Are you sure you want to run the analysis again?", "Run Femix", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                    return;
            };


            if (C_variables.MAIN_PARAMETERS_n > 0)
            {
                foreach (IList<string> mainParameters in C_variables.MAIN_PARAMETERS_list)
                {
                    if (mainParameters[1] == "NUMBER_OF_COMBINATIONS_BEFORE_RESTART")
                    {
                        int num1 = int.Parse(mainParameters[2]);
                        if (num1 > 0 && !File.Exists(path + file_name + "_dr.bin"))
                        {
                            int num2 = (int)MessageBox.Show(string.Format("The number of combinations before restart is {0}, but the project directory has no analysis records. \nPlease check this value is the \"MAIN_PARAMETERS\" block!", (object)num1));
                            return;
                        }
                    }
                }
            }


            //string[] extensions = { ".bin","_ctrl.dat","_lcc.aux","_ac.csv","_cp.s3d","_cs.s3d","_di.csv","_di.pva","_dm.s3d","_ep.csv"
            //,"_ep.lpt","_ev.s3d","_gl.lpt","_hd.csv","_if.csv","_ip.s3d","_me.s3d","_pz.s3d","_rd.lpt","_rd.pva","_re.csv",".lpt","_sa.pva","_se.pva","_sr.pva","_ve.csv","_op.pva","_te.pva",
            // "_te.csv","_sd.csv","_tm.lpt","_gf.pva","_cr.pva","_cr.lpt","_cf.csv","_hp.pva","_ps.s3d","_um.s3d","_cp.cfg","_cs.cfg","_pos.dat","_femix.geo","_femix.msh"};
            //foreach (string ext in extensions)
            //    foreach (string file in Directory.GetFiles(path, file_name + "*" + ext).Where(item => item.EndsWith(ext)))
            //        File.Delete(file);
            string[] folders_to_delet = { "dipva", "sepva", "sapva", "cps3d" };
            foreach (string folder in folders_to_delet)
                if (Directory.Exists(path + folder)) Directory.Delete(path + folder, true);
            ////end clear all previous analysis data


            string filepathtorun = femix_filepath.Remove(femix_filepath.Length - 7);

            femix_log femix_log = new femix_log();

            femix_log.Stringtransfer = femix_log.ExecuteCommand(@"FemixExe\Prefemix.exe " + $"\"{filepathtorun}\"", true, false);

            if (!femix_log.Stringtransfer.Contains("### Success ###"))
            {
                int num = (int)femix_log.ShowDialog();
            }
            else
            {
                LogBox_add("FEMIX analysis started: " + DateTime.Now.ToString());
                femix_log.ExecuteCommand(@"FemixExe\Femix.exe " + $"\"{filepathtorun}\"", false, true);
                LogBox_add("FEMIX analysis finished: " + DateTime.Now.ToString());
            }


            /*
            femix_log.Stringtransfer = femix_log.ExecuteCommand(@"FemixExe\Prefemix.exe " + $"\"{filepathtorun}\"", true, false);
            if (femix_log.Stringtransfer.Contains("### Success ###")==false) {
                femix_log.ShowDialog();
                return;
            }
            */



            //femix_log femix_log = new femix_log();
            //femix_log.run_femix(femix_filepath);
            //femix_log.ShowDialog();

        }

        private void postfemix_toolstrip_Click(object sender, EventArgs e)
        {


            string filepathtorun = femix_filepath.Remove(femix_filepath.Length - 7);
            string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            file_name = file_name.Remove(file_name.Length - 3);
            string directory = Path.GetDirectoryName(femix_filepath) + @"\";
            string appdirectory = AppDomain.CurrentDomain.BaseDirectory;
            femix_log femix_log = new femix_log();

            string batFileName = directory + @"\" + Guid.NewGuid() + ".bat";
            List<string> batfiledata = new List<string>();


            



            //number of layers
            int nlayer = 1;
            int nlayer_max = 1;
            string turn_on_layer = "false";
            foreach (var item_elemntprop in C_variables.ELEMENT_PROPERTIES_list)
            {
                if (item_elemntprop[6] == "_LAY_PATT")
                {
                    turn_on_layer = "true";
                    foreach (var item_layer in C_variables.LAYER_PATTERNS_list)
                    {
                        if (Int32.Parse(item_layer[3]) > nlayer_max)
                            nlayer_max = Int32.Parse(item_layer[3]);
                    }
                    //string input = "1";
                    //nlayer = int.Parse(input);
                    //if (ShowInputDialog(ref input, "Element layer? <=" + nlayer_max) == DialogResult.OK)
                    //{
                    //nlayer = int.Parse(input);
                    //if (nlayer < 1)
                    //    nlayer = 0;
                    //if (nlayer > nlayer_max)
                    //    nlayer = nlayer_max;
                    break;
                    //}
                }
            }


            /////number of combinations
            int LAST_CONVERGED_COMBINATION = 0;
            int First_CONVERGED_COMBINATION = 1;

            if (!File.Exists(directory + file_name + "_lcc.aux"))
            {
                MessageBox.Show("First, analyze the project!"); return;
            }

            string[] lines = System.IO.File.ReadAllLines(directory + file_name + "_lcc.aux");
            foreach (string line in lines)
            {
                string[] words = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Count() > 2)
                {
                    if (words[0] == "LAST_CONVERGED_COMBINATION")
                    {
                        LAST_CONVERGED_COMBINATION = int.Parse(words[2]);
                    }
                }
            }
            string combination = "all";

            //ShowInputDialog(ref combination, $"Combination (<={LAST_CONVERGED_COMBINATION})?");
            ///// end n comb



            Form output_setting = new Femix_Forms.output_setting();
            Femix_Forms.output_setting.input_param[0] = turn_on_layer;//haslayer
            Femix_Forms.output_setting.input_param[1] = nlayer_max.ToString();//maxlayer
            Femix_Forms.output_setting.input_param[2] = LAST_CONVERGED_COMBINATION.ToString();//maxcombo

            output_setting.ShowDialog();
            //runposfemix
            if (Femix_Forms.output_setting.output_param[0] == "false")
                return;


            string[] folders_to_delet = { "dipva", "sepva", "sapva", "cps3d" };
            foreach (string folder in folders_to_delet)
                if (Directory.Exists(directory + folder)) Directory.Delete(directory + folder, true);



            string ngroup, out_disp, out_stress, out_strain, out_crack;
            nlayer = Int32.Parse(Femix_Forms.output_setting.output_param[1]); //nlayer
            combination = Femix_Forms.output_setting.output_param[2];//ncombo
            ngroup = Femix_Forms.output_setting.output_param[3];//ngroup
            out_disp = Femix_Forms.output_setting.output_param[4];//dis
            out_stress = Femix_Forms.output_setting.output_param[5];//stress
            out_strain = Femix_Forms.output_setting.output_param[6];//strain
            out_crack = Femix_Forms.output_setting.output_param[7];//crack
            //"Checked"  "Unchecked"
            if (combination != "all")
            {
                //if (int.Parse(combination) > LAST_CONVERGED_COMBINATION)
                //    combination = LAST_CONVERGED_COMBINATION.ToString();
                //if (int.Parse(combination) < First_CONVERGED_COMBINATION)
                //    combination = First_CONVERGED_COMBINATION.ToString();
                LAST_CONVERGED_COMBINATION = int.Parse(combination);
                First_CONVERGED_COMBINATION = int.Parse(combination);
            }




            string output_type = "";

            if (out_disp == "Checked")
            {
                // dipva - write the displacements in the files jobname_..._di.pva
                output_type = "dipva";

                batfiledata.Add($"(echo {output_type}    &    echo {combination}    &    echo end )" + " | \"" + appdirectory + @"FemixExe\Posfemix.exe" + "\" " + $"\"{filepathtorun}\"");


                batfiledata.Add($"mkdir \"{directory}dipva\"");
                batfiledata.Add($"MOVE \"{directory}*_di.pva\" \"{directory}dipva\\\"");
                //batfiledata.Add($"for /r \"{directory}\" %%x in (*_di.pva) do move \"%%x\" \"{directory}dipva\\\""); // /r include files in subdirectories
            }




            if (out_crack == "Checked")
            {
                foreach (var item_group in C_variables.GROUP_NAMES_list)
                {
                    if (ngroup != "all")
                        if (ngroup != item_group[1])
                            continue;

                    bool group_is_used = false;
                    bool group_has_layer = false;
                    foreach (var item_elemntprop in C_variables.ELEMENT_PROPERTIES_list)
                    {
                        if (item_group[2] == item_elemntprop[2])
                            group_is_used = true;
                        if (item_elemntprop[6] == "_LAY_PATT")
                            group_has_layer = true;
                        //break;
                    }

                    string layer_string = "";
                    if (group_has_layer)
                        layer_string = $"   &   echo {nlayer}";
                    output_type = "cps3d";
                    if (group_is_used)
                    {
                        for (int ncombo = First_CONVERGED_COMBINATION; ncombo <= LAST_CONVERGED_COMBINATION; ncombo++)
                        {
                            batfiledata.Add($"(echo {output_type}    &    echo {ncombo}   &  echo {item_group[1]}  {layer_string} & echo all  & echo n & echo 1  & echo 1  & echo n &  echo end )" + " | \"" + appdirectory + @"FemixExe\Posfemix.exe" + "\" " + $"\"{filepathtorun}\"");
                            batfiledata.Add($"mkdir \"{directory}cps3d\\{item_group[2]}\"");
                            batfiledata.Add($"MOVE \"{directory}*_cp.s3d\" \"{directory}cps3d\\{item_group[2]}\\\"");
                            batfiledata.Add($"MOVE \"{directory}*_cp.cfg\" \"{directory}cps3d\\{item_group[2]}\\\"");
                        }
                    }
                }

                //batfiledata.Add($"mkdir \"{directory}cps3d\"");
                //batfiledata.Add($"MOVE \"{directory}*_cp.s3d\" \"{directory}cps3d\\\"");
                //batfiledata.Add($"MOVE \"{directory}*_cp.cfg\" \"{directory}cps3d\\\"");

            }


            if (out_stress == "Checked")
            {
                foreach (var item_group in C_variables.GROUP_NAMES_list)
                {

                    if (ngroup != "all")
                        if (ngroup != item_group[1])
                            continue;

                    bool group_is_used = false;
                    bool group_has_layer = false;
                    foreach (var item_elemntprop in C_variables.ELEMENT_PROPERTIES_list)
                    {
                        if (item_group[2] == item_elemntprop[2])
                            group_is_used = true;
                        if (item_elemntprop[6] == "_LAY_PATT")
                            group_has_layer = true;
                        //break;
                    }

                    string layer_string = "";
                    if (group_has_layer)
                        layer_string = $"   &   echo {nlayer}";
                    output_type = "sepva";
                    if (group_is_used)
                    {
                        batfiledata.Add($"(echo {output_type}  & echo y  &    echo {combination} &  echo y &  echo n  &  echo {item_group[1]}  {layer_string} &  echo end )" + " | \"" + appdirectory + @"FemixExe\Posfemix.exe" + "\" " + $"\"{filepathtorun}\"");
                        batfiledata.Add($"mkdir \"{directory}sepva\\{item_group[2]}\"");
                        batfiledata.Add($"MOVE \"{directory}*_se.pva\" \"{directory}sepva\\{item_group[2]}\\\"");
                    }
                }
            }


            if (out_strain == "Checked")
            {
                foreach (var item_group in C_variables.GROUP_NAMES_list)
                {
                    if (ngroup != "all")
                        if (ngroup != item_group[1])
                            continue;

                    bool group_is_used = false;
                    bool group_has_layer = false;
                    foreach (var item_elemntprop in C_variables.ELEMENT_PROPERTIES_list)
                    {
                        if (item_group[2] == item_elemntprop[2])
                            group_is_used = true;
                        if (item_elemntprop[6] == "_LAY_PATT")
                            group_has_layer = true;
                        //break;
                    }

                    string layer_string = "";
                    if (group_has_layer)
                        layer_string = $"   &   echo {nlayer}";
                    output_type = "sapva";
                    if (group_is_used)
                    {
                        //without smothing
                        batfiledata.Add($"(echo {output_type}  & echo y  &    echo {combination} &  echo y &  echo n  &  echo {item_group[1]}  {layer_string} &  echo end )" + " | \"" + appdirectory + @"FemixExe\Posfemix.exe" + "\" " + $"\"{filepathtorun}\"");
                        batfiledata.Add($"mkdir \"{directory}sapva\\{item_group[2]}\"");
                        batfiledata.Add($"MOVE \"{directory}*_sa.pva\" \"{directory}sapva\\{item_group[2]}\\\"");
                    }
                }

            }



            System.IO.File.WriteAllLines(batFileName, batfiledata);
            femix_log.ExecuteCommand($"\"{batFileName}\"", false, false);
            File.Delete(batFileName);


            ////combine stress/strain groups

            string filesubdirectory = "";
            string[] fileExtension_array = new string[] { "_se.pva", "_sa.pva" };
            foreach (string fileExtension in fileExtension_array)
            {
                if (fileExtension == "_se.pva") { filesubdirectory = "sepva\\"; }
                if (fileExtension == "_sa.pva") { filesubdirectory = "sapva\\"; }

                string[] element_range;
                int startline, endline, n_node_s, n_node_e;

                foreach (var item_elemntprop in C_variables.ELEMENT_PROPERTIES_list)
                {

                    if (item_elemntprop[1] == "1" || ngroup != "all")//create the files
                    {
                        if (Directory.Exists(directory + filesubdirectory + item_elemntprop[2]))
                            foreach (string file in Directory.GetFiles(directory + filesubdirectory + item_elemntprop[2], file_name + "*" + fileExtension).OrderBy(d => new FileInfo(d).CreationTime))
                                System.IO.File.Copy(file, directory + filesubdirectory + Path.GetFileName(file), true);
                    }



                    element_range = item_elemntprop[4].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                    if (element_range.Count() == 1)
                    {
                        startline = Int32.Parse(element_range[0]);
                        endline = startline;
                    }
                    else
                    {
                        startline = Int32.Parse(element_range[0]);
                        endline = Int32.Parse(element_range[1]);
                    }

                    n_node_s = 0;
                    for (int i = 1; i <= startline - 1; i++)
                    {
                        n_node_s += Int32.Parse(C_variables.ELEMENT_NODES_list[startline - 1][2]);
                    }
                    n_node_e = n_node_s;
                    n_node_s += 1;
                    for (int i = startline; i <= endline; i++)
                    {
                        n_node_e += Int32.Parse(C_variables.ELEMENT_NODES_list[startline - 1][2]);
                    }
                    //n_node_s = Int32.Parse( C_variables.ELEMENT_NODES_list[startline-1][2]);

                    startline = n_node_s;
                    endline = n_node_e;

                    if (Directory.Exists(directory + filesubdirectory + item_elemntprop[2]))
                    {
                        foreach (string file in Directory.GetFiles(directory + filesubdirectory + item_elemntprop[2], file_name + "*" + fileExtension))
                        {
                            string[] lines_source = System.IO.File.ReadAllLines(file);
                            string[] lines_target = System.IO.File.ReadAllLines(directory + filesubdirectory + Path.GetFileName(file));

                            for (int i = startline; i <= endline; i++)
                            {
                                //targetlistdata.Add(lines_source[i-1]);
                                lines_target[i - 1] = lines_source[i - 1];
                            }
                            System.IO.File.WriteAllLines(directory + filesubdirectory + Path.GetFileName(file), lines_target);
                        }
                    }
                }
            }
            ////end combine stress groups




            tabControl_left.SelectedIndex = 3;

            combo_sub_result.SelectedIndex = -1;
            combo_step.SelectedIndex = -1;




            //string fileExtension = "*_di.pva";
            //string[] txtFiles = Directory.GetFiles(directory, fileExtension);
            //foreach (var item in txtFiles)
            //    File.Move(item, Path.Combine(directory+ "dipva\\", Path.GetFileName(item)));



        }

        private void combo_sections_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_item = combo_sections.SelectedItem.ToString();
            frm_list_items formlist = new frm_list_items();


            switch (selected_item)
            {
                case "MAIN_PARAMETERS":
                    formlist.selected_item = "MAIN_PARAMETERS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "GROUP_NAMES":
                    formlist.selected_item = "GROUP_NAMES";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "POINT_COORDINATES":
                    formlist.selected_item = "POINT_COORDINATES";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "AUXILIARY_VECTORS":
                    formlist.selected_item = "AUXILIARY_VECTORS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "COORDINATE_SYSTEMS":
                    formlist.selected_item = "COORDINATE_SYSTEMS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "POINTS_WITH_SPECIAL_COORDINATE_SYSTEM":
                    formlist.selected_item = "POINTS_WITH_SPECIAL_COORDINATE_SYSTEM";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "SUPPORTS":
                    formlist.selected_item = "SUPPORTS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "INTERNAL_POINT_LINKS":
                    formlist.selected_item = "INTERNAL_POINT_LINKS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LINEAR_ISOTROPIC_MATERIALS":
                    formlist.selected_item = "LINEAR_ISOTROPIC_MATERIALS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LINEAR_ORTHOTROPIC_MATERIALS":
                    formlist.selected_item = "LINEAR_ORTHOTROPIC_MATERIALS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LINEAR_INTERFACE_POINT_PROPERTIES":
                    formlist.selected_item = "LINEAR_INTERFACE_POINT_PROPERTIES";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LINEAR_INTERFACE_LINE_PROPERTIES":
                    formlist.selected_item = "LINEAR_INTERFACE_LINE_PROPERTIES";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LINEAR_INTERFACE_SURFACE_PROPERTIES":
                    formlist.selected_item = "LINEAR_INTERFACE_SURFACE_PROPERTIES";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM101":
                    formlist.selected_item = "NLMM101";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM104":
                    formlist.selected_item = "NLMM104";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM111":
                    formlist.selected_item = "NLMM111";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM141":
                    formlist.selected_item = "NLMM141";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM151":
                    formlist.selected_item = "NLMM151";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM201":
                    formlist.selected_item = "NLMM201";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM202":
                    formlist.selected_item = "NLMM202";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM210":
                    formlist.selected_item = "NLMM210";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM220":
                    formlist.selected_item = "NLMM220";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM298":
                    formlist.selected_item = "NLMM298";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM299":
                    formlist.selected_item = "NLMM299";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM123":
                    formlist.selected_item = "NLMM123";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM121":
                    formlist.selected_item = "NLMM121";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM124":
                    formlist.selected_item = "NLMM124";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM125":
                    formlist.selected_item = "NLMM125";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM142":
                    formlist.selected_item = "NLMM142";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM145":
                    formlist.selected_item = "NLMM145";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NLMM211":
                    formlist.selected_item = "NLMM211";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;
                case "NLMM102":
                    formlist.selected_item = "NLMM102";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;
                case "NLMM122":
                    formlist.selected_item = "NLMM122";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;
                case "NLMM401":
                    formlist.selected_item = "NLMM401";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;
                case "NLMM451":
                    formlist.selected_item = "NLMM451";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;
                case "NLMM301":
                    formlist.selected_item = "NLMM301";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;
                case "NLMM302":
                    formlist.selected_item = "NLMM302";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;
                case "NLMM303":
                    formlist.selected_item = "NLMM303";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;
                case "NLMM305":
                    formlist.selected_item = "NLMM305";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;
                case "NLMM306":
                    formlist.selected_item = "NLMM306";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;
                case "NLMM320":
                    formlist.selected_item = "NLMM320";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "CROSS_SECTION_AREAS":
                    formlist.selected_item = "CROSS_SECTION_AREAS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "FRAME_2D_CROSS_SECTIONS":
                    formlist.selected_item = "FRAME_2D_CROSS_SECTIONS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "FRAME_3D_CROSS_SECTIONS":
                    formlist.selected_item = "FRAME_3D_CROSS_SECTIONS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LAMINATE_THICKNESSES":
                    formlist.selected_item = "LAMINATE_THICKNESSES";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "GEOMETRY_PATTERNS":
                    formlist.selected_item = "GEOMETRY_PATTERNS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "FIBROUS_PATTERNS":
                    formlist.selected_item = "FIBROUS_PATTERNS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LAYER_PROPERTIES":
                    formlist.selected_item = "LAYER_PROPERTIES";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LAYER_PATTERNS":
                    formlist.selected_item = "LAYER_PATTERNS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "NUMERICAL_INTEGRATION":
                    formlist.selected_item = "NUMERICAL_INTEGRATION";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "ELEMENT_NODES":
                    formlist.selected_item = "ELEMENT_NODES";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "ELEMENT_PROPERTIES":
                    formlist.selected_item = "ELEMENT_PROPERTIES";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LINEAR_SPRING_MATERIALS":
                    formlist.selected_item = "LINEAR_SPRING_MATERIALS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "POINT_SPRINGS":
                    formlist.selected_item = "POINT_SPRINGS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LINE_SPRINGS":
                    formlist.selected_item = "LINE_SPRINGS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "SURFACE_SPRINGS":
                    formlist.selected_item = "SURFACE_SPRINGS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "STIFFENERS":
                    formlist.selected_item = "STIFFENERS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LOAD_CASE_PARAMETERS":
                    formlist.selected_item = "LOAD_CASE_PARAMETERS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "GRAVITY_LOAD":
                    formlist.selected_item = "GRAVITY_LOAD";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "POINT_LOADS":
                    formlist.selected_item = "POINT_LOADS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "INTERNAL_POINT_LOADS":
                    formlist.selected_item = "INTERNAL_POINT_LOADS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "EDGE_LOADS":
                    formlist.selected_item = "EDGE_LOADS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "INTERNAL_EDGE_LOADS":
                    formlist.selected_item = "INTERNAL_EDGE_LOADS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "FACE_LOADS":
                    formlist.selected_item = "FACE_LOADS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "INTERNAL_FACE_LOADS":
                    formlist.selected_item = "INTERNAL_FACE_LOADS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "TEMPERATURE_VARIATION":
                    formlist.selected_item = "TEMPERATURE_VARIATION";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "DIFFERENTIAL_TEMPERATURE_VARIATION":
                    formlist.selected_item = "DIFFERENTIAL_TEMPERATURE_VARIATION";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "PRESCRIBED_DISPLACEMENTS":
                    formlist.selected_item = "PRESCRIBED_DISPLACEMENTS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS":
                    formlist.selected_item = "INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                case "LOAD_CASE_COMBINATIONS":
                    formlist.selected_item = "LOAD_CASE_COMBINATIONS";
                    formlist.ShowDialog();
                    if (formlist.is_there_change == true)
                        reload_textbox_and_tree(C_variables.Femix_string);
                    break;

                default:
                    break;
            }

            if (formlist.is_there_change == true)
                draw_objects();
        }

        private void reload_textbox_and_tree(string femix_string)
        {
            femix_toolstip.Enabled = false;
            postfemix_toolstrip.Enabled = false;
            toolStripclean.Enabled = false;

            Femixbox1.Text = femix_string;
            tree_femix_fill();
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Femixbox1_Leave(object sender, EventArgs e)
        {

            C_variables.Femix_string = Femixbox1.Text;
            Class_femix_reader.extract_femix(femix_filepath, C_variables.Femix_string);
            femix_toolstip.Enabled = false;
            postfemix_toolstrip.Enabled = false;
            toolStripclean.Enabled = false;

            tree_femix_fill();
            draw_objects();

        }

        private void tree_femix_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {


        }
        public static void displayErrorBox(string error_message)
        {
            MessageBox.Show(error_message, "Error");
        }
        public void LogBox_add(string _text_to_add)
        {
            LogBox.AppendText(_text_to_add + Environment.NewLine);
        }
        private void toolStripclean_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete the results?", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {

                string path = Path.GetDirectoryName(femix_filepath) + @"\";
                string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
                file_name = file_name.Remove(file_name.Length - 3);

                string[] extensions = { ".bin","_ctrl.dat","_lcc.aux","_ac.csv","_cp.s3d","_cs.s3d","_di.csv","_di.pva","_dm.s3d","_ep.csv"
            ,"_ep.lpt","_ev.s3d","_gl.lpt","_hd.csv","_if.csv","_ip.s3d","_me.s3d","_pz.s3d","_rd.lpt","_rd.pva","_re.csv",".lpt","_sa.pva","_se.pva","_sr.pva","_ve.csv","_op.pva","_te.pva",
             "_te.csv","_sd.csv","_tm.lpt","_gf.pva","_cr.pva","_cr.lpt","_cf.csv","_hp.pva","_ps.s3d","_um.s3d","_cp.cfg","_cs.cfg","_pos.dat","_femix.geo","_femix.msh"};
                foreach (string ext in extensions)
                    foreach (string file in Directory.GetFiles(path, file_name + "*" + ext).Where(item => item.EndsWith(ext)))
                        File.Delete(file);


                string[] folders_to_delet = { "dipva" , "sepva","sapva","cps3d"};
                foreach (string folder in folders_to_delet)
                    if (Directory.Exists(path+folder)) Directory.Delete(path+folder, true);

                MessageBox.Show("The project folder cleared successfully!", "Done");
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }



        private void toolStrip_view_xy_Click(object sender, EventArgs e)
        {
            KbRen.SetViewOri("XY");
        }

        private void toolStrip_view_YX_Click(object sender, EventArgs e)
        {
            KbRen.SetViewOri("YX");
        }

        private void toolStrip_view_YZ_Click(object sender, EventArgs e)
        {
            KbRen.SetViewOri("YZ");
        }

        private void toolStrip_view_ZY_Click(object sender, EventArgs e)
        {
            KbRen.SetViewOri("ZY");
        }

        private void toolStrip_view_XZ_Click(object sender, EventArgs e)
        {
            KbRen.SetViewOri("XZ");
        }

        private void toolStrip_view_ZX_Click(object sender, EventArgs e)
        {
            KbRen.SetViewOri("ZX");
        }

        private void toolStrip_view_ISO_Click(object sender, EventArgs e)
        {
            KbRen.SetViewOri("ISO");
        }

        private void toolStrip_view_fit_Click(object sender, EventArgs e)
        {
            KbRen.FitView();
        }

        private void toolStrip_perspective_off_Click(object sender, EventArgs e)
        {
            KbRen.Perspective("off");
        }

        private void toolStrip_perspective_on_Click(object sender, EventArgs e)
        {
            KbRen.Perspective("on");
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About Aboutfrm = new About();
            Aboutfrm.ShowDialog();
        }
        private void DG_Layers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void DG_Layers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            //if (DG_Layers.Rows.Count > C_variables.GROUP_NAMES_n+C_variables.LOAD_CASE_PARAMETERS_n+1 + 1)
            //{
            //Console.WriteLine(DG_Layers.Columns[e.ColumnIndex].Index);
            try
            {
                if (DG_Layers.CurrentCell != null && DG_Layers.Rows.Count > 1)
                {
                    int selected_row_index = DG_Layers.CurrentCell.RowIndex;
                    string selected_layer_name = DG_Layers.Rows[selected_row_index].Cells["col_name1"].Value.ToString();
                    switch (selected_row_index)
                    {
                        case 1://Label Nodes
                            if (DG_Layers.Rows[selected_row_index].Cells["col_visible"].Value.ToString() == "true")
                            {
                                foreach (int actor_index in C_variables.Layer_items["Nodes"])
                                    KbRen.labelactors[actor_index].VisibilityOn();
                            }
                            else
                            {
                                foreach (int actor_index in C_variables.Layer_items["Nodes"])
                                    KbRen.labelactors[actor_index].VisibilityOff();
                            }
                            KbRen.Refresh();
                            break;
                        case 2://Label elements
                            if (DG_Layers.Rows[selected_row_index].Cells["col_visible"].Value.ToString() == "true")
                            {
                                for (int i = 3; i < 3 + C_variables.GROUP_NAMES_list.Count(); i++)
                                {
                                    if (DG_Layers.Rows[i].Cells["col_visible"].Value.ToString().ToLower() == "true")
                                    {
                                        foreach (int actor_index in C_variables.Layer_items[DG_Layers.Rows[i].Cells["col_name1"].Value.ToString()])
                                            KbRen.labelactors[actor_index].VisibilityOn();
                                    }
                                }
                            }
                            else
                            {
                                foreach (int actor_index in C_variables.Layer_items["1D"])
                                    KbRen.labelactors[actor_index].VisibilityOff();
                                foreach (int actor_index in C_variables.Layer_items["2D"])
                                    KbRen.labelactors[actor_index].VisibilityOff();
                                foreach (int actor_index in C_variables.Layer_items["3D"])
                                    KbRen.labelactors[actor_index].VisibilityOff();
                            }

                            KbRen.Refresh();
                            break;
                        default:
                            if (DG_Layers.Rows[selected_row_index].Cells["col_visible"].Value.ToString() == "true")
                            {
                                foreach (int actor_index in C_variables.Layer_items[selected_layer_name])
                                {
                                    KbRen.actors[actor_index].VisibilityOn();
                                    KbRen.EdgeActors[actor_index].VisibilityOn();
                                    if (DG_Layers.Rows[2].Cells["col_visible"].Value.ToString() == "true" && selected_row_index > 0)
                                        KbRen.labelactors[actor_index].VisibilityOn();
                                }

                            }
                            else
                            {
                                foreach (int actor_index in C_variables.Layer_items[selected_layer_name])
                                {
                                    KbRen.actors[actor_index].VisibilityOff();
                                    KbRen.EdgeActors[actor_index].VisibilityOff();
                                    KbRen.labelactors[actor_index].VisibilityOff();
                                }
                            }
                            KbRen.Refresh();
                            break;
                    }

                }
                //}
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }

        }
        public void draw_objects()
        {

            reload_dg_layer();

            KbRen.Draw_objects(C_variables.POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.ELEMENT_NODES_list,"true");

            KbRen.Draw_loads_supports(C_variables.POINT_LOADS_list, C_variables.PRESCRIBED_DISPLACEMENTS_list, C_variables.SUPPORTS_list, C_variables.support_size_factor / 25);//100

            KbRen.Renderer.ResetCamera();
            KbRen.Refresh();
        }

        private void reload_dg_layer()
        {
            C_variables.Layer_items.Clear();
            C_variables.Layer_items.Add("Nodes", new List<int> { });
            C_variables.Layer_items.Add("1D", new List<int> { });
            C_variables.Layer_items.Add("2D", new List<int> { });
            C_variables.Layer_items.Add("3D", new List<int> { });
            C_variables.Layer_items.Add("Supports", new List<int> { });
            //cracks
            C_variables.Layer_items.Add("_INITIATING", new List<int> { });
            C_variables.Layer_items.Add("_OPENING", new List<int> { });
            C_variables.Layer_items.Add("_CLOSING", new List<int> { });
            C_variables.Layer_items.Add("_CLOSED", new List<int> { });
            C_variables.Layer_items.Add("_REOPENING", new List<int> { });
            C_variables.Layer_items.Add("_FULLY_OPEN", new List<int> { });

            foreach (var item in C_variables.LOAD_CASE_PARAMETERS_list)
                if (!C_variables.Layer_items.ContainsKey(item[2]))
                    C_variables.Layer_items.Add(item[2], new List<int> { });

            //C_variables.Layer_items.Add("Label_Nodes", new List<int> { });
            //C_variables.Layer_items.Add("Label_Lines", new List<int> { });
            //C_variables.Layer_items.Add("Label_Surfaces", new List<int> { });
            //C_variables.Layer_items.Add("Label_Volumes", new List<int> { });

            foreach (var item in C_variables.GROUP_NAMES_list)
                C_variables.Layer_items.Add(item[2], new List<int> { });




            int row_index = 0;
            DG_Layers.Rows.Clear();
            DG_Layers.Rows.Add();
            DG_Layers.Rows[0].Cells["col_name1"].Value = "Nodes";
            DG_Layers.Rows[0].Cells["col_visible"].Value = false;

            row_index++;
            DG_Layers.Rows.Add();
            DG_Layers.Rows[row_index].Cells["col_name1"].Value = "Label Nodes";
            DG_Layers.Rows[row_index].Cells["col_visible"].Value = false;
            row_index++;
            DG_Layers.Rows.Add();
            DG_Layers.Rows[row_index].Cells["col_name1"].Value = "Label Elements";
            DG_Layers.Rows[row_index].Cells["col_visible"].Value = false;


            foreach (var item in C_variables.GROUP_NAMES_list)
            {
                row_index++;
                DG_Layers.Rows.Add();
                DG_Layers.Rows[row_index].Cells["col_name1"].Value = item[2];
                DG_Layers.Rows[row_index].Cells["col_visible"].Value = true;
            }

            row_index++;
            DG_Layers.Rows.Add();
            DG_Layers.Rows[row_index].Cells["col_name1"].Value = "Supports";
            DG_Layers.Rows[row_index].Cells["col_visible"].Value = true;

            foreach (var item in C_variables.LOAD_CASE_PARAMETERS_list)
            {
                row_index++;
                DG_Layers.Rows.Add();
                DG_Layers.Rows[row_index].Cells["col_name1"].Value = item[2];
                DG_Layers.Rows[row_index].Cells["col_visible"].Value = true;
            }

        }

        List<string> path_to_output;
 
        private void scan_folder_for_load_case(string femix_filepath, string result, string sub_result)
        {
            string path = Path.GetDirectoryName(femix_filepath) + @"\";
            string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            file_name = file_name.Remove(file_name.Length - 3);

            path_to_output = new List<string>();
            string extensions = "";
            string file_ext = "";
            string loadcase = "";
            combo_step.Items.Clear();


            switch (combo_results.SelectedItem.ToString())
            {
                case "Displacement":
                    file_ext = "_" + sub_result + "_di.pva";
                    extensions = "_di.pva";
                    if (Directory.Exists(path+"dipva"))
                        path += @"dipva\";
                    break;
                case "Stress":
                    file_ext = "_" + sub_result + "_se.pva";
                    extensions = "_se.pva";
                    if (Directory.Exists(path + "sepva"))
                        path += @"sepva\";
                    break;
                case "Strains":
                    file_ext = "_" + sub_result + "_sa.pva";
                    extensions = "_sa.pva";
                    if (Directory.Exists(path + "sapva"))
                        path += @"sapva\";
                    break;
                case "Crack pattern":
                    file_ext = "_gr*" + (combo_sub_result.SelectedIndex+1).ToString() +"_*"+ "cp.s3d";
                    extensions = "_cp.s3d";
                    if (Directory.Exists(path + @"cps3d\"))
                        path += @"cps3d\";
                    if (Directory.Exists(path + $"{combo_sub_result.Text}"))
                        path += $"{combo_sub_result.Text}\\";
                    break;
            }


            //foreach (string file in Directory.GetFiles(path, file_name + "*"+ extensions).OrderBy(d => new FileInfo(d).CreationTime).Where(item => item.EndsWith(extensions)))
            foreach (string file in Directory.GetFiles(path, file_name + "*"+ file_ext).OrderBy(d => new FileInfo(d).CreationTime).Where(item => item.EndsWith(extensions)))
            {
                path_to_output.Add(file);
                //loadcase = (Path.GetFileNameWithoutExtension(file.Remove(file.Length - (file_ext.Length)))).Remove(0, file_name.Length + 1);

                string[] loadcase_arr = file.Split(new string[] {"_"}, StringSplitOptions.RemoveEmptyEntries);

                bool isNumberOccurance1 = loadcase_arr[1].Any(letter => char.IsDigit(letter));
                bool isNumberOccurance2 = loadcase_arr[2].Any(letter => char.IsDigit(letter));
                if (isNumberOccurance2) { loadcase = loadcase_arr[2];}
                if (isNumberOccurance1) { loadcase = loadcase_arr[1];}
                loadcase = String.Concat(loadcase.Where(char.IsDigit));
                combo_step.Items.Add(Path.GetFileNameWithoutExtension(loadcase));
            }

        }

        private void combo_results_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo_sub_result.Items.Clear();
            List<string> sub_items = new List<string>();

            groupBox_deformed.Enabled = false;
            groupBox_crack.Enabled = false;
            switch (combo_results.SelectedItem.ToString())
            {
                case "Displacement":
                    sub_items = new List<string> { "D1", "D2", "D3","R1","R2","R3" };
                    groupBox_deformed.Enabled = true;
                    break;
                case "Stress":
                    sub_items = new List<string> { "Sigma1", "Sigma2", "Sigma3", "Tau23", "Tau31", "Tau12" };
                    groupBox_deformed.Enabled = true;
                    break;
                case "Strains":
                    sub_items = new List<string> { "Eps1", "Eps2", "Eps3", "Gamma23", "Gamma31", "Gamma12" };
                    groupBox_deformed.Enabled = true;
                    break;
                case "Crack pattern":
                    sub_items = new List<string> {};
                    foreach (var item in C_variables.GROUP_NAMES_list)
                    {
                        sub_items.Add(item[2]);
                    }
                    groupBox_crack.Enabled = true;
                    break;
            }


            foreach (string item in sub_items)
                combo_sub_result.Items.Add(item);

            toolStrip_undeormed.PerformClick();
            combo_sub_result.SelectedIndex = -1;
            combo_step.SelectedIndex = -1;

        }

        private void combo_sub_result_SelectedIndexChanged(object sender, EventArgs e)
        {
            scan_folder_for_load_case(femix_filepath, combo_results.Text, combo_sub_result.Text);
        }
   

        private void Load_results()
        {

            if (combo_step.SelectedIndex == -1)
            {
                MessageBox.Show("Select analysis step!!");
                return;
            }
                

            if (combo_results.SelectedItem.ToString() == "Crack pattern") { Load_results_crack();return; }


            if (toolStrip_co_deformed.Checked == true || toolStrip_co_both.Checked == true)
                Result_Fem.Create_deformed_shape_coordinates(femix_filepath, combo_step.SelectedItem.ToString(), double.Parse(txt_result_scale.Text));


            bool unconected_mesh = false;
            if (combo_results.SelectedItem.ToString()== "Stress" || combo_results.SelectedItem.ToString() == "Strains")
            {
                unconected_mesh = true;
                Result_Fem.Create_unconected_mesh(KbRen);
                if (toolStrip_co_deformed.Checked == true || toolStrip_co_both.Checked == true)
                    Result_Fem.Create_unconected_deformed_mesh(KbRen);
            }
                
           
           

           

            if (toolStrip_co_undeformed.Checked == true)
                Result_Fem.draw_undeformed_shape(KbRen, unconected_mesh);

            if (toolStrip_co_deformed.Checked == true)
                Result_Fem.draw_deformed_shape(KbRen, unconected_mesh);

            if (toolStrip_co_deformed.Checked == true)
                Result_Fem.draw_deformed_shape(KbRen, unconected_mesh);

            if (toolStrip_co_both.Checked == true)
                Result_Fem.draw_both_shape(KbRen, unconected_mesh);




            //if (toolStrip_co_undeformed.Checked == true)
            //    Result_Fem.draw_undeformed_shape(KbRen);


            //if (combo_step.SelectedIndex>=0)
            //       MessageBox.Show(path_to_output[combo_step.SelectedIndex]);
            string outputFile = path_to_output[combo_step.SelectedIndex];
            //int TotalLines = File.ReadLines(outputFile).Count(); 
            List<IList<double>> results = new List<IList<double>>();
            double Min_val, Max_val;
            Min_val = 0.0;
            Max_val = 0.0;
            string[] lines = System.IO.File.ReadAllLines(outputFile);
            int row = 0;
            foreach (string line in lines)
            {
                string[] words = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                results.Add(new List<double> { row, double.Parse(words[0]), double.Parse(words[1]) });
                if (row == 0)
                {
                    Min_val = double.Parse(words[1]);
                    Max_val = double.Parse(words[1]);
                }
                else
                {
                    Min_val = Math.Min(Min_val, double.Parse(words[1]));
                    Max_val = Math.Max(Max_val, double.Parse(words[1]));
                }
                row++;
            }

            bool in_numeric=false;
            double number_out;
            in_numeric = double.TryParse(txt_min_contour.Text, out number_out);
            if (in_numeric)
                Min_val = number_out;
            in_numeric = false;
            in_numeric = double.TryParse(txt_max_contour.Text, out number_out);
            if (in_numeric)
                Max_val = number_out;

            KbRen.set_results_point_data(results.Count, results, Min_val, Max_val, $"{combo_results.Text} \n({combo_sub_result.Text})[{combo_step.Text}]", C_variables.Val(txt_number_color.Text), toolStrip_Discretize.Checked);//5

        }

        private void Load_results_crack()
        {
            DG_Layers.Rows.Clear();
            int row_index = 0;
            DG_Layers.Rows.Clear();
            DG_Layers.Rows.Add();
            DG_Layers.Rows[0].Cells["col_name1"].Value = "Nodes";
            DG_Layers.Rows[0].Cells["col_visible"].Value = false;
            //DG_Layers.Rows[0].Cells["col_color"].Style.BackColor = Color.Yellow;
            //DG_Layers.Rows[0].Cells["col_color"].Style.SelectionBackColor = Color.Yellow;

            row_index++;
            DG_Layers.Rows.Add();
            DG_Layers.Rows[row_index].Cells["col_name1"].Value = "Label Nodes";
            DG_Layers.Rows[row_index].Cells["col_visible"].Value = false;
            row_index++;
            DG_Layers.Rows.Add();
            DG_Layers.Rows[row_index].Cells["col_name1"].Value = "Label Elements";
            DG_Layers.Rows[row_index].Cells["col_visible"].Value = false;

            foreach (var item in C_variables.GROUP_NAMES_list)
            {
                row_index++;
                DG_Layers.Rows.Add();
                DG_Layers.Rows[row_index].Cells["col_name1"].Value = item[2];
                DG_Layers.Rows[row_index].Cells["col_visible"].Value = true;
            }

            DG_Layers.Rows.Add();
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_name1"].Value = "_INITIATING";
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_visible"].Value = true;
            DG_Layers.Rows.Add();
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_name1"].Value = "_OPENING";
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_visible"].Value = true;
            DG_Layers.Rows.Add();
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_name1"].Value = "_CLOSING";
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_visible"].Value = true;
            DG_Layers.Rows.Add();
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_name1"].Value = "_CLOSED";
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_visible"].Value = true;
            DG_Layers.Rows.Add();
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_name1"].Value = "_REOPENING";
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_visible"].Value = true;
            DG_Layers.Rows.Add();
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_name1"].Value = "_FULLY_OPEN";
            DG_Layers.Rows[DG_Layers.Rows.Count - 1].Cells["col_visible"].Value = true;

            C_variables.Layer_items["_INITIATING"].Clear();
            C_variables.Layer_items["_OPENING"].Clear();
            C_variables.Layer_items["_CLOSING"].Clear();
            C_variables.Layer_items["_CLOSED"].Clear();
            C_variables.Layer_items["_REOPENING"].Clear();
            C_variables.Layer_items["_FULLY_OPEN"].Clear();


            KbRen.Draw_objects(C_variables.POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.ELEMENT_NODES_list, "true");


            for (int i = 0; i < KbRen.actors.Count; i++)
            {
                KbRen.actors[i].GetProperty().SetColor(235, 235, 235);
                //KbRen.EdgeActors[i].GetProperty().SetColor(0,0,0);
            }
            //KbRen.Refresh();

            //KbRen.Renderer.RemoveAllViewProps();

            List<IList<string>> Point_list; List<IList<string>> ELEMENT_NODES; string crack_type;
            crack_type = "";

            string outputFile = path_to_output[combo_step.SelectedIndex]; // @"C:\Users\Kamyar\Desktop\qqq\tempblock\block_Comb100_gr02_la10_cp.s3d";
            //int TotalLines = File.ReadLines(outputFile).Count();

            string[] lines = System.IO.File.ReadAllLines(outputFile);
            int start_node, start_element, end_node, end_element;
            //int row = 0;
            for (int i = 0; i < lines.Count(); i++)
            {
                string[] words = lines[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Count() > 0)
                {
                    if (words[0] == "Combination:")
                    {
                        for (int i1 = 0; i1 <= words.Count(); i1++)
                        {
                            if (words[i1] == "status:")
                            {
                                crack_type = words[i1 + 1];
                                break;
                            }

                        }

                        string[] words_next_line = lines[i + 1].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (words_next_line[0] == "0")
                        {
                            start_node = 0; start_element = 0; end_node = 0; end_element = 0;
                            continue;
                        }
                        start_element = i + 2;
                        end_element = start_element + Int32.Parse(words_next_line[0]) - 1;
                        start_node = end_element + 1;
                        end_node = start_node + Int32.Parse(words_next_line[1]) - 1;
                        //i = Int32.Parse(words_next_line[0]) + Int32.Parse(words_next_line[1]) + Int32.Parse(words_next_line[2]);

                        //row = 0;
                        ELEMENT_NODES = new List<IList<string>>();
                        for (int i2 = start_element; i2 <= end_element; i2++)
                        {
                            string[] words_t = lines[i2].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            ELEMENT_NODES.Add(new List<string> { words_t[0], words_t[1], words_t[2], words_t[3], words_t[4], words_t[5], words_t[6] });
                            //row++;
                        }

                        //row = 0;
                        Point_list = new List<IList<string>>();
                        for (int i2 = start_node; i2 <= end_node; i2++)
                        {
                            string[] words_t = lines[i2].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                            Point_list.Add(new List<string> { words_t[0], words_t[1], words_t[2], words_t[3] });
                            //row++;
                        }

                        KbRen.Draw_cracks(Point_list, ELEMENT_NODES, crack_type,txt_crack_w_sc.Text,txt_crack_l_sc.Text);
                    }
                }
                // results.Add(new List<double> { row, double.Parse(words[0]), double.Parse(words[1]) });
            }

            KbRen.Draw_cracks_legend();
            // KbRen.Draw_cracks(Point_list, ELEMENT_NODES, crack_type);

            KbRen.Refresh();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Load_results();
        }

        private void toolStrip_view_mesh_Click(object sender, EventArgs e)
        {
            KbRen.mesh_view_opacity("view_mesh_edge");
        }

        private void toolStrip_hide_mesh_Click(object sender, EventArgs e)
        {
            KbRen.mesh_view_opacity("hide_mesh_edge");
        }

        bool opa_click = false;
        private void toolStripButton_opacity_Click(object sender, EventArgs e)
        {

            if (opa_click == true)
            {
                opa_click = false;
                KbRen.mesh_view_opacity("opacity100");
                toolStripButton_opacity.Checked = false;
            }
            else
            {
                opa_click = true;
                KbRen.mesh_view_opacity("opacity40");
                toolStripButton_opacity.Checked = true;

            }

        }

        private void combo_step_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_step.SelectedIndex > -1)
            {
                Load_results();
            }
            else
            {
                KbRen.clear_point_cell_data();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (combo_step.SelectedIndex < combo_step.Items.Count - 1)
                combo_step.SelectedIndex += 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (combo_step.Items.Count > 0)
                combo_step.SelectedIndex = combo_step.Items.Count - 1;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (combo_step.Items.Count > 0)
                combo_step.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (combo_step.SelectedIndex > 0)
                combo_step.SelectedIndex -= 1;
        }

        private void toolStrip_undeormed_Click(object sender, EventArgs e)
        {
            reload_dg_layer();
            
            toolStrip_co_undeformed.Checked = true;
            toolStrip_co_deformed.Checked = false;
            toolStrip_co_both.Checked = false;

            Result_Fem.draw_undeformed_shape(KbRen,false);
        }

        private void button6_Click(object sender, EventArgs e)
        {


        }

        private string creat_Gmesh_imported_segments(List<IList<string>> input_list)
        {
            string _segment = "";
            _segment = $"\nCOUNT = {input_list.Count()} ;\n";

            List<string> temp_list = new List<string>();
            List<string> row_list = new List<string>();
            int row_counter = 0;
            foreach (var row in input_list)
            {
                row_counter++;
                row_list = row.ToList();
                row_list.RemoveAt(0);
                row_list.RemoveAt(0);//remove default counter
                row_list.Insert(0,row_counter.ToString()); //add a new counter
                row_list.Add(" ;");
                temp_list.Add(string.Join("     ", row_list.ToArray()));
            }

            string _segment2 = string.Join(Environment.NewLine, temp_list.ToArray());
            _segment += _segment2;
            _segment += "\n";

            return _segment;
        }
        private void toolStrip_select_Click(object sender, EventArgs e)
        {
            if (KbRen.SelectionMode == true)
            {
                KbRen.SelectionMode = false;
                toolStrip_select.Checked = false;
                KbRen.Renderer.RemoveActor(KbRen.SelectionActor);
                KbRen.Refresh();
            }
            else
            {
                KbRen.SelectionPoints = vtkPoints.New();
                LogBox_add("Select a point?");
                KbRen.SelectionMode = true;
                toolStrip_select.Checked = true;
            }

        }



        private void LogBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LogBox_add("Cancel");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            //KbRen.SelectionPoints = vtkPoints.New();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // KbRen.SelectionPoints = vtkPoints.New();
            //  KbRen.Refresh();
        }
        private void toolStripButton_point_Click(object sender, EventArgs e)
        {
            KbRen.part_point_select = true;
            KbRen.part_cell_select = false;
            KbRen.part_hide_extrusion = true;
            KbRen.Part_draw();
            LogBox_add(Environment.NewLine + "New point: Enter Point coordinate?");
            LogBox.Focus();
        }
        private int line_start_id, line_end_id;
        private void toolStripButton_line_Click(object sender, EventArgs e)
        {
            KbRen.part_point_select = true;
            KbRen.part_cell_select = false;
            KbRen.part_hide_extrusion = true;
            KbRen.Part_draw();
            LogBox_add(Environment.NewLine + "New line: Select start point?");
            line_start_id = -1;
            line_end_id = -1;

            // LogBox.Focus();
        }

 private List<int> surface_vertices;
        private void toolStripButton_surface_Click(object sender, EventArgs e)
        {
            KbRen.part_point_select = true;
            KbRen.part_cell_select = false;
            KbRen.part_hide_extrusion = true;
            KbRen.Part_draw();
            LogBox_add(Environment.NewLine + "New surface: Select start vertice?");
        }

        private void toolStripButton_volume_Click(object sender, EventArgs e)
        {
            KbRen.part_point_select = true;
            KbRen.part_cell_select = true;
            KbRen.part_hide_extrusion = true;
            KbRen.Part_draw();
            LogBox_add(Environment.NewLine + "Delete point/line/surface: Select Point/line/surface?");
        }

        private void importGMSHModelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void importGMSHMeshToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void runGMSHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            femix_log femix_log = new femix_log();
            femix_log.run_GMSH_exe();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New_femix_toolstrip.PerformClick();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open_femix_toolstrip.PerformClick();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save_femix_toolstrip.Enabled == true)
                save_femix_toolstrip.PerformClick();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save_as_toolstrip.Enabled == true)
                save_as_toolstrip.PerformClick();
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        string delete_type = "";
        int delete_id = -1;
        private void LogBox_TextChanged(object sender, EventArgs e)
        {
            if (LogBox.Lines.Count() > 2)
            {
                string command = LogBox.Lines[LogBox.Lines.Length - 3];
                string userinput = LogBox.Lines[LogBox.Lines.Length - 2];
                string lastLine = LogBox.Lines[LogBox.Lines.Length - 1];
                string[] user_input_array;
                int selected_point_id = -1;
                bool isInteger;


                if (!userinput.ToUpper().Contains("CANCEL") && lastLine == "" && command != userinput)
                {
                    if (command == "New point: Enter Point coordinate?")
                    {
                        user_input_array = userinput.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (user_input_array.Count() < 2 | user_input_array.Count() > 3)
                        {
                            LogBox_add("Error on input, Enter point coordinates like: x,y,z or x,y");
                            LogBox_add("New point: Enter Point coordinate?");
                            return;
                        }


                        //MessageBox.Show(user_input[0] + user_input[1] + user_input[2]);
                        bool valx, valy, valz;
                        double x, y, z;
                        valx = Double.TryParse(user_input_array[0], out x);
                        valy = Double.TryParse(user_input_array[1], out y);
                        if (user_input_array.Count() == 2)
                        {
                            valz = Double.TryParse("0", out z);//z=0
                        }
                        else
                        {
                            valz = Double.TryParse(user_input_array[2], out z);
                        }


                        int id = 0;
                        foreach (var item in KbRen.part_points)
                        {
                            if (x == item[0] && y == item[1] && z == item[2])
                            {
                                LogBox_add($"Error, This coordinate exist.Id:{id} [{x},{y},{z}]");
                                LogBox_add("New point: Enter Point coordinate?");
                                return;
                            }
                            id++;
                        }

                        if (valx && valy && valz)
                        {
                            KbRen.part_points.Add(new List<double> { x, y, z });
                            KbRen.Part_draw();
                            save_data_part();
                        }
                        else
                        { LogBox_add(Environment.NewLine + "Error on input, Enter Numeric values!!"); }
                        LogBox_add("New point: Enter Point coordinate?");

                        // draw_point(user_input);
                    }

                    if (command == "New line: Select start point?")
                    {
                        user_input_array = userinput.Split(new string[] { ":", "[" }, StringSplitOptions.RemoveEmptyEntries);
                        if (user_input_array.Count() == 1)
                        {

                            isInteger = Int32.TryParse(user_input_array[0], out selected_point_id);
                            if (!isInteger)
                            {
                                LogBox_add("Error: Enter the point ID or Select a point!");
                                LogBox_add("New line: Select start point?");
                                return;
                            }
                        }
                        else if (user_input_array.Count() == 3)
                        {
                            isInteger = Int32.TryParse(user_input_array[1], out selected_point_id);
                        }
                        else
                        {
                            LogBox_add("Error: Enter the point ID or Select a point!" + Environment.NewLine + "New line: Select start point?");
                            return;
                        }

                        if (selected_point_id > KbRen.part_points.Count())
                        {
                            LogBox_add("Error: This point ID doesn't exist!" + Environment.NewLine + "New line: Select start point?");
                            return;
                        }
                        //MessageBox.Show(selected_point_id.ToString());
                        line_start_id = selected_point_id;
                        LogBox_add("New line: Select end point?");
                    }
                    if (command == "New line: Select end point?")
                    {
                        if (line_start_id == -1)
                        {
                            LogBox_add("Error: First point has not been selected!" + Environment.NewLine + "New line: Select start point?");
                            return;
                        }
                        user_input_array = userinput.Split(new string[] { ":", "[" }, StringSplitOptions.RemoveEmptyEntries);
                        if (user_input_array.Count() == 1)
                        {

                            isInteger = Int32.TryParse(user_input_array[0], out selected_point_id);
                            if (!isInteger)
                            {
                                LogBox_add("Error: Enter the point ID or Select a point!" + Environment.NewLine + "New line: Select end point?");
                                return;
                            }
                        }
                        else if (user_input_array.Count() == 3)
                        {
                            isInteger = Int32.TryParse(user_input_array[1], out selected_point_id);
                        }
                        else
                        {
                            LogBox.AppendText("Error: Enter the point ID or Select a point!" + Environment.NewLine + "New line: Select end point?");
                            return;
                        }
                        if (selected_point_id > KbRen.part_points.Count())
                        {
                            LogBox.AppendText("Error: This point ID doesn't exist!" + Environment.NewLine + "New line: Select end point?");
                            return;
                        }
                        //MessageBox.Show(selected_point_id.ToString());
                        line_end_id = selected_point_id;
                        if (line_start_id != line_end_id)
                        {
                            LogBox_add($"Line added. (p{line_start_id}-p{line_end_id})");




                            KbRen.part_lines.Add(new List<int> { line_start_id, line_end_id });
                            KbRen.Part_draw();
                            save_data_part();
                            line_start_id = line_end_id;
                        }
                        LogBox_add("New line: Select end point?");
                    }

                    if (command == "New surface: Select start vertice?")
                    {
                        user_input_array = userinput.Split(new string[] { ":", "[" }, StringSplitOptions.RemoveEmptyEntries);
                        if (user_input_array.Count() == 1)
                        {

                            isInteger = Int32.TryParse(user_input_array[0], out selected_point_id);
                            if (!isInteger)
                            {
                                LogBox_add("Error: Enter the point ID or Select a point!" + Environment.NewLine + "New surface: Select start vertice?");
                                return;
                            }
                        }
                        else if (user_input_array.Count() == 3)
                        {
                            isInteger = Int32.TryParse(user_input_array[1], out selected_point_id);
                        }
                        else
                        {
                            LogBox_add("Error: Enter the point ID or Select a point!" + Environment.NewLine + "New surface: Select start vertice?");
                            return;
                        }

                        if (selected_point_id > KbRen.part_points.Count())
                        {
                            LogBox_add("Error: This point ID doesn't exist!" + Environment.NewLine + "New surface: Select start vertice?");
                            return;
                        }
                        //MessageBox.Show(selected_point_id.ToString());

                        surface_vertices = new List<int>();

                        surface_vertices.Add(selected_point_id);
                        KbRen.part_surfaces.Add(surface_vertices);
                        //LogBox_add(selected_point_id.ToString());
                        LogBox_add("New surface: Select next vertices points?");
                    }

                    if (command == "New surface: Select next vertices points?")
                    {
                        user_input_array = userinput.Split(new string[] { ":", "[" }, StringSplitOptions.RemoveEmptyEntries);
                        if (user_input_array.Count() == 1)
                        {

                            isInteger = Int32.TryParse(user_input_array[0], out selected_point_id);
                            if (!isInteger)
                            {
                                LogBox_add("Error: Enter the point ID or Select a point!" + Environment.NewLine + "New surface: Select next vertices points?");
                                return;
                            }
                        }
                        else if (user_input_array.Count() == 3)
                        {
                            isInteger = Int32.TryParse(user_input_array[1], out selected_point_id);
                        }
                        else
                        {
                            LogBox_add("Error: Enter the point ID or Select a point!" + Environment.NewLine + "New surface: Select next vertices points?");
                            return;
                        }

                        if (selected_point_id > KbRen.part_points.Count())
                        {
                            LogBox_add("Error: This point ID doesn't exist!" + Environment.NewLine + "New surface: Select next vertices points?");
                            return;
                        }


                        bool has_intersection = false;
                        foreach (long item in surface_vertices)
                        {
                            if (selected_point_id == item)
                            {
                                has_intersection = true;
                                break;
                            }
                        }
                        if (has_intersection == false)//prevent intersection between vertices
                        {
                            surface_vertices.Add(selected_point_id);
                        }

                        //listOfStrings[listOfStrings.FindIndex(ind => ind.Equals("123"))]
                        KbRen.part_surfaces[KbRen.part_surfaces.Count - 1] = surface_vertices;
                        //foreach (var item in KbRen.part_surfaces[KbRen.part_surfaces.Count - 1])
                        //    LogBox_add(item.ToString());
                        if (surface_vertices.Count > 2)
                        {
                            KbRen.Part_draw();
                            save_data_part();
                        }

                        LogBox_add("New surface: Select next vertices points?");
                    }


                    if (command == "Delete point/line/surface: Select Point/line/surface?")
                    {
                        user_input_array = userinput.Split(new string[] { ":", "[", " " }, StringSplitOptions.RemoveEmptyEntries);
                        if (user_input_array.Count() < 4)
                        {
                            LogBox_add("Error: Select point/line/surface!");
                            LogBox_add("Delete point/line/surface: Select Point/line/surface?");
                            return;
                        }
                        else
                        {
                            isInteger = Int32.TryParse(user_input_array[3], out selected_point_id);
                            delete_type = user_input_array[1];
                            delete_id = selected_point_id;
                            if (delete_type == "point") { }
                            else if (delete_type == "line") { delete_id = delete_id - 1; } //1 is the points cell in ugrid
                            else if (delete_type == "surface") { delete_id = delete_id - KbRen.part_lines.Count() - 1; }
                        }

                        LogBox_add("Are you sure you want to delete the selected item? (yes/no)");
                        LogBox.AppendText("yes");
                        LogBox.Focus();
                    }
                    if (command == "Are you sure you want to delete the selected item? (yes/no)")
                    {
                        if (userinput.ToUpper() == "Y" || userinput.ToUpper() == "YES")
                        {
                            if (selected_point_id > KbRen.part_points.Count())
                            {
                                LogBox_add("Error: This point ID doesn't exist!" + Environment.NewLine + "Delete point/line/surface: Select Point/line/surface?");
                                return;
                            }
                            if (delete_type == "point")
                            {
                                foreach (var item in KbRen.part_lines)
                                {
                                    foreach (long col in item)
                                    {
                                        if (col == delete_id)
                                        {
                                            LogBox_add("Error: Couldn't delete this point. First, delete the lines connected to this point!");
                                            LogBox_add("Delete point/line/surface: Select Point/line/surface?");
                                            return;
                                        }
                                    }
                                }
                                foreach (var item in KbRen.part_surfaces)
                                {
                                    foreach (long col in item)
                                    {
                                        if (col == delete_id)
                                        {
                                            LogBox_add("Error: Couldn't delete this point. First, delete the surfaces connected to this point!");
                                            LogBox_add("Delete point/line/surface: Select Point/line/surface?");
                                            return;
                                        }
                                    }
                                }

                                KbRen.part_points.RemoveAt(delete_id);

                                for (int i = 0; i < KbRen.part_lines.Count(); i++)
                                {
                                    for (int j = 0; j < KbRen.part_lines[i].Count(); j++)
                                    {
                                        if (KbRen.part_lines[i][j] > delete_id)
                                        {
                                            //add 1 to each point id
                                            KbRen.part_lines[i][j] -= 1;
                                        }
                                    }
                                }
                                for (int i = 0; i < KbRen.part_surfaces.Count(); i++)
                                {
                                    for (int j = 0; j < KbRen.part_surfaces[i].Count(); j++)
                                    {
                                        if (KbRen.part_surfaces[i][j] > delete_id)
                                        {
                                            //add 1 to each point id
                                            KbRen.part_surfaces[i][j] -= 1;
                                        }
                                    }
                                }

                            }
                            else if (delete_type == "line")
                            {
                                KbRen.part_lines.RemoveAt(delete_id);
                            }
                            else if (delete_type == "surface")
                            {
                                KbRen.part_surfaces.RemoveAt(delete_id);
                            }
                            KbRen.Part_draw();
                            save_data_part();
                            LogBox_add("Object Deleted successfully!");
                            LogBox_add("Delete point/line/surface: Select Point/line/surface?");
                        }
                        else
                        {
                            LogBox_add("Delete command canceled!");
                            LogBox_add("Delete point/line/surface: Select Point/line/surface?");
                        }

                    }


                    //if (command == "Copy points: Select a point or enter point(s) id(s)?")
                    //{

                    //}


                }
                //else
                //{
                //    KbRen.part_point_select = false;
                //    KbRen.part_cell_select = false;
                //    KbRen.part_delete = false;
                //}
            }
        }
        private void load_parts()
        {
            string path = Path.GetDirectoryName(femix_filepath) + "\\";
            string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            file_name = file_name.Remove(file_name.Length - 3) + "_";
           
            
            
            //string filename_part;
            //combo_part_names.Items.Clear();
            //string extensions = "_femix.part";
            ////foreach (string ext in extensions)
            //foreach (string file in Directory.GetFiles(path,file_name + "*"  + extensions).OrderBy(d => new FileInfo(d).CreationTime).Where(item => item.EndsWith(extensions)))
            //{

            //    filename_part = Path.GetFileNameWithoutExtension(file);
            //    filename_part = filename_part.Remove(0, file_name.Length);
            //    combo_part_names.Items.Add(filename_part.Remove(filename_part.Length - 6));
            //    //combo_part_names.Items.Add(filename_part.Remove(filename_part.Length - file_name.Length - 6));
            //}


            string path1 = path + file_name + "parts\\";
            if (!Directory.Exists(path1))
                return;
            this.combo_part_names.Items.Clear();
            string extensions = "_femix.part";
            foreach (string file in ((IEnumerable<string>)Directory.GetFiles(path1, "*" + extensions)).OrderBy<string, DateTime>((Func<string, DateTime>)(d => new FileInfo(d).CreationTime)).Where<string>((Func<string, bool>)(item => item.EndsWith(extensions))))
            {
                string withoutExtension2 = Path.GetFileNameWithoutExtension(file);
                this.combo_part_names.Items.Add((object)withoutExtension2.Remove(withoutExtension2.Length - 6));
            }

        }

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {

            //string path = Path.GetDirectoryName(femix_filepath) + @"\";
            //string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            //file_name = file_name.Remove(file_name.Length - 3);
            //string extensions = "_femix.part";

            //string input = "Part_1";
            ////ShowInputDialog(ref input);
            //if (ShowInputDialog(ref input,"Name") == DialogResult.OK)
            //{
            //    input = input.Replace(" ", "_");
            //    foreach (string item in combo_part_names.Items)
            //    {
            //        if (item == input)
            //        {
            //            MessageBox.Show("This part exists and could not be created.");
            //            return;
            //        }
            //    }
            //    //file_name = path + input + "_" + file_name + extensions;
            //    file_name =  path + file_name+ "_" + input  +  extensions;

            //    System.IO.File.WriteAllText(file_name, $"{input} Femix Pre-Post Part. {DateTime.Today}\n");
            //    combo_part_names.Items.Add(input);
            //    combo_part_names.SelectedItem = input;
            //    LogBox_add($"Part \"{input}\" created successfully.");
            //}

            string str1 = Path.GetDirectoryName(this.femix_filepath) + "\\";
            string withoutExtension = Path.GetFileNameWithoutExtension(this.femix_filepath);
            string str2 = withoutExtension.Remove(withoutExtension.Length - 3);
            string str3 = "_femix.part";
            string path = str1 + str2 + "_parts\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string input = "Part_1";
            if (MainForm.ShowInputDialog(ref input, "Name") != DialogResult.OK)
                return;
            string str4 = input.Replace(" ", "_");
            foreach (string str5 in this.combo_part_names.Items)
            {
                if (str5 == str4)
                {
                    int num = (int)MessageBox.Show("This part exists and could not be created.");
                    return;
                }
            }
            File.WriteAllText(path + str4 + str3, string.Format("{0} Femix Pre-Post Part. {1}\n", (object)str4, (object)DateTime.Today));
            this.combo_part_names.Items.Add((object)str4);
            this.combo_part_names.SelectedItem = (object)str4;
            this.LogBox_add("Part \"" + str4 + "\" created successfully.");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete {combo_part_names.SelectedItem}?", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string path = Path.GetDirectoryName(femix_filepath) + @"\";
                string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
                file_name = file_name.Remove(file_name.Length - 3) + "_";
                string extensions = "_femix";
                string input = combo_part_names.SelectedItem.ToString();
                if (File.Exists(path + file_name + input + extensions + ".part"))
                    File.Delete(path + file_name + input + extensions + ".part");
                if (File.Exists(path + file_name + input + extensions + ".msh"))
                    File.Delete(path + file_name + input + extensions + ".msh");
                if (File.Exists(path + file_name + input + extensions + ".geo"))
                    File.Delete(path + file_name + input + extensions + ".geo");
                combo_part_names.Items.Remove(input);
                LogBox_add(path + file_name + input + extensions + " Deleted!");
                KbRen.Renderer.RemoveAllViewProps();
                KbRen.Refresh();
                //MessageBox.Show(path + input + file_name + extensions);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

            //string input = combo_part_names.SelectedItem.ToString();
            ////ShowInputDialog(ref input);
            //if (ShowInputDialog(ref input,"Name") == DialogResult.OK)
            //{
            //    input = input.Replace(" ", "_");
            //    string path = Path.GetDirectoryName(femix_filepath) + @"\";
            //    string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            //    file_name = file_name.Remove(file_name.Length - 3) + "_";
            //    string extensions = "_femix.part";
            //    string inputold = combo_part_names.SelectedItem.ToString();
            //    //File.Delete(path + input + file_name + extensions);
            //    System.IO.File.Move(path + file_name + inputold + extensions, path + file_name + input + extensions);
            //    combo_part_names.Items.Remove(inputold);
            //    combo_part_names.Items.Add(input);
            //    combo_part_names.SelectedItem = input;
            //    LogBox_add(inputold + " Renamed to " + input);
            //}

            if (this.combo_part_names.SelectedIndex == -1)
                return;
            string input = this.combo_part_names.SelectedItem.ToString();
            if (MainForm.ShowInputDialog(ref input, "Name") != DialogResult.OK)
                return;
            string str1 = input.Replace(" ", "_");
            string str2 = Path.GetDirectoryName(this.femix_filepath) + "\\";
            string withoutExtension = Path.GetFileNameWithoutExtension(this.femix_filepath);
            string str3 = withoutExtension.Remove(withoutExtension.Length - 3) + "_";
            string str4 = str2 + str3 + "parts\\";
            string str5 = "_femix.part";
            string str6 = this.combo_part_names.SelectedItem.ToString();
            File.Move(str4 + str6 + str5, str4 + str1 + str5);
            this.combo_part_names.Items.Remove((object)str6);
            this.combo_part_names.Items.Add((object)str1);
            this.combo_part_names.SelectedItem = (object)str1;
            this.LogBox_add(str6 + " Renamed to " + str1);

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //string path = Path.GetDirectoryName(femix_filepath) + @"\";
            //string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            //file_name = file_name.Remove(file_name.Length - 3) + "_";
            //string extensions = "_femix.part";
            //string input = combo_part_names.SelectedItem.ToString();
            ////File.Delete(path + input + file_name + extensions);
            //System.IO.File.Copy(path + file_name + input + extensions, path + file_name + input + "_2" + extensions);
            //combo_part_names.Items.Add(input + "_2");
            //combo_part_names.SelectedItem = input + "_2";
            //LogBox_add(input + "_2 Created");

            if (this.combo_part_names.SelectedIndex == -1)
                return;
            string str1 = Path.GetDirectoryName(this.femix_filepath) + "\\";
            string withoutExtension = Path.GetFileNameWithoutExtension(this.femix_filepath);
            string str2 = withoutExtension.Remove(withoutExtension.Length - 3) + "_";
            string str3 = str1 + str2 + "parts\\";
            string str4 = "_femix.part";
            string str5 = this.combo_part_names.SelectedItem.ToString();
            Random random = new Random();
            string str6 = "";
            if (File.Exists(str3 + str5 + "_2" + str4))
                str6 = random.Next(100).ToString();
            File.Copy(str3 + str5 + str4, str3 + str5 + str6 + "_2" + str4);
            this.combo_part_names.Items.Add((object)(str5 + str6 + "_2"));
            this.combo_part_names.SelectedItem = (object)(str5 + str6 + "_2");
            this.LogBox_add(str5 + str6 + "_2 Created");
        }

        private string part_path;
        private void save_data_part()
        {
            List<string> file_content_lines = new List<string>();
            file_content_lines.Add("Part_type, " + combo_part_type.SelectedIndex);
            file_content_lines.Add($"extrude_x, {txt_extrude_x.Text}");
            file_content_lines.Add($"extrude_y, {txt_extrude_y.Text}");
            file_content_lines.Add($"extrude_z, {txt_extrude_z.Text}");
            foreach (var item in KbRen.part_points)
            {
                file_content_lines.Add($"point,{item[0]},{item[1]},{item[2]}");
            }
            foreach (var item in KbRen.part_lines)
            {
                file_content_lines.Add($"line,{item[0]},{item[1]}");
            }
            foreach (var item in KbRen.part_surfaces)
            {
                if (item.Count() > 2)
                {
                    string col_s = "";
                    foreach (long col in item)
                        col_s += $"{col}, ";
                    col_s = col_s.Remove(col_s.Length - 2);//remove last ,
                    file_content_lines.Add($"surface, {col_s}");
                }
            }

            System.IO.File.WriteAllLines(part_path, file_content_lines);
        }
        bool first_load_of_part_type;
        private void combo_part_names_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (combo_part_names.SelectedIndex < 0)
            //    return;

            //string path = Path.GetDirectoryName(femix_filepath) + @"\";

            //KbRen.part_points = new List<List<double>>();


            //KbRen.part_lines = new List<List<int>>();
            //KbRen.part_surfaces = new List<List<int>>();

            //KbRen.extrusion_vector = new double[3];

            //KbRen.part_point_select = false;
            //KbRen.part_cell_select = false;

            //string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            //file_name = file_name.Remove(file_name.Length - 3);
            //string extensions = "_femix.part";
            //string input = combo_part_names.SelectedItem.ToString();
            //part_path = path + file_name + "_" + input + extensions;

            //string[] linecontent;
            //var lines = System.IO.File.ReadLines(part_path);
            //first_load_of_part_type = true;

            //combo_part_type.SelectedIndex = -1;
            //txt_extrude_x.Text = "0";
            //txt_extrude_y.Text = "0";
            //txt_extrude_z.Text = "0";
            //KbRen.extrusion_vector[0] = 0;
            //KbRen.extrusion_vector[1] = 0;
            //KbRen.extrusion_vector[2] = 0;

            //foreach (string line in lines)
            //{
            //    if (line != null && line != "")
            //    {

            //        linecontent = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            //        if (linecontent[0] == "Part_type")
            //        {
            //            combo_part_type.SelectedIndex = Int32.Parse(linecontent[1]);
            //            first_load_of_part_type = false;
            //        }

            //        if (linecontent[0] == "extrude_x")
            //        {
            //            txt_extrude_x.Text = double.Parse(linecontent[1]).ToString();
            //            KbRen.extrusion_vector[0] = double.Parse(linecontent[1]);
            //        }

            //        if (linecontent[0] == "extrude_y")
            //        {
            //            txt_extrude_y.Text = double.Parse(linecontent[1]).ToString();
            //            KbRen.extrusion_vector[1] = double.Parse(linecontent[1]);
            //        }

            //        if (linecontent[0] == "extrude_z")
            //        {
            //            txt_extrude_z.Text = double.Parse(linecontent[1]).ToString();
            //            KbRen.extrusion_vector[2] = double.Parse(linecontent[1]);
            //        }

            //        if (linecontent[0] == "point")
            //        {
            //            KbRen.part_points.Add(new List<double> { double.Parse(linecontent[1]), double.Parse(linecontent[2]), double.Parse(linecontent[3]) });
            //        }
            //        if (linecontent[0] == "line")
            //        {

            //            KbRen.part_lines.Add(new List<int> { Int32.Parse(linecontent[1]), Int32.Parse(linecontent[2]) });

            //        }
            //        if (linecontent[0] == "surface")
            //        {

            //            List<int> surface_vertices = new List<int>();
            //            for (int i = 1; i < linecontent.Count(); i++)

            //                surface_vertices.Add(Int32.Parse(linecontent[i]));

            //            KbRen.part_surfaces.Add(surface_vertices);
            //        }
            //    }
            //}
            //KbRen.Part_draw();
            //KbRen.FitView();

            if (this.combo_part_names.SelectedIndex < 0)
                return;
            string str1 = Path.GetDirectoryName(this.femix_filepath) + "\\";
            string withoutExtension = Path.GetFileNameWithoutExtension(this.femix_filepath);
            string str2 = withoutExtension.Remove(withoutExtension.Length - 3);
            string str3 = str1 + str2 + "_parts\\";
            string str4 = "_femix.part";
            this.KbRen.part_points = new List<List<double>>();
            this.KbRen.part_lines = new List<List<int>>();
            this.KbRen.part_surfaces = new List<List<int>>();
            this.KbRen.extrusion_vector = new double[3];
            this.KbRen.part_point_select = false;
            this.KbRen.part_cell_select = false;
            string str5 = this.combo_part_names.SelectedItem.ToString();
            this.part_path = str3 + str5 + str4;
            IEnumerable<string> strings = File.ReadLines(this.part_path);
            this.first_load_of_part_type = true;
            this.combo_part_type.SelectedIndex = -1;
            this.txt_extrude_x.Text = "0";
            this.txt_extrude_y.Text = "0";
            this.txt_extrude_z.Text = "0";
            this.KbRen.extrusion_vector[0] = 0.0;
            this.KbRen.extrusion_vector[1] = 0.0;
            this.KbRen.extrusion_vector[2] = 0.0;
            foreach (string str6 in strings)
            {
                if (str6 != null && str6 != "")
                {
                    string[] source = str6.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (source[0] == "Part_type")
                    {
                        this.combo_part_type.SelectedIndex = int.Parse(source[1]);
                        this.first_load_of_part_type = false;
                    }
                    double num;
                    if (source[0] == "extrude_x")
                    {
                        TextBox txtExtrudeX = this.txt_extrude_x;
                        num = double.Parse(source[1]);
                        string str7 = num.ToString();
                        txtExtrudeX.Text = str7;
                        this.KbRen.extrusion_vector[0] = double.Parse(source[1]);
                    }
                    if (source[0] == "extrude_y")
                    {
                        TextBox txtExtrudeY = this.txt_extrude_y;
                        num = double.Parse(source[1]);
                        string str8 = num.ToString();
                        txtExtrudeY.Text = str8;
                        this.KbRen.extrusion_vector[1] = double.Parse(source[1]);
                    }
                    if (source[0] == "extrude_z")
                    {
                        TextBox txtExtrudeZ = this.txt_extrude_z;
                        num = double.Parse(source[1]);
                        string str9 = num.ToString();
                        txtExtrudeZ.Text = str9;
                        this.KbRen.extrusion_vector[2] = double.Parse(source[1]);
                    }
                    if (source[0] == "point")
                        this.KbRen.part_points.Add(new List<double>()
            {
              double.Parse(source[1]),
              double.Parse(source[2]),
              double.Parse(source[3])
            });
                    if (source[0] == "line")
                        this.KbRen.part_lines.Add(new List<int>()
            {
              int.Parse(source[1]),
              int.Parse(source[2])
            });
                    if (source[0] == "surface")
                    {
                        List<int> intList = new List<int>();
                        for (int index = 1; index < ((IEnumerable<string>)source).Count<string>(); ++index)
                            intList.Add(int.Parse(source[index]));
                        this.KbRen.part_surfaces.Add(intList);
                    }
                }
            }
            this.KbRen.Part_draw();
            this.KbRen.FitView();

        }
        private void combo_part_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            KbRen.part_point_select = false;
            KbRen.part_cell_select = false;
            if (first_load_of_part_type == false)
                save_data_part();

            toolStripButton_point.Enabled = false;
            toolStripButton_line.Enabled = false;
            toolStripButton_surface.Enabled = false;
            toolStripButton_delete.Enabled = false;
            groupBox_extrusion.Enabled = false;
            KbRen.part_hide_extrusion = true;
            switch (combo_part_type.SelectedIndex)
            {
                case 0:
                    toolStripButton_point.Enabled = true;
                    toolStripButton_line.Enabled = false;
                    toolStripButton_surface.Enabled = false;
                    toolStripButton_delete.Enabled = true;
                    groupBox_extrusion.Enabled = false;
                    KbRen.part_hide_extrusion = true;
                    break;
                case 1:
                    toolStripButton_point.Enabled = true;
                    toolStripButton_line.Enabled = true;
                    toolStripButton_surface.Enabled = false;
                    toolStripButton_delete.Enabled = true;
                    groupBox_extrusion.Enabled = false;
                    KbRen.part_hide_extrusion = true;
                    break;
                case 2:
                    toolStripButton_point.Enabled = true;
                    toolStripButton_line.Enabled = false;
                    toolStripButton_surface.Enabled = true;
                    toolStripButton_delete.Enabled = true;
                    groupBox_extrusion.Enabled = false;
                    KbRen.part_hide_extrusion = true;
                    break;
                case 3:
                    toolStripButton_point.Enabled = true;
                    toolStripButton_line.Enabled = false;
                    toolStripButton_surface.Enabled = true;
                    toolStripButton_delete.Enabled = true;
                    groupBox_extrusion.Enabled = true;
                    KbRen.part_hide_extrusion = false;
                    break;
                default:
                    toolStripButton_point.Enabled = false;
                    toolStripButton_line.Enabled = false;
                    toolStripButton_surface.Enabled = false;
                    toolStripButton_delete.Enabled = false;
                    groupBox_extrusion.Enabled = false;
                    KbRen.part_hide_extrusion = true;
                    break;
            }
            KbRen.Part_draw();
        }

        private void combo_part_type_Click(object sender, EventArgs e)
        {

        }

        private void txt_extrude_x_TextChanged(object sender, EventArgs e)
        {


        }

        private void txt_extrude_z_TextChanged(object sender, EventArgs e)
        {

        }
        private void txt_extrude_z_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_extrude_z.Text == "")
                return;

            KbRen.extrusion_vector[2] = double.Parse(txt_extrude_z.Text);
            KbRen.part_hide_extrusion = false;
            KbRen.part_point_select = false;
            KbRen.part_cell_select = false;
            save_data_part();
            KbRen.Part_draw();


        }

        private void txt_extrude_y_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_extrude_y.Text == "")
                return;

            KbRen.extrusion_vector[1] = double.Parse(txt_extrude_y.Text);
            KbRen.part_hide_extrusion = false;
            KbRen.part_point_select = false;
            KbRen.part_cell_select = false;
            save_data_part();
            KbRen.Part_draw();

        }

        private void txt_extrude_x_KeyUp(object sender, KeyEventArgs e)
        {
            if (txt_extrude_x.Text == "")
                return;

            KbRen.extrusion_vector[0] = double.Parse(txt_extrude_x.Text);
            KbRen.part_hide_extrusion = false;
            KbRen.part_point_select = false;
            KbRen.part_cell_select = false;
            save_data_part();
            KbRen.Part_draw();

        }

        private void toolStrip_partselect_Click(object sender, EventArgs e)
        {
            KbRen.part_point_select = true;
            KbRen.part_cell_select = true;
            KbRen.part_hide_extrusion = true;
            KbRen.Part_draw();
            LogBox_add(Environment.NewLine + "Select point/line/surface?");
        }


        private void importGMSHMeshToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openFileDialog_femix.Filter = "GMSH Mesh|*.msh";
            openFileDialog_femix.Title = "Select GMSH Mesh output (*.msh)";
            string GMSH_filepath = "";
            string[] Gmsh_output = new string[10];
            DialogResult result = openFileDialog_femix.ShowDialog();
            if (result == DialogResult.OK)
            {

                GMSH_filepath = openFileDialog_femix.FileName;
                //string[] argument = new string[5];
                Gmsh_output = GMSHconvert.Convert(GMSH_filepath, new int[5] { C_variables.GROUP_NAMES_list.Count(), C_variables.POINT_COORDINATES_list.Count(), C_variables.SUPPORTS_list.Count(), C_variables.ELEMENT_NODES_list.Count(), C_variables.ELEMENT_PROPERTIES_list.Count() },
                    C_variables.POINT_COORDINATES_list, chk_douplicate_nodes.Checked,true, null,null,false, false, null);


                //0 --> Groups
                C_variables.Temp_tag = Gmsh_output[0];
                C_variables.Temp_list.Clear();
                if (C_variables.Temp_tag != null)
                {
                    C_variables.Temp_list = Class_femix_reader.ExtractLine(C_variables.Temp_tag, false);

                    ////////////////
                    bool exist = false;
                    foreach (var row in C_variables.Temp_list)
                    {
                        foreach (var row2 in C_variables.GROUP_NAMES_list) { if (row[2] == row2[2]) { exist = true; break; } else { exist = false; } }
                        if (exist == false)
                        {
                            C_variables.GROUP_NAMES_list.Add(row);
                        }
                    }
                    ///////////////
                    //C_variables.GROUP_NAMES_list.AddRange(C_variables.Temp_list);
                    C_variables.GROUP_NAMES_n = C_variables.GROUP_NAMES_list.Count();
                    C_variables.GROUP_NAMES_tag = creat_Gmesh_imported_segments(C_variables.GROUP_NAMES_list);
                }
                //End Group

                //1 --> point
                C_variables.Temp_tag = Gmsh_output[1];
                C_variables.Temp_list.Clear();
                if (C_variables.Temp_tag != null)
                {
                    C_variables.Temp_list = Class_femix_reader.ExtractLine(C_variables.Temp_tag, false);

                    C_variables.POINT_COORDINATES_list.AddRange(C_variables.Temp_list);
                    C_variables.POINT_COORDINATES_n = C_variables.POINT_COORDINATES_list.Count();
                    C_variables.POINT_COORDINATES_tag = creat_Gmesh_imported_segments(C_variables.POINT_COORDINATES_list);
                }
                //End

                //2 --> Support
                C_variables.Temp_tag = Gmsh_output[2];
                C_variables.Temp_list.Clear();
                if (C_variables.Temp_tag != null)
                {
                    C_variables.Temp_list = Class_femix_reader.ExtractLine(C_variables.Temp_tag, false);
                    C_variables.SUPPORTS_list.AddRange(C_variables.Temp_list);
                    C_variables.SUPPORTS_n = C_variables.SUPPORTS_list.Count();
                    C_variables.SUPPORTS_tag = creat_Gmesh_imported_segments(C_variables.SUPPORTS_list);
                }
                //End

                //3 --> Elements
                C_variables.Temp_tag = Gmsh_output[3];
                C_variables.Temp_list.Clear();
                if (C_variables.Temp_tag != null)
                {
                    C_variables.Temp_list = Class_femix_reader.ExtractLine(C_variables.Temp_tag, false);

                    ////////////////
                    //exist = false;
                    //foreach (var row in C_variables.Temp_list)
                    //{
                    //    foreach (var row2 in C_variables.GROUP_NAMES_list) { if (row[2] == row2[2]) { exist = true; } }
                    //    if (exist == false)
                    //    {
                    //        C_variables.ELEMENT_NODES_list.Add(row);
                    //    }
                    //}
                    ///////////////
                    C_variables.ELEMENT_NODES_list.AddRange(C_variables.Temp_list);
                    C_variables.ELEMENT_NODES_n = C_variables.ELEMENT_NODES_list.Count();
                    C_variables.ELEMENT_NODES_tag = creat_Gmesh_imported_segments(C_variables.ELEMENT_NODES_list);
                }
                //End

                //4 --> Elements Properties
                C_variables.Temp_tag = Gmsh_output[4];
                C_variables.Temp_list.Clear();
                if (C_variables.Temp_tag != null)
                {
                    C_variables.Temp_list = Class_femix_reader.ExtractLine(C_variables.Temp_tag, false);
                    C_variables.ELEMENT_PROPERTIES_list.AddRange(C_variables.Temp_list);
                    C_variables.ELEMENT_PROPERTIES_n = C_variables.ELEMENT_PROPERTIES_list.Count();
                    C_variables.ELEMENT_PROPERTIES_tag = creat_Gmesh_imported_segments(C_variables.ELEMENT_PROPERTIES_list);
                }
                //End


                tree_femix_fill();
                C_variables.femix_string_generator();
                Femixbox1.Text = C_variables.Femix_string;
                //Femixbox1.Text = Gmsh_output[0];
            }
        }

        private void btn_generate_Click(object sender, EventArgs e)
        {
            btn_add_mesh.Enabled = true;
            string path = Path.GetDirectoryName(femix_filepath) + "\\";
            string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            file_name = file_name.Remove(file_name.Length - 3);
            file_name = file_name + "_parts\\";
            string extensions = "_femix.part";
            string input = combo_part_names_mesh.SelectedItem.ToString();
            string working_path = path + file_name + input + extensions;
            working_path = working_path.Remove(part_path.Length - 5); //remove extension



            bool has_more_than_5_corner = false;
            List<string> file_content_lines = new List<string>();

            //////////Auto Mesh for embeded elements
            if (combo_part_type_mesh.SelectedIndex == 1 && chk_embeding.Checked == true)
            {
                progressBar_mesh.Value = 0;
                progressBar_mesh.Visible = true;
                generate_1D_auto_mesh(working_path, input);
                progressBar_mesh.Visible = false;
                LogBox_add("Auto mesh for embedded elements with respect to mother elements finished successfully!");
                return;
            }
            //////////End of Auto Mesh for embeded elements


            file_content_lines.Add($"// Gmsh project created by Femix Pre-Post on {DateTime.Now}");
            file_content_lines.Add($"lc = {double.Parse(txt_mesh_size.Text)};");
            switch (combo_part_type_mesh.SelectedIndex) {
                case -1:
                    MessageBox.Show("Error: There is no element type!");
                    return;
                case 0:
                    file_content_lines.Add($"Physical Point(\"{input}\") = " + "{};");
                    break;
                case 1:
                    file_content_lines.Add($"Physical Curve(\"{input}\") = " + "{};");
                    break;
                case 2:
                    file_content_lines.Add($"Physical Surface(\"{input}\") = " + "{};");
                    break;
                case 3:
                    file_content_lines.Add($"Physical Volume(\"{input}\") = " + "{};");
                    break;
                default:
                    break;
            }
            file_content_lines.Add("//+");
            int counter = 0;
            foreach (var item in KbRen.part_points)
            {
                file_content_lines.Add($"Point({counter}) = " + "{" + $"{item[0]}, {item[1]}, {item[2]}" + ", lc};");
                if (combo_part_type_mesh.SelectedIndex == 0)
                    file_content_lines.Add($"Physical Point(\"{input}\") += " + "{" + counter + "};");
                counter++;
            }

            if (combo_part_type_mesh.SelectedIndex == 1) {
                file_content_lines.Add("//+");
                counter = 0;
                foreach (var item in KbRen.part_lines)
                {
                    counter++;
                    file_content_lines.Add($"Curve({counter}) = " + "{" + $" {item[0]}, {item[1]}" + "}; ");
                    if (combo_part_type_mesh.SelectedIndex == 1)
                        file_content_lines.Add($"Physical Curve(\"{input}\") += " + "{" + counter + "};");
                }
            }


            if (combo_part_type_mesh.SelectedIndex > 1)
            {
                string curve_loop_list, Surface_top_list = null;

                file_content_lines.Add("//+");

                for (int i = 0; i < KbRen.part_surfaces.Count(); i++)
                {
                    curve_loop_list = "";
                    Surface_top_list = "";
                    for (int j = 0; j < KbRen.part_surfaces[i].Count() - 1; j++)
                    {
                        //KbRen.part_surfaces[i][j]
                        file_content_lines.Add($"L[{j + 1}]=newc; Curve(L[{j + 1}]) = " + "{" + $"{KbRen.part_surfaces[i][j]}, {KbRen.part_surfaces[i][j + 1]}" + "};");
                        curve_loop_list += $"L[{j + 1}], ";
                        Surface_top_list += $"ST[{j}], ";
                    }
                    file_content_lines.Add($"L[{KbRen.part_surfaces[i].Count()}]=newc; Curve(L[{KbRen.part_surfaces[i].Count()}]) = " + "{" + $"{KbRen.part_surfaces[i][KbRen.part_surfaces[i].Count() - 1]}, {KbRen.part_surfaces[i][0]}" + "};");
                    curve_loop_list += $"L[{KbRen.part_surfaces[i].Count()}]";
                    Surface_top_list += $"ST[{KbRen.part_surfaces[i].Count() - 1}]";
                    file_content_lines.Add("cloop = newll; Curve Loop(cloop) = {" + curve_loop_list + "};");
                    //Surface
                    file_content_lines.Add($"Plane Surface({i + 1}) = " + "{cloop};");
                    if (chk_structured_mesh.Checked == true)
                    {
                        if (double.Parse(txt_transfinite.Text) > 0)
                            file_content_lines.Add("Transfinite Line{" + $"{curve_loop_list}" + "} =" + $" {txt_transfinite.Text};");
                    }
                    if (combo_part_type_mesh.SelectedIndex == 2)
                    {
                        file_content_lines.Add($"Physical Surface(\"{input}\") += " + "{" + $"{i + 1}" + "};");
                    }
                    file_content_lines.Add("//+");
                    if (KbRen.part_surfaces[i].Count() > 4)
                        has_more_than_5_corner = true;
                }

                if (combo_part_type_mesh.SelectedIndex == 3)
                {
                    file_content_lines.Add("//+");
                    if (KbRen.extrusion_vector[0] != 0 || KbRen.extrusion_vector[1] != 0 || KbRen.extrusion_vector[2] != 0) {
                        for (int i = 0; i < KbRen.part_surfaces.Count(); i++)
                        {
                            file_content_lines.Add("listextru[]=Extrude {" + $"{KbRen.extrusion_vector[0]}, {KbRen.extrusion_vector[1]}, {KbRen.extrusion_vector[2]}" + "} {Surface{" + $"{i + 1}" + "};};");
                            file_content_lines.Add($"Physical Volume(\"{input}\") += " + "{listextru[1]};");
                            file_content_lines.Add($"ST_ka   = listextru[0];");
                            file_content_lines.Add("ST[] = Boundary{Surface{ST_ka};};");
                            ///surface_top   = listextru[0];
                            ///volume = listextru[1];
                            ///lines_top[] = Boundary{Surface{surface_top};};
                            if (chk_structured_mesh.Checked == true)
                            {
                                if (double.Parse(txt_transfinite.Text) > 0)
                                    file_content_lines.Add("Transfinite Line{" + $"{Surface_top_list}" + "} =" + $" {txt_transfinite.Text};");
                            }

                            file_content_lines.Add("//+");
                        }
                    }
                }
                if (radio_quad.Checked == true) 
                {
                file_content_lines.Add("//+");
                file_content_lines.Add("Recombine Surface{:};");
                file_content_lines.Add("Recombine Volume{:};");
                }

                if (chk_structured_mesh.Checked == true)
                {
                    //if (double.Parse(txt_transfinite.Text) > 0)
                    //    file_content_lines.Add("Transfinite Line{:} ="+$" {txt_transfinite.Text};");
                    file_content_lines.Add("Transfinite Surface{:};");
                    file_content_lines.Add("Transfinite Volume{:};");
                    if (has_more_than_5_corner)
                        LogBox_add("Error on Mesh: Structured mesh cannot be use on surfaces with more than 4 corners!!");
                }
            }

            if (chk_quad_mesh.Checked == true)
            {
                file_content_lines.Add("//+");
                file_content_lines.Add("Mesh.ElementOrder=2;");
                file_content_lines.Add("Mesh.SecondOrderIncomplete=1;");
            }
            file_content_lines.Add("Geometry.Tolerance = 1e-3;");
            file_content_lines.Add("Coherence;");
            file_content_lines.Add("Coherence Mesh;");

            //txt_mesh_min
            //txt_mesh_max
            file_content_lines.Add($"\n// kamyar_ba@yahoo.com");
            System.IO.File.WriteAllLines(working_path + ".geo", file_content_lines);

            LogBox_add("GMSH geometry (.geo) file created.");

            femix_log femix_log = new femix_log();
            //femix_log.run_GMSH(@"C:\Users\Kamyar\Desktop\Christoph\simple.geo");
            femix_log.Stringtransfer = femix_log.run_GMSH(working_path + ".geo", double.Parse(txt_mesh_min.Text), double.Parse(txt_mesh_max.Text));
            femix_log.ShowDialog();

            StringReader strReader = new StringReader(femix_log.Stringtransfer);
            string aLine = null;
            while (true)
            {
                aLine = strReader.ReadLine();
                if (aLine != null)
                {
                    if (aLine.Contains("nodes") && aLine.Contains("elements"))
                    {
                        LogBox_add(aLine);
                        LogBox_add("Mesh generated.");
                    }
                }
                else
                {
                    break;
                }
            }


            draw_gmsh_msh_file(working_path + ".msh", input);
        }

        private void draw_gmsh_msh_file(string msh_file_path, string input)
        {
            if (!File.Exists(msh_file_path))
                return;
            string[] Gmsh_output = new string[10];
            //try
            //{
                Gmsh_output = GMSHconvert.Convert(msh_file_path, new int[5] { 0, 0, 0, 0, 0 },
               C_variables.POINT_COORDINATES_list, true, false,
               input, combo_part_type_mesh.SelectedItem.ToString(),radio_quad.Checked,chk_embeding.Checked, null);


                C_variables.Layer_items.Clear();
                C_variables.Layer_items.Add("Nodes", new List<int> { });
                C_variables.Layer_items.Add("1D", new List<int> { });
                C_variables.Layer_items.Add("2D", new List<int> { });
                C_variables.Layer_items.Add("3D", new List<int> { });
                C_variables.Layer_items.Add(input, new List<int> { });

                DG_Layers.Rows.Clear();
                DG_Layers.Rows.Add();
                DG_Layers.Rows[0].Cells["col_name1"].Value = "Nodes";
                DG_Layers.Rows[0].Cells["col_visible"].Value = false;
                DG_Layers.Rows.Add();
                DG_Layers.Rows[1].Cells["col_name1"].Value = "Label Nodes";
                DG_Layers.Rows[1].Cells["col_visible"].Value = false;
                DG_Layers.Rows.Add();
                DG_Layers.Rows[2].Cells["col_name1"].Value = "Label Elements";
                DG_Layers.Rows[2].Cells["col_visible"].Value = false;
                DG_Layers.Rows.Add();
                DG_Layers.Rows[3].Cells["col_name1"].Value = input;
                DG_Layers.Rows[3].Cells["col_visible"].Value = true;

                //Console.WriteLine(Gmsh_output[1]);
                KbRen.Draw_objects(Class_femix_reader.ExtractLine(Gmsh_output[1], false),
                Class_femix_reader.ExtractLine(Gmsh_output[4], false),
                Class_femix_reader.ExtractLine(Gmsh_output[3], false),"true");

            //KbRen.Renderer.ResetCamera();
            DG_Layers.Rows[0].Cells[1].Value = true;
            foreach (int actor_index in C_variables.Layer_items["Nodes"])
                KbRen.actors[actor_index].VisibilityOn();
            

            KbRen.Refresh();
            //}
            //catch //(Exception e2)
            //{
            //    MessageBox.Show("Error, The meshed element could not be drawn!");
            //}
            

        }
        private string[,] Interface_element_nodes(string slave_element_name, string slave_element_type, bool slave_element_quad, bool slave_element_quadratic,
            List<IList<string>> mesh_point_list, List<IList<string>> coordinate_list, List<IList<string>> element_list, List<IList<string>> element_properties)
        {


            string[,] interface_element_list = null;
            int n_node_interface = 0;

            switch (slave_element_type)
            {
                case "0d-Point":
                    n_node_interface = 2;
                    interface_element_list = new string[mesh_point_list.Count(), n_node_interface];
                    
                    for (int i=0;i< mesh_point_list.Count();i++)
                    {
                    Application.DoEvents();
                    progressBar_mesh.Value = Convert.ToInt32((i / mesh_point_list.Count())*100);

                        for (int j = 0; j < coordinate_list.Count(); j++)
                        {
                            if (Math.Abs(double.Parse(mesh_point_list[i][2]) - double.Parse(coordinate_list[j][2])) < 1e-3 &&
                                Math.Abs(double.Parse(mesh_point_list[i][3]) - double.Parse(coordinate_list[j][3])) < 1e-3 &&
                                Math.Abs(double.Parse(mesh_point_list[i][4]) - double.Parse(coordinate_list[j][4])) < 1e-3)
                            {
                                if (interface_element_list[i, 0] == null)
                                {
                                    interface_element_list[i, 0] = coordinate_list[j][1];//point name
                                }
                                else if (interface_element_list[i, 1] == null)
                                {
                                    interface_element_list[i, 1] = coordinate_list[j][1];//point name
                                }
                                else
                                {
                                    MessageBox.Show("There are more than 2 points for the following coordinate. Please verify the generated interfaces."+
                                        $"\n[{coordinate_list[j][2]} , {coordinate_list[j][3]} , {coordinate_list[j][4]}]");
                                }

                            }
                        }

                    }
                    return interface_element_list;

                case "1d-Wire":
                    if (slave_element_quadratic==true)
                        n_node_interface = 6;
                    else
                        n_node_interface = 4;
                    break;
                case "2d-Shell":
                    if (slave_element_quad==true)
                    {
                        if (slave_element_quadratic == true)
                            n_node_interface = 16;
                        else
                            n_node_interface = 8;
                    }
                    else
                    {
                        if (slave_element_quadratic == true)
                            n_node_interface = 12;
                        else
                            n_node_interface = 6;
                    }
                    break;
                case "3d-Solid":
                    MessageBox.Show("The interface element could not be generated for 3d-Solid!");
                    break;
                default:
                    break;
            }


            

            foreach (var item in element_properties)
            {
                if (item[2] == slave_element_name)
                {
                    int[] elementrange = KbRen.element_range(item[4]);
                    interface_element_list = new string[(elementrange[1]- elementrange[0])+1, n_node_interface];
                    int current_row, n_col;
                    string current_point_name;
                    n_col = n_node_interface;
                    for (int i = elementrange[0]; i <= elementrange[1]; i++)
                    {
                        Application.DoEvents();
                        progressBar_mesh.Value = Convert.ToInt32((i - elementrange[0]) * (100 / Convert.ToDouble(elementrange[1] - elementrange[0])));

                        //Console.WriteLine($"{i}    {ELEMENT_NODES[i - 1][1]}");
                        int n_node = Int32.Parse(element_list[i - 1][2]);
                        if (n_node != n_node_interface / 2)
                        {
                            MessageBox.Show("The selected mesh options are not compatible with mesh type!!\nThe interface could not be generated! Regenerate the mesh and try again!");
                            return null;
                        }

                        current_row = i - elementrange[0];

                        for (int i2 = 0; i2 < n_col / 2; i2++)
                        {
                            current_point_name = element_list[i - 1][i2 + 3];

                            double[] point_coordinate = new double[5];
                            point_coordinate[1] = double.Parse(coordinate_list[Int32.Parse(current_point_name) - 1][1]);
                            point_coordinate[2] = double.Parse(coordinate_list[Int32.Parse(current_point_name) - 1][2]);
                            point_coordinate[3] = double.Parse(coordinate_list[Int32.Parse(current_point_name) - 1][3]);
                            point_coordinate[4] = double.Parse(coordinate_list[Int32.Parse(current_point_name) - 1][4]);

                            interface_element_list[current_row, i2 + (n_col / 2)] = current_point_name;

                            for (int j = 0; j < coordinate_list.Count(); j++)
                            {
                                if (point_coordinate[1] != double.Parse(coordinate_list[j][1]) &&
                                    Math.Abs(point_coordinate[2] - double.Parse(coordinate_list[j][2])) < 1e-3 &&
                                    Math.Abs(point_coordinate[3] - double.Parse(coordinate_list[j][3])) < 1e-3 &&
                                    Math.Abs(point_coordinate[4] - double.Parse(coordinate_list[j][4])) < 1e-3)
                                {
                                    interface_element_list[current_row, i2] = coordinate_list[j][1];
                                }

                            }


                        }
                        //for (int j = 0; j < n_col; j++)
                        //    Console.Write(j + "-->" + interface_element_list[current_row, j] + ",");
                        //Console.WriteLine("|");
                    }


                }
            }


            //Console.WriteLine(slave_element_name);


            //list_col.Add("error");
            //list_col.Add("The selected mesh options are not compatible with mesh type!!\nThe interface could not be generated! Regenerate the mesh and try again!");
            //detected_interface_intersections.Add(list_col);
            //progressBar_mesh.Visible = false;
            //return detected_interface_intersections;

            return interface_element_list;
        }
        private void generate_1D_auto_mesh(string working_path, string input)
        {
            //foreach (var item in KbRen.part_points)
            //{
            //   LogBox_add($"point,{item[0]},{item[1]},{item[2]}");
            //}

            Point3D P_line0, P_line1,P_line_element0, P_line_element1, P_plane0, P_plane1, P_plane2, P_plane3, P_intersection;
            double distance_from_start;
            //Int32.Parse(LineSegment[0].ToString())
            int line_segment_counter = 0;
            List<IList<double>> List_of_intersections=new List<IList<double>>();
            // 0:line segment   ;   1:x,2:y,3:z     ; 4:length from start of line segment
            //List_of_intersections.Add(new List<double> { line_counter, P_intersection.X, P_intersection.Y, P_intersection.Z, distance_from_start });
            foreach (var LineSegment in KbRen.part_lines)
            {
                Application.DoEvents();
                line_segment_counter++;
                P_line0 = new Point3D(KbRen.part_points[Convert.ToInt32(LineSegment[0])][0],
                                     KbRen.part_points[Convert.ToInt32(LineSegment[0])][1],
                                      KbRen.part_points[Convert.ToInt32(LineSegment[0])][2]);
                P_line1 = new Point3D(KbRen.part_points[Convert.ToInt32(LineSegment[1])][0],
                                      KbRen.part_points[Convert.ToInt32(LineSegment[1])][1],
                                      KbRen.part_points[Convert.ToInt32(LineSegment[1])][2]);

                LogBox_add($"Generating mesh for Line segment {line_segment_counter} of {KbRen.part_lines.Count}");
                LogBox_add($"The line segment from point {LineSegment[0]} {P_line0.ToString()} to {LineSegment[1]} {P_line1.ToString()}");


                int[] elementrange = new int[] { };
                int[] pointNumber = new int[30];
                double progress = 0;
                if (C_variables.ELEMENT_PROPERTIES_list.Count > 0 && C_variables.ELEMENT_NODES_list.Count > 0)
                {
                    foreach (var item in C_variables.ELEMENT_PROPERTIES_list)
                    {
                        //Application.DoEvents();
                        if (item.Count() > 5)
                        {
                            elementrange = KbRen.element_range(item[4]);
                            if (item[5] == "_SOLID_HEXA")
                            {
                                LogBox_add($"Mother element: {item[2]}");
                                LogBox_add($"Generating embeded elements for 3D mother element....");
                                for (int i = elementrange[0]; i <= elementrange[1]; i++)
                                {
                                    Application.DoEvents();
                                    progress = (i - elementrange[0]) * (100 / Convert.ToDouble(elementrange[1] - elementrange[0]));
                                    progressBar_mesh.Value = Convert.ToInt32(progress);

                                    if (Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][1]) != i) { MainForm.displayErrorBox("The Number of Nodes are not in sequence or the element doesn't exist!!"); return; }
                                    if (Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][2]) == 8)//8 nodes
                                    {
                                        pointNumber[0] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][0+3]);
                                        pointNumber[1] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][1+3]);
                                        pointNumber[2] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][2+3]);
                                        pointNumber[3] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][3+3]);
                                        pointNumber[4] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][4+3]);
                                        pointNumber[5] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][5+3]);
                                        pointNumber[6] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][6+3]);
                                        pointNumber[7] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][7+3]);

                                        for (int i1 = 0; i1 < 8; i1++)
                                        {
                                            pointNumber[i1] = pointNumber[i1] - 1;
                                        }


                                        List<IList<int>> PlanePoints = new List<IList<int>>();
                                        PlanePoints.Add(new List<int> { 0, 1, 2, 3 });
                                        PlanePoints.Add(new List<int> { 4, 5, 6, 7 });
                                        PlanePoints.Add(new List<int> { 1, 2, 6, 5 });
                                        PlanePoints.Add(new List<int> { 0, 3, 7, 4 });
                                        PlanePoints.Add(new List<int> { 2, 3, 7, 6 });
                                        PlanePoints.Add(new List<int> { 1, 0, 4, 5 });

                                        foreach (var planelist in PlanePoints)
                                        {
                                            P_plane0 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[0]]][2]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[0]]][3]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[0]]][4]));
                                            P_plane1 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[1]]][2]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[1]]][3]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[1]]][4]));
                                            P_plane2 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[2]]][2]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[2]]][3]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[2]]][4]));
                                            P_plane3 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[3]]][2]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[3]]][3]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[3]]][4]));
                                            P_intersection = Vector3D.QuadraticPlaneLineSegmentIntersection(P_plane0, P_plane1, P_plane2, P_plane3, P_line0, P_line1);

                                            if (P_intersection != null)//there is an intersection
                                            {
                                                distance_from_start = intersection.Distance_between_points(P_line0, P_intersection);
                                                List_of_intersections.Add(new List<double> { line_segment_counter, P_intersection.X, P_intersection.Y, P_intersection.Z, distance_from_start });
                                            }
                                        }


                                    }
                                    else if (Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][2]) == 20)//20 nodes
                                    {

                                        pointNumber[0] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][0 + 3]);
                                        pointNumber[1] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][1 + 3]);
                                        pointNumber[2] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][2 + 3]);
                                        pointNumber[3] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][3 + 3]);
                                        pointNumber[4] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][4 + 3]);
                                        pointNumber[5] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][5 + 3]);
                                        pointNumber[6] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][6 + 3]);
                                        pointNumber[7] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][7 + 3]);
                                        pointNumber[8] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][8 + 3]);
                                        pointNumber[9 ] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][9 + 3]);
                                        pointNumber[10] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][10+ 3]);
                                        pointNumber[11] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][11+ 3]);
                                        pointNumber[12] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][12 + 3]);
                                        pointNumber[13] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][13 + 3]);
                                        pointNumber[14] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][14 + 3]);
                                        pointNumber[15] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][15 + 3]);
                                        pointNumber[16] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][16+ 3]);
                                        pointNumber[17] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][17+ 3]);
                                        pointNumber[18] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][18 + 3]);
                                        pointNumber[19] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][19 + 3]);

                                        for (int i1 = 0; i1 < 20; i1++)
                                        {
                                            pointNumber[i1] = pointNumber[i1] - 1;
                                        }

                                        List<IList<int>> PlanePoints = new List<IList<int>>();
                                        PlanePoints.Add(new List<int> { 0, 2, 4, 6 });
                                        PlanePoints.Add(new List<int> { 12, 14, 16, 18 });
                                        PlanePoints.Add(new List<int> { 2, 4, 16, 14 });
                                        PlanePoints.Add(new List<int> { 0, 6, 18, 12 });
                                        PlanePoints.Add(new List<int> { 4, 6, 18, 16 });
                                        PlanePoints.Add(new List<int> { 0, 2, 14, 12 });
                                        
                                        foreach (var planelist in PlanePoints)
                                        {
                                            P_plane0 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[0]]][2]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[0]]][3]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[0]]][4]));
                                            P_plane1 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[1]]][2]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[1]]][3]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[1]]][4]));
                                            P_plane2 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[2]]][2]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[2]]][3]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[2]]][4]));
                                            P_plane3 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[3]]][2]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[3]]][3]),
                                                   double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[planelist[3]]][4]));
                                            P_intersection = Vector3D.QuadraticPlaneLineSegmentIntersection(P_plane0, P_plane1, P_plane2, P_plane3, P_line0, P_line1);

                                            if (P_intersection != null)//there is an intersection
                                            {
                                                distance_from_start = intersection.Distance_between_points(P_line0, P_intersection);
                                                List_of_intersections.Add(new List<double> { line_segment_counter, P_intersection.X, P_intersection.Y, P_intersection.Z, distance_from_start });
                                            }
                                        }

                                    }
                                }

                            }

                            if (item[5] == "_PLANE_STRESS_QUAD" || item[5] == "_MINDLIN_SHELL_QUAD" || item[5] == "_MINDLIN_SLAB_QUAD" || item[5] == "_AHMAD_SHELL_QUAD")
                            {
                                LogBox_add($"Mother element: \"{item[2]}\" N. elements= {elementrange[1]- elementrange[0]}");
                                LogBox_add($"Generating embeded elements for 2D mother element....");
                                for (int i = elementrange[0]; i <= elementrange[1]; i++)
                                {
                                    Application.DoEvents();
                                    progress = (i - elementrange[0]) * (100 / Convert.ToDouble(elementrange[1] - elementrange[0]));
                                    progressBar_mesh.Value = Convert.ToInt32(progress);

                                    if (Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][1]) != i) { MainForm.displayErrorBox("The Number of Nodes are not in sequence or the element doesn't exist!!"); return; }
                                    if (Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][2]) == 4)//4 nodes
                                    {
                                        pointNumber[0] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][3]);
                                        pointNumber[1] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][4]);
                                        pointNumber[2] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][5]);
                                        pointNumber[3] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][6]);

                                        for (int i1 = 0; i1 < 4; i1++)
                                        {
                                            pointNumber[i1] = pointNumber[i1] - 1;
                                        }


                                        int startline, endline;
                                        List<int> startANDend = new List<int> { 0, 1, 2, 3, 0 };
                                        for (int i2=0;i2< startANDend.Count - 1; i2++)
                                        {
                                            startline = startANDend[i2]; endline = startANDend[i2+1];

                                            P_line_element0 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[startline]][2]),
                                               double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[startline]][3]),
                                               double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[startline]][4]));
                                            P_line_element1 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[endline]][2]),
                                                double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[endline]][3]),
                                                double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[endline]][4]));
                                            P_intersection = new Point3D();
                                            int result = intersection.get_2Dline_intersection(P_line0, P_line1, P_line_element0, P_line_element1, ref P_intersection);
                                            if (result == 1)//there is intersection
                                            {
                                                distance_from_start = intersection.Distance_between_points(P_line0, P_intersection);
                                                List_of_intersections.Add(new List<double> { line_segment_counter, P_intersection.X, P_intersection.Y, P_intersection.Z, distance_from_start });
                                            }
                                        }



                                    }
                                    else if (Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][2]) == 8)//8 nodes
                                    {

                                        pointNumber[0] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][0 + 3]);
                                        pointNumber[1] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][1 + 3]);
                                        pointNumber[2] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][2 + 3]);
                                        pointNumber[3] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][3 + 3]);
                                        pointNumber[4] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][4 + 3]);
                                        pointNumber[5] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][5 + 3]);
                                        pointNumber[6] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][6 + 3]);
                                        pointNumber[7] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][7 + 3]);

                                        for (int i1 = 0; i1 < 8; i1++)
                                        {
                                            pointNumber[i1] = pointNumber[i1] - 1;
                                        }

                                        int startline, endline;
                                        List<int> startANDend = new List<int> { 0, 2, 4, 6, 0 };
                                        for (int i2 = 0; i2 < startANDend.Count - 1; i2++)
                                        {
                                            startline = startANDend[i2]; endline = startANDend[i2 + 1];

                                            P_line_element0 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[startline]][2]),
                                               double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[startline]][3]),
                                               double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[startline]][4]));
                                            P_line_element1 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[endline]][2]),
                                                double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[endline]][3]),
                                                double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[endline]][4]));
                                            P_intersection = new Point3D();
                                            int result = intersection.get_2Dline_intersection(P_line0, P_line1, P_line_element0, P_line_element1, ref P_intersection);
                                            if (result == 1)//there is intersection
                                            {
                                                distance_from_start = intersection.Distance_between_points(P_line0, P_intersection);
                                                List_of_intersections.Add(new List<double> { line_segment_counter, P_intersection.X, P_intersection.Y, P_intersection.Z, distance_from_start });
                                            }
                                        }

                                    }
                                    else if (Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][2]) == 9)//9 nodes
                                    {

                                        pointNumber[0] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][0 + 3]);
                                        pointNumber[1] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][1 + 3]);
                                        pointNumber[2] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][2 + 3]);
                                        pointNumber[3] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][3 + 3]);
                                        pointNumber[4] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][4 + 3]);
                                        pointNumber[5] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][5 + 3]);
                                        pointNumber[6] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][6 + 3]);
                                        pointNumber[7] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][7 + 3]);
                                        pointNumber[8] = Int32.Parse(C_variables.ELEMENT_NODES_list[i - 1][8 + 3]);
                                        for (int i1 = 0; i1 < 9; i1++)
                                        {
                                            pointNumber[i1] = pointNumber[i1] - 1;
                                        }

                                        int startline, endline;
                                        List<int> startANDend = new List<int> { 0, 2, 4, 6, 0 };
                                        for (int i2 = 0; i2 < startANDend.Count - 1; i2++)
                                        {
                                            startline = startANDend[i2]; endline = startANDend[i2 + 1];

                                            P_line_element0 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[startline]][2]),
                                               double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[startline]][3]),
                                               double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[startline]][4]));
                                            P_line_element1 = new Point3D(double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[endline]][2]),
                                                double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[endline]][3]),
                                                double.Parse(C_variables.POINT_COORDINATES_list[pointNumber[endline]][4]));
                                            P_intersection = new Point3D();
                                            int result = intersection.get_2Dline_intersection(P_line0, P_line1, P_line_element0, P_line_element1, ref P_intersection);
                                            if (result == 1)//there is intersection
                                            {
                                                distance_from_start = intersection.Distance_between_points(P_line0, P_intersection);
                                                List_of_intersections.Add(new List<double> { line_segment_counter, P_intersection.X, P_intersection.Y, P_intersection.Z, distance_from_start });
                                            }
                                        }

                                    }

                                }
                            }
                        }
                    }
                }
            }


            List_of_intersections = List_of_intersections.OrderBy(x => x[4]).ToList();//4: sort by distance
            List_of_intersections = List_of_intersections.OrderBy(x => x[0]).ToList();//4: sort by line segment

            //.OrderBy(f => f[0]).ThenBy(f => f[1]).ToList();
            //List<int> distinct = list.Distinct().ToList(); //Unique list
            //remove duplicates
            //var modified_List_of_intersections = List_of_intersections.ToList();

            List<IList<double>> modified_List_of_intersections = new List<IList<double>>();
            bool is_repeated = false;
            foreach (var item in List_of_intersections.Select((value, index) => new { value, index }))
            {
                is_repeated = false;
                for (int i = item.index + 1; i < List_of_intersections.Count; i++)
                {
                    if (item.value[0] == List_of_intersections[i][0] && item.value[4] == List_of_intersections[i][4])//col line name and distance
                    {
                        is_repeated = true;
                        break;
                    }
                }
                if (is_repeated == false)
                    modified_List_of_intersections.Add(List_of_intersections[item.index].ToList());
            }

            if (chk_quad_mesh.Checked == true)
            {
                int N_item = modified_List_of_intersections.Count;
                for (int i = 0; i < N_item-1; i++)
                {
                    if (modified_List_of_intersections[i][0] == modified_List_of_intersections[i + 1][0])
                    {
                        double Midpoint_x, Midpoint_y, Midpoint_z;
                        Midpoint_x = (modified_List_of_intersections[i][1] + modified_List_of_intersections[i + 1][1]) / 2;
                        Midpoint_y = (modified_List_of_intersections[i][2] + modified_List_of_intersections[i + 1][2]) / 2;
                        Midpoint_z = (modified_List_of_intersections[i][3] + modified_List_of_intersections[i + 1][3]) / 2;
                        distance_from_start = (modified_List_of_intersections[i][4] + modified_List_of_intersections[i + 1][4]) / 2;

                        modified_List_of_intersections.Add(new List<double> { modified_List_of_intersections[i][0], Midpoint_x, Midpoint_y, Midpoint_z, distance_from_start });
                    }
                    }

                modified_List_of_intersections = modified_List_of_intersections.OrderBy(x => x[4]).ToList();//4: sort by distance
                modified_List_of_intersections = modified_List_of_intersections.OrderBy(x => x[0]).ToList();//4: sort by line segment

            }


            List_of_intersections = modified_List_of_intersections.ToList();

            if (List_of_intersections.Count < 2)
            {
                LogBox_add("There is no intersection. No mesh could be generated.");
                return;
            }

            //Console.WriteLine(List_of_intersections.Count);
            //foreach (var item in List_of_intersections.Select((value, index) => new { value, index }))
            //{
            //    Console.WriteLine($"{item.index} - {item.value[0]} | {item.value[1]} | {item.value[2]} | {item.value[3]} | {item.value[4]}");
            //}

            List<string> file_content_lines = new List<string>();
            file_content_lines.Add("$MeshFormat");
            file_content_lines.Add("4.8 0 8");
            file_content_lines.Add("$EndMeshFormat");
            file_content_lines.Add("$PhysicalNames");
            file_content_lines.Add("1");
            file_content_lines.Add($"1 1 \"{input}\"");
            file_content_lines.Add("$EndPhysicalNames");
            file_content_lines.Add("$Entities");
            file_content_lines.Add($"0 {line_segment_counter} 0 0");
            for (int i = 1; i < line_segment_counter + 1; i++)
            {
                file_content_lines.Add($"{i} 0 0 0 0 0 0 1 1 1 1 0");
            }
            file_content_lines.Add("$EndEntities");
            file_content_lines.Add("$Nodes");
            file_content_lines.Add($"{1} {List_of_intersections.Count} {1} {List_of_intersections.Count}");
            LogBox_add($"Number of generated nodes: {List_of_intersections.Count}");
            //for (int i = 1; i < line_segment_counter + 1; i++)
            //{
            //file_content_lines.Add($"{i} 1 0 {number_of_point_ineach_segment[i]}");
            file_content_lines.Add($"1 1 0 {List_of_intersections.Count}");
            //Node numbers
            foreach (var item in List_of_intersections.Select((value, index) => new { value, index }))
            {
                //if (item.value[0] == i)
                //{
                file_content_lines.Add($"{item.index + 1}");
                //}
            }
            //Node coordinates
            foreach (var item in List_of_intersections.Select((value, index) => new { value, index }))
            {
                //if (item.value[0] == i)
                //{
                file_content_lines.Add($"{item.value[1]} {item.value[2]} {item.value[3]}");
                // }
            }
            //}
            file_content_lines.Add("$EndNodes");
            file_content_lines.Add("$Elements");

            int[] number_of_point_ineach_segment = new int[line_segment_counter + 1];
            number_of_point_ineach_segment[0] = 0;//number of total elements
            for (int i = 1; i < line_segment_counter + 1; i++)
                number_of_point_ineach_segment[i] = 0;
            foreach (var item in List_of_intersections)
                number_of_point_ineach_segment[Convert.ToInt32(item[0])]++;

           if (chk_quad_mesh.Checked == true)
            {
                file_content_lines.Add($"{line_segment_counter} {(List_of_intersections.Count - line_segment_counter) / 2} {1} {(List_of_intersections.Count - line_segment_counter) / 2}");
                LogBox_add($"Number of generated 2nd order elements: {(List_of_intersections.Count - line_segment_counter) / 2}");
            }
            else
            {
                file_content_lines.Add($"{line_segment_counter} {List_of_intersections.Count- line_segment_counter} {1} {List_of_intersections.Count- line_segment_counter}");
                LogBox_add($"Number of generated elements: {List_of_intersections.Count - line_segment_counter}");
            }


            int element_counter = 0;

            for (int i = 1; i < line_segment_counter + 1; i++)
            {
                
                if (chk_quad_mesh.Checked == true)
                {

                    file_content_lines.Add($"1 {i} 8 {(number_of_point_ineach_segment[i] - 1)/2}");

                    int start_node = 0;
                    if (i > 1)
                        start_node = number_of_point_ineach_segment[i-1];

                    for (int i2 = start_node; i2 < List_of_intersections.Count() - 2; i2 = i2 + 2)
                    {
                        if (i != List_of_intersections[i2][0] || i != List_of_intersections[i2 + 2][0])
                            break;
                        //Console.WriteLine(i2);
                        element_counter++;
                        file_content_lines.Add($"{element_counter} {i2 + 1} {i2 + 3} {i2 + 2}");

                    }

                }
                else
                {
                    file_content_lines.Add($"1 {i} 1 {number_of_point_ineach_segment[i] - 1}");
                    for (int i2 = 0; i2 < List_of_intersections.Count - 1; i2++)
                    {
                        if (i== List_of_intersections[i2][0] && i == List_of_intersections[i2+1][0])
                        {
                            element_counter++;
                            file_content_lines.Add($"{element_counter} {i2 + 1} {i2 + 2}");
                        }
                    }
                }


            }

            file_content_lines.Add("$EndElements");
            System.IO.File.WriteAllLines(working_path + ".msh", file_content_lines);

            draw_gmsh_msh_file(working_path + ".msh", input);

        }
        private void btn_add_mesh_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.Yes;
            foreach (var item in C_variables.GROUP_NAMES_list)
            {
                if (item[2] == combo_part_names_mesh.SelectedItem.ToString())
                {
                    dialogResult = MessageBox.Show($"You already have this group name on the model,\n Are you sure you want to add it again?", "Repeated group name", MessageBoxButtons.YesNo);
                    break;
                }
            }

            if (dialogResult == DialogResult.Yes)
            {

                string path = Path.GetDirectoryName(femix_filepath) + "\\";
                string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
                file_name = file_name.Remove(file_name.Length - 3);
                file_name = file_name + "_parts\\";
                string extensions = "_femix.part";
                string input = combo_part_names_mesh.SelectedItem.ToString();
                string working_path = path + file_name + input + extensions;
                working_path = working_path.Remove(part_path.Length - 5); //remove extension
                working_path += ".msh";


                if (!File.Exists(working_path))
                {
                    MessageBox.Show("The mesh file (.msh) doesn't exist! Generate the mesh first.");
                    return;
                }

                string[] Gmsh_output = new string[10];

                string GMSH_filepath = working_path;
                //string[] argument = new string[5];
                Gmsh_output = GMSHconvert.Convert(GMSH_filepath, new int[5] { C_variables.GROUP_NAMES_list.Count(), C_variables.POINT_COORDINATES_list.Count(), C_variables.SUPPORTS_list.Count(), C_variables.ELEMENT_NODES_list.Count(), C_variables.ELEMENT_PROPERTIES_list.Count() },
                    C_variables.POINT_COORDINATES_list, chk_douplicate_nodes.Checked, false, input, combo_part_type_mesh.SelectedItem.ToString(),radio_quad.Checked,chk_embeding.Checked, null);

                LogBox_add($"New points number will start after {C_variables.POINT_COORDINATES_list.Count()}");
                LogBox_add($"New elements number will start after {C_variables.ELEMENT_NODES_list.Count()}");
                //0 --> Groups
                List<IList<string>> mesh_point_list = new List<IList<string>>();
 
                C_variables.Temp_tag = Gmsh_output[0];
                C_variables.Temp_list.Clear();
                if (C_variables.Temp_tag != null)
                {
                    C_variables.Temp_list = Class_femix_reader.ExtractLine(C_variables.Temp_tag, false);

                    ////////////////
                    bool exist = false;
                    foreach (var row in C_variables.Temp_list)
                    {
                        foreach (var row2 in C_variables.GROUP_NAMES_list) { if (row[2] == row2[2]) { exist = true; break; } else { exist = false; } }
                        if (exist == false)
                        {
                            C_variables.GROUP_NAMES_list.Add(row);
                        }
                    }
                    ///////////////
                    //C_variables.GROUP_NAMES_list.AddRange(C_variables.Temp_list);
                    C_variables.GROUP_NAMES_n = C_variables.GROUP_NAMES_list.Count();
                    C_variables.GROUP_NAMES_tag = creat_Gmesh_imported_segments(C_variables.GROUP_NAMES_list);
                }
                //End Group

                //1 --> point
                C_variables.Temp_tag = Gmsh_output[1];
                C_variables.Temp_list.Clear();
                if (C_variables.Temp_tag != null)
                {
                    C_variables.Temp_list = Class_femix_reader.ExtractLine(C_variables.Temp_tag, false);
                    mesh_point_list = C_variables.Temp_list.ToList();

                    C_variables.POINT_COORDINATES_list.AddRange(C_variables.Temp_list);
                    C_variables.POINT_COORDINATES_n = C_variables.POINT_COORDINATES_list.Count();
                    C_variables.POINT_COORDINATES_tag = creat_Gmesh_imported_segments(C_variables.POINT_COORDINATES_list);
                }
                //End

                //2 --> Support
                C_variables.Temp_tag = Gmsh_output[2];
                C_variables.Temp_list.Clear();
                if (C_variables.Temp_tag != null)
                {
                    C_variables.Temp_list = Class_femix_reader.ExtractLine(C_variables.Temp_tag, false);
                    C_variables.SUPPORTS_list.AddRange(C_variables.Temp_list);
                    C_variables.SUPPORTS_n = C_variables.SUPPORTS_list.Count();
                    C_variables.SUPPORTS_tag = creat_Gmesh_imported_segments(C_variables.SUPPORTS_list);
                }
                //End

                //3 --> Elements
                C_variables.Temp_tag = Gmsh_output[3];
                C_variables.Temp_list.Clear();
                if (C_variables.Temp_tag != null)
                {
                    C_variables.Temp_list = Class_femix_reader.ExtractLine(C_variables.Temp_tag, false);

                    ////////////////
                    //exist = false;
                    //foreach (var row in C_variables.Temp_list)
                    //{
                    //    foreach (var row2 in C_variables.GROUP_NAMES_list) { if (row[2] == row2[2]) { exist = true; } }
                    //    if (exist == false)
                    //    {
                    //        C_variables.ELEMENT_NODES_list.Add(row);
                    //    }
                    //}
                    ///////////////
                    C_variables.ELEMENT_NODES_list.AddRange(C_variables.Temp_list);
                    C_variables.ELEMENT_NODES_n = C_variables.ELEMENT_NODES_list.Count();
                    C_variables.ELEMENT_NODES_tag = creat_Gmesh_imported_segments(C_variables.ELEMENT_NODES_list);
                }
                //End

                //4 --> Elements Properties
                C_variables.Temp_tag = Gmsh_output[4];
                C_variables.Temp_list.Clear();
                if (C_variables.Temp_tag != null)
                {
                    C_variables.Temp_list = Class_femix_reader.ExtractLine(C_variables.Temp_tag, false);
                    C_variables.ELEMENT_PROPERTIES_list.AddRange(C_variables.Temp_list);
                    C_variables.ELEMENT_PROPERTIES_n = C_variables.ELEMENT_PROPERTIES_list.Count();
                    C_variables.ELEMENT_PROPERTIES_tag = creat_Gmesh_imported_segments(C_variables.ELEMENT_PROPERTIES_list);
                }
                //End


                if (chk_douplicate_nodes.Checked == true)
                {
                    progressBar_mesh.Value = 0;
                    progressBar_mesh.Visible = true;

                    string[,] detected_interface_elements = Interface_element_nodes(combo_part_names_mesh.Text, combo_part_type_mesh.Text, 
                        radio_quad.Checked, chk_quad_mesh.Checked, mesh_point_list,C_variables.POINT_COORDINATES_list,C_variables.ELEMENT_NODES_list,C_variables.ELEMENT_PROPERTIES_list);


                    if (detected_interface_elements != null)
                    {
                        int n_row, n_col;
                        n_row = detected_interface_elements.GetLength(0);
                        n_col = detected_interface_elements.GetLength(1);

                        string interface_name = "";
                        string interface_type = "";
                        switch (combo_part_type_mesh.Text)
                        {
                            case "0d-Point":
                                interface_name = combo_part_names_mesh.Text + "_NTERFACE_POINT";
                                interface_type = "_INTERFACE_POINT";
                                break;
                            case "1d-Wire":
                                interface_name = combo_part_names_mesh.Text + "_INTERFACE_LINE";
                                interface_type = "_INTERFACE_LINE_3D";
                                break;
                            case "2d-Shell":
                                if (radio_quad.Checked == true)
                                {
                                    interface_name = combo_part_names_mesh.Text + "_INTERFACE_QUAD";
                                    interface_type = "_INTERFACE_QUAD";
                                }
                                else
                                {
                                    interface_name = combo_part_names_mesh.Text + "_INTERFACE_TRIANG";
                                    interface_type = "_INTERFACE_TRIANG";
                                }
                                break;
                            //case "3d-Solid":
                            //    break;
                            default:
                                break;
                        }

                        int interface_element_start, interface_element_end, element_counter;
                        interface_element_start = C_variables.ELEMENT_NODES_list.Count();
                        element_counter = interface_element_start;
                        C_variables.Temp_list.Clear();
                        List<string> temp_col = new List<string>();
                        temp_col.Clear();
                        for (int i = 0; i < n_row; i++)
                        {
                            temp_col.Clear();
                            temp_col.Add((element_counter - 1).ToString());
                            temp_col.Add(element_counter.ToString());
                            temp_col.Add(n_col.ToString());

                            bool has_null = false;
                            for (int j = 0; j < n_col; j++)
                            {
                                if (detected_interface_elements[i, j] == null)
                                    has_null = true;
                                temp_col.Add(detected_interface_elements[i, j]);
                            }
                            if (has_null == false)
                            {
                                C_variables.Temp_list.Add(temp_col.ToList());
                                element_counter++;
                            }
                        }

                        C_variables.GROUP_NAMES_list.Add(new List<string> { (C_variables.GROUP_NAMES_list.Count() - 1).ToString(), C_variables.GROUP_NAMES_list.Count().ToString(), interface_name });
                        C_variables.GROUP_NAMES_n = C_variables.GROUP_NAMES_list.Count();
                        C_variables.GROUP_NAMES_tag = creat_Gmesh_imported_segments(C_variables.GROUP_NAMES_list);

                        C_variables.ELEMENT_NODES_list.AddRange(C_variables.Temp_list);
                        C_variables.ELEMENT_NODES_n = C_variables.ELEMENT_NODES_list.Count();
                        C_variables.ELEMENT_NODES_tag = creat_Gmesh_imported_segments(C_variables.ELEMENT_NODES_list);

                        interface_element_end = C_variables.ELEMENT_NODES_list.Count();

                        C_variables.Temp_list.Clear();
                        temp_col.Clear();
                        temp_col.Add((C_variables.ELEMENT_PROPERTIES_list.Count() - 1).ToString());
                        temp_col.Add(C_variables.ELEMENT_PROPERTIES_list.Count().ToString());
                        temp_col.Add(interface_name);
                        temp_col.Add("1");//phase
                        if ((interface_element_start + 1) == interface_element_end)
                        {
                            temp_col.Add((interface_element_start + 1).ToString());//range
                        }
                        else
                        {
                            temp_col.Add($"[{interface_element_start + 1}-{interface_element_end}]");//range
                        }
                        temp_col.Add(interface_type);//interface type
                        C_variables.Temp_list.Add(temp_col.ToList());

                        C_variables.ELEMENT_PROPERTIES_list.AddRange(C_variables.Temp_list);
                        C_variables.ELEMENT_PROPERTIES_n = C_variables.ELEMENT_PROPERTIES_list.Count();
                        C_variables.ELEMENT_PROPERTIES_tag = creat_Gmesh_imported_segments(C_variables.ELEMENT_PROPERTIES_list);
                    }

                    progressBar_mesh.Visible = false;
                }
                    


                tree_femix_fill();
                C_variables.femix_string_generator();
                Femixbox1.Text = C_variables.Femix_string;
                draw_objects();

                LogBox_add($"Part \"{input}\" added to the FEMIX model.");
                //Femixbox1.Text = Gmsh_output[0];
            }
        }

        private void tabControl_left_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl_left.SelectedIndex == 0)//Model tab
            {
                combo_part_names.SelectedIndex = -1;
                //combo_part_type.SelectedIndex = -1;
            }
            if (tabControl_left.SelectedIndex == 1)//Mesh tab
            {

                combo_part_names_mesh.Items.Clear();
                foreach (string item in combo_part_names.Items)
                {
                    combo_part_names_mesh.Items.Add(item);
                }
                combo_part_names_mesh.SelectedIndex = -1;
                combo_part_type_mesh.SelectedIndex = -1;
                btn_generate.Enabled = false;
                btn_add_mesh.Enabled = false;
               
            }
            if (tabControl_left.SelectedIndex == 2)//Femix tab
            {
                if (femix_filepath != null)
                    draw_objects();
            }
            if (tabControl_left.SelectedIndex == 3)//Result tab
            {
                //scan_folder_for_results(femix_filepath);
            }

            if (tabControl_left.SelectedIndex == 4)//Graph tab
            {
                KbRen.RenWinControl1.Visible = false;
                ZedGraph.zg1.Visible = true;
                //DG_Layers.Rows.Clear();
            }
            else
            {
                KbRen.RenWinControl1.Visible = true;
                ZedGraph.zg1.Visible = false;
            }
        }
        private void combo_part_names_mesh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_part_names_mesh.SelectedIndex >= 0)
            {
                btn_generate.Enabled = true;
                btn_add_mesh.Enabled = true;
            }
               


            string path = Path.GetDirectoryName(femix_filepath) + @"\";

            KbRen.part_points = new List<List<double>>();

            KbRen.part_lines = new List<List<int>>();
            KbRen.part_surfaces = new List<List<int>>();

            KbRen.extrusion_vector = new double[3];

            KbRen.part_point_select = false;
            KbRen.part_cell_select = false;

            string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            file_name = file_name.Remove(file_name.Length - 3);
            file_name = file_name + "_parts\\";
            string extensions = "_femix.part";
            string input = combo_part_names_mesh.SelectedItem.ToString();
            part_path = path + file_name  + input + extensions;

            string[] linecontent;
            var lines = System.IO.File.ReadLines(part_path);

            combo_part_type_mesh.SelectedIndex = -1;

            foreach (string line in lines)
            {
                if (line != null && line != "")
                {
                    linecontent = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                    if (linecontent[0] == "Part_type")
                    {
                        combo_part_type_mesh.SelectedIndex = Int32.Parse(linecontent[1]);
                        if (Int32.Parse(linecontent[1]) == 3)
                            KbRen.part_hide_extrusion = false;
                    }

                    if (linecontent[0] == "extrude_x")
                    {
                        KbRen.extrusion_vector[0] = double.Parse(linecontent[1]);
                    }

                    if (linecontent[0] == "extrude_y")
                    {
                        KbRen.extrusion_vector[1] = double.Parse(linecontent[1]);
                    }

                    if (linecontent[0] == "extrude_z")
                    {
                        KbRen.extrusion_vector[2] = double.Parse(linecontent[1]);
                    }

                    if (linecontent[0] == "point")
                    {
                        KbRen.part_points.Add(new List<double> { double.Parse(linecontent[1]), double.Parse(linecontent[2]), double.Parse(linecontent[3]) });
                    }
                    if (linecontent[0] == "line")
                    {
                        KbRen.part_lines.Add(new List<int> { Int32.Parse(linecontent[1]), Int32.Parse(linecontent[2]) });
                    }
                    if (linecontent[0] == "surface")
                    {
                        List<int> surface_vertices = new List<int>();

                        for (int i = 1; i < linecontent.Count(); i++)
                            surface_vertices.Add(Int32.Parse(linecontent[i]));

                        KbRen.part_surfaces.Add(surface_vertices);
                    }
                }
            }

            string msh_file_path = part_path;
            msh_file_path = part_path.Remove(part_path.Length - 5) + ".msh";
            bool file_exist = false;
            file_exist = File.Exists(msh_file_path);

            if (combo_part_type_mesh.SelectedIndex == 1 && chk_embeding.Checked == true)
                file_exist = false;

            if (file_exist)
            {
                draw_gmsh_msh_file(msh_file_path, input);
            }
            else
            {
                KbRen.Part_draw();
            }

            KbRen.FitView();

        }

        private void combo_part_type_mesh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_part_type_mesh.SelectedIndex == 1)
            {
                chk_embeding.Enabled = true;
            }
            else
            {
                chk_embeding.Enabled = false;
            }

        }

        private void chk_structured_mesh_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_structured_mesh.Checked == true)
            {
                lbl_transfinite.Enabled = true;
                txt_transfinite.Enabled = true;
            }
            else
            {
                lbl_transfinite.Enabled = false;
                txt_transfinite.Enabled = false;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            KbRen.part_point_select = true;
            KbRen.part_cell_select = false;
            KbRen.part_hide_extrusion = true;
            KbRen.Part_draw();
            LogBox_add(Environment.NewLine + "Copy points: Select a point or enter point(s) id(s)?");
        }

        private void toolStrip_editinput_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(femix_filepath) + @"\";
            string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            file_name = file_name.Remove(file_name.Length - 3) + "_";
            file_name = file_name + "parts\\";
            string extensions = "_femix.part";
            string input = combo_part_names.SelectedItem.ToString();
            Process.Start("notepad.exe", path +file_name  + input + extensions);
            
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        
        private void btn_plotgraph_Click(object sender, EventArgs e)
        {
            if (txt_xaxis_nodes.Text=="" || txt_yaxis_nodes.Text == "")
            {
                MessageBox.Show("Before plot, you need to select the list of the nodes!","Notice");
                return;
            }
            
            this.UseWaitCursor = true;

            ZedGraph.CreateGraph_data(femix_filepath, combo_xaxis.SelectedItem.ToString(), txt_xaxis_nodes.Text, txt_xmulti.Text , combo_x_direction.SelectedIndex, combo_yaxis.SelectedItem.ToString(), txt_yaxis_nodes.Text, txt_ymulti.Text, combo_y_direction.SelectedIndex, chk_keepold.Checked, chk_majoraxis.Checked, chk_minoraxis.Checked);
            KbRen.RenWinControl1.Visible = false;
            ZedGraph.zg1.Visible = true;
            btn_graphdata.Enabled = true;
            this.UseWaitCursor = false;
            this.ZedGraph.Creat_Experimental_graph(this.femix_filepath, "experimental.csv");
        }

        private void btn_graphdata_Click(object sender, EventArgs e)
        {
           ZedGraph.export_table_data(femix_filepath);
        }

        private void btn_xnode_select_Click(object sender, EventArgs e)
        {
            btn_xnode_select_end.Enabled = true;
            KbRen.RenWinControl1.Visible = true;
            ZedGraph.zg1.Visible = false;

            KbRen.SelectionPoints = vtkPoints.New();

            string user_input = txt_xaxis_nodes.Text;
            string[] user_input_array;
            user_input_array = user_input.Split(new string[] { "," ," "}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in user_input_array)
            {
                if (item == "")
                    continue;
                KbRen.SelectionPoints.InsertNextPoint(double.Parse(C_variables.POINT_COORDINATES_list[Int32.Parse(item)-1][2]),
                    double.Parse(C_variables.POINT_COORDINATES_list[Int32.Parse(item)-1][3]),
                    double.Parse(C_variables.POINT_COORDINATES_list[Int32.Parse(item)-1][4]));
            }
            if (user_input_array.Count() > 0)
            {
                KbRen.draw_selected_points();
                KbRen.Refresh();
            }
                

            LogBox_add("Select list of nodes and click on the end select button...");
            KbRen.SelectionMode = true;
            toolStrip_select.Checked = true;

        }

        private void btn_xnode_select_end_Click(object sender, EventArgs e)
        {
            btn_xnode_select_end.Enabled = false;
            KbRen.RenWinControl1.Visible = false;
            ZedGraph.zg1.Visible = true;

            KbRen.SelectionMode = false;
            toolStrip_select.Checked = false;

            txt_xaxis_nodes.Text = "";
            for (int i = 0; i < KbRen.SelectionPoints.GetNumberOfPoints(); i++)
            {
                foreach (var item in C_variables.POINT_COORDINATES_list)
                {
                    if (Math.Abs(KbRen.SelectionPoints.GetPoint(i)[0] - double.Parse(item[2])) < 1e-3 &&
                        Math.Abs(KbRen.SelectionPoints.GetPoint(i)[1] - double.Parse(item[3])) < 1e-3 &&
                        Math.Abs(KbRen.SelectionPoints.GetPoint(i)[2] - double.Parse(item[4])) < 1e-3)
                    {
                        txt_xaxis_nodes.Text += item[1] + ",";
                    }
                }
            }

            KbRen.Renderer.RemoveActor(KbRen.SelectionActor);
            KbRen.Refresh();

        }

        private void txt_xaxis_nodes_TextChanged(object sender, EventArgs e)
        {
            btn_xnode_select_end.Enabled = false;
        }

        private void txt_yaxis_nodes_TextChanged(object sender, EventArgs e)
        {
            btn_ynode_select_end.Enabled = false;
        }

        private void btn_ynode_select_Click(object sender, EventArgs e)
        {
            btn_ynode_select_end.Enabled = true;
            KbRen.RenWinControl1.Visible = true;
            ZedGraph.zg1.Visible = false;

            KbRen.SelectionPoints = vtkPoints.New();

            string user_input = txt_yaxis_nodes.Text;
            string[] user_input_array;
            user_input_array = user_input.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in user_input_array)
            {
                if (item == "")
                    continue;
                KbRen.SelectionPoints.InsertNextPoint(double.Parse(C_variables.POINT_COORDINATES_list[Int32.Parse(item) - 1][2]),
                    double.Parse(C_variables.POINT_COORDINATES_list[Int32.Parse(item) - 1][3]),
                    double.Parse(C_variables.POINT_COORDINATES_list[Int32.Parse(item) - 1][4]));
            }
            if (user_input_array.Count() > 0)
            {
                KbRen.draw_selected_points();
                KbRen.Refresh();
            }


            LogBox_add("Select list of nodes and click on the end select button...");
            KbRen.SelectionMode = true;
            toolStrip_select.Checked = true;
        }

        private void btn_ynode_select_end_Click(object sender, EventArgs e)
        {
            btn_ynode_select_end.Enabled = false;
            KbRen.RenWinControl1.Visible = false;
            ZedGraph.zg1.Visible = true;

            KbRen.SelectionMode = false;
            toolStrip_select.Checked = false;

            txt_yaxis_nodes.Text = "";
            for (int i = 0; i < KbRen.SelectionPoints.GetNumberOfPoints(); i++)
            {
                foreach (var item in C_variables.POINT_COORDINATES_list)
                {
                    if (Math.Abs(KbRen.SelectionPoints.GetPoint(i)[0] - double.Parse(item[2])) < 1e-3 &&
                        Math.Abs(KbRen.SelectionPoints.GetPoint(i)[1] - double.Parse(item[3])) < 1e-3 &&
                        Math.Abs(KbRen.SelectionPoints.GetPoint(i)[2] - double.Parse(item[4])) < 1e-3)
                    {
                        txt_yaxis_nodes.Text += item[1] + ",";
                    }
                }
            }

            KbRen.Renderer.RemoveActor(KbRen.SelectionActor);
            KbRen.Refresh();
        }

        private void runPreFEMIXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepathtorun = femix_filepath.Remove(femix_filepath.Length - 7);
            //femix_log.run_prefemix(femix_filepath);
            femix_log.ExecuteCommand(@"FemixExe\Prefemix.exe " + $"\"{filepathtorun}\"", false, true);
        }

        private void runFEMIXToolStripMenuItem_Click(object sender, EventArgs e)
        {
          // femix_log.run_femix(femix_filepath);
            string filepathtorun = femix_filepath.Remove(femix_filepath.Length - 7);
            femix_log.ExecuteCommand(@"FemixExe\Femix.exe " + $"\"{filepathtorun}\"", false, true);
        }

        private void runPosFEMIXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //femix_log.run_postfemix(femix_filepath);

            string filepathtorun = femix_filepath.Remove(femix_filepath.Length - 7);
            femix_log.ExecuteCommand(@"FemixExe\Posfemix.exe " + $"\"{filepathtorun}\"", false, true);

            //scan_folder_for_results(femix_filepath);
        }

        private void cleanProjectFolderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripclean.PerformClick();
        }



        private void toolStrip_deformed_Click(object sender, EventArgs e)
        {
            if (combo_results.SelectedItem.ToString() == "Crack pattern") { return; }

            toolStrip_co_undeformed.Checked = false;
            toolStrip_co_deformed.Checked = true;
            toolStrip_co_both.Checked = false;

            if (combo_step.SelectedIndex != -1)
            {
            Result_Fem.Create_deformed_shape_coordinates(femix_filepath, combo_step.SelectedItem.ToString(), double.Parse(txt_result_scale.Text));
            Result_Fem.draw_deformed_shape(KbRen,false);
            }

        }

        private void txt_result_scale_TextChanged(object sender, EventArgs e)
        {
            combo_step.SelectedIndex = combo_step.SelectedIndex;
        }
        private void toolStrip_co_undeformed_Click(object sender, EventArgs e)
        {
            toolStrip_co_undeformed.Checked = true;
            toolStrip_co_deformed.Checked = false;

            //Result_Fem.draw_undeformed_shape(KbRen);
            Load_results();
        }
        private void toolStrip_co_deformed_Click(object sender, EventArgs e)
        {
            if (combo_results.SelectedItem.ToString() == "Crack pattern") { return; }

            toolStrip_co_undeformed.Checked = false;
            toolStrip_co_deformed.Checked = true;
            toolStrip_co_both.Checked = false;

            //Result_Fem.draw_deformed_shape(KbRen);

            Load_results();
            
        }

        private void toolStrip_co_both_Click(object sender, EventArgs e)
        {
            if (combo_results.SelectedItem.ToString() == "Crack pattern") { return; }
                toolStrip_co_undeformed.Checked = false;
            toolStrip_co_deformed.Checked = false;
            toolStrip_co_both.Checked = true;

            //Result_Fem.draw_both_shape(KbRen);
            Load_results();
        }
        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStrip_discrete_Click(object sender, EventArgs e)
        {
         
        }





        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KbRen.Background_Color(null);
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KbRen.Background_Color("white");
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KbRen.Background_Color("black");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (femix_filepath!=null && femix_toolstip.Enabled == false) {
                DialogResult dialogResult = MessageBox.Show($"Do you want to save changes before exite?", "Femix Pre-Post", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    save_femix_toolstrip.PerformClick();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //Do nothing
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void toolStrip_refresh_result_Click(object sender, EventArgs e)
        {
         
            Load_results();
        }

        private double[] BoundaryRange;
        bool ClipMode = true;
        private void toolStripButton_clip_Click(object sender, EventArgs e)
        {
            BoundaryRange_calc();
            //if (ClipMode == false) ClipMode = true; else ClipMode = false;

            if (femix_filepath == null)
                return;

            if (ClipMode == true)
            {

                KbRen.ShowClip(ClipMode, BoundaryRange);

                toolStripButton_clip.Checked = true;
                ClipMode = false;
            }
            else
            {
                KbRen.ShowClip(ClipMode, null);
                toolStripButton_clip.Checked = false;
                ClipMode = true;
            }
            KbRen.Refresh();
        }
        
        private void BoundaryRange_calc()
        {
            int nPts1 = C_variables.POINT_COORDINATES_list.Count();
            BoundaryRange = new double[6];
            BoundaryRange[0] = double.Parse(C_variables.POINT_COORDINATES_list[0][2]); BoundaryRange[1] = double.Parse(C_variables.POINT_COORDINATES_list[0][2]); //X min,max
            BoundaryRange[2] = double.Parse(C_variables.POINT_COORDINATES_list[0][3]); BoundaryRange[3] = double.Parse(C_variables.POINT_COORDINATES_list[0][3]); //Y min,max
            BoundaryRange[4] = double.Parse(C_variables.POINT_COORDINATES_list[0][4]); BoundaryRange[5] = double.Parse(C_variables.POINT_COORDINATES_list[0][4]); //Z min,max

            for (int i = 0; i < nPts1; i++)
            {
                if (double.Parse(C_variables.POINT_COORDINATES_list[i][2]) < BoundaryRange[0])
                    BoundaryRange[0] = double.Parse(C_variables.POINT_COORDINATES_list[i][2]);//x min
                if (double.Parse(C_variables.POINT_COORDINATES_list[i][2]) > BoundaryRange[1])
                    BoundaryRange[1] = double.Parse(C_variables.POINT_COORDINATES_list[i][2]);//x max
                if (double.Parse(C_variables.POINT_COORDINATES_list[i][3]) < BoundaryRange[2])
                    BoundaryRange[2] = double.Parse(C_variables.POINT_COORDINATES_list[i][3]);//y min
                if (double.Parse(C_variables.POINT_COORDINATES_list[i][3]) > BoundaryRange[3])
                    BoundaryRange[3] = double.Parse(C_variables.POINT_COORDINATES_list[i][3]);//y max
                if (double.Parse(C_variables.POINT_COORDINATES_list[i][4]) < BoundaryRange[4])
                    BoundaryRange[4] = double.Parse(C_variables.POINT_COORDINATES_list[i][4]);//z min
                if (double.Parse(C_variables.POINT_COORDINATES_list[i][4]) > BoundaryRange[5])
                    BoundaryRange[5] = double.Parse(C_variables.POINT_COORDINATES_list[i][4]);//z max
            }
        }
        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string export_file_name = "";
            saveFileDialog_femix.Filter = "Paraview .vtu|*.vtu";
            saveFileDialog_femix.Title = "Export model to Paraview";
            DialogResult result = saveFileDialog_femix.ShowDialog();
            if (result == DialogResult.OK)
            {
                export_file_name = saveFileDialog_femix.FileName;

                export_file_name = Path.GetDirectoryName(export_file_name) + @"\" + Path.GetFileNameWithoutExtension(export_file_name);
                if (combo_step.SelectedIndex != -1)
                    export_file_name += "_" + combo_results.SelectedItem.ToString() + "_" + combo_sub_result.SelectedItem.ToString() + "_" + combo_step.SelectedItem.ToString();
                export_file_name += ".vtu";
                //MessageBox.Show(export_file_name);
                KbRen.export(export_file_name);
            }
        }

        private void toolStrip4_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStrip_Discretize_Click(object sender, EventArgs e)
        {
            if (toolStrip_Discretize.Checked == true)
            {
                toolStrip_Discretize.Checked = false;
            }
            else
            {
                toolStrip_Discretize.Checked = true;
            }
            Load_results();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BoundaryRange_calc();
            KbRen.apply_ResampleToImage_filter(BoundaryRange);
        }

        private void DG_Layers_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DG_Layers.IsCurrentCellDirty)
            {
                DG_Layers.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        bool shrink_click = false;
        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            if (shrink_click == true)
            {
                shrink_click = false;
                toolStrip_shrink.Checked = false;
            }
            else
            {
                shrink_click = true;
                toolStrip_shrink.Checked = true;
            }

            KbRen.Shrink_filter(0.75, toolStrip_shrink.Checked);
        }
        private void clear_unused_elements_and_points()
        {

            //sort element properties
            for (int i1 = 0; i1 < C_variables.ELEMENT_PROPERTIES_list.Count(); i1++)
            {
                string[] elementrange1 = C_variables.ELEMENT_PROPERTIES_list[i1][4].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i2 = i1 + 1; i2 < C_variables.ELEMENT_PROPERTIES_list.Count(); i2++)
                {
                    string[] elementrange2 = C_variables.ELEMENT_PROPERTIES_list[i2][4].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                    if (Int32.Parse(elementrange2[0]) < Int32.Parse(elementrange1[0]))
                    {
                        List<string> row_index1 = new List<string>();
                        List<string> row_index2 = new List<string>();
                        row_index1 = C_variables.ELEMENT_PROPERTIES_list[i1].ToList();
                        row_index2 = C_variables.ELEMENT_PROPERTIES_list[i2].ToList();
                        C_variables.ELEMENT_PROPERTIES_list[i1] = row_index2;
                        C_variables.ELEMENT_PROPERTIES_list[i2] = row_index1;
                        i1 = i1 - 1;
                        break;
                    }
                }
            }


            ////detect and remove unused elements
            //int index_before = 0;
            //int index_current = 0;
            //for (int i1 = 0; i1 < C_variables.ELEMENT_PROPERTIES_list.Count(); i1++)
            //{
            //    string[] elementrange1 = C_variables.ELEMENT_PROPERTIES_list[i1][4].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
            //    index_current = Int32.Parse(elementrange1[0]);

            //    if (i1 == 0)
            //        index_before = 0;
            //    if (i1 > 0)
            //    {
            //        string[] elementrange0 = C_variables.ELEMENT_PROPERTIES_list[i1 - 1][4].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
            //        if (elementrange0.Count() == 1)
            //        {
            //            index_before = Int32.Parse(elementrange0[0]);
            //        }
            //        else
            //        {
            //            index_before = Int32.Parse(elementrange0[1]);
            //        }

            //        if (i1 == C_variables.ELEMENT_PROPERTIES_list.Count() - 1)//remove elements after last element
            //            C_variables.ELEMENT_NODES_list.RemoveRange(Int32.Parse(elementrange1[1]), C_variables.ELEMENT_NODES_list.Count() - Int32.Parse(elementrange1[1]));

            //    }


            //    if (index_current != (index_before + 1))
            //    {
            //        for (int i2 = i1; i2 < C_variables.ELEMENT_PROPERTIES_list.Count(); i2++)
            //        {
            //            string new_range = "";
            //            List<string> row_index1 = new List<string>();
            //            row_index1 = C_variables.ELEMENT_PROPERTIES_list[i2].ToList();
            //            row_index1.RemoveAt(4);//remove current range
            //            elementrange1 = C_variables.ELEMENT_PROPERTIES_list[i2][4].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
            //            if (elementrange1.Count() == 1)
            //            {
            //                new_range = (Int32.Parse(elementrange1[0]) - (index_current - index_before - 1)).ToString();
            //            }
            //            else
            //            {
            //                new_range = "[" + (Int32.Parse(elementrange1[0]) - (index_current - index_before - 1)).ToString() + "-" +
            //                     (Int32.Parse(elementrange1[1]) - (index_current - index_before - 1)).ToString() + "]";
            //            }
            //            row_index1.Insert(4, new_range);
            //            C_variables.ELEMENT_PROPERTIES_list[i2] = row_index1;

            //        }
            //        //remove elements
            //        C_variables.ELEMENT_NODES_list.RemoveRange(index_before, (index_current - index_before - 1));
            //    }
            //}

            //elements
            List<string> used_element_list = new List<string>();
            used_element_list.Clear();
            string[] element_map_array = new string[C_variables.ELEMENT_NODES_list.Count()];
            string element_name="";
            bool alreadyExist=false;

            //element properties
            foreach (var item in C_variables.ELEMENT_PROPERTIES_list)
            {
                string[] elementrange1 = item[4].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                int start, end;
                start = Int32.Parse(elementrange1[0]);
                if (elementrange1.Count() == 1)
                { end = start; }
                else { end = Int32.Parse(elementrange1[1]); }
                for (int i = start; i <= end; i++)
                {
                    element_name = i.ToString();
                    alreadyExist = used_element_list.Contains(element_name);
                    if (alreadyExist == false)
                        used_element_list.Add(element_name);
                }
            }

            //remove unused elements
            List<IList<string>> temp_element_list = new List<IList<string>>();
            temp_element_list.Clear();
            foreach (var item in C_variables.ELEMENT_NODES_list)
            {
                element_name = item[1];
                alreadyExist = used_element_list.Contains(element_name);
                if (alreadyExist)
                {
                    temp_element_list.Add(item);
                    element_map_array[Int32.Parse(element_name) - 1] = (temp_element_list.Count()).ToString();
                }
            }

            C_variables.ELEMENT_NODES_list = temp_element_list.ToList();

            //update node names
            //element properties
            for (int i = 0; i < C_variables.ELEMENT_PROPERTIES_list.Count(); i++)
            {
                string[] elementrange1 = C_variables.ELEMENT_PROPERTIES_list[i][4].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                string new_range = "";

                if (elementrange1.Count() == 1)
                {
                    new_range = element_map_array[Int32.Parse(elementrange1[0]) - 1];
                }
                else
                {
                    new_range = "[" + element_map_array[Int32.Parse(elementrange1[0]) - 1] + "-" +
                        element_map_array[Int32.Parse(elementrange1[1]) - 1] + "]";
                }
                C_variables.ELEMENT_PROPERTIES_list[i][4] = new_range;
            }
            //edge load
            for (int i = 0; i < C_variables.EDGE_LOADS_list.Count(); i++)
            {
                string[] elementrange1 = C_variables.EDGE_LOADS_list[i][2].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                string new_range = "";

                if (elementrange1.Count() == 1)
                {
                    new_range = element_map_array[Int32.Parse(elementrange1[0]) - 1];
                }
                else
                {
                    new_range = "[" + element_map_array[Int32.Parse(elementrange1[0]) - 1] + "-" +
                        element_map_array[Int32.Parse(elementrange1[1]) - 1] + "]";
                }
                C_variables.EDGE_LOADS_list[i][2] = new_range;
            }


            #region node
            /////nodes
            List<string> used_point_list = new List<string>();
            used_point_list.Clear();
            string[] point_map_array = new string[C_variables.POINT_COORDINATES_list.Count()];
            string node_name = "";
            alreadyExist=false;
            //elements
            foreach (var item in C_variables.ELEMENT_NODES_list)
            {
                for (int col_i = 3; col_i < 3 + Int32.Parse(item[2]); col_i++)
                {
                    node_name = item[col_i];
                    alreadyExist = used_point_list.Contains(node_name);
                    if (alreadyExist == false)
                    {
                        used_point_list.Add(node_name);
                    }
                }
            }
            //point load
            foreach (var item in C_variables.POINT_LOADS_list)
            {
                string[] elementrange1 = item[2].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                int start, end;
                start = Int32.Parse(elementrange1[0]); 
                if (elementrange1.Count() == 1)
                   {end = start;}else{end = Int32.Parse(elementrange1[1]);}
                for (int i = start; i <= end; i++)
                {
                    node_name = i.ToString();
                    alreadyExist = used_point_list.Contains(node_name);
                    if (alreadyExist == false)
                        used_point_list.Add(node_name);
                }
            }
            //support
            foreach (var item in C_variables.SUPPORTS_list)
            {
                string[] elementrange1 = item[4].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                int start, end;
                start = Int32.Parse(elementrange1[0]);
                if (elementrange1.Count() == 1)
                { end = start; }
                else { end = Int32.Parse(elementrange1[1]); }
                for (int i = start; i <= end; i++)
                {
                    node_name = i.ToString();
                    alreadyExist = used_point_list.Contains(node_name);
                    if (alreadyExist == false)
                        used_point_list.Add(node_name);
                }
            }

            //remove unused points
            List<IList<string>> temp_list = new List<IList<string>>();
            temp_list.Clear();
            foreach (var item in C_variables.POINT_COORDINATES_list)
            {
                node_name = item[1];
                alreadyExist = used_point_list.Contains(node_name);
                if (alreadyExist)
                {
                    temp_list.Add(item);
                    point_map_array[Int32.Parse(node_name) - 1] = (temp_list.Count()).ToString();
                }
            }

            C_variables.POINT_COORDINATES_list = temp_list.ToList();

            //update node names
            //elements
            for (int i = 0; i < C_variables.ELEMENT_NODES_list.Count(); i++)
            {
                for (int col_i = 3; col_i < 3 + Int32.Parse(C_variables.ELEMENT_NODES_list[i][2]); col_i++)
                {
                    C_variables.ELEMENT_NODES_list[i][col_i] = point_map_array[Int32.Parse(C_variables.ELEMENT_NODES_list[i][col_i]) - 1];
                }
            }
            //point load
            for (int i = 0; i < C_variables.POINT_LOADS_list.Count(); i++)
            {
                string[] elementrange1 = C_variables.POINT_LOADS_list[i][2].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                string new_range = "";
                
                if (elementrange1.Count() == 1)
                {
                    new_range = point_map_array[Int32.Parse(elementrange1[0]) - 1];
                } else {
                    new_range ="["+ point_map_array[Int32.Parse(elementrange1[0]) - 1] + "-"+
                        point_map_array[Int32.Parse(elementrange1[1]) - 1] + "]";
                }
                C_variables.POINT_LOADS_list[i][2] = new_range;

            }
            //support
            for (int i = 0; i < C_variables.SUPPORTS_list.Count(); i++)
            {
                string[] elementrange1 = C_variables.SUPPORTS_list[i][4].Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                string new_range = "";

                if (elementrange1.Count() == 1)
                {
                    new_range = point_map_array[Int32.Parse(elementrange1[0]) - 1];
                }
                else
                {
                    new_range = "[" + point_map_array[Int32.Parse(elementrange1[0]) - 1] + "-" +
                        point_map_array[Int32.Parse(elementrange1[1]) - 1] + "]";
                }
                C_variables.SUPPORTS_list[i][4] = new_range;

            }

            ///end of nodes
            #endregion node
            
            C_variables.EDGE_LOADS_tag = creat_Gmesh_imported_segments(C_variables.EDGE_LOADS_list);

            C_variables.POINT_LOADS_tag = creat_Gmesh_imported_segments(C_variables.POINT_LOADS_list);
            C_variables.SUPPORTS_tag = creat_Gmesh_imported_segments(C_variables.SUPPORTS_list);

            C_variables.POINT_COORDINATES_n = C_variables.POINT_COORDINATES_list.Count();
            C_variables.POINT_COORDINATES_tag = creat_Gmesh_imported_segments(C_variables.POINT_COORDINATES_list);

            C_variables.ELEMENT_NODES_n = C_variables.ELEMENT_NODES_list.Count();
            C_variables.ELEMENT_NODES_tag = creat_Gmesh_imported_segments(C_variables.ELEMENT_NODES_list);

            C_variables.ELEMENT_PROPERTIES_tag = creat_Gmesh_imported_segments(C_variables.ELEMENT_PROPERTIES_list);


            C_variables.Load_tag_generator();
            C_variables.femix_string_generator();
            reload_textbox_and_tree(C_variables.Femix_string);

        }
        private void removeUnusedElementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear_unused_elements_and_points();

        }

        private void runDrawmeshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"FemixExe\drawmesh.exe");
        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void removeUnusedPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            
          
        }

        private void chk_douplicate_nodes_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void label22_Click(object sender, EventArgs e)
        {
            if (chk_douplicate_nodes.Checked == true)
                chk_douplicate_nodes.Checked = false;
            else
                chk_douplicate_nodes.Checked = true;
        }

        private void toolStripleft_main_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "MAIN_PARAMETERS";
        }

        private void toolStripButton_left_support_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "SUPPORTS";
        }

        private void linearIsotropicToolStrip_linear_iso_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "LINEAR_ISOTROPIC_MATERIALS";
        }

        private void linearOrtropicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "LINEAR_ORTHOTROPIC_MATERIALS";
        }

        private void nLMM201ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "NLMM201";
        }

        private void nLMM202ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "NLMM202";
        }

        private void nLMM101ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "NLMM101";
        }

        private void nLMM104ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "NLMM104";
        }

        private void nLMM111ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "NLMM111";
        }

        private void nLMM141ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "NLMM141";
        }

        private void nLMM151ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "NLMM151";
        }

        private void nLMM124ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "NLMM124";
        }

        private void lINEARINTERFACEPOINTPROPERTIESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "LINEAR_INTERFACE_POINT_PROPERTIES";
        }

        private void lINEARINTERFACELINEPROPERTIESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "LINEAR_INTERFACE_LINE_PROPERTIES";
        }

        private void lINEARINTERFACESURFACEPROPERTIESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "LINEAR_INTERFACE_SURFACE_PROPERTIES";
        }

        private void nLMM301ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "NLMM301";
        }

        private void nLMM302ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "NLMM302";
        }

        private void nLMM305ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "NLMM305";
        }

        private void laminateThicknessesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "LAMINATE_THICKNESSES";
        }

        private void crossSectionAreasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "CROSS_SECTION_AREAS";
        }

        private void frame2dCrossSectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "FRAME_2D_CROSS_SECTIONS";
        }

        private void frame3dCrossSectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "FRAME_3D_CROSS_SECTIONS";
        }

        private void geometryPatternsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "GEOMETRY_PATTERNS";
        }

        private void toolStripButton_left_element_properties_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "ELEMENT_PROPERTIES";
        }

        private void lOADCASEPARAMETERSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "LOAD_CASE_PARAMETERS";
        }

        private void gRAVITYLOADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "GRAVITY_LOAD";
        }

        private void pOINTLOADSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "POINT_LOADS";
        }

        private void eDGELOADSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "EDGE_LOADS";
        }

        private void fACELOADSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "FACE_LOADS";
        }

        private void tEMPERATUREVARIATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "TEMPERATURE_VARIATION";
        }

        private void pRESCRIBEDDISPLACEMENTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "PRESCRIBED_DISPLACEMENTS";
        }

        private void toolStripButton5_Click_2(object sender, EventArgs e)
        {
            combo_sections.SelectedIndex = 0;
            combo_sections.SelectedItem = "LOAD_CASE_COMBINATIONS";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tab_results_Click(object sender, EventArgs e)
        {

        }

        private static DialogResult ShowInputDialog(ref string input, string title)
        {
            System.Drawing.Size size = new System.Drawing.Size(240, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = title;

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);
            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;
            inputBox.StartPosition = FormStartPosition.CenterParent;
            inputBox.MinimizeBox = false;
            inputBox.MaximizeBox = false;
            inputBox.ShowInTaskbar = false;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

    }

}

