using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Femix
{
    class C_variables
    {
        public static string Femix_string;
        public static string MAIN_PARAMETERS_tag, GROUP_NAMES_tag, POINT_COORDINATES_tag, AUXILIARY_VECTORS_tag, COORDINATE_SYSTEMS_tag, POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_tag, SUPPORTS_tag, INTERNAL_POINT_LINKS_tag, LINEAR_ISOTROPIC_MATERIALS_tag, LINEAR_ORTHOTROPIC_MATERIALS_tag, LINEAR_INTERFACE_POINT_PROPERTIES_tag, LINEAR_INTERFACE_LINE_PROPERTIES_tag, LINEAR_INTERFACE_SURFACE_PROPERTIES_tag, NLMM101_tag, NLMM104_tag, NLMM111_tag, NLMM141_tag, NLMM151_tag, NLMM201_tag,
            NLMM202_tag, NLMM210_tag, NLMM220_tag, NLMM298_tag, NLMM299_tag, NLMM123_tag, NLMM121_tag, NLMM124_tag, NLMM125_tag, NLMM142_tag, NLMM145_tag, NLMM211_tag, NLMM_PATT_tag, NLMM102_tag, NLMM122_tag, NLMM401_tag, NLMM451_tag, NLMM301_tag, NLMM302_tag, NLMM303_tag, NLMM305_tag, NLMM306_tag, NLMM320_tag,
            CROSS_SECTION_AREAS_tag, FRAME_2D_CROSS_SECTIONS_tag, FRAME_3D_CROSS_SECTIONS_tag, LAMINATE_THICKNESSES_tag, GEOMETRY_PATTERNS_tag, FIBROUS_PATTERNS_tag, LAYER_PROPERTIES_tag, LAYER_PATTERNS_tag, NUMERICAL_INTEGRATION_tag, ELEMENT_NODES_tag, ELEMENT_PROPERTIES_tag, LINEAR_SPRING_MATERIALS_tag, POINT_SPRINGS_tag, LINE_SPRINGS_tag, SURFACE_SPRINGS_tag, STIFFENERS_tag, LOAD_CASE_PARAMETERS_tag, GRAVITY_LOAD_tag, POINT_LOADS_tag, INTERNAL_POINT_LOADS_tag, EDGE_LOADS_tag, INTERNAL_EDGE_LOADS_tag, FACE_LOADS_tag, INTERNAL_FACE_LOADS_tag, TEMPERATURE_VARIATION_tag, DIFFERENTIAL_TEMPERATURE_VARIATION_tag, PRESCRIBED_DISPLACEMENTS_tag, INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_tag, LOAD_CASE_COMBINATIONS_tag, COMBINATION_tag, COMBINATION_PARAMETERS_tag, LOAD_CASE_FACTORS_tag, KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag, LOAD_tag, Temp_tag, ARC_LENGTH_PARAMETERS_tag;
        public static int MAIN_PARAMETERS_n, GROUP_NAMES_n, POINT_COORDINATES_n, AUXILIARY_VECTORS_n, COORDINATE_SYSTEMS_n, POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_n, SUPPORTS_n, INTERNAL_POINT_LINKS_n, LINEAR_ISOTROPIC_MATERIALS_n, LINEAR_ORTHOTROPIC_MATERIALS_n, LINEAR_INTERFACE_POINT_PROPERTIES_n, LINEAR_INTERFACE_LINE_PROPERTIES_n, LINEAR_INTERFACE_SURFACE_PROPERTIES_n, NLMM101_n, NLMM104_n, NLMM111_n, NLMM141_n, NLMM151_n, NLMM201_n,
            NLMM202_n, NLMM210_n, NLMM220_n, NLMM298_n, NLMM299_n, NLMM123_n, NLMM121_n, NLMM124_n, NLMM125_n, NLMM142_n, NLMM145_n, NLMM211_n, NLMM102_n, NLMM122_n, NLMM401_n, NLMM451_n, NLMM301_n, NLMM302_n, NLMM303_n, NLMM305_n, NLMM306_n, NLMM320_n,
            CROSS_SECTION_AREAS_n, FRAME_2D_CROSS_SECTIONS_n, FRAME_3D_CROSS_SECTIONS_n, LAMINATE_THICKNESSES_n, GEOMETRY_PATTERNS_n, FIBROUS_PATTERNS_n, LAYER_PROPERTIES_n, LAYER_PATTERNS_n, NUMERICAL_INTEGRATION_n, ELEMENT_NODES_n, ELEMENT_PROPERTIES_n, LINEAR_SPRING_MATERIALS_n, POINT_SPRINGS_n, LINE_SPRINGS_n, SURFACE_SPRINGS_n, STIFFENERS_n, LOAD_CASE_PARAMETERS_n, GRAVITY_LOAD_n, POINT_LOADS_n, INTERNAL_POINT_LOADS_n, EDGE_LOADS_n, INTERNAL_EDGE_LOADS_n, FACE_LOADS_n, INTERNAL_FACE_LOADS_n, TEMPERATURE_VARIATION_n, DIFFERENTIAL_TEMPERATURE_VARIATION_n, PRESCRIBED_DISPLACEMENTS_n, INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_n, LOAD_CASE_COMBINATIONS_n, COMBINATION_n, COMBINATION_PARAMETERS_n, LOAD_CASE_FACTORS_n, LOAD_n, LOAD_CASE_n, KB_LOAD_CASE_COMBINATIONS_GENARATOR_n, ARC_LENGTH_PARAMETERS_n;
        public static List<IList<string>> MAIN_PARAMETERS_list, GROUP_NAMES_list, POINT_COORDINATES_list, AUXILIARY_VECTORS_list, COORDINATE_SYSTEMS_list, POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_list, SUPPORTS_list, INTERNAL_POINT_LINKS_list, LINEAR_ISOTROPIC_MATERIALS_list, LINEAR_ORTHOTROPIC_MATERIALS_list, LINEAR_INTERFACE_POINT_PROPERTIES_list, LINEAR_INTERFACE_LINE_PROPERTIES_list, LINEAR_INTERFACE_SURFACE_PROPERTIES_list, NLMM101_list, NLMM104_list, NLMM111_list, NLMM141_list, NLMM151_list, NLMM201_list,
                        NLMM202_list, NLMM210_list, NLMM220_list, NLMM298_list, NLMM299_list, NLMM123_list, NLMM121_list, NLMM124_list, NLMM125_list, NLMM142_list, NLMM145_list, NLMM211_list, NLMM102_list, NLMM122_list, NLMM401_list, NLMM451_list, NLMM301_list, NLMM302_list, NLMM303_list, NLMM305_list, NLMM306_list, NLMM320_list,
            CROSS_SECTION_AREAS_list, FRAME_2D_CROSS_SECTIONS_list, FRAME_3D_CROSS_SECTIONS_list, LAMINATE_THICKNESSES_list, GEOMETRY_PATTERNS_list, FIBROUS_PATTERNS_list, LAYER_PROPERTIES_list, LAYER_PATTERNS_list, NUMERICAL_INTEGRATION_list, ELEMENT_NODES_list, ELEMENT_PROPERTIES_list, LINEAR_SPRING_MATERIALS_list, POINT_SPRINGS_list, LINE_SPRINGS_list, SURFACE_SPRINGS_list, STIFFENERS_list, LOAD_CASE_PARAMETERS_list, GRAVITY_LOAD_list, POINT_LOADS_list, INTERNAL_POINT_LOADS_list, EDGE_LOADS_list, INTERNAL_EDGE_LOADS_list, FACE_LOADS_list, INTERNAL_FACE_LOADS_list, TEMPERATURE_VARIATION_list, DIFFERENTIAL_TEMPERATURE_VARIATION_list, PRESCRIBED_DISPLACEMENTS_list, INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_list, LOAD_CASE_COMBINATIONS_list, COMBINATION_list, COMBINATION_PARAMETERS_list, LOAD_CASE_FACTORS_list, LOAD_list, KB_LOAD_CASE_COMBINATIONS_GENARATOR_list, Temp_list, ARC_LENGTH_PARAMETERS_list
        = new List<IList<string>>();
        public static List<IList<string>> Deformed_POINT_COORDINATES_list, unconnected_mesh_POINT_COORDINATES_list, unconnected_deformed_mesh_POINT_COORDINATES_list, unconnected_ELEMENT_NODES_list;
        public static List<string> LOAD_CASE_list = new List<string>();
        public static Dictionary<string, List<int>> Layer_items;
        public static double support_size_factor;
        public static void femix_string_generator()
        {
            Femix_string = "<FEMIX_DATA_FILE_V4.0>\n";
            Femix_string += "#Generated By FEMIX PRE-POST\n";
            if (C_variables.MAIN_PARAMETERS_n > 0)
                Femix_string += "<MAIN_PARAMETERS>" + MAIN_PARAMETERS_tag + "</MAIN_PARAMETERS>\n";
            Femix_string += "<MESH>\n";
            if (C_variables.GROUP_NAMES_n > 0)
                Femix_string += "<GROUP_NAMES>" + GROUP_NAMES_tag + "</GROUP_NAMES>\n";
            if (C_variables.POINT_COORDINATES_n > 0)
                Femix_string += "<POINT_COORDINATES>" + POINT_COORDINATES_tag + "</POINT_COORDINATES>\n";
            if (C_variables.AUXILIARY_VECTORS_n > 0)
                Femix_string += "<AUXILIARY_VECTORS>" + AUXILIARY_VECTORS_tag + "</AUXILIARY_VECTORS>\n";
            if (C_variables.COORDINATE_SYSTEMS_n > 0)
                Femix_string += "<COORDINATE_SYSTEMS>" + COORDINATE_SYSTEMS_tag + "</COORDINATE_SYSTEMS>\n";
            if (C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_n > 0)
                Femix_string += "<POINTS_WITH_SPECIAL_COORDINATE_SYSTEM>" + POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_tag + "</POINTS_WITH_SPECIAL_COORDINATE_SYSTEM>\n";
            if (C_variables.SUPPORTS_n > 0)
                Femix_string += "<SUPPORTS>" + SUPPORTS_tag + "</SUPPORTS>\n";
            if (C_variables.INTERNAL_POINT_LINKS_n > 0)
                Femix_string += "<INTERNAL_POINT_LINKS>" + INTERNAL_POINT_LINKS_tag + "</INTERNAL_POINT_LINKS>\n";
            if (C_variables.LINEAR_ISOTROPIC_MATERIALS_n > 0)
                Femix_string += "<LINEAR_ISOTROPIC_MATERIALS>" + LINEAR_ISOTROPIC_MATERIALS_tag + "</LINEAR_ISOTROPIC_MATERIALS>\n";
            if (C_variables.LINEAR_ORTHOTROPIC_MATERIALS_n > 0)
                Femix_string += "<LINEAR_ORTHOTROPIC_MATERIALS>" + LINEAR_ORTHOTROPIC_MATERIALS_tag + "</LINEAR_ORTHOTROPIC_MATERIALS>\n";
            if (C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_n > 0)
                Femix_string += "<LINEAR_INTERFACE_POINT_PROPERTIES>" + LINEAR_INTERFACE_POINT_PROPERTIES_tag + "</LINEAR_INTERFACE_POINT_PROPERTIES>\n";
            if (C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_n > 0)
                Femix_string += "<LINEAR_INTERFACE_LINE_PROPERTIES>" + LINEAR_INTERFACE_LINE_PROPERTIES_tag + "</LINEAR_INTERFACE_LINE_PROPERTIES>\n";
            if (C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_n > 0)
                Femix_string += "<LINEAR_INTERFACE_SURFACE_PROPERTIES>" + LINEAR_INTERFACE_SURFACE_PROPERTIES_tag + "</LINEAR_INTERFACE_SURFACE_PROPERTIES>\n";
            if (C_variables.NLMM101_n > 0)
                Femix_string += "<NLMM101>" + NLMM101_tag + "</NLMM101>\n";
            if (C_variables.NLMM104_n > 0)
                Femix_string += "<NLMM104>" + NLMM104_tag + "</NLMM104>\n";
            if (C_variables.NLMM111_n > 0)
                Femix_string += "<NLMM111>" + NLMM111_tag + "</NLMM111>\n";
            if (C_variables.NLMM141_n > 0)
                Femix_string += "<NLMM141>" + NLMM141_tag + "</NLMM141>\n";
            if (C_variables.NLMM151_n > 0)
                Femix_string += "<NLMM151>" + NLMM151_tag + "</NLMM151>\n";
            if (C_variables.NLMM201_n > 0)
                Femix_string += "<NLMM201>" + NLMM201_tag + "</NLMM201>\n";
            if (C_variables.NLMM202_n > 0)
                Femix_string += "<NLMM202>" + NLMM202_tag + "</NLMM202>\n";
            if (C_variables.NLMM210_n > 0)
                Femix_string += "<NLMM210>" + NLMM210_tag + "</NLMM210>\n";
            if (C_variables.NLMM220_n > 0)
                Femix_string += "<NLMM220>" + NLMM220_tag + "</NLMM220>\n";
            if (C_variables.NLMM298_n > 0)
                Femix_string += "<NLMM298>" + NLMM298_tag + "</NLMM298>\n";
            if (C_variables.NLMM299_n > 0)
                Femix_string += "<NLMM299>" + NLMM299_tag + "</NLMM299>\n";
            if (C_variables.NLMM123_n > 0)
                Femix_string += "<NLMM123>" + NLMM123_tag + "</NLMM123>\n";
            if (C_variables.NLMM121_n > 0)
                Femix_string += "<NLMM121>" + NLMM121_tag + "</NLMM121>\n";
            if (C_variables.NLMM124_n > 0)
                Femix_string += "<NLMM124>" + NLMM124_tag + "</NLMM124>\n";
            if (C_variables.NLMM125_n > 0)
                Femix_string += "<NLMM125>" + NLMM125_tag + "</NLMM125>\n";
            if (C_variables.NLMM142_n > 0)
                Femix_string += "<NLMM142>" + NLMM142_tag + "</NLMM142>\n";
            if (C_variables.NLMM145_n > 0)
                Femix_string += "<NLMM145>" + NLMM145_tag + "</NLMM145>\n";
            if (C_variables.NLMM211_n > 0)
                Femix_string += "<NLMM211>" + NLMM211_tag + "</NLMM211>\n";
            if (C_variables.NLMM102_n > 0)
                Femix_string += "<NLMM102>" + NLMM102_tag + "</NLMM102>\n";
            if (C_variables.NLMM122_n > 0)
                Femix_string += "<NLMM122>" + NLMM122_tag + "</NLMM122>\n";
            if (C_variables.NLMM401_n > 0)
                Femix_string += "<NLMM401>" + NLMM401_tag + "</NLMM401>\n";
            if (C_variables.NLMM451_n > 0)
                Femix_string += "<NLMM451>" + NLMM451_tag + "</NLMM451>\n";
            if (C_variables.NLMM301_n > 0)
                Femix_string += "<NLMM301>" + NLMM301_tag + "</NLMM301>\n";
            if (C_variables.NLMM302_n > 0)
                Femix_string += "<NLMM302>" + NLMM302_tag + "</NLMM302>\n";
            if (C_variables.NLMM303_n > 0)
                Femix_string += "<NLMM303>" + NLMM303_tag + "</NLMM303>\n";
            if (C_variables.NLMM305_n > 0)
                Femix_string += "<NLMM305>" + NLMM305_tag + "</NLMM305>\n";
            if (C_variables.NLMM306_n > 0)
                Femix_string += "<NLMM306>" + NLMM306_tag + "</NLMM306>\n";
            if (C_variables.NLMM320_n > 0)
                Femix_string += "<NLMM320>" + NLMM320_tag + "</NLMM320>\n";
            if (C_variables.CROSS_SECTION_AREAS_n > 0)
                Femix_string += "<CROSS_SECTION_AREAS>" + CROSS_SECTION_AREAS_tag + "</CROSS_SECTION_AREAS>\n";
            if (C_variables.FRAME_2D_CROSS_SECTIONS_n > 0)
                Femix_string += "<FRAME_2D_CROSS_SECTIONS>" + FRAME_2D_CROSS_SECTIONS_tag + "</FRAME_2D_CROSS_SECTIONS>\n";
            if (C_variables.FRAME_3D_CROSS_SECTIONS_n > 0)
                Femix_string += "<FRAME_3D_CROSS_SECTIONS>" + FRAME_3D_CROSS_SECTIONS_tag + "</FRAME_3D_CROSS_SECTIONS>\n";
            if (C_variables.LAMINATE_THICKNESSES_n > 0)
                Femix_string += "<LAMINATE_THICKNESSES>" + LAMINATE_THICKNESSES_tag + "</LAMINATE_THICKNESSES>\n";
            if (C_variables.GEOMETRY_PATTERNS_n > 0)
                Femix_string += "<GEOMETRY_PATTERNS>" + GEOMETRY_PATTERNS_tag + "</GEOMETRY_PATTERNS>\n";
            if (C_variables.FIBROUS_PATTERNS_n > 0)
                Femix_string += "<FIBROUS_PATTERNS>" + FIBROUS_PATTERNS_tag + "</FIBROUS_PATTERNS>\n";
            if (C_variables.LAYER_PROPERTIES_n > 0)
                Femix_string += "<LAYER_PROPERTIES>" + LAYER_PROPERTIES_tag + "</LAYER_PROPERTIES>\n";
            if (C_variables.LAYER_PATTERNS_n > 0)
                Femix_string += "<LAYER_PATTERNS>" + LAYER_PATTERNS_tag + "</LAYER_PATTERNS>\n";
            if (C_variables.NUMERICAL_INTEGRATION_n > 0)
                Femix_string += "<NUMERICAL_INTEGRATION>" + NUMERICAL_INTEGRATION_tag + "</NUMERICAL_INTEGRATION>\n";
            if (C_variables.ELEMENT_NODES_n > 0)
                Femix_string += "<ELEMENT_NODES>" + ELEMENT_NODES_tag + "</ELEMENT_NODES>\n";
            if (C_variables.ELEMENT_PROPERTIES_n > 0)
                Femix_string += "<ELEMENT_PROPERTIES>" + ELEMENT_PROPERTIES_tag + "</ELEMENT_PROPERTIES>\n";
            if (C_variables.LINEAR_SPRING_MATERIALS_n > 0)
                Femix_string += "<LINEAR_SPRING_MATERIALS>" + LINEAR_SPRING_MATERIALS_tag + "</LINEAR_SPRING_MATERIALS>\n";
            if (C_variables.POINT_SPRINGS_n > 0)
                Femix_string += "<POINT_SPRINGS>" + POINT_SPRINGS_tag + "</POINT_SPRINGS>\n";
            if (C_variables.LINE_SPRINGS_n > 0)
                Femix_string += "<LINE_SPRINGS>" + LINE_SPRINGS_tag + "</LINE_SPRINGS>\n";
            if (C_variables.SURFACE_SPRINGS_n > 0)
                Femix_string += "<SURFACE_SPRINGS>" + SURFACE_SPRINGS_tag + "</SURFACE_SPRINGS>\n";
            if (C_variables.STIFFENERS_n > 0)
                Femix_string += "<STIFFENERS>" + STIFFENERS_tag + "</STIFFENERS>\n";
            Femix_string += "\n</MESH>\n";
            if (C_variables.ARC_LENGTH_PARAMETERS_n > 0)
                Femix_string += "<ARC_LENGTH_PARAMETERS>" + ARC_LENGTH_PARAMETERS_tag + "</ARC_LENGTH_PARAMETERS>\n";
            Femix_string += "<LOAD>" + LOAD_tag + "</LOAD>\n";
            if (C_variables.LOAD_CASE_COMBINATIONS_n > 0)
                Femix_string += "\n<LOAD_CASE_COMBINATIONS>\n\n" + LOAD_CASE_COMBINATIONS_tag + "</LOAD_CASE_COMBINATIONS>\n\n";
            Femix_string += "</FEMIX_DATA_FILE_V4.0>\n";

            if (C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_n > 0)
                Femix_string += "\n<KB_LOAD_CASE_COMBINATIONS_GENARATOR>" + KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag + "</KB_LOAD_CASE_COMBINATIONS_GENARATOR>\n";

        }

        public static void Load_tag_generator()
        {
            
          
            //Edit MAIN_PARAMETERS
            C_variables.MAIN_PARAMETERS_tag = "";
            foreach (IList<string> row in C_variables.MAIN_PARAMETERS_list)
            {
                if (row[1] == "NUMBER_OF_LOAD_CASES")
                      row[2]= C_variables.LOAD_CASE_PARAMETERS_n.ToString();
                C_variables.MAIN_PARAMETERS_tag += $"  {row[1]} = {row[2]} ;\n";

            }


            int i = 0; int _count = 0;
            //string LOAD_CASE_NUMBER, LOAD_CASE_TITLE, LOAD_CASE_GROUP, INTEGRATION_TYPE, INTEGRATION_NAME, String_identifier;
            String _start, _end, coment1, coment2;
            List<IList<string>> loadoutputlist = new List<IList<string>>();
            LOAD_tag = "";
            //foreach (string item in C_variables.LOAD_CASE_list)
            foreach (IList<string> loadCaseParameters in C_variables.LOAD_CASE_PARAMETERS_list)
            {
                ++i;
                //LOAD_CASE_NUMBER = ""; LOAD_CASE_TITLE = ""; LOAD_CASE_GROUP = ""; INTEGRATION_TYPE = ""; INTEGRATION_NAME = "";

                LOAD_tag += "\n<LOAD_CASE>\n";

                _start = "<LOAD_CASE_PARAMETERS>"; _end = "</LOAD_CASE_PARAMETERS>";

                if (C_variables.LOAD_CASE_PARAMETERS_list.Count>0)
                {
                    // check if the names don't have space
                    LOAD_tag += _start+"\n";
                    LOAD_tag += $"   LOAD_CASE_NUMBER = {LOAD_CASE_PARAMETERS_list[i-1][1]} ;\n";
                    LOAD_tag += $"   LOAD_CASE_TITLE = {LOAD_CASE_PARAMETERS_list[i-1][2]} ;\n";
                    LOAD_tag += $"   LOAD_CASE_GROUP = {LOAD_CASE_PARAMETERS_list[i-1][3]} ;\n\n";
                    LOAD_tag += $"   INTEGRATION_TYPE = {LOAD_CASE_PARAMETERS_list[i-1][4]} ; # Keywords: _GLEG, _GLOB, _NCOTES or _DEFAULT\n";
                    LOAD_tag += $"   INTEGRATION_NAME = {LOAD_CASE_PARAMETERS_list[i-1][5]} ; # Defined in the -NUMERICAL_INTEGRATION- block or _DEFAULT\n";
                    LOAD_tag += _end+"\n";

                }


                _start = "<GRAVITY_LOAD>"; _end = "</GRAVITY_LOAD>";
                coment1 = "\n";
                coment2 = "    ## Gravity acceleration (global coordinate system) \n## Content of each column: \n#  A -> Acceleration - XG1 \n#  B -> Acceleration - XG2 \n#  C -> Acceleration - XG3 \n#       A       B       C";
                loadoutputlist = C_variables.GRAVITY_LOAD_list;

                if (loadoutputlist.Count > 0)
                {
                    _count = 0;
                    foreach (var row in loadoutputlist)
                        if (Val(row[row.Count - 1]) == i)
                            _count++;

                    if (_count > 0)
                    {
                        LOAD_tag += _start + "\n";
                        LOAD_tag += coment2+"\n";
                        foreach (var row in loadoutputlist)
                        {
                            if (Val(row[row.Count - 1]) == i)
                            {
                                for (int i2 = 2; i2 < row.Count - 1; i2++)
                                {
                                    LOAD_tag += "        " + row[i2];
                                }
                                LOAD_tag += " ;\n";
                            }
                        }
                        LOAD_tag += _end + "\n";
                    }
                }



                _start = "<POINT_LOADS>"; _end = "</POINT_LOADS>";
                coment1 = "## Point loads (global coordinate system) \n# N. of point loads \n";
                coment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded point (or loaded point range) \n#  C -> Force - XG1 \n#  D -> Force - XG2 \n#  E -> Force - XG3 \n#  F -> Moment - XG1 \n#  G -> Moment - XG2 \n#  H -> Moment - XG3 \n#		A		B		C		D		E		F		G		H\n";
                LOAD_tag += load_tag_segment_genarator(C_variables.POINT_LOADS_list, i, _start, _end, coment1, coment2);

                _start = "<INTERNAL_POINT_LOADS>"; _end = "</INTERNAL_POINT_LOADS>";
                coment1 = "## Load applied to a point inside an element (global coordinate system) \n# N. of internal point loads \n";
                coment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element \n#  C -> Auxiliary point with the load (or auxiliary point range) \n#  D -> Force - XG1 \n#  E -> Force - XG2 \n#  F -> Force - XG3 \n#  G -> Moment - XG1 \n#  H -> Moment - XG2 \n#  I -> Moment - XG3 \n#		A		B		C		D		E		F		G		H		I\n";
                LOAD_tag += load_tag_segment_genarator(C_variables.INTERNAL_POINT_LOADS_list, i, _start, _end, coment1, coment2);

               

                _start = "<EDGE_LOADS>"; _end = "</EDGE_LOADS>";
                coment1 = "## Load on the edge of an element \n# N. of edge loads \n";
                coment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Loaded face \n#  D -> Loaded edge \n#  E -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Frame: _L1, _L2 or _L3 \n#       - Solid/Shell: _T, _NN or _NT \n#       - Plane stress/Plane strain/Axisymmetry: _T or _NT \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  F -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  G -> N. of nodes of the edge of the element \n#  H -> Load values \n# \n#  Note: when G is equal to 1 the load is constant \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the bar \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _T  - tangential to the edge \n#     _NN - normal to the edge and normal to the element face \n#     _NT - normal to the edge and tangential to the element face \n# \n#		A		B		C	D	E		F		G		H\n";
                LOAD_tag += load_tag_segment_genarator(C_variables.EDGE_LOADS_list, i, _start, _end, coment1, coment2);

              

                _start = "<INTERNAL_EDGE_LOADS>"; _end = "</INTERNAL_EDGE_LOADS>";
                coment1 = "## Line load inside an element \n# N. of internal edge loads \n";
                coment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Loaded element \n#  C -> Auxiliary line element with the load \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Frame: _L1, _L2 or _L3 \n#       - Solid surface/Shell: _T, _NN or _NT \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the auxiliary line element \n#  G -> Load values \n# \n#  Notes: \n#  - when F is equal to 1 the load is constant; \n#  - the n. of nodes of the auxiliary line element must match the n. of nodes of the edge of the loaded element. \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the bar \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _T  - tangential to the edge \n#     _NN - normal to the edge and normal to the element face \n#     _NT - normal to the edge and tangential to the element face \n# \n#		A		B		C	D		E		F		G\n";
                LOAD_tag += load_tag_segment_genarator(C_variables.INTERNAL_EDGE_LOADS_list, i, _start, _end, coment1, coment2);


                _start = "<FACE_LOADS>"; _end = "</FACE_LOADS>";
                coment1 = "## Load on the face of an element \n# N. of face loads \n";
                coment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Loaded face \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Solid/Shell: _L1, _L2 or _N \n#       - Plane stress/Plane strain/Axisymmetry: _L1 or _L2 \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the face of the element \n#  G -> Load values \n# \n#  Note: when F is equal to 1 the load is constant \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the face and following the l1 local axis \n#     _L2 - tangential to the face and following the l2 local axis \n#     _N  - normal to the face \n# \n#		A		B		C	D		E		F		G\n";
                LOAD_tag += load_tag_segment_genarator(C_variables.FACE_LOADS_list, i, _start, _end, coment1, coment2);

             

                _start = "<INTERNAL_FACE_LOADS>"; _end = "</INTERNAL_FACE_LOADS>";
                coment1 = "## Surface load inside an element \n# N. of internal face loads \n";
                coment2 = "\n## Content of each column: \n#  A -> Counter \n#  B -> Loaded element \n#  C -> Auxiliary surface element with the load \n#  D -> Direction vector: \n#       - Global: _XG1, _XG2 or _XG3 \n#       - Solid surface/Shell: _L1, _L2 or _N \n#       - Specified: vector from the [AUXILIARY_VECTORS] block \n#  E -> Type of generalized force (_FORC - Force; _MOM - Moment) \n#  F -> N. of nodes of the auxiliary surface element \n#  G -> Load values \n# \n#  Notes: \n#  - when F is equal to 1 the load is constant; \n#  - the n. of nodes of the auxiliary surface element must match the n. of nodes of the face of the loaded element. \n# \n#  Direction vector keywords: \n#     _L1 - tangential to the face and following the l1 local axis \n#     _L2 - tangential to the face and following the l2 local axis \n#     _N  - normal to the face \n# \n#		A		B		C	D			E			F			G\n";
                LOAD_tag += load_tag_segment_genarator(C_variables.INTERNAL_FACE_LOADS_list, i, _start, _end, coment1, coment2);

            


                _start = "<TEMPERATURE_VARIATION>"; _end = "</TEMPERATURE_VARIATION>";
                coment1 = "## Elements with temperature variation \n# N. of elements with temperature variation \n";
                coment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> N. of nodes of the element \n#  D -> Temperature variation values \n# \n#  Note: when C is equal to 1 the temperature variation is the same for all the element nodes \n# \n#		A		B		C		D\n";
                LOAD_tag += load_tag_segment_genarator(C_variables.TEMPERATURE_VARIATION_list, i, _start, _end, coment1, coment2);

              


                _start = "<DIFFERENTIAL_TEMPERATURE_VARIATION>"; _end = "</DIFFERENTIAL_TEMPERATURE_VARIATION>";
                coment1 = "## Elements with differential temperature variation \n# N. of elements with differential temperature variation \n";
                coment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Loaded element (or loaded element range) \n#  C -> Direction vector: \n#       - Frame: _L2 or _L3 \n#       - Shell: _N \n#  D -> N. of nodes of the element \n#  E -> Differential temperature variation values \n# \n#  Note: when D is equal to 1 the diff. temp. var. is the same for all the element nodes \n# \n#  Direction vector keywords: \n#     _L2 - normal to the bar and following axis l2 \n#     _L3 - normal to the bar and following axis l3 \n#     _N  - normal to the shell mean surface \n# \n#		A		B	C		D		E\n";
                LOAD_tag += load_tag_segment_genarator(C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_list, i, _start, _end, coment1, coment2);

            

                _start = "<PRESCRIBED_DISPLACEMENTS>"; _end = "</PRESCRIBED_DISPLACEMENTS>";
                coment1 = "## Points with prescribed displacement \n# N. of prescribed displacements \n";
                coment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Point number (or point number range) \n#  C -> Prescribed degree of freedom \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#  D -> Value of the prescribed displacement \n#		A		B	C		D\n";
                LOAD_tag += load_tag_segment_genarator(C_variables.PRESCRIBED_DISPLACEMENTS_list, i, _start, _end, coment1, coment2);

             

                _start = "<INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS>"; _end = "</INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS>";
                coment1 = "## Points with internal prescribed relative displacement \n# N. of internal prescribed relative displacements \n";
                coment2 = "\n## Content of each column: \n#  A -> Counter (or counter range) \n#  B -> Point number - M (Master) (or point number range) \n#  C -> Point number - S (Slave) (or point number range) \n#  D -> Prescribed degree of freedom \n#       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 \n#  E -> Value of the prescribed displacement \n#		A		B		C	D		E\n";
                LOAD_tag += load_tag_segment_genarator(C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_list, i, _start, _end, coment1, coment2);

            

                LOAD_tag += "\n</LOAD_CASE>\n";
 
            }




        }
        private static string load_tag_segment_genarator(List<IList<string>> loadoutputlist, int i_lodcase,string _start,string _end,string comment1, string comment2)
        {
            string _segment = "";
            
            string srting_range;
            int range_start, range_end;

            int _count = 0;
            if (loadoutputlist.Count > 0)
            {
                _count = 0;
                foreach (var row in loadoutputlist)
                    if (Val(row[row.Count - 1]) == i_lodcase)
                        _count++;

                if (_count > 0)
                {
                    _segment = _start + "\n";

                    /////////////////////
                    int counter = 0;
                    foreach (var row in loadoutputlist)
                    {
                        srting_range = row[2];
                        string[] elementrange = srting_range.Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                        counter++;
                        if (elementrange.Count() > 1 && srting_range.Contains("[") && srting_range.Contains("-") && srting_range.Contains("]"))
                        {
                            range_start = Int32.Parse(elementrange[0]);
                            range_end = Int32.Parse(elementrange[1]);
                            counter = counter + (range_end - range_start);
                        }
                    }
                    _segment += comment1 + $"COUNT = {counter} ;\n" + comment2;
                    counter = 0;
                    ////////////////////
                    //_segment += coment1 + $"COUNT = {_count} ;\n" + coment2;

                    int i1 = 0;
                    foreach (var row in loadoutputlist)
                    {
                        if (Val(row[row.Count - 1]) == i_lodcase)
                        {
                            i1++;
                            //_segment += "              " + i1.ToString();
                            /////////////////////
                            srting_range = row[2];
                            string[] elementrange = srting_range.Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                            counter++;
                            if (elementrange.Count() > 1 && srting_range.Contains("[") && srting_range.Contains("-") && srting_range.Contains("]"))
                            {
                                range_start = Int32.Parse(elementrange[0]);
                                range_end = Int32.Parse(elementrange[1]);
                                _segment += "        " + $"[{counter}-{counter + (range_end - range_start)}]";
                                counter = counter + (range_end - range_start);
                            }
                            else
                            {
                                _segment += "        " + counter;
                            }
                            //////////////////////////////


                            for (int i2 = 2; i2 < row.Count - 1; i2++)
                            {
                                _segment += "        " + row[i2];
                            }
                            _segment += " ;\n";
                        }
                    }
                    _segment += _end + "\n";
                }
            }

            return _segment;

        }
        public static int Val(string _s)
        {
            string _new_string = string.Empty;
            foreach (char _c in _s)
            {
                if (Char.IsDigit(_c)) { _new_string += _c; } else { break; }
            }
            return String.IsNullOrEmpty(_new_string) ? 0 : int.Parse(_new_string);
        }

    }

   
}
