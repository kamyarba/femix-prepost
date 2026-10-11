using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ZedGraph;
using System.IO;
using System.Diagnostics;

namespace Femix
{
    class Graph_class
    {
        public ZedGraph.ZedGraphControl zg1;
		private List<string> plotdata;
		public void CreateGraphview(System.Windows.Forms.SplitContainer Window)
        {
            this.zg1 = new ZedGraph.ZedGraphControl();

            this.zg1.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zg1.Location = new System.Drawing.Point(32, 29);
            this.zg1.Margin = new System.Windows.Forms.Padding(21, 17, 21, 17);
            this.zg1.Name = "zg1";
            this.zg1.PanModifierKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None)));
            this.zg1.ScrollGrace = 0D;
            this.zg1.ScrollMaxX = 0D;
            this.zg1.ScrollMaxY = 0D;
            this.zg1.ScrollMaxY2 = 0D;
            this.zg1.ScrollMinX = 0D;
            this.zg1.ScrollMinY = 0D;
            this.zg1.ScrollMinY2 = 0D;
            //this.zg1.Size = new System.Drawing.Size(1171, 708);
            this.zg1.TabIndex = 0;
            this.zg1.Dock = System.Windows.Forms.DockStyle.Fill;
            Window.Panel1.Controls.Add(this.zg1);

			plotdata = new List<string>();
		}
		public void export_table_data(string femix_filepath)
        {
            try { 
			string path = Path.GetDirectoryName(femix_filepath) + @"\";
			System.IO.File.WriteAllLines(path + "Plot_data.csv", plotdata);
			Process.Start(path + "Plot_data.csv");
            }
            catch (Exception e)
            {
				MainForm.displayErrorBox(e.Message);
            }
		}

        public void Creat_Experimental_graph(string femix_filepath, string file_name)
        {
            string path = Path.GetDirectoryName(femix_filepath) + "\\" + file_name;
            if (!File.Exists(path))
                return;
            PointPairList list = new PointPairList();
            foreach (string readAllLine in File.ReadAllLines(path))
            {
                char[] chArray = new char[1] { ',' };
                string[] strArray = readAllLine.Split(chArray);
                list.Add(double.Parse(strArray[0]), double.Parse(strArray[1]));
            }
            this.CreateGraph(true, "Load-Deflection", "Deflection [mm]", "Load [kN]", true, true, "Experimental", list);
        }


        public void CreateGraph_data(string femix_filepath, string combo_xaxis, string txt_xaxis_nodes, string txt_xmulti, int combo_x_direction, string combo_yaxis, string txt_yaxis_nodes, string txt_ymulti, int combo_y_direction, bool chk_keepold,bool chk_majoraxis, bool chk_minoraxis)
        {

			string[] di_node_list_arr, re_node_list_arr;
			di_node_list_arr = txt_xaxis_nodes.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
			re_node_list_arr = txt_yaxis_nodes.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);

			createOutput(femix_filepath, di_node_list_arr, re_node_list_arr);

			if (chk_keepold == false)
            {
				plotdata.Clear();
            }

			// Make up some data points from the Sine function
			string title, x_axis, y_axis, legend;
			//Load
			//Reaction force
			//Deflection
			//Displacement
			title = "Load-Deflection";
			x_axis = "Deflection";
			y_axis= "Load";
			legend = "";
			if (Math.Abs(double.Parse(txt_xmulti)) == 1)
				x_axis +=" [mm]";
			if (Math.Abs(double.Parse(txt_xmulti)) == 0.001)
				x_axis += " [m]";
			if (Math.Abs(double.Parse(txt_ymulti)) == 1)
				y_axis += " [N]";
			if (Math.Abs(double.Parse(txt_ymulti)) == 2)
				y_axis += " [N]";
			if (Math.Abs(double.Parse(txt_ymulti)) == 0.001)
				y_axis += " [kN]";
			if (Math.Abs(double.Parse(txt_ymulti)) == 0.002)
				y_axis += " [kN]";
			//if (plotdata.Count() == 0)
			//	legend = "";


			string filepathtorun = femix_filepath.Remove(femix_filepath.Length - 7);

			int start_line_number, end_line_number;
			start_line_number = 0; end_line_number = 0;
			List<string> lines = new List<string>();

			if (!File.Exists($"{filepathtorun}_{di_node_list_arr[0]}_di.csv"))
            {
				MainForm.displayErrorBox($"Output for Node [{di_node_list_arr[0]}] doesn't exist!");
				return;
            }
			lines = File.ReadLines($"{filepathtorun}_{di_node_list_arr[0]}_di.csv").ToList();
			int line_counter=0;
			foreach (string line in lines)
			{
				line_counter++;
				string[] values = line.Trim('"').Split(',');
				if (values.Count() > 0)
                {
					//if (values[0] == ">>> Displacements")
					if (values[0] == "Content of each column:")
                    {
						if (lines[line_counter + 1].Trim('"').Trim() == "B  -> D1 displacement" && lines[line_counter + 6].Trim('"').Trim() == "G  -> R3 rotation")
						{
							start_line_number = line_counter + 9;
						} else if (lines[line_counter + 1].Trim('"').Trim() == "B  -> D2 displacement" && lines[line_counter + 3].Trim('"').Trim() == "A,                  B,                  C,")
						{
							start_line_number = line_counter + 5;
							if (combo_x_direction != 1 && combo_x_direction != 2)//D2 & D3
                            {
								MainForm.displayErrorBox("Your selected degree of freedom does not exist. \nChoose between D2 & D3!");
								return;
							}
							if (combo_y_direction != 1 && combo_y_direction != 2)//F2 & F3
							{
								MainForm.displayErrorBox("Your selected degree of freedom does not exist. \nChoose between F2 & F3!");
								return;
							}
							combo_x_direction = combo_x_direction - 1;//Because D1 does not exist
							combo_y_direction = combo_y_direction - 1;//Because F1 does not exist
						}
                        else
                        {
							MainForm.displayErrorBox("The format of the input file is unknown! \nplease contact kamyar_ba@yahoo.com");
							return;
						}
                    }
						if (values[0] == ">>> End of Femix Displacements")
						end_line_number = line_counter -2;
				}
			}


			double[] sum_of_reaction_force = new double[line_counter];
			line_counter = 0;
			foreach (var y_node in re_node_list_arr)
			{
				lines = File.ReadLines($"{filepathtorun}_{y_node}_re.csv").ToList();
				line_counter = 0;

				

				foreach (string line in lines)
				{
					line_counter++;
					string[] values = line.Trim('"').Split(',');
					
					if (line_counter >= start_line_number && line_counter <= end_line_number)
					{
						sum_of_reaction_force[line_counter-1] += double.Parse(values[combo_y_direction+1]);
						//Console.WriteLine($"{line_counter}"+values[combo_y_direction + 1]);
					}
                }
            }

            //line_counter = 0;
            //foreach (var item in sum_of_reaction_force)
            //{
            //    line_counter++;
            //    Console.WriteLine($"{line_counter}:" + item);
            //}

            if (chk_keepold == false)
			{
				zg1.GraphPane.CurveList.Clear();
				zg1.GraphPane.GraphObjList.Clear();
			}

			double x_value,y_value;
			x_value = 0;y_value = 0;

			plotdata.Add($"FEMIX PrePost");
			foreach (var x_node in di_node_list_arr)
            {
				PointPairList list = new PointPairList();
				if (di_node_list_arr.Count() > 1 || chk_keepold==true)
					legend = "Node " + x_node;
				lines = File.ReadLines($"{filepathtorun}_{x_node}_di.csv").ToList();
				plotdata.Add($"{DateTime.Now.ToString()}");
				plotdata.Add($"Node:,{x_node}");
				plotdata.Add($"{x_axis},{y_axis}");
				list.Add(0, 0);
				plotdata.Add($"{0}, {0}");
				line_counter = 0;
				foreach (var reaction in sum_of_reaction_force)
				{
					line_counter++;
					if (line_counter >= start_line_number && line_counter <= end_line_number)
					{
						y_value = reaction * double.Parse(txt_ymulti);
						string[] values = lines[line_counter - 1].Split(',');
						x_value = double.Parse(values[combo_x_direction + 1]);
						x_value = x_value * double.Parse(txt_xmulti);

						list.Add(x_value, y_value);
						plotdata.Add($"{x_value}, {y_value}");
					}
				}
				CreateGraph(chk_keepold, title, x_axis, y_axis,chk_majoraxis,chk_minoraxis, legend, list);
			}




            foreach (var node in di_node_list_arr)
            {
                if (node == "")
                    continue;
                string filename = $"{filepathtorun}_{node}_di.csv";
                File.Delete(filename);
            }
            foreach (var node in re_node_list_arr)
            {
                if (node == "")
                    continue;
                string filename = $"{filepathtorun}_{node}_re.csv";
                File.Delete(filename);
            }


        }
		private void createOutput(string femix_filepath, string[] di_node_list_arr, string[] re_node_list_arr)
		{

			string filepathtorun = femix_filepath.Remove(femix_filepath.Length - 7);
			string file_name = Path.GetFileNameWithoutExtension(femix_filepath);
			file_name = file_name.Remove(file_name.Length - 3);
			string directory = Path.GetDirectoryName(femix_filepath) + @"\";
			string appdirectory = AppDomain.CurrentDomain.BaseDirectory;
			femix_log femix_log = new femix_log();

			string batFileName = directory + Guid.NewGuid() + ".bat";
			List<string> batfiledata = new List<string>();

			string output_type = "dicsv";
			string ext = "_di.csv";
			foreach (var node in di_node_list_arr)
			{
				if (node == "")
					continue;
                //Femix x86
                //batfiledata.Add($"(echo {output_type}   & echo all   & echo {node}   & echo y   & echo end )" + " | " + appdirectory + @"FemixExe\Posfemix.exe " + $"\"{filepathtorun}\"");
                //Femix x64
                batfiledata.Add($"(echo {output_type}   & echo {node}   & echo all  & echo end )" + " | " + appdirectory + @"FemixExe\Posfemix.exe " + $"\"{filepathtorun}\"");

                //batfiledata.Add($"(echo Yes) | move \"{filepathtorun}{ext}\" \"{filepathtorun}_{node}{ext}\"");
                batfiledata.Add($"(echo Yes) | move \"{filepathtorun}_node{node}{ext}\" \"{filepathtorun}_{node}{ext}\"");

            }
            output_type = "recsv";
			ext = "_re.csv";
			foreach (var node in re_node_list_arr)
			{
				if (node == "")
					continue;
                //batfiledata.Add($"(echo {output_type}   & echo all   & echo {node}   & echo y   & echo end )" + " | " + appdirectory + @"FemixExe\Posfemix.exe " + $"\"{filepathtorun}\"");
                batfiledata.Add($"(echo {output_type}  & echo {node}  & echo all   & echo end )" + " | " + appdirectory + @"FemixExe\Posfemix.exe " + $"\"{filepathtorun}\"");

                batfiledata.Add($"(echo Yes) | move \"{filepathtorun}{ext}\" \"{filepathtorun}_{node}{ext}\"");
			}

			System.IO.File.WriteAllLines(batFileName, batfiledata);
			femix_log.ExecuteCommand($"\"{batFileName}\"",false, false);
			File.Delete(batFileName);
		}
		public void CreateGraph(bool keepold, string Title,string x_axis_title,string y_axis_title, bool chk_majoraxis,bool chk_minoraxis,string legend, PointPairList list)
		{

			ZedGraphControl zgc = zg1;

			GraphPane myPane = zgc.GraphPane;
			
			// Set the titles and axis labels
			myPane.Title.Text = Title;
			myPane.XAxis.Title.Text = x_axis_title;
			myPane.YAxis.Title.Text = y_axis_title;

			Random rnd = new Random();
		    Color randomColor = Color.FromArgb(rnd.Next(200), rnd.Next(200), rnd.Next(200));
			LineItem myCurve = myPane.AddCurve(legend, list, randomColor,
									SymbolType.None);
			//myCurve.Line.Fill = new Fill(Color.White, Color.Red, 45F);
			//myCurve.Symbol.Fill = new Fill(Color.White);
			//myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);
			//myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);



			if (chk_majoraxis==true)
			{
				myPane.XAxis.MajorGrid.IsVisible = true;
				myPane.XAxis.MajorGrid.DashOff = 0;
				myPane.XAxis.MajorGrid.Color = Color.FromArgb(208, 208, 208);
				myPane.YAxis.MajorGrid.IsVisible = true;
				myPane.YAxis.MajorGrid.DashOff = 0;
				myPane.YAxis.MajorGrid.Color = Color.FromArgb(208, 208, 208);
                //myPane.XAxis.MinorGrid.PenWidth = 1F;
            }
            else
            {
				myPane.XAxis.MajorGrid.IsVisible = false;
				myPane.YAxis.MajorGrid.IsVisible = false;
			}
			if (chk_minoraxis == true)
			{
				myPane.XAxis.MinorGrid.IsVisible = true;
				myPane.XAxis.MinorGrid.DashOff = 0;
				myPane.XAxis.MinorGrid.Color = Color.FromArgb(245, 245, 245);
				myPane.YAxis.MinorGrid.IsVisible = true;
				myPane.YAxis.MinorGrid.DashOff = 0;
				myPane.YAxis.MinorGrid.Color = Color.FromArgb(245, 245, 245);
			}
            else
            {
				myPane.XAxis.MinorGrid.IsVisible = false;
				myPane.YAxis.MinorGrid.IsVisible = false;
			}

			zgc.RestoreScale(zgc.GraphPane);

			zgc.IsShowPointValues = true;
			zgc.AxisChange();
			zgc.Refresh();
		}



		private void CreateGraph2()
		{
			ZedGraphControl zgc = zg1;
			//clear
			zgc.GraphPane.CurveList.Clear();
			zgc.GraphPane.GraphObjList.Clear();

			GraphPane myPane = zgc.GraphPane;

			// Set the titles and axis labels
			myPane.Title.Text = "My Test Graph22";
			myPane.XAxis.Title.Text = "sX Value2";
			myPane.YAxis.Title.Text = "22My Y Axis2";

			// Make up some data points from the Sine function
			PointPairList list = new PointPairList();
			for (double x = 0; x < 36; x++)
			{
				double y = Math.Cos(x * Math.PI / 15.0);

				list.Add(x, y);
			}

			// Generate a blue curve with circle symbols, and "My Curve 2" in the legend
			LineItem myCurve = myPane.AddCurve("My Curve", list, Color.Red,
									SymbolType.Circle);
			// Fill the area under the curve with a white-red gradient at 45 degrees
			myCurve.Line.Fill = new Fill(Color.White, Color.Red, 45F);
			// Make the symbols opaque by filling them with white
			myCurve.Symbol.Fill = new Fill(Color.White);

			// Fill the axis background with a color gradient
			myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);

			// Fill the pane background with a color gradient
			myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);

			//myPane.XAxis.Scale.MajorStepAuto = false;
			//myPane.XAxis.Scale.MajorStep = 100;
			myPane.XAxis.MajorGrid.IsVisible = true;
			myPane.XAxis.MajorGrid.DashOff = 0;
			myPane.XAxis.MajorGrid.Color= Color.FromArgb(0, 255, 0);

			myPane.XAxis.MinorGrid.IsVisible = true;
			myPane.XAxis.MinorGrid.DashOff = 0;
			myPane.XAxis.MinorGrid.Color = Color.FromArgb(0, 0, 255);
			myPane.XAxis.MinorGrid.PenWidth = 1F;

			// Calculate the Axis Scale Ranges
			zgc.AxisChange();

			//myPane.XAxis.Scale.Min = -300;
			//myPane.XAxis.Scale.Max = 300;
			//myPane.YAxis.Scale.Min = -100;
			//myPane.YAxis.Scale.Max = 500;
			zgc.IsShowHScrollBar = true;
			zgc.IsShowVScrollBar = true;
			zgc.IsShowPointValues = true;
			zgc.Refresh();
			//zgc.ZoomOut(myPane);
		}
	}
}
