using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GmshToFemix
{
    public class ClassGMSHConvert
    {
        frm_gmsh_groups form_group = new frm_gmsh_groups();
        public string kamyar()
        {
            return "Kamyar_ba@yahoo.com";
        }
        public void frm()
        {

            form_group.Show();
        }

        /// <summary>
        ///0 --> Groups ; 1 --> point ; 2 --> Support ; 3 --> Elements  ; 4 --> Elements Properties ;
        /// </summary>
        /// <param name="msh_file">figr</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string[] Convert(string msh_file, int[] counters, List<IList<string>> Existed_points, bool keep_douplicate_point, bool imported_from_GMSH, string part_name, string element_type,bool quad_hex,bool embeded, string[] args)
        {


            string file_path, _strat, _end, _segment;

            string[] convert_output = new string[10];

            //try
            //{
                int counter = 0;
            List<String> Femix_Mesh = new List<String>();
            List<String> Elements_3 = new List<String>();
            List<String> sub_list = new List<String>();
            List<int> sub_list2 = new List<int>();


            List<IList<string>> MeshFormat, PhysicalNames_list, Entities, Nodes, Elements, Femix_Point_coordinates
                = new List<IList<string>>();

            Femix_Point_coordinates = new List<IList<string>>();

            List<IList<string>> Entities_2, Elements_2, group_elemnt_range_pro, group_support_range ;
            group_elemnt_range_pro = new List<IList<string>>();
            group_support_range = new List<IList<string>>();

            //Console.WriteLine("Enter Gmsh(*.msh) file name: ");
            file_path = msh_file;

            //if (!Path.IsPathRooted(file_path)){}
            if (!Path.HasExtension(file_path)) { file_path = file_path + ".msh"; }

            //while (!File.Exists(file_path))
            //{
            //    Console.Write($"File {file_path} does not exist!!! \nEnter a valid path:");
            //    file_path = Console.ReadLine();
            //    if (!Path.HasExtension(file_path)) { file_path = file_path + ".msh"; }
            //}
            //Console.WriteLine("Loading....." + Path.GetFullPath(file_path) + "\n");

            //output_file_name = file_path + ".Femix_Mesh";


            string gmsh_file = System.IO.File.ReadAllText(@file_path);
            //Console.WriteLine(gmsh_file);

            _strat = "$MeshFormat"; _end = "$EndMeshFormat";
            _segment = ExtractString(gmsh_file, _strat, _end);
            MeshFormat = ExtractLine(_segment);

            _strat = "$PhysicalNames"; _end = "$EndPhysicalNames"; _segment = ExtractString(gmsh_file, _strat, _end);
            PhysicalNames_list = ExtractLine(_segment);
            _strat = "$Entities"; _end = "$EndEntities"; _segment = ExtractString(gmsh_file, _strat, _end);
            Entities = ExtractLine(_segment);
            _strat = "$Nodes"; _end = "$EndNodes"; _segment = ExtractString(gmsh_file, _strat, _end);
            Nodes = ExtractLine(_segment);
            _strat = "$Elements"; _end = "$EndElements"; _segment = ExtractString(gmsh_file, _strat, _end);
            Elements = ExtractLine(_segment);


            //foreach (var item in PhysicalNames_list)
            //{
            //    foreach (var column in item)
            //        {
            //        Console.Write("--> {0}", column);
            //        }
            //    Console.WriteLine("__");
            //}
            //// Console.WriteLine(PhysicalNames_list[0][1]);

            /////Filter Group
            Entities_2 = ini_Entity_2(Entities);
            Elements_2 = ini_Element_2(Elements, Entities_2);


            form_group.list_all_group = new List<string>();
            form_group.list_element_group = new List<string>();
            form_group.list_support_group = new List<string>();

            List<string> list_id_element_group = new List<string>();
            List<IList<string>> list_id_support_group = new List<IList<string>>();


            foreach (var item in PhysicalNames_list)
            {
                if (item[0] == "0") { continue; }//ignore first line
                form_group.list_all_group.Add(item[3].Trim('"'));
            }

            if (imported_from_GMSH == true)
            {
                form_group.ShowDialog();
            }
            else
            {
                if (element_type == "0d-Point")
                {
                    //import as support
                    form_group.list_support_group.Add(part_name);
                }
                else
                {
                    //import as element
                    form_group.list_element_group.Add(part_name);
                }
            }

            

            //Nodes in Femix Format
            int nline_row_refrence = 1;
            int number_of_point_in_block = 0;
            int p_id;
            double p_x, p_y, p_z;
            while (nline_row_refrence < Nodes.Count - 1)
            {
                number_of_point_in_block = Val(Nodes[nline_row_refrence][4]);
                //Console.WriteLine(Nodes[nline_row_refrence][1]);
                for (int i = nline_row_refrence + 1; i <= (nline_row_refrence + number_of_point_in_block); i++)
                {
                    p_id = Int32.Parse(Nodes[i][1]);
                    p_x = double.Parse(Nodes[i + number_of_point_in_block][1]);
                    p_x = Math.Round(p_x, 5);
                    p_y = double.Parse(Nodes[i + number_of_point_in_block][2]);
                    p_y = Math.Round(p_y, 5);
                    p_z = double.Parse(Nodes[i + number_of_point_in_block][3]);
                    p_z = Math.Round(p_z, 5);
                    //Femix_Mesh.Add($" {p_id+counters[1]}  {p_x}  {p_y}  {p_z} ;");

                    Femix_Point_coordinates.Add(new List<string> { (p_id).ToString(), p_x.ToString(), p_y.ToString(), p_z.ToString() });
                    //Femix_Mesh.Add($"       {Nodes[i][1].PadRight(20)}  {Nodes[i + number_of_point_in_block][1].PadRight(20)}  {Nodes[i + number_of_point_in_block][2].PadRight(20)}  {Nodes[i + number_of_point_in_block][3].PadRight(20)} ;");
                    //Console.WriteLine(Nodes[i][1]);
                    //Console.WriteLine(Nodes[i+ number_of_point_in_block][1]);
                }
                nline_row_refrence += (number_of_point_in_block * 2) + 1;
            }


            ///////////////Points Map
            string[,] Point_Map = new string[Femix_Point_coordinates.Count, 3];
            if (keep_douplicate_point == false && Existed_points.Count > 0)
            {
                counter = counters[1];
                for (int i = 0; i < Femix_Point_coordinates.Count; i++)
                {
                    Point_Map[i, 0] = (Femix_Point_coordinates[i][0]);//ID_current
                    Point_Map[i, 1] = "New"; //ID_old_default
                    for (int i2 = 0; i2 < Existed_points.Count; i2++)
                    {
                        if (double.Parse(Femix_Point_coordinates[i][1]) == double.Parse(Existed_points[i2][2]) &&
                            double.Parse(Femix_Point_coordinates[i][2]) == double.Parse(Existed_points[i2][3]) &&
                            double.Parse(Femix_Point_coordinates[i][3]) == double.Parse(Existed_points[i2][4]))
                        {
                            Point_Map[i, 1] = (Existed_points[i2][1]);//ID_old
                        }
                    }
                    if (Point_Map[i, 1] == "New")
                    {
                        counter++;
                        Point_Map[i, 2] = counter.ToString();//ID_target
                    }
                    else
                    {
                        Point_Map[i, 2] = Point_Map[i, 1];//ID_target
                    }
                }

            }
            ///////////////End Points Map



            /////Elements and element properties

            bool is_item_exist = false;
            counter = 0;
            foreach (var item in PhysicalNames_list)
            {
                if (item[0] == "0") { continue; }//ignore first line
                //if (Val(item[1]) >0) //don't add points group
                //{
                //Console.WriteLine(item[3]);
                is_item_exist = form_group.list_element_group.Contains(item[3].Trim('"'));
                if (is_item_exist) {
                      list_id_element_group.Add(item[2]);//Add group id to a list

                      sub_list = new List<String>();
                      sub_list.Add(counter.ToString());
                      sub_list.Add(item[2]);
                      sub_list.Add(item[3].Trim('"'));
                      sub_list.Add(item[1]);
                      sub_list.Add("x");
                      sub_list.Add("x");
                    for (int i = 1; i <= Elements_2.Count; i++) { sub_list.Add(""); }
                      group_elemnt_range_pro.Add(sub_list);
                      //Console.WriteLine(item[2]);
                      counter++;
                }
                //}
            }
            //////ELEMENTS
            counter = 0;
            string line_content = "";
            foreach (var item in Elements_2)
            {
                //if (Val(item[4]) > 0 && item[2] != "0")//Only add elements with tag   && Don't add points 
                //{
                is_item_exist = list_id_element_group.Contains(item[4]);
                if (is_item_exist)
                {
                    counter++;

                    foreach (var item2 in group_elemnt_range_pro)
                    {
                        if (item[4] == item2[1])
                        {
                            group_elemnt_range_pro[Val(item2[0])][5 + counter] = counter.ToString();//add each new row to the list of group
                            break;
                        }
                    }

                    // Console.WriteLine(counter);
                    line_content = $"  {counter + counters[3]} " ;
                    line_content += item[10].PadRight(10) + " ";
                    for (int i = 11; i < 11 + Val(item[10]); i++)
                    {
                        p_id  = Int32.Parse(item[i]);
                        if (keep_douplicate_point == false && Existed_points.Count > 0)
                        {
                                for (int i2 = 0; i2 < Femix_Point_coordinates.Count; i2++)
                                {
                                    if (p_id == Int32.Parse(Point_Map[i2, 0])) 
                                    { 
                                        p_id = Int32.Parse(Point_Map[i2, 2]);
                                        break;
                                    }
                                }
                            }
                        else
                        {
                            p_id = Int32.Parse(item[i]) + counters[1];
                        }
                            line_content += $"{p_id}      ";//point numbers
                        //line_content += $"{Int32.Parse(item[i]) + counters[1]}      ";//point numbers
                    }
                    //Console.WriteLine("\n__");
                    Elements_3.Add(line_content + " ;");
                }
                //}
            }

            ///////support filter
            /////Elements and element properties

            is_item_exist = false;
            counter = 0;
            foreach (var item in PhysicalNames_list)
            {
                if (item[0] == "0") { continue; }//ignore first line
                is_item_exist = form_group.list_support_group.Contains(item[3].Trim('"'));
                if (is_item_exist)
                {
                    list_id_support_group.Add(new List<string> { item[2] , item[3].Trim('"') }) ;//Add group id and name to a list
                }
            }

            counter = 0;
            
            foreach (var item_support in list_id_support_group)
            {
                sub_list = new List<String>();
                sub_list2 = new List<int>();

                foreach (var item in Elements_2)
                {
                    //is_item_exist = list_id_support_group.Contains(item[4]);
                    if (item[4]== item_support[0])
                    {
                        for (int i = 11; i < 11 + Val(item[10]); i++)
                        {
                            if (!sub_list2.Contains(Val(item[i]))) 
                            { 
                            sub_list2.Add(Val(item[i]));
                            }
                        }
                    }
                }

                sub_list2.Sort();
                sub_list = sub_list2.ConvertAll<string>(delegate (int i) { return i.ToString(); });
                sub_list.Insert(0,item_support[0]);
                sub_list.Insert(1,item_support[1]);
                group_support_range.Add(sub_list);
            }

            ///end support filter
            /////end Filter Group




            Femix_Mesh.Clear();
            //Femix_Mesh.Add($"<MESH>\n#Mesh generated from GMSH V.{MeshFormat[0][1].PadRight(10)} \n");
            if (MeshFormat[0][2] != "0") { Console.WriteLine("Error on MeshFormat"); return null; }
            //Femix_Mesh.Add("<GROUP_NAMES>\n## Declaration of the names of the groups\n# N. of groups");

            //Groups
            if (PhysicalNames_list[0][1] == "null")
            {
                Femix_Mesh.Add("    COUNT = 1 ;\n");
                Femix_Mesh.Add("## Content of each column:\n#  A -> Counter\n#  B -> Name\n#        A  B");
                Femix_Mesh.Add("         1  Ka_Group ;");

            }
            else
            {
                Femix_Mesh.Add($"    COUNT = {PhysicalNames_list.Count-1} ;\n");
                Femix_Mesh.Add("## Content of each column:\n#  A -> Counter\n#  B -> Name\n#        A   B");
                counter = 0;
                foreach (var item in PhysicalNames_list)
                {
                    if (item[0] == "0") { continue; }
                    counter++;
                    Femix_Mesh.Add($"         {counter+counters[0]} {item[3].Trim('"')} ;");
                }
            }
            //Femix_Mesh.Add("</GROUP_NAMES>\n");
            convert_output[0] = string.Join("\n ", Femix_Mesh);
            Femix_Mesh.Clear();

            //Nodes
            //Femix_Mesh.Add("<POINT_COORDINATES> \n# Point coordinates (global coordinate system) \n# N. of points");
            Femix_Mesh.Add($"   COUNT = {Nodes[0][2]} ;\n");
            Femix_Mesh.Add("# Content of each column:\n#  A -> Counter\n#  B -> Coordinate - XG1\n#  C -> Coordinate - XG2\n#  D -> Coordinate - XG3");
            Femix_Mesh.Add($"#      {"A".PadRight(20)}  {"B".PadRight(20)}  {"C".PadRight(20)}  {"D".PadRight(20)}");


            //int n_node = Val(Nodes[0][2]);
            //for (int i = 1; i <= n_node; i++)
            //{
            // Femix_Mesh.Add($"       {Nodes[3*i-1][1].PadRight(20)}  {Nodes[3 * i][1].PadRight(20)}  {Nodes[3 * i][2].PadRight(20)}  {Nodes[3 * i][3].PadRight(20)} ;");
            //}
            //foreach (var item in Nodes)
            //{
            //    if (item[0] == "0") { continue; }
            //    Console.WriteLine(item[0]);
            //}



            if (keep_douplicate_point == false && Existed_points.Count > 0)
            {
                for (int i = 0; i < Femix_Point_coordinates.Count; i++)
                {
                    if (Point_Map[i, 1] == "New")
                    {
                        p_id = Int32.Parse(Point_Map[i, 2]);
                        p_x = double.Parse(Femix_Point_coordinates[i][1]);
                        p_y = double.Parse(Femix_Point_coordinates[i][2]);
                        p_z = double.Parse(Femix_Point_coordinates[i][3]);
                        Femix_Mesh.Add($" {p_id}  {p_x}  {p_y}  {p_z} ;");
                    }
                    //Console.WriteLine(Point_Map[i,0] + " | " + Point_Map[i, 1].PadRight(5) + " | " + Point_Map[i, 2]);
                }
            }
            else
            {
                foreach (var item in Femix_Point_coordinates)
                {
                    //p_id, p_x, p_y, p_z
                    Femix_Mesh.Add($"{Int32.Parse(item[0]) + counters[1]}    {item[1]}   {item[2]}   {item[3]}   ;");
                }
            }




            //Femix_Mesh.Add("</POINT_COORDINATES>\n");
            convert_output[1] = string.Join("\n ", Femix_Mesh);
            Femix_Mesh.Clear();

            //Femix_Mesh.Add("<SUPPORTS>");
            Femix_Mesh.Add("    ## Points with fixed degrees of freedom\n    # N. of points with fixed degrees of freedom ");
            Femix_Mesh.Add("    COUNT = 0 ;\n");
            Femix_Mesh.Add("    ## Content of each column: \n    #  A -> Counter (or counter range) \n    #  B -> Group name \n    #  C -> Phase (or phase range) \n    #  D -> Point number (or point number range) \n    #  E -> N. of fixed degrees of freedom in the current point(s) \n    #  F -> Fixed degrees of freedom: \n    #       - Available keywords: _D1, _D2, _D3, _R1, _R2 or _R3 ");
            Femix_Mesh.Add("    #       A   B           C       D   E       F \n");

            //////////////////////

            counter = 0;
            string next_coulmn_item, previous_coulmn_item, counter_start, counter_end;
            counter_start = ""; counter_end = "";
            foreach (var item in group_support_range)
            {
                line_content = "";
                for (int i = 2; i < item.Count; i++)
                {
                    //Console.Write(i+"--> {0}", item[i]+" | ");
                    if (i == item.Count - 1) { next_coulmn_item = ""; } else { next_coulmn_item = item[i + 1]; }
                    if (i == 2) { previous_coulmn_item = ""; } else { previous_coulmn_item = item[i - 1]; }
                    if (previous_coulmn_item == "" && item[i] != "") { counter_start = item[i]; }

                    if (Val(next_coulmn_item) != Val(previous_coulmn_item) + 1) { next_coulmn_item = ""; counter_start = item[i]; }
                    if ((next_coulmn_item == "" && item[i] != ""))
                    {
                        counter_end = item[i];

                        counter++;
                        if (keep_douplicate_point == false && Existed_points.Count > 0)
                        {
                            for (int i2 = 0; i2 < Femix_Point_coordinates.Count; i2++)
                            {
                                if (counter_start == Point_Map[i2, 0])
                                    counter_start = Point_Map[i2, 2];
                                if (counter_end == Point_Map[i2, 0])
                                    counter_end = Point_Map[i2, 2];
                            }
                        }
                        else
                        {
                            counter_start = (Int32.Parse(counter_start) + counters[1]).ToString();
                            counter_end = (Int32.Parse(counter_end) + counters[1]).ToString();
                        }
                        //Console.Write("[[[[["+counter_start + "-"+ counter_end+"]]]]]");
                        if (counter_start == counter_end)
                        {
                            //counter++;
                            //Femix_Mesh.Add($"       {counter+ counters[2]}      {item[1].PadRight(40)} {1.ToString().PadRight(15)} {Int32.Parse(counter_start) + counters[1]}   ;");
                            Femix_Mesh.Add($"       {counter + counters[2]}      {item[1]}      {1}     {counter_start}   ;");
                        }
                        else
                        {
                            //counter++;
                            //Femix_Mesh.Add($"       {counter+ counters[2]}      {item[1].PadRight(40)} {1.ToString().PadRight(15)} [{Int32.Parse(counter_start) + counters[1]}-{Int32.Parse(counter_end) + counters[1]}]   ;");
                            Femix_Mesh.Add($"       {counter + counters[2]}      {item[1]}     {1}     [{counter_start}-{counter_end}]   ;");
                        }
                    }
                }
                //Console.WriteLine("__");
            }
            //////////////////////////


            //Femix_Mesh.Add("</SUPPORTS> \n");
            convert_output[2] = string.Join("\n ", Femix_Mesh);
            Femix_Mesh.Clear();

            //Elements
            //Femix_Mesh.Add("<ELEMENT_NODES>\n# Nodes defining the elements\n# N. of elements");
            Femix_Mesh.Add($"   COUNT = {Elements_3.Count} ; \n");

            Femix_Mesh.Add("## Content of each column:\n#  A -> Counter\n#  B -> N. of nodes of the element\n#  C -> Nodes of the element");
            Femix_Mesh.Add($"#      {"A".PadRight(10)}{"B".PadRight(10)} {"C".PadRight(10)}");

            foreach (var item in Elements_3)
            {
                Femix_Mesh.Add(item);
            }
            //Femix_Mesh.Add("</ELEMENT_NODES>\n");
            convert_output[3] = string.Join("\n ", Femix_Mesh);
            Femix_Mesh.Clear();




            //Femix_Mesh.Add("<ELEMENT_PROPERTIES>\n# Specification of the element properties\n# N. of specifications");
            Femix_Mesh.Add($"   COUNT = {0} ; ");

            Femix_Mesh.Add("## Content of each column:\n# A -> Counter\n#  B -> Group name\n#  C -> Phase (or phase range)\n#  D -> Element (or element range)\n#  E -> Element type\n#  F -> Material type\n#  G -> Material name\n#  H -> Geometry type\n#  I -> Geometry name\n#  J -> Integration type (stiffness matrix)\n#  K -> Integration name (stiffness matrix)");
            Femix_Mesh.Add("#      A   B                                        C                D       E                          F                          G                          H                          I                          J                          K");

            //////////////////////
            counter = 0;
            //string next_coulmn_item, previous_coulmn_item, counter_start, counter_end;
            string element_type_print = "";
            if (element_type == "0d-Point")
            {
            }else if (element_type== "1d-Wire")
            {
                if (embeded == true)
                {
                    element_type_print = "_EMB_CABLE_3D";
                }
                else
                {
                    element_type_print = "_FRAME_3D";
                }
                
            }
            else if (element_type == "2d-Shell")
            {
                if (quad_hex == true)
                {
                    element_type_print = "_PLANE_STRESS_QUAD";
                }
                else
                {
                    element_type_print = "_PLANE_STRESS_TRIANG";
                }
                
            }
            else if (element_type == "3d-Solid")
            {
                if (quad_hex == true)
                { element_type_print = "_SOLID_HEXA"; }
                else
                { element_type_print = "_SOLID_TETRA"; }
            }
                counter_start = ""; counter_end = "";
            foreach (var item in group_elemnt_range_pro)
            {
                line_content = "";
                for (int i = 6; i < item.Count; i++)
                {
                    //Console.Write(i+"--> {0}", item[i]+" | ");
                    if (i == item.Count - 1) { next_coulmn_item = ""; } else { next_coulmn_item = item[i + 1]; }
                    if (i == 6) { previous_coulmn_item = ""; } else { previous_coulmn_item = item[i - 1]; }
                    if (previous_coulmn_item == "" && item[i] != "") { counter_start = item[i]; }
                    if (next_coulmn_item == "" && item[i] != "")
                    {
                        counter_end = item[i];
                        //Console.Write("[[[[["+counter_start + "-"+ counter_end+"]]]]]");
                        if (counter_start == counter_end)
                        {
                            counter++;
                            Femix_Mesh.Add($"       {counter + counters[4]} {item[2].PadRight(40)} {1.ToString().PadRight(15)} {Int32.Parse(counter_start) + counters[3]}   {element_type_print} ;");
                        }
                        else
                        {
                            counter++;
                            Femix_Mesh.Add($"       {counter + counters[4]} {item[2].PadRight(40)} {1.ToString().PadRight(15)} [{Int32.Parse(counter_start) + counters[3]}-{Int32.Parse(counter_end) + counters[3]}]      {element_type_print} ;");
                        }

                    }
                }
                //Console.WriteLine("__");
            }
            //////////////////////////

            //Femix_Mesh.Add("</ELEMENT_PROPERTIES>");
            convert_output[4] = string.Join("\n ", Femix_Mesh);
            Femix_Mesh.Clear();

            Femix_Mesh.Add("</MESH>");

            //convert_output[1] = string.Join("\n ", Femix_Mesh);
            //File.WriteAllLines(output_file_name, Femix_Mesh);
            //Console.WriteLine("FEMIX Mesh saved as: \n" + Path.GetFullPath(output_file_name));

            //Console.WriteLine("\nPress any key to exit.");
            //System.Console.ReadKey();

            //}
            //catch (Exception e)
            //{
            //    System.Windows.Forms.MessageBox.Show("Error, The is an error on the mesh file!"); //WinForms
            //    //  Block of code to handle errors
            //}
            return convert_output;
        }


        private static string ExtractString(string _string, string _start, string _end)
        {
            if (!_string.Contains(_start)) { return "null"; }
            var startTag = _start;
            int startIndex = _string.IndexOf(startTag) + startTag.Length;
            int endIndex = _string.IndexOf(_end, startIndex);
            return _string.Substring(startIndex, endIndex - startIndex);
        }

        private static List<IList<string>> ExtractLine(string _segment)
        {
            string _line;
            string comment_char = "#";
            int LineCounter = 0;
            //int WordCounter = 0;
            List<IList<string>> list = new List<IList<string>>();
            List<String> list_col = new List<String>();

            StringReader strReader = new StringReader(_segment);
            while (true)
            {
                _line = strReader.ReadLine();

                if (_line != null)
                {
                    _line = _line.Trim();
                    if (!String.IsNullOrWhiteSpace(_line) && _line[0] != comment_char[0])
                    {

                        //for femix://_line= _line.GetUntilSemicolon()
                        string[] words = _line.Split(new string[] { " ", "=" }, StringSplitOptions.RemoveEmptyEntries);
                        list_col = new List<String>();
                        list_col.Add(LineCounter.ToString());
                        //WordCounter = 0;
                        foreach (var word in words)
                        {
                            //WordCounter += 1;
                            list_col.Add(word);
                        }
                        //list_col[0] = WordCounter.ToString();

                        list.Add(list_col);

                        LineCounter += 1;
                    }
                }
                else { break; }
            }
            return list;
        }

        private static List<IList<string>> ini_Entity_2(List<IList<string>> Entities)
        {
            List<IList<string>> Entities_2 = new List<IList<string>>();
            List<String> sub_list = new List<String>();
            int counter = 0;

            int npoint, ncurve, nsurface, nvolume;
            npoint = Val(Entities[0][1]);
            ncurve = Val(Entities[0][2]); nsurface = Val(Entities[0][3]); nvolume = Val(Entities[0][4]);
            counter = 0;
            for (int i = 0 + 1; i <= 0 + npoint; i++)
            {
                counter++;
                sub_list = new List<String>();
                sub_list.Add(counter.ToString());
                sub_list.Add(Entities[i][1]);//tag  1
                sub_list.Add(Entities[i][2]);//x    2
                sub_list.Add(Entities[i][3]);//y    3
                sub_list.Add(Entities[i][4]);//tag  4
                sub_list.Add("x");//5
                sub_list.Add("x");//6
                sub_list.Add("x");//7
                sub_list.Add("0");//8-->0=point,1=curve,2=surface,3=volume
                sub_list.Add(Entities[i][5]);//n physical 9
                sub_list.Add(Entities[i][5 + Val(Entities[i][5])]);//last physical    10
                Entities_2.Add(sub_list);
            }
            for (int i = npoint + 1; i <= npoint + ncurve; i++)
            {
                counter++;
                sub_list = new List<String>();
                sub_list.Add(counter.ToString());
                sub_list.Add(Entities[i][1]);//tag  1
                sub_list.Add(Entities[i][2]);//x    2
                sub_list.Add(Entities[i][3]);//y    3
                sub_list.Add(Entities[i][4]);//tag  4
                sub_list.Add(Entities[i][5]);//5
                sub_list.Add(Entities[i][6]);//6
                sub_list.Add(Entities[i][7]);//7
                sub_list.Add("1");//8-->0=point,1=curve,2=surface,3=volume
                sub_list.Add(Entities[i][8]);//n physical 9
                sub_list.Add(Entities[i][8 + Val(Entities[i][8])]);//last physical    10
                sub_list.Add(Entities[i][8 + Val(Entities[i][8]) + 1]);//n boundary     11
                for (int j = (8 + Val(Entities[i][8]) + 1); j <= (8 + Val(Entities[i][8]) + 1 + (Val(Entities[i][8 + Val(Entities[i][8]) + 1]))); j++)
                {
                    sub_list.Add(Entities[i][j]);//tag boundary
                }
                Entities_2.Add(sub_list);
            }
            for (int i = npoint + ncurve + 1; i <= npoint + ncurve + nsurface; i++)
            {
                counter++;
                sub_list = new List<String>();
                sub_list.Add(counter.ToString());
                sub_list.Add(Entities[i][1]);//tag  1
                sub_list.Add(Entities[i][2]);//x    2
                sub_list.Add(Entities[i][3]);//y    3
                sub_list.Add(Entities[i][4]);//tag  4
                sub_list.Add(Entities[i][5]);//5
                sub_list.Add(Entities[i][6]);//6
                sub_list.Add(Entities[i][7]);//7
                sub_list.Add("2");//8-->0=point,1=curve,2=surface,3=volume
                sub_list.Add(Entities[i][8]);//n physical 9
                sub_list.Add(Entities[i][8 + Val(Entities[i][8])]);//last physical    10
                sub_list.Add(Entities[i][8 + Val(Entities[i][8]) + 1]);//n boundary     11
                for (int j = (8 + Val(Entities[i][8]) + 1); j <= (8 + Val(Entities[i][8]) + 1 + (Val(Entities[i][8 + Val(Entities[i][8]) + 1]))); j++)
                {
                    sub_list.Add(Entities[i][j]);//tag boundary
                }
                Entities_2.Add(sub_list);
            }
            for (int i = npoint + ncurve + nsurface + 1; i <= npoint + ncurve + nsurface + nvolume + 000; i++)
            {
                counter++;
                sub_list = new List<String>();
                sub_list.Add(counter.ToString());
                sub_list.Add(Entities[i][1]);//tag  1
                sub_list.Add(Entities[i][2]);//x    2
                sub_list.Add(Entities[i][3]);//y    3
                sub_list.Add(Entities[i][4]);//tag  4
                sub_list.Add(Entities[i][5]);//5
                sub_list.Add(Entities[i][6]);//6
                sub_list.Add(Entities[i][7]);//7
                sub_list.Add("3");//8-->0=point,1=curve,2=surface,3=volume
                sub_list.Add(Entities[i][8]);//n physical 9
                sub_list.Add(Entities[i][8 + Val(Entities[i][8])]);//last physical    10
                sub_list.Add(Entities[i][8 + Val(Entities[i][8]) + 1]);//n boundary     11
                for (int j = (8 + Val(Entities[i][8]) + 1); j <= (8 + Val(Entities[i][8]) + 1 + (Val(Entities[i][8 + Val(Entities[i][8]) + 1]))); j++)
                {
                    sub_list.Add(Entities[i][j]);//tag boundary
                }
                Entities_2.Add(sub_list);
            }
            return Entities_2;
        }


        private static List<IList<string>> ini_Element_2(List<IList<string>> Elements, List<IList<string>> Entity_2)
        {
            List<IList<string>> Elements_2 = new List<IList<string>>();
            List<String> sub_list = new List<String>();
            List<int> parent_row = new List<int>();
            int counter = 0;

            int n_row_parent = 1;
            parent_row.Add(n_row_parent);
            foreach (var item in Elements)
            {
                if (counter == n_row_parent)
                {
                    n_row_parent += Val(Elements[counter][4]) + 1;
                    if (n_row_parent >= Elements.Count) { break; }
                    parent_row.Add(n_row_parent);
                    //Console.WriteLine(Elements[n_row_parent][1]);
                }
                counter += 1;
            }

            counter = 0;
            foreach (int line_parent in parent_row)
            {
                //Console.WriteLine( ">>>>" + line_parent);
                string par_ent_typ, par_ent_tag, par_ent_phys, par_elem_type, elem_n_node;
                par_ent_typ = Elements[line_parent][1];
                par_ent_tag = Elements[line_parent][2];
                par_ent_phys = "xxx";
                
                foreach (var item in Entity_2) { if (par_ent_tag == item[1] && par_ent_typ == item[8]) { par_ent_phys = item[10]; break; } }

                par_elem_type = Elements[line_parent][3];
                elem_n_node = get_number_of_node(par_elem_type);


                for (int j = 1; j < Elements.Count; j++)
                {
                    if (j > line_parent && j <= (line_parent + Val(Elements[line_parent][4])))
                    {
                        //Console.WriteLine(Elements[j][1]);
                        counter++;
                        sub_list = new List<String>();
                        sub_list.Add(counter.ToString());//0
                        sub_list.Add(Elements[j][1]);//1
                        sub_list.Add(par_ent_typ);//2
                        sub_list.Add(par_ent_tag);//3
                        sub_list.Add(par_ent_phys);//4
                        sub_list.Add(par_elem_type);//5
                        sub_list.Add("x");//6
                        sub_list.Add("x");//7
                        sub_list.Add("x");//8
                        sub_list.Add("x");//9
                        sub_list.Add(elem_n_node);//10

                        int[] nodenumbers= new int[] {0};
                        //elementType
                        switch (par_elem_type)
                        {
                            case "8":
                                nodenumbers = new int[] { 2, 4, 3 };
                                break;
                            case "16":
                                nodenumbers = new int[] { 2, 6, 3, 7, 4, 8, 5, 9 };
                                break;
                            case "10":
                                nodenumbers = new int[] { 2, 6, 3, 7, 4, 8, 5, 9, 10 };
                                break;
                            case "5":
                                nodenumbers = new int[] { 3, 2, 6, 7, 4, 5, 9, 8 };
                                break;
                            case "17":
                                nodenumbers = new int[] { 3, 10, 2, 12, 6, 18, 7, 14, 13, 11, 19, 20, 4, 15, 5, 17, 9, 21, 8, 16 };
                                break;
                            case "9":
                                nodenumbers = new int[] { 2,5,3,6,4,7 };
                                break;
                            case "11":
                                nodenumbers = new int[] { 2,6,3,7,4,8,9,11,10,5 };//2, 6, 3, 8, 4, 7, 10, 9, 11, 5 
                                break;
                            default:
                                nodenumbers = new int[Val(elem_n_node)];
                                for (int i = 2; i <= Val(elem_n_node) + 1; i++)
                                {
                                    nodenumbers[i-2]=i;//11 to 11+n_node
                                }
                                break;
                        }

                        foreach (int i in nodenumbers)
                        {
                            sub_list.Add(Elements[j][i]);
                        }

                        Elements_2.Add(sub_list);
                    }
                }

                // Console.WriteLine(item);
                // Console.WriteLine(Elements[item][1]);
            }

            return Elements_2;
        }

        private static string get_number_of_node(string _element_type)
        {
            switch (_element_type)
            {
                case "15":
                    return "1";
                case "1":
                    return "2";
                case "8":
                    return "3";
                case "3":
                    return "4";
                case "16":
                    return "8";
                case "10":
                    return "9";
                case "5":
                    return "8";
                case "17":
                    return "20";
                case "2":
                    return "3";
                case "9":
                    return "6";
                case "4":
                    return "4";
                case "11":
                    return "10";
                default:
                    return "0";
            }
        }
        private static int Val(string _s)
        {
            string _new_string = string.Empty;
            //int output = 0;

            foreach (char _c in _s)
            {
                if (Char.IsDigit(_c)) { _new_string += _c; } else { break; }
                //Double:  if (Char.IsDigit(_c) || _c.Equals('.')) { _new_string += _c; } else { break; }

            }

            //Double:
            //return String.IsNullOrEmpty(_new_string) ? 0 : Convert.ToDouble(_new_string);
            //num = (int)number1;
            return String.IsNullOrEmpty(_new_string) ? 0 : int.Parse(_new_string);
        }

    }
}
