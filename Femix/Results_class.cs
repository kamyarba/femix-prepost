using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Femix
{
    class Results_class
    {
        public MainForm mainfrm;
        public Results_class(MainForm form)
        {
            this.mainfrm = form;
        }
        //update nodes instead of drawing
        public void draw_undeformed_shape(RenderInterface KbRen,bool Unconnected_mesh)
        {
            //draw_objects();
            //KbRen.Draw_loads_supports(C_variables.POINT_LOADS_list, C_variables.PRESCRIBED_DISPLACEMENTS_list, C_variables.SUPPORTS_list, C_variables.support_size_factor / 25);//100
            //KbRen.Renderer.ResetCamera();

            if (Unconnected_mesh==true)
            {
                KbRen.Draw_objects(C_variables.unconnected_mesh_POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.unconnected_ELEMENT_NODES_list, "true");
            }
            else
            {
                KbRen.Draw_objects(C_variables.POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.ELEMENT_NODES_list, "true");
            }

            KbRen.Refresh();
        }
        public void draw_deformed_shape(RenderInterface KbRen, bool Unconnected_mesh)
        {
            //draw_objects();
            if (Unconnected_mesh == true)
            {
                KbRen.Draw_objects(C_variables.unconnected_deformed_mesh_POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.unconnected_ELEMENT_NODES_list, "true");
            }
            else
            {
                KbRen.Draw_objects(C_variables.Deformed_POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.ELEMENT_NODES_list,"true");
            }
            //KbRen.Draw_loads_supports(C_variables.Deformed_POINT_COORDINATES_list, C_variables.PRESCRIBED_DISPLACEMENTS_list, C_variables.SUPPORTS_list, C_variables.support_size_factor / 25);//100
            //KbRen.Renderer.ResetCamera();
            KbRen.Refresh();
        }
        public void draw_both_shape(RenderInterface KbRen, bool Unconnected_mesh)
        {
            if (Unconnected_mesh == true)
            {
                KbRen.Draw_objects(C_variables.unconnected_mesh_POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.unconnected_ELEMENT_NODES_list, "true_false");
                KbRen.mesh_view_opacity("opacity50");
                KbRen.Draw_objects(C_variables.unconnected_deformed_mesh_POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.unconnected_ELEMENT_NODES_list, "false");
            }
            else
            {
                KbRen.Draw_objects(C_variables.POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.ELEMENT_NODES_list, "true_false");
                KbRen.mesh_view_opacity("opacity50");
                KbRen.Draw_objects(C_variables.Deformed_POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.ELEMENT_NODES_list, "false");
            }
            KbRen.Refresh();
        }
        
        public void Create_deformed_shape_coordinates(string femix_filepath, string step, double scale)
        {


            string D1_filename, D2_filename, D3_filename, Comboname;
            string path = Path.GetDirectoryName(femix_filepath) + @"\";

            if (Directory.Exists(path + "dipva"))
                path += @"dipva\";

            string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            file_name = file_name.Remove(file_name.Length - 3);
            D1_filename = ""; D2_filename = ""; D3_filename = ""; Comboname = "";


            //scan for displacements and exctract combo name
            string extensions = "_di.pva";
            foreach (string file in Directory.GetFiles(path, file_name + "*" + extensions).OrderBy(d => new FileInfo(d).CreationTime).Where(item => item.EndsWith(extensions)))
            {
                Comboname = (Path.GetFileNameWithoutExtension(file.Remove(file.Length - (extensions.Length + 2)))).Remove(0, file_name.Length + 1);
                Comboname = String.Concat(Comboname.Where(char.IsLetter));
                //loadcase = String.Concat(loadcase.Where(char.IsDigit));
                if (Comboname != "")
                    break;
            }

            //if (Comboname == "")
            //    return;


            D1_filename = file_name + "_" + Comboname + step + "_D1_di.pva";
            D2_filename = file_name + "_" + Comboname + step + "_D2_di.pva";
            D3_filename = file_name + "_" + Comboname + step + "_D3_di.pva";

            string[] lines_D1, lines_D2, lines_D3;
            lines_D1 = new string[0]; lines_D2 = new string[0]; lines_D3 = new string[0];
            if (File.Exists(path + D1_filename))
                lines_D1 = System.IO.File.ReadAllLines(path + D1_filename);

            if (File.Exists(path + D2_filename))
                lines_D2 = System.IO.File.ReadAllLines(path + D2_filename);

            if (File.Exists(path + D3_filename))
                lines_D3 = System.IO.File.ReadAllLines(path + D3_filename);

            int number_of_lines = Math.Max(lines_D1.Count(), lines_D2.Count());
            number_of_lines = Math.Max(number_of_lines, lines_D3.Count());

            C_variables.Deformed_POINT_COORDINATES_list.Clear();
            for (int i = 0; i < number_of_lines; i++)
            {
                double Di_D1, Di_D2, Di_D3;
                Di_D1 = 0; Di_D2 = 0; Di_D3 = 0;
                string[] words_D1, words_D2, words_D3;
                words_D1 = new string[0]; words_D2 = new string[0]; words_D3 = new string[0];
                if (lines_D1.Count() == number_of_lines)
                {
                    words_D1 = lines_D1[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    Di_D1 = double.Parse(words_D1[1]);
                }
                if (lines_D2.Count() == number_of_lines)
                {
                    words_D2 = lines_D2[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    Di_D2 = double.Parse(words_D2[1]);
                }
                if (lines_D3.Count() == number_of_lines)
                {
                    words_D3 = lines_D3[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    Di_D3 = double.Parse(words_D3[1]);
                }
                Di_D1 = Di_D1 * scale; Di_D2 = Di_D2 * scale; Di_D3 = Di_D3 * scale;

                Di_D1 = double.Parse(C_variables.POINT_COORDINATES_list[i][2]) + Di_D1;
                Di_D2 = double.Parse(C_variables.POINT_COORDINATES_list[i][3]) + Di_D2;
                Di_D3 = double.Parse(C_variables.POINT_COORDINATES_list[i][4]) + Di_D3;


                C_variables.Deformed_POINT_COORDINATES_list.Add(new List<string> { i.ToString(), (i + 1).ToString(), Di_D1.ToString(), Di_D2.ToString(), Di_D3.ToString() });
                //Console.WriteLine($"{words_D1[0]}-{words_D2[0]}-{words_D3[0]}");
                //Console.WriteLine($"{words_D1[1]}/{words_D2[1]}/{words_D3[1]}");
            }



                //foreach (var item in C_variables.POINT_COORDINATES_list)
                //{
                //    foreach (var col in item)
                //    {
                //        Console.Write(col + "    ");
                //    }
                //    Console.WriteLine();
                //}

                //foreach (var item in C_variables.Deformed_POINT_COORDINATES_list)
                //{
                //    foreach (var col in item)
                //    {
                //        Console.Write(col + "    ");
                //    }
                //    Console.WriteLine();
                //}

  

        }

        public void Create_unconected_mesh(RenderInterface KbRen)
        {
            //Deformed_POINT_COORDINATES_list, unconnected_mesh_POINT_COORDINATES_list, unconnected_deformed_mesh_POINT_COORDINATES_list, unconnected_ELEMENT_NODES_list;
            C_variables.unconnected_ELEMENT_NODES_list.Clear();
            C_variables.unconnected_mesh_POINT_COORDINATES_list.Clear();
            int node_counter = 0;
            string x, y, z;
            List<string> Elements, Nodes;
             
            foreach (var item in C_variables.ELEMENT_NODES_list)
            {
                Elements = new List<string>();
                Elements.Add(item[0]);
                Elements.Add(item[1]);
                Elements.Add(item[2]);
                foreach (var col in item.Select((value, index) => new {value, index}))
                {
                    if (col.index > 2)
                    {
                        node_counter++;
                        Nodes = new List<string>();
                        Nodes.Add(col.value);
                        Nodes.Add(node_counter.ToString());
                        x = C_variables.POINT_COORDINATES_list[Int32.Parse(col.value) - 1][2];
                        y = C_variables.POINT_COORDINATES_list[Int32.Parse(col.value) - 1][3];
                        z = C_variables.POINT_COORDINATES_list[Int32.Parse(col.value) - 1][4];
                        Nodes.Add(x); 
                        Nodes.Add(y); 
                        Nodes.Add(z);
                        C_variables.unconnected_mesh_POINT_COORDINATES_list.Add(Nodes);
                        Elements.Add(node_counter.ToString());
                        //Console.Write(col.value + "  ");
                    }
                }
                C_variables.unconnected_ELEMENT_NODES_list.Add(Elements);

                //Console.WriteLine(Elements[0]);
                //Console.WriteLine("");
            }

            //KbRen.Draw_objects(C_variables.unconnected_mesh_POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.unconnected_ELEMENT_NODES_list, "true");
            //KbRen.Refresh();
        }
        public void Create_unconected_deformed_mesh(RenderInterface KbRen)
        {
            C_variables.unconnected_deformed_mesh_POINT_COORDINATES_list.Clear();
            int node_counter = 0;
            string x, y, z;
            List<string> Elements, Nodes;

            foreach (var item in C_variables.ELEMENT_NODES_list)
            {
                Elements = new List<string>();
                Elements.Add(item[0]);
                Elements.Add(item[1]);
                Elements.Add(item[2]);
                foreach (var col in item.Select((value, index) => new { value, index }))
                {
                    if (col.index > 2)
                    {
                        node_counter++;
                        Nodes = new List<string>();
                        Nodes.Add(col.value);
                        Nodes.Add(node_counter.ToString());
                        x = C_variables.Deformed_POINT_COORDINATES_list[Int32.Parse(col.value) - 1][2];
                        y = C_variables.Deformed_POINT_COORDINATES_list[Int32.Parse(col.value) - 1][3];
                        z = C_variables.Deformed_POINT_COORDINATES_list[Int32.Parse(col.value) - 1][4];
                        Nodes.Add(x);
                        Nodes.Add(y);
                        Nodes.Add(z);
                        C_variables.unconnected_deformed_mesh_POINT_COORDINATES_list.Add(Nodes);
                        Elements.Add(node_counter.ToString());
                        //Console.Write(col.value + "  ");
                    }
                }
                //C_variables.unconnected_ELEMENT_NODES_list.Add(Elements);
                //Console.WriteLine(Elements[0]);
                //Console.WriteLine("");
            }

            //KbRen.Draw_objects(C_variables.unconnected_mesh_POINT_COORDINATES_list, C_variables.ELEMENT_PROPERTIES_list, C_variables.unconnected_ELEMENT_NODES_list, "true");
            //KbRen.Refresh();
        }
        private void scan_folder_for_results(string femix_filepath)
        {
            //string path = Path.GetDirectoryName(femix_filepath) + @"\";
            //string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
            //file_name = file_name.Remove(file_name.Length - 3);

            //List<string> List_of_files = new List<string>();
            //string files_join = "";
            //string[] extensions = { "_di.pva", "_se.pva", "_sa.pva", "_cp.s3d" };
            //foreach (string ext in extensions)
            //    foreach (string file in Directory.GetFiles(path, file_name + "*" + ext).Where(item => item.EndsWith(ext)))
            //    {
            //        List_of_files.Add(file);
            //        files_join += file;
            //    }

            //combo_results.Items.Clear();
            //if (files_join.Contains("_di.pva"))
            //    combo_results.Items.Add("Displacement");
            //if (files_join.Contains("_se.pva"))
            //    combo_results.Items.Add("Stress");
            //if (files_join.Contains("_sa.pva"))
            //    combo_results.Items.Add("Strains");
            ////if (files_join.Contains("_cp.s3d"))
            ////    combo_results.Items.Add("Crack pattern");

            ////if (files_join.Contains("_sr.pva"))
            ////    combo_results.Items.Add("Stress resultants");
            ////if (files_join.Contains("_te.pva"))
            ////    combo_results.Items.Add("Temperatures");


        }


    }
}
