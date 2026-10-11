using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Femix
{

   
    public partial class femix_log : Form
    {
        public string Stringtransfer { get; set; }
        public femix_log()
        {
            InitializeComponent();
            Thread.Sleep(2);
        }

        
        private void Form2_Load(object sender, EventArgs e)
        {
            txtLog.Text=Stringtransfer;
        }

        public string ExecuteCommand(string command, bool Hidden_window, bool keep_process_open)
        {
            ///(echo recsv   & echo all   & echo 6   & echo Y   & echo end )  | "D:\PhD\Femix\posfemix.exe" "C:\Users\Kamyar\Desktop\qqq\Block\Plate"
            ///(echo Yes) | move "C:\Users\Kamyar\Desktop\qqq\Block\Plate_re.csv" "Plate_6_re.csv"
            string output = "";

            //var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.FileName = "cmd.exe";
            /*
            if (keep_process_open == true)
            {
                processInfo.Arguments = "/k " + command;
            }
            else
            {
                processInfo.Arguments = "/c " + command;
            }
            */
            processInfo.Arguments = !keep_process_open ? "/c " + command : "/k " + command;


            if (Hidden_window == true){
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;
            }

            var process = Process.Start(processInfo);

            if (Hidden_window == true)
            {
                process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                output += e.Data + "\n";

                //Console.WriteLine("output>>" + e.Data);
                
                process.BeginOutputReadLine();

                process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                    output += e.Data + "\n";
                //Console.WriteLine("error>>" + e.Data);
                process.BeginErrorReadLine();
            }

            //process.WaitForExit();
            if (process != null && !process.HasExited)
                process.WaitForExit();
            //Console.WriteLine("ExitCode: {0}", process.ExitCode);
            //process.Close();

            return output;
        }

        public void run_prefemix(string femix_filepath)
        {
            string filepathtorun = Path.GetDirectoryName(femix_filepath) + @"\" + Path.GetFileNameWithoutExtension(femix_filepath);
            filepathtorun = filepathtorun.Remove(filepathtorun.Length - 3);

            //Process.Start(@"FemixExe\Prefemix.exe", filepathtorun);

            //int ProcID;
            System.Diagnostics.Process myProcess = new Process();

            myProcess.StartInfo.FileName = @"cmd.exe";

            //myProcess.StartInfo.FileName = @"FemixExe\Prefemix.exe ";
            //myProcess.StartInfo.Arguments = filepathtorun;
            //myProcess.StartInfo.UseShellExecute = false;
            //myProcess.StartInfo.RedirectStandardOutput = true;

            //myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            myProcess.Start();


            //ProcID = myProcess.Id;
            Thread.Sleep(500);

            ////AppActivate();
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(@"FemixExe\Prefemix.exe " + $"\"{filepathtorun}\"");
            SendKeys.SendWait("{ENTER}");
            // SendKeys.SendWait(exit);

            //string output = myProcess.StandardOutput.ReadToEnd();
            myProcess.WaitForExit();

            //femix_log f = new femix_log();
            //f.txtLog.Text = ("ff");

            //return output;
            ////Console.WriteLine(myProcess.ExitTime)

            ////defdocros_max_def_output = read_defdocros_output(InputFileName)
        }

        public void run_femix(string femix_filepath)
        {
            string filepathtorun = Path.GetDirectoryName(femix_filepath) + @"\" + Path.GetFileNameWithoutExtension(femix_filepath);
            filepathtorun = filepathtorun.Remove(filepathtorun.Length - 3);

            // Process.Start(@"FemixExe\Prefemix.exe", filepathtorun);

            //int ProcID;
            System.Diagnostics.Process myProcess = new Process();

            myProcess.StartInfo.FileName = @"cmd.exe";

            //myProcess.StartInfo.FileName = @"FemixExe\Femix.exe ";
            //myProcess.StartInfo.Arguments = filepathtorun;
            //myProcess.StartInfo.UseShellExecute = false;
            //myProcess.StartInfo.RedirectStandardOutput = true;

            //myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            myProcess.Start();


            ////ProcID = myProcess.Id;
            Thread.Sleep(500);

            ////AppActivate();
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(@"FemixExe\Femix.exe " + $"\"{filepathtorun}\"");
            SendKeys.SendWait("{ENTER}");
            //// SendKeys.SendWait(exit);


            //string output = myProcess.StandardOutput.ReadToEnd();

            myProcess.WaitForExit();

            //return output;

            ////Console.WriteLine(myProcess.ExitTime)
            //MessageBox.Show(output);
            ////defdocros_max_def_output = read_defdocros_output(InputFileName)
        }

        public void run_postfemix( string femix_filepath)
        {
            ///(echo recsv   & echo all   & echo 6   & echo Y   & echo end )  | "D:\PhD\Femix\posfemix.exe" "C:\Users\Kamyar\Desktop\qqq\Block\Plate"
            ///(echo Yes) | move "C:\Users\Kamyar\Desktop\qqq\Block\Plate_re.csv" "Plate_6_re.csv"
            string filepathtorun = Path.GetDirectoryName(femix_filepath) + @"\" + Path.GetFileNameWithoutExtension(femix_filepath);
            filepathtorun = filepathtorun.Remove(filepathtorun.Length - 3);

            // Process.Start(@"FemixExe\Prefemix.exe", filepathtorun);

            //int ProcID;
            System.Diagnostics.Process myProcess = new Process();

            myProcess.StartInfo.FileName = @"cmd.exe";

            //myProcess.StartInfo.FileName = @"FemixExe\Posfemix.exe ";
            //myProcess.StartInfo.Arguments = filepathtorun;
            //myProcess.StartInfo.UseShellExecute = false;
            //myProcess.StartInfo.RedirectStandardOutput = true;

            //myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            myProcess.Start();


            //ProcID = myProcess.Id;
            Thread.Sleep(500);

            //AppActivate();
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(@"FemixExe\Posfemix.exe " + $"\"{filepathtorun}\"");
            SendKeys.SendWait("{ENTER}");
            //SendKeys.SendWait("help");
            //SendKeys.SendWait("{ENTER}");
            // SendKeys.SendWait(exit);


            //string output = myProcess.StandardOutput.ReadToEnd();

            myProcess.WaitForExit();

            ////Console.WriteLine(myProcess.ExitTime)
            //MessageBox.Show(output);
            ////defdocros_max_def_output = read_defdocros_output(InputFileName)
        }

        public string run_GMSH(string Geo_filepath, double clmin , double clmax)
        {

            //gmsh.exe simple.geo -1 -2 -3 -o k.msh -format msh
            //-clmin 0.1 -clmax 0.5

            string filename = Path.GetDirectoryName(Geo_filepath) + @"\" + Path.GetFileNameWithoutExtension(Geo_filepath);
            //filepathtorun = filepathtorun.Remove(filepathtorun.Length - 3);
            //filepathtorun = "-version";

            string filepathtorun = $"\"{filename}.geo\" -clmin {clmin} -clmax {clmax} -1 -2 -3 -o \"{filename}.msh\" -format msh ";
            string output = "";
            //Process.Start(@"FemixExe\Prefemix.exe", filepathtorun);

            //int ProcID;
            System.Diagnostics.Process myProcess = new Process();

            //myProcess.StartInfo.FileName = @"cmd.exe";

            myProcess.StartInfo.FileName = @"GMSH\gmsh.exe";
            myProcess.StartInfo.Arguments = filepathtorun;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardOutput = true;

            //myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            myProcess.Start();


            ////ProcID = myProcess.Id;
            //Thread.Sleep(500);

            //////AppActivate();
            //SendKeys.SendWait("{ENTER}");
            //SendKeys.SendWait(@"GMSH\gmsh.exe " + filepathtorun);
            //SendKeys.SendWait("{ENTER}");
            ////SendKeys.SendWait("exit");

            output += myProcess.StandardOutput.ReadToEnd();
            myProcess.WaitForExit();
            output += "\n"+ myProcess.ExitTime;

            return output;
            ////Console.WriteLine(myProcess.ExitTime)

        }

        public void run_GMSH_exe()
        {
            Process.Start(@"GMSH\gmsh.exe");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
