using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Femix
{
    partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.lbl_name.Text = "FEMIX PrePost";//AssemblyProduct;
            this.lbl_version.Text = String.Format("Version {0}", AssemblyVersion);
            //this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            //this.textBoxDescription.Text = AssemblyDescription;

            string description;
            description = "FEMIX PrePost \nProgramming Language: C# \nDevelopers: \nKamyar Bagherinejad Shahrbijari (Kamyar.ba@civil.uminho.pt) \nProfessor Joaquim Barros (barros@civil.uminho.pt)\n ";
            description += "\nAbout FEMIX  4.0 - Finite Element Analysis:\nStatic and dynamic analysis of structures\nLinear and materially nonlinear behavior\nElements: 3D beam - plane stress - plane strain - plate - 3D shell - 3D solid - cable - interface element\nDirect and iterative solvers\n";
            description += "\nFEMIX founders:\r\nAlvaro Azevedo \r\nJoaquim Barros (barros@civil.uminho.pt)\r\n\nFEMIX main contributors:\r\nJose Sena Cruz\r\nVentura Gouveia\r\n\nFEMIX Collaborators:\r\nRajendra Varma, Vitor Cunha, Eduardo Pereira, Ali Behbahani, Tiago Valente, Fábio Figueiredo, Luis Matos\r\n";
            description += "\nPlugins:\n";
            description += "GMSH mesh importer\nDeveloper: Kamyar Bagherinejad Shahrbijari\nKamyar.ba@gmail.com\n";
            description += "\nOpen Source Softwares:\n";
            description += "\nKitware VTK:\nThe Visualization Toolkit (VTK) is open source software for manipulating and displaying scientific data. It comes with state-of-the-art tools for 3D rendering, a suite of widgets for 3D interaction, and extensive 2D plotting capability.\n https://vtk.org/ \n";
            description += "\nGmsh:\nA three-dimensional finite element mesh generator.\n https://gmsh.info/ \n";
            description += "\nZedGraph:\nZedGraph is a class library, user control, and web control for .net, written in C#, for drawing 2D Line, Bar, and Pie Charts.\n https://github.com/ZedGraph \n";
            description += "\nFastColoredTextBox:\nFast Colored TextBox is text editor component for .NET. Allows you to create custom text editor with syntax highlighting.\n https://github.com/PavelTorgashov/FastColoredTextBox \n";
          
            this.textBoxDescription.Text = description;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void About_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:kamyar.ba@civil.uminho.pt");
        }
    }
}
