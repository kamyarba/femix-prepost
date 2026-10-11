using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace Femix
{
    public partial class frm_list_items : Form
    {
        public string selected_item { get; set; }
        public bool is_there_change;

        public frm_list_items()
        {
            InitializeComponent();
            this.DG1.DataError += new DataGridViewDataErrorEventHandler(this.DG1_DataError);
        }
        private void DG1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            this.DG1.Rows[e.RowIndex].ErrorText = "The table cell value is not valid or an item has been modified on another form!";
            e.Cancel = true;
        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            is_there_change = false;
            this.Close();
        }


        private void DG1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value is string)
            {
                e.Value = ((string)e.Value).Trim();
                e.Value.ToString().Replace(" ", string.Empty);
                string cellVal1 = (string)e.Value;
                if (cellVal1.Any(Char.IsWhiteSpace)){
                    this.DG1.Rows[e.RowIndex].ErrorText = "The table cell value has white space. try to remove spaces.!";
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            //remove spaces from DataGrid

                string comment1, comment2, _segment;
            switch (selected_item)
            {
                case "MAIN_PARAMETERS":
                    _segment = "\n";
                    foreach (DataGridViewRow row in DG1.Rows)
                    {
                        if (row.Index < 16 || row.Index > 28)//except arc lenght
                        {
                            if (row.Cells[0].Value == null)
                            {
                                MessageBox.Show("Error on input!\n" + row.HeaderCell.Value + " is empty!!");
                                return;
                            }
                            if (row.Visible == true)
                                _segment += $"  {row.HeaderCell.Value} = {row.Cells[0].Value.ToString()} ;\n";
                        }
                    }
                    C_variables.MAIN_PARAMETERS_tag = _segment;
                    C_variables.MAIN_PARAMETERS_list = Class_femix_reader.ExtractLine(C_variables.MAIN_PARAMETERS_tag, false);
                    C_variables.MAIN_PARAMETERS_n = C_variables.MAIN_PARAMETERS_list.Count;

                    _segment = "\n";
                    foreach (DataGridViewRow row in DG1.Rows)
                    {
                        if (row.Index >= 16 && row.Index <= 28 && row.Index != 23)// arc lenght
                        {
                            if (row.Visible == true)
                            {
                                if (row.HeaderCell.Value.ToString() != "MOMENT_ROTATION_SCALING_FACTOR")
                                {
                                    if (row.HeaderCell.Value.ToString() == "DISPLACEMENT CONTROL")
                                    {
                                        if (row.Cells[0].Value.ToString() == "DEFAULT")
                                        {
                                            _segment += $"        DISPLACEMENT_CONTROL = _Y ;\n";
                                        }
                                        else
                                        {
                                            _segment += $"        RELATIVE_DISPLACEMENT_CONTROL = _Y ;\n";
                                        }
                                    }
                                    else
                                    {
                                     _segment += $"        {row.HeaderCell.Value} = {row.Cells[0].Value} ;\n";
                                    }
                                }
                            }
                        }
                    }
                    C_variables.ARC_LENGTH_PARAMETERS_tag = _segment;
                    C_variables.ARC_LENGTH_PARAMETERS_list = Class_femix_reader.ExtractLine(C_variables.ARC_LENGTH_PARAMETERS_tag, false);
                    C_variables.ARC_LENGTH_PARAMETERS_n = C_variables.ARC_LENGTH_PARAMETERS_list.Count;


                    break;
                case "GROUP_NAMES":
                    comment1= "## Declaration of the names of the groups\n# N. of groups\n";
                    comment2 = "\n## Content of each column:\n#  A -> Counter\n#  B -> Name\n#            A        B\n";

                     _segment = fill_segment( comment1, comment2);

                    C_variables.GROUP_NAMES_tag = _segment;
                    C_variables.GROUP_NAMES_list =Class_femix_reader.ExtractLine(C_variables.GROUP_NAMES_tag, false);
                    C_variables.GROUP_NAMES_n = C_variables.GROUP_NAMES_list.Count;

                    break;

                case "POINT_COORDINATES":
                    comment1 = "## Point coordinates (global coordinate system) \n# N. of points \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Coordinate - XG1 \n#  C -> Coordinate - XG2 \n#  D -> Coordinate - XG3 \n#			A		B		C		D\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.POINT_COORDINATES_tag = _segment;
                    C_variables.POINT_COORDINATES_list = Class_femix_reader.ExtractLine(C_variables.POINT_COORDINATES_tag, false);
                    C_variables.POINT_COORDINATES_n = C_variables.POINT_COORDINATES_list.Count;

                    break;

                case "AUXILIARY_VECTORS":
                    comment1 = "## Auxiliary vectors (global coordinate system) \n## All vectors are normalized immediately after input \n# N. of auxiliary vectors \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the auxiliary vector \n#  C -> Component of the auxiliary vector - 1 \n#  D -> Component of the auxiliary vector - 2 \n#  E -> Component of the auxiliary vector - 3 \n#			A	B		C		D      E\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.AUXILIARY_VECTORS_tag = _segment;
                    C_variables.AUXILIARY_VECTORS_list = Class_femix_reader.ExtractLine(C_variables.AUXILIARY_VECTORS_tag, false);
                    C_variables.AUXILIARY_VECTORS_n = C_variables.AUXILIARY_VECTORS_list.Count;

                    break;

                case "COORDINATE_SYSTEMS":
                    comment1 = "## Coordinate systems defined with 3 vectors \n# N. of coordinate systems \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Coordinate system name \n#  C -> Name of the auxiliary vector - 1 \n#  D -> Name of the auxiliary vector - 2 \n#  E -> Name of the auxiliary vector - 3 \n#			A	B		C		D		E\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.COORDINATE_SYSTEMS_tag = _segment;
                    C_variables.COORDINATE_SYSTEMS_list = Class_femix_reader.ExtractLine(C_variables.COORDINATE_SYSTEMS_tag, false);
                    C_variables.COORDINATE_SYSTEMS_n = C_variables.COORDINATE_SYSTEMS_list.Count;

                    break;

                case "POINTS_WITH_SPECIAL_COORDINATE_SYSTEM":
                    comment1 = "## Points with special coordinate system \n# N. of points with special coordinate system \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number (or point number range) \n#  E -> Name of the coordinate system \n#			A	B				C			D   E\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_tag = _segment;
                    C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_list = Class_femix_reader.ExtractLine(C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_tag, false);
                    C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_n = C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_list.Count;

                    break;

                case "SUPPORTS":
                    comment1 = "## Points with fixed degrees of freedom \n# N. of points with fixed degrees of freedom \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number (or point number range) \n#  E -> N. of fixed degrees of freedom in the current point(s) \n#  F -> Fixed degrees of freedom: \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#		A	B			C		D	E		F\n";


                    _segment = "\n";
                    if (DG1.RowCount > 1)
                    {


                        int counter = 0;
                        for (int i = 0; i < DG1.RowCount - 1; i++)
                        {
                            string srting_support = DG1.Rows[i].Cells[2].Value.ToString();
                            int support_start, support_end;
                            string[] elementrange = srting_support.Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                            support_start = Int32.Parse(elementrange[0]);
                            counter++;
                            if (elementrange.Count() > 1)
                            {
                                support_end = Int32.Parse(elementrange[1]);
                                counter = counter + (support_end - support_start);
                            }
                        }

                        _segment += comment1 + $"COUNT = {counter} ;\n" + comment2;

                        counter = 0;
                        for (int i = 0; i < DG1.RowCount - 1; i++)
                        {

                            string srting_support = DG1.Rows[i].Cells[2].Value.ToString();
                            int support_start, support_end;
                            string[] elementrange = srting_support.Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                            support_start = Int32.Parse(elementrange[0]);
                            counter++;
                            if (elementrange.Count() > 1)
                            {      
                                support_end = Int32.Parse(elementrange[1]);
                                _segment += "        " + $"[{counter}-{counter+(support_end- support_start)}]";
                                counter = counter + (support_end - support_start);
                            }
                            else
                            {
                                _segment += "        " + counter;
                            }



                            //_segment += "        " + DG1.Rows[i].HeaderCell.Value;

                            _segment += "        " + DG1.Rows[i].Cells[0].Value;
                            _segment += "        " + DG1.Rows[i].Cells[1].Value;
                            _segment += "        " + DG1.Rows[i].Cells[2].Value;

                            int support_count = 0;
                            for (int j = 3; j < DG1.ColumnCount; j++)
                            {
                                if (DG1.Rows[i].Cells[j].Value !=null)
                                    if (DG1.Rows[i].Cells[j].Value.ToString().ToUpper() != "FALSE")
                                        support_count++;
                                
                            }
                            _segment += "        " + support_count;
                            for (int j = 3; j < DG1.ColumnCount; j++)
                            {
                                if (DG1.Rows[i].Cells[j].Value != null )
                                    if (DG1.Rows[i].Cells[j].Value.ToString().ToUpper() != "FALSE")
                                        _segment += "        " + DG1.Rows[i].Cells[j].Value;
                
                            }

                            _segment += " ;\n";
                        }
                    }

                    C_variables.SUPPORTS_tag = _segment;
                    C_variables.SUPPORTS_list = Class_femix_reader.ExtractLine(C_variables.SUPPORTS_tag, false);
                    C_variables.SUPPORTS_n = C_variables.SUPPORTS_list.Count;

                    break;

                case "INTERNAL_POINT_LINKS":
                    comment1 = "## Points with linked degrees of freedom \n# N. of pairs of points with linked degrees of freedom \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number - M (Master) (or point number range) \n#  E -> Point number - S (Slave) (or point number range) \n#  F -> N. of linked degrees of freedom in the current pair of points \n#  G -> Linked degrees of freedom: \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#		A	B			C		D		E	F		G\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.INTERNAL_POINT_LINKS_tag = _segment;
                    C_variables.INTERNAL_POINT_LINKS_list = Class_femix_reader.ExtractLine(C_variables.INTERNAL_POINT_LINKS_tag, false);
                    C_variables.INTERNAL_POINT_LINKS_n = C_variables.INTERNAL_POINT_LINKS_list.Count;

                    break;

                case "LINEAR_ISOTROPIC_MATERIALS":
                    comment1 = "## Keyword: _LIN_ISO \n## Properties of the linear isotropic materials \n# N. of linear isotropic materials \n";
                    comment2 = " \n## Content of each column: \n#  A -> Counter \n#  B -> Material name \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Young's modulus \n#  F -> Poisson's coefficient \n#		A	B		C		D		E		F\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.LINEAR_ISOTROPIC_MATERIALS_tag = _segment;
                    C_variables.LINEAR_ISOTROPIC_MATERIALS_list = Class_femix_reader.ExtractLine(C_variables.LINEAR_ISOTROPIC_MATERIALS_tag, false);
                    C_variables.LINEAR_ISOTROPIC_MATERIALS_n = C_variables.LINEAR_ISOTROPIC_MATERIALS_list.Count;

                    break;

                case "LINEAR_ORTHOTROPIC_MATERIALS":
                    comment1 = "## Keyword: _LIN_ORTHO \n## Properties of the linear orthotropic materials \n# N. of linear orthotropic materials \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Material name \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient - l1 \n#  E -> Temperature coefficient - l2 \n#  F -> Temperature coefficient - l3 \n#  G -> Young's modulus - l1 \n#  H -> Young's modulus - l2 \n#  I -> Young's modulus - l3 \n#  J -> Shear modulus - l2/l3 \n#  K -> Shear modulus - l3/l1 \n#  L -> Shear modulus - l1/l2 \n#  M -> Poisson's coefficient - l2/l3 \n#  N -> Poisson's coefficient - l3/l1 \n#  O -> Poisson's coefficient - l1/l2 \n#  P -> Name of the coordinate system \n#	A	B		C		D		E		F	G	H	I	J	K	L		M		N		O	P\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.LINEAR_ORTHOTROPIC_MATERIALS_tag = _segment;
                    C_variables.LINEAR_ORTHOTROPIC_MATERIALS_list = Class_femix_reader.ExtractLine(C_variables.LINEAR_ORTHOTROPIC_MATERIALS_tag, false);
                    C_variables.LINEAR_ORTHOTROPIC_MATERIALS_n = C_variables.LINEAR_ORTHOTROPIC_MATERIALS_list.Count;

                    break;

                case "LINEAR_INTERFACE_POINT_PROPERTIES":
                    comment1 = "## Keyword: _LIN_INT_POINT \n## Properties of linear interface elements that link a pair of points \n# N. of types of point interface elements \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the type of point interface element \n#  C -> Degree of freedom with linking spring \n#  D -> Stiffness of the spring \n#		A	B			C			D\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_tag = _segment;
                    C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_list = Class_femix_reader.ExtractLine(C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_tag, false);
                    C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_n = C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_list.Count;

                    break;

                case "LINEAR_INTERFACE_LINE_PROPERTIES":
                    comment1 = "## Keyword: _LIN_INT_LINE \n## Properties of linear interface elements that link a pair of edges (2D only) \n# N. of types of line interface elements \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the type of line interface element \n#  C -> Normal stiffness \n#  D -> Tangential stiffness \n#		A  B				C				D\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_tag = _segment;
                    C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_list = Class_femix_reader.ExtractLine(C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_tag, false);
                    C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_n = C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_list.Count;

                    break;

                case "LINEAR_INTERFACE_SURFACE_PROPERTIES":
                    comment1 = "## Keyword: _LIN_INT_SURF \n## Properties of linear interface elements that link a pair of faces (solid/shell only) \n# N. of types of surface interface elements \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the type of surface interface element \n#  C -> Normal stiffness \n#  D -> Tangential stiffness - s1 \n#  E -> Tangential stiffness - s2 \n#		A	B				C				D				E\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_tag = _segment;
                    C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_list = Class_femix_reader.ExtractLine(C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_tag, false);
                    C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_n = C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_list.Count;

                    break;

                case "NLMM101":
                    comment1 = "## Keyword: _NLMM101 \n## Properties defining the material constitutive law: \n##    CEB-FIP 1993 law for compression behavior \n##    Linear-parabola diagram for tensile up-to-peak behaviour \n##    Trilinear diagram for tensile post-peak behavior \n##    Kinematic hardening rule; associated flow rule; strain hardening; Owen yield criteria \n##    Multi-fixed smeared crack model \n \n# N. of materials modeled by the NLMM101 law \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the constitutive law \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n \n#  G -> Compression peak stress \n#  H -> Strain at compression peak stress \n#  I -> Parameter for defining the compressive strength and rigidity \n#       degradation according to Vecchio and Collins law (0=no degradation) \n#  J -> Unloading and reloading law in compression (_ELASTIC, _SECANT) \n \n#  K -> Tensile peak stress \n#  L -> Strain at tensile peak stress \n#  M -> Up limit tensile stress of the linear branch \n#  N -> Unloading and reloading law for the up-to-peak tensile stress (_ELASTIC, _SECANT) \n \n#  O -> Stress at the first post-peak point of the tensile-softening diagram (fracture mode I) \n#  P -> Crack strain at the first post-peak point of the tensile-softening diagram (fracture mode I) \n#  Q -> Stress at the second post-peak point of the tensile-softening diagram (fracture mode I) \n#  R -> Crack strain at the second post-peak point of the tensile-softening diagram (fracture mode I) \n#  S -> Mode I fracture energy (Gf) \n#  T -> Unloading and reloading law for the tensile post-peak strain softening diagram \n#       (_ELASTIC, _SECANT) (fracture mode I) \n \n#  U -> Parameter to define the shear retention factor for the fracture mode II \n#  V -> Crack band width for the fracture mode I (_SQRT_ELE; _SQRT_GP; _CONST_VALUE) \n#  X -> Criteria for opening a new crack [1: principal tensile stress greater than uniaxial tensile strength (_SI_GT_FCT); \n#                                         2: angle between the principal tensile stress and the existing cracks \n#                                            exceed the value of a threshold angle (_ANG_GT_THRANG); \n#                                         3: 1 AND 2 (_SIGTFCT_AND_ANGGTTHRANG); \n#                                         4: 1 OR 2 (_SIGTFCT_OR_ANGGTTHRANG); \n#  Y -> Threshold angle (zero for the first criteria of the new crack opening); \n#  W -> Parameter to define the mode I fracture energy available for the new crack (zero for the initial Gf) \n#		A	B		C		D		E		F		G		H		I		J		K		L		M		N		O	P		Q	R		S	T		U	V		X			Y		W	\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.NLMM101_tag = _segment;
                    C_variables.NLMM101_list = Class_femix_reader.ExtractLine(C_variables.NLMM101_tag, false);
                    C_variables.NLMM101_n = C_variables.NLMM101_list.Count;

                    break;

                case "NLMM104":
                    comment1 = "## Keyword: _NLMM104 \n## Properties of the NLMM104 material model \n# N. of NLMM104 materials \n ";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Tensile strength \n#  I -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN \n#  J -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  K -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  L -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  M -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  N -> Ratio between the strain at the third post-peak point and the \n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I) \n#  O -> Ratio between the stress at the third post-peak point and the \n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I) \n#  P -> Mode I fracture energy (Gf) \n#  Q -> Type of mode I fracture energy update law \n#       - Available keywords: _NONE, _LINEAR, _QUADRATIC or _CUBIC \n#  R -> Ultimate crack shear sliding (keyword or constant value) \n#       - Available keywords: _MAXIMUM_CRACKWIDTH \n#  S -> Parameter to define the mode I fracture energy available to the new crack \n#  T -> Type of shear-softening diagram \n#       - Available keywords: _NONE or _LINEAR \n#  U -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n#  V -> Type of crack shear modelling appoach (used in shear retention model) \n#       - Available keywords: _INCREMENTAL or _TOTAL \n#  W -> Crack shear strength \n#  X -> Shear fracture energy (Gfs=GfII) (in nonlinear Mindlin shell: In-plane shear fracture energy) \n#  Y -> Type of unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n#  Z -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n# AA -> Maximum n. of cracks \n# AB -> Threshold angle (degress) \n# \n# Out-of-plane shear data: \n#    -> Used in nonlinear Mindlin shell \n# \n# AC -> Type of out-of-plane shear-softening diagram \n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN \n# AD -> Minimum out-of-plane shear stress for softening behavior \n# AE -> Out-of-plane shear fracture energy \n# AF -> Ratio between the out-of-plane shear strain at the first \n#       post-peak point and the ultimate out-of-plane shear strain \n#       trilinear out-of-plane shear-softening diagram \n# AG -> Ratio between the out-of-plane shear stress at the first \n#       post-peak point and the out-of-plane shear strength of the \n#       trilinear out-of-plane shear-softening diagram \n# AH -> Ratio between the out-of-plane shear strain at the second \n#       post-peak point and the ultimate out-of-plane shear strain \n#       of the trilinear out-of-plane shear-softening diagram \n# AI -> Ratio between the out-of-plane shear stress at the second \n#       post-peak point and the out-of-plane shear strength of the \n#       trilinear out-of-plane shear-softening diagram \n \n \n#     A     B     C     D     E     F     G		 H     I     J     K     L     M     N     O	    P     Q     R     S     T     U		   Y     Z     AA     AB     AC     AD     AE	   V     W     X	   AF     AG     AH     AI \n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.NLMM104_tag = _segment;
                    C_variables.NLMM104_list = Class_femix_reader.ExtractLine(C_variables.NLMM104_tag, false);
                    C_variables.NLMM104_n = C_variables.NLMM104_list.Count;

                    break;

                case "NLMM111":
                    comment1 = "## Keyword: _NLMM111 \n## Properties of the NLMM111 material model \n# N. of NLMM111 materials \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Tensile strength \n#  I -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN \n#  J -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  K -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  L -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  M -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  N -> Ratio between the strain at the third post-peak point and the \n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I) \n#  O -> Ratio between the stress at the third post-peak point and the \n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I) \n#  P -> Mode I fracture energy (Gf) \n#  Q -> Parameter to define the mode I fracture energy available to the new crack \n#  R -> Type of shear-softening diagram \n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN \n#  S -> Ratio between the (shear strain minus peak shear strain) at the first post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n#  T -> Ratio between the the shear stress at the first post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n#  U -> Ratio between the (shear strain minus peak shear strain) at the second post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n#  V -> Ratio between the the shear stress at the second post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n#  W -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n#  X -> Type of crack shear modelling appoach (used in shear retention model) \n#       - Available keywords: _INCREMENTAL or _TOTAL \n#  Y -> Crack shear strength \n#  Z -> Shear fracture energy (Gfs=GfII=GfIII) \n# AA -> Type of unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n# AB -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n# AC -> Maximum n. of cracks \n# AD -> Threshold angle (degress) \n \n#     A     B     C     D     E     F     G		   H     I     J     K     L     M     N     O	   P     Q     R     S     T     U     V	   W     X     Y     Z	  AA     AB     AC     AD \n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.NLMM111_tag = _segment;
                    C_variables.NLMM111_list = Class_femix_reader.ExtractLine(C_variables.NLMM111_tag, false);
                    C_variables.NLMM111_n = C_variables.NLMM111_list.Count;

                    break;

                case "NLMM141":
                    comment1 = "## Keyword: _NLMM141 \n## Properties of the NLMM141 material model \n# N. of NLMM141 materials \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Strain at compression peak stress \n#  I -> Parameter defining the initial yield surface \n#  J -> Compression/stiffness degradation parameter due to concrete cracking \n#  K -> Tensile strength \n#  L -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR or _CORNELISSEN \n#  M -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear tensile-softening diagram (fracture mode I) \n#  N -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear tensile-softening diagram (fracture mode I) \n#  O -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear tensile-softening diagram (fracture mode I) \n#  P -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear tensile-softening diagram (fracture mode I) \n#  Q -> Mode I fracture energy (Gf) \n#  R -> Parameter to define the mode I fracture energy available to the new crack \n#  S -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n#  T -> Type of tensile unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n#  U -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n#  V -> Maximum n. of cracks \n#  X -> Threshold angle (degrees) \n \n#     A     B     C     D     E     F     G     H	  I     J     K     L     M     N     O     P	   Q     R     S     T     U     V     X \n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.NLMM141_tag = _segment;
                    C_variables.NLMM141_list = Class_femix_reader.ExtractLine(C_variables.NLMM141_tag, false);
                    C_variables.NLMM141_n = C_variables.NLMM141_list.Count;

                    break;

                case "NLMM151":
                    comment1 = "## Keyword: _NLMM151 \n## Properties of the NLMM151 material model \n # N. of NLMM151 materials \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Strain at compressive strength \n#  I -> Parameter defining the linear part of the compressive strength \n#  J -> Compressive fracture energy \n#  K -> Type of hardening function after peak load \n#       - Available keywords: _CEB_FIP93 or _PERFECT_PLASTIC \n#  L -> Volume fraction of fibers (Vf) \n# \n#       Note: for plane concrete a Value of zero should be set for this parameter \n# \n#  M -> Aspect ratio of fibers (lf/Df) \n#  N -> Length efficiency factor of fibers \n#  O -> Orientation factor of fibers \n#  P -> Ultimate bond strength \n#  Q -> Tensile strength \n#  R -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN \n#  S -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  T -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  U -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  V -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  W -> Ratio between the strain at the third post-peak point and the \n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I) \n#  X -> Ratio between the stress at the third post-peak point and the \n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I) \n#  Y -> Mode I fracture energy (Gf) \n#  Z -> Parameter to define the mode I fracture energy available to the new crack \n# AA -> Type of shear-softening diagram \n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN \n# AB -> Ratio between the (shear strain minus peak shear strain) at the first post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n# AC -> Ratio between the the shear stress at the first post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n# AD -> Ratio between the (shear strain minus peak shear strain) at the second post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n# AE -> Ratio between the the shear stress at the second post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n# AF -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n# AG -> Type of crack shear modelling approach (used in shear retention model) \n#       - Available keywords: _INCREMENTAL or _TOTAL \n# AH -> Crack shear strength \n# AI -> Shear fracture energy (Gfs=GfII=GfIII) \n# AJ -> Type of unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n# AK -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n# AL -> Maximum n. of cracks \n# AM -> Threshold angle (degrees) \n \n#     A     B     C     D     E     F     G     H     I     J     K     L     M     N     O     P     Q     R     S     T     U     V     W     X     Y     Z     AA     AB     AC      AD     AE     AF     AG     AH     AI	     AJ     AK     AL     AM \n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.NLMM151_tag = _segment;
                    C_variables.NLMM151_list = Class_femix_reader.ExtractLine(C_variables.NLMM151_tag, false);
                    C_variables.NLMM151_n = C_variables.NLMM151_list.Count;

                    break;

                case "NLMM201":
                    comment1 = "## Properties of the NLMM201 material model \n## Keyword: _NLMM201 \n # N. of NLMM201 materials \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Strain at the end of the first branch \n#  F -> Stress at the end of the first branch \n#  G -> Strain at the end of the second branch \n#  H -> Stress at the end of the second branch \n#  I -> Strain at the end of the third branch \n#  J -> Stress at the end of the third branch \n#  K -> Third branch exponent \n \n#			A			B			C			D			E			F			G			H			I			J			K \n";
                    _segment = fill_segment(comment1, comment2);
                    C_variables.NLMM201_tag = _segment;
                    C_variables.NLMM201_list = Class_femix_reader.ExtractLine(C_variables.NLMM201_tag, false);
                    C_variables.NLMM201_n = C_variables.NLMM201_list.Count;
                    break;

                case "NLMM202":
                    comment1 = "## Properties of the NLMM202 material model\n## Keyword: _NLMM202\n# N. of NLMM202 materials  \n";
                    comment2 = "\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Mass per unit volume\n#  D -> Temperature coefficient\n#  E -> Elastic modulus\n#  F -> Tangent modulus at strain hardening\n#  G -> Yielding strain\n#  H -> Yielding stress\n#  I -> Hardening strain\n#  J -> Hardening stress\n#  K -> Strain at the ultimate stress\n#  L -> Ultimate stress\n#  A  B        C        D       E         F 		 G        H      I      J      K      L  \n";
                    _segment = fill_segment(comment1, comment2);
                    C_variables.NLMM202_tag = _segment;
                    C_variables.NLMM202_list = Class_femix_reader.ExtractLine(C_variables.NLMM202_tag, false);
                    C_variables.NLMM202_n = C_variables.NLMM202_list.Count;
                    break;

                case "NLMM210":
                    comment1 = "## Properties of the NLMM210 material model\n## Keyword: _NLMM210\n# N. of NLMM210 materials  \n";
                    comment2 = "\nn## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Mass per unit volume\n#  D -> Temperature coefficient\n#  E -> Concrete initial Young modulus\n#  F -> Concrete strain at peak confined stress\n#  G -> Confined concrete stress\n#  H -> Non-dimensional critical strain on the compression envelope\n#  I -> Strain at peak tension stress\n#  J -> Concrete tension strength\n#  K -> Critical strain on the tension envelope curve\n#  A  B       C        D        E        F 			G      H        I         J        K  \n";
                    _segment = fill_segment(comment1, comment2);
                    C_variables.NLMM210_tag = _segment;
                    C_variables.NLMM210_list = Class_femix_reader.ExtractLine(C_variables.NLMM210_tag, false);
                    C_variables.NLMM210_n = C_variables.NLMM210_list.Count;
                    break;

                case "NLMM220":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM298":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM299":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM123":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM121":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM124":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM125":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM142":
                    comment1 = "## Properties of the NLMM142 material model\n## Keyword: _NLMM142  \n";
                    comment2 = "\n\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Mass per unit volume\n#  D -> Temperature coefficient\n#  E -> Poisson's coefficient\n#  F -> Young's modulus\n#  G -> Compressive strength\n#  H -> Strain at compression peak stress\n#  I -> Parameter defining the initial yield surface\n#  J -> Type of hardeding function after peak load\n#       - Available keywords: _CEB_FIP93 or _PERFECT_PLASTIC\n#  K -> Volume fraction of fibers (Vf)\n#\n#       Note: for plane concrete a Value of zero should be set for this parameter\n#\n#  L -> Aspect ratio of fibers (lf/Df)\n#  M -> Length efficiency factor of fibers\n#  N -> Orientation factor of fibers\n#  O -> Ultimate bond strength\n#  P -> Tensile strength\n#  Q -> Type of tensile-softening diagram\n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN\n#  R -> Ratio between the strain at the first post-peak point and the\n#      ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  S -> Ratio between the stress at the first post-peak point and the\n#     tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  T -> Ratio between the strain at the second post-peak point and the\n#      ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  U -> Ratio between the stress at the second post-peak point and the\n#     tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  V -> Ratio between the strain at the third post-peak point and the\n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I)\n#  W -> Ratio between the stress at the third post-peak point and the\n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I)\n#  X -> Mode I fracture energy (Gf)\n#  Y -> Type of mode I fracture energy update law\n#       - Available keywords: _NONE, _LINEAR, _QUADRATIC or _CUBIC\n#  Z -> Ultimate crack shear sliding (keyword or constant value)\n#       - Available keywords: _MAXIMUM_CRACKWIDTH\n#  AA -> Parameter to define the mode I fracture energy available to the new crack\n#  AB -> Type of shear-softening diagram\n#       - Available keywords: _NONE or _LINEAR\n#  AC -> Type of shear retention factor law (keyword or constant value)\n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC\n#  AD -> Type of crack shear modelling appoach (used in shear retention model)\n#       - Available keywords: _INCREMENTAL or _TOTAL\n#  AE -> Crack shear strength\n#  AF -> Shear fracture energy (Gfs=GfII) (in nonlinear Mindlin shell: In-plane shear fracture energy)\n#  AG -> Type of unloading/reloading diagram\n#       - Available keywords: _SECANT or _ELASTIC\n#  AH -> Crack band width for the fracture mode I (keyword or constant value)\n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP\n# AI -> Maximum n. of cracks\n# AJ -> Threshold angle (degress)\n#\n# Out-of-plane shear data:\n#    -> Used in nonlinear Mindlin shell\n#\n# AK -> Type of out-of-plane shear-softening diagram\n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN\n# AL -> Minimum out-of-plane shear stress for softening behavior\n# AM -> Out-of-plane shear fracture energy\n# AN -> Ratio between the out-of-plane shear strain at the first\n#       post-peak point and the ultimate out-of-plane shear strain\n#       trilinear out-of-plane shear-softening diagram\n# AO -> Ratio between the out-of-plane shear stress at the first\n#       post-peak point and the out-of-plane shear strength of the\n#       trilinear out-of-plane shear-softening diagram\n# AP -> Ratio between the out-of-plane shear strain at the second\n#       post-peak point and the ultimate out-of-plane shear strain\n#       of the trilinear out-of-plane shear-softening diagram\n# AQ -> Ratio between the out-of-plane shear stress at the second\n#       post-peak point and the out-of-plane shear strength of the\n#       trilinear out-of-plane shear-softening diagram  \n";
                    _segment = fill_segment(comment1, comment2);
                    C_variables.NLMM142_tag = _segment;
                    C_variables.NLMM142_list = Class_femix_reader.ExtractLine(C_variables.NLMM142_tag, false);
                    C_variables.NLMM142_n = C_variables.NLMM142_list.Count;
                    break;

                case "NLMM145":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM211":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM102":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM122":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM401":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM451":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM301":
                    comment1 = "## Properties of the NLMM301 material model\n## Keyword: _NLMM301\n# N. of NLMM301 materials \n";
                    comment2 = "\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Slip at the end of the linear bond stress-slip relationship\n#  D -> Slip at peak bond stress\n#  E -> Peak bond stress\n#  F -> Parameter used in the definition of the pre-peak bond stress-slip relationship\n#  G -> Parameter used in the definition of the post-peak bond stress-slip relationship\n#  H -> Normal stiffness\n#      A  B                                         C                 D                 E                 F                 G                 H\n";
                    _segment = fill_segment(comment1, comment2);
                    C_variables.NLMM301_tag = _segment;
                    C_variables.NLMM301_list = Class_femix_reader.ExtractLine(C_variables.NLMM301_tag, false);
                    C_variables.NLMM301_n = C_variables.NLMM301_list.Count;
                    break;

                case "NLMM302":
                    comment1 = "## Keyword: _NLMM302\n## Properties of the NLMM302 material model\n# N. of NLMM302 materials  \n";
                    comment2 = "\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Constitutive value defining fracture mode II\n#  D -> Initial constitutive value defining fracture mode I\n#  E -> Tensile strength\n#  F -> First post-peak point of the crack opening\n#  G -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the tensile-softening diagram\n#  H -> Second post-peak point of the crack opening\n#  I -> Ratio between the stress at the second post-peak point\n#       and the tensile strength of the tensile-softening diagram\n#  J -> Ultimate post-peak point of the crack opnening\n#      A  B                                         C                 D                 E                 F                 G                 H                 I                 J  \n";
                    _segment = fill_segment(comment1, comment2);
                    C_variables.NLMM302_tag = _segment;
                    C_variables.NLMM302_list = Class_femix_reader.ExtractLine(C_variables.NLMM302_tag, false);
                    C_variables.NLMM302_n = C_variables.NLMM302_list.Count;
                    break;

                case "NLMM303":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM305":
                    comment1 = "## Properties of the NLMM305 material model\n## Keyword: _NLMM305\n# N. of NLMM305 materials  \n";
                    comment2 = "\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Slip at the end of the linear bond stress-slip relationship\n#  D -> Slip at peak bond stress\n#  E -> Material cohesion\n#  F -> Friction angle between materials in contact\n#  G -> Parameter used in the definition of the pre-peak bond stress-slip relationship\n#  H -> Parameter used in the definition of the post-peak bond stress-slip relationship\n#  I -> Normal stiffness\n#      A  B                 C                 D                 E                 F                 G                 H                 I  \n";
                    _segment = fill_segment(comment1, comment2);
                    C_variables.NLMM305_tag = _segment;
                    C_variables.NLMM305_list = Class_femix_reader.ExtractLine(C_variables.NLMM305_tag, false);
                    C_variables.NLMM305_n = C_variables.NLMM305_list.Count;
                    break;

                case "NLMM306":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "NLMM320":
                    //comment1 = "##  \n";
                    //comment2 = "\n##  \n";
                    //_segment = fill_segment(comment1, comment2);
                    //C_variables.x_tag = _segment;
                    //C_variables.x_list = Class_femix_reader.ExtractLine(C_variables.x_tag, false);
                    //C_variables.x_n = C_variables.x_list.Count;
                    MessageBox.Show("Error: This keyword could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "CROSS_SECTION_AREAS":
                    comment1 = "## Keyword: _CS_AREA \n## Cross section areas \n# N. of cross section areas \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the cross section \n#  C -> Area \n#		A	B		C\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.CROSS_SECTION_AREAS_tag = _segment;
                    C_variables.CROSS_SECTION_AREAS_list = Class_femix_reader.ExtractLine(C_variables.CROSS_SECTION_AREAS_tag, false);
                    C_variables.CROSS_SECTION_AREAS_n = C_variables.CROSS_SECTION_AREAS_list.Count;

                    break;

                case "FRAME_2D_CROSS_SECTIONS":
                    comment1 = "## Keyword: _CS_2D \n## Cross section properties of the 2D frame elements \n# N. of 2D frame cross sections \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the cross section \n#  C -> Area \n#  D -> Shear factor \n#  E -> Moment of inertia \n#  F -> Cross section height (used with differential temperature only) \n#		A	B			C			D			E			F\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.FRAME_2D_CROSS_SECTIONS_tag = _segment;
                    C_variables.FRAME_2D_CROSS_SECTIONS_list = Class_femix_reader.ExtractLine(C_variables.FRAME_2D_CROSS_SECTIONS_tag, false);
                    C_variables.FRAME_2D_CROSS_SECTIONS_n = C_variables.FRAME_2D_CROSS_SECTIONS_list.Count;

                    break;

                case "FRAME_3D_CROSS_SECTIONS":
                    comment1 = "## Keyword: _CS_3D \n## Cross section properties of the 3D frame elements \n# N. of 3D frame cross sections \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the cross section \n#  C -> Area \n#  D -> Shear factor (l2) \n#  E -> Shear factor (l3) \n#  F -> Torsional constant \n#  G -> Moment of inertia (l2) \n#  H -> Moment of inertia (l3) \n#  I -> Local coordinate of the shear center (l2) \n#  J -> Local coordinate of the shear center (l3) \n#  K -> Cross section height (l2) (used with differential temperature only) \n#  L -> Cross section height (l3) (used with differential temperature only) \n#  M -> Angle (degrees) \n#	A	B		C		D		E		F		G		H		I		J		K		L		M\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.FRAME_3D_CROSS_SECTIONS_tag = _segment;
                    C_variables.FRAME_3D_CROSS_SECTIONS_list = Class_femix_reader.ExtractLine(C_variables.FRAME_3D_CROSS_SECTIONS_tag, false);
                    C_variables.FRAME_3D_CROSS_SECTIONS_n = C_variables.FRAME_3D_CROSS_SECTIONS_list.Count;

                    break;

                case "LAMINATE_THICKNESSES":
                    comment1 = "## Keyword: _LAM_THICK \n## Thickness of the laminate elements \n# N. of nodal thicknesses \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the thickness \n#  C -> Thickness \n#		A		B		C\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.LAMINATE_THICKNESSES_tag = _segment;
                    C_variables.LAMINATE_THICKNESSES_list = Class_femix_reader.ExtractLine(C_variables.LAMINATE_THICKNESSES_tag, false);
                    C_variables.LAMINATE_THICKNESSES_n = C_variables.LAMINATE_THICKNESSES_list.Count;

                    break;

                case "GEOMETRY_PATTERNS":
                    comment1 = "## Keyword: _GEOM_PATT \n## Geometry patterns of several types of properties \n# N. of geometry patterns \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Pattern name \n#  C -> Type of geometry description \n#       - Available keywords: _CS_AREA, _CS_2D, _CS_3D or _LAM_THICK \n#  D -> N. of nodes of the pattern \n#  E -> Names of geometry descriptions \n#		A	B		C		D	E\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.GEOMETRY_PATTERNS_tag = _segment;
                    C_variables.GEOMETRY_PATTERNS_list = Class_femix_reader.ExtractLine(C_variables.GEOMETRY_PATTERNS_tag, false);
                    C_variables.GEOMETRY_PATTERNS_n = C_variables.GEOMETRY_PATTERNS_list.Count;

                    break;

                case "FIBROUS_PATTERNS":
                    comment1 = "## Keyword: _FIB_PATT \n## Lists of files describing fibrous cross sections \n# N. of lists of files describing fibrous cross sections \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Material/geometry name \n#  C -> N. of nodes of the fibrous element \n#  D -> File names defining the properties of the fibrous cross section \n# \n#  Note: when C is equal to 1 the cross section is constant \n# \n#		A	B		C	D\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.FIBROUS_PATTERNS_tag = _segment;
                    C_variables.FIBROUS_PATTERNS_list = Class_femix_reader.ExtractLine(C_variables.FIBROUS_PATTERNS_tag, false);
                    C_variables.FIBROUS_PATTERNS_n = C_variables.FIBROUS_PATTERNS_list.Count;

                    break;

                case "LAYER_PROPERTIES":
                    comment1 = "## Layer properties \n# N. of layer properties \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Layer name \n#  C -> Material type \n#  D -> Material name \n#  E -> Geometry type \n#  F -> Geometry name \n#	A	B		C		D		E		F\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.LAYER_PROPERTIES_tag = _segment;
                    C_variables.LAYER_PROPERTIES_list = Class_femix_reader.ExtractLine(C_variables.LAYER_PROPERTIES_tag, false);
                    C_variables.LAYER_PROPERTIES_n = C_variables.LAYER_PROPERTIES_list.Count;

                    break;

                case "LAYER_PATTERNS":
                    comment1 = "## Keyword: _LAY_PATT \n## Layer patterns \n# N. of layer patterns \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Pattern name \n#  C -> N. of layers \n#  E -> Layer (or layer range) \n#  D -> Name of the layer properties in the [LAYER_PROPERTIES] block \n# \n#	A	B			C		D	E	\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.LAYER_PATTERNS_tag = _segment;
                    C_variables.LAYER_PATTERNS_list = Class_femix_reader.ExtractLine(C_variables.LAYER_PATTERNS_tag, false);
                    C_variables.LAYER_PATTERNS_n = C_variables.LAYER_PATTERNS_list.Count;

                    break;

                case "NUMERICAL_INTEGRATION":
                    comment1 = "## Keywords: _GLEG, _GLOB or _NCOTES \n## Numerical integration definition \n# N. of layouts of integration points \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the layout \n#  C -> N. of keywords defining the layout \n#  D -> Keywords defining the type of term and the n. of points in each direction \n# \n#  Valid syntax for D is the following: \n#     _i_Sj_k \n# \n#     i: type of terms \n#       G -> General use (stiffness matrix, springs and load vector) \n#       M -> Membrane terms (stiffness matrix) \n#       S -> Shear terms (stiffness matrix) \n#       T -> Torsional terms (stiffness matrix) \n#       B -> Bending terms (stiffness matrix) \n# \n#     j: direction \n#       1 -> direction s1 \n#       2 -> direction s2 \n#       3 -> direction s3 \n# \n#     k: n. of points in the specified direction \n# \n#     Note: k must be in the range [1,10] \n# \n#     Examples: _G_S1_5, _M_S2_1, _S_S3_2, _T_S1_7, _B_S2_10 \n# \n#		A	B				C  D\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.NUMERICAL_INTEGRATION_tag = _segment;
                    C_variables.NUMERICAL_INTEGRATION_list = Class_femix_reader.ExtractLine(C_variables.NUMERICAL_INTEGRATION_tag, false);
                    C_variables.NUMERICAL_INTEGRATION_n = C_variables.NUMERICAL_INTEGRATION_list.Count;

                    break;

                case "ELEMENT_NODES":
                    comment1 = "## Nodes defining the elements \n# N. of elements \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> N. of nodes of the element \n#  C -> Nodes of the element \n#	A	B	C\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.ELEMENT_NODES_tag = _segment;
                    C_variables.ELEMENT_NODES_list = Class_femix_reader.ExtractLine(C_variables.ELEMENT_NODES_tag, false);
                    C_variables.ELEMENT_NODES_n = C_variables.ELEMENT_NODES_list.Count;

                    break;

                case "ELEMENT_PROPERTIES":
                    comment1 = "## Specification of the element properties \n# N. of specifications \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Element (or element range) \n#  E -> Element type \n#  F -> Material type \n#  G -> Material name \n#  H -> Geometry type \n#  I -> Geometry name \n#  J -> Integration type (stiffness matrix) \n#  K -> Integration name (stiffness matrix) \n#	A	B		C		D	E			F			G			H			I			J			K\n";

                    _segment = fill_segment(comment1, comment2);

                    int number_of_pro_old = C_variables.ELEMENT_PROPERTIES_n;

                    C_variables.ELEMENT_PROPERTIES_tag = _segment;
                    C_variables.ELEMENT_PROPERTIES_list = Class_femix_reader.ExtractLine(C_variables.ELEMENT_PROPERTIES_tag, false);
                    C_variables.ELEMENT_PROPERTIES_n = C_variables.ELEMENT_PROPERTIES_list.Count;

                    //check if element doesn't exist

                    //end of check

                    break;

                case "LINEAR_SPRING_MATERIALS":
                    comment1 = "## Keyword: _LIN_SPRING \n## Properties of the linear spring materials \n# N. of linear spring materials \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Material name \n#  C -> Stiffness \n#		A	B		C\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.LINEAR_SPRING_MATERIALS_tag = _segment;
                    C_variables.LINEAR_SPRING_MATERIALS_list = Class_femix_reader.ExtractLine(C_variables.LINEAR_SPRING_MATERIALS_tag, false);
                    C_variables.LINEAR_SPRING_MATERIALS_n = C_variables.LINEAR_SPRING_MATERIALS_list.Count;

                    break;

                case "POINT_SPRINGS":
                    comment1 = "## Points with springs \n# N. of point springs \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number (or point number range) \n#  E -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  F -> Spring location: \n#       - Direction vector from the structure (S) to the ground (G): _SG \n#       - Direction vector from the ground (G) to the structure (S): _GS \n#         Example: \n#           Direction vector: -----> _XG1 \n#                                            E     F \n#           Structure - Spring - Ground  =>  _XG1  _SG \n#           Ground - Spring - Structure  =>  _XG1  _GS \n#  G -> Dof type (_TRANS - Translational; _ROT - Rotational) \n#  H -> Spring material type \n#  I -> Spring material name(s) \n#		A		B			C			D	E		F		G		H		I\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.POINT_SPRINGS_tag = _segment;
                    C_variables.POINT_SPRINGS_list = Class_femix_reader.ExtractLine(C_variables.POINT_SPRINGS_tag, false);
                    C_variables.POINT_SPRINGS_n = C_variables.POINT_SPRINGS_list.Count;

                    break;

                case "LINE_SPRINGS":
                    comment1 = "## Lines with springs \n# N. of line springs \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Element number (or element number range) \n#  E -> Face number \n#  F -> Edge number \n#  G -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Timoshenko beam: _L1, _L2 or _L3 \n#       - Solid/Shell: _T, _NN or _NT \n#       - Plane stress/Plane strain/Axisymmetry: _T or _NT \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  H -> Spring location: \n#       - Direction vector from the structure (S) to the ground (G): _SG \n#       - Direction vector from the ground (G) to the structure (S): _GS \n#         Example: \n#           Direction vector: -----> _XG1 \n#                                            G     H \n#           Structure - Spring - Ground  =>  _XG1  _SG \n#           Ground - Spring - Structure  =>  _XG1  _GS \n#  I -> Dof type (_TRANS - Translational; _ROT - Rotational) \n#  J -> Integration type (spring stiffness matrix) \n#  K -> Integration name (spring stiffness matrix) \n#  L -> Spring material type \n#  M -> N. of nodes of the edge of the element \n#  N -> Spring material name(s) \n# \n#  Note: when M is equal to 1 the spring is constant \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the bar \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _T  - tangential to the edge \n#     _NN - normal to the edge and normal to the element face \n#     _NT - normal to the edge and tangential to the element face \n# \n#		A	B			C			D		E	F	G		H		I			J			K			L			M	N\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.LINE_SPRINGS_tag = _segment;
                    C_variables.LINE_SPRINGS_list = Class_femix_reader.ExtractLine(C_variables.LINE_SPRINGS_tag, false);
                    C_variables.LINE_SPRINGS_n = C_variables.LINE_SPRINGS_list.Count;

                    break;

                case "SURFACE_SPRINGS":
                    comment1 = "## Surface with springs \n# N. of surface springs \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Element number (or element number range) \n#  E -> Face number \n#  F -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Solid/Shell: _L1, _L2 or _N \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  G -> Spring location: \n#       - Direction vector from the structure (S) to the ground (G): _SG \n#       - Direction vector from the ground (G) to the structure (S): _GS \n#         Example: \n#           Direction vector: -----> _XG1 \n#                                            F     G \n#           Structure - Spring - Ground  =>  _XG1  _SG \n#           Ground - Spring - Structure  =>  _XG1  _GS \n#  H -> Integration type (spring stiffness matrix) \n#  I -> Integration name (spring stiffness matrix) \n#  J -> Spring material type \n#  K -> N. of nodes of the face of the element \n#  L -> Spring material name(s) \n# \n#  Notes: \n#         when K is equal to 1 the spring is constant \n#         Only translational stiffness is available \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the face and following the l1 local axis \n#     _L2 - tangential to the face and following the l2 local axis \n#     _N  - normal to the face \n# \n#		A	B			C			D	E	F		G	H		I		J			K	L\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.SURFACE_SPRINGS_tag = _segment;
                    C_variables.SURFACE_SPRINGS_list = Class_femix_reader.ExtractLine(C_variables.SURFACE_SPRINGS_tag, false);
                    C_variables.SURFACE_SPRINGS_n = C_variables.SURFACE_SPRINGS_list.Count;

                    break;

                case "STIFFENERS":
                    comment1 = "## Stiffener elements \n# N. of stiffeners \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Stiffener element (or stiffener element range) \n#  C -> Mother element (or mother element range) \n# \n#		A		B		C\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.STIFFENERS_tag = _segment;
                    C_variables.STIFFENERS_list = Class_femix_reader.ExtractLine(C_variables.STIFFENERS_tag, false);
                    C_variables.STIFFENERS_n = C_variables.STIFFENERS_list.Count;

                    break;
                    
                case "LOAD_CASE_PARAMETERS":

                    comment1 = "# NTEGRATION_TYPE Keywords: _GLEG, _GLOB, _NCOTES or _DEFAULT \n";
                    comment2 = "\n# INTEGRATION_NAME Defined in the <NUMERICAL_INTEGRATION> block or _DEFAULT\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.LOAD_CASE_PARAMETERS_tag = _segment;
                    C_variables.LOAD_CASE_PARAMETERS_list = Class_femix_reader.ExtractLine(C_variables.LOAD_CASE_PARAMETERS_tag, false);
                    C_variables.LOAD_CASE_PARAMETERS_n = C_variables.LOAD_CASE_PARAMETERS_list.Count;

                    C_variables.Load_tag_generator();

                    break;

                case "GRAVITY_LOAD":
                    comment1 = "\n";
                    comment2 = "    ## Gravity acceleration (global coordinate system) \n## Content of each column: \n#  A -> Acceleration - XG1 \n#  B -> Acceleration - XG2 \n#  C -> Acceleration - XG3 \n#       A       B       C";

                    //_segment = fill_segment(comment1, comment2);
                    _segment = comment2+"\n";

                    if (DG1.RowCount > 1)
                    {
                        //_segment += comment1 + $"COUNT = {DG1.RowCount - 1} ;\n" + comment2;
                        for (int i = 0; i < DG1.RowCount - 1; i++)
                        {
                            _segment += "              " + DG1.Rows[i].HeaderCell.Value;
                            for (int j = 0; j < DG1.ColumnCount; j++)
                            {
                                _segment += $"        " + DG1.Rows[i].Cells[j].Value;
                            }
                            _segment += " ;\n";
                        }
                    }

                    C_variables.GRAVITY_LOAD_tag = _segment;
                    C_variables.GRAVITY_LOAD_list = Class_femix_reader.ExtractLine(C_variables.GRAVITY_LOAD_tag, false);
                    C_variables.GRAVITY_LOAD_n = C_variables.GRAVITY_LOAD_list.Count;
                    C_variables.Load_tag_generator();

                    break;

                case "POINT_LOADS":

                    comment1 = "## Point loads (global coordinate system) \n# N. of point loads \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded point (or loaded point range) \n#  C -> Force - XG1 \n#  D -> Force - XG2 \n#  E -> Force - XG3 \n#  F -> Moment - XG1 \n#  G -> Moment - XG2 \n#  H -> Moment - XG3 \n#		A		B		C		D		E		F		G		H\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.POINT_LOADS_tag = _segment;
                    C_variables.POINT_LOADS_list = Class_femix_reader.ExtractLine(C_variables.POINT_LOADS_tag, false);
                    C_variables.POINT_LOADS_n = C_variables.POINT_LOADS_list.Count;
                    C_variables.Load_tag_generator();

                    break;

                case "INTERNAL_POINT_LOADS":
                    comment1 = "## Load applied to a point inside an element (global coordinate system) \n# N. of internal point loads \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element \n#  C -> Auxiliary point with the load (or auxiliary point range) \n#  D -> Force - XG1 \n#  E -> Force - XG2 \n#  F -> Force - XG3 \n#  G -> Moment - XG1 \n#  H -> Moment - XG2 \n#  I -> Moment - XG3 \n#		A		B		C		D		E		F		G		H		I\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.INTERNAL_POINT_LOADS_tag = _segment;
                    C_variables.INTERNAL_POINT_LOADS_list = Class_femix_reader.ExtractLine(C_variables.INTERNAL_POINT_LOADS_tag, false);
                    C_variables.INTERNAL_POINT_LOADS_n = C_variables.INTERNAL_POINT_LOADS_list.Count;
                    C_variables.Load_tag_generator();

                    break;

                case "EDGE_LOADS":
                    comment1 = "## Load on the edge of an element \n# N. of edge loads \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Loaded face \n#  D -> Loaded edge \n#  E -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Frame: _L1, _L2 or _L3 \n#       - Solid/Shell: _T, _NN or _NT \n#       - Plane stress/Plane strain/Axisymmetry: _T or _NT \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  F -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  G -> N. of nodes of the edge of the element \n#  H -> Load values \n# \n#  Note: when G is equal to 1 the load is constant \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the bar \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _T  - tangential to the edge \n#     _NN - normal to the edge and normal to the element face \n#     _NT - normal to the edge and tangential to the element face \n# \n#		A		B		C	D	E		F		G		H\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.EDGE_LOADS_tag = _segment;
                    C_variables.EDGE_LOADS_list = Class_femix_reader.ExtractLine(C_variables.EDGE_LOADS_tag, false);
                    C_variables.EDGE_LOADS_n = C_variables.EDGE_LOADS_list.Count;
                    C_variables.Load_tag_generator();

                    break;

                case "INTERNAL_EDGE_LOADS":
                    comment1 = "## Line load inside an element \n# N. of internal edge loads \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Loaded element \n#  C -> Auxiliary line element with the load \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Frame: _L1, _L2 or _L3 \n#       - Solid surface/Shell: _T, _NN or _NT \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the auxiliary line element \n#  G -> Load values \n# \n#  Notes: \n#  - when F is equal to 1 the load is constant; \n#  - the n. of nodes of the auxiliary line element must match the n. of nodes of the edge of the loaded element. \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the bar \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _T  - tangential to the edge \n#     _NN - normal to the edge and normal to the element face \n#     _NT - normal to the edge and tangential to the element face \n# \n#		A		B		C	D		E		F		G\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.INTERNAL_EDGE_LOADS_tag = _segment;
                    C_variables.INTERNAL_EDGE_LOADS_list = Class_femix_reader.ExtractLine(C_variables.INTERNAL_EDGE_LOADS_tag, false);
                    C_variables.INTERNAL_EDGE_LOADS_n = C_variables.INTERNAL_EDGE_LOADS_list.Count;
                    C_variables.Load_tag_generator();

                    break;

                case "FACE_LOADS":
                    comment1 = "## Load on the face of an element \n# N. of face loads \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Loaded face \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Solid/Shell: _L1, _L2 or _N \n#       - Plane stress/Plane strain/Axisymmetry: _L1 or _L2 \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the face of the element \n#  G -> Load values \n# \n#  Note: when F is equal to 1 the load is constant \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the face and following the l1 local axis \n#     _L2 - tangential to the face and following the l2 local axis \n#     _N  - normal to the face \n# \n#		A		B		C	D		E		F		G\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.FACE_LOADS_tag = _segment;
                    C_variables.FACE_LOADS_list = Class_femix_reader.ExtractLine(C_variables.FACE_LOADS_tag, false);
                    C_variables.FACE_LOADS_n = C_variables.FACE_LOADS_list.Count;
                    C_variables.Load_tag_generator();

                    break;

                case "INTERNAL_FACE_LOADS":
                    comment1 = "## Surface load inside an element \n# N. of internal face loads \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Loaded element \n#  C -> Auxiliary surface element with the load \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Solid surface/Shell: _L1, _L2 or _N \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the auxiliary surface element \n#  G -> Load values \n# \n#  Notes: \n#  - when F is equal to 1 the load is constant; \n#  - the n. of nodes of the auxiliary surface element must match the n. of nodes of the face of the loaded element. \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the face and following the l1 local axis \n#     _L2 - tangential to the face and following the l2 local axis \n#     _N  - normal to the face \n# \n#		A		B		C	D			E			F			G\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.INTERNAL_FACE_LOADS_tag = _segment;
                    C_variables.INTERNAL_FACE_LOADS_list = Class_femix_reader.ExtractLine(C_variables.INTERNAL_FACE_LOADS_tag, false);
                    C_variables.INTERNAL_FACE_LOADS_n = C_variables.INTERNAL_FACE_LOADS_list.Count;
                    C_variables.Load_tag_generator();

                    break;

                case "TEMPERATURE_VARIATION":
                    comment1 = "## Elements with temperature variation \n# N. of elements with temperature variation \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> N. of nodes of the element \n#  D -> Temperature variation values \n# \n#  Note: when C is equal to 1 the temperature variation is the same for all the element nodes \n# \n#		A		B		C		D\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.TEMPERATURE_VARIATION_tag = _segment;
                    C_variables.TEMPERATURE_VARIATION_list = Class_femix_reader.ExtractLine(C_variables.TEMPERATURE_VARIATION_tag, false);
                    C_variables.TEMPERATURE_VARIATION_n = C_variables.TEMPERATURE_VARIATION_list.Count;
                    C_variables.Load_tag_generator();

                    break;

                case "DIFFERENTIAL_TEMPERATURE_VARIATION":
                    comment1 = "## Elements with differential temperature variation \n# N. of elements with differential temperature variation \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Direction vector: \n#       - Frame: _L2 or _L3 \n#       - Shell: _N \n#  D -> N. of nodes of the element \n#  E -> Differential temperature variation values \n# \n#  Note: when D is equal to 1 the diff. temp. var. is the same for all the element nodes \n# \n#  Direction vector keywords: \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _N  - normal to the shell mean surface \n# \n#		A		B	C		D		E\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_tag = _segment;
                    C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_list = Class_femix_reader.ExtractLine(C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_tag, false);
                    C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_n = C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_list.Count;
                    C_variables.Load_tag_generator();

                    break;

                case "PRESCRIBED_DISPLACEMENTS":
                    comment1 = "## Points with prescribed displacement \n# N. of prescribed displacements \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Point number (or point number range) \n#  C -> Prescribed degree of freedom \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#  D -> Value of the prescribed displacement \n#		A		B	C		D\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.PRESCRIBED_DISPLACEMENTS_tag = _segment;
                    C_variables.PRESCRIBED_DISPLACEMENTS_list = Class_femix_reader.ExtractLine(C_variables.PRESCRIBED_DISPLACEMENTS_tag, false);
                    C_variables.PRESCRIBED_DISPLACEMENTS_n = C_variables.PRESCRIBED_DISPLACEMENTS_list.Count;
                    C_variables.Load_tag_generator();

                    break;

                case "INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS":
                    comment1 = "## Points with internal prescribed relative displacement \n# N. of internal prescribed relative displacements \n";
                    comment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Point number - M (Master) (or point number range) \n#  C -> Point number - S (Slave) (or point number range) \n#  D -> Prescribed degree of freedom \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#  E -> Value of the prescribed displacement \n#		A		B		C	D		E\n";

                    _segment = fill_segment(comment1, comment2);

                    C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_tag = _segment;
                    C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_list = Class_femix_reader.ExtractLine(C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_tag, false);
                    C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_n = C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_list.Count;
                    C_variables.Load_tag_generator();

                    break;





                case "LOAD_CASE_COMBINATIONS":
                    comment1 = "\n##This section is not a part of the FEMIX data file and will be used for the generation of the load case combinations.\n";
                    comment2 = "\n#   A -> Counter\n#   B -> Combination Title\n#	C -> Group Name\n#   D -> Number of increments in the combinations\n#   E -> Load case number\n#   F -> Load factor\n##  E1-F1   E2-F2   E3-F3 ...\n## E4-FIX1 --> Keep Load factor Constant during increments.\n#             A        B        C        D		E-F  \n";

                    _segment = "";
                    if (DG1.RowCount > 1)
                    {
                        _segment += comment1 + $"COUNT = {DG1.RowCount - 1} ;\n" + comment2;

                        for (int i = 0; i < DG1.RowCount - 1; i++)
                        {
                            _segment += "              " + DG1.Rows[i].HeaderCell.Value;
                            _segment += "        " + DG1.Rows[i].Cells[0].Value;
                            _segment += "        " + DG1.Rows[i].Cells[1].Value;
                            _segment += "        " + DG1.Rows[i].Cells[2].Value;
                            for (int j = 3; j < DG1.ColumnCount; j++)
                            {
                                if (DG1.Rows[i].Cells[j].Value == null)
                                    DG1.Rows[i].Cells[j].Value = "N";
                                if (int.TryParse(DG1.Rows[i].Cells[j].Value.ToString(), out _)|| DG1.Rows[i].Cells[j].Value.ToString().ToUpper().Contains("FIX"))//check if is numeric or contain FIX
                                { 
                                _segment += "        " + C_variables.Val(DG1.Columns[j].HeaderText);
                                _segment += "-" + DG1.Rows[i].Cells[j].Value;
                                }
                            }
                            _segment += " ;\n";
                        }
                    }



                    C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag = _segment;
                    C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list = Class_femix_reader.ExtractLine(C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag, false);
                    C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_n = C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list.Count;
                    
                    if (C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_n > 0)
                        if (C_variables.LOAD_CASE_COMBINATIONS_n == 0)
                            C_variables.LOAD_CASE_COMBINATIONS_n = 1;

                    C_variables.LOAD_CASE_COMBINATIONS_tag = create_load_combinations(C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list);




                    int n_combo = 0;
                    foreach (var row in C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list)
                    {
                        n_combo += Int32.Parse(row[4]);
                    }
                    //Edit MAIN_PARAMETERS
                    C_variables.MAIN_PARAMETERS_tag = "";
                    foreach (var row in C_variables.MAIN_PARAMETERS_list)
                    {
                        if (row[1] == "NUMBER_OF_COMBINATIONS")
                            row[2] = n_combo.ToString();
                        C_variables.MAIN_PARAMETERS_tag += $"  {row[1]} = {row[2]} ;\n";
                    }



                    break;

                default:
                    break;
            }

            C_variables.femix_string_generator();

            is_there_change = true;

            this.Close();

            //frm_list_items formlist = new frm_list_items();
            //formlist.selected_item = "kkkkk";
            //formlist.Show();
            //C_variables.Femix_string = "kkk";
        }

        private string fill_segment(string comment1 , string comment2)
        {
            string _segment = "\n";
            //string srting_range;
            //int range_start, range_end;
            if (DG1.RowCount > 1)
            {

                //int counter = 0;
                //for (int i = 0; i < DG1.RowCount - 1; i++)
                //{
                //    if (DG1.Rows[i].Cells[0].Value == null)
                //        DG1.Rows[i].Cells[0].Value = "";
                //    srting_range = DG1.Rows[i].Cells[0].Value.ToString();
                //    string[] elementrange = srting_range.Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                //    counter++;
                //    if (elementrange.Count() > 1 && srting_range.Contains("[") && srting_range.Contains("-") && srting_range.Contains("]"))
                //    {
                //        range_start = Int32.Parse(elementrange[0]);
                //        range_end = Int32.Parse(elementrange[1]);
                //        counter = counter + (range_end - range_start);
                //    }
                //}
                //_segment += comment1 + $"COUNT = {counter} ;\n" + comment2;
                //counter = 0;

                _segment += comment1 + $"COUNT = {DG1.RowCount - 1} ;\n" + comment2;

                for (int i = 0; i < DG1.RowCount - 1; i++)
                {

                    //srting_range = DG1.Rows[i].Cells[0].Value.ToString();
                    //string[] elementrange = srting_range.Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                    //counter++;
                    //if (elementrange.Count() > 1 && srting_range.Contains("[") && srting_range.Contains("-") && srting_range.Contains("]"))
                    //{
                    //    range_start = Int32.Parse(elementrange[0]);
                    //    range_end = Int32.Parse(elementrange[1]);
                    //    _segment += "        " + $"[{counter}-{counter + (range_end - range_start)}]";
                    //    counter = counter + (range_end - range_start);
                    //}
                    //else
                    //{
                    //    _segment += "        " + counter;
                    //}
                    _segment += "              " + DG1.Rows[i].HeaderCell.Value;

                    for (int j = 0; j < DG1.ColumnCount; j++)
                    {
                        if (DG1.Rows[i].Cells[j].Value == null)
                            DG1.Rows[i].Cells[j].Value = "";
                        _segment += "        " + DG1.Rows[i].Cells[j].Value.ToString();
                    }
                    _segment += " ;\n";
                }
            }
            _segment += "\n";
            return _segment;
        }

        private void frm_list_items_Load(object sender, EventArgs e)
        {
            load_table_items();
        }
        private void load_table_items()
        {
            DG1.ContextMenuStrip = Edit_context_menu;

            splitContainer_info_ver.Panel2Collapsed = true;

            this.Text = selected_item;
            label_item.Text = selected_item;

            List<string> Dg_col_name;
            DG1.Columns.Clear();

            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            List<string> DgDs = new List<string>();

            switch (selected_item)
            {
                case "MAIN_PARAMETERS":
                    DG1.ContextMenuStrip = null;

                    splitContainer3.Panel2Collapsed = true;
                    txtinfo.Text = "## MAIN PARAMETERS ";

                    Dg_col_name = new List<string>() { "Parameters" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_MAIN_PARAMETERS_DG1_femix(C_variables.MAIN_PARAMETERS_n, C_variables.MAIN_PARAMETERS_list);

                    break;


                case "GROUP_NAMES":


                    txtinfo.Text = "## Declaration of the names of the groups\n## Content of each column:\n#  A -> Counter\n#  B -> Name";

                    Dg_col_name = new List<string>() { "Name" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    //DG1.DataSource = C_variables.GROUP_NAMES_list;
                    fill_DG1_femix(C_variables.GROUP_NAMES_n, C_variables.GROUP_NAMES_list);

                    break;

                case "POINT_COORDINATES":
                    txtinfo.Text = "## Point coordinates (global coordinate system) \n# N. of points \n\n## Content of each column: \n#  A -> Counter \n#  B -> Coordinate - XG1 \n#  C -> Coordinate - XG2 \n#  D -> Coordinate - XG3 \n";

                    Dg_col_name = new List<string>() { "XG1 (x)", "XG2 (y)", "XG3 (z)" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.POINT_COORDINATES_n, C_variables.POINT_COORDINATES_list);

                    break;

                case "AUXILIARY_VECTORS":
                    txtinfo.Text = "## Auxiliary vectors (global coordinate system) \n## All vectors are normalized immediately after input \n# N. of auxiliary vectors \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the auxiliary vector \n#  C -> Component of the auxiliary vector - 1 \n#  D -> Component of the auxiliary vector - 2 \n#  E -> Component of the auxiliary vector - 3 ";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.AUXILIARY_VECTORS_n, C_variables.AUXILIARY_VECTORS_list);

                    break;

                case "COORDINATE_SYSTEMS":
                    txtinfo.Text = "## Coordinate systems defined with 3 vectors \n# N. of coordinate systems \n\n## Content of each column: \n#  A -> Counter \n#  B -> Coordinate system name \n#  C -> Name of the auxiliary vector - 1 \n#  D -> Name of the auxiliary vector - 2 \n#  E -> Name of the auxiliary vector - 3";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.COORDINATE_SYSTEMS_n, C_variables.COORDINATE_SYSTEMS_list);

                    break;

                case "POINTS_WITH_SPECIAL_COORDINATE_SYSTEM":
                    txtinfo.Text = "## Points with special coordinate system \n# N. of points with special coordinate system \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number (or point number range) \n#  E -> Name of the coordinate system";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_n, C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_list);

                    break;

                case "SUPPORTS":
                    txtinfo.Text = "## Points with fixed degrees of freedom \n# N. of points with fixed degrees of freedom \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number (or point number range) \n#  E -> N. of fixed degrees of freedom in the current point(s) \n#  F -> Fixed degrees of freedom: \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3";


                    Create_combobox_column_femix(C_variables.GROUP_NAMES_n, C_variables.GROUP_NAMES_list, 2,false, "B", "Group");

                    Dg_col_name = new List<string>() { "Phase", "Point Number" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    DataGridViewCheckBoxColumn dgvChb;

                    Dg_col_name = new List<string>() { "_D1", "_D2", "_D3", "_R1", "_R2", "_R3" };
                    foreach (string item in Dg_col_name)
                    {
                        dgvChb = new DataGridViewCheckBoxColumn() { TrueValue = item };
                        dgvChb.HeaderText = item;
                        dgvChb.Name = item;
                        dgvChb.FlatStyle = FlatStyle.Standard;
                        DG1.Columns.Add(dgvChb);
                    }

                    fill_support_DG1_femix(C_variables.SUPPORTS_n, C_variables.SUPPORTS_list);

                    break;

                case "INTERNAL_POINT_LINKS":
                    txtinfo.Text = "## Points with linked degrees of freedom \n# N. of pairs of points with linked degrees of freedom \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number - M (Master) (or point number range) \n#  E -> Point number - S (Slave) (or point number range) \n#  F -> N. of linked degrees of freedom in the current pair of points \n#  G -> Linked degrees of freedom: \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.INTERNAL_POINT_LINKS_n, C_variables.INTERNAL_POINT_LINKS_list);

                    break;

                case "LINEAR_ISOTROPIC_MATERIALS":
                    txtinfo.Text = "## Keyword: _LIN_ISO \n## Properties of the linear isotropic materials \n# N. of linear isotropic materials \n\n## Content of each column: \n#  A -> Counter \n#  B -> Material name \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Young's modulus \n#  F -> Poisson's coefficient";

                    Dg_col_name = new List<string>() { "Material name", "Mass per unit volume", "Temperature coefficient", "Young's modulus", "Poisson's coefficient" };//B,C,D,E,F
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.LINEAR_ISOTROPIC_MATERIALS_n, C_variables.LINEAR_ISOTROPIC_MATERIALS_list);

                    break;

                case "LINEAR_ORTHOTROPIC_MATERIALS":
                    txtinfo.Text = "## Keyword: _LIN_ORTHO \n## Properties of the linear orthotropic materials \n# N. of linear orthotropic materials \n\n## Content of each column: \n#  A -> Counter \n#  B -> Material name \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient - l1 \n#  E -> Temperature coefficient - l2 \n#  F -> Temperature coefficient - l3 \n#  G -> Young's modulus - l1 \n#  H -> Young's modulus - l2 \n#  I -> Young's modulus - l3 \n#  J -> Shear modulus - l2/l3 \n#  K -> Shear modulus - l3/l1 \n#  L -> Shear modulus - l1/l2 \n#  M -> Poisson's coefficient - l2/l3 \n#  N -> Poisson's coefficient - l3/l1 \n#  O -> Poisson's coefficient - l1/l2 \n#  P -> Name of the coordinate system";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.LINEAR_ORTHOTROPIC_MATERIALS_n, C_variables.LINEAR_ORTHOTROPIC_MATERIALS_list);

                    break;

                case "LINEAR_INTERFACE_POINT_PROPERTIES":
                    txtinfo.Text = "## Keyword: _LIN_INT_POINT \n## Properties of linear interface elements that link a pair of points \n# N. of types of point interface elements \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the type of point interface element \n#  C -> Degree of freedom with linking spring \n#  D -> Stiffness of the spring";

                    Dg_col_name = new List<string>() { "B", "C", "D" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_n, C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_list);

                    break;

                case "LINEAR_INTERFACE_LINE_PROPERTIES":
                    txtinfo.Text = "## Keyword: _LIN_INT_LINE \n## Properties of linear interface elements that link a pair of edges (2D only) \n# N. of types of line interface elements \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the type of line interface element \n#  C -> Normal stiffness \n#  D -> Tangential stiffness";

                    Dg_col_name = new List<string>() { "B", "C", "D" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_n, C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_list);

                    break;

                case "LINEAR_INTERFACE_SURFACE_PROPERTIES":
                    txtinfo.Text = "## Keyword: _LIN_INT_SURF \n## Properties of linear interface elements that link a pair of faces (solid/shell only) \n# N. of types of surface interface elements \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the type of surface interface element \n#  C -> Normal stiffness \n#  D -> Tangential stiffness - s1 \n#  E -> Tangential stiffness - s2";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_n, C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_list);

                    break;

                case "NLMM101":
                    txtinfo.Text = "## Keyword: _NLMM101 \n## Properties defining the material constitutive law: \n##    CEB-FIP 1993 law for compression behavior \n##    Linear-parabola diagram for tensile up-to-peak behaviour \n##    Trilinear diagram for tensile post-peak behavior \n##    Kinematic hardening rule; associated flow rule; strain hardening; Owen yield criteria \n##    Multi-fixed smeared crack model \n \n# N. of materials modeled by the NLMM101 law \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the constitutive law \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n \n#  G -> Compression peak stress \n#  H -> Strain at compression peak stress \n#  I -> Parameter for defining the compressive strength and rigidity \n#       degradation according to Vecchio and Collins law (0=no degradation) \n#  J -> Unloading and reloading law in compression (_ELASTIC, _SECANT) \n \n#  K -> Tensile peak stress \n#  L -> Strain at tensile peak stress \n#  M -> Up limit tensile stress of the linear branch \n#  N -> Unloading and reloading law for the up-to-peak tensile stress (_ELASTIC, _SECANT) \n \n#  O -> Stress at the first post-peak point of the tensile-softening diagram (fracture mode I) \n#  P -> Crack strain at the first post-peak point of the tensile-softening diagram (fracture mode I) \n#  Q -> Stress at the second post-peak point of the tensile-softening diagram (fracture mode I) \n#  R -> Crack strain at the second post-peak point of the tensile-softening diagram (fracture mode I) \n#  S -> Mode I fracture energy (Gf) \n#  T -> Unloading and reloading law for the tensile post-peak strain softening diagram \n#       (_ELASTIC, _SECANT) (fracture mode I) \n \n#  U -> Parameter to define the shear retention factor for the fracture mode II \n#  V -> Crack band width for the fracture mode I (_SQRT_ELE; _SQRT_GP; _CONST_VALUE) \n#  X -> Criteria for opening a new crack [1: principal tensile stress greater than uniaxial tensile strength (_SI_GT_FCT); \n# 2: angle between the principal tensile stress and the existing cracks \n#    exceed the value of a threshold angle (_ANG_GT_THRANG); \n# 3: 1 AND 2 (_SIGTFCT_AND_ANGGTTHRANG); \n# 4: 1 OR 2 (_SIGTFCT_OR_ANGGTTHRANG); \n#  Y -> Threshold angle (zero for the first criteria of the new crack opening); \n#  W -> Parameter to define the mode I fracture energy available for the new crack (zero for the initial Gf)";

                    splitContainer_info_ver.Panel2Collapsed = false;


                    create_help_image_tab("NLMM104-2-TRI.png", "Fracture mode I-TRI");
                    create_help_image_tab("NLMM104-2-CORN.png", "Fracture mode I-CORN");

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.NLMM101_n, C_variables.NLMM101_list);

                    break;

                case "NLMM104":
                    txtinfo.Text = "## Keyword: _NLMM104 \n## Properties of the NLMM104 material model \n# N. of NLMM104 materials \n \n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Tensile strength \n#  I -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN \n#  J -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  K -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  L -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  M -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  N -> Ratio between the strain at the third post-peak point and the \n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I) \n#  O -> Ratio between the stress at the third post-peak point and the \n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I) \n#  P -> Mode I fracture energy (Gf) \n#  Q -> Type of mode I fracture energy update law \n#       - Available keywords: _NONE, _LINEAR, _QUADRATIC or _CUBIC \n#  R -> Ultimate crack shear sliding (keyword or constant value) \n#       - Available keywords: _MAXIMUM_CRACKWIDTH \n#  S -> Parameter to define the mode I fracture energy available to the new crack \n#  T -> Type of shear-softening diagram \n#       - Available keywords: _NONE or _LINEAR \n#  U -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n#  V -> Type of crack shear modelling appoach (used in shear retention model) \n#       - Available keywords: _INCREMENTAL or _TOTAL \n#  W -> Crack shear strength \n#  X -> Shear fracture energy (Gfs=GfII) (in nonlinear Mindlin shell: In-plane shear fracture energy) \n#  Y -> Type of unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n#  Z -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n# AA -> Maximum n. of cracks \n# AB -> Threshold angle (degress) \n# \n# Out-of-plane shear data: \n#    -> Used in nonlinear Mindlin shell \n# \n# AC -> Type of out-of-plane shear-softening diagram \n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN \n# AD -> Minimum out-of-plane shear stress for softening behavior \n# AE -> Out-of-plane shear fracture energy \n# AF -> Ratio between the out-of-plane shear strain at the first \n#       post-peak point and the ultimate out-of-plane shear strain \n#       trilinear out-of-plane shear-softening diagram \n# AG -> Ratio between the out-of-plane shear stress at the first \n#       post-peak point and the out-of-plane shear strength of the \n#       trilinear out-of-plane shear-softening diagram \n# AH -> Ratio between the out-of-plane shear strain at the second \n#       post-peak point and the ultimate out-of-plane shear strain \n#       of the trilinear out-of-plane shear-softening diagram \n# AI -> Ratio between the out-of-plane shear stress at the second \n#       post-peak point and the out-of-plane shear strength of the \n#       trilinear out-of-plane shear-softening diagram \n";

                    splitContainer_info_ver.Panel2Collapsed = false;

                    create_help_image_tab("NLMM104-2-TRI.png", "Fracture mode I-TRI");
                    create_help_image_tab("NLMM104-2-QUAD.png", "Fracture mode I-QUAD");
                    create_help_image_tab("NLMM104-2-CORN.png", "Fracture mode I-CORN");
                    create_help_image_tab("NLMM104-3.png", "Fracture mode II-Linear");
                    create_help_image_tab("NLMM104-5-LIN.png", "OP shear-softening-LIN");
                    create_help_image_tab("NLMM104-5-TRI.png", "OP shear-softening-TRI");
                    create_help_image_tab("NLMM104-5-CORN.png", "OP shear-softening-CORN");

                    //"B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "V", "W", "X", "AF", "AG", "AH", "AI" 


                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "V", "W", "X", "AF", "AG", "AH", "AI" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    /*
                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "V", "W", "X", "AF", "AG", "AH", "AI" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);


                    Dg_col_name = new List<string>() { "Name", "C(ρ)", "Temperature coefficient", "Poisson's ratio", "Young's modulus", "Compressive strength", "Tensile strength" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);


                    



                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_TRILINEAR");
                    DgDs.Add("_QUADRILINEAR");
                    DgDs.Add("_CORNELISSEN");

                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "I";
                    dgvCmb.Name = "I";
                    DG1.Columns.Add(dgvCmb);

                    Dg_col_name = new List<string>() { "J (ζ_1)", "K (α_1)", "L (ζ_2)", "M (α_2)", "N (ζ_3)", "O (α_3)", "P (Gf)" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();

                    DgDs.Add("_NONE");
                    DgDs.Add("_LINEAR");
                    DgDs.Add("_QUADRATIC");
                    DgDs.Add("_CUBIC");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "Q";
                    dgvCmb.Name = "Q";
                    DG1.Columns.Add(dgvCmb);

                    //dgvCmb = new DataGridViewComboBoxColumn();
                    //DgDs = new List<string>();
                    //DgDs.Add("_MAXIMUM_CRACKWIDTH");
                    //dgvCmb.DataSource = DgDs;
                    //dgvCmb.HeaderText = "R";
                    //dgvCmb.Name = "R";
                    //DG1.Columns.Add(dgvCmb);
                     DG1.Columns.Add("R", "R");

                    DG1.Columns.Add("S", "S");

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_NONE");
                    DgDs.Add("_LINEAR");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "T";
                    dgvCmb.Name = "T";
                    DG1.Columns.Add(dgvCmb);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_LINEAR");
                    DgDs.Add("_QUADRATIC");
                    DgDs.Add("_CUBIC");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "U";
                    dgvCmb.Name = "U";
                    DG1.Columns.Add(dgvCmb);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_INCREMENTAL");
                    DgDs.Add("_TOTAL");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "V";
                    dgvCmb.Name = "V";
                    DG1.Columns.Add(dgvCmb);

                    Dg_col_name = new List<string>() { "Crack shear strength", "X (Shear fracture energy)" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_SECANT");
                    DgDs.Add("_ELASTIC");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "Y";
                    dgvCmb.Name = "Y";
                    DG1.Columns.Add(dgvCmb);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_SQRT_ELEMENT");
                    DgDs.Add("_SQRT_IP");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "Z";
                    dgvCmb.Name = "Z";
                    DG1.Columns.Add(dgvCmb);

                    Dg_col_name = new List<string>() { "Maximum n. of cracks", "AB (Threshold angle (degress))" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_NONE");
                    DgDs.Add("_LINEAR");
                    DgDs.Add("_TRILINEAR");
                    DgDs.Add("_CORNELISSEN");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "AC";
                    dgvCmb.Name = "AC";
                    DG1.Columns.Add(dgvCmb);

                    Dg_col_name = new List<string>() { "AD", "AE", "AF", "AG", "AH", "AI" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);
                    */

                    fill_DG1_femix(C_variables.NLMM104_n, C_variables.NLMM104_list);

                    break;







                case "NLMM111":
                    txtinfo.Text = "## Keyword: _NLMM111 \n## Properties of the NLMM111 material model \n# N. of NLMM111 materials \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Tensile strength \n#  I -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN \n#  J -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  K -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  L -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  M -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  N -> Ratio between the strain at the third post-peak point and the \n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I) \n#  O -> Ratio between the stress at the third post-peak point and the \n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I) \n#  P -> Mode I fracture energy (Gf) \n#  Q -> Parameter to define the mode I fracture energy available to the new crack \n#  R -> Type of shear-softening diagram \n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN \n#  S -> Ratio between the (shear strain minus peak shear strain) at the first post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n#  T -> Ratio between the the shear stress at the first post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n#  U -> Ratio between the (shear strain minus peak shear strain) at the second post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n#  V -> Ratio between the the shear stress at the second post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n#  W -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n#  X -> Type of crack shear modelling appoach (used in shear retention model) \n#       - Available keywords: _INCREMENTAL or _TOTAL \n#  Y -> Crack shear strength \n#  Z -> Shear fracture energy (Gfs=GfII=GfIII) \n# AA -> Type of unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n# AB -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n# AC -> Maximum n. of cracks \n# AD -> Threshold angle (degress) ";

                    create_help_image_tab("NLMM104-2-TRI.png", "Fracture mode I-TRI");
                    create_help_image_tab("NLMM104-2-QUAD.png", "Fracture mode I-QUAD");
                    create_help_image_tab("NLMM104-2-CORN.png", "Fracture mode I-CORN");
                    create_help_image_tab("NLMM104-5-LIN.png", "Fracture mode II-LIN");
                    create_help_image_tab("NLMM104-5-TRI.png", "Fracture mode II-TRI");
                    create_help_image_tab("NLMM104-5-CORN.png", "Fracture mode II-CORN");



                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.NLMM111_n, C_variables.NLMM111_list);

                    break;

                case "NLMM141":
                    txtinfo.Text = "## Keyword: _NLMM141 \n## Properties of the NLMM141 material model \n# N. of NLMM141 materials \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Strain at compression peak stress \n#  I -> Parameter defining the initial yield surface \n#  J -> Compression/stiffness degradation parameter due to concrete cracking \n#  K -> Tensile strength \n#  L -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR or _CORNELISSEN \n#  M -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear tensile-softening diagram (fracture mode I) \n#  N -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear tensile-softening diagram (fracture mode I) \n#  O -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear tensile-softening diagram (fracture mode I) \n#  P -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear tensile-softening diagram (fracture mode I) \n#  Q -> Mode I fracture energy (Gf) \n#  R -> Parameter to define the mode I fracture energy available to the new crack \n#  S -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n#  T -> Type of tensile unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n#  U -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n#  V -> Maximum n. of cracks \n#  X -> Threshold angle (degrees) \n";

                    create_help_image_tab("NLMM141-1.png", "Generic Pro.");
                    create_help_image_tab("NLMM141-2.png", "Generic Pro.");

                    create_help_image_tab("NLMM104-2-TRI.png", "Fracture mode I-TRI");
                    create_help_image_tab("NLMM104-2-CORN.png", "Fracture mode I-CORN");


                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "X", };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.NLMM141_n, C_variables.NLMM141_list);

                    break;

                case "NLMM151":
                    txtinfo.Text = "## Keyword: _NLMM151 \n## Properties of the NLMM151 material model \n # N. of NLMM151 materials \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Poisson's coefficient \n#  F -> Young's modulus \n#  G -> Compressive strength \n#  H -> Strain at compressive strength \n#  I -> Parameter defining the linear part of the compressive strength \n#  J -> Compressive fracture energy \n#  K -> Type of hardening function after peak load \n#       - Available keywords: _CEB_FIP93 or _PERFECT_PLASTIC \n#  L -> Volume fraction of fibers (Vf) \n# \n#       Note: for plane concrete a Value of zero should be set for this parameter \n# \n#  M -> Aspect ratio of fibers (lf/Df) \n#  N -> Length efficiency factor of fibers \n#  O -> Orientation factor of fibers \n#  P -> Ultimate bond strength \n#  Q -> Tensile strength \n#  R -> Type of tensile-softening diagram \n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN \n#  S -> Ratio between the strain at the first post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  T -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  U -> Ratio between the strain at the second post-peak point and the \n#       ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  V -> Ratio between the stress at the second post-peak point and the \n#       tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I) \n#  W -> Ratio between the strain at the third post-peak point and the \n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I) \n#  X -> Ratio between the stress at the third post-peak point and the \n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I) \n#  Y -> Mode I fracture energy (Gf) \n#  Z -> Parameter to define the mode I fracture energy available to the new crack \n# AA -> Type of shear-softening diagram \n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN \n# AB -> Ratio between the (shear strain minus peak shear strain) at the first post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n# AC -> Ratio between the the shear stress at the first post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n# AD -> Ratio between the (shear strain minus peak shear strain) at the second post-peak point and \n#       the (ultimate shear strain minus peak shear strain) of the trilinear shear-softening diagram \n# AE -> Ratio between the the shear stress at the second post-peak point and the \n#       shear strength of the trilinear shear-softening diagram \n# AF -> Type of shear retention factor law (keyword or constant value) \n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC \n# AG -> Type of crack shear modelling approach (used in shear retention model) \n#       - Available keywords: _INCREMENTAL or _TOTAL \n# AH -> Crack shear strength \n# AI -> Shear fracture energy (Gfs=GfII=GfIII) \n# AJ -> Type of unloading/reloading diagram \n#       - Available keywords: _SECANT or _ELASTIC \n# AK -> Crack band width for the fracture mode I (keyword or constant value) \n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP \n# AL -> Maximum n. of cracks \n# AM -> Threshold angle (degrees) \n";

                    create_help_image_tab("NLMM104-2-TRI.png", "Fracture mode I-TRI");
                    create_help_image_tab("NLMM104-2-QUAD.png", "Fracture mode I-QUAD");
                    create_help_image_tab("NLMM104-2-CORN.png", "Fracture mode I-CORN");
                    create_help_image_tab("NLMM104-3.png", "Fracture mode II-Linear");
                    create_help_image_tab("NLMM104-5-TRI.png", "Fracture mode II-TRI");
                    create_help_image_tab("NLMM104-5-CORN.png", "Fracture mode II-CORN");

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.NLMM151_n, C_variables.NLMM151_list);

                    break;

                case "NLMM201":
                    txtinfo.Text = "## Properties of the NLMM201 material model \n## Keyword: _NLMM201 \n # N. of NLMM201 materials \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the material model \n#  C -> Mass per unit volume \n#  D -> Temperature coefficient \n#  E -> Strain at the end of the first branch \n#  F -> Stress at the end of the first branch \n#  G -> Strain at the end of the second branch \n#  H -> Stress at the end of the second branch \n#  I -> Strain at the end of the third branch \n#  J -> Stress at the end of the third branch \n#  K -> Third branch exponent \n ";

                    create_help_image_tab("NLMM201.png", "Stress-strain");

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.NLMM201_n, C_variables.NLMM201_list);


                    break;

                case "NLMM202":
                    txtinfo.Text = "## Properties of the NLMM202 material model\n## Keyword: _NLMM202\n# N. of NLMM202 materials\n\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Mass per unit volume\n#  D -> Temperature coefficient\n#  E -> Elastic modulus\n#  F -> Tangent modulus at strain hardening\n#  G -> Yielding strain\n#  H -> Yielding stress\n#  I -> Hardening strain\n#  J -> Hardening stress\n#  K -> Strain at the ultimate stress\n#  L -> Ultimate stress\n#  A  B        C        D       E         F 		 G        H      I      J      K      L";
                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" ,"L"};
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);
                    fill_DG1_femix(C_variables.NLMM202_n, C_variables.NLMM202_list);
                    break;
                case "NLMM210":
                    txtinfo.Text = "## Properties of the NLMM210 material model\n## Keyword: _NLMM210\n# N. of NLMM210 materials\n\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Mass per unit volume\n#  D -> Temperature coefficient\n#  E -> Concrete initial Young modulus\n#  F -> Concrete strain at peak confined stress\n#  G -> Confined concrete stress\n#  H -> Non-dimensional critical strain on the compression envelope\n#  I -> Strain at peak tension stress\n#  J -> Concrete tension strength\n#  K -> Critical strain on the tension envelope curve\n#  A  B       C        D        E        F 			G      H        I         J        K";
                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);
                    fill_DG1_femix(C_variables.NLMM210_n, C_variables.NLMM210_list);
                    break;
                case "NLMM220":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM220> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM298":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM298> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM299":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM299> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM123":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM123> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM121":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM121> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM124":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM124> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM125":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM125> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM142":
                    txtinfo.Text = "## Properties of the NLMM142 material model\n## Keyword: _NLMM142\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Mass per unit volume\n#  D -> Temperature coefficient\n#  E -> Poisson's coefficient\n#  F -> Young's modulus\n#  G -> Compressive strength\n#  H -> Strain at compression peak stress\n#  I -> Parameter defining the initial yield surface\n#  J -> Type of hardeding function after peak load\n#       - Available keywords: _CEB_FIP93 or _PERFECT_PLASTIC\n#  K -> Volume fraction of fibers (Vf)\n#\n#       Note: for plane concrete a Value of zero should be set for this parameter\n#\n#  L -> Aspect ratio of fibers (lf/Df)\n#  M -> Length efficiency factor of fibers\n#  N -> Orientation factor of fibers\n#  O -> Ultimate bond strength\n#  P -> Tensile strength\n#  Q -> Type of tensile-softening diagram\n#       - Available keywords: _TRILINEAR, _QUADRILINEAR or _CORNELISSEN\n#  R -> Ratio between the strain at the first post-peak point and the\n#      ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  S -> Ratio between the stress at the first post-peak point and the\n#     tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  T -> Ratio between the strain at the second post-peak point and the\n#      ultimate strain of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  U -> Ratio between the stress at the second post-peak point and the\n#     tensile strength of the trilinear/quadrilinear tensile-softening diagram (fracture mode I)\n#  V -> Ratio between the strain at the third post-peak point and the\n#       ultimate strain of the quadrilinear tensile-softening diagram (fracture mode I)\n#  W -> Ratio between the stress at the third post-peak point and the\n#       tensile strength of the quadrilinear tensile-softening diagram (fracture mode I)\n#  X -> Mode I fracture energy (Gf)\n#  Y -> Type of mode I fracture energy update law\n#       - Available keywords: _NONE, _LINEAR, _QUADRATIC or _CUBIC\n#  Z -> Ultimate crack shear sliding (keyword or constant value)\n#       - Available keywords: _MAXIMUM_CRACKWIDTH\n#  AA -> Parameter to define the mode I fracture energy available to the new crack\n#  AB -> Type of shear-softening diagram\n#       - Available keywords: _NONE or _LINEAR\n#  AC -> Type of shear retention factor law (keyword or constant value)\n#       - Available keywords: _LINEAR, _QUADRATIC or _CUBIC\n#  AD -> Type of crack shear modelling appoach (used in shear retention model)\n#       - Available keywords: _INCREMENTAL or _TOTAL\n#  AE -> Crack shear strength\n#  AF -> Shear fracture energy (Gfs=GfII) (in nonlinear Mindlin shell: In-plane shear fracture energy)\n#  AG -> Type of unloading/reloading diagram\n#       - Available keywords: _SECANT or _ELASTIC\n#  AH -> Crack band width for the fracture mode I (keyword or constant value)\n#       - Available keywords: _SQRT_ELEMENT or _SQRT_IP\n# AI -> Maximum n. of cracks\n# AJ -> Threshold angle (degress)\n#\n# Out-of-plane shear data:\n#    -> Used in nonlinear Mindlin shell\n#\n# AK -> Type of out-of-plane shear-softening diagram\n#       - Available keywords: _NONE, _LINEAR, _TRILINEAR or _CORNELISSEN\n# AL -> Minimum out-of-plane shear stress for softening behavior\n# AM -> Out-of-plane shear fracture energy\n# AN -> Ratio between the out-of-plane shear strain at the first\n#       post-peak point and the ultimate out-of-plane shear strain\n#       trilinear out-of-plane shear-softening diagram\n# AO -> Ratio between the out-of-plane shear stress at the first\n#       post-peak point and the out-of-plane shear strength of the\n#       trilinear out-of-plane shear-softening diagram\n# AP -> Ratio between the out-of-plane shear strain at the second\n#       post-peak point and the ultimate out-of-plane shear strain\n#       of the trilinear out-of-plane shear-softening diagram\n# AQ -> Ratio between the out-of-plane shear stress at the second\n#       post-peak point and the out-of-plane shear strength of the\n#       trilinear out-of-plane shear-softening diagram";
                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);
                    fill_DG1_femix(C_variables.NLMM142_n, C_variables.NLMM142_list);
                    break;
                case "NLMM145":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM145> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM211":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM211> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM102":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM102> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM122":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM122> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM401":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM401> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM451":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM451> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM301":
                    txtinfo.Text = "## Properties of the NLMM301 material model\n## Keyword: _NLMM301\n# N. of NLMM301 materials\n\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Slip at the end of the linear bond stress-slip relationship\n#  D -> Slip at peak bond stress\n#  E -> Peak bond stress\n#  F -> Parameter used in the definition of the pre-peak bond stress-slip relationship\n#  G -> Parameter used in the definition of the post-peak bond stress-slip relationship\n#  H -> Normal stiffness";
                    Dg_col_name = new List<string>() { "B","C","D","E","F","G","H" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);
                    fill_DG1_femix(C_variables.NLMM301_n, C_variables.NLMM301_list);
                    break;
                case "NLMM302":
                    txtinfo.Text = "## Keyword: _NLMM302\n## Properties of the NLMM302 material model\n# N. of NLMM302 materials\n\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Constitutive value defining fracture mode II\n#  D -> Initial constitutive value defining fracture mode I\n#  E -> Tensile strength\n#  F -> First post-peak point of the crack opening\n#  G -> Ratio between the stress at the first post-peak point and the \n#       tensile strength of the tensile-softening diagram\n#  H -> Second post-peak point of the crack opening\n#  I -> Ratio between the stress at the second post-peak point\n#       and the tensile strength of the tensile-softening diagram\n#  J -> Ultimate post-peak point of the crack opnening";
                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H","I","J" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);
                    fill_DG1_femix(C_variables.NLMM302_n, C_variables.NLMM302_list);
                    break;
                case "NLMM303":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM303> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM305":
                    txtinfo.Text = "## Properties of the NLMM305 material model\n## Keyword: _NLMM305\n# N. of NLMM305 materials\n\n## Content of each column:\n#  A -> Counter\n#  B -> Name of the material model\n#  C -> Slip at the end of the linear bond stress-slip relationship\n#  D -> Slip at peak bond stress\n#  E -> Material cohesion\n#  F -> Friction angle between materials in contact\n#  G -> Parameter used in the definition of the pre-peak bond stress-slip relationship\n#  H -> Parameter used in the definition of the post-peak bond stress-slip relationship\n#  I -> Normal stiffness";
                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I"};
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);
                    fill_DG1_femix(C_variables.NLMM305_n, C_variables.NLMM305_list);
                    break;
                case "NLMM306":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM306> could not be edited in this form. You could only modify it in the data file.");
                    break;
                case "NLMM320":
                    txtinfo.Text = "##";
                    //Dg_col_name = new List<string>() { };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    //fill_DG1_femix(C_variables.xxxxxx_n, C_variables.xxxxxx_list);
                    MessageBox.Show("Error: This keyword <NLMM320> could not be edited in this form. You could only modify it in the data file.");
                    break;

                case "CROSS_SECTION_AREAS":
                    txtinfo.Text = "## Keyword: _CS_AREA \n## Cross section areas \n# N. of cross section areas \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the cross section \n#  C -> Area \n";

                    Dg_col_name = new List<string>() { "B", "C" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.CROSS_SECTION_AREAS_n, C_variables.CROSS_SECTION_AREAS_list);

                    break;

                case "FRAME_2D_CROSS_SECTIONS":
                    txtinfo.Text = "## Keyword: _CS_2D \n## Cross section properties of the 2D frame elements \n# N. of 2D frame cross sections \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the cross section \n#  C -> Area \n#  D -> Shear factor \n#  E -> Moment of inertia \n#  F -> Cross section height (used with differential temperature only) \n";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.FRAME_2D_CROSS_SECTIONS_n, C_variables.FRAME_2D_CROSS_SECTIONS_list);

                    break;

                case "FRAME_3D_CROSS_SECTIONS":
                    txtinfo.Text = "## Keyword: _CS_3D \n## Cross section properties of the 3D frame elements \n# N. of 3D frame cross sections \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the cross section \n#  C -> Area \n#  D -> Shear factor (l2) \n#  E -> Shear factor (l3) \n#  F -> Torsional constant \n#  G -> Moment of inertia (l2) \n#  H -> Moment of inertia (l3) \n#  I -> Local coordinate of the shear center (l2) \n#  J -> Local coordinate of the shear center (l3) \n#  K -> Cross section height (l2) (used with differential temperature only) \n#  L -> Cross section height (l3) (used with differential temperature only) \n#  M -> Angle (degrees)";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.FRAME_3D_CROSS_SECTIONS_n, C_variables.FRAME_3D_CROSS_SECTIONS_list);

                    break;

                case "LAMINATE_THICKNESSES":
                    txtinfo.Text = "## Keyword: _LAM_THICK \n## Thickness of the laminate elements \n# N. of nodal thicknesses \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the thickness \n#  C -> Thickness \n";

                    Dg_col_name = new List<string>() { "Name", "Thickness" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.LAMINATE_THICKNESSES_n, C_variables.LAMINATE_THICKNESSES_list);

                    break;

                case "GEOMETRY_PATTERNS":
                    txtinfo.Text = "## Keyword: _GEOM_PATT \n## Geometry patterns of several types of properties \n# N. of geometry patterns \n\n## Content of each column: \n#  A -> Counter \n#  B -> Pattern name \n#  C -> Type of geometry description \n#       - Available keywords: _CS_AREA, _CS_2D, _CS_3D or _LAM_THICK \n#  D -> N. of nodes of the pattern \n#  E -> Names of geometry descriptions \n";

                    //Dg_col_name = new List<string>() { "B", "C", "D", "E" };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);
                    DG1.Columns.Add("B", "Pattern name");

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>() { "_CS_AREA", "_CS_2D", "_CS_3D", "_LAM_THICK" };
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "Type of geometry";
                    dgvCmb.Name = "C";
                    DG1.Columns.Add(dgvCmb);

                    double max_point = 0;
                    if (C_variables.GEOMETRY_PATTERNS_n > 0)
                        foreach (var item in C_variables.GEOMETRY_PATTERNS_list)
                            max_point = Math.Max(double.Parse(item[4]), max_point);


                    DG1.Columns.Add("D", "N. of nodes of the pattern");



                    for (int i = 0; i < max_point; i++)
                    {
                        //DG1.Columns.Add("E" + (i + 1), "E" + (i + 1));
                        dgvCmb = new DataGridViewComboBoxColumn();
                        dgvCmb.DataSource = Name_of_all_geometries();
                        dgvCmb.HeaderText = "E" + (i + 1);
                        dgvCmb.Name = "E" + (i + 1);
                        DG1.Columns.Add(dgvCmb);
                    }


                    fill_DG1_femix(C_variables.GEOMETRY_PATTERNS_n, C_variables.GEOMETRY_PATTERNS_list);

                    break;

                case "FIBROUS_PATTERNS":
                    txtinfo.Text = "## Keyword: _FIB_PATT \n## Lists of files describing fibrous cross sections \n# N. of lists of files describing fibrous cross sections \n\n## Content of each column: \n#  A -> Counter \n#  B -> Material/geometry name \n#  C -> N. of nodes of the fibrous element \n#  D -> File names defining the properties of the fibrous cross section \n# \n#  Note: when C is equal to 1 the cross section is constant \n";

                    Dg_col_name = new List<string>() { "B", "C", "D" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.FIBROUS_PATTERNS_n, C_variables.FIBROUS_PATTERNS_list);

                    break;

                case "LAYER_PROPERTIES":
                    txtinfo.Text = "## Layer properties \n# N. of layer properties \n\n## Content of each column: \n#  A -> Counter \n#  B -> Layer name \n#  C -> Material type \n#  D -> Material name \n#  E -> Geometry type \n#  F -> Geometry name \n";

                    Dg_col_name = new List<string>() { "Layer name" };//B
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);


                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>(){ "_LIN_ISO", "_LIN_ORTHO", "_NLMM101", "_NLMM104","_NLMM111","_NLMM141","_NLMM151",
                        "_NLMM201", "_NLMM202", "_NLMM210", "_NLMM220", "_NLMM298", "_NLMM299", "_NLMM123", "_NLMM121", "_NLMM124",
                        "_NLMM125", "_NLMM142", "_NLMM145", "_NLMM211", "_NLMM102", "_NLMM122", "_NLMM401", "_NLMM451", "_NLMM301",
                        "_NLMM302", "_NLMM303", "_NLMM305", "_NLMM306", "_NLMM320"};
                    //DgDs.Add("_LIN_ISO");
                    //DgDs.Add("_LIN_ORTHO");
                    //DgDs.Add("_NLMM101");
                    //DgDs.Add("_NLMM104");
                    //DgDs.Add("_NLMM111");
                    //DgDs.Add("_NLMM141");
                    //DgDs.Add("_NLMM151");
                    //DgDs.Add("_NLMM201");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "Material type";
                    dgvCmb.Name = "C";
                    DG1.Columns.Add(dgvCmb);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    dgvCmb.HeaderText = "Material name";
                    dgvCmb.DataSource = Name_of_all_materials();
                    dgvCmb.Name = "D";
                    DG1.Columns.Add(dgvCmb);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_NONE");
                    DgDs.Add("_LAM_THICK");
                    DgDs.Add("_GEOM_PATT");
                    //DgDs.Add("_CS_AREA");
                    //DgDs.Add("_CS_2D");
                    //DgDs.Add("_CS_3D");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "Geometry type";
                    dgvCmb.Name = "E";
                    DG1.Columns.Add(dgvCmb);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    dgvCmb.DataSource = Name_of_all_geometries();
                    dgvCmb.HeaderText = "Geometry name";
                    dgvCmb.Name = "F";
                    DG1.Columns.Add(dgvCmb);


                    fill_DG1_femix(C_variables.LAYER_PROPERTIES_n, C_variables.LAYER_PROPERTIES_list);

                    break;

                case "LAYER_PATTERNS":
                    txtinfo.Text = "## Keyword: _LAY_PATT \n## Layer patterns \n# N. of layer patterns \n\n## Content of each column: \n#  A -> Counter \n#  B -> Pattern name \n#  C -> N. of layers \n#  E -> Layer (or layer range) \n#  D -> Name of the layer properties in the [LAYER_PROPERTIES] block \n";

                    Dg_col_name = new List<string>() { "Pattern name", "N. of layers", "Layer(s)" };//B,C,D,E
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    Create_combobox_column_femix(C_variables.LAYER_PROPERTIES_n, C_variables.LAYER_PROPERTIES_list, 2, false, "E", "Layer properties");


                    fill_DG1_femix(C_variables.LAYER_PATTERNS_n, C_variables.LAYER_PATTERNS_list);

                    break;

                case "NUMERICAL_INTEGRATION":
                    txtinfo.Text = "## Keywords: _GLEG, _GLOB or _NCOTES \n## Numerical integration definition \n# N. of layouts of integration points \n\n## Content of each column: \n#  A -> Counter \n#  B -> Name of the layout \n#  C -> N. of keywords defining the layout \n#  D -> Keywords defining the type of term and the n. of points in each direction \n# \n#  Valid syntax for D is the following: \n#     _i_Sj_k \n# \n#     i: type of terms \n#       G -> General use (stiffness matrix, springs and load vector) \n#       M -> Membrane terms (stiffness matrix) \n#       S -> Shear terms (stiffness matrix) \n#       T -> Torsional terms (stiffness matrix) \n#       B -> Bending terms (stiffness matrix) \n# \n#     j: direction \n#       1 -> direction s1 \n#       2 -> direction s2 \n#       3 -> direction s3 \n# \n#     k: n. of points in the specified direction \n# \n#     Note: k must be in the range [1,10] \n# \n#     Examples: _G_S1_5, _M_S2_1, _S_S3_2, _T_S1_7, _B_S2_10 \n";

                    Dg_col_name = new List<string>() { "Name", "N. of keywords" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    max_point = 0;
                    if (C_variables.NUMERICAL_INTEGRATION_n > 0)
                    {
                        foreach (var item in C_variables.NUMERICAL_INTEGRATION_list)
                        {
                            //if (item[0] != "0")
                            //{
                            max_point = Math.Max(double.Parse(item[3]), max_point);
                            //}
                        }
                    }
                    for (int i = 0; i < max_point; i++)
                    {
                        DG1.Columns.Add("D" + (i + 1), "D" + (i + 1));
                    }

                    fill_DG1_femix(C_variables.NUMERICAL_INTEGRATION_n, C_variables.NUMERICAL_INTEGRATION_list);

                    break;

                case "ELEMENT_NODES":
                    txtinfo.Text = "## Nodes defining the elements \n# N. of elements \n\n## Content of each column: \n#  A -> Counter \n#  B -> N. of nodes of the element \n#  C -> Nodes of the element \n";

                    max_point = 0;
                    if (C_variables.ELEMENT_NODES_n > 0)
                    {
                        foreach (var item in C_variables.ELEMENT_NODES_list)
                        {
                            //if (item[0] != "0")
                            //{
                            max_point = Math.Max(double.Parse(item[2]), max_point);
                            //}
                        }
                    }

                    Dg_col_name = new List<string>() { "N. of nodes" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    for (int i = 0; i < max_point; i++)
                    {
                        DG1.Columns.Add("C" + (i + 1), "C" + (i + 1));
                    }

                    fill_DG1_femix(C_variables.ELEMENT_NODES_n, C_variables.ELEMENT_NODES_list);

                    break;

                case "ELEMENT_PROPERTIES":
                    txtinfo.Text = "## Specification of the element properties \n# N. of specifications \n\n## Content of each column: \n#  A -> Counter \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Element (or element range) \n#  E -> Element type \n#  F -> Material type \n#  G -> Material name \n#  H -> Geometry type \n#  I -> Geometry name \n#  J -> Integration type (stiffness matrix) \n#  K -> Integration name (stiffness matrix) \n";

                    Create_combobox_column_femix(C_variables.GROUP_NAMES_n, C_variables.GROUP_NAMES_list, 2, false, "B", "Group");
                    DG1.Columns.Add("C", "Phase");
                    DG1.Columns.Add("D", "Element");

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();

                    DgDs.Add("_TRUSS_2D");
                    DgDs.Add("_TRUSS_3D");
                    DgDs.Add("_FRAME_2D");
                    DgDs.Add("_FRAME_3D");
                    DgDs.Add("_CABLE_2D");
                    DgDs.Add("_CABLE_3D");
                    DgDs.Add("_TIMOSHENKO_BEAM_2D");
                    DgDs.Add("_TIMOSHENKO_BEAM_3D");
                    DgDs.Add("_PLANE_STRESS_TRIANG");
                    DgDs.Add("_PLANE_STRESS_QUAD");
                    DgDs.Add("_PLANE_STRESS_QUAD_THERMAL");
                    DgDs.Add("_PLANE_STRAIN_TRIANG");
                    DgDs.Add("_PLANE_STRAIN_QUAD");
                    DgDs.Add("_AXIS_LINE");
                    DgDs.Add("_AXIS_TRIANG");
                    DgDs.Add("_AXIS_QUAD");
                    DgDs.Add("_MINDLIN_SLAB_TRIANG");
                    DgDs.Add("_MINDLIN_SLAB_QUAD");
                    DgDs.Add("_MINDLIN_SHELL_TRIANG");
                    DgDs.Add("_MINDLIN_SHELL_QUAD");
                    DgDs.Add("_AHMAD_SHELL_TRIANG");
                    DgDs.Add("_AHMAD_SHELL_QUAD");
                    DgDs.Add("_SOLID_TETRA");
                    DgDs.Add("_SOLID_HEXA");
                    DgDs.Add("_SOLID_HEXA_THERMAL");
                    DgDs.Add("_INTERFACE_POINT");
                    DgDs.Add("_INTERFACE_LINE_2D");
                    DgDs.Add("_INTERFACE_LINE_3D");
                    DgDs.Add("_INTERFACE_TRIANG");
                    DgDs.Add("_INTERFACE_QUAD");
                    DgDs.Add("_EMB_CABLE_2D");
                    DgDs.Add("_EMB_CABLE_3D");

                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "Element type";
                    dgvCmb.Name = "E";
                    DG1.Columns.Add(dgvCmb);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>(){ "_LIN_ISO", "_LIN_ORTHO", "_NLMM101", "_NLMM104","_NLMM111","_NLMM141","_NLMM151",
                        "_NLMM201", "_NLMM202", "_NLMM210", "_NLMM220", "_NLMM298", "_NLMM299", "_NLMM123", "_NLMM121", "_NLMM124",
                        "_NLMM125", "_NLMM142", "_NLMM145", "_NLMM211", "_NLMM102", "_NLMM122", "_NLMM401", "_NLMM451", "_NLMM301",
                        "_NLMM302", "_NLMM303", "_NLMM305", "_NLMM306", "_NLMM320","_LAY_PATT","_LIN_INT_POINT","_LIN_INT_LINE","_LIN_INT_SURF","_NONE"};

                    //DgDs.Add("_LIN_ISO");
                    //DgDs.Add("_LIN_ORTHO");
                    //DgDs.Add("_NLMM101");
                    //DgDs.Add("_NLMM104");
                    //DgDs.Add("_NLMM111");
                    //DgDs.Add("_NLMM141");
                    //DgDs.Add("_NLMM151");
                    //DgDs.Add("_NLMM201");
                    //DgDs.Add("_LAY_PATT");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "Material type";
                    dgvCmb.Name = "F";
                    DG1.Columns.Add(dgvCmb);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    dgvCmb.HeaderText = "Material name";
                    dgvCmb.DataSource = Name_of_all_materials();
                    dgvCmb.Name = "G";
                    DG1.Columns.Add(dgvCmb);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_NONE");
                    DgDs.Add("_LAM_THICK");
                    DgDs.Add("_CS_AREA");
                    DgDs.Add("_CS_2D");
                    DgDs.Add("_CS_3D");
                    DgDs.Add("_GEOM_PATT");

                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "Geometry type";
                    dgvCmb.Name = "H";
                    DG1.Columns.Add(dgvCmb);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    dgvCmb.DataSource = Name_of_all_geometries();
                    dgvCmb.HeaderText = "Geometry name";
                    dgvCmb.Name = "I";
                    DG1.Columns.Add(dgvCmb);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_NONE");
                    DgDs.Add("_GLEG");
                    DgDs.Add("_GLOB");
                    DgDs.Add("_NCOTES");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "Integration type";
                    dgvCmb.Name = "J";
                    DG1.Columns.Add(dgvCmb);

                    Create_combobox_column_femix(C_variables.NUMERICAL_INTEGRATION_n, C_variables.NUMERICAL_INTEGRATION_list, 2,true, "K", "Integration name");

                    //Dg_col_name = new List<string>() {"K" };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.ELEMENT_PROPERTIES_n, C_variables.ELEMENT_PROPERTIES_list);

                    break;

                case "LINEAR_SPRING_MATERIALS":
                    txtinfo.Text = "## Keyword: _LIN_SPRING \n## Properties of the linear spring materials \n# N. of linear spring materials \n\n## Content of each column: \n#  A -> Counter \n#  B -> Material name \n#  C -> Stiffness \n";

                    Dg_col_name = new List<string>() { "B", "C" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.LINEAR_SPRING_MATERIALS_n, C_variables.LINEAR_SPRING_MATERIALS_list);

                    break;

                case "POINT_SPRINGS":
                    txtinfo.Text = "## Points with springs \n# N. of point springs \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Point number (or point number range) \n#  E -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  F -> Spring location: \n#       - Direction vector from the structure (S) to the ground (G): _SG \n#       - Direction vector from the ground (G) to the structure (S): _GS \n#         Example: \n#           Direction vector: -----> _XG1 \n#    E     F \n#           Structure - Spring - Ground  =>  _XG1  _SG \n#           Ground - Spring - Structure  =>  _XG1  _GS \n#  G -> Dof type (_TRANS - Translational; _ROT - Rotational) \n#  H -> Spring material type \n#  I -> Spring material name(s) \n";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.POINT_SPRINGS_n, C_variables.POINT_SPRINGS_list);

                    break;

                case "LINE_SPRINGS":
                    txtinfo.Text = "## Lines with springs \n# N. of line springs \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Element number (or element number range) \n#  E -> Face number \n#  F -> Edge number \n#  G -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Timoshenko beam: _L1, _L2 or _L3 \n#       - Solid/Shell: _T, _NN or _NT \n#       - Plane stress/Plane strain/Axisymmetry: _T or _NT \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  H -> Spring location: \n#       - Direction vector from the structure (S) to the ground (G): _SG \n#       - Direction vector from the ground (G) to the structure (S): _GS \n#         Example: \n#           Direction vector: -----> _XG1 \n#    G     H \n#           Structure - Spring - Ground  =>  _XG1  _SG \n#           Ground - Spring - Structure  =>  _XG1  _GS \n#  I -> Dof type (_TRANS - Translational; _ROT - Rotational) \n#  J -> Integration type (spring stiffness matrix) \n#  K -> Integration name (spring stiffness matrix) \n#  L -> Spring material type \n#  M -> N. of nodes of the edge of the element \n#  N -> Spring material name(s) \n# \n#  Note: when M is equal to 1 the spring is constant \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the bar \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _T  - tangential to the edge \n#     _NN - normal to the edge and normal to the element face \n#     _NT - normal to the edge and tangential to the element face \n";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.LINE_SPRINGS_n, C_variables.LINE_SPRINGS_list);

                    break;

                case "SURFACE_SPRINGS":
                    txtinfo.Text = "## Surface with springs \n# N. of surface springs \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Group name \n#  C -> Phase (or phase range) \n#  D -> Element number (or element number range) \n#  E -> Face number \n#  F -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Solid/Shell: _L1, _L2 or _N \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  G -> Spring location: \n#       - Direction vector from the structure (S) to the ground (G): _SG \n#       - Direction vector from the ground (G) to the structure (S): _GS \n#         Example: \n#           Direction vector: -----> _XG1 \n#    F     G \n#           Structure - Spring - Ground  =>  _XG1  _SG \n#           Ground - Spring - Structure  =>  _XG1  _GS \n#  H -> Integration type (spring stiffness matrix) \n#  I -> Integration name (spring stiffness matrix) \n#  J -> Spring material type \n#  K -> N. of nodes of the face of the element \n#  L -> Spring material name(s) \n# \n#  Notes: \n#         when K is equal to 1 the spring is constant \n#         Only translational stiffness is available \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the face and following the l1 local axis \n#     _L2 - tangential to the face and following the l2 local axis \n#     _N  - normal to the face \n";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.SURFACE_SPRINGS_n, C_variables.SURFACE_SPRINGS_list);

                    break;

                case "STIFFENERS":
                    txtinfo.Text = "## Stiffener elements \n# N. of stiffeners \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Stiffener element (or stiffener element range) \n#  C -> Mother element (or mother element range) \n";

                    Dg_col_name = new List<string>() { "B", "C" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    fill_DG1_femix(C_variables.STIFFENERS_n, C_variables.STIFFENERS_list);

                    break;

                case "LOAD_CASE_PARAMETERS":
                    txtinfo.Text = "# INTEGRATION_TYPE Keywords: _GLEG, _GLOB, _NCOTES or _DEFAULT \n INTEGRATION_NAME Defined in the <NUMERICAL_INTEGRATION> block or _DEFAULT\n";

                    Dg_col_name = new List<string>() { "LOAD_CASE_TITLE" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    Create_combobox_column_femix(C_variables.GROUP_NAMES_n, C_variables.GROUP_NAMES_list, 2, false, "LCG", "LOAD_CASE_GROUP");

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_DEFAULT");
                    DgDs.Add("_GLEG");
                    DgDs.Add("_GLOB");
                    DgDs.Add("_NCOTES");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "INTEGRATION_TYPE";
                    dgvCmb.Name = "INTEGRATION_TYPE";
                    DG1.Columns.Add(dgvCmb);


                    List<string> StringList = new List<string>();
                    StringList.Add("_DEFAULT");
                    if (C_variables.NUMERICAL_INTEGRATION_n > 0)
                    {
                        foreach (var item in C_variables.NUMERICAL_INTEGRATION_list)
                        {
                            StringList.Add(item[2]);
                        }
                    }
                    dgvCmb = new DataGridViewComboBoxColumn();
                    dgvCmb.HeaderText = "Integration name";
                    dgvCmb.DataSource = StringList;
                    dgvCmb.Name = "IN";
                    DG1.Columns.Add(dgvCmb);



                    fill_DG1_femix(C_variables.LOAD_CASE_PARAMETERS_n, C_variables.LOAD_CASE_PARAMETERS_list);

                    break;



                case "GRAVITY_LOAD":
                    txtinfo.Text = "    ## Gravity acceleration (global coordinate system) \n## Content of each column: \n#  A -> Acceleration - XG1 \n#  B -> Acceleration - XG2 \n#  C -> Acceleration - XG3 \n#       A       B       C";

                    Dg_col_name = new List<string>() { "A", "B", "C" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    Create_combobox_load_cases();

                    fill_DG1_femix(C_variables.GRAVITY_LOAD_n, C_variables.GRAVITY_LOAD_list);

                    break;

                case "POINT_LOADS":
                    txtinfo.Text = "## Point loads (global coordinate system) \n# N. of point loads \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded point (or loaded point range) \n#  C -> Force - XG1 \n#  D -> Force - XG2 \n#  E -> Force - XG3 \n#  F -> Moment - XG1 \n#  G -> Moment - XG2 \n#  H -> Moment - XG3 \n";

                    Dg_col_name = new List<string>() { "Loaded point(s)", "Force-XG1", "Force-XG2", "Force-XG3", "Moment-XG1", "Moment-XG2", "Moment-XG3" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);
                    Create_combobox_load_cases();

                    fill_DG1_femix(C_variables.POINT_LOADS_n, C_variables.POINT_LOADS_list);

                    break;

                case "INTERNAL_POINT_LOADS":
                    txtinfo.Text = "## Load applied to a point inside an element (global coordinate system) \n# N. of internal point loads \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element \n#  C -> Auxiliary point with the load (or auxiliary point range) \n#  D -> Force - XG1 \n#  E -> Force - XG2 \n#  F -> Force - XG3 \n#  G -> Moment - XG1 \n#  H -> Moment - XG2 \n#  I -> Moment - XG3 \n";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G", "H", "I" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    Create_combobox_load_cases();

                    fill_DG1_femix(C_variables.INTERNAL_POINT_LOADS_n, C_variables.INTERNAL_POINT_LOADS_list);

                    break;

                case "EDGE_LOADS":
                    txtinfo.Text = "## Load on the edge of an element \n# N. of edge loads \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Loaded face \n#  D -> Loaded edge \n#  E -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Frame: _L1, _L2 or _L3 \n#       - Solid/Shell: _T, _NN or _NT \n#       - Plane stress/Plane strain/Axisymmetry: _T or _NT \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  F -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  G -> N. of nodes of the edge of the element \n#  H -> Load values \n# \n#  Note: when G is equal to 1 the load is constant \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the bar \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _T  - tangential to the edge \n#     _NN - normal to the edge and normal to the element face \n#     _NT - normal to the edge and tangential to the element face \n";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    dgvCmb = new DataGridViewComboBoxColumn();
                    DgDs = new List<string>();
                    DgDs.Add("_FORC");
                    DgDs.Add("_MOM");
                    dgvCmb.DataSource = DgDs;
                    dgvCmb.HeaderText = "F";
                    dgvCmb.Name = "F";
                    DG1.Columns.Add(dgvCmb);

                    Dg_col_name = new List<string>() { "G", "H" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);


                    Create_combobox_load_cases();

                    fill_DG1_femix(C_variables.EDGE_LOADS_n, C_variables.EDGE_LOADS_list);

                    break;

                case "INTERNAL_EDGE_LOADS":
                    txtinfo.Text = "## Line load inside an element \n# N. of internal edge loads \n\n## Content of each column: \n#  A -> Counter \n#  B -> Loaded element \n#  C -> Auxiliary line element with the load \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Frame: _L1, _L2 or _L3 \n#       - Solid surface/Shell: _T, _NN or _NT \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the auxiliary line element \n#  G -> Load values \n# \n#  Notes: \n#  - when F is equal to 1 the load is constant; \n#  - the n. of nodes of the auxiliary line element must match the n. of nodes of the edge of the loaded element. \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the bar \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _T  - tangential to the edge \n#     _NN - normal to the edge and normal to the element face \n#     _NT - normal to the edge and tangential to the element face \n";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    Create_combobox_load_cases();

                    fill_DG1_femix(C_variables.INTERNAL_EDGE_LOADS_n, C_variables.INTERNAL_EDGE_LOADS_list);

                    break;

                case "FACE_LOADS":
                    txtinfo.Text = "## Load on the face of an element \n# N. of face loads \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Loaded face \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Solid/Shell: _L1, _L2 or _N \n#       - Plane stress/Plane strain/Axisymmetry: _L1 or _L2 \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the face of the element \n#  G -> Load values \n# \n#  Note: when F is equal to 1 the load is constant \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the face and following the l1 local axis \n#     _L2 - tangential to the face and following the l2 local axis \n#     _N  - normal to the face \n";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    Create_combobox_load_cases();

                    fill_DG1_femix(C_variables.FACE_LOADS_n, C_variables.FACE_LOADS_list);

                    break;

                case "INTERNAL_FACE_LOADS":
                    txtinfo.Text = "## Surface load inside an element \n# N. of internal face loads \n\n## Content of each column: \n#  A -> Counter \n#  B -> Loaded element \n#  C -> Auxiliary surface element with the load \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Solid surface/Shell: _L1, _L2 or _N \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the auxiliary surface element \n#  G -> Load values \n# \n#  Notes: \n#  - when F is equal to 1 the load is constant; \n#  - the n. of nodes of the auxiliary surface element must match the n. of nodes of the face of the loaded element. \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the face and following the l1 local axis \n#     _L2 - tangential to the face and following the l2 local axis \n#     _N  - normal to the face \n";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E", "F", "G" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    Create_combobox_load_cases();

                    fill_DG1_femix(C_variables.INTERNAL_FACE_LOADS_n, C_variables.INTERNAL_FACE_LOADS_list);

                    break;

                case "TEMPERATURE_VARIATION":
                    txtinfo.Text = "## Elements with temperature variation \n# N. of elements with temperature variation \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> N. of nodes of the element \n#  D -> Temperature variation values \n# \n#  Note: when C is equal to 1 the temperature variation is the same for all the element nodes \n";

                    Dg_col_name = new List<string>() { "B", "C", "D" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    Create_combobox_load_cases();

                    fill_DG1_femix(C_variables.TEMPERATURE_VARIATION_n, C_variables.TEMPERATURE_VARIATION_list);

                    break;

                case "DIFFERENTIAL_TEMPERATURE_VARIATION":
                    txtinfo.Text = "## Elements with differential temperature variation \n# N. of elements with differential temperature variation \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Direction vector: \n#       - Frame: _L2 or _L3 \n#       - Shell: _N \n#  D -> N. of nodes of the element \n#  E -> Differential temperature variation values \n# \n#  Note: when D is equal to 1 the diff. temp. var. is the same for all the element nodes \n# \n#  Direction vector keywords: \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _N  - normal to the shell mean surface \n";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    Create_combobox_load_cases();

                    fill_DG1_femix(C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_n, C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_list);

                    break;

                case "PRESCRIBED_DISPLACEMENTS":
                    txtinfo.Text = "## Points with prescribed displacement \n# N. of prescribed displacements \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Point number (or point number range) \n#  C -> Prescribed degree of freedom \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#  D -> Value of the prescribed displacement \n";

                    //Dg_col_name = new List<string>() { "B", "C", "D" };
                    //foreach (string item in Dg_col_name)
                    //    DG1.Columns.Add(item, item);

                    DG1.Columns.Add("B", "B");

                    List<string> stringlist = new List<string>() { "_D1", "_D2", "_D3", "_R1", "_R2", "_R3" };
                    dgvCmb = new DataGridViewComboBoxColumn();
                    dgvCmb.DataSource = stringlist;
                    dgvCmb.HeaderText = "C";
                    dgvCmb.Name = "C";
                    DG1.Columns.Add(dgvCmb);

                    DG1.Columns.Add("D", "D");

                    Create_combobox_load_cases();

                    fill_DG1_femix(C_variables.PRESCRIBED_DISPLACEMENTS_n, C_variables.PRESCRIBED_DISPLACEMENTS_list);

                    break;

                case "INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS":
                    txtinfo.Text = "## Points with internal prescribed relative displacement \n# N. of internal prescribed relative displacements \n\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Point number - M (Master) (or point number range) \n#  C -> Point number - S (Slave) (or point number range) \n#  D -> Prescribed degree of freedom \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#  E -> Value of the prescribed displacement \n";

                    Dg_col_name = new List<string>() { "B", "C", "D", "E" };
                    foreach (string item in Dg_col_name)
                        DG1.Columns.Add(item, item);

                    Create_combobox_load_cases();

                    fill_DG1_femix(C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_n, C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_list);

                    break;

                case "LOAD_CASE_COMBINATIONS":
                    txtinfo.Text = "\n#   A -> Counter\n#   B -> Combination Title\n#   C -> Group Name\n#   D -> Number of increments in the combinations\n#   E -> Load case number\n#   F -> Load factor\n##  E1-F1   E2-F2   E3-F3 ...\n## E..-FIX1 --> Keep Load factor Constant during increments.\n";

                    DG1.Columns.Add("B", "Combination Title");
                    //DG1.Columns.Add("C", "Group Name");
                    Create_combobox_column_femix(C_variables.GROUP_NAMES_n, C_variables.GROUP_NAMES_list, 2, false, "C", "Group Name");
                    DG1.Columns.Add("D", "N. increments");

                    if (C_variables.LOAD_CASE_PARAMETERS_n > 0)
                    {
                        foreach (var item in C_variables.LOAD_CASE_PARAMETERS_list)
                        {
                            DG1.Columns.Add("E", $"{item[1]}-{item[2]} Load factor");
                        }
                    }


                    if (C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_n > 0)
                    {
                        DG1.RowCount = C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_n + 1;
                        int row_index = 0;
                        foreach (var item in C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list)
                        {
                            DG1.Rows[row_index].Cells[0].Value = item[2];
                            DG1.Rows[row_index].Cells[1].Value = item[3];
                            DG1.Rows[row_index].Cells[2].Value = item[4];
                            for (int i = 5; i < item.Count; i++)
                            {
                                string[] words = item[i].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                                for (int i1 = 3; i1 < DG1.ColumnCount; i1++)
                                {
                                    if (C_variables.Val(words[0]) == C_variables.Val(DG1.Columns[i1].HeaderText.ToString()))
                                        DG1.Rows[row_index].Cells[i1].Value = words[1];
                                }
                            }
                            row_index++;
                        }
                    }


                    break;

                default:
                    break;
            }
        }
        private string create_load_combinations(List<IList<string>> combination_list)
        {
            string _combinations = "";
            int COMBINATION_NUMBER, n_cobmination_local,line_counter;
            double increment;
            string COMBINATION_TITLE, COMBINATION_GROUP, PHASE_NUMBER, comment1, comment2;
            PHASE_NUMBER = "1";
            comment1 = "## Load case factors defining a combination \n# N. of load cases in the combination\n";
            comment2 = "\n## Content of each column:\n#  A -> Counter (or counter range)\n#  B -> Load case number (or load case range)\n#  C -> Load case factor\n#      A         B                C \n";

            if (combination_list.Count == 0) 
            {
                 C_variables.LOAD_CASE_COMBINATIONS_n=0;
                 return "";
            }
            COMBINATION_NUMBER = 0;
            for (int i=0; i < combination_list.Count;i++)
            {
                COMBINATION_TITLE = combination_list[i][2];
                COMBINATION_GROUP = combination_list[i][3];
                n_cobmination_local = C_variables.Val(combination_list[i][4]);

                for (int i1=0;i1< n_cobmination_local; i1++)
                {
                    COMBINATION_NUMBER++;
                    _combinations += "<COMBINATION>\n\n";
                    _combinations += "<COMBINATION_PARAMETERS>\n";
                    _combinations += $"   COMBINATION_NUMBER = {COMBINATION_NUMBER} ; \n";
                    _combinations += $"   COMBINATION_TITLE = {COMBINATION_TITLE} ; \n";
                    _combinations += $"   COMBINATION_GROUP = {COMBINATION_GROUP} ; \n";
                    _combinations += $"   PHASE_NUMBER = {PHASE_NUMBER} ; \n";
                    _combinations += "</COMBINATION_PARAMETERS>\n\n";
                    _combinations += "<LOAD_CASE_FACTORS>\n"+comment1;
                    _combinations += $"   COUNT = {combination_list[i].Count-5} ; \n";
                    _combinations += comment2;
                    line_counter = 0;
                       for (int j=5;j< combination_list[i].Count;j++)
                       {
                        line_counter++;
                        string[] words = combination_list[i][j].Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        if (words[1].ToUpper().Contains("FIX")){
                            increment=double.Parse(words[1].Substring(3));
                        }else{
                            increment = Math.Round((double.Parse(words[1]) / n_cobmination_local) *(i1+1),3);
                        }
                            
                        _combinations += $"       {line_counter}         {words[0]}                {increment} ;\n";
                          // _combinations += combination_list[i][j]+"  ";
                       }
                    _combinations += "</LOAD_CASE_FACTORS>\n\n";
                    _combinations += "</COMBINATION>\n\n";
                }
                
            }

           return _combinations;
        }
        private void create_help_image_tab(string Picture_name, string tabpage_text)
        {
            splitContainer_info_ver.Panel2Collapsed = false;
            PictureBox picture = new PictureBox
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                //Name = "pictureBox1",
                SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
                Image = Image.FromFile("image/"+ Picture_name),
            };
            TabPage tabPage1 = new TabPage();
            //tabPage1.Name = "tabPage1";
            tabPage1.Text = tabpage_text;
            tabPage1.BackColor = Color.White; tabControl1.TabPages.Add(tabPage1); tabPage1.Controls.Add(picture);
        }
        private void fill_DG1_femix(int n_listitems, List<IList<string>> data_list)
        {
            if (n_listitems > 0)
            {
                DG1.RowCount = n_listitems+1;
                int row_index = 0; int col_index = 0;
                foreach (var item in data_list)
                {
                    //if (row_index > 0)
                   // {
                        foreach (var column in item)
                        {
                            //Console.Write(col_index + "-->" + column + "     " );
                            if (col_index > 1)
                            DG1.Rows[row_index].Cells[col_index-2].Value = column;
                            col_index++;
                         }
                    //
                    //Console.WriteLine("\n"+row_index + "__");
                    row_index++;
                    col_index = 0;
                }
            }
        }

        private void Create_combobox_column_femix(int n_listitems, List<IList<string>> data_list,int target_col,bool add_none_string, string col_name,string col_text)
        {

            List<string> StringList = new List<string>();
            if (add_none_string)
                StringList.Add("_NONE");

            if (n_listitems > 0)
            {
                foreach (var item in data_list)
                {
                    //if (item[0]!="0")
                    //{
                        StringList.Add(item[target_col]);
                    //}
                    //row_index++;
                }
            }

            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = col_text;
            dgvCmb.DataSource = StringList;
            dgvCmb.Name = col_name;

            DG1.Columns.Add(dgvCmb);
            //DG1.Rows[1].Cells[1].Value = "Ishver";
            //DG1.Rows[1].Cells["cmbName"].Value = "Ishver";


        }
        private void Create_combobox_load_cases()
        {

            List<string> StringList = new List<string>();

            if (C_variables.LOAD_CASE_PARAMETERS_n > 0)
            {
                foreach (var item in C_variables.LOAD_CASE_PARAMETERS_list)
                {
                    //if (item[0]!="0")
                    //{
                    StringList.Add($"{item[1]}-{item[2]}");
                    //}
                    //row_index++;
                }
            }

            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "n.- Load Case";
            dgvCmb.DataSource = StringList;
            dgvCmb.Name ="load_case";

            DG1.Columns.Add(dgvCmb);
            //DG1.Rows[1].Cells[1].Value = "Ishver";
            //DG1.Rows[1].Cells["cmbName"].Value = "Ishver";


        }

        private List<string> Name_of_all_materials()
        {

            List<string> StringList = new List<string>();
            //iso
            if (C_variables.LINEAR_ISOTROPIC_MATERIALS_n > 0)
                foreach (var item in C_variables.LINEAR_ISOTROPIC_MATERIALS_list)
                        StringList.Add(item[2]);
            //
            if (C_variables.LINEAR_ORTHOTROPIC_MATERIALS_n > 0)
                foreach (var item in C_variables.LINEAR_ORTHOTROPIC_MATERIALS_list)
                        StringList.Add(item[2]);
            //
            if (C_variables.NLMM101_n > 0)
                foreach (var item in C_variables.NLMM101_list)
                        StringList.Add(item[2]);
            //
            if (C_variables.NLMM104_n > 0)
                foreach (var item in C_variables.NLMM104_list)
                        StringList.Add(item[2]);

            //
            if (C_variables.NLMM111_n > 0)
                foreach (var item in C_variables.NLMM111_list)
                        StringList.Add(item[2]);

            //
            if (C_variables.NLMM141_n > 0)
                foreach (var item in C_variables.NLMM141_list)
                        StringList.Add(item[2]);

            //
            if (C_variables.NLMM151_n > 0)
                foreach (var item in C_variables.NLMM151_list)
                        StringList.Add(item[2]);

            //
            if (C_variables.NLMM201_n > 0)
                foreach (var item in C_variables.NLMM201_list)
                        StringList.Add(item[2]);

            if (C_variables.NLMM202_n > 0)
                foreach (var item in C_variables.NLMM202_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM210_n > 0)
                foreach (var item in C_variables.NLMM210_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM220_n > 0)
                foreach (var item in C_variables.NLMM220_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM298_n > 0)
                foreach (var item in C_variables.NLMM298_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM299_n > 0)
                foreach (var item in C_variables.NLMM299_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM123_n > 0)
                foreach (var item in C_variables.NLMM123_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM121_n > 0)
                foreach (var item in C_variables.NLMM121_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM124_n > 0)
                foreach (var item in C_variables.NLMM124_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM125_n > 0)
                foreach (var item in C_variables.NLMM125_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM142_n > 0)
                foreach (var item in C_variables.NLMM142_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM145_n > 0)
                foreach (var item in C_variables.NLMM145_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM211_n > 0)
                foreach (var item in C_variables.NLMM211_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM102_n > 0)
                foreach (var item in C_variables.NLMM102_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM122_n > 0)
                foreach (var item in C_variables.NLMM122_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM401_n > 0)
                foreach (var item in C_variables.NLMM401_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM451_n > 0)
                foreach (var item in C_variables.NLMM451_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM301_n > 0)
                foreach (var item in C_variables.NLMM301_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM302_n > 0)
                foreach (var item in C_variables.NLMM302_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM303_n > 0)
                foreach (var item in C_variables.NLMM303_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM305_n > 0)
                foreach (var item in C_variables.NLMM305_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM306_n > 0)
                foreach (var item in C_variables.NLMM306_list)
                    StringList.Add(item[2]);
            if (C_variables.NLMM320_n > 0)
                foreach (var item in C_variables.NLMM320_list)
                    StringList.Add(item[2]);

            if (C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_n > 0)
                foreach (var item in C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_list)
                    StringList.Add(item[2]);
            if (C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_n > 0)
                foreach (var item in C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_list)
                    StringList.Add(item[2]);
            if (C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_n > 0)
                foreach (var item in C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_list)
                    StringList.Add(item[2]);

            if (C_variables.LAYER_PATTERNS_n > 0)
                foreach (var item in C_variables.LAYER_PATTERNS_list)
                    StringList.Add(item[2]);

            StringList.Add("_NONE");

            return StringList;

        }

        private List<string> Name_of_all_geometries()
        {

            List<string> StringList = new List<string>();
            StringList.Add("_NONE");
            //
            if (C_variables.CROSS_SECTION_AREAS_n > 0)
            {
                foreach (var item in C_variables.CROSS_SECTION_AREAS_list)
                {
                        StringList.Add(item[2]);
                }
            }
            //
            if (C_variables.LAMINATE_THICKNESSES_n > 0)
            {
                foreach (var item in C_variables.LAMINATE_THICKNESSES_list)
                {
                        StringList.Add(item[2]);
                }
            }
            //
            if (C_variables.FRAME_2D_CROSS_SECTIONS_n > 0)
            {
                foreach (var item in C_variables.FRAME_2D_CROSS_SECTIONS_list)
                {
                    StringList.Add(item[2]);
                }
            }
            //
            if (C_variables.FRAME_3D_CROSS_SECTIONS_n > 0)
            {
                foreach (var item in C_variables.FRAME_3D_CROSS_SECTIONS_list)
                {
                    StringList.Add(item[2]);
                }
            }

            if (C_variables.GEOMETRY_PATTERNS_n > 0)
            {
                foreach (var item in C_variables.GEOMETRY_PATTERNS_list)
                {
                    StringList.Add(item[2]);
                }
            }

            return StringList;

        }

        private void fill_support_DG1_femix(int n_listitems, List<IList<string>> data_list)
        {
            if (n_listitems > 0)
            {
                DG1.RowCount = n_listitems + 1;
                int row_index = 0; int col_index = 0;
                foreach (var item in data_list)
                {
                    //if (row_index > 0)
                    //{

                        //DG1.Rows[2].Cells["_D1"].Value = true;
                        //_D1, _D2, _D3, _R1, _R2 , _R3
                        DG1.Rows[row_index].Cells[0].Value = item[2];
                        DG1.Rows[row_index].Cells[1].Value = item[3];
                        DG1.Rows[row_index].Cells[2].Value = item[4];

                    if (item.Count > 5)
                    {
                        for (int i=5;i<5+ Int32.Parse(item[5]); i++)
                        {
                            DG1.Rows[row_index].Cells[item[i + 1]].Value = item[i + 1];
                        }

                    }
                    //}
                    //Console.WriteLine("\n"+row_index + "__");
                    row_index++;
                    col_index = 0;
                }
            }
        }

        private void fill_MAIN_PARAMETERS_DG1_femix(int n_listitems, List<IList<string>> data_list)
        {
            DG1.AllowUserToAddRows = false;
            DG1.AllowUserToDeleteRows = false;

            DG1.Columns[0].Width = 300;

            int row_index = 0; int col_index = 0;

            //0
            DG1.Rows.Add(1);
            DG1.Rows[row_index].HeaderCell.Value = "MAIN_TITLE";
            row_index++;

            //1
            List<string> ComboList = new List<string>() { "_STATIC","_DYNAMIC","_MODAL_ANALYSIS","_MODAL_SUPERPOSITION","_THERMAL_STEADY_STATE","_THERMAL_TRANSIENT","_TRANSIENT" };
            DataGridViewRow Row_combo = new DataGridViewRow();
            DataGridViewComboBoxCell cell_combo = new DataGridViewComboBoxCell();
            cell_combo.DataSource = ComboList;
            cell_combo.Value = "_STATIC";
            Row_combo.Cells.Add(cell_combo);
            DG1.Rows.Add(Row_combo);
            DG1.Rows[row_index].HeaderCell.Value = "ANALYSIS_TYPE";
            row_index++;


            //solver
            //2
            ComboList = new List<string>() { "_SYMMETRIC_NONCONSTANT_SEMIBAND", "_SYMMETRIC_SPARSE" };
            Row_combo = new DataGridViewRow();
            cell_combo = new DataGridViewComboBoxCell();
            cell_combo.DataSource = ComboList;
            cell_combo.Value = "_SYMMETRIC_NONCONSTANT_SEMIBAND";
            Row_combo.Cells.Add(cell_combo);
            DG1.Rows.Add(Row_combo);
            DG1.Rows[row_index].HeaderCell.Value = "STIFFNESS_MATRIX_STORAGE_TECHNIQUE";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightGreen;
            row_index++;

            ////3
            //ComboList = new List<string>() { "_GAUSS_ELIM" };
            //Row_combo = new DataGridViewRow();
            //cell_combo = new DataGridViewComboBoxCell();
            //cell_combo.DataSource = ComboList;
            //cell_combo.Value = "_GAUSS_ELIM";
            //Row_combo.Cells.Add(cell_combo);
            //DG1.Rows.Add(Row_combo);
            //DG1.Rows[row_index].HeaderCell.Value = "SYSTEM_LIN_EQ_ALGORITHM";
            //DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightGreen;
            //row_index++;

            //3
            ComboList = new List<string>() { "_GAUSS_ELIM", "_ITERATIVE_JPCG", "_ITERATIVE_ICFPCG" };
            Row_combo = new DataGridViewRow();
            cell_combo = new DataGridViewComboBoxCell();
            cell_combo.DataSource = ComboList;
            cell_combo.Value = "_GAUSS_ELIM";
            //cell_combo.Value = "_ITERATIVE_JPCG";
            Row_combo.Cells.Add(cell_combo);
            DG1.Rows.Add(Row_combo);
            DG1.Rows[row_index].HeaderCell.Value = "SYSTEM_LIN_EQ_ALGORITHM";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightGreen;
            row_index++;

            //4 for--> _THERMAL_TRANSIENT 
            ComboList = new List<string>() { "_FORWARD_EULER", "_BACKWARD_EULER", "", "_CRANK_NICOLSON", "_GALERKIN_23", "_GALERKIN_13" };
            Row_combo = new DataGridViewRow();
            cell_combo = new DataGridViewComboBoxCell();
            cell_combo.DataSource = ComboList;
            cell_combo.Value = "_FORWARD_EULER";
            Row_combo.Cells.Add(cell_combo);
            DG1.Rows.Add(Row_combo);
            DG1.Rows[row_index].HeaderCell.Value = "THERMAL_TRANSIENT_TIME_STEPPING_METHOD";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightGreen;
            row_index++;

            //5 for-> _SYMMETRIC_SPARSE
            DG1.Rows.Add("1.0e-012");
            DG1.Rows[row_index].HeaderCell.Value = "PRECONDITIONED_RESIDUAL_DECAY";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightGreen;
            row_index++;

            //end solver


            //6
            DataGridViewRow Row_chk = new DataGridViewRow();
            DataGridViewCheckBoxCell cell_chk = new DataGridViewCheckBoxCell() { TrueValue = "_Y", FalseValue = "_N", };
            cell_chk.Style.NullValue = false;
            cell_chk.Value = "_N";
            Row_chk.Cells.Add(cell_chk);
            cell_chk.FlatStyle = FlatStyle.Standard;
            DG1.Rows.Add(Row_chk);
            DG1.Rows[row_index].HeaderCell.Value = "GEOMETRICALLY_NONLINEAR_PROBLEM";
            row_index++;
            //DG1.Rows[6].Cells[0].Value = true;


            //start material nonlinear
            //7
            Row_chk = new DataGridViewRow();
            cell_chk = new DataGridViewCheckBoxCell(){TrueValue = "_Y", FalseValue = "_N", };
            cell_chk.Style.NullValue = false;
            cell_chk.Value = "_N";
            Row_chk.Cells.Add(cell_chk);
            cell_chk.FlatStyle = FlatStyle.Standard;
            DG1.Rows.Add(Row_chk);
            DG1.Rows[row_index].HeaderCell.Value = "MATERIALLY_NONLINEAR_PROBLEM";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightBlue;
            row_index++;

            //8
             ComboList = new List<string>() { "_NEWTON_RAPHSON_STD","_NEWTON_RAPHSON_MOD_1","_NEWTON_RAPHSON_MOD_2","_NEWTON_RAPHSON_MOD_3","_NEWTON_RAPHSON_MOD_4" };
             Row_combo = new DataGridViewRow();
             cell_combo = new DataGridViewComboBoxCell();
            cell_combo.DataSource = ComboList;
            cell_combo.Value = "_NEWTON_RAPHSON_STD";
            Row_combo.Cells.Add(cell_combo);
            DG1.Rows.Add(Row_combo);
            DG1.Rows[row_index].HeaderCell.Value = "ITERATIVE_ALGORITHM";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightBlue;
            row_index++;

            //9
            ComboList = new List<string>() { "_DISPLACEMENT_NORM", "_FORCE_NORM", "_ENERGY" };
            Row_combo = new DataGridViewRow();
            cell_combo = new DataGridViewComboBoxCell();
            cell_combo.DataSource = ComboList;
            cell_combo.Value = "_ENERGY";
            Row_combo.Cells.Add(cell_combo);
            DG1.Rows.Add(Row_combo);
            DG1.Rows[row_index].HeaderCell.Value = "CONVERGENCE_CRITERION";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightBlue;
            row_index++;

            //10
            DG1.Rows.Add("1e-03");
            DG1.Rows[row_index].HeaderCell.Value = "TOLERANCE_IN_EACH_COMBINATION";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightBlue;
            row_index++;

            //11
            DG1.Rows.Add("100");
            DG1.Rows[row_index].HeaderCell.Value = "MAXIMUM_NUMBER_OF_ITERATIONS_IN_EACH_COMBINATION";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightBlue;
            row_index++;

            //12
            DG1.Rows.Add("0");
            DG1.Rows[row_index].HeaderCell.Value = "NUMBER_OF_COMBINATIONS_BEFORE_RESTART";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightBlue;
            row_index++;

            //13
            Row_chk = new DataGridViewRow();
            cell_chk = new DataGridViewCheckBoxCell() { TrueValue = "_Y", FalseValue = "_N", };
            cell_chk.Style.NullValue = false;
            cell_chk.Value = "_N";
            Row_chk.Cells.Add(cell_chk);
            cell_chk.FlatStyle = FlatStyle.Standard;
            DG1.Rows.Add(Row_chk);
            DG1.Rows[row_index].HeaderCell.Value = "ADD_THE_UNBALANCED_FORCES_IN_EACH_COMBINATION";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightBlue;
            row_index++;
            
            //14
            Row_chk = new DataGridViewRow();
            cell_chk = new DataGridViewCheckBoxCell() { TrueValue = "_Y", FalseValue = "_N", };
            cell_chk.Style.NullValue = false;
            cell_chk.Value = "_N";
            Row_chk.Cells.Add(cell_chk);
            cell_chk.FlatStyle = FlatStyle.Standard;
            DG1.Rows.Add(Row_chk);
            DG1.Rows[row_index].HeaderCell.Value = "ARC_LENGTH";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;

            //15
            DG1.Rows.Add("100");
            DG1.Rows[row_index].HeaderCell.Value = "MAXIMUM_NUMBER_OF_ARC_LENGTH_COMBINATIONS";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;

            ///////////Arc Lenght
            //16
            ComboList = new List<string>() { "DEFAULT", "RELATIVE" };
            Row_combo = new DataGridViewRow();
            cell_combo = new DataGridViewComboBoxCell();
            cell_combo.DataSource = ComboList;
            cell_combo.Value = "DEFAULT";
            Row_combo.Cells.Add(cell_combo);
            DG1.Rows.Add(Row_combo);
            DG1.Rows[row_index].HeaderCell.Value = "DISPLACEMENT CONTROL";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //17
            DG1.Rows.Add("");
            DG1.Rows[row_index].HeaderCell.Value = "POINT_NUMBER";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //18
            DG1.Rows.Add("");
            DG1.Rows[row_index].HeaderCell.Value = "DISPLACEMENT_INCREMENT";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //19
            DG1.Rows.Add("");
            DG1.Rows[row_index].HeaderCell.Value = "POINT_A_NUMBER";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //20
            DG1.Rows.Add("");
            DG1.Rows[row_index].HeaderCell.Value = "POINT_B_NUMBER";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //21
            DG1.Rows.Add("");
            DG1.Rows[row_index].HeaderCell.Value = "RELATIVE_DISPLACEMENT_INCREMENT";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //22
            ComboList = new List<string>() { "_D1", "_D2", "_D3" };
            Row_combo = new DataGridViewRow();
            cell_combo = new DataGridViewComboBoxCell();
            cell_combo.DataSource = ComboList;
            cell_combo.Value = "_D3";
            Row_combo.Cells.Add(cell_combo);
            DG1.Rows.Add(Row_combo);
            DG1.Rows[row_index].HeaderCell.Value = "DEGREE_OF_FREEDOM";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //23
            ComboList = new List<string>() { "DEFAULT", "CUSTOM" };
            Row_combo = new DataGridViewRow();
            cell_combo = new DataGridViewComboBoxCell();
            cell_combo.DataSource = ComboList;
            cell_combo.Value = "DEFAULT";
            Row_combo.Cells.Add(cell_combo);
            DG1.Rows.Add(Row_combo);
            DG1.Rows[row_index].HeaderCell.Value = "ARC_LENGTH_PARAMETERS";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //24
            Row_chk = new DataGridViewRow();
            cell_chk = new DataGridViewCheckBoxCell() { TrueValue = "_Y", FalseValue = "_N", };
            cell_chk.Style.NullValue = false;
            cell_chk.Value = "_N";
            Row_chk.Cells.Add(cell_chk);
            cell_chk.FlatStyle = FlatStyle.Standard;
            DG1.Rows.Add(Row_chk);
            DG1.Rows[row_index].HeaderCell.Value = "CONSTANT_RADIUS";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //25
            DG1.Rows.Add("1");
            DG1.Rows[row_index].HeaderCell.Value = "LOAD_FACTOR";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //26
            DG1.Rows.Add("1");
            DG1.Rows[row_index].HeaderCell.Value = "RADIUS_FACTOR";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //27
            DG1.Rows.Add("0");
            DG1.Rows[row_index].HeaderCell.Value = "FORCE_DISPLACEMENT_SCALING_FACTOR";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;
            //28
            DG1.Rows.Add("0");
            DG1.Rows[row_index].HeaderCell.Value = "MOMENT_ROTATION_SCALING_FACTOR";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightCyan;
            row_index++;

            ///////////End of Arc lenght

            //29
            Row_chk = new DataGridViewRow();
            cell_chk = new DataGridViewCheckBoxCell() { TrueValue = "_Y", FalseValue = "_N", };
            cell_chk.Style.NullValue = false;
            cell_chk.Value = "_N";
            Row_chk.Cells.Add(cell_chk);
            cell_chk.FlatStyle = FlatStyle.Standard;
            DG1.Rows.Add(Row_chk);
            DG1.Rows[row_index].HeaderCell.Value = "LINE_SEARCH";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightBlue;
            row_index++;


            //30
            ComboList = new List<string>() { "_INDEPENDENT", "_DEPENDENT"};
            Row_combo = new DataGridViewRow();
            cell_combo = new DataGridViewComboBoxCell();
            cell_combo.DataSource = ComboList;
            cell_combo.Value = "_INDEPENDENT";
            Row_combo.Cells.Add(cell_combo);
            DG1.Rows.Add(Row_combo);
            DG1.Rows[row_index].HeaderCell.Value = "PATH_BEHAVIOR";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.LightBlue;
            row_index++;

            //end material nonlinear

            //31
            DG1.Rows.Add("1");
            DG1.Rows[row_index].HeaderCell.Value = "NUMBER_OF_PHASES";
            row_index++;

            //32
            DG1.Rows.Add("0");
            DG1.Rows[row_index].HeaderCell.Value = "NUMBER_OF_LOAD_CASES";
            DG1.Rows[row_index].ReadOnly = true;
            DG1.Rows[row_index].Cells[0].ToolTipText = "This item is read only!";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.Gray;
            row_index++;

            //33
            DG1.Rows.Add("0");
            DG1.Rows[row_index].HeaderCell.Value = "NUMBER_OF_COMBINATIONS";
            DG1.Rows[row_index].ReadOnly = true;
            DG1.Rows[row_index].Cells[0].ToolTipText = "This item is read only!";
            DG1.Rows[row_index].DefaultCellStyle.BackColor = Color.Gray;

            row_index++;


            // DG1.Rows[7].Visible=false;

            // DG1.Rows[8].DefaultCellStyle.BackColor = Color.LightBlue;


            //DG1.Rows[7].Cells[0].Value ="_Y" ;

            if (n_listitems > 0)
            {
                foreach (var item in data_list)
                {
                    //Console.WriteLine(item[1]);
                    foreach (DataGridViewRow row in DG1.Rows)
                    {
                        if (row.Index < 16 || row.Index > 28) {//except arc lenght
                        if (row.HeaderCell.Value.ToString() == item[1])
                        {
                            row.Cells[0].Value = item[2];
                        }
                    }
                    }
                }
            }
            //arc lenght
            if (C_variables.ARC_LENGTH_PARAMETERS_n > 0)
            {
                foreach (var item in C_variables.ARC_LENGTH_PARAMETERS_list)
                {
                    //Console.WriteLine(item[1]);
                    foreach (DataGridViewRow row in DG1.Rows)
                    {
                        if (row.Index >= 16 && row.Index <= 28)
                        {
                            if (row.HeaderCell.Value.ToString() == item[1])
                            {
                                row.Cells[0].Value = item[2];
                            }
                        }

                        if (item[1] == "DISPLACEMENT_CONTROL" && item[2] == "_Y") {DG1.Rows[16].Cells[0].Value = "DEFAULT"; }
                        if (item[1] == "RELATIVE_DISPLACEMENT_CONTROL" && item[2] == "_Y") { DG1.Rows[16].Cells[0].Value = "RELATIVE"; }
                        if (item[1] == "MOMENT_ROTATION_SCALING_FACTOR") { DG1.Rows[23].Cells[0].Value = "CUSTOM"; }

                        
                    }
                }
            }
        }
        private void dddd()
        {
            DG1.Columns.Add("A", "A");
            DG1.ColumnCount = 4;
            DG1.RowCount = 6;
            DG1.Rows[0].Cells[0].Value = "546875";

           

            //DG1.Rows.Add("hh", "Jignesh");
            //DG1.Rows[0].Height = 50;
            //DG1.Columns[0].Width = 50;
            //DG1.AllowUserToAddRows=false;

            DG1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            DG1.Columns[0].HeaderText = "A";
            DG1.Columns[1].HeaderCell.Value = "h";
            DG1.Rows[0].HeaderCell.Value = "214";
            DG1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
            //DG1.taba
            //DG1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            //System.Windows.Forms.DataGridViewCellStyle dgstyle = new System.Windows.Forms.DataGridViewCellStyle();
            //dgstyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //DG1.Columns[0].DefaultCellStyle = dgstyle;
            //DG1.Columns[0].HeaderCell.Style = dgstyle;


            DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
            dgvCmb.HeaderText = "Namexx";
            //List<string> StringList = new List<string>() { "B", "C", "D" };
            //CellSample.DataSource = StringList; // list of the string items that I want to insert in ComboBox
            //CellSample.Value = StringList[0]; // default value for the ComboBox
            dgvCmb.Items.Add("Ghanashyam");
            dgvCmb.Items.Add("Jignesh");
            dgvCmb.Items.Add("Ishver");
            dgvCmb.Items.Add("Anand");
            dgvCmb.Name = "cmbName";
            
            //DG1.Columns.Add(dgvCmb);
            DG1.Columns.Insert(1, dgvCmb);
            //DG1.Rows[1].Cells[1].Value = "Ishver";
            DG1.Rows[1].Cells["cmbName"].Value = "Ishver";


            //DataGridViewCheckBoxColumn dgvChb = new DataGridViewCheckBoxColumn();
            //dgvChb.HeaderText = "Pass";
            //dgvChb.Name = "chbPass";
            //dgvChb.FlatStyle = FlatStyle.Standard;
            //dgvChb.ThreeState = true;
            //dgvChb.CellTemplate.Style.BackColor = System.Drawing.Color.LightBlue;
            //DG1.Columns.Add(dgvChb);



            //DataGridViewRow RowSample = new DataGridViewRow();
            //DataGridViewComboBoxCell CellSample = new DataGridViewComboBoxCell();
            //CellSample.Items.Add("kkk");
            //DataGridViewCell cell = new DataGridViewTextBoxCell();
            //cell.Value = "CellText"; // creating the text cell
            //RowSample.Cells.Add(cell);
            //RowSample.Cells.Add(CellSample);
            //DG1.Rows.Add(RowSample);
            ////DG1.Rows.Insert(0, RowSample);





            //DataGridViewRow Row_chk = new DataGridViewRow();
            //DataGridViewCheckBoxCell cell_chk = new DataGridViewCheckBoxCell();
            //cell_chk.Style.NullValue = false;
            //Row_chk.Cells.Add(cell_chk);
            //cell_chk.FlatStyle = FlatStyle.Standard;
            //DG1.Rows.Add(Row_chk);
            //DG1.Rows[6].Cells[0].Value = true;


            //DG1.Rows[7].Visible = false;
            //DG1.Rows[8].DefaultCellStyle.BackColor = Color.LightBlue;


            foreach (DataGridViewRow row in DG1.Rows)
                if (row.Index<DG1.RowCount-1)
                    row.HeaderCell.Value = (row.Index + 1).ToString();
            //row.Cells[0].Value = (row.Index + 1).ToString();

        }
        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DG1_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void DG1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void DG1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {

            switch (selected_item)
            {
                case "MAIN_PARAMETERS":
                   
                    break;

                default:
                    foreach (DataGridViewRow row in DG1.Rows)
                    {
                        //row.Height = 30;
                        if (row.Index < DG1.RowCount - 1)
                        {
                            row.HeaderCell.Value = (row.Index + 1).ToString();
                            if (row.Index % 2 != 0)
                            {
                                row.DefaultCellStyle.BackColor = Color.LightGray;
                            }
                            else
                            {
                                row.DefaultCellStyle.BackColor = Color.White;
                            }
                        }
                    }
                    break;
            }
                  
            DG1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
            //DG1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void DG1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            switch (selected_item)
            {
                case "MAIN_PARAMETERS":
                    if (DG1.RowCount > 29)
                    {
                        if (DG1.Rows[7].Cells[0].Value.ToString() == "_Y")
                        {
                            for (int i = 8; i <= 29; i++)
                            {
                                DG1.Rows[i ].Visible = true;
                            }

                            if (DG1.Rows[14].Cells[0].Value.ToString() == "_Y")
                            {
                                for (int i = 15; i <= 28; i++)
                                {
                                    DG1.Rows[i].Visible = true;
                                }

                                if (DG1.Rows[16].Cells[0].Value.ToString() == "DEFAULT")
                                {
                                    DG1.Rows[17].Visible = true;
                                    DG1.Rows[18].Visible = true;
                                    DG1.Rows[19].Visible = false;
                                    DG1.Rows[20].Visible = false;
                                    DG1.Rows[21].Visible = false;
                                }
                                else
                                {
                                    DG1.Rows[17].Visible = false;
                                    DG1.Rows[18].Visible = false;
                                    DG1.Rows[19].Visible = true;
                                    DG1.Rows[20].Visible = true;
                                    DG1.Rows[21].Visible = true;
                                }


                                if (DG1.Rows[23].Cells[0].Value.ToString() == "DEFAULT")
                                {
                                    for (int i = 24; i <= 28; i++)
                                    {
                                        DG1.Rows[i].Visible = false;
                                    }
                                }
                                else
                                {
                                    for (int i = 24; i <= 28; i++)
                                    {
                                        DG1.Rows[i].Visible = true;
                                    }

                                    if (DG1.Rows[24].Cells[0].Value.ToString() == "_Y")
                                    {
                                        DG1.Rows[26].Visible = true;
                                        DG1.Rows[25].Visible = false;
                                    }
                                    else
                                    {
                                        DG1.Rows[26].Visible = false;
                                        DG1.Rows[25].Visible = true;
                                    }

                                }


                            }
                            else
                            {
                                for (int i = 15; i <= 28; i++)
                                {
                                    DG1.Rows[i].Visible = false;
                                }
                            }


                        }
                        else
                        {
                            for (int i = 8; i <= 29; i++)
                            {
                                DG1.Rows[i].Visible = false;
                            }
                        }




                        if (DG1.Rows[1].Cells[0].Value.ToString() == "_THERMAL_TRANSIENT")
                        {
                            DG1.Rows[4].Visible = true;
                        }
                        else
                        {
                            DG1.Rows[4].Visible = false;
                        }

                       
                        if (DG1.Rows[2].Cells[0].Value.ToString() == "_SYMMETRIC_SPARSE")
                        {
                            DG1.Rows[5].Visible = true;
                        }
                        else
                        {
                            DG1.Rows[5].Visible = false;
                        }


                    }
                    break;

                case "NUMERICAL_INTEGRATION":
                    //column 1
                    if (DG1.Rows[e.RowIndex].Cells[1].Value!=null)
                    {
                        if (Int32.Parse(DG1.Rows[e.RowIndex].Cells[1].Value.ToString()) > DG1.ColumnCount - 2)
                        {
                            DG1.ColumnCount = Int32.Parse(DG1.Rows[e.RowIndex].Cells[1].Value.ToString()) + 2;
                        }
                    }

                    break;
                case "ELEMENT_NODES":
                    //column 0
                    if (DG1.Rows[e.RowIndex].Cells[0].Value != null)
                    {
                        if (Int32.Parse(DG1.Rows[e.RowIndex].Cells[0].Value.ToString()) > DG1.ColumnCount - 1)
                        {
                            DG1.ColumnCount = Int32.Parse(DG1.Rows[e.RowIndex].Cells[0].Value.ToString()) + 1;
                        }
                    }

                    break;

                case "GEOMETRY_PATTERNS":
                    //column 2
                    
                    if (DG1.Rows[e.RowIndex].Cells[2].Value != null)
                    {
                        if (Int32.Parse(DG1.Rows[e.RowIndex].Cells[2].Value.ToString()) > DG1.ColumnCount - 3)
                        {
                            for (int i = 0; i < Int32.Parse(DG1.Rows[e.RowIndex].Cells[2].Value.ToString())-(DG1.ColumnCount - 3); i++)
                            {
                                //DG1.Columns.Add("E" + (i + 1), "E" + (i + 1));
                                DataGridViewComboBoxColumn dgvCmb = new DataGridViewComboBoxColumn();
                                dgvCmb.DataSource = Name_of_all_geometries();
                                dgvCmb.HeaderText = "E";
                                //dgvCmb.Name = "E";
                                DG1.Columns.Add(dgvCmb);
                            }

                            //DG1.ColumnCount = Int32.Parse(DG1.Rows[e.RowIndex].Cells[2].Value.ToString()) + 3;
                        }
                    }

                    break;


                default:

                    break;
            }
        }

        private void DG1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (DG1.IsCurrentCellDirty)
            {
                DG1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DG1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DG1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {

        }

        private void frm_edit_menu_Click(object sender, EventArgs e)
        {
            if (DG1.SelectedRows.Count > 1)
            {
                MessageBox.Show("Select only one row to edit!");
                return;
            }
            if (DG1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select one row to edit!");
                return;
            }

            //int selected_row_index = DG1.CurrentCell.RowIndex;
            int selected_row_index = DG1.SelectedRows[0].Index;

            string[] Editing_val_arr = new string[DG1.ColumnCount];
            for (int i = 0; i < DG1.ColumnCount; i++)
            {
                Editing_val_arr[i] = "";
                if (DG1.Rows[selected_row_index].Cells[i].Value != null)
                    Editing_val_arr[i] = DG1.Rows[selected_row_index].Cells[i].Value.ToString();
            }
                

            //for (int i = 0; i < Editing_val_arr.Count(); i++)
            //    MessageBox.Show(Editing_val_arr[i].ToString());


            //Editing_val_arr[0] = "kkk";
            //FemixForms.GROUP_NAMES_list = C_variables.GROUP_NAMES_list.ToList();
            //MessageBox.Show(FemixForms.kamyar());
            //return;


            //frm_list_items formlist = new frm_list_items();
            //formlist.selected_item = "GROUP_NAMES";
            //formlist.ShowDialog();
            //load_table_items();


            Editing_val_arr =Femix_Forms.FemixFRM_Class.Run_form(selected_item, Editing_val_arr);

            //load_table_items();

            if (Femix_Forms.FemixFRM_Class.is_new)
            {
                DG1.Rows.Add();
                selected_row_index = DG1.Rows.Count - 2;
            }

            //DG1.ClearSelection();
            //DG1.Rows[selected_row_index].Selected = true;


            if (Editing_val_arr == null)
            {
                MessageBox.Show("You can edit this item on the table.");
                return;
            }
            
            

            for (int i = 0; i < Editing_val_arr.Count(); i++)
            {
                if (DG1.Columns[i].CellType.Name.ToString() == "DataGridViewComboBoxCell")
                {
                    DataGridViewComboBoxColumn theColumn = (DataGridViewComboBoxColumn)this.DG1.Columns[i];//theColumn.DataSource = null;//theColumn.Items.Clear();
                    List<string> dslist1 = (List<string>)theColumn.DataSource;
                    List<string> dslist = dslist1.ToList();//new List<string> { "plate", "plate2","platexxx" };
                    string itemToAdd = Editing_val_arr[i];
                    if (!dslist.Contains(itemToAdd))
                    {
                        dslist.Add(itemToAdd);
                        theColumn.DataSource = dslist;
                    }
                }
                DG1.Rows[selected_row_index].Cells[i].Value= Editing_val_arr[i];
            }

        }

        private void frm_new_menu_Click(object sender, EventArgs e)
        {
            DG1.ClearSelection();
            DG1.Rows.Add();
            DG1.Rows[DG1.Rows.Count-2].Selected = true;
            frm_edit_menu.PerformClick();
        }

        private void deleteRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DG1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select the row(s) to remove!");
                return;
            }
            DataGridViewRow row;
            int length;

            length = DG1.SelectedRows.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                row = DG1.SelectedRows[i];
                DG1.Rows.Remove(row);
            }

        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Copy to clipboard
            DG1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            DataObject dataObj = DG1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void duplicateSelectedRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DG1.SelectedRows.Count !=1)
            {
                MessageBox.Show("Select one row!");
                return;
            }
            int selected_row_index = DG1.SelectedRows[0].Index;

            object[] Editing_val_arr = new object[DG1.ColumnCount];
            for (int i = 0; i < DG1.ColumnCount; i++)
            {
                Editing_val_arr[i] = "";
                if (DG1.Rows[selected_row_index].Cells[i].Value != null)
                    Editing_val_arr[i] = DG1.Rows[selected_row_index].Cells[i].Value;
            }

            DG1.Rows.Insert(selected_row_index+1);
            for (int i = 0; i < Editing_val_arr.Count(); i++)
                DG1.Rows[selected_row_index+1].Cells[i].Value = Editing_val_arr[i];
        }



    }
}
