using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Femix
{
    static class Class_femix_reader
    {
        public static void extract_femix(string Femix_file_Path, string Femix_string)
        {

            C_variables.MAIN_PARAMETERS_n = 0; C_variables.GROUP_NAMES_n = 0; C_variables.POINT_COORDINATES_n = 0; C_variables.AUXILIARY_VECTORS_n = 0; C_variables.COORDINATE_SYSTEMS_n = 0; C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_n = 0; C_variables.SUPPORTS_n = 0; C_variables.INTERNAL_POINT_LINKS_n = 0; C_variables.LINEAR_ISOTROPIC_MATERIALS_n = 0; C_variables.LINEAR_ORTHOTROPIC_MATERIALS_n = 0; C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_n = 0; C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_n = 0; C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_n = 0; C_variables.NLMM101_n = 0; C_variables.NLMM104_n = 0; C_variables.NLMM111_n = 0; C_variables.NLMM141_n = 0; C_variables.NLMM151_n = 0; C_variables.NLMM201_n = 0; C_variables.NLMM202_n = 0; C_variables.NLMM210_n = 0; C_variables.NLMM220_n = 0; C_variables.NLMM298_n = 0; C_variables.NLMM299_n = 0; C_variables.NLMM123_n = 0; C_variables.NLMM121_n = 0; C_variables.NLMM124_n = 0; C_variables.NLMM125_n = 0; C_variables.NLMM142_n = 0; C_variables.NLMM145_n = 0; C_variables.NLMM211_n = 0; C_variables.NLMM102_n = 0; C_variables.NLMM122_n = 0; C_variables.NLMM401_n = 0; C_variables.NLMM451_n = 0; C_variables.NLMM301_n = 0; C_variables.NLMM302_n = 0; C_variables.NLMM303_n = 0; C_variables.NLMM305_n = 0; C_variables.NLMM306_n = 0; C_variables.NLMM320_n = 0;            C_variables.CROSS_SECTION_AREAS_n = 0; C_variables.FRAME_2D_CROSS_SECTIONS_n = 0; C_variables.FRAME_3D_CROSS_SECTIONS_n = 0; C_variables.LAMINATE_THICKNESSES_n = 0; C_variables.GEOMETRY_PATTERNS_n = 0; C_variables.FIBROUS_PATTERNS_n = 0; C_variables.LAYER_PROPERTIES_n = 0; C_variables.LAYER_PATTERNS_n = 0; C_variables.NUMERICAL_INTEGRATION_n = 0; C_variables.ELEMENT_NODES_n = 0; C_variables.ELEMENT_PROPERTIES_n = 0; C_variables.LINEAR_SPRING_MATERIALS_n = 0; C_variables.POINT_SPRINGS_n = 0; C_variables.LINE_SPRINGS_n = 0; C_variables.SURFACE_SPRINGS_n = 0; C_variables.STIFFENERS_n = 0; C_variables.LOAD_CASE_PARAMETERS_n = 0; C_variables.GRAVITY_LOAD_n = 0; C_variables.POINT_LOADS_n = 0; C_variables.INTERNAL_POINT_LOADS_n = 0; C_variables.EDGE_LOADS_n = 0; C_variables.INTERNAL_EDGE_LOADS_n = 0; C_variables.FACE_LOADS_n = 0; C_variables.INTERNAL_FACE_LOADS_n = 0; C_variables.TEMPERATURE_VARIATION_n = 0; C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_n = 0; C_variables.PRESCRIBED_DISPLACEMENTS_n = 0; C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_n = 0; C_variables.LOAD_CASE_COMBINATIONS_n=0; C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_n = 0;
            C_variables.MAIN_PARAMETERS_list = new List<IList<string>>(); C_variables.GROUP_NAMES_list = new List<IList<string>>(); C_variables.POINT_COORDINATES_list = new List<IList<string>>(); C_variables.AUXILIARY_VECTORS_list = new List<IList<string>>(); C_variables.COORDINATE_SYSTEMS_list = new List<IList<string>>(); C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_list = new List<IList<string>>(); C_variables.SUPPORTS_list = new List<IList<string>>(); C_variables.INTERNAL_POINT_LINKS_list = new List<IList<string>>(); C_variables.LINEAR_ISOTROPIC_MATERIALS_list = new List<IList<string>>(); C_variables.LINEAR_ORTHOTROPIC_MATERIALS_list = new List<IList<string>>(); C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_list = new List<IList<string>>(); C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_list = new List<IList<string>>(); C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_list = new List<IList<string>>(); C_variables.NLMM101_list = new List<IList<string>>(); C_variables.NLMM104_list = new List<IList<string>>(); C_variables.NLMM111_list = new List<IList<string>>(); C_variables.NLMM141_list = new List<IList<string>>(); C_variables.NLMM151_list = new List<IList<string>>(); C_variables.NLMM201_list = new List<IList<string>>();C_variables.NLMM202_list = new List<IList<string>>(); C_variables.NLMM210_list = new List<IList<string>>(); C_variables.NLMM220_list = new List<IList<string>>(); C_variables.NLMM298_list = new List<IList<string>>(); C_variables.NLMM299_list = new List<IList<string>>(); C_variables.NLMM123_list = new List<IList<string>>(); C_variables.NLMM121_list = new List<IList<string>>(); C_variables.NLMM124_list = new List<IList<string>>(); C_variables.NLMM125_list = new List<IList<string>>(); C_variables.NLMM142_list = new List<IList<string>>(); C_variables.NLMM145_list = new List<IList<string>>(); C_variables.NLMM211_list = new List<IList<string>>(); C_variables.NLMM102_list = new List<IList<string>>(); C_variables.NLMM122_list = new List<IList<string>>(); C_variables.NLMM401_list = new List<IList<string>>(); C_variables.NLMM451_list = new List<IList<string>>(); C_variables.NLMM301_list = new List<IList<string>>(); C_variables.NLMM302_list = new List<IList<string>>(); C_variables.NLMM303_list = new List<IList<string>>(); C_variables.NLMM305_list = new List<IList<string>>(); C_variables.NLMM306_list = new List<IList<string>>(); C_variables.NLMM320_list = new List<IList<string>>();
            C_variables.CROSS_SECTION_AREAS_list = new List<IList<string>>(); C_variables.FRAME_2D_CROSS_SECTIONS_list = new List<IList<string>>(); C_variables.FRAME_3D_CROSS_SECTIONS_list = new List<IList<string>>(); C_variables.LAMINATE_THICKNESSES_list = new List<IList<string>>(); C_variables.GEOMETRY_PATTERNS_list = new List<IList<string>>(); C_variables.FIBROUS_PATTERNS_list = new List<IList<string>>(); C_variables.LAYER_PROPERTIES_list = new List<IList<string>>(); C_variables.LAYER_PATTERNS_list = new List<IList<string>>(); C_variables.NUMERICAL_INTEGRATION_list = new List<IList<string>>(); C_variables.ELEMENT_NODES_list = new List<IList<string>>(); C_variables.ELEMENT_PROPERTIES_list = new List<IList<string>>(); C_variables.LINEAR_SPRING_MATERIALS_list = new List<IList<string>>(); C_variables.POINT_SPRINGS_list = new List<IList<string>>(); C_variables.LINE_SPRINGS_list = new List<IList<string>>(); C_variables.SURFACE_SPRINGS_list = new List<IList<string>>(); C_variables.STIFFENERS_list = new List<IList<string>>(); C_variables.LOAD_CASE_PARAMETERS_list = new List<IList<string>>(); C_variables.GRAVITY_LOAD_list = new List<IList<string>>(); C_variables.POINT_LOADS_list = new List<IList<string>>(); C_variables.INTERNAL_POINT_LOADS_list = new List<IList<string>>(); C_variables.EDGE_LOADS_list = new List<IList<string>>(); C_variables.INTERNAL_EDGE_LOADS_list = new List<IList<string>>(); C_variables.FACE_LOADS_list = new List<IList<string>>(); C_variables.INTERNAL_FACE_LOADS_list = new List<IList<string>>(); C_variables.TEMPERATURE_VARIATION_list = new List<IList<string>>(); C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_list = new List<IList<string>>(); C_variables.PRESCRIBED_DISPLACEMENTS_list = new List<IList<string>>(); C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_list = new List<IList<string>>(); C_variables.LOAD_CASE_COMBINATIONS_list = new List<IList<string>>(); C_variables.COMBINATION_list = new List<IList<string>>(); C_variables.COMBINATION_PARAMETERS_list = new List<IList<string>>(); C_variables.LOAD_CASE_FACTORS_list = new List<IList<string>>(); C_variables.LOAD_list = new List<IList<string>>(); C_variables.Temp_list = new List<IList<string>>(); C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list = new List<IList<string>>();
            C_variables.Deformed_POINT_COORDINATES_list = new List<IList<string>>(); C_variables.unconnected_mesh_POINT_COORDINATES_list = new List<IList<string>>(); C_variables.unconnected_deformed_mesh_POINT_COORDINATES_list = new List<IList<string>>(); C_variables.unconnected_ELEMENT_NODES_list = new List<IList<string>>();
            
            C_variables.LOAD_CASE_list = new List<string>();

            string _start, _end;
            //_strat = "$MeshFormat"; _end = "$EndMeshFormat";
            //_segment = ExtractString(gmsh_file, _strat, _end);
            //MeshFormat = ExtractLine(_segment);


            _start = "<MAIN_PARAMETERS>"; _end = "</MAIN_PARAMETERS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.MAIN_PARAMETERS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.MAIN_PARAMETERS_list = ExtractLine(C_variables.MAIN_PARAMETERS_tag, false);
                C_variables.MAIN_PARAMETERS_n = C_variables.MAIN_PARAMETERS_list.Count;
            }

            _start = "<ARC_LENGTH_PARAMETERS>"; _end = "</ARC_LENGTH_PARAMETERS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.ARC_LENGTH_PARAMETERS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.ARC_LENGTH_PARAMETERS_list = ExtractLine(C_variables.ARC_LENGTH_PARAMETERS_tag, false);
                C_variables.ARC_LENGTH_PARAMETERS_n = C_variables.ARC_LENGTH_PARAMETERS_list.Count;
            }

            _start = "<GROUP_NAMES>"; _end = "</GROUP_NAMES>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.GROUP_NAMES_tag = ExtractString(Femix_string, _start, _end);
                C_variables.GROUP_NAMES_list = ExtractLine(C_variables.GROUP_NAMES_tag, false);
                C_variables.GROUP_NAMES_n = C_variables.GROUP_NAMES_list.Count;
            }


            _start = "<POINT_COORDINATES>"; _end = "</POINT_COORDINATES>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {

                C_variables.POINT_COORDINATES_tag = ExtractString(Femix_string, _start, _end);
                C_variables.POINT_COORDINATES_list = ExtractLine(C_variables.POINT_COORDINATES_tag, false);
                C_variables.POINT_COORDINATES_n = C_variables.POINT_COORDINATES_list.Count;
            }

            double point_min, point_max;
            C_variables.support_size_factor = 1;
            if (C_variables.POINT_COORDINATES_n > 0) {
                point_min = (double.Parse(C_variables.POINT_COORDINATES_list[0][2]));
                point_max = (double.Parse(C_variables.POINT_COORDINATES_list[0][2]));
                foreach (var item in C_variables.POINT_COORDINATES_list)
                {
                   // Console.WriteLine(item[0] + "/" + item[1] + "/"+ item[2] + "/"+item[3] + "/"+item[4] + "/");

                    if (double.Parse(item[2]) < point_min)
                        point_min = double.Parse(item[2]);
                    if (double.Parse(item[3]) < point_min)
                        point_min = double.Parse(item[3]);
                    if (double.Parse(item[4]) < point_min)
                        point_min = double.Parse(item[4]);

                    if (double.Parse(item[2]) > point_max)
                        point_max = double.Parse(item[2]);
                    if (double.Parse(item[3]) > point_max)
                        point_max = double.Parse(item[3]);
                    if (double.Parse(item[4]) > point_max)
                        point_max = double.Parse(item[4]);
                }
                C_variables.support_size_factor = point_max - point_min;
            }

            _start = "<AUXILIARY_VECTORS>"; _end = "</AUXILIARY_VECTORS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.AUXILIARY_VECTORS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.AUXILIARY_VECTORS_list = ExtractLine(C_variables.AUXILIARY_VECTORS_tag, false);
                C_variables.AUXILIARY_VECTORS_n = C_variables.AUXILIARY_VECTORS_list.Count;
            }


            _start = "<COORDINATE_SYSTEMS>"; _end = "</COORDINATE_SYSTEMS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.COORDINATE_SYSTEMS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.COORDINATE_SYSTEMS_list = ExtractLine(C_variables.COORDINATE_SYSTEMS_tag, false);
                C_variables.COORDINATE_SYSTEMS_n = C_variables.COORDINATE_SYSTEMS_list.Count;
            }


            _start = "<POINTS_WITH_SPECIAL_COORDINATE_SYSTEM>"; _end = "</POINTS_WITH_SPECIAL_COORDINATE_SYSTEM>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_tag = ExtractString(Femix_string, _start, _end);
                C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_list = ExtractLine(C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_tag, false);
                C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_n = C_variables.POINTS_WITH_SPECIAL_COORDINATE_SYSTEM_list.Count;
            }


            _start = "<SUPPORTS>"; _end = "</SUPPORTS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.SUPPORTS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.SUPPORTS_list = ExtractLine(C_variables.SUPPORTS_tag, false);
                C_variables.SUPPORTS_n = C_variables.SUPPORTS_list.Count;
            }




            _start = "<INTERNAL_POINT_LINKS>"; _end = "</INTERNAL_POINT_LINKS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.INTERNAL_POINT_LINKS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.INTERNAL_POINT_LINKS_list = ExtractLine(C_variables.INTERNAL_POINT_LINKS_tag, false);
                C_variables.INTERNAL_POINT_LINKS_n = C_variables.INTERNAL_POINT_LINKS_list.Count;
            }



            _start = "<LINEAR_ISOTROPIC_MATERIALS>"; _end = "</LINEAR_ISOTROPIC_MATERIALS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LINEAR_ISOTROPIC_MATERIALS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LINEAR_ISOTROPIC_MATERIALS_list = ExtractLine(C_variables.LINEAR_ISOTROPIC_MATERIALS_tag, false);
                C_variables.LINEAR_ISOTROPIC_MATERIALS_n = C_variables.LINEAR_ISOTROPIC_MATERIALS_list.Count;
            }


            _start = "<LINEAR_ORTHOTROPIC_MATERIALS>"; _end = "</LINEAR_ORTHOTROPIC_MATERIALS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LINEAR_ORTHOTROPIC_MATERIALS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LINEAR_ORTHOTROPIC_MATERIALS_list = ExtractLine(C_variables.LINEAR_ORTHOTROPIC_MATERIALS_tag, false);
                C_variables.LINEAR_ORTHOTROPIC_MATERIALS_n = C_variables.LINEAR_ORTHOTROPIC_MATERIALS_list.Count;
            }


            _start = "<LINEAR_INTERFACE_POINT_PROPERTIES>"; _end = "</LINEAR_INTERFACE_POINT_PROPERTIES>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_list = ExtractLine(C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_tag, false);
                C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_n = C_variables.LINEAR_INTERFACE_POINT_PROPERTIES_list.Count;
            }



            _start = "<LINEAR_INTERFACE_LINE_PROPERTIES>"; _end = "</LINEAR_INTERFACE_LINE_PROPERTIES>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_list = ExtractLine(C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_tag, false);
                C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_n = C_variables.LINEAR_INTERFACE_LINE_PROPERTIES_list.Count;
            }


            _start = "<LINEAR_INTERFACE_SURFACE_PROPERTIES>"; _end = "</LINEAR_INTERFACE_SURFACE_PROPERTIES>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_list = ExtractLine(C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_tag, false);
                C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_n = C_variables.LINEAR_INTERFACE_SURFACE_PROPERTIES_list.Count;
            }


            _start = "<NLMM101>"; _end = "</NLMM101>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM101_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM101_list = ExtractLine(C_variables.NLMM101_tag, true);
                C_variables.NLMM101_n = C_variables.NLMM101_list.Count;
            }


            _start = "<NLMM104>"; _end = "</NLMM104>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM104_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM104_list = ExtractLine(C_variables.NLMM104_tag, true);
                C_variables.NLMM104_n = C_variables.NLMM104_list.Count;
            }


            _start = "<NLMM111>"; _end = "</NLMM111>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM111_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM111_list = ExtractLine(C_variables.NLMM111_tag, true);
                C_variables.NLMM111_n = C_variables.NLMM111_list.Count;
            }



            _start = "<NLMM141>"; _end = "</NLMM141>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM141_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM141_list = ExtractLine(C_variables.NLMM141_tag, true);
                C_variables.NLMM141_n = C_variables.NLMM141_list.Count;
            }


            _start = "<NLMM151>"; _end = "</NLMM151>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM151_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM151_list = ExtractLine(C_variables.NLMM151_tag, true);
                C_variables.NLMM151_n = C_variables.NLMM151_list.Count;
            }


            _start = "<NLMM201>"; _end = "</NLMM201>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM201_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM201_list = ExtractLine(C_variables.NLMM201_tag, true);
                C_variables.NLMM201_n = C_variables.NLMM201_list.Count;
            }

            _start = "<NLMM202>"; _end = "</NLMM202>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM202_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM202_list = ExtractLine(C_variables.NLMM202_tag, true);
                C_variables.NLMM202_n = C_variables.NLMM202_list.Count;
            }

            _start = "<NLMM210>"; _end = "</NLMM210>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM210_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM210_list = ExtractLine(C_variables.NLMM210_tag, true);
                C_variables.NLMM210_n = C_variables.NLMM210_list.Count;
            }

            _start = "<NLMM220>"; _end = "</NLMM220>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM220_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM220_list = ExtractLine(C_variables.NLMM220_tag, true);
                C_variables.NLMM220_n = C_variables.NLMM220_list.Count;
            }

            _start = "<NLMM298>"; _end = "</NLMM298>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM298_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM298_list = ExtractLine(C_variables.NLMM298_tag, true);
                C_variables.NLMM298_n = C_variables.NLMM298_list.Count;
            }

            _start = "<NLMM299>"; _end = "</NLMM299>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM299_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM299_list = ExtractLine(C_variables.NLMM299_tag, true);
                C_variables.NLMM299_n = C_variables.NLMM299_list.Count;
            }

            _start = "<NLMM123>"; _end = "</NLMM123>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM123_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM123_list = ExtractLine(C_variables.NLMM123_tag, true);
                C_variables.NLMM123_n = C_variables.NLMM123_list.Count;
            }

            _start = "<NLMM121>"; _end = "</NLMM121>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM121_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM121_list = ExtractLine(C_variables.NLMM121_tag, true);
                C_variables.NLMM121_n = C_variables.NLMM121_list.Count;
            }

            _start = "<NLMM124>"; _end = "</NLMM124>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM124_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM124_list = ExtractLine(C_variables.NLMM124_tag, true);
                C_variables.NLMM124_n = C_variables.NLMM124_list.Count;
            }

            _start = "<NLMM125>"; _end = "</NLMM125>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM125_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM125_list = ExtractLine(C_variables.NLMM125_tag, true);
                C_variables.NLMM125_n = C_variables.NLMM125_list.Count;
            }

            _start = "<NLMM142>"; _end = "</NLMM142>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM142_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM142_list = ExtractLine(C_variables.NLMM142_tag, true);
                C_variables.NLMM142_n = C_variables.NLMM142_list.Count;
            }

            _start = "<NLMM145>"; _end = "</NLMM145>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM145_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM145_list = ExtractLine(C_variables.NLMM145_tag, true);
                C_variables.NLMM145_n = C_variables.NLMM145_list.Count;
            }

            _start = "<NLMM211>"; _end = "</NLMM211>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM211_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM211_list = ExtractLine(C_variables.NLMM211_tag, true);
                C_variables.NLMM211_n = C_variables.NLMM211_list.Count;
            }

            _start = "<NLMM102>"; _end = "</NLMM102>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM102_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM102_list = ExtractLine(C_variables.NLMM102_tag, true);
                C_variables.NLMM102_n = C_variables.NLMM102_list.Count;
            }

            _start = "<NLMM122>"; _end = "</NLMM122>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM122_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM122_list = ExtractLine(C_variables.NLMM122_tag, true);
                C_variables.NLMM122_n = C_variables.NLMM122_list.Count;
            }

            _start = "<NLMM401>"; _end = "</NLMM401>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM401_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM401_list = ExtractLine(C_variables.NLMM401_tag, true);
                C_variables.NLMM401_n = C_variables.NLMM401_list.Count;
            }

            _start = "<NLMM451>"; _end = "</NLMM451>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM451_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM451_list = ExtractLine(C_variables.NLMM451_tag, true);
                C_variables.NLMM451_n = C_variables.NLMM451_list.Count;
            }

            _start = "<NLMM301>"; _end = "</NLMM301>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM301_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM301_list = ExtractLine(C_variables.NLMM301_tag, true);
                C_variables.NLMM301_n = C_variables.NLMM301_list.Count;
            }

            _start = "<NLMM302>"; _end = "</NLMM302>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM302_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM302_list = ExtractLine(C_variables.NLMM302_tag, true);
                C_variables.NLMM302_n = C_variables.NLMM302_list.Count;
            }

            _start = "<NLMM303>"; _end = "</NLMM303>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM303_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM303_list = ExtractLine(C_variables.NLMM303_tag, true);
                C_variables.NLMM303_n = C_variables.NLMM303_list.Count;
            }

            _start = "<NLMM305>"; _end = "</NLMM305>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM305_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM305_list = ExtractLine(C_variables.NLMM305_tag, true);
                C_variables.NLMM305_n = C_variables.NLMM305_list.Count;
            }

            _start = "<NLMM306>"; _end = "</NLMM306>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM306_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM306_list = ExtractLine(C_variables.NLMM306_tag, true);
                C_variables.NLMM306_n = C_variables.NLMM306_list.Count;
            }

            _start = "<NLMM320>"; _end = "</NLMM320>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NLMM320_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NLMM320_list = ExtractLine(C_variables.NLMM320_tag, true);
                C_variables.NLMM320_n = C_variables.NLMM320_list.Count;
            }




            _start = "<CROSS_SECTION_AREAS>"; _end = "</CROSS_SECTION_AREAS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.CROSS_SECTION_AREAS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.CROSS_SECTION_AREAS_list = ExtractLine(C_variables.CROSS_SECTION_AREAS_tag, false);
                C_variables.CROSS_SECTION_AREAS_n = C_variables.CROSS_SECTION_AREAS_list.Count;
            }


            _start = "<FRAME_2D_CROSS_SECTIONS>"; _end = "</FRAME_2D_CROSS_SECTIONS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.FRAME_2D_CROSS_SECTIONS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.FRAME_2D_CROSS_SECTIONS_list = ExtractLine(C_variables.FRAME_2D_CROSS_SECTIONS_tag, false);
                C_variables.FRAME_2D_CROSS_SECTIONS_n = C_variables.FRAME_2D_CROSS_SECTIONS_list.Count;
            }


            _start = "<FRAME_3D_CROSS_SECTIONS>"; _end = "</FRAME_3D_CROSS_SECTIONS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.FRAME_3D_CROSS_SECTIONS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.FRAME_3D_CROSS_SECTIONS_list = ExtractLine(C_variables.FRAME_3D_CROSS_SECTIONS_tag, false);
                C_variables.FRAME_3D_CROSS_SECTIONS_n = C_variables.FRAME_3D_CROSS_SECTIONS_list.Count;
            }



            _start = "<LAMINATE_THICKNESSES>"; _end = "</LAMINATE_THICKNESSES>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LAMINATE_THICKNESSES_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LAMINATE_THICKNESSES_list = ExtractLine(C_variables.LAMINATE_THICKNESSES_tag, false);
                C_variables.LAMINATE_THICKNESSES_n = C_variables.LAMINATE_THICKNESSES_list.Count;
            }


            _start = "<GEOMETRY_PATTERNS>"; _end = "</GEOMETRY_PATTERNS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.GEOMETRY_PATTERNS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.GEOMETRY_PATTERNS_list = ExtractLine(C_variables.GEOMETRY_PATTERNS_tag, false);
                C_variables.GEOMETRY_PATTERNS_n = C_variables.GEOMETRY_PATTERNS_list.Count;
            }


            _start = "<FIBROUS_PATTERNS>"; _end = "</FIBROUS_PATTERNS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.FIBROUS_PATTERNS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.FIBROUS_PATTERNS_list = ExtractLine(C_variables.FIBROUS_PATTERNS_tag, false);
                C_variables.FIBROUS_PATTERNS_n = C_variables.FIBROUS_PATTERNS_list.Count;
            }


            _start = "<LAYER_PROPERTIES>"; _end = "</LAYER_PROPERTIES>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LAYER_PROPERTIES_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LAYER_PROPERTIES_list = ExtractLine(C_variables.LAYER_PROPERTIES_tag, false);
                C_variables.LAYER_PROPERTIES_n = C_variables.LAYER_PROPERTIES_list.Count;
            }


            _start = "<LAYER_PATTERNS>"; _end = "</LAYER_PATTERNS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LAYER_PATTERNS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LAYER_PATTERNS_list = ExtractLine(C_variables.LAYER_PATTERNS_tag, false);
                C_variables.LAYER_PATTERNS_n = C_variables.LAYER_PATTERNS_list.Count;
            }


            _start = "<NUMERICAL_INTEGRATION>"; _end = "</NUMERICAL_INTEGRATION>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.NUMERICAL_INTEGRATION_tag = ExtractString(Femix_string, _start, _end);
                C_variables.NUMERICAL_INTEGRATION_list = ExtractLine(C_variables.NUMERICAL_INTEGRATION_tag, false);
                C_variables.NUMERICAL_INTEGRATION_n = C_variables.NUMERICAL_INTEGRATION_list.Count;
            }


            _start = "<ELEMENT_NODES>"; _end = "</ELEMENT_NODES>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.ELEMENT_NODES_tag = ExtractString(Femix_string, _start, _end);
                C_variables.ELEMENT_NODES_list = ExtractLine(C_variables.ELEMENT_NODES_tag, false);
                C_variables.ELEMENT_NODES_n = C_variables.ELEMENT_NODES_list.Count;
            }


            _start = "<ELEMENT_PROPERTIES>"; _end = "</ELEMENT_PROPERTIES>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.ELEMENT_PROPERTIES_tag = ExtractString(Femix_string, _start, _end);
                C_variables.ELEMENT_PROPERTIES_list = ExtractLine(C_variables.ELEMENT_PROPERTIES_tag, false);
                C_variables.ELEMENT_PROPERTIES_n = C_variables.ELEMENT_PROPERTIES_list.Count;
            }


            _start = "<LINEAR_SPRING_MATERIALS>"; _end = "</LINEAR_SPRING_MATERIALS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LINEAR_SPRING_MATERIALS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LINEAR_SPRING_MATERIALS_list = ExtractLine(C_variables.LINEAR_SPRING_MATERIALS_tag, false);
                C_variables.LINEAR_SPRING_MATERIALS_n = C_variables.LINEAR_SPRING_MATERIALS_list.Count;
            }



            _start = "<POINT_SPRINGS>"; _end = "</POINT_SPRINGS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.POINT_SPRINGS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.POINT_SPRINGS_list = ExtractLine(C_variables.POINT_SPRINGS_tag, false);
                C_variables.POINT_SPRINGS_n = C_variables.POINT_SPRINGS_list.Count;
            }



            _start = "<LINE_SPRINGS>"; _end = "</LINE_SPRINGS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LINE_SPRINGS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LINE_SPRINGS_list = ExtractLine(C_variables.LINE_SPRINGS_tag, false);
                C_variables.LINE_SPRINGS_n = C_variables.LINE_SPRINGS_list.Count;
            }


            _start = "<SURFACE_SPRINGS>"; _end = "</SURFACE_SPRINGS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.SURFACE_SPRINGS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.SURFACE_SPRINGS_list = ExtractLine(C_variables.SURFACE_SPRINGS_tag, false);
                C_variables.SURFACE_SPRINGS_n = C_variables.SURFACE_SPRINGS_list.Count;
            }

            _start = "<STIFFENERS>"; _end = "</STIFFENERS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.STIFFENERS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.STIFFENERS_list = ExtractLine(C_variables.STIFFENERS_tag, false);
                C_variables.STIFFENERS_n = C_variables.STIFFENERS_list.Count;
            }


            _start = "<LOAD>"; _end = "</LOAD>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LOAD_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LOAD_list = ExtractLine(C_variables.LOAD_tag, false);
                C_variables.LOAD_n = C_variables.LOAD_list.Count;
            }





            _start = "<LOAD_CASE>"; _end = "</LOAD_CASE>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {

                C_variables.LOAD_CASE_list = ExtarctMultiItemsString(Femix_string, _start, _end);
                C_variables.LOAD_CASE_n = C_variables.LOAD_CASE_list.Count;

            }




            int i = 0;
            string LOAD_CASE_NUMBER, LOAD_CASE_TITLE, LOAD_CASE_GROUP, INTEGRATION_TYPE, INTEGRATION_NAME,String_identifier;
            foreach (string item in C_variables.LOAD_CASE_list)
            {
                i++;
                LOAD_CASE_NUMBER = ""; LOAD_CASE_TITLE = ""; LOAD_CASE_GROUP = ""; INTEGRATION_TYPE = ""; INTEGRATION_NAME = "";


                _start = "<LOAD_CASE_PARAMETERS>"; _end = "</LOAD_CASE_PARAMETERS>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    //////vertical to horizontal
                      foreach (var row in C_variables.Temp_list)
                    {
                        if (row[1] == "LOAD_CASE_NUMBER")
                            LOAD_CASE_NUMBER = row[2];
                        if (row[1] == "LOAD_CASE_TITLE")
                            LOAD_CASE_TITLE = row[2];
                        if (row[01] == "LOAD_CASE_GROUP")
                            LOAD_CASE_GROUP = row[2];
                        if (row[1] == "INTEGRATION_TYPE")
                            INTEGRATION_TYPE = row[2];
                        if (row[1] == "INTEGRATION_NAME")
                            INTEGRATION_NAME = row[2];
                    }
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_list.Add(new List<string> {(i-1).ToString(), LOAD_CASE_NUMBER, LOAD_CASE_TITLE, LOAD_CASE_GROUP, INTEGRATION_TYPE, INTEGRATION_NAME });
                    //////end vertical to horizontal
                    ///
                    
                    //foreach (var row in C_variables.Temp_list)
                    //    row.Add($"({LOAD_CASE_NUMBER}) {LOAD_CASE_TITLE}");

                    C_variables.LOAD_CASE_PARAMETERS_list.AddRange(C_variables.Temp_list);
                    C_variables.LOAD_CASE_PARAMETERS_n = C_variables.LOAD_CASE_PARAMETERS_list.Count;
                }

                String_identifier = $"{LOAD_CASE_NUMBER}-{LOAD_CASE_TITLE}";



                _start = "<GRAVITY_LOAD>"; _end = "</GRAVITY_LOAD>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    //just for GRAVITY_LOAD (add row counter)
                    foreach (var row in C_variables.Temp_list)
                        row.Insert(0,"1");

                    foreach (var row in C_variables.Temp_list)
                        row.Add(String_identifier);

                    C_variables.GRAVITY_LOAD_list.AddRange(C_variables.Temp_list);
                    C_variables.GRAVITY_LOAD_n = C_variables.GRAVITY_LOAD_list.Count;
                }




                _start = "<POINT_LOADS>"; _end = "</POINT_LOADS>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    foreach (var row in C_variables.Temp_list)
                        row.Add(String_identifier);

                    C_variables.POINT_LOADS_list.AddRange(C_variables.Temp_list);
                    C_variables.POINT_LOADS_n = C_variables.POINT_LOADS_list.Count;
                }



                _start = "<INTERNAL_POINT_LOADS>"; _end = "</INTERNAL_POINT_LOADS>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    foreach (var row in C_variables.Temp_list)
                        row.Add(String_identifier);

                    C_variables.INTERNAL_POINT_LOADS_list.AddRange(C_variables.Temp_list);
                    C_variables.INTERNAL_POINT_LOADS_n = C_variables.INTERNAL_POINT_LOADS_list.Count;
                }


                _start = "<EDGE_LOADS>"; _end = "</EDGE_LOADS>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    foreach (var row in C_variables.Temp_list)
                        row.Add(String_identifier);

                    C_variables.EDGE_LOADS_list.AddRange(C_variables.Temp_list);
                    C_variables.EDGE_LOADS_n = C_variables.EDGE_LOADS_list.Count;
                }



                _start = "<INTERNAL_EDGE_LOADS>"; _end = "</INTERNAL_EDGE_LOADS>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    foreach (var row in C_variables.Temp_list)
                        row.Add(String_identifier);

                    C_variables.INTERNAL_EDGE_LOADS_list.AddRange(C_variables.Temp_list);
                    C_variables.INTERNAL_EDGE_LOADS_n = C_variables.INTERNAL_EDGE_LOADS_list.Count;
                }


                _start = "<FACE_LOADS>"; _end = "</FACE_LOADS>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    foreach (var row in C_variables.Temp_list)
                        row.Add(String_identifier);

                    C_variables.FACE_LOADS_list.AddRange(C_variables.Temp_list);
                    C_variables.FACE_LOADS_n = C_variables.FACE_LOADS_list.Count;
                }


                _start = "<INTERNAL_FACE_LOADS>"; _end = "</INTERNAL_FACE_LOADS>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    foreach (var row in C_variables.Temp_list)
                        row.Add(String_identifier);

                    C_variables.INTERNAL_FACE_LOADS_list.AddRange(C_variables.Temp_list);
                    C_variables.INTERNAL_FACE_LOADS_n = C_variables.INTERNAL_FACE_LOADS_list.Count;
                }


                _start = "<TEMPERATURE_VARIATION>"; _end = "</TEMPERATURE_VARIATION>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    foreach (var row in C_variables.Temp_list)
                        row.Add(String_identifier);

                    C_variables.TEMPERATURE_VARIATION_list.AddRange(C_variables.Temp_list);
                    C_variables.TEMPERATURE_VARIATION_n = C_variables.TEMPERATURE_VARIATION_list.Count;
                }


                _start = "<DIFFERENTIAL_TEMPERATURE_VARIATION>"; _end = "</DIFFERENTIAL_TEMPERATURE_VARIATION>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    foreach (var row in C_variables.Temp_list)
                        row.Add(String_identifier);

                    C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_list.AddRange(C_variables.Temp_list);
                    C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_n = C_variables.DIFFERENTIAL_TEMPERATURE_VARIATION_list.Count;
                }


                _start = "<PRESCRIBED_DISPLACEMENTS>"; _end = "</PRESCRIBED_DISPLACEMENTS>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    foreach (var row in C_variables.Temp_list)
                        row.Add(String_identifier);

                    C_variables.PRESCRIBED_DISPLACEMENTS_list.AddRange(C_variables.Temp_list);
                    C_variables.PRESCRIBED_DISPLACEMENTS_n = C_variables.PRESCRIBED_DISPLACEMENTS_list.Count;
                }


                _start = "<INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS>"; _end = "</INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS>";
                if (item.Contains(_start) && item.Contains(_end))
                {
                    C_variables.Temp_list.Clear();
                    C_variables.Temp_tag = ExtractString(item, _start, _end);
                    C_variables.Temp_list = ExtractLine(C_variables.Temp_tag, false);

                    foreach (var row in C_variables.Temp_list)
                        row.Add(String_identifier);

                    C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_list.AddRange(C_variables.Temp_list);
                    C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_n = C_variables.INTERNAL_PRESCRIBED_RELATIVE_DISPLACEMENTS_list.Count;
                }


            }




            _start = "<LOAD_CASE_COMBINATIONS>"; _end = "</LOAD_CASE_COMBINATIONS>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.LOAD_CASE_COMBINATIONS_tag = ExtractString(Femix_string, _start, _end);
                C_variables.LOAD_CASE_COMBINATIONS_list = ExtractLine(C_variables.LOAD_CASE_COMBINATIONS_tag, false);
                C_variables.LOAD_CASE_COMBINATIONS_n = C_variables.LOAD_CASE_COMBINATIONS_list.Count;
            }

            _start = "<KB_LOAD_CASE_COMBINATIONS_GENARATOR>"; _end = "</KB_LOAD_CASE_COMBINATIONS_GENARATOR>";
            if (Femix_string.Contains(_start) && Femix_string.Contains(_end))
            {
                C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag = ExtractString(Femix_string, _start, _end);
                C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list = ExtractLine(C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag, false);
                C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_n = C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list.Count;
            }
            else
            {
                string curFile = Path.GetDirectoryName(Femix_file_Path)+@"\femix_combinations.dat";
                if (C_variables.LOAD_CASE_COMBINATIONS_n>0 && File.Exists(curFile))
                {
                    curFile = System.IO.File.ReadAllText(curFile);
                    
                    string _line;
                    StringReader strReader = new StringReader(curFile);
                    while (true)
                    {
                        _line = strReader.ReadLine();
                        if (_line != null)
                        {
                            if (!String.IsNullOrWhiteSpace(_line))
                                C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list.Add(new List<string> { _line });
                        }
                        else { break; }
                    }

                    C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag = "";
                    int start_line, n_counter,n_current, n_loadcase;
                    string combo_name, combo_group,combo_list;
                    n_counter = 0; start_line = 0;
                    List<int> start_line_list = new List<int>();
                    List<int> current_n_list = new List<int>();
                    for (int i1 = 0; i1 < C_variables.Val(C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[0][0]); i1++)
                    {
                        if (i1 == 0)
                        {
                            start_line = 1;
                        }
                        else
                        {
                            start_line = start_line + 6+(C_variables.Val(C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[(start_line + 5)][0]) * 2);
                        }
                        start_line_list.Add(start_line);
                    }
                    foreach (int item in start_line_list)
                        current_n_list.Add(C_variables.Val(C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[item + 3][0]));

                        foreach (int item in start_line_list)
                    {
                        n_counter++;
                        combo_name = C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[item + 0][0];
                        combo_group = C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[item + 1][0];
                        n_current = C_variables.Val(C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[item + 3][0]);
                        if (item != 1)
                            n_current = n_current- current_n_list[n_counter - 1 - 1];
                        
                        n_loadcase = C_variables.Val(C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[item + 5][0]);
                        C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag += $"      {n_counter}    {combo_name}    {combo_group}    {n_current}";
         
                        for (int i2 = 1; i2 <= n_loadcase; i2++)
                        {
                            if (item == 1)
                            {
                                combo_list = C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[(item + 5) + (i2 * 2) - 1][0] + "-" +
                                                 C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[(item + 5) + ((i2 * 2))][0];
                            }
                            else
                            {
                                if (C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[(item + 5) + ((i2 * 2))][0] == "1")
                                {
                                    combo_list = C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[(item + 5) + (i2 * 2) - 1][0] + "-" +
                                               "FIX"+C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[(item + 5) + ((i2 * 2))][0];
                                }
                                else
                                {
                                combo_list = C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[(item + 5) + (i2 * 2) - 1][0] + "-" +
                                            C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list[(item + 5) + ((i2 * 2))][0];
                                }

                            }
                                C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag += $"    {combo_list}";
                        }
                        C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag += "     ;\n";
                    }
               

                    //Console.WriteLine(C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag, false);
                    //C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag = ExtractString(Femix_string, _start, _end);
                    C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list = ExtractLine(C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_tag, false);
                    C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_n = C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_list.Count;
                }
            }

            if (C_variables.KB_LOAD_CASE_COMBINATIONS_GENARATOR_n > 0)
                if (C_variables.LOAD_CASE_COMBINATIONS_n==0)
                     C_variables.LOAD_CASE_COMBINATIONS_n = 1;

            //Console.WriteLine(C_variables.MAIN_PARAMETERS_tag);
            //Console.WriteLine(C_variables.MAIN_PARAMETERS_list);
            //Console.WriteLine(C_variables.MAIN_PARAMETERS_n);

            //foreach (var item in C_variables.MAIN_PARAMETERS_list)
            //{
            //    foreach (var column in item)
            //    {
            //        Console.Write("[{0}]", column);
            //    }
            //    Console.WriteLine("\n__\n");
            //}
            //// Console.WriteLine(PhysicalNames_list[0][1]);

        }
        private static string ExtractString(string _string, string _start, string _end)
        {
            if (!_string.Contains(_start) | !_string.Contains(_end)) { return "null"; }
           //Console.WriteLine(_start);

            var startTag = _start;
            int startIndex = _string.IndexOf(startTag) + startTag.Length;
            int endIndex = _string.IndexOf(_end, startIndex);
            return _string.Substring(startIndex, endIndex - startIndex);

           
            //string value2= _string.Split(new string[] { _start,_end }, StringSplitOptions.None)[1];
  

        }

        private static List<string> ExtarctMultiItemsString(string _string, string _start, string _end)
        {

            //// For Single
            //var value = inputString.Split("<tag1>", '</tag1>')[1];

            //// For Multiple
            //var values = inputString.Split("<tag1>", '</tag1>').Where((_, index) => index % 2 != 0);


            List<String> output = new List<String>();
            if (!_string.Contains(_start) | !_string.Contains(_end)) { return output; }

            Regex pattern = new Regex(_start + @"[\s\S]*?" + _end);//@"<tag>[\s\S]*?</tag>
            //MatchCollection allMatches = pattern.Matches(_string);

            var arr = pattern.Matches(_string)
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();
            //foreach (var m in allMatches)
            //{
            //    //Console.WriteLine(m.ToString());
            //    output.Add(m.ToString());
            //}

            return arr;


        }
        private static string MergeLinesToSemicolon(string _string)
        {
            string _line;
            string comment_char = "#";
            string output_string="";
            StringReader strReader = new StringReader(_string);
            while (true)
            {
                _line = strReader.ReadLine();
                if (_line != null)
                {
                    _line = _line.Trim();
                    if (!String.IsNullOrWhiteSpace(_line) && _line[0] != comment_char[0])
                    {
                        if (_line.Contains(";") || _line.Contains(">"))
                        {
                            _line = GetUntilSemicolon(_line);
                            _line = _line + "\n";
                        }
                        //
                        output_string +=_line+" ";
                    }
                }
            else { break; }
            }

          return output_string;
        }
        public static List<IList<string>> ExtractLine(string _segment,Boolean line_without_semiclone)
        {
            
            string _line;
            string comment_char = "#";
            int LineCounter = 0;
            //int WordCounter = 0;
            List<IList<string>> list = new List<IList<string>>();
            List<String> list_col = new List<String>();

            if (line_without_semiclone==true)
                _segment=MergeLinesToSemicolon(_segment);

            StringReader strReader = new StringReader(_segment);
            while (true)
            {
                _line = strReader.ReadLine();

                if (_line != null)
                {
                    _line = _line.Trim();
                    if (!String.IsNullOrWhiteSpace(_line) && _line[0] != comment_char[0])
                    {

                        _line = GetUntilSemicolon(_line);
                         
                        string[] words = _line.Split(new string[] { " ", "=", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                        if (_line.Contains("="))
                        {
                            for (int i = 2; i < words.Count(); i++)
                                words[1] += " "+words[i];
                        }
                       

                        //Console.WriteLine(words[0]);

                        if (words[0].ToUpper()!= "COUNT" )///////////ignor lines with count data
                        {
                           
                            list_col = new List<String>();
                               list_col.Add(LineCounter.ToString());
                               //WordCounter = 0;
                               foreach (var word in words)
                               {
                                   //WordCounter += 1;
                                   list_col.Add(word);
                                   //Console.Write(word + "  | ");
                               }

                                //list_col[0] = WordCounter.ToString();
                                //Console.WriteLine("   ");

                                list.Add(list_col);

                               LineCounter += 1;
                        }
                    }
                }
                else { break; }
            }
            return list;
        }
         public static string GetUntilSemicolon(string text, string stopAt = ";")
    {
        //		Console.WriteLine("2232321.j".GetUntilSemicolon());
        if (!String.IsNullOrWhiteSpace(text))
        {
            int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

            if (charLocation > 0)
            {
                return text.Substring(0, charLocation);
            }
        }
        //return String.Empty;
        return text;
    }
    }
}
