#define VTK_MAJOR_VERSION_5
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using intersection;
using Kitware.VTK;

namespace Femix
{
    public class RenderInterface
    {

        
        public MainForm mainfrm;
        public RenderInterface(MainForm form)
        {
            this.mainfrm = form;
        }

        //intersection
        kamyar_intersection intersection = new kamyar_intersection();
        Point3D Point3d = new Point3D(); 
        Vector3D Vector3D = new Vector3D();

        // VTK rendering objects
        public RenderWindowControl RenWinControl1;
        private vtkRenderWindow renderWindow;
        public vtkRenderer Renderer;//viewport
        private vtkScalarBarActor ScalarBar;
        private vtkLookupTable colorLookupTable;

        // Interactors
        private vtkRenderWindowInteractor InterWindow;
        private vtkInteractorStyleRubberBandPick InterStylePick;
        private vtkInteractorStyleTrackballCamera InterStyleTrack;

        private vtkDataSetMapper selectedMapper;
        private vtkActor selectedActor;


        // Coordinate system object
        private vtkOrientationMarkerWidget Widget;
        private vtkAxesActor Axes;
        private vtkTextProperty TextProp;

        //
        //public List<vtkActor> actors;
        //private int n_actor;
        //public Dictionary<string,vtkActor> actorsD;
        //public Dictionary<string, vtkActor2D> labelactorsD;
        public List<vtkActor> actors;
        public List<vtkActor> EdgeActors;
        int Actor_Index_draw;
        public List<vtkActor2D> labelactors;
        private List<vtkUnstructuredGrid> uGrid;
        private List<vtkDataSetMapper> mapper;
        private List<vtkDataSetMapper> edgeMapper;
        private vtkLookupTable lut;
        private vtkPoints points;
        private vtkScalarBarActor scalarBar;



        //// Clip obejcts
        //private vtkPlane ClipPlane;
        //private double ClipPlaneSize;
        //private vtkActor ClipPlaneActor;
        //private vtkTextWidget Reverse;
        //private vtkTextWidget ClipX;
        //private vtkTextWidget ClipY;
        //private vtkTextWidget ClipZ;
        //private vtkTextWidget Grid;
        //private double[] BoundaryRange;

        //private vtkSliderWidget SliderWidget;
        //private vtkSliderRepresentation2D SliderRep;

        // Selecting method items
        //public vtkAppendPolyData AppendFaces;
        //public vtkPolyData Faces;
        public bool SelectionMode;
        private vtkTextWidget ModeWidget;
        private double SelectionSize;
        public vtkPoints SelectionPoints;
        private vtkPolyData SelectionPolyData;
        

        private vtkSphereSource SelectionSphere;
        private vtkGlyph3D SelectionGlyph;

        public vtkActor SelectionActor;

        private double[] color_edge, color_0d, color_1d, color_2d, color_3d,color_interface,color_axis,color_load,color_displacement,color_support_d, color_support_r, color_lefttext;

        public RenderInterface()
        {
            vtkObject.GlobalWarningDisplayOff(); // Turn off Warning Output Window
        }
        public void CreateViewport(System.Windows.Forms.SplitContainer Window)
        {

            color_edge = new double[] { 160 / 255.0, 160 / 255.0, 160 / 255.0 };
            color_0d = new double[] { 255 / 255.0, 255 / 255.0, 0 / 255.0 };
            color_1d = new double[] { 32 / 255.0, 56 / 255.0, 255 / 255.0 };
            color_2d = new double[] { 199 / 255.0, 91 / 255.0, 194 / 255.0 };
            color_3d = new double[] { 75 / 255.0, 190 / 255.0, 75 / 255.0 };
            color_interface = new double[] { 255 / 255.0, 77 / 255.0, 0 / 255.0 };
            color_axis = new double[] { 255 / 255.0, 217 / 255.0, 0 / 255.0 };
            color_lefttext = new double[] { 0.8, 0.4, 0.5 };
            color_load = new double[] { 107 / 255.0, 205 / 255.0, 91 / 255.0 };
            color_displacement = new double[] { 91 / 255.0, 150 / 255.0, 199 / 255.0 };
            color_support_d = new double[] { 91 / 255.0, 150 / 255.0, 199 / 255.0 };
            color_support_r = new double[] { 91 / 255.0, 199 / 255.0, 195 / 255.0 };

            vtkObject.GlobalWarningDisplayOff();
            RenWinControl1 = new RenderWindowControl();  // Initialize VTK Renderer Window Control

            RenWinControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            //RenWinControl1.Load += new System.EventHandler(this.RenderWindow1_Load);
            //this.Controls.Add(this.RenderWindow1);
            Window.Panel1.Controls.Add(this.RenWinControl1);


            // Create Render Window
            renderWindow = RenWinControl1.RenderWindow;
            //renderWindow.PointSmoothingOn();

            initialize_drawing_items();
            
            // Initialize Interactor
            InterWindow = vtkRenderWindowInteractor.New();

            InterWindow.LeftButtonPressEvt += new vtkObject.vtkObjectEventHandler(SelectPointClick);
   
            InterWindow.RightButtonPressEvt += new vtkObject.vtkObjectEventHandler(UnselectPointClick);
            renderWindow.SetInteractor(InterWindow);
            InterWindow.Initialize();

            InterStyleTrack = vtkInteractorStyleTrackballCamera.New();
            //InterStyleTrack.RemoveAllObservers();
            //InterWindow.SetInteractorStyle(InterStyleTrack);
            InterStylePick = vtkInteractorStyleRubberBandPick.New();
            
            InterWindow.SetInteractorStyle(InterStylePick);

            //InterWindow.Disable();

            selectedMapper = vtkDataSetMapper.New();
            selectedActor = vtkActor.New();
            


            // Initialize View
            Renderer = renderWindow.GetRenderers().GetFirstRenderer();
            Renderer.RemoveAllViewProps();
            //CreateViewportBorder(Renderer, new double[3] { 128.0, 128.0, 128.0 });

            // Set default background color
            Background_Color(null);

            Renderer.GetActiveCamera().ParallelProjectionOff();
            
            //// Initialize Selection objects
            //AppendFaces = vtkAppendPolyData.New();
            //Faces = vtkPolyData.New();
            //SelectionMode = false;
            //SelectionSize = 0.1;
            
            SelectionActor = vtkActor.New();
            SelectionPolyData = vtkPolyData.New();
            //SelectionPolyData.SetPoints(SelectionPoints);

            //SelectionSphere = vtkSphereSource.New();
            //SelectionSphere.SetPhiResolution(12);
            //SelectionSphere.SetThetaResolution(12);
            //SelectionSphere.SetRadius(SelectionSize);
            SelectionGlyph = vtkGlyph3D.New();
            //SelectionGlyph.SetInput(SelectionPolyData);
            //SelectionGlyph.SetSourceConnection(SelectionSphere.GetOutputPort());

            //SelectionMapper.SetInputConnection(SelectionGlyph.GetOutputPort());

            //SelectionActor.SetMapper(SelectionMapper);
            //SelectionActor.GetProperty().SetColor(1, 1, 1);
            //SelectionActor.VisibilityOn();
            //Renderer.AddActor(SelectionActor);

            // Create new Properties and Objects
            //CreateColorMap();
            //CreateScalarBar();

            SetViewOri("YZ");
            CreateAxes();
            

            leftcornertesxt();

            //Renderer.RemoveAllViewProps();




        }




        private void SelectPointClick(vtkObject sender, vtkObjectEventArgs e)
        {

            //InterStylePick.OnLeftButtonUp();
            // InterWindow.SetInteractorStyle(InterStylePick);
            //InterWindow.SetInteractorStyle(null);
            //InterStylePick.OnMouseMove();
            if (part_point_select) { part_left_click(); return; }

            if (SelectionMode == true)
            {
                //InterStylePick.OnRightButtonDown(); //prevent rotation
                /////point picker
                int[] clickPos = InterWindow.GetEventPosition();
                vtkPointPicker PointPicker = vtkPointPicker.New();
                PointPicker.SetTolerance(0.025);
                PointPicker.Pick(clickPos[0], clickPos[1], 0, Renderer);

                //vtkPoints points = Faces.GetPoints();

                double[] PickPosition = PointPicker.GetPickPosition();
                double PickpointID = PointPicker.GetPointId();
                
                
                string pointIDs = "";
                int pointcounter = 0;
                bool addtoselection = false;
                for (int j = 0; j < points.GetNumberOfPoints(); j++)
                {
                    if (Math.Abs(points.GetPoint(j)[0] - PickPosition[0]) < 1e-3 &&
                        Math.Abs(points.GetPoint(j)[1] - PickPosition[1]) < 1e-3 &&
                        Math.Abs(points.GetPoint(j)[2] - PickPosition[2]) < 1e-3)
                    {
                        pointcounter++;
                        PickpointID = j;
                        pointIDs += $"({j+1}); ";
                        PickPosition[0] = points.GetPoint(j)[0];
                        PickPosition[1] = points.GetPoint(j)[1];
                        PickPosition[2] = points.GetPoint(j)[2];
                        addtoselection = true;
                        //break;
                    }
                }

//PointPicker.GetPointId() != -1
                if (addtoselection)
                {
                    addtoselection = true;
                    for (int j = 0; j < SelectionPoints.GetNumberOfPoints(); j++)
                    {
                        if (SelectionPoints.GetPoint(j)[0] == PickPosition[0] &&
                            SelectionPoints.GetPoint(j)[1] == PickPosition[1] &&
                            SelectionPoints.GetPoint(j)[2] == PickPosition[2])
                        {
                            mainfrm.LogBox_add("Unselected point:");
                            if (pointcounter > 1)
                            {
                                mainfrm.LogBox_add($"{pointcounter} points. No.: {pointIDs}");
                            }
                            else
                            {
                                mainfrm.LogBox_add($"No.: {PickpointID + 1}");
                            }
                            
                            //mainfrm.LogBox_add($"[{PickPosition[0]} , {PickPosition[1]} , {PickPosition[2]}]");

                            vtkPoints newPoints = vtkPoints.New();

                            for (int i1 = 0; i1 < SelectionPoints.GetNumberOfPoints(); i1++)
                            {
                                if (i1 != j)
                                {
                                    newPoints.InsertNextPoint(SelectionPoints.GetPoint(i1)[0], SelectionPoints.GetPoint(i1)[1], SelectionPoints.GetPoint(i1)[2]);
                                }
                            }
                            SelectionPoints.ShallowCopy(newPoints);
                            addtoselection = false;
                            break;
                        }
              
                    }


                    if (addtoselection == true)
                    {
                        //Console.WriteLine(PickpointID + " ||| " + PickPosition[0] + "," + PickPosition[1] + "," + PickPosition[2]);
                        mainfrm.LogBox_add("Selected point:");
                        if (pointcounter > 1)
                        {
                            mainfrm.LogBox_add($"{pointcounter} points. No.: {pointIDs}");
                        }
                        else
                        {
                            mainfrm.LogBox_add($"No.: {PickpointID + 1}");
                        }
                        mainfrm.LogBox_add($"[{PickPosition[0]} , {PickPosition[1]} , {PickPosition[2]}]");

                        //SelectionPoints = new vtkPoints();
                        SelectionPoints.InsertNextPoint(PickPosition[0], PickPosition[1], PickPosition[2]);
                    }


                    //private vtkPolyDataMapper SelectionMapper;
                    //SelectionMapper = vtkPolyDataMapper.New();
                    //vtkVertex SelectionVertex = vtkVertex.New();
                    //vtkCellArray selectvertices = vtkCellArray.New();
                    //SelectionVertex.GetPointIds().SetId(0, SelectionPoints.GetNumberOfPoints() - 1);
                    //selectvertices.InsertNextCell(SelectionVertex);
                    //SelectionPolyData.SetPoints(SelectionPoints);
                    //SelectionPolyData.SetVerts(selectvertices);
                    //SelectionMapper.SetInputConnection(SelectionPolyData.GetProducerPort());

                    draw_selected_points();

                 }




                ///////cell picker
                ////int[] clickPos = InterWindow.GetEventPosition();
                //vtkCellPicker CellPicker = vtkCellPicker.New();
                //CellPicker.SetTolerance(0.025);
                //CellPicker.Pick(clickPos[0], clickPos[1], 0, Renderer);

                ////vtkPoints points = Faces.GetPoints();

                ////double[] PickPosition = CellPicker.GetPickPosition();
                //double PickCellID = CellPicker.GetCellId();
                //vtkPoints cellpoints = uGrid[0].GetCell(0).GetPoints();
                //MainForm.displayErrorBox(cellpoints.ToString());
                ////vtkActor pickactor = CellPicker.GetActor();
                ////pickactor.GetProperty().SetColor(1, 0, 0);
                ////Renderer.AddActor(pickactor);

                //mainfrm.LogBox_add(part_uGrid.GetCell(PickCellID).GetCellType().ToString());
                //vtkIdList listofpoints = part_uGrid.GetCell(PickCellID).GetPointIds();
                //mainfrm.LogBox_add("N. point" + listofpoints.GetNumberOfIds().ToString());
                //mainfrm.LogBox_add(listofpoints.GetId(0).ToString());
                //mainfrm.LogBox_add(listofpoints.GetId(1).ToString());

                //Console.WriteLine(PickCellID + " ||| " + PickPosition[0] + "," + PickPosition[1] + "," + PickPosition[2]);


                //vtkAreaPicker areaPicker = vtkAreaPicker.New();
                //vtkRenderWindowInteractor renderWindowInteractor = renderWindow.GetInteractor();
                //renderWindowInteractor.SetPicker(areaPicker);

            }


            //// Get the location of the click (in window coordinates)
            //int[] pos = InterWindow.GetEventPosition();

            //vtkCellPicker picker = vtkCellPicker.New();//picker.GetCellId()
            ////vtkPointPicker PointPicker = vtkPointPicker.New();
            ////vtkPicker picker = vtkPicker.New();//picker.GetActor()

            //picker.SetTolerance(0.05);

            //// Pick from this location.
            //picker.Pick(pos[0], pos[1], 0, Renderer);

            //double[] worldPosition = picker.GetPickPosition();
            //Console.WriteLine($"<< Cell id is:  << {picker.GetCellId()}");
            //if (picker.GetCellId() != -1)
            //{
            //    Console.WriteLine($"Pick position is: << {worldPosition[0]} <<" +
            //                 $"<< {worldPosition[1]} <<  << {worldPosition[2]}");
            //    vtkIdTypeArray ids = vtkIdTypeArray.New();
            //    ids.SetNumberOfComponents(1);
            //    ids.InsertNextValue(picker.GetCellId());

            //    vtkSelectionNode selectionNode = vtkSelectionNode.New();
            //    //selectionNode.SetFieldType(vtkSelectionNode::CELL);
            //    //selectionNode.SetContentType(vtkSelectionNode::INDICES);
            //    selectionNode.SetSelectionList(ids);

            //    vtkSelection selection = vtkSelection.New();
            //    selection.AddNode(selectionNode);

            //    vtkExtractSelection extractSelection = vtkExtractSelection.New();
            //    //extractSelection.SetInput(0, Data);
            //    extractSelection.SetInput(1, selection);
            //    extractSelection.Update();

            //    // In selection
            //    vtkUnstructuredGrid selected = new vtkUnstructuredGrid();
            //    selected.ShallowCopy(extractSelection.GetOutput());


            //    Console.WriteLine(
            //    $"There are  << {selected.GetNumberOfPoints()}" +
            //              $"<<  points in the selection. << ;" +
            //    $"<< There are  << {selected.GetNumberOfCells()}" +
            //              $"<<  cells in the selection.");

            //    selectedMapper.SetInput(selected);
            //    selectedActor.SetMapper(selectedMapper);
            //    selectedActor.GetProperty().EdgeVisibilityOn();
            //    selectedActor.GetProperty().SetColor(1, 0, 0);

            //    selectedActor.GetProperty().SetLineWidth(3);

            //    InterWindow.GetRenderWindow().GetRenderers().GetFirstRenderer().AddActor(selectedActor);

            //    Renderer.AddActor(selectedActor);
            //}

        }

        public void draw_selected_points()
        {
            vtkPolyVertex selectvertices = vtkPolyVertex.New();
            selectvertices.GetPointIds().SetNumberOfIds(SelectionPoints.GetNumberOfPoints());
            for (int i = 0; i < SelectionPoints.GetNumberOfPoints(); i++)
                selectvertices.GetPointIds().SetId(i, i);
            vtkUnstructuredGrid SelectionunStructuredGrid = vtkUnstructuredGrid.New();
            SelectionunStructuredGrid.SetPoints(SelectionPoints);
            SelectionunStructuredGrid.InsertNextCell(selectvertices.GetCellType(), selectvertices.GetPointIds());

            vtkDataSetMapper SelectionMapper = vtkDataSetMapper.New();
#if VTK_MAJOR_VERSION_5
             SelectionMapper.SetInput(SelectionunStructuredGrid);
#else
          SelectionMapper.SetInputData(SelectionunStructuredGrid);
#endif

            SelectionActor.SetMapper(SelectionMapper);
            SelectionActor.VisibilityOn();
            SelectionActor.GetProperty().SetPointSize(7);
            SelectionActor.GetProperty().SetColor(1, 0, 0);

            Renderer.AddActor(SelectionActor);
        }
        private void UnselectPointClick(vtkObject sender, vtkObjectEventArgs e)
        {
          

            //SelectionMode = true;
            //if (SelectionMode == true)
            //{
            //    int[] clickPos = InterWindow.GetEventPosition();
            //    vtkPointPicker PointPicker = vtkPointPicker.New();
            //    PointPicker.SetTolerance(0.05);
            //    PointPicker.Pick(clickPos[0], clickPos[1], 0, Renderer);


            //    double[] PickPosition = PointPicker.GetPickPosition();
            //    vtkPoints temp = vtkPoints.New();

            //    for (int j = 0; j < SelectionPoints.GetNumberOfPoints(); j++)
            //    {
            //        if (Math.Abs(SelectionPoints.GetPoint(j)[0] - PickPosition[0]) < 1e-6 &&
            //            Math.Abs(SelectionPoints.GetPoint(j)[1] - PickPosition[1]) < 1e-6 &&
            //            Math.Abs(SelectionPoints.GetPoint(j)[2] - PickPosition[2]) < 1e-6)
            //        { }
            //        else
            //        {
            //            temp.InsertNextPoint(SelectionPoints.GetPoint(j)[0],
            //                                 SelectionPoints.GetPoint(j)[1],
            //                                 SelectionPoints.GetPoint(j)[2]);
            //        }
            //    }

            //    SelectionPoints.ShallowCopy(temp);
            //    SelectionPolyData.SetPoints(SelectionPoints);
            //    SelectionGlyph = vtkGlyph3D.New();
            //    SelectionGlyph.SetInput(SelectionPolyData);
            //    SelectionGlyph.SetSourceConnection(SelectionSphere.GetOutputPort());
            //    SelectionMapper.SetInputConnection(SelectionGlyph.GetOutputPort());
            //    SelectionActor.SetMapper(SelectionMapper);

            //    // Refresh Viewport
            //    Refresh();
            //}
            Refresh();
        }

        private void initialize_drawing_items()
        {
            //list of actors
            actors = new List<vtkActor>();
            EdgeActors = new List<vtkActor>();
            mapper = new List<vtkDataSetMapper>();
            edgeMapper = new List<vtkDataSetMapper>();
            uGrid = new List<vtkUnstructuredGrid>();
            scalarBar = new vtkScalarBarActor();

            //n_actor = 0;
            //actors.Add("Nodes", vtkActor.New());

            labelactors = new List<vtkActor2D>();
            //labelactors.Add(vtkActor2D.New());
        }
        private void add_drawing_items()
        {
            labelactors.Add(vtkActor2D.New());

            edgeMapper.Add(vtkDataSetMapper.New());
            EdgeActors.Add(vtkActor.New());

            mapper.Add(vtkDataSetMapper.New());
            actors.Add(vtkActor.New());

            uGrid.Add(vtkUnstructuredGrid.New());
        }
        private void clear_drawing_items()
        {
            labelactors.Clear();
            mapper.Clear();
            actors.Clear();
            edgeMapper.Clear();
            EdgeActors.Clear();
            uGrid.Clear();
        }
        public void leftcornertesxt()
        {
            ////text
            vtkTextMapper textMapper = vtkTextMapper.New();
            vtkActor2D textactor = vtkActor2D.New();
            textMapper.SetInput($"FEMIX PrePost".ToString());
            textactor.SetMapper(textMapper);
            textactor.SetPosition(10, 10);

            textactor.GetProperty().SetColor(color_lefttext[0], color_lefttext[1], color_lefttext[2]);
            //textactor.GetProperty().SetPointSize();
            textactor.GetProperty().SetOpacity(0.5);
            Renderer.AddActor(textactor);

            CreateSlider();
            CreateClipPlane();


             
        }
        private void CreateViewportBorder(vtkRenderer renderer, double[] color)
        {
            //ModelLoaded = false;

            // points start at upper right and proceed anti-clockwise
            vtkPoints points = vtkPoints.New();
            points.SetNumberOfPoints(4);
            points.InsertPoint(0, 1, 1, 0);
            points.InsertPoint(1, 1e-3, 1, 0);
            points.InsertPoint(2, 1e-3, 1e-3, 0);
            points.InsertPoint(3, 1, 1e-3, 0);

            // create cells, and lines
            vtkCellArray cells = vtkCellArray.New();
            cells.Initialize();

            vtkPolyLine lines = vtkPolyLine.New();
            lines.GetPointIds().SetNumberOfIds(5);
            for (int i = 0; i < 4; ++i) lines.GetPointIds().SetId(i, i);
            lines.GetPointIds().SetId(4, 0);
            cells.InsertNextCell(lines);

            // now make tge polydata and display it
            vtkPolyData poly = vtkPolyData.New();
            poly.Initialize();
            poly.SetPoints(points);
            poly.SetLines(cells);

            // use normalized viewport coordinates since
            // they are independent of window size
            vtkCoordinate coordinate = vtkCoordinate.New();
            coordinate.SetCoordinateSystemToNormalizedViewport();

            vtkPolyDataMapper2D mapper = vtkPolyDataMapper2D.New();
#if VTK_MAJOR_VERSION_5
  mapper.SetInput(poly);
#else
         mapper.SetInputData(poly); 
#endif

            mapper.SetTransformCoordinate(coordinate);

            vtkActor2D actor = vtkActor2D.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetColor(color[0], color[1], color[2]);
            // line width should be at least 2 to be visible at extremes

            actor.GetProperty().SetLineWidth((float)2.0); // Line Width

            renderer.AddViewProp(actor);
        }

      
        public void Background_Color(string color)
        {
            switch (color)
            {
                case "white":
                    Renderer.GradientBackgroundOff();
                    Renderer.SetBackground(0.98,0.98,0.98);
                    break;
                case "black":
                    Renderer.GradientBackgroundOff();
                    Renderer.SetBackground(0.1,0.1,0.1);
                    break;
                 default:
                    Renderer.GradientBackgroundOn();
                    //Renderer.SetBackground(163.0 / 255.0, 163.0 / 255.0, 163.0 / 255.0);
                    Renderer.SetBackground(212.0 / 255, 214.0 / 255, 255.0 / 255);
                    Renderer.SetBackground2(45.0 / 255.0, 85.0 / 255.0, 125.0 / 255.0);
                    break;
            }

            Refresh();
            //Renderer.SetBackground(0.2, 0.3, 0.4);


            // Other properties
            //Renderer.GetActiveCamera().ParallelProjectionOn();
        }
        public void Refresh()
        {
            //Renderer.ResetCamera();
            Renderer.Render();
            //Renderer.ResetCameraClippingRange();
            RenWinControl1.Refresh();
        }
        private void CreateAxes()
        {
            Axes = new vtkAxesActor();
            Axes.GetXAxisShaftProperty().SetLineWidth((float)3.0);
            Axes.GetYAxisShaftProperty().SetLineWidth((float)3.0);
            Axes.GetZAxisShaftProperty().SetLineWidth((float)3.0);
            Axes.GetXAxisShaftProperty().SetRepresentationToSurface();
            Axes.GetYAxisShaftProperty().SetRepresentationToSurface();
            Axes.GetZAxisShaftProperty().SetRepresentationToSurface();

            Axes.GetXAxisCaptionActor2D().GetCaptionTextProperty().SetFontSize(20);
            Axes.GetXAxisCaptionActor2D().GetCaptionTextProperty().SetBold(0);
            Axes.GetXAxisCaptionActor2D().GetCaptionTextProperty().ItalicOff();
            Axes.GetXAxisCaptionActor2D().GetCaptionTextProperty().ShadowOff();
            Axes.GetXAxisCaptionActor2D().GetTextActor().SetTextScaleModeToNone();

            Axes.GetYAxisCaptionActor2D().GetCaptionTextProperty().SetFontSize(20);
            Axes.GetYAxisCaptionActor2D().GetCaptionTextProperty().SetBold(0);
            Axes.GetYAxisCaptionActor2D().GetCaptionTextProperty().ItalicOff();
            Axes.GetYAxisCaptionActor2D().GetCaptionTextProperty().ShadowOff();
            Axes.GetYAxisCaptionActor2D().GetTextActor().SetTextScaleModeToNone();

            Axes.GetZAxisCaptionActor2D().GetCaptionTextProperty().SetFontSize(20);
            Axes.GetZAxisCaptionActor2D().GetCaptionTextProperty().SetBold(0);
            Axes.GetZAxisCaptionActor2D().GetCaptionTextProperty().ItalicOff();
            Axes.GetZAxisCaptionActor2D().GetCaptionTextProperty().ShadowOff();
            Axes.GetZAxisCaptionActor2D().GetTextActor().SetTextScaleModeToNone();

            Widget = new vtkOrientationMarkerWidget();
            Widget.SetOrientationMarker(Axes);
            Widget.SetInteractor(renderWindow.GetInteractor());
            Widget.SetViewport(0.85, 0.7, 1.2, 1.2);
            Widget.SetEnabled(1);
            Widget.InteractiveOff();
        }

        public void SetViewOri(string view)
        {
            if (view == "XY")
            {
                Renderer.GetActiveCamera().SetPosition(0, 0, 1);
                Renderer.GetActiveCamera().SetFocalPoint(0, 0, -1);
                Renderer.GetActiveCamera().SetViewUp(0, 1, 0);
                Renderer.ResetCamera();
            }
            if (view == "YX")
            {
                Renderer.GetActiveCamera().SetPosition(0, 0, -1);
                Renderer.GetActiveCamera().SetFocalPoint(0, 0, 1);
                Renderer.GetActiveCamera().SetViewUp(0, -1, 0);
                Renderer.ResetCamera();
            }
            if (view == "YZ")
            {
                Renderer.GetActiveCamera().SetPosition(1, 0, 0);
                Renderer.GetActiveCamera().SetFocalPoint(-1, 0, 0);
                Renderer.GetActiveCamera().SetViewUp(0, 0, 1);
                Renderer.ResetCamera();
            }
            if (view == "ZY")
            {
                Renderer.GetActiveCamera().SetPosition(-1, 0, 0);
                Renderer.GetActiveCamera().SetFocalPoint(1, 0, 0);
                Renderer.GetActiveCamera().SetViewUp(0, 0, 1);
                Renderer.ResetCamera();
            }
            if (view == "ZX")
            {
                Renderer.GetActiveCamera().SetPosition(0, 1, 0);
                Renderer.GetActiveCamera().SetFocalPoint(0, -1, 0);
                Renderer.GetActiveCamera().SetViewUp(0, 0, 1);
                Renderer.ResetCamera();
            }
            if (view == "XZ")
            {
                Renderer.GetActiveCamera().SetPosition(0, -1, 0);
                Renderer.GetActiveCamera().SetFocalPoint(0, 1, 0);
                Renderer.GetActiveCamera().SetViewUp(0, 0, 1);
                Renderer.ResetCamera();
            }
            if (view == "ISO")
            {
                Renderer.GetActiveCamera().SetPosition(0, 0, 1);
                Renderer.GetActiveCamera().SetFocalPoint(1, 1, 0);
                Renderer.GetActiveCamera().SetViewUp(0, 0, 1);
                Renderer.ResetCamera();
            }
            Refresh();
        }
        public void FitView()
        {  
            Renderer.ResetCamera();
            Refresh();
        }
        public void Perspective(string view)
        {
            if (view == "on")
            {
                Renderer.GetActiveCamera().ParallelProjectionOff();
            }
            else
            {
                Renderer.GetActiveCamera().ParallelProjectionOn();
            }
            Renderer.ResetCamera();
            Refresh();
        }
   
        public void Draw_loads_supports(List<IList<string>> POINT_LOADS_list, List<IList<string>> PRESCRIBED_DISPLACEMENTS_list, List<IList<string>>  SUPPORTS_list,double scale)
        {
            if (points == null)
                return;

            int[] elementrange = new int[2];

            foreach (var item in SUPPORTS_list)
            {
                if (item.Count < 6) { return; }
                elementrange = element_range(item[4]);
                for (int pointID = elementrange[0]; pointID <= elementrange[1]; pointID++)
                {
                    
                     for (int i = 0; i < Int32.Parse(item[5]); i++)
                     {
                        //Console.Write(item[i+6] + "   ");
                        labelactors.Add(vtkActor2D.New());
                        EdgeActors.Add(vtkActor.New());
                        actors.Add(vtkActor.New());
                        actors[Actor_Index_draw] = Arrow_Lod_supt_Actor(new double[] { points.GetPoint(pointID-1)[0], points.GetPoint(pointID-1)[1], points.GetPoint(pointID-1)[2] },
                             "support", item[i + 6], "+", scale/2);
                         Renderer.AddActor(actors[Actor_Index_draw]);
                         C_variables.Layer_items["Supports"].Add(Actor_Index_draw);
                         Actor_Index_draw++;
                     }
                }
            }
            string sign = "";
            string axe = "";
            foreach (var item in POINT_LOADS_list)
            {
                //Console.WriteLine(item[9] );
                elementrange = element_range(item[2]);
                for (int pointID = elementrange[0]; pointID <= elementrange[1]; pointID++)
                {
                    for (int i = 0; i < 3;i++)
                    {
                        if (i == 0)
                            axe = "x";
                        if (i == 1)
                            axe = "y";
                        if (i == 2)
                            axe = "z";

                        if (double.Parse(item[i+3]) != 0)
                        {
                            if (double.Parse(item[i+3]) > 0) { sign = "+"; } else { sign = "-"; }
                            labelactors.Add(vtkActor2D.New());
                            EdgeActors.Add(vtkActor.New());
                            actors.Add(vtkActor.New());
                            actors[Actor_Index_draw] = Arrow_Lod_supt_Actor(new double[] { points.GetPoint(pointID - 1)[0], points.GetPoint(pointID - 1)[1], points.GetPoint(pointID - 1)[2] },
                                "load", axe, sign, scale*2);
                            Renderer.AddActor(actors[Actor_Index_draw]);
                            C_variables.Layer_items[item[9].Substring(item[9].IndexOf('-') + 1)].Add(Actor_Index_draw);
                            Actor_Index_draw++;
                        }

                        if (double.Parse(item[i + 6]) != 0)
                        {
                            if (double.Parse(item[i + 6]) > 0) { sign = "+"; } else { sign = "-"; }
                            labelactors.Add(vtkActor2D.New());
                            EdgeActors.Add(vtkActor.New());
                            actors.Add(vtkActor.New());
                            actors[Actor_Index_draw] = Arrow_Lod_supt_Actor(new double[] { points.GetPoint(pointID - 1)[0], points.GetPoint(pointID - 1)[1], points.GetPoint(pointID - 1)[2] },
                                "moment", axe, sign, scale * 2);
                            Renderer.AddActor(actors[Actor_Index_draw]);
                            C_variables.Layer_items[item[9].Substring(item[9].IndexOf('-') + 1)].Add(Actor_Index_draw);
                            Actor_Index_draw++;
                        }

                    }
                }
            }


            foreach (var item in PRESCRIBED_DISPLACEMENTS_list)
            {
                Console.WriteLine(item[5] );
                elementrange = element_range(item[2]);
                for (int pointID = elementrange[0]; pointID <= elementrange[1]; pointID++)
                {


                    if (double.Parse(item[4]) != 0)
                    {
                        if (double.Parse(item[4]) > 0) { sign = "+"; } else { sign = "-"; }
                        add_drawing_items();

                        actors[Actor_Index_draw] = Arrow_Lod_supt_Actor(new double[] { points.GetPoint(pointID - 1)[0], points.GetPoint(pointID - 1)[1], points.GetPoint(pointID - 1)[2] },
                            "disp", item[3], sign, scale * 2);
                        Renderer.AddActor(actors[Actor_Index_draw]);
                        C_variables.Layer_items[item[5].Substring(item[5].IndexOf('-') + 1)].Add(Actor_Index_draw);
                        Actor_Index_draw++;
                    }
    
                }
            }


            //Arrow_Lod_supt(new double[] { 1, 1, 0 }, "load", "x", "+", 100);

        }
        private vtkActor Arrow_Lod_supt_Actor(double[] point, string type, string axe, string direction, double scale)
        {
            //type = load; disp; supp_d; supp_ro
            //axe = x; y; z
            //if (type == "load")
            //{
            //    if (axe == "x")
            //    {
            //    }
            //}

            // _D1, _D2, _D3, _R1, _R2 or _R3

            vtkArrowSource arrowLoadx = vtkArrowSource.New();
            vtkArrowSource arrowLoady = vtkArrowSource.New();
            vtkArrowSource arrowLoadz = vtkArrowSource.New();
            vtkArrowSource arrowDispx = vtkArrowSource.New();
            vtkArrowSource arrowDispy = vtkArrowSource.New();
            vtkArrowSource arrowDispz = vtkArrowSource.New();
            vtkArrowSource arrowSuppx = vtkArrowSource.New();
            vtkArrowSource arrowSuppy = vtkArrowSource.New();
            vtkArrowSource arrowSuppz = vtkArrowSource.New();
            vtkArrowSource arrowSuppxx = vtkArrowSource.New();
            vtkArrowSource arrowSuppyy = vtkArrowSource.New();
            vtkArrowSource arrowSuppzz = vtkArrowSource.New();


            if (type == "support")
            {
                if (axe == "_D1") { type = "supp_d"; axe = "x"; }
                if (axe == "_D2") { type = "supp_d"; axe = "y"; }
                if (axe == "_D3") { type = "supp_d"; axe = "z"; }
                if (axe == "_R1") { type = "supp_ro"; axe = "x"; }
                if (axe == "_R2") { type = "supp_ro"; axe = "y"; }
                if (axe == "_R3") { type = "supp_ro"; axe = "z"; }
            }


            if (type == "disp")
            {
                if (axe == "_D1") { axe = "x";  }
                if (axe == "_D2") { axe = "y"; }
                if (axe == "_D3") { axe = "z"; }
                if (axe == "_R1") { axe = "x"; arrowDispx.SetTipLength(0.6); }
                if (axe == "_R2") { axe = "y"; arrowDispy.SetTipLength(0.6); }
                if (axe == "_R3") { axe = "z"; arrowDispz.SetTipLength(0.6); }
            }
            if (type == "moment")
            {
                type= "load";
                arrowLoadx.SetTipLength (0.6);
                arrowLoady.SetTipLength(0.6);
                arrowLoadz.SetTipLength(0.6);
            }



            arrowLoadx.SetShaftResolution(24);
            arrowLoadx.SetTipResolution(36);
            arrowLoady.SetShaftResolution(24);
            arrowLoady.SetTipResolution(36);
            arrowLoadz.SetShaftResolution(24);
            arrowLoadz.SetTipResolution(36);
            arrowDispx.SetShaftResolution(24);
            arrowDispx.SetTipResolution(36);
            arrowDispy.SetShaftResolution(24);
            arrowDispy.SetTipResolution(36);
            arrowDispz.SetShaftResolution(24);
            arrowDispz.SetTipResolution(36);

            arrowSuppxx.SetTipResolution(5);
            arrowSuppyy.SetTipResolution(5);
            arrowSuppzz.SetTipResolution(5);

            arrowSuppx.SetTipLength(1);
            arrowSuppy.SetTipLength(1);
            arrowSuppz.SetTipLength(1);
            arrowSuppxx.SetTipLength(0.9);
            arrowSuppyy.SetTipLength(0.9);
            arrowSuppzz.SetTipLength(0.9);
            arrowSuppx.SetTipRadius(0.5);
            arrowSuppy.SetTipRadius(0.5);
            arrowSuppz.SetTipRadius(0.5);
            arrowSuppxx.SetTipRadius(0.4);
            arrowSuppyy.SetTipRadius(0.4);
            arrowSuppzz.SetTipRadius(0.4);

            //arrowSource02.SetShaftResolution(24);   // default = 6
            //arrowSource02.SetTipResolution(36);     // default = 6
            //arrowSource02.SetShaftRadius(0.1);
            //arrowSource02.SetTipRadius(0.2);
            //arrowSource02.SetTipLength(0.5);


            // Visualize
            vtkPolyDataMapper mapper_Loadx = vtkPolyDataMapper.New();
            vtkPolyDataMapper mapper_Loady = vtkPolyDataMapper.New();
            vtkPolyDataMapper mapper_Loadz = vtkPolyDataMapper.New();
            vtkPolyDataMapper mapper_Dispx = vtkPolyDataMapper.New();
            vtkPolyDataMapper mapper_Dispy = vtkPolyDataMapper.New();
            vtkPolyDataMapper mapper_Dispz = vtkPolyDataMapper.New();
            vtkPolyDataMapper mapper_Suppx = vtkPolyDataMapper.New();
            vtkPolyDataMapper mapper_Suppy = vtkPolyDataMapper.New();
            vtkPolyDataMapper mapper_Suppz = vtkPolyDataMapper.New();
            vtkPolyDataMapper mapper_Suppxx = vtkPolyDataMapper.New();
            vtkPolyDataMapper mapper_Suppyy = vtkPolyDataMapper.New();
            vtkPolyDataMapper mapper_Suppzz = vtkPolyDataMapper.New();

            mapper_Loadx.SetInputConnection(arrowLoadx.GetOutputPort());
            mapper_Loady.SetInputConnection(arrowLoady.GetOutputPort());
            mapper_Loadz.SetInputConnection(arrowLoadz.GetOutputPort());
            mapper_Dispx.SetInputConnection(arrowDispx.GetOutputPort());
            mapper_Dispy.SetInputConnection(arrowDispy.GetOutputPort());
            mapper_Dispz.SetInputConnection(arrowDispz.GetOutputPort());
            mapper_Suppx.SetInputConnection(arrowSuppx.GetOutputPort());
            mapper_Suppy.SetInputConnection(arrowSuppy.GetOutputPort());
            mapper_Suppz.SetInputConnection(arrowSuppz.GetOutputPort());
            mapper_Suppxx.SetInputConnection(arrowSuppxx.GetOutputPort());
            mapper_Suppyy.SetInputConnection(arrowSuppyy.GetOutputPort());
            mapper_Suppzz.SetInputConnection(arrowSuppzz.GetOutputPort());

            vtkActor actor_Loadx = vtkActor.New();
            vtkActor actor_Loady = vtkActor.New();
            vtkActor actor_Loadz = vtkActor.New();
            vtkActor actor_Dispx = vtkActor.New();
            vtkActor actor_Dispy = vtkActor.New();
            vtkActor actor_Dispz = vtkActor.New();
            vtkActor actor_Suppx = vtkActor.New();
            vtkActor actor_Suppy = vtkActor.New();
            vtkActor actor_Suppz = vtkActor.New();
            vtkActor actor_Suppxx = vtkActor.New();
            vtkActor actor_Suppyy = vtkActor.New();
            vtkActor actor_Suppzz = vtkActor.New();



            actor_Loadx.SetMapper(mapper_Loadx);
            actor_Loady.SetMapper(mapper_Loady);
            actor_Loadz.SetMapper(mapper_Loadz);
            actor_Dispx.SetMapper(mapper_Dispx);
            actor_Dispy.SetMapper(mapper_Dispy);
            actor_Dispz.SetMapper(mapper_Dispz);
            actor_Suppx.SetMapper(mapper_Suppx);
            actor_Suppy.SetMapper(mapper_Suppy);
            actor_Suppz.SetMapper(mapper_Suppz);
            actor_Suppxx.SetMapper(mapper_Suppxx);
            actor_Suppyy.SetMapper(mapper_Suppyy);
            actor_Suppzz.SetMapper(mapper_Suppzz);



            if (direction == "+")
            {
                actor_Loadx.SetPosition((-1 * scale) + point[0], point[1], point[2]);
                actor_Loadx.SetOrigin(0, 0, 0);
                actor_Loadx.SetOrientation(0, 0, 0);
                actor_Loadx.SetScale(scale);

                actor_Loady.SetPosition(point[0], (-1 * scale) + point[1], point[2]);
                actor_Loady.SetOrigin(0, 0, 0);
                actor_Loady.SetOrientation(0, 0, 90);
                actor_Loady.SetScale(scale);

                actor_Loadz.SetPosition(point[0], point[1], (-1 * scale) + point[2]);
                actor_Loadz.SetOrigin(0, 0, 0);
                actor_Loadz.SetOrientation(0, -90, 0);
                actor_Loadz.SetScale(scale);
            }
            else
            {

                actor_Loadx.SetPosition((1 * scale) + point[0], point[1], point[2]);
                actor_Loadx.SetOrigin(0, 0, 0);
                actor_Loadx.SetOrientation(0, 0, 180);
                actor_Loadx.SetScale(scale);

                actor_Loady.SetPosition(point[0], (1 * scale) + point[1], point[2]);
                actor_Loady.SetOrigin(0, 0, 0);
                actor_Loady.SetOrientation(0, 0, -90);
                actor_Loady.SetScale(scale);

                actor_Loadz.SetPosition(point[0], point[1], (1 * scale) + point[2]);
                actor_Loadz.SetOrigin(0, 0, 0);
                actor_Loadz.SetOrientation(0, 90, 0);
                actor_Loadz.SetScale(scale);
            }


            if (direction == "+")
            {
                actor_Dispx.SetPosition(point[0], point[1], point[2]);
                actor_Dispx.SetOrigin(0, 0, 0);
                actor_Dispx.SetOrientation(0, 0, 0);
                actor_Dispx.SetScale(scale);

                actor_Dispy.SetPosition(point[0], point[1], point[2]);
                actor_Dispy.SetOrigin(0, 0, 0);
                actor_Dispy.SetOrientation(0, 0, 90);
                actor_Dispy.SetScale(scale);

                actor_Dispz.SetPosition(point[0], point[1], point[2]);
                actor_Dispz.SetOrigin(0, 0, 0);
                actor_Dispz.SetOrientation(0, -90, 0);
                actor_Dispz.SetScale(scale);
            }
            else
            {
                actor_Dispx.SetPosition(point[0], point[1], point[2]);
                actor_Dispx.SetOrigin(0, 0, 0);
                actor_Dispx.SetOrientation(0, 0, 180);
                actor_Dispx.SetScale(scale);

                actor_Dispy.SetPosition(point[0], point[1], point[2]);
                actor_Dispy.SetOrigin(0, 0, 0);
                actor_Dispy.SetOrientation(0, 0, -90);
                actor_Dispy.SetScale(scale);

                actor_Dispz.SetPosition(point[0], point[1], point[2]);
                actor_Dispz.SetOrigin(0, 0, 0);
                actor_Dispz.SetOrientation(0, 90, 0);
                actor_Dispz.SetScale(scale);
            }


            actor_Suppx.SetPosition((-1 * scale) + point[0], point[1], point[2]);
            actor_Suppx.SetOrigin(0, 0, 0);
            actor_Suppx.SetOrientation(0, 0, 0);
            actor_Suppx.SetScale(scale);

            actor_Suppy.SetPosition(point[0], (-1 * scale) + point[1], point[2]);
            actor_Suppy.SetOrigin(0, 0, 0);
            actor_Suppy.SetOrientation(0, 0, 90);
            actor_Suppy.SetScale(scale);

            actor_Suppz.SetPosition(point[0], point[1], (-1 * scale) + point[2]);
            actor_Suppz.SetOrigin(0, 0, 0);
            actor_Suppz.SetOrientation(0, -90, 0);
            actor_Suppz.SetScale(scale);


            actor_Suppxx.SetPosition((-1 * scale) + point[0], point[1], point[2]);
            actor_Suppxx.SetOrigin(0, 0, 0);
            actor_Suppxx.SetOrientation(0, 0, 0);
            actor_Suppxx.SetScale(scale);

            actor_Suppyy.SetPosition(point[0], (-1 * scale) + point[1], point[2]);
            actor_Suppyy.SetOrigin(0, 0, 0);
            actor_Suppyy.SetOrientation(0, 0, 90);
            actor_Suppyy.SetScale(scale);

            actor_Suppzz.SetPosition(point[0], point[1], (-1 * scale) + point[2]);
            actor_Suppzz.SetOrigin(0, 0, 0);
            actor_Suppzz.SetOrientation(0, -90, 0);
            actor_Suppzz.SetScale(scale);

            
            actor_Loadx.GetProperty().SetColor(color_load[0], color_load[1], color_load[2]);
            actor_Loady.GetProperty().SetColor(color_load[0], color_load[1], color_load[2]);
            actor_Loadz.GetProperty().SetColor(color_load[0], color_load[1], color_load[2]);
            actor_Dispx.GetProperty().SetColor(color_displacement[0], color_displacement[1], color_displacement[2]);
            actor_Dispy.GetProperty().SetColor(color_displacement[0], color_displacement[1], color_displacement[2]);
            actor_Dispz.GetProperty().SetColor(color_displacement[0], color_displacement[1], color_displacement[2]);
            actor_Suppx.GetProperty().SetColor(color_support_d[0], color_support_d[1], color_support_d[2]);
            actor_Suppy.GetProperty().SetColor(color_support_d[0], color_support_d[1], color_support_d[2]);
            actor_Suppz.GetProperty().SetColor(color_support_d[0], color_support_d[1], color_support_d[2]);
            actor_Suppxx.GetProperty().SetColor(color_support_r[0], color_support_r[1], color_support_r[2]);
            actor_Suppyy.GetProperty().SetColor(color_support_r[0], color_support_r[1], color_support_r[2]);
            actor_Suppzz.GetProperty().SetColor(color_support_r[0], color_support_r[1], color_support_r[2]);


            actor_Suppx.GetProperty().SetRepresentationToWireframe();
            actor_Suppy.GetProperty().SetRepresentationToWireframe();
            actor_Suppz.GetProperty().SetRepresentationToWireframe();
            actor_Suppxx.GetProperty().SetRepresentationToWireframe();
            actor_Suppyy.GetProperty().SetRepresentationToWireframe();
            actor_Suppzz.GetProperty().SetRepresentationToWireframe();


            if (type == "load")
            {
                if (axe == "x")
                {
                    return actor_Loadx;
                }
                if (axe == "y")
                {
                    return actor_Loady;
                }
                if (axe == "z")
                {
                    return actor_Loadz;
                }
            }
            if (type == "disp")
            {
                if (axe == "x")
                {
                    return actor_Dispx;
                }
                if (axe == "y")
                {
                    return actor_Dispy;
                }
                if (axe == "z")
                {
                    return actor_Dispz;
                }
            }

            if (type == "supp_d")
            {
                if (axe == "x")
                {
                    return actor_Suppx;
                }
                if (axe == "y")
                {
                    return actor_Suppy;
                }
                if (axe == "z")
                {
                    return actor_Suppz;
                }
            }
            if (type == "supp_ro")
            {
                if (axe == "x")
                {
                    return actor_Suppxx;
                }
                if (axe == "y")
                {
                    return actor_Suppyy;
                }
                if (axe == "z")
                {
                    return actor_Suppzz;
                }
            }
            return null;
            //Renderer.ResetCamera();
        }

        public void Draw_objects(List<IList<string>> Point_list, List<IList<string>> ELEMENT_PROPERTIES, List<IList<string>> ELEMENT_NODES, string New_drawing)
        {

            if (New_drawing != "false")
            {
                Renderer.RemoveAllViewProps();
                leftcornertesxt();
                clear_drawing_items();
                Actor_Index_draw = 0;
            }

            if (Point_list.Count() == 0)
            {
                mainfrm.LogBox_add("The node list is empty!");
                return;
            }

            int nPts = Point_list.Count();
            double[,] p = new double[nPts, 3];
            for (int i = 0; i < nPts; i++)
            {
                p[i, 0] = double.Parse(Point_list[i][2]);
                p[i, 1] = double.Parse(Point_list[i][3]);
                p[i, 2] = double.Parse(Point_list[i][4]);
            }

            points = vtkPoints.New();
            //Console.WriteLine("ppppppppp:\n"+points.GetNumberOfPoint

            //int nPts = 8;
            //double[,] p = new double[,] {
            //   { 0.0, 0.0, 0.0 },
            //   { 1.0, 0.0, 0.0 },
            //   { 1.0, 1.0, 0.0 },s());
            for (int i = 0; i < nPts; i++)
                points.InsertNextPoint(p[i, 0], p[i, 1], p[i, 2]);
            //Console.WriteLine(points.GetNumberOfPoints());

            //   { 0.0, 1.0, 0.0 },
            //   { 0.0, 0.0, 1.0 },
            //   { 1.0, 0.0, 1.0 },
            //   { 1.0, 1.0, 1.0 },
            //   { 0.0, 1.0, 1.0 }
            //};


            //List<string> actornameList = new List<string>();
            //actornameList.Add("Nodes");
            //actornameList.Add("Polyline");
            //foreach (string item in actornameList)
            //    if (!actors.ContainsKey(item))
            //        actors.Add(item, vtkActor.New());

            //foreach (string item in actornameList)
            //Renderer.RemoveActor(actors[item]);

            // Create the points



            /*
                                  */
            //Actor_Index_draw--;

            int[] elementrange = new int[] { };


            //Console.WriteLine(points.GetNumberOfPoints());
            //Console.WriteLine(points.GetPoint(2)[0] +"," + points.GetPoint(2)[1] + "," + points.GetPoint(2)[2]);

            int[] pointNumber = new int[30];
            if (ELEMENT_PROPERTIES.Count > 0 && ELEMENT_NODES.Count > 0)
            {
                foreach (var item in ELEMENT_PROPERTIES)
                {

                    if (item.Count() > 5)
                    {
                        //"_CABLE_2D" "_CABLE_3D" "_EMB_CABLE_2D" "_EMB_CABLE_3D" "_TRUSS_2D" "_TRUSS_3D" "_FRAME_2D" "_FRAME_3D"
                        // "_PLANE_STRESS_QUAD" "_SOLID_HEXA" "_TIMOSHENKO_BEAM_2D" "_TIMOSHENKO_BEAM_3D" "_MINDLIN_SHELL_QUAD"
                        // "_MINDLIN_SLAB_QUAD" "_AHMAD_SHELL_QUAD"

                        elementrange = element_range(item[4]);

                        if (item[5] == "_SOLID_HEXA" || item[5] == "_SOLID_HEXA_THERMAL")
                        {
                            add_drawing_items();
                            //uGrid = vtkUnstructuredGrid.New();
                            uGrid[Actor_Index_draw].SetPoints(points);
                            for (int i = elementrange[0]; i <= elementrange[1]; i++)
                            {
                                //Console.WriteLine($"{i}    {ELEMENT_NODES[i - 1][1]}");
                                //if (Int32.Parse(ELEMENT_NODES[i - 1][1]) != i) { MainForm.displayErrorBox("The Number of Nodes are not in sequence or the element doesn't exist!!"); return; }
                                int n_node = Int32.Parse(ELEMENT_NODES[i - 1][2]);
                                if (n_node == 8)//8 nodes
                                {
                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][4]);
                                    pointNumber[2] = Int32.Parse(ELEMENT_NODES[i - 1][5]);
                                    pointNumber[3] = Int32.Parse(ELEMENT_NODES[i - 1][6]);
                                    pointNumber[4] = Int32.Parse(ELEMENT_NODES[i - 1][7]);
                                    pointNumber[5] = Int32.Parse(ELEMENT_NODES[i - 1][8]);
                                    pointNumber[6] = Int32.Parse(ELEMENT_NODES[i - 1][9]);
                                    pointNumber[7] = Int32.Parse(ELEMENT_NODES[i - 1][10]);

                                    draw_hex(new int[8] { pointNumber[0] - 1, pointNumber[1] - 1, pointNumber[2] - 1, pointNumber[3] - 1, pointNumber[4] - 1, pointNumber[5] - 1, pointNumber[6] - 1, pointNumber[7] - 1 }, points, Actor_Index_draw);

                                }
                                else if (n_node == 20)//20 nodes
                                {
                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][0 + 3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][2 + 3]);
                                    pointNumber[2] = Int32.Parse(ELEMENT_NODES[i - 1][4 + 3]);
                                    pointNumber[3] = Int32.Parse(ELEMENT_NODES[i - 1][6 + 3]);
                                    pointNumber[4] = Int32.Parse(ELEMENT_NODES[i - 1][12 + 3]);
                                    pointNumber[5] = Int32.Parse(ELEMENT_NODES[i - 1][14 + 3]);
                                    pointNumber[6] = Int32.Parse(ELEMENT_NODES[i - 1][16 + 3]);
                                    pointNumber[7] = Int32.Parse(ELEMENT_NODES[i - 1][18 + 3]);

                                    pointNumber[8] = Int32.Parse(ELEMENT_NODES[i - 1][1 + 3]);
                                    pointNumber[9] = Int32.Parse(ELEMENT_NODES[i - 1][3 + 3]);
                                    pointNumber[10] = Int32.Parse(ELEMENT_NODES[i - 1][5 + 3]);
                                    pointNumber[11] = Int32.Parse(ELEMENT_NODES[i - 1][7 + 3]);
                                    pointNumber[12] = Int32.Parse(ELEMENT_NODES[i - 1][13 + 3]);
                                    pointNumber[13] = Int32.Parse(ELEMENT_NODES[i - 1][15 + 3]);
                                    pointNumber[14] = Int32.Parse(ELEMENT_NODES[i - 1][17 + 3]);
                                    pointNumber[15] = Int32.Parse(ELEMENT_NODES[i - 1][19 + 3]);
                                    pointNumber[16] = Int32.Parse(ELEMENT_NODES[i - 1][8 + 3]);
                                    pointNumber[17] = Int32.Parse(ELEMENT_NODES[i - 1][9 + 3]);
                                    pointNumber[18] = Int32.Parse(ELEMENT_NODES[i - 1][10 + 3]);
                                    pointNumber[19] = Int32.Parse(ELEMENT_NODES[i - 1][11 + 3]);

                                    int[] list_point = new int[20];
                                    for (int i1 = 0; i1 < 20; i1++)
                                    {
                                        list_point[i1] = pointNumber[i1] - 1;
                                    }
                                    draw_quadratic_hex(list_point, points, Actor_Index_draw);
                                }
                                else { MainForm.displayErrorBox($"The mesh type is not supported by FEMIX or you have selected the wrong element type[{item[5]}]!\nChange mesh type and try again."); break; }
                            }
                            C_variables.Layer_items[item[2]].Add(Actor_Index_draw);
                            C_variables.Layer_items["3D"].Add(Actor_Index_draw);
                            draw_hex_mapper(Actor_Index_draw);
                            cell_label(Actor_Index_draw, (elementrange[1] - elementrange[0]) + 1, uGrid[Actor_Index_draw], elementrange[0]);
                            Actor_Index_draw++;
                        }

                        /////tetra
                        if (item[5] == "_SOLID_TETRA")
                        {
                            add_drawing_items();
                            //uGrid = vtkUnstructuredGrid.New();
                            uGrid[Actor_Index_draw].SetPoints(points);
                            for (int i = elementrange[0]; i <= elementrange[1]; i++)
                            {
                                //Console.WriteLine($"{i}    {ELEMENT_NODES[i - 1][1]}");
                                //if (Int32.Parse(ELEMENT_NODES[i - 1][1]) != i) { MainForm.displayErrorBox("The Number of Nodes are not in sequence or the element doesn't exist!!"); return; }
                                int n_node = Int32.Parse(ELEMENT_NODES[i - 1][2]);
                                if (n_node == 4)//4 nodes
                                {
                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][4]);
                                    pointNumber[2] = Int32.Parse(ELEMENT_NODES[i - 1][5]);
                                    pointNumber[3] = Int32.Parse(ELEMENT_NODES[i - 1][6]);

                                    draw_tet(new int[4] { pointNumber[0] - 1, pointNumber[1] - 1, pointNumber[2] - 1, pointNumber[3] - 1 }, points, Actor_Index_draw);

                                }
                                else if (n_node == 10)//10 nodes
                                {
                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][0 + 3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][2 + 3]);
                                    pointNumber[2] = Int32.Parse(ELEMENT_NODES[i - 1][4 + 3]);
                                    pointNumber[3] = Int32.Parse(ELEMENT_NODES[i - 1][9 + 3]);
                                    pointNumber[4] = Int32.Parse(ELEMENT_NODES[i - 1][1 + 3]);
                                    pointNumber[5] = Int32.Parse(ELEMENT_NODES[i - 1][3 + 3]);
                                    pointNumber[6] = Int32.Parse(ELEMENT_NODES[i - 1][5 + 3]);
                                    pointNumber[7] = Int32.Parse(ELEMENT_NODES[i - 1][6 + 3]);
                                    pointNumber[8] = Int32.Parse(ELEMENT_NODES[i - 1][7 + 3]);
                                    pointNumber[9] = Int32.Parse(ELEMENT_NODES[i - 1][8 + 3]);
   

                                    int[] list_point = new int[10];
                                    for (int i1 = 0; i1 < 10; i1++)
                                    {
                                        list_point[i1] = pointNumber[i1] - 1;
                                    }
                                    draw_quadratic_tet(list_point, points, Actor_Index_draw);
                                }
                                else { MainForm.displayErrorBox($"The mesh type is not supported by FEMIX or you have selected the wrong element type[{item[5]}]!\nChange mesh type and try again."); break; }
                            }
                            C_variables.Layer_items[item[2]].Add(Actor_Index_draw);
                            C_variables.Layer_items["3D"].Add(Actor_Index_draw);
                            draw_hex_mapper(Actor_Index_draw);
                            cell_label(Actor_Index_draw, (elementrange[1] - elementrange[0]) + 1, uGrid[Actor_Index_draw], elementrange[0]);
                            Actor_Index_draw++;
                        }
                        /////
                    }
                }


                foreach (var item in ELEMENT_PROPERTIES)
                {
                    if (item.Count() > 5)
                    {
                        elementrange = element_range(item[4]);

                        if (item[5] == "_PLANE_STRESS_QUAD" || item[5] == "_PLANE_STRAIN_QUAD" || item[5] == "_PLANE_STRESS_QUAD_THERMAL"  || item[5] == "_MINDLIN_SHELL_QUAD" || 
                            item[5] == "_MINDLIN_SLAB_QUAD" || item[5] == "_AHMAD_SHELL_QUAD" || item[5] == "_INTERFACE_QUAD" || item[5] == "_AXIS_QUAD")
                        {
                            //uGrid = vtkUnstructuredGrid.New();
                            //uGrid.Add(vtkUnstructuredGrid.New());
                            add_drawing_items();
                            uGrid[Actor_Index_draw].SetPoints(points);
                            for (int i = elementrange[0]; i <= elementrange[1]; i++)
                            {
                                //Console.WriteLine($"{i}    {ELEMENT_NODES[i - 1][1]}");
                                //if (Int32.Parse(ELEMENT_NODES[i - 1][1]) != i) { MainForm.displayErrorBox("The Number of Nodes are not in sequence or the element doesn't exist!!"); return; }
                                int n_node = Int32.Parse(ELEMENT_NODES[i - 1][2]);
                                if (item[5].Contains("_INTERFACE"))
                                    n_node = n_node / 2;
                                if (n_node == 4)//4 nodes
                                {
                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][0+3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][1+3]);
                                    pointNumber[2] = Int32.Parse(ELEMENT_NODES[i - 1][2+3]);
                                    pointNumber[3] = Int32.Parse(ELEMENT_NODES[i - 1][3+3]);

                                    draw_quad(new int[4] { pointNumber[0] - 1, pointNumber[1] - 1, pointNumber[2] - 1, pointNumber[3] - 1 }, points, Actor_Index_draw);

                                }
                                else if (n_node == 8)//8 nodes
                                {
                                    int[] list_point = new int[8];
                                    //for (int i1 = 0; i1 < 8; i1++)
                                    //{
                                    //    pointNumber[i1] = Int32.Parse(ELEMENT_NODES[i - 1][3 + i1]);
                                    //    list_point[i1] = pointNumber[i1] - 1;
                                    //}
                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][0 + 3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][2 + 3]);
                                    pointNumber[2] = Int32.Parse(ELEMENT_NODES[i - 1][4 + 3]);
                                    pointNumber[3] = Int32.Parse(ELEMENT_NODES[i - 1][6 + 3]);
                                    pointNumber[4] = Int32.Parse(ELEMENT_NODES[i - 1][1 + 3]);
                                    pointNumber[5] = Int32.Parse(ELEMENT_NODES[i - 1][3 + 3]);
                                    pointNumber[6] = Int32.Parse(ELEMENT_NODES[i - 1][5 + 3]);
                                    pointNumber[7] = Int32.Parse(ELEMENT_NODES[i - 1][7 + 3]);

                                    for (int i1 = 0; i1 < 8; i1++)
                                    {
                                        list_point[i1] = pointNumber[i1] - 1;
                                    }
                                    draw_quadratic_quad(list_point, points, Actor_Index_draw);
                                    //draw_quad(list_point, points, Actor_Index_draw);

                                }
                                else if (n_node == 9)//9 nodes
                                {
                                    int[] list_point = new int[9];
                                    //for (int i1 = 0; i1 < 9; i1++)
                                    //{
                                    //    pointNumber[i1] = Int32.Parse(ELEMENT_NODES[i - 1][3 + i1]);
                                    //    list_point[i1] = pointNumber[i1] - 1;
                                    //}
                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][0 + 3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][2 + 3]);
                                    pointNumber[2] = Int32.Parse(ELEMENT_NODES[i - 1][4 + 3]);
                                    pointNumber[3] = Int32.Parse(ELEMENT_NODES[i - 1][6 + 3]);
                                    pointNumber[4] = Int32.Parse(ELEMENT_NODES[i - 1][1 + 3]);
                                    pointNumber[5] = Int32.Parse(ELEMENT_NODES[i - 1][3 + 3]);
                                    pointNumber[6] = Int32.Parse(ELEMENT_NODES[i - 1][5 + 3]);
                                    pointNumber[7] = Int32.Parse(ELEMENT_NODES[i - 1][7 + 3]);
                                    pointNumber[8] = Int32.Parse(ELEMENT_NODES[i - 1][8 + 3]);
                                    for (int i1 = 0; i1 < 9; i1++)
                                    {
                                        list_point[i1] = pointNumber[i1] - 1;
                                    }
                                    draw_Bi_quadratic_quad(list_point, points, Actor_Index_draw);
                                }
                                else { MainForm.displayErrorBox($"The mesh type is not supported by FEMIX or you have selected the wrong element type[{item[5]}]!\nChange mesh type and try again."); break; }
                            }
                            C_variables.Layer_items[item[2]].Add(Actor_Index_draw);
                            C_variables.Layer_items["2D"].Add(Actor_Index_draw);
                            draw_quad_mapper(Actor_Index_draw, item[5]);
                            cell_label(Actor_Index_draw, (elementrange[1] - elementrange[0]) + 1, uGrid[Actor_Index_draw], elementrange[0]);
                            Actor_Index_draw++;
                        }
                        ///////////tri
                        if (item[5] == "_PLANE_STRESS_TRIANG" || item[5] == "_PLANE_STRAIN_TRIANG" || item[5] == "_MINDLIN_SLAB_TRIANG" || item[5] == "_MINDLIN_SHELL_TRIANG" || 
                            item[5] == "_AHMAD_SHELL_TRIANG" || item[5] == "_INTERFACE_TRIANG" || item[5] == "_AXIS_TRIANG")
                        {
                            //uGrid = vtkUnstructuredGrid.New();
                            //uGrid.Add(vtkUnstructuredGrid.New());
                            add_drawing_items();
                            uGrid[Actor_Index_draw].SetPoints(points);
                            for (int i = elementrange[0]; i <= elementrange[1]; i++)
                            {
                                //Console.WriteLine($"{i}    {ELEMENT_NODES[i - 1][1]}");
                                //if (Int32.Parse(ELEMENT_NODES[i - 1][1]) != i) { MainForm.displayErrorBox("The Number of Nodes are not in sequence or the element doesn't exist!!"); return; }
                                int n_node = Int32.Parse(ELEMENT_NODES[i - 1][2]);
                                if (item[5].Contains("_INTERFACE"))
                                    n_node = n_node / 2;
                                if (n_node == 3)//3 nodes
                                {
                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][0 + 3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][1 + 3]);
                                    pointNumber[2] = Int32.Parse(ELEMENT_NODES[i - 1][2 + 3]);

                                    draw_triangle(new int[3] { pointNumber[0] - 1, pointNumber[1] - 1, pointNumber[2] - 1 }, points, Actor_Index_draw);

                                }
                                else if (n_node == 6)//6 nodes
                                {

                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][0 + 3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][2 + 3]);
                                    pointNumber[2] = Int32.Parse(ELEMENT_NODES[i - 1][4 + 3]);
                                    pointNumber[3] = Int32.Parse(ELEMENT_NODES[i - 1][1 + 3]);
                                    pointNumber[4] = Int32.Parse(ELEMENT_NODES[i - 1][3 + 3]);
                                    pointNumber[5] = Int32.Parse(ELEMENT_NODES[i - 1][5 + 3]);
                                    
                                    int[] list_point = new int[6];
                                    for (int i1 = 0; i1 < 6; i1++)
                                    {
                                        list_point[i1] = pointNumber[i1] - 1;
                                    }
                                    draw_quadratic_triangle(list_point, points, Actor_Index_draw);
                                    //draw_quad(list_point, points, Actor_Index_draw);

                                }
                                else { MainForm.displayErrorBox($"The mesh type is not supported by FEMIX or you have selected the wrong element type[{item[5]}]!\nChange mesh type and try again."); break; }
                            }
                            C_variables.Layer_items[item[2]].Add(Actor_Index_draw);
                            C_variables.Layer_items["2D"].Add(Actor_Index_draw);
                            draw_quad_mapper(Actor_Index_draw, item[5]);
                            cell_label(Actor_Index_draw, (elementrange[1] - elementrange[0]) + 1, uGrid[Actor_Index_draw], elementrange[0]);
                            Actor_Index_draw++;
                        }
                        ///////////
                    }

                }

                foreach (var item in ELEMENT_PROPERTIES)
                {
                    if (item.Count() > 5)
                    {

                        elementrange = element_range(item[4]);

                        if (item[5] == "_TRUSS_2D" || item[5] == "_TRUSS_3D" || item[5] == "_TIMOSHENKO_BEAM_2D" || item[5] == "_TIMOSHENKO_BEAM_3D" ||
                            item[5] == "_CABLE_2D" || item[5] == "_CABLE_3D" || item[5] == "_EMB_CABLE_2D" || item[5] == "_EMB_CABLE_3D" ||
                            item[5] == "_FRAME_2D" || item[5] == "_FRAME_3D" || item[5] == "_AXIS_LINE" || item[5] == "_INTERFACE_LINE_2D" ||
                            item[5] == "_INTERFACE_LINE_3D")
                        {
                            //uGrid = vtkUnstructuredGrid.New();
                            //uGrid.Add(vtkUnstructuredGrid.New());
                            add_drawing_items();

                            uGrid[Actor_Index_draw].SetPoints(points);
                            for (int i = elementrange[0]; i <= elementrange[1]; i++)
                            {
                                //Console.WriteLine($"{i}    {ELEMENT_NODES[i-1][1]}");
                                //if (Int32.Parse(ELEMENT_NODES[i - 1][1]) != i) { MainForm.displayErrorBox("The Number of Nodes are not in sequence or the element doesn't exist!!"); return; }
                                int n_node = Int32.Parse(ELEMENT_NODES[i - 1][2]);
                                if (item[5].Contains("_INTERFACE"))
                                    n_node = n_node / 2;
                                if (n_node == 2)//2 nodes
                                {
                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][0+3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][1+3]);

                                    draw_line(pointNumber[0] - 1, pointNumber[1] - 1, points, Actor_Index_draw);

                                }
                                else if (n_node == 3)//3 nodes
                                {
                                    int[] list_point = new int[3];
                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][0 + 3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][2 + 3]);
                                    pointNumber[2] = Int32.Parse(ELEMENT_NODES[i - 1][1 + 3]);

                                    for (int i1 = 0; i1 < 3; i1++)
                                    {
                                        list_point[i1] = pointNumber[i1] - 1;
                                    }
                                    draw_quadratic_Line(list_point, points, Actor_Index_draw);
                                }
                                else { MainForm.displayErrorBox($"The mesh type is not supported by FEMIX or you have selected the wrong element type[{item[5]}]!\nChange mesh type and try again."); break; }
                            }
                            C_variables.Layer_items[item[2]].Add(Actor_Index_draw);
                            C_variables.Layer_items["1D"].Add(Actor_Index_draw);

                            draw_line_mapper(Actor_Index_draw, item[5]);
                            cell_label(Actor_Index_draw, (elementrange[1] - elementrange[0]) + 1, uGrid[Actor_Index_draw], elementrange[0]);
                            Actor_Index_draw++;
                        }
                    }
                }


                foreach (var item in ELEMENT_PROPERTIES)
                {
                    if (item.Count() > 5)
                    {

                        elementrange = element_range(item[4]);

                        if (item[5] == "_INTERFACE_POINT")
                        {
                            //MainForm.displayErrorBox($"The mesh type is not supported by FEMIX or you have selected the wrong element type[{item[5]}]!\nChange mesh type and try again.");
                            add_drawing_items();
                            uGrid[Actor_Index_draw].SetPoints(points);
                            for (int i = elementrange[0]; i <= elementrange[1]; i++)
                            {
                                //Console.WriteLine($"{i}    {ELEMENT_NODES[i-1][1]}");
                                if (Int32.Parse(ELEMENT_NODES[i - 1][2]) == 2)//1 nodes
                                {
                                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i - 1][3]);
                                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i - 1][4]);

                                    draw_interface_point(pointNumber[0] - 1, points, Actor_Index_draw);

                                }
                                else { MainForm.displayErrorBox($"The mesh type is not supported by FEMIX or you have selected the wrong element type[{item[5]}]!\nChange mesh type and try again."); break; }
                            }
                            C_variables.Layer_items[item[2]].Add(Actor_Index_draw);
                            C_variables.Layer_items["1D"].Add(Actor_Index_draw);

                            draw_interface_point_mapper(Actor_Index_draw);
                            cell_label(Actor_Index_draw, (elementrange[1] - elementrange[0]) + 1, uGrid[Actor_Index_draw], elementrange[0]);
                            Actor_Index_draw++;
                        }
                    }
                }


            }

            //Console.WriteLine(uGrid[2].GetCell(0) + " ");
            //Console.WriteLine(uGrid.GetCell(0).GetCellType() + " ");
            //Console.WriteLine(uGrid.GetCell(0).GetFace(0) + " ");


            ////------>index=point_number -1
            //draw_hex(new int[8] { 0, 1, 2, 3,4,5,6,7 }, points, Actor_Index_draw);
            //Actor_Index_draw++;
            //draw_quad(new int[4] { 0, 1, 2, 3 }, points, Actor_Index_draw);
            //Actor_Index_draw++;
            //draw_polyline(0, 2, points, Actor_Index_draw); 
            //Actor_Index_draw++;
            //draw_line(0, 6, points, Actor_Index_draw); 
            //Actor_Index_draw++;
            //draw_vertices(nPts, points, Actor_Index_draw);
            //Actor_Index_draw++;            


            if (New_drawing == "true" || New_drawing == "false")
            {
                C_variables.Layer_items["Nodes"].Add(Actor_Index_draw);
                draw_vertices(nPts, points, Actor_Index_draw, 1);
                actors[Actor_Index_draw].VisibilityOff();
                Actor_Index_draw++;
            }

           
           

            //actors[1].VisibilityOff();

            //Renderer.ResetCamera();
            //Refresh();
        }
        private void cell_label(int Actor_Index_draw, int n_cells, vtkUnstructuredGrid input_ugrid, int startID)
        {
            // Label the points
            vtkIntArray pointValues = vtkIntArray.New();
            pointValues.SetName("CellIDs");
            for (int i = 0; i < n_cells; i++)
                pointValues.InsertValue(i, i + startID);//New Label
            input_ugrid.GetCellData().AddArray(pointValues);

            vtkIdFilter idFilter = vtkIdFilter.New();

#if VTK_MAJOR_VERSION_5
idFilter.SetInput(input_ugrid);
#else
      idFilter.SetInputData(input_ugrid);      
#endif

            idFilter.SetIdsArrayName("CellIDs");
            idFilter.PointIdsOn();
            idFilter.CellIdsOn();
            idFilter.Update();


            //vtkPointSetToLabelHierarchy idFilter = vtkPointSetToLabelHierarchy.New();
            //idFilter.SetInput(uGrid);
            //idFilter.SetMaximumDepth(15);
            //idFilter.SetTargetLabelCount(100);
            //idFilter.SetLabelArrayName("VertexIDs");
            //idFilter.Update();

            vtkCellCenters cellcenter = vtkCellCenters.New();
            cellcenter.SetInputConnection(idFilter.GetOutputPort());

            //vtkDynamic2DLabelMapper labelMapper = vtkDynamic2DLabelMapper.New();
            vtkLabeledDataMapper labelMapper = vtkLabeledDataMapper.New();
            labelMapper.SetInputConnection(cellcenter.GetOutputPort());
            labelMapper.SetLabelModeToLabelFieldData();
            labelMapper.GetLabelTextProperty().SetColor(255/255.0, 228 / 255.0, 200 / 255.0);
            labelMapper.GetLabelTextProperty().SetJustificationToCentered();
            //labelMapper.SetInput(uGrid);

            //vtkLabelPlacementMapper labelMapper = vtkLabelPlacementMapper.New();
            //labelMapper.SetInputConnection(idFilter.GetOutputPort());
            //vtkFreeTypeLabelRenderStrategy strategy = new vtkFreeTypeLabelRenderStrategy();
            //labelMapper.SetRenderStrategy(strategy);
            //labelMapper.UseDepthBufferOn();
            //labelMapper.SetShapeToNone();
            //labelMapper.SetStyleToOutline();
            //labelMapper.UseUnicodeStringsOff();
            labelactors[Actor_Index_draw].SetMapper(labelMapper);
            labelactors[Actor_Index_draw].VisibilityOff();
            Renderer.AddActor(labelactors[Actor_Index_draw]);
        }
        private void draw_vertices(int numberOfVertices, vtkPoints points, int Actor_Index_draw,int startID)
        {

            vtkPolyVertex polyvertex = vtkPolyVertex.New();
            polyvertex.GetPointIds().SetNumberOfIds(numberOfVertices);
            for (int i = 0; i < numberOfVertices; i++)
                polyvertex.GetPointIds().SetId(i, i);


            // Create one text property for all
            vtkTextProperty textProperty = vtkTextProperty.New();
            textProperty.SetFontSize(10);
            textProperty.SetJustificationToCentered();

            add_drawing_items();

            //vtkUnstructuredGrid uGrid1 = vtkUnstructuredGrid.New();
            uGrid[Actor_Index_draw].SetPoints(points);

            uGrid[Actor_Index_draw].InsertNextCell(polyvertex.GetCellType(), polyvertex.GetPointIds());


            //Console.WriteLine(uGrid.GetCell(0) + " ");
            //Console.WriteLine(uGrid.GetCell(0).GetCellType() + " ");
            //Console.WriteLine(uGrid.GetCell(0).GetFace(0) + " ");





            // Label the points
            vtkIntArray pointValues = vtkIntArray.New();
            pointValues.SetName("VertexIDs");
            for (int i = 0; i < numberOfVertices; i++)
                pointValues.InsertValue(i, i+ startID);//New Label
            uGrid[Actor_Index_draw].GetPointData().AddArray(pointValues);

            vtkIdFilter idFilter = vtkIdFilter.New();
#if VTK_MAJOR_VERSION_5
idFilter.SetInput(uGrid[Actor_Index_draw]);
#else
            idFilter.SetInputData(uGrid[Actor_Index_draw]);
#endif
            idFilter.SetIdsArrayName("VertexIDs");
            idFilter.PointIdsOn();
            idFilter.CellIdsOn();
            idFilter.Update();


            //vtkPointSetToLabelHierarchy idFilter = vtkPointSetToLabelHierarchy.New();
            //idFilter.SetInput(uGrid);
            //idFilter.SetMaximumDepth(15);
            //idFilter.SetTargetLabelCount(100);
            //idFilter.SetLabelArrayName("VertexIDs");
            //idFilter.Update();

            vtkLabeledDataMapper labelMapper = vtkLabeledDataMapper.New();
            //vtkDynamic2DLabelMapper labelMapper = vtkDynamic2DLabelMapper.New();
            labelMapper.SetInputConnection(idFilter.GetOutputPort());
            labelMapper.SetLabelModeToLabelFieldData();

            //labelMapper.SetInput(uGrid);

            //vtkLabelPlacementMapper labelMapper = vtkLabelPlacementMapper.New();
            //labelMapper.SetInputConnection(idFilter.GetOutputPort());
            //vtkFreeTypeLabelRenderStrategy strategy = new vtkFreeTypeLabelRenderStrategy();
            //labelMapper.SetRenderStrategy(strategy);
            //labelMapper.UseDepthBufferOn();
            //labelMapper.SetShapeToNone();
            //labelMapper.SetStyleToOutline();
            //labelMapper.UseUnicodeStringsOff();




            labelactors[Actor_Index_draw].SetMapper(labelMapper);
            labelactors[Actor_Index_draw].VisibilityOff();
            Renderer.AddActor(labelactors[Actor_Index_draw]);

            // Visualize
            //vtkDataSetMapper mapper = vtkDataSetMapper.New();
            //mapper.SetInput(uGrid);

#if VTK_MAJOR_VERSION_5
mapper[Actor_Index_draw].SetInput(uGrid[Actor_Index_draw]);
#else            
              mapper[Actor_Index_draw].SetInputData(uGrid[Actor_Index_draw]);
#endif

            //Renderer.RemoveActor(actors[rendererName]);

            actors[Actor_Index_draw].SetMapper(mapper[Actor_Index_draw]);
            actors[Actor_Index_draw].GetProperty().SetPointSize(3);

            actors[Actor_Index_draw].GetProperty().SetColor(color_0d[0], color_0d[1], color_0d[2]);
            Renderer.AddActor(actors[Actor_Index_draw]);

        }

        public void clear_point_cell_data()
        {
            for (int i = 0; i < uGrid.Count; i++)
            {
                uGrid[i].GetPointData().SetActiveScalars("");
                uGrid[i].GetCellData().SetActiveScalars("");
#if VTK_MAJOR_VERSION_5
//uGrid[i].Update();
#else
#endif
            }

            for (int i = 0; i < mapper.Count; i++)//uGrid
            {
                mapper[i].ScalarVisibilityOff();
                //mapper[i].Update();
            }
            //Refresh();
        }

        public void Draw_cracks(List<IList<string>> Point_list, List<IList<string>> ELEMENT_NODES, string crack_type,string width_scale,string length_scale)
        {

            
            //Actor_Index_draw = uGrid.Count();
            //Actor_Index_draw = 0;

            double[] color = new double[3];
            switch (crack_type)
            {
                case "_INITIATING":
                    color[0] = 0; color[1] = 0; color[2] = 0;
                    break;
                case "_OPENING":
                    color[0] = 240; color[1] = 10; color[2] = 25;
                    break;
                case "_CLOSING":
                    color[0] = 0; color[1] = 225; color[2] = 25;
                    break;
                case "_CLOSED":
                    color[0] = 0; color[1] = 40; color[2] = 225;
                    break;
                case "_REOPENING":
                    color[0] = 77; color[1] = 241; color[2] = 250;
                    break;
                case "_FULLY_OPEN":
                    color[0] = 223; color[1] = 13; color[2] = 255;
                    break;
            }

            if (Point_list.Count() == 0)
            {
                //there is nothing point to draw
                return;
            }
            int nPts = Point_list.Count();
            double[,] p = new double[nPts, 3];
            for (int i = 0; i < nPts; i++)
            {
                p[i, 0] = double.Parse(Point_list[i][1]);
                p[i, 1] = double.Parse(Point_list[i][2]);
                p[i, 2] = double.Parse(Point_list[i][3]);
                //Console.WriteLine($"{ p[i, 0] } , { p[i, 1] } , { p[i, 2] }");
            }


            ///////////////////Scale
            double scale1 = double.Parse(width_scale);
            double scale2 = double.Parse(length_scale);

            double scale = 1;
            double lenght = 0;
            for (int i = 0; i < ELEMENT_NODES.Count(); i++)
            {

                int[] firstpointid = new int[] { 2, 4, 3, 5 };
                foreach (int item in firstpointid)
                {
                    if (item == 2 || item == 4)
                        scale = scale1;
                    if (item == 3 || item == 5)
                        scale = scale2;

                    int i_1 = Int32.Parse(ELEMENT_NODES[i][item]) - 1;
                    int i_2 = Int32.Parse(ELEMENT_NODES[i][item + 1]) - 1;

                    Point3D P_s = new Point3D(p[i_1, 0], p[i_1, 1], p[i_1, 2]);
                    Point3D P_e = new Point3D(p[i_2, 0], p[i_2, 1], p[i_2, 2]);

                    Vector3D V_s = new Vector3D(P_s.X, P_s.Y, P_s.Z);
                    Vector3D V_e = new Vector3D(P_e.X, P_e.Y, P_e.Z);

                    lenght = intersection.Distance_between_points(P_s, P_e);
                    Vector3D linevector = new Vector3D().NewVectorfrom2points(P_s, P_e);
                    Vector3D normalvector = linevector.Normalize();
                    Vector3D newvector = (normalvector * (0.5 * (scale-1) * lenght));

                    V_s = V_s - newvector;
                    V_e = V_e + newvector;
                    P_s = V_s.vectorXYZ();
                    P_e = V_e.vectorXYZ();

                    p[i_1, 0] = P_s.X; p[i_1, 1] = P_s.Y; p[i_1, 2] = P_s.Z;
                    p[i_2, 0] = P_e.X; p[i_2, 1] = P_e.Y; p[i_2, 2] = P_e.Z;
                }
            }


            ///////////////////end Scale




            vtkPoints crack_points = vtkPoints.New();
            for (int i = 0; i < nPts; i++)
                crack_points.InsertNextPoint(p[i, 0], p[i, 1], p[i, 2]);
            //draw cracks

            //vtkUnstructuredGrid uGrid = vtkUnstructuredGrid.New();

            add_drawing_items();


            uGrid[Actor_Index_draw].SetPoints(crack_points);



            int[] pointNumber = new int[5];
            for (int i = 0; i < ELEMENT_NODES.Count(); i++)
            {

                if (Int32.Parse(ELEMENT_NODES[i][1]) == 5)//5 nodes
                {
                    pointNumber[0] = Int32.Parse(ELEMENT_NODES[i][2]) - 1;
                    pointNumber[1] = Int32.Parse(ELEMENT_NODES[i][3]) - 1;
                    pointNumber[2] = Int32.Parse(ELEMENT_NODES[i][4]) - 1;
                    pointNumber[3] = Int32.Parse(ELEMENT_NODES[i][5]) - 1;
                    pointNumber[4] = Int32.Parse(ELEMENT_NODES[i][6]) - 1;



                    //Console.WriteLine($"{pointNumber[0]} ,{pointNumber[1]} ,{pointNumber[2]}, {pointNumber[3]}, {pointNumber[4]}");

                    int numberOfVertices = 5;
                    vtkPolyLine polyline = vtkPolyLine.New();
                    polyline.GetPointIds().SetNumberOfIds(numberOfVertices);
                    for (int i1 = 0; i1 < numberOfVertices; i1++)
                        polyline.GetPointIds().SetId(i1, pointNumber[i1]);
                    //uGrid[Actor_Index_draw].InsertNextCell(polyline.GetCellType(), polyline.GetPointIds());
                    uGrid[Actor_Index_draw].InsertNextCell(polyline.GetCellType(), polyline.GetPointIds());

                }
                else
                {
                    mainfrm.LogBox_add("Error on crack type!!!");
                }
            }
            //C_variables.Layer_items[crack_type].Add(Actor_Index_draw);
            //C_variables.Layer_items["2D"].Add(Actor_Index_draw);




            //mapper[Actor_Index_draw].SetInput(uGrid[Actor_Index_draw]);

#if VTK_MAJOR_VERSION_5
mapper[Actor_Index_draw].SetInput(uGrid[Actor_Index_draw]);
#else
        mapper[Actor_Index_draw].SetInputData(uGrid[Actor_Index_draw]);   
#endif


            actors[Actor_Index_draw].SetMapper(mapper[Actor_Index_draw]);
            actors[Actor_Index_draw].GetProperty().SetLineWidth(1);

            actors[Actor_Index_draw].GetProperty().SetColor(color[0] / 255.0, color[1] / 255.0, color[2] / 255.0);
            Renderer.AddActor(actors[Actor_Index_draw]);

            C_variables.Layer_items[crack_type].Add(Actor_Index_draw);

            //Renderer.AddViewProp(actors[Actor_Index_draw]);

            Actor_Index_draw++;


        }
        public void Draw_cracks_legend()
        {

            vtkLegendBoxActor legend = vtkLegendBoxActor.New();
            legend.SetNumberOfEntries(6);

            vtkCubeSource legendBox = vtkCubeSource.New();
            legendBox.Update();

            int i = 0;
            double[] color = new double[3];

            i = 0;
            legend.SetEntrySymbol(i, legendBox.GetOutput());
            legend.SetEntryString(i, "Initiating");
            color[0] = 0; color[1] = 0; color[2] = 0;
            legend.SetEntryColor(i, color[0]/ 255.0 , color[1]/ 255.0 , color[2]/ 255.0 );
            i = 1;
            legend.SetEntrySymbol(i, legendBox.GetOutput());
            legend.SetEntryString(i, "Opening");
            color[0] = 240; color[1] = 10; color[2] = 25;
            legend.SetEntryColor(i, color[0] / 255.0, color[1] / 255.0, color[2] / 255.0);
            i = 2;
            legend.SetEntrySymbol(i, legendBox.GetOutput());
            legend.SetEntryString(i, "Closing");
            color[0] = 0; color[1] = 225; color[2] = 25;
            legend.SetEntryColor(i, color[0] / 255.0, color[1] / 255.0, color[2] / 255.0);
            i = 3;
            legend.SetEntrySymbol(i, legendBox.GetOutput());
            legend.SetEntryString(i, "Closed");
            color[0] = 0; color[1] = 40; color[2] = 225;
            legend.SetEntryColor(i, color[0] / 255.0, color[1] / 255.0, color[2] / 255.0);
            i = 4;
            legend.SetEntrySymbol(i, legendBox.GetOutput());
            legend.SetEntryString(i, "Reopening");
            color[0] = 77; color[1] = 241; color[2] = 250;
            legend.SetEntryColor(i, color[0] / 255.0, color[1] / 255.0, color[2] / 255.0);
            i = 5;
            legend.SetEntrySymbol(i, legendBox.GetOutput());
            legend.SetEntryString(i, "Fully open");
            color[0] = 223; color[1] = 13; color[2] = 255;
            legend.SetEntryColor(i, color[0] / 255.0, color[1] / 255.0, color[2] / 255.0);

            //legend.GetProperty().SetColor(0.7, 0.2, 0.1);

            //vtkTextProperty textProp = new vtkTextProperty();
            //textProp.SetFontSize(8);
            //textProp.SetColor(1.0, 0.5, 0.5);
            //textProp.SetFontFamilyToArial();
            //legend.SetEntryTextProperty(textProp);

            // place legend in lower right
            legend.GetPositionCoordinate().SetCoordinateSystemToView();
            legend.GetPositionCoordinate().SetValue(0.75, -0.95);

            legend.GetPosition2Coordinate().SetCoordinateSystemToView();
            legend.GetPosition2Coordinate().SetValue(1.2, -0.55);

            legend.UseBackgroundOn();
            legend.SetBackgroundOpacity(0.8);
            legend.SetBackgroundColor(250/255.0, 250/255.0, 250/255.0);

            //legend.SetBorder(0);
            //legend.SetHeight(0.2);

            Renderer.AddActor(legend);

        }

        public void apply_ResampleToImage_filter( double[] BoundaryRange)
        {


#if VTK_MAJOR_VERSION_5

#else
            double dim_x, dim_y, dim_z;
            dim_x =Math.Abs( BoundaryRange[0] - BoundaryRange[1]);
            dim_y = Math.Abs(BoundaryRange[2] - BoundaryRange[3]);
            dim_z = Math.Abs(BoundaryRange[4] - BoundaryRange[5]);
            if (dim_x == 0) dim_x = 1;
            if (dim_y == 0) dim_y = 1;
            if (dim_z == 0) dim_z = 1;

            for (int i = 0; i < uGrid.Count; i++)
            {

            vtkGeometryFilter GeometryFilter = vtkGeometryFilter.New();
            GeometryFilter.SetInputData(uGrid[i]);
            GeometryFilter.Update();

            vtkResampleToImage resampletoimagefilter = vtkResampleToImage.New();
            resampletoimagefilter.SetInputConnection(GeometryFilter.GetOutputPort());
            resampletoimagefilter.SetSamplingDimensions(Int32.Parse(dim_x.ToString()), Int32.Parse(dim_y.ToString()), Int32.Parse(dim_z.ToString()));
            resampletoimagefilter.UseInputBoundsOn();
            resampletoimagefilter.Update();
            vtkImageDataGeometryFilter imageDataGeometryFilter = vtkImageDataGeometryFilter.New();
            imageDataGeometryFilter.SetInputConnection(resampletoimagefilter.GetOutputPort());
            imageDataGeometryFilter.Update();

            //mapper2.SetInputConnection(resampletoimagefilter.GetOutputPort());
            mapper[i].SetInputData(imageDataGeometryFilter.GetOutput());
            mapper[i].Update();
            }
            Refresh();
#endif

        }
        public void set_results_point_data(int npoints,List<IList<double>> pointData, double Mapper_Min, double Mapper_max,string title, int number_of_color,bool discrete)
        {
            try
            {

            //discrete = false;
            //////////////////////////////////////////////set data
            int dataSize = npoints;
            vtkDoubleArray pointValues = vtkDoubleArray.New();
            pointValues.SetNumberOfComponents(1);
            pointValues.SetNumberOfTuples(dataSize);
            for (int i = 0; i < dataSize; ++i)
            {
                //Console.WriteLine($"{i}    {pointData[i][2]}");
                pointValues.SetValue(i, pointData[i][2]);
            }

                //int numberOfCells = 1;
                //vtkDoubleArray cellValues = vtkDoubleArray.New();
                //cellValues.SetNumberOfTuples(numberOfCells);
                ////for (int i = 0; i < numberOfCells; ++i)
                ////{
                ////    cellValues.SetValue(i, i);
                ////}
                //cellValues.SetValue(0, 55);


                //uGrid.GetCellData().SetScalars(cellValues);


                //////////////////////////////////////////////end of set data
                // if i < uGrid.Count-1   ---> the data won't be assigned to nodes
                for (int i = 0; i < uGrid.Count; i++)
                {
                    uGrid[i].GetPointData().SetScalars(pointValues);
                    //uGrid[i].GetPointData().SetTensors(pointValues);
                }



                ///////////////////////////////////vtkLookupTable
                // Map the scalar values in the image to colors with a lookup table:
                lut = vtkLookupTable.New();

            // Use the point data
            //number of colors
            int color_number = number_of_color;
            lut.SetNumberOfTableValues(color_number);
            ////Use the cell data
            //lut.SetNumberOfTableValues(numberOfCells);

            lut.Build();

            //// Assign some specific colors in this case
            //lut.SetNumberOfTableValues(3);
            //lut.SetTableValue(0, 0, 0, 1, 1);
            //lut.SetTableValue(1, 1, 1, 1, 1);
            //lut.SetTableValue(2, 1, 0, 0, 1);


            //lut.SetTableRange(0, 1);
            //lut.SetHueRange(0, 1);
            //lut.SetSaturationRange(1, 1);
            //lut.SetValueRange(1, 1);

            //Rainbow - blue to red.
            lut.SetHueRange(0.6667, 0.0);

            ////Rainbow - red to blue.
            //lut.SetHueRange(0.0, 0.667);


            ////Greyscale tables usually provide better structural details.
            //lut.SetHueRange(0, 0);
            //lut.SetSaturationRange(0, 0);
            //lut.SetValueRange(0.2, 1.0);


            //////Red - White - Blue
            //lut.SetScaleToLinear();
            //double r=0;
            //double g=0;
            //double b=0;
            //for (int i = 0; i < color_number; i++)
            //{
            //    double val = Mapper_Min + ((double)i / color_number) * (Mapper_max- Mapper_Min);
            //    getColorCorrespondingTovalue(val, ref r, ref g, ref b,Mapper_Min,Mapper_max);
            //    lut.SetTableValue(i, r, g, b,1);
            //}




            //add data to mapper
            for (int i = 0; i < uGrid.Count; i++)
            {
                    mapper[i].SetLookupTable(lut);
                    mapper[i].SetScalarRange(Mapper_Min, Mapper_max);
                    mapper[i].InterpolateScalarsBeforeMappingOn();
                    mapper[i].ScalarVisibilityOn();

                    //edgeMapper[i].SetLookupTable(lut);
                    //edgeMapper[i].SetScalarRange(Mapper_Min, Mapper_max);
                    //edgeMapper[i].InterpolateScalarsBeforeMappingOn();
                    edgeMapper[i].ScalarVisibilityOff();

                }

                if (discrete == true)
                {
                    lut.SetNumberOfColors(color_number);
                }
                else
                {
                    lut.SetNumberOfColors(1024);
                }
                ///////////////////////////////end vtkLookupTable



                /////////////////////////////////Legend

                //scalarBar = vtkScalarBarActor.New();
                scalarBar.SetLookupTable(mapper[0].GetLookupTable());




            //scalarBar.SetLookupTable(lut);
            scalarBar.SetTitle(title);
            scalarBar.SetNumberOfLabels(color_number+1);

            //scalarBar.SetLabelFormat("%.2e");
            //scalarBar.SetLabelFormat("%.2E");

            vtkTextProperty TextProp = vtkTextProperty.New();
            TextProp.SetFontSize(10);
            TextProp.SetBold(0);
            TextProp.SetFontFamilyToArial();
            TextProp.ItalicOff();
            TextProp.SetJustificationToLeft();
            TextProp.SetVerticalJustificationToBottom();
            TextProp.ShadowOff();
            TextProp.SetColor(1, 1, 1);

            scalarBar.SetTitleTextProperty(TextProp);
            scalarBar.SetLabelTextProperty(TextProp);

            // Assign default size of Scalar Bar
            scalarBar.SetMaximumWidthInPixels(100);
            scalarBar.SetPosition(0.015, 0.10);
            scalarBar.SetPosition2(0.16, 0.75);

            //renderer.AddActor2D(scalarBar);
            //Hide ScalarBar
            //scalarBar.VisibilityOff();
            /////////////////////////////////end Legend
            Renderer.RemoveActor2D(scalarBar);
            Renderer.AddActor2D(scalarBar);
            //Renderer.RemoveActor2D(scalarBar);
            Refresh();

            }
            catch (Exception e)
            {
                MainForm.displayErrorBox("Deformation data for selected combination doesn't exist \n"+e.Message);
            }
        }



        public void getColorCorrespondingTovalue(double val, ref double r, ref double g, ref double b,double min, double max)
        {
            double range = max - min;
            int numColorNodes = 3;
            double[][] color =
            {
              new double[] {0.0, 0.0, 1.0},
              new double[] {1.0, 1.0, 1.0},
              new double[] {1.0, 0.0, 0.0}
          };
            for (int i = 0; i < (numColorNodes - 1); i++)
            {
                double currFloor = min + ((double)i / (numColorNodes - 1)) * range;
                double currCeil = min + ((double)(i + 1) / (numColorNodes - 1)) * range;
                if ((val >= currFloor) && (val <= currCeil))
                {
                    double currFraction = (val - currFloor) / (currCeil - currFloor);
                    r = color[i][0] * (1.0 - currFraction) + color[i + 1][0] * currFraction;
                    g = color[i][1] * (1.0 - currFraction) + color[i + 1][1] * currFraction;
                    b = color[i][2] * (1.0 - currFraction) + color[i + 1][2] * currFraction;
                }
            }
        }

        private void draw_interface_point(int point, vtkPoints points, int Actor_Index_draw)
        {
            vtkVertex vertexpoint = vtkVertex.New();
            vertexpoint.GetPointIds().SetId(0, point);
            uGrid[Actor_Index_draw].InsertNextCell(vertexpoint.GetCellType(), vertexpoint.GetPointIds());
        }
        private void draw_interface_point_mapper(int Actor_Index_draw)
        {
#if VTK_MAJOR_VERSION_5
            mapper[Actor_Index_draw].SetInput(uGrid[Actor_Index_draw]);
#else
         mapper[Actor_Index_draw].SetInputData(uGrid[Actor_Index_draw]);
#endif
            actors[Actor_Index_draw].SetMapper(mapper[Actor_Index_draw]);
            actors[Actor_Index_draw].GetProperty().SetPointSize(6);
    
            actors[Actor_Index_draw].GetProperty().SetColor(color_interface[0], color_interface[1], color_interface[2]);
           
            Renderer.AddActor(actors[Actor_Index_draw]);

        }
        private void draw_polyline(int start_point,int end_point, vtkPoints points, int Actor_Index_draw)
        {

            //polyline
            int numberOfVertices = (end_point - start_point) + 1;
            vtkPolyLine polyline = vtkPolyLine.New();
            polyline.GetPointIds().SetNumberOfIds(numberOfVertices);
            for (int i= 0; i< numberOfVertices; i++)
                polyline.GetPointIds().SetId(i, start_point+i);

            // Add the points and hexahedron to an unstructured grid
            //vtkUnstructuredGrid uGrid = vtkUnstructuredGrid.New();
            // vtkDataSetMapper mapper = vtkDataSetMapper.New();




            uGrid[Actor_Index_draw].SetPoints(points);

            uGrid[Actor_Index_draw].InsertNextCell(polyline.GetCellType(), polyline.GetPointIds());


            // Visualize

#if VTK_MAJOR_VERSION_5
mapper[Actor_Index_draw].SetInput(uGrid[Actor_Index_draw]);
#else
     mapper[Actor_Index_draw].SetInputData(uGrid[Actor_Index_draw]);       
#endif


            //actors.Add("nodes", vtkActor.New());
            //actors["Nodes"].GetProperty().SetLineWidth(4);
            actors[Actor_Index_draw].SetMapper(mapper[Actor_Index_draw]);
            //actors[rendererName].GetProperty().SetPointSize(5);
            //actor.GetProperty().EdgeVisibilityOn();
            //actor.GetProperty().SetEdgeColor(0.8, 0.4, 0.5);
            actors[Actor_Index_draw].GetProperty().SetLineWidth(3);
            //actor.GetProperty().SetPointSize(10);

            actors[Actor_Index_draw].GetProperty().SetColor(color_1d[0], color_1d[1], color_1d[2]);
            Renderer.AddActor(actors[Actor_Index_draw]);
           
        }
        private void draw_line(int start_point, int end_point, vtkPoints points, int Actor_Index_draw)
        {

            vtkLine line = vtkLine.New();
            line.GetPointIds().SetId(0, start_point);
            line.GetPointIds().SetId(1, end_point);

            // Add the points and hexahedron to an unstructured grid
            //uGrid = vtkUnstructuredGrid.New();
            //uGrid.SetPoints(points);

            uGrid[Actor_Index_draw].InsertNextCell(line.GetCellType(), line.GetPointIds());

        }
        private void draw_quadratic_Line(int[] vertices, vtkPoints points, int Actor_Index_draw)
        {
            vtkQuadraticEdge quad_line = vtkQuadraticEdge.New();
            //quad.GetPointIds().SetNumberOfIds(4);
            for (int i = 0; i < 3; i++)
                quad_line.GetPointIds().SetId(i, vertices[i]);
            uGrid[Actor_Index_draw].InsertNextCell(quad_line.GetCellType(), quad_line.GetPointIds());
        }

        private void draw_line_mapper(int Actor_Index_draw, string element_type)
        {
            // Visualize
            //mapper = vtkDataSetMapper.New();


#if VTK_MAJOR_VERSION_5
mapper[Actor_Index_draw].SetInput(uGrid[Actor_Index_draw]);    
#else
         mapper[Actor_Index_draw].SetInputData(uGrid[Actor_Index_draw]);
#endif
            //actors.Add("nodes", vtkActor.New());
            //actors["Nodes"].GetProperty().SetLineWidth(4);
            actors[Actor_Index_draw].SetMapper(mapper[Actor_Index_draw]);
            //actors[rendererName].GetProperty().SetPointSize(5);
            //actors[Actor_Index_draw].GetProperty().EdgeVisibilityOn();
            //actors[Actor_Index_draw].GetProperty().SetEdgeColor(0.8, 0.4, 0.5);
            actors[Actor_Index_draw].GetProperty().SetLineWidth(3);
            //actor.GetProperty().SetPointSize(10);
            if (element_type.Contains("INTERFACE"))
            {
                actors[Actor_Index_draw].GetProperty().SetColor(color_interface[0], color_interface[1], color_interface[2]);
            }
            else if (element_type.Contains("AXIS"))
            {
                actors[Actor_Index_draw].GetProperty().SetColor(color_axis[0], color_axis[1], color_axis[2]);
            }
            else
            {
                actors[Actor_Index_draw].GetProperty().SetColor(color_1d[0], color_1d[1], color_1d[2]);
            }

            Renderer.AddActor(actors[Actor_Index_draw]);
        }
        private void draw_quad(int[] vertices, vtkPoints points, int Actor_Index_draw)
        {
            vtkQuad quad = vtkQuad.New();
            //quad.GetPointIds().SetNumberOfIds(4);
            for (int i = 0; i < 4; i++)
                quad.GetPointIds().SetId(i, vertices[i]);
            // Add the points and hexahedron to an unstructured grid
            //uGrid = vtkUnstructuredGrid.New();
            //uGrid.SetPoints(points);
            uGrid[Actor_Index_draw].InsertNextCell(quad.GetCellType(), quad.GetPointIds());
            //vtkCellArray cells = vtkCellArray.New();
            //cells.InsertNextCell(quad);
            //vtkPolyData polyData = vtkPolyData.New();
            //polyData.SetPoints(points);
            //polyData.SetPolys(cells);
        }

        private void draw_quadratic_quad(int[] vertices, vtkPoints points, int Actor_Index_draw)
        {
            vtkQuadraticQuad quad_quad = vtkQuadraticQuad.New();
            //quad.GetPointIds().SetNumberOfIds(4);
            for (int i = 0; i < 8; i++)
                quad_quad.GetPointIds().SetId(i, vertices[i]);
            uGrid[Actor_Index_draw].InsertNextCell(quad_quad.GetCellType(), quad_quad.GetPointIds());
        }
        private void draw_Bi_quadratic_quad(int[] vertices, vtkPoints points, int Actor_Index_draw)
        {
            vtkBiQuadraticQuad Bi_quad_quad = vtkBiQuadraticQuad.New();
            //quad.GetPointIds().SetNumberOfIds(4);
            for (int i = 0; i < 9; i++)
                Bi_quad_quad.GetPointIds().SetId(i, vertices[i]);
            uGrid[Actor_Index_draw].InsertNextCell(Bi_quad_quad.GetCellType(), Bi_quad_quad.GetPointIds());
        }
        private void draw_triangle(int[] vertices, vtkPoints points, int Actor_Index_draw)
        {
            vtkTriangle triangle = vtkTriangle.New();
            for (int i = 0; i < 3; i++)
                triangle.GetPointIds().SetId(i, vertices[i]);
            uGrid[Actor_Index_draw].InsertNextCell(triangle.GetCellType(), triangle.GetPointIds());
        }
        private void draw_quadratic_triangle(int[] vertices, vtkPoints points, int Actor_Index_draw)
        {
            vtkQuadraticTriangle triangle_quad = vtkQuadraticTriangle.New();
            for (int i = 0; i < 6; i++)
                triangle_quad.GetPointIds().SetId(i, vertices[i]);
            uGrid[Actor_Index_draw].InsertNextCell(triangle_quad.GetCellType(), triangle_quad.GetPointIds());
        }

        private void draw_quad_mapper(int Actor_Index_draw,string element_type)
        {
            // Visualize
            //mapper = vtkDataSetMapper.New();

#if VTK_MAJOR_VERSION_5
 mapper[Actor_Index_draw].SetInput(uGrid[Actor_Index_draw]);  
#else
        mapper[Actor_Index_draw].SetInputData(uGrid[Actor_Index_draw]); 
#endif


            //actors.Add("nodes", vtkActor.New());
            //actors["Nodes"].GetProperty().SetLineWidth(4);
            actors[Actor_Index_draw].SetMapper(mapper[Actor_Index_draw]);
            //actors[rendererName].GetProperty().SetPointSize(5);
            //actors[Actor_Index_draw].GetProperty().EdgeVisibilityOn();
            //actors[Actor_Index_draw].GetProperty().SetEdgeVisibility(1);
            actors[Actor_Index_draw].GetProperty().SetEdgeColor(color_edge[0], color_edge[1], color_edge[2]);
            //actors[Actor_Index_draw].GetProperty().SetLineWidth((float)0.5);
            //actor.GetProperty().SetPointSize(10);
            //actors[Actor_Index_draw].GetProperty().BackfaceCullingOn();
            //actors[Actor_Index_draw].GetProperty().FrontfaceCullingOn();
            //actors[Actor_Index_draw].SetVisibility(0);
            if (element_type.Contains("INTERFACE"))
            {
                actors[Actor_Index_draw].GetProperty().SetColor(color_interface[0], color_interface[1], color_interface[2]);
            }
            else if (element_type.Contains("AXIS"))
            {
                actors[Actor_Index_draw].GetProperty().SetColor(color_axis[0], color_axis[1], color_axis[2]);
            }
            else
            {
            actors[Actor_Index_draw].GetProperty().SetColor(color_2d[0], color_2d[1], color_2d[2]);
            }
            Renderer.AddActor(actors[Actor_Index_draw]);



//            vtkUnstructuredGridGeometryFilter uggFilter = vtkUnstructuredGridGeometryFilter.New();
//#if VTK_MAJOR_VERSION_5
// uggFilter.SetInput(uGrid[Actor_Index_draw]);
//#else
//          uggFilter.SetInputData(uGrid[Actor_Index_draw]);   
//#endif


            vtkExtractEdges edges = vtkExtractEdges.New();
            //edges.SetInputConnection(uggFilter.GetOutputPort());
#if VTK_MAJOR_VERSION_5
            edges.SetInput(uGrid[Actor_Index_draw]);
#else
            edges.SetInputData(uGrid[Actor_Index_draw]);
#endif

           


            //vtkDataSetMapper edgeMapper = vtkDataSetMapper.New();
            edgeMapper[Actor_Index_draw].SetInputConnection(edges.GetOutputPort());

            //vtkActor EdgeActor = vtkActor.New();
            EdgeActors[Actor_Index_draw].SetMapper(edgeMapper[Actor_Index_draw]);
            EdgeActors[Actor_Index_draw].GetProperty().SetColor(color_edge[0], color_edge[1], color_edge[2]);
            //freeEdgeActor.GetProperty().SetDiffuseColor(0, 0.5, 0);

            EdgeActors[Actor_Index_draw].GetProperty().EdgeVisibilityOff();

            //EdgeActors[Actor_Index_draw].GetProperty().SetEdgeVisibility(0);
            //EdgeActors[Actor_Index_draw].GetProperty().SetOpacity(1);
            EdgeActors[Actor_Index_draw].GetProperty().SetEdgeColor(color_edge[0], color_edge[1], color_edge[2]);
            
            //EdgeActors[Actor_Index_draw].GetProperty().SetLineWidth(1);
            //EdgeActors[Actor_Index_draw].GetProperty().BackfaceCullingOn();
            //EdgeActors[Actor_Index_draw].GetProperty().FrontfaceCullingOn();
            //EdgeActors[Actor_Index_draw].GetProperty().SetLineWidth(1);
            Renderer.AddActor(EdgeActors[Actor_Index_draw]);


        }
        private void draw_hex(int[] vertices, vtkPoints points, int Actor_Index_draw)
        {
            // Create a hexahedron from the points
            vtkHexahedron hex = vtkHexahedron.New();
            for (int i = 0; i < 8; i++)
                hex.GetPointIds().SetId(i, vertices[i]);
            // Add the points and hexahedron to an unstructured grid
            //uGrid = vtkUnstructuredGrid.New();
            //uGrid.SetPoints(points);
            uGrid[Actor_Index_draw].InsertNextCell(hex.GetCellType(), hex.GetPointIds());
        }
        private void draw_quadratic_hex(int[] vertices, vtkPoints points, int Actor_Index_draw)
        {
            vtkQuadraticHexahedron Quad_hex = vtkQuadraticHexahedron.New();
            for (int i = 0; i < 20; i++)
                Quad_hex.GetPointIds().SetId(i, vertices[i]);
            uGrid[Actor_Index_draw].InsertNextCell(Quad_hex.GetCellType(), Quad_hex.GetPointIds());
        }
        private void draw_tet(int[] vertices, vtkPoints points, int Actor_Index_draw)
        {
            vtkTetra tetrahedron = vtkTetra.New();
            for (int i = 0; i < 4; i++)
                tetrahedron.GetPointIds().SetId(i, vertices[i]);
            uGrid[Actor_Index_draw].InsertNextCell(tetrahedron.GetCellType(), tetrahedron.GetPointIds());
        }
        private void draw_quadratic_tet(int[] vertices, vtkPoints points, int Actor_Index_draw)
        {
            vtkQuadraticTetra Quad_tetrahedron = vtkQuadraticTetra.New();
            for (int i = 0; i < 10; i++)
                Quad_tetrahedron.GetPointIds().SetId(i, vertices[i]);
            uGrid[Actor_Index_draw].InsertNextCell(Quad_tetrahedron.GetCellType(), Quad_tetrahedron.GetPointIds());
        }
        private void draw_hex_mapper(int Actor_Index_draw)
        {
            //// Visualize
            ////mapper = vtkDataSetMapper.New();

            //vtkEdgeFlags
            //vtkUnstructuredGridGeometryFilter uggFilter = vtkUnstructuredGridGeometryFilter.New();
            //uggFilter.SetInput(uGrid[Actor_Index_draw]);
            //vtkExtractEdges edges = vtkExtractEdges.New();
            //edges.SetInputConnection(uggFilter.GetOutputPort());


            //vtkDataSetMapper edgeMapper = vtkDataSetMapper.New();
            //edgeMapper.SetInputConnection(edges.GetOutputPort());

            //vtkActor freeEdgeActor = vtkActor.New();
            //freeEdgeActor.SetMapper(edgeMapper);
            //freeEdgeActor.GetProperty().BackfaceCullingOn();
            //freeEdgeActor.GetProperty().SetDiffuseColor(0, 0.5, 0);
            //freeEdgeActor.GetProperty().SetEdgeVisibility(1);
            //freeEdgeActor.GetProperty().SetEdgeColor(160 / 255.0, 160 / 255.0, 160 / 255.0);
            //Renderer.AddActor(freeEdgeActor);




#if VTK_MAJOR_VERSION_5
  mapper[Actor_Index_draw].SetInput(uGrid[Actor_Index_draw]);
#else
        mapper[Actor_Index_draw].SetInputData(uGrid[Actor_Index_draw]);  
#endif




            //actors.Add("nodes", vtkActor.New());
            //actors["Nodes"].GetProperty().SetLineWidth(4);
            actors[Actor_Index_draw].SetMapper(mapper[Actor_Index_draw]);
            //actors[rendererName].GetProperty().SetPointSize(5);
            //actors[Actor_Index_draw].GetProperty().EdgeVisibilityOn();
            actors[Actor_Index_draw].GetProperty().SetEdgeColor(160 / 255.0, 160 / 255.0, 160 / 255.0);
            //actors[Actor_Index_draw].GetProperty().SetLineWidth(2);
            //actor.GetProperty().SetPointSize(10);
            //double red= Math.Round(204/255,2);

            

            actors[Actor_Index_draw].GetProperty().SetColor(color_3d[0], color_3d[1], color_3d[2]);
            Renderer.AddActor(actors[Actor_Index_draw]);



//            vtkUnstructuredGridGeometryFilter uggFilter = vtkUnstructuredGridGeometryFilter.New();
//#if VTK_MAJOR_VERSION_5
//             uggFilter.SetInput(uGrid[Actor_Index_draw]);
//#else
//           uggFilter.SetInputData(uGrid[Actor_Index_draw]);
//#endif


            vtkExtractEdges edges = vtkExtractEdges.New();
            //edges.SetInputConnection(uggFilter.GetOutputPort());

#if VTK_MAJOR_VERSION_5
            edges.SetInput(uGrid[Actor_Index_draw]);
#else
            edges.SetInputData(uGrid[Actor_Index_draw]);   
#endif


            //vtkDataSetMapper edgeMapper = vtkDataSetMapper.New();
            edgeMapper[Actor_Index_draw].SetInputConnection(edges.GetOutputPort());

            //vtkActor EdgeActor = vtkActor.New();
            EdgeActors[Actor_Index_draw].SetMapper(edgeMapper[Actor_Index_draw]);
            EdgeActors[Actor_Index_draw].GetProperty().SetColor(color_edge[0], color_edge[1], color_edge[2]);

            //freeEdgeActor.GetProperty().BackfaceCullingOn();
            //freeEdgeActor.GetProperty().SetDiffuseColor(0, 0.5, 0);

            EdgeActors[Actor_Index_draw].GetProperty().EdgeVisibilityOff();

            //EdgeActors[Actor_Index_draw].GetProperty().SetEdgeVisibility(1);
            EdgeActors[Actor_Index_draw].GetProperty().SetEdgeColor(color_edge[0], color_edge[1], color_edge[2]);
            //freeEdgeActor.GetProperty().SetLineWidth(2);
            Renderer.AddActor(EdgeActors[Actor_Index_draw]);

        }
        public int[] element_range(string _range_string)
        {
            int[] output = new int[2];
            string[] elementrange = _range_string.Split(new string[] { "-", "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
            output[0] = Int32.Parse(elementrange[0]);
            if (elementrange.Count() > 1)
            {
                output[1] = Int32.Parse(elementrange[1]);
            }
            else
            {
                output[1] = Int32.Parse(elementrange[0]);
            }
            return output;
        }


        public void mesh_view_opacity(string command)
        {

            //actors[Actor_Index_draw].GetProperty().SetRepresentationToWireframe();
            //actors[i].GetProperty().LightingOff();

            if (command == "view_mesh_edge")
            {
                //foreach (int i in C_variables.Layer_items["2D"])
                //    actors[i].GetProperty().EdgeVisibilityOn();
                //foreach (int i in C_variables.Layer_items["3D"])
                //    actors[i].GetProperty().EdgeVisibilityOn();

                foreach (int i in C_variables.Layer_items["2D"])
                    EdgeActors[i].VisibilityOn();
                foreach (int i in C_variables.Layer_items["3D"])
                    EdgeActors[i].VisibilityOn();

            }
            if (command == "hide_mesh_edge")
            {
                for (int i = 0; i < actors.Count; i++)
                {
                    //actors[i].GetProperty().EdgeVisibilityOff();
                    EdgeActors[i].VisibilityOff();

                }
            }
            if (command == "opacity40")
            {
                for (int i = 0; i < actors.Count; i++)
                {
                    actors[i].GetProperty().SetOpacity(0.4);
                    EdgeActors[i].GetProperty().SetOpacity(0.4);
                }
            }
            if (command == "opacity50")
            {
                for (int i = 0; i < actors.Count; i++)
                {
                    actors[i].GetProperty().SetOpacity(0.5);
                    EdgeActors[i].GetProperty().SetOpacity(0.5);
                }
            }
            if (command == "opacity100")
            {
                for (int i = 0; i < actors.Count; i++)
                {
                    actors[i].GetProperty().SetOpacity(1);
                    EdgeActors[i].GetProperty().SetOpacity(1);
                }
            }
            Refresh();
        }


        public void export(string file_name)
        {

            //string inputFileName = @"d:\test.vtu";
            //string outputFileName = @"d:\test.ply";
            //vtkXMLPolyDataReader reader = vtkXMLPolyDataReader.New();
            //reader.SetFileName(inputFileName);
            //reader.Update();

            //vtkPLYWriter writer = vtkPLYWriter.New();

            //writer.SetFileName(outputFileName);
            ////writer.SetInputConnection(mapper[1].GetOutputPort());
            //writer.SetInputConnection(reader.GetOutputPort());

            //writer.Update();

            //http://conself.com

            //Modify this to only visible objects
            vtkAppendFilter Append = vtkAppendFilter.New();
            for (int i =0; i < uGrid.Count; i++)
            {

                if (actors[i].GetVisibility() == 0)
                    continue;

#if VTK_MAJOR_VERSION_5
 Append.AddInput(uGrid[i]);
#else
             Append.AddInputData(uGrid[i]);  
#endif
            }


            // Here we write out the grid in xml format
            vtkXMLUnstructuredGridWriter xmlwriter = vtkXMLUnstructuredGridWriter.New();

            //xmlwriter.SetInput(uGrid[0]);
#if VTK_MAJOR_VERSION_5
         xmlwriter.SetInput(Append.GetOutput());
#else
         xmlwriter.SetInputData(Append.GetOutput()); 
#endif

            xmlwriter.SetFileName(file_name);
            xmlwriter.SetDataModeToAscii();
            xmlwriter.Update();



//            // Here we write out the grid in legacy format
//            vtkUnstructuredGridWriter writer = vtkUnstructuredGridWriter.New();
//            //writer.SetInput(uGrid[0]);

//#if VTK_MAJOR_VERSION_5
//writer.SetInput(Append.GetOutput());
//#else
//            writer.SetInputData(Append.GetOutput());
//#endif
//            writer.SetFileName(@"d:\polyhedron.vtk");
//            writer.Write();



        }

        #region clip
        // ----- Clip plane --------------------------------------------------
        private vtkPlane ClipPlane;
        private double ClipPlaneSize;
        private vtkActor ClipPlaneActor;
        private vtkTextWidget Reverse;
        private vtkTextWidget ClipX;
        private vtkTextWidget ClipY;
        private vtkTextWidget ClipZ;
        private vtkTextWidget Grid;
        private double[] BoundaryRange;

        private vtkSliderWidget SliderWidget;
        private vtkSliderRepresentation2D SliderRep;

        private void CreateSlider()
        {
            //  ------------ Cliping slider ------------------------------------------------------------
            SliderRep = vtkSliderRepresentation2D.New();

            // Default value range and slider in the middle
            SliderRep.SetMinimumValue(0);
            SliderRep.SetMaximumValue(10);
            SliderRep.SetValue(5);

            // Title and Label properties
            SliderRep.SetTitleText("Cut Plane position");
            SliderRep.GetTitleProperty().SetFontSize(10);
            SliderRep.GetTitleProperty().SetFontFamilyToArial();
            SliderRep.GetTitleProperty().SetBold(0);
            SliderRep.GetTitleProperty().ShadowOff();
            SliderRep.SetTitleHeight(0.03);
            SliderRep.GetLabelProperty().SetFontSize(10);
            SliderRep.GetLabelProperty().SetFontFamilyToArial();
            SliderRep.GetLabelProperty().SetBold(0);
            SliderRep.GetLabelProperty().ShadowOff();

            // Slider positon - normalized to viewport
            SliderRep.GetPoint1Coordinate().SetCoordinateSystemToNormalizedViewport();
            SliderRep.GetPoint1Coordinate().SetValue(0.20, 0.09);
            SliderRep.GetPoint2Coordinate().SetCoordinateSystemToNormalizedViewport();
            SliderRep.GetPoint2Coordinate().SetValue(0.95, 0.09);

            // Slider dimensions
            SliderRep.SetSliderLength(0.08);
            SliderRep.SetSliderWidth(0.025);
            SliderRep.SetHandleSize(0.01);
            SliderRep.SetTubeWidth(0.005);
            SliderRep.SetEndCapLength(0.00);

            // Slider color properties:
            SliderRep.GetSliderProperty().SetColor(180.0 / 255.0, 180.0 / 255.0, 180.0 / 255.0);  // Change the color of the knob that slides
            SliderRep.GetTitleProperty().SetColor(1.0, 1.0, 1.0);                                 // Change the color of the text indicating what the slider controls
            SliderRep.GetLabelProperty().SetColor(1.0, 1.0, 1.0);                                 // Change the color of the text displaying the value
    
            SliderRep.GetSelectedProperty().SetColor(131.0 / 255, 245.0 / 255.0, 3.0 / 255.0);    // Change the color of the knob when the mouse is held on it
            SliderRep.GetTubeProperty().SetColor(180.0 / 255.0, 180.0 / 255.0, 180.0 / 255.0);    // Change the color of the bar
            SliderRep.GetCapProperty().SetColor(180.0 / 255.0, 180.0 / 255.0, 180.0 / 255.0);     // Change the color of the ends of the bar

            // Slider Widget
            SliderWidget = vtkSliderWidget.New();
            SliderWidget.SetInteractor(renderWindow.GetInteractor());
            SliderWidget.SetRepresentation(SliderRep);
            SliderWidget.SetAnimationModeToAnimate();
            SliderWidget.SetEnabled(0);
            SliderWidget.InteractionEvt += new vtkObject.vtkObjectEventHandler(MoveClipPlane);
        }
        private void CreateClipPlane()
        {
            // Clip Plane
            ClipPlane = vtkPlane.New();

            vtkPoints ClipPoints = vtkPoints.New();
            ClipPlaneSize = 1;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    double y = j * ClipPlaneSize / 9 - ClipPlaneSize / 2;
                    double z = i * ClipPlaneSize / 9 - ClipPlaneSize / 2;
                    ClipPoints.InsertNextPoint(0, y, z);
                }
            }
            vtkUnstructuredGrid ClipGrid = vtkUnstructuredGrid.New();
            ClipGrid.SetPoints(ClipPoints);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    vtkQuad quad = vtkQuad.New();
                    quad.GetPointIds().SetId(0, i * 10 + j);
                    quad.GetPointIds().SetId(1, i * 10 + j + 1);
                    quad.GetPointIds().SetId(2, (i + 1) * 10 + j + 1);
                    quad.GetPointIds().SetId(3, (i + 1) * 10 + j);
                    ClipGrid.InsertNextCell(quad.GetCellType(), quad.GetPointIds());
                }
            }

            vtkDataSetMapper ClipMapper = vtkDataSetMapper.New();


#if VTK_MAJOR_VERSION_5
 ClipMapper.SetInput(ClipGrid);
#else
            ClipMapper.SetInputData(ClipGrid);
#endif
            ClipPlaneActor = vtkActor.New();
            ClipPlaneActor.SetMapper(ClipMapper);
            ClipPlaneActor.GetProperty().EdgeVisibilityOn();
            ClipPlaneActor.GetProperty().SetColor(195.0 / 255.0, 195.0 / 255.0, 195.0 / 255.0);
            ClipPlaneActor.GetProperty().SetOpacity(0.1);
            ClipPlaneActor.VisibilityOff();
            Renderer.AddActor(ClipPlaneActor);
            
            //  ------------- Clip Plane Buttons --------------------------------------------
            Grid = CreateClipButton("Grid OFF", new double[2] { 0.875, 0.39 }, 8);
            Grid.StartInteractionEvt += new vtkObject.vtkObjectEventHandler(ShowGrid);

            Reverse = CreateClipButton("Reverse", new double[2] { 0.875, 0.34 }, 8);
            Reverse.StartInteractionEvt += new vtkObject.vtkObjectEventHandler(ReverseClipPlaneNormal);

            ClipX = CreateClipButton("X-Plane", new double[2] { 0.88, 0.29 }, 8);
            ClipX.StartInteractionEvt += new vtkObject.vtkObjectEventHandler(SetClipPlaneNormal);

            ClipY = CreateClipButton("Y-Plane", new double[2] { 0.88, 0.24 }, 8);
            ClipY.StartInteractionEvt += new vtkObject.vtkObjectEventHandler(SetClipPlaneNormal);

            ClipZ = CreateClipButton("Z-Plane", new double[2] { 0.88, 0.19 }, 8);
            ClipZ.StartInteractionEvt += new vtkObject.vtkObjectEventHandler(SetClipPlaneNormal);

        }

        private vtkTextWidget CreateClipButton(string Text, double[] Position, int FontSize)
        {
            // Create Text Actor and Representation
            vtkTextActor TextActor = vtkTextActor.New();
            TextActor.SetInput(Text);
            TextActor.GetTextProperty().SetBold(0);
            TextActor.GetTextProperty().SetFontFamilyToArial();
            vtkTextRepresentation Rep = vtkTextRepresentation.New();
            Rep.SetPosition(Position[0], Position[1]);
            Rep.SetTextActor(TextActor);
            Rep.SetShowBorderToOff();

            // Set widget
            vtkTextWidget Widget = vtkTextWidget.New();
            Widget.SetRepresentation(Rep);
            Widget.GetTextActor().GetTextProperty().SetFontSize(FontSize);
            Widget.GetTextActor().SetTextScaleModeToViewport();
            Widget.SetInteractor(renderWindow.GetInteractor());
            Widget.SelectableOn();
            Widget.SetEnabled(0);
            Widget.ResizableOff();

            return Widget;
        }

        private void ShowGrid(vtkObject sender, vtkObjectEventArgs e)
        {
            if (ClipPlaneActor.GetVisibility() == 1)
            {
                ClipPlaneActor.VisibilityOff();
                Grid.GetTextActor().SetInput("Grid ON");
            }
            else
            {
                ClipPlaneActor.VisibilityOn();
                Grid.GetTextActor().SetInput("Grid OFF");
            }

            Refresh();
        }

        private void SetClipPlaneNormal(vtkObject sender, vtkObjectEventArgs e)
        {
            vtkTextWidget widget = sender as vtkTextWidget;
            string text = widget.GetTextActor().GetInput();

            if (text == "X-Plane" || text == "Clip X") SetClipPlane("X");
            if (text == "Y-Plane" || text == "Clip Y") SetClipPlane("Y");
            if (text == "Z-Plane" || text == "Clip Z") SetClipPlane("Z");

            Refresh();
        }

        private void ReverseClipPlaneNormal(vtkObject sender, vtkObjectEventArgs e)
        {
            double[] N = ClipPlane.GetNormal();
            ClipPlane.SetNormal(-N[0], -N[1], -N[2]);
            Refresh();
        }

        private void MoveClipPlane(vtkObject sender, vtkObjectEventArgs e)
        {
            if (ClipPlane.GetNormal()[0] != 0) ClipPlane.SetOrigin(SliderRep.GetValue(), ClipPlane.GetOrigin()[1], ClipPlane.GetOrigin()[2]);
            if (ClipPlane.GetNormal()[1] != 0) ClipPlane.SetOrigin(ClipPlane.GetOrigin()[0], SliderRep.GetValue(), ClipPlane.GetOrigin()[2]);
            if (ClipPlane.GetNormal()[2] != 0) ClipPlane.SetOrigin(ClipPlane.GetOrigin()[0], ClipPlane.GetOrigin()[1], SliderRep.GetValue());

            ClipPlaneActor.SetPosition(
                                ClipPlane.GetOrigin()[0],
                                ClipPlane.GetOrigin()[1],
                                ClipPlane.GetOrigin()[2]);
        }

      
        public void ShowClip(bool show, double[] BoundaryRange)
        {
            if (show == true)
            {
                SetClipPlaneScale(BoundaryRange);
                SetClipPlane("X");

                ClipPart(ClipPlane, true);

                SliderWidget.SetEnabled(1);
                ClipPlaneActor.VisibilityOn();
                Reverse.SetEnabled(1);
                ClipX.SetEnabled(1);
                ClipY.SetEnabled(1);
                ClipZ.SetEnabled(1);
                Grid.SetEnabled(1);

                // Restore Clip Buttons position
                Grid.GetTextActor().SetPosition(0.88, 0.39);
                Reverse.GetTextActor().SetPosition(0.875, 0.34);
                ClipX.GetTextActor().SetPosition(0.88, 0.29);
                ClipY.GetTextActor().SetPosition(0.88, 0.24);
                ClipZ.GetTextActor().SetPosition(0.88, 0.19);
            }
            else
            {
                SliderWidget.SetEnabled(0);
                ClipPlaneActor.VisibilityOff();
                Reverse.SetEnabled(0);
                ClipX.SetEnabled(0);
                ClipY.SetEnabled(0);
                ClipZ.SetEnabled(0);
                Grid.SetEnabled(0);

                ClipPart(null, show);
            }
        }

        /// <summary>
        /// Set Clip Plane scale based on maximum dimension.
        /// <br>This method is used at the begining to set Plane size.</br>
        /// </summary>
        public void SetClipPlaneScale(double[] boundaryRange)
        {
            List<double> temp = new List<double>();
            temp.Add(Math.Abs(boundaryRange[1] - boundaryRange[0]) * 1.25);
            temp.Add(Math.Abs(boundaryRange[3] - boundaryRange[2]) * 1.25);
            temp.Add(Math.Abs(boundaryRange[5] - boundaryRange[4]) * 1.25);

            ClipPlaneSize = temp.Max();
            ClipPlaneActor.SetScale(ClipPlaneSize);
            BoundaryRange = boundaryRange;
        }

        /// <summary>
        /// Set Clip Plane direction (normal). If direction has changed plane is set at the minimal position
        /// </summary>
        public void SetClipPlane(string N)
        {
            bool OriChanged = false;             // Check if orientation has changed
            double[] n = ClipPlane.GetNormal();  // Previous direction of Normal
            double min = 0; double max = 1;
            if (N == "X")
            {
                if (n[0] == 0) OriChanged = true;
                min = BoundaryRange[0];
                max = BoundaryRange[1];
                ClipPlane.SetNormal(1, 0, 0);
                ClipPlaneActor.SetOrientation(0, 0, 0);
            }
            if (N == "Y")
            {
                if (n[1] == 0) OriChanged = true;
                min = BoundaryRange[2];
                max = BoundaryRange[3];
                ClipPlane.SetNormal(0, 1, 0);
                ClipPlaneActor.SetOrientation(0, 0, 90);
            }
            if (N == "Z")
            {
                if (n[2] == 0) OriChanged = true;
                min = BoundaryRange[4];
                max = BoundaryRange[5];
                ClipPlane.SetNormal(0, 0, 1);
                ClipPlaneActor.SetOrientation(0, 90, 0);
            }
            if (N == "-X")
            {
                if (n[0] == 0) OriChanged = true;
                min = BoundaryRange[0];
                max = BoundaryRange[1];
                ClipPlane.SetNormal(-1, 0, 0);
                ClipPlaneActor.SetOrientation(0, 0, 0);
            }
            if (N == "-Y")
            {
                if (n[1] == 0) OriChanged = true;
                min = BoundaryRange[2];
                max = BoundaryRange[3];
                ClipPlane.SetNormal(0, -1, 0);
                ClipPlaneActor.SetOrientation(0, 0, 90);
            }
            if (N == "-Z")
            {
                if (n[2] == 0) OriChanged = true;
                min = BoundaryRange[4];
                max = BoundaryRange[5];
                ClipPlane.SetNormal(0, 0, -1);
                ClipPlaneActor.SetOrientation(0, 90, 0);
            }
            // Update slider values
            SliderRep.SetMinimumValue(min);
            SliderRep.SetMaximumValue(max);

            // Use Boundary range of all Actors to set Clip Plane in the middle
            if (OriChanged == true)
            {
                if (N == "X")
                {
                    ClipPlane.SetOrigin(
                            BoundaryRange[0],
                            (BoundaryRange[3] + BoundaryRange[2]) / 2.0,
                            (BoundaryRange[5] + BoundaryRange[4]) / 2.0);
                }
                if (N == "Y")
                {
                    ClipPlane.SetOrigin(
                            (BoundaryRange[1] + BoundaryRange[0]) / 2.0,
                            BoundaryRange[2],
                            (BoundaryRange[5] + BoundaryRange[4]) / 2.0);
                }
                if (N == "Z")
                {
                    ClipPlane.SetOrigin(
                            (BoundaryRange[1] + BoundaryRange[0]) / 2.0,
                            (BoundaryRange[3] + BoundaryRange[2]) / 2.0,
                            BoundaryRange[4]);
                }

                ClipPlaneActor.SetPosition(
                    ClipPlane.GetOrigin()[0],
                    ClipPlane.GetOrigin()[1],
                    ClipPlane.GetOrigin()[2]);

                SliderRep.SetValue(min);
            }
        }



        // Clipping
        //private List<vtkTableBasedClipDataSet> Clipper;
        private vtkTableBasedClipDataSet Clipper;

        public void ClipPart(vtkPlane ClipPlane, bool clip)
        {

            if (clip == true)
            {
               // Clipper.Add(vtkTableBasedClipDataSet.New());
               // Clipper.Clear();

                //Clipper = vtkTableBasedClipDataSet.New();
                for (int i = 0; i < mapper.Count; i++)
                  {
                    Clipper = vtkTableBasedClipDataSet.New();
                    Clipper.SetClipFunction(ClipPlane);

#if VTK_MAJOR_VERSION_5
//Clipper.SetInputConnection(uGrid[i].GetProducerPort());
                    Clipper.SetInput(uGrid[i]);
#else
           Clipper.SetInputData(uGrid[i]);         
#endif
                   
                    Clipper.Update();
                    mapper[i].SetInputConnection(Clipper.GetOutputPort());
                    mapper[i].Update();

                    vtkExtractEdges edges = vtkExtractEdges.New();

                    edges.SetInputConnection(Clipper.GetOutputPort());
                    edgeMapper[i].SetInputConnection(edges.GetOutputPort());
                    edgeMapper[i].Update();
                   }
                //// Update Feature Filter -> will update Edges and Faces
                //Filter.SetInput(Clipper.GetOutput());
                //Filter.Update();

            }
            else
            {

                for (int i = 0; i < mapper.Count; i++)
                {




                    vtkExtractEdges edges = vtkExtractEdges.New();
#if VTK_MAJOR_VERSION_5
                    mapper[i].SetInput(uGrid[i]);
                    edges.SetInput(uGrid[i]);
                    //edgeMapper[i].SetInput(edges);
#else
                    mapper[i].SetInputData(uGrid[i]);
                    edges.SetInputData(uGrid[i]);  
                    //edgeMapper[i].SetInputData(edges);
#endif

                    edgeMapper[i].SetInputConnection(edges.GetOutputPort());

                    mapper[i].Update();
                    edgeMapper[i].Update();
                }




                //Mapper.SetInput(Grid);
                //Mapper.Update();
                //// Update Feature Filter -> will update Edges and Faces
                //Filter.SetInput(Grid);
                //Filter.Update();

            }
        }




        #endregion clip


        public void Shrink_filter(double factor,bool apply_filter)
        {
          
            if (apply_filter == true) {
            for (int i = 0; i < uGrid.Count-1; i++) //-1 for keep node unshrinked
            {
                vtkShrinkFilter shrinkfilter = vtkShrinkFilter.New();
#if VTK_MAJOR_VERSION_5
 shrinkfilter.SetInput(uGrid[i]);
#else
                    shrinkfilter.SetInputData(uGrid[i]);
#endif

                shrinkfilter.Update();
                shrinkfilter.SetShrinkFactor(factor);

                    //mapper2.SetInputConnection(resampletoimagefilter.GetOutputPort());
#if VTK_MAJOR_VERSION_5
 mapper[i].SetInput(shrinkfilter.GetOutput());
#else
                    mapper[i].SetInputData(shrinkfilter.GetOutput());
#endif

                    mapper[i].Update();
            }
            }
            else
            {
                for (int i = 0; i < uGrid.Count; i++)
                {
#if VTK_MAJOR_VERSION_5
                    mapper[i].SetInput(uGrid[i]);
#else
                    mapper[i].SetInputData(uGrid[i]);
#endif

                    mapper[i].Update();
                }
            }


            Refresh();
    }
        private void draw_points(List<IList<string>> Point_list)
        {
            ////List<IList<double>> Point_list

            int nPts =  Point_list.Count();
            double[,] p = new double[nPts, 3];

            for (int i = 0; i < nPts; i++)
            {
                p[i, 0] = double.Parse(Point_list[i][2]);
                p[i, 1] = double.Parse(Point_list[i][3]);
                p[i, 2] = double.Parse(Point_list[i][4]);
            }


            //// Setup the coordinates of eight points 
            //// (faces must be in counter clockwise order as viewed from the outside)
            //double[,] p = new double[,] {
            //   { 0.0, 0.0, 0.0 },
            //   { 1.0, 0.0, 0.0 },
            //   { 1.0, 1.0, 0.0 },
            //   { 0.0, 1.0, 0.0 },
            //   { 0.0, 0.0, 1.0 },
            //   { 1.0, 0.0, 1.0 },
            //   { 1.0, 1.0, 1.0 },
            //   { 0.0, 1.0, 1.0 }
            //};




            // Create the points
            vtkPoints points = vtkPoints.New();
            for (int i = 0; i < nPts; i++)
                points.InsertNextPoint(p[i, 0], p[i, 1], p[i, 2]);


            ////vertex
            //vtkVertex vertex = vtkVertex.New();
            //vertex.GetPointIds().SetId(0, 3);

            //polyvertex
            int numberOfVertices = nPts;
            //points.InsertNextPoint(0, 2, 1);
            //points.InsertNextPoint(1, 0, .4);
            //points.InsertNextPoint(0, 1, .6);
            vtkPolyVertex polyvertex = vtkPolyVertex.New();
            polyvertex.GetPointIds().SetNumberOfIds(numberOfVertices);
            for (int i = 0; i < numberOfVertices; i++)
                polyvertex.GetPointIds().SetId(i,i);



            ////  Line
            //vtkLine line = vtkLine.New();
            //line.GetPointIds().SetId(0, 2);
            //line.GetPointIds().SetId(1, 1);

            ////polyline
            //vtkPolyLine polyline = vtkPolyLine.New();
            //polyline.GetPointIds().SetNumberOfIds(numberOfVertices);
            //polyline.GetPointIds().SetId(0, 1);
            //polyline.GetPointIds().SetId(1, 2);
            //polyline.GetPointIds().SetId(2, 3);

            ////triangle
            //vtkTriangle triangle = vtkTriangle.New();
            //for (int i = 0; i < 3; i++)
            //    triangle.GetPointIds().SetId(i, i);



            //vtkPolygon polygon = vtkPolygon.New();
            //polygon.GetPointIds().SetNumberOfIds(numberOfVertices);
            //for (int i = 0; i < 3; i++)
            //    polygon.GetPointIds().SetId(i, i);


            //vtkQuad quad = vtkQuad.New();
            //for (int i = 0; i < 5; i++)
            //    quad.GetPointIds().SetId(i, i);

            //vtkTetra tetra = vtkTetra.New();
            //tetra.GetPointIds().SetId(0, 0);
            //tetra.GetPointIds().SetId(1, 1);
            //tetra.GetPointIds().SetId(2, 2);
            //tetra.GetPointIds().SetId(3, 4);

            //vtkQuadraticEdge quadline = vtkQuadraticEdge.New();
            //for (int i = 0; i < 4; i++)
            //    quadline.GetPointIds().SetId(i, i);

            //// Create a hexahedron from the points
            //vtkHexahedron hex = vtkHexahedron.New();
            //for (int i = 0; i < 8; i++)
            //    hex.GetPointIds().SetId(i, i);



            // Create one text property for all
            vtkTextProperty textProperty = vtkTextProperty.New();
            textProperty.SetFontSize(10);
            textProperty.SetJustificationToCentered();


            //// Add the hexahedron to a cell array
            //vtkCellArray cells = vtkCellArray.New();
            //cells.InsertNextCell(hex);



            // Add the points and hexahedron to an unstructured grid
            vtkUnstructuredGrid uGrid = vtkUnstructuredGrid.New();
            uGrid.SetPoints(points);

            //uGrid.InsertNextCell(quadline.GetCellType(), quadline.GetPointIds());
            //uGrid.InsertNextCell(tetra.GetCellType(), tetra.GetPointIds());
            //uGrid.InsertNextCell(quad.GetCellType(), quad.GetPointIds());
            //uGrid.InsertNextCell(polygon.GetCellType(), polygon.GetPointIds());
            //uGrid.InsertNextCell(triangle.GetCellType(), triangle.GetPointIds());
            //uGrid.InsertNextCell(hex.GetCellType(), hex.GetPointIds());
            //uGrid.InsertNextCell(polyline.GetCellType(), polyline.GetPointIds());
            //uGrid.InsertNextCell(line.GetCellType(), line.GetPointIds());
            uGrid.InsertNextCell(polyvertex.GetCellType(), polyvertex.GetPointIds());
            //uGrid.InsertNextCell(vertex.GetCellType(), vertex.GetPointIds());




            // Visualize
            vtkDataSetMapper mapper = vtkDataSetMapper.New();
#if VTK_MAJOR_VERSION_5
  mapper.SetInput(uGrid);
#else
      mapper.SetInputData(uGrid);    
#endif


            //vtkActor actor = vtkActor.New();
            //actor.SetMapper(mapper);
            //// actor.GetProperty().SetOpacity(0.95);
            //actor.GetProperty().EdgeVisibilityOn();
            //actor.GetProperty().SetEdgeColor(0.8, 0.4, 0.5);
            //actor.GetProperty().SetLineWidth(2);
            //actor.GetProperty().SetPointSize(10);
            //actor.GetProperty().SetColor(0.1, 0.5, 0.9);



            //// Label the points
            //vtkLabeledDataMapper labelMapper = vtkLabeledDataMapper.New();
            //labelMapper.SetInput(uGrid);
            //vtkActor2D labelActor = vtkActor2D.New();
            //labelActor.SetMapper(labelMapper);
            //labelActor.VisibilityOff();


            //Renderer.AddViewProp(labelActor);




            //vtkStringArray label = vtkStringArray.New();
            //label.SetName("Node Labels");
            //for (int i = 0; i < nPts; i++)
            //    label.InsertNextValue($"{Point_list[i][1]}");
            ////label.SetValue(0, "K1");

            //// Label the points
            //uGrid.GetPointData().AddArray(label);


            //// Generate the label hierarchy.
            //vtkPointSetToLabelHierarchy pointSetToLabelHierarchyFilter = vtkPointSetToLabelHierarchy.New();
            //pointSetToLabelHierarchyFilter.SetInput(uGrid);
            //pointSetToLabelHierarchyFilter.SetLabelArrayName("Node Labels");
            ////pointSetToLabelHierarchyFilter.SetPriorityArrayName("sizes");
            //pointSetToLabelHierarchyFilter.Update();

            //// Create a mapper and actor for the labels.//vtkLabeledDataMapper vtkLabelPlacementMapper  
            //vtkLabelPlacementMapper labelMapper2 = vtkLabelPlacementMapper.New();
            //labelMapper2.SetInputConnection(pointSetToLabelHierarchyFilter.GetOutputPort());

            //Renderer.RemoveActor(labelactors["Node Labels"]);
            //labelactors["Node Labels"].SetMapper(labelMapper2);
            //Renderer.AddActor(labelactors["Node Labels"]);







            //////renderer.RemoveViewProp(labelActor);

            ////renderer.AddViewProp(pointActor);


            ////renderer.RemoveAllViewProps();

            //Renderer.AddActor(actor);

            Renderer.RemoveActor(actors[0]);
            // actors.Add("nodes", vtkActor.New());
            //actors["Nodes"].GetProperty().SetLineWidth(4);
            actors[0].SetMapper(mapper);
            actors[0].GetProperty().SetPointSize(5);
            //actor.GetProperty().EdgeVisibilityOn();
            //actor.GetProperty().SetEdgeColor(0.8, 0.4, 0.5);
            //actor.GetProperty().SetLineWidth(2);
            //actor.GetProperty().SetPointSize(10);
            actors[0].GetProperty().SetColor(0.6, 0.6, 0.2);
            Renderer.AddActor(actors[0]);

            Refresh();


        }

        public bool part_point_select;
        public bool part_cell_select;
        public bool part_hide_extrusion;
        public double[] extrusion_vector;
        public List<int> part_selected_ids;
        private vtkPoints part_vtkpoint;
        public List<List<double>> part_points;

        public List<List<int>> part_lines;
        public List<List<int>> part_surfaces;

        private vtkActor part_actor;
        private vtkActor2D part_actor2d;
        private vtkDataSetMapper part_mapper;
        private vtkUnstructuredGrid part_uGrid;

      public void Part_draw()
        {
            Renderer.RemoveAllViewProps();
            leftcornertesxt();

            int numberOfVertices = part_points.Count();
            if (numberOfVertices < 0) { return; }

            //part_select = false;
            part_actor = vtkActor.New();
            part_mapper = vtkDataSetMapper.New();
            part_uGrid = vtkUnstructuredGrid.New();
            part_actor2d = vtkActor2D.New();

            part_vtkpoint = vtkPoints.New();
            foreach (var item in part_points)
              part_vtkpoint.InsertNextPoint(item[0], item[1], item[2]);
            part_uGrid.SetPoints(part_vtkpoint);

            vtkPolyVertex polyvertex = vtkPolyVertex.New();
            polyvertex.GetPointIds().SetNumberOfIds(numberOfVertices);
            for (int i = 0; i < numberOfVertices; i++)
                polyvertex.GetPointIds().SetId(i, i);
            part_uGrid.InsertNextCell(polyvertex.GetCellType(), polyvertex.GetPointIds());

            vtkLine line = vtkLine.New();
            foreach (var item in part_lines)
            {
                line.GetPointIds().SetId(0, item[0]);
                line.GetPointIds().SetId(1, item[1]);
                part_uGrid.InsertNextCell(line.GetCellType(), line.GetPointIds());
            }


            vtkPolygon polygon;
            polygon = vtkPolygon.New();
            foreach (var item in part_surfaces)
            {
                if (item.Count > 2)
                {
                    
                    polygon.GetPointIds().SetNumberOfIds(item.Count);
                    for (int i = 0; i < item.Count; i++)
                        polygon.GetPointIds().SetId(i, item[i]);
                    part_uGrid.InsertNextCell(polygon.GetCellType(), polygon.GetPointIds());
                }
            }


            if (part_hide_extrusion == false)
            {
                if (extrusion_vector[0] != 0 || extrusion_vector[1] != 0 || extrusion_vector[2] != 0)
                {
                    Console.WriteLine(extrusion_vector[0] + extrusion_vector[1]+ extrusion_vector[2]);
                    // Create a cell array to store the quad in and add the quad to it
                    vtkCellArray cells = vtkCellArray.New();
                    cells.InsertNextCell(polygon);
                    vtkPolyData polyData = vtkPolyData.New();
                    polyData.SetPoints(part_vtkpoint);
                    polyData.SetPolys(cells);
                    // Apply linear extrusion 
                    vtkLinearExtrusionFilter extrude = vtkLinearExtrusionFilter.New();
#if VTK_MAJOR_VERSION_5
extrude.SetInputConnection(polyData.GetProducerPort());
#else
        extrude.SetInputData(polyData);            
#endif

                    extrude.SetExtrusionTypeToNormalExtrusion();
                    extrude.SetVector(extrusion_vector[0], extrusion_vector[1], extrusion_vector[2]);
                    extrude.SetScaleFactor(1);
                    //vtkTriangleFilter triangleFilter = vtkTriangleFilter.New();
                    //triangleFilter.SetInputConnection(extrude.GetOutputPort());
                    vtkDataSetMapper mapper = vtkDataSetMapper.New();
                    mapper.SetInputConnection(extrude.GetOutputPort());
                    vtkActor part_extrude_actor = vtkActor.New();
                    part_extrude_actor.SetMapper(mapper);
                    part_extrude_actor.GetProperty().SetOpacity(1.0);
                    part_extrude_actor.GetProperty().EdgeVisibilityOn();
                    part_extrude_actor.GetProperty().SetColor(197 / 255.0, 225 / 255.0, 165 / 255.0);
                    Renderer.AddActor(part_extrude_actor);
                }
            }


            // Create one text property for all
            //vtkTextProperty textProperty = vtkTextProperty.New();
            //textProperty.SetFontSize(10);
            //textProperty.SetJustificationToCentered();

            // Label the points
            vtkLabeledDataMapper labelMapper = vtkLabeledDataMapper.New();
#if VTK_MAJOR_VERSION_5
 labelMapper.SetInput(part_uGrid);   
#else
        labelMapper.SetInputData(part_uGrid);
#endif
            //labelMapper.SetLabelFormat("P %d");
            part_actor2d.SetMapper(labelMapper);
            //part_actor2d.VisibilityOff();
            Renderer.AddActor(part_actor2d);

            // Visualize
#if VTK_MAJOR_VERSION_5
 part_mapper.SetInput(part_uGrid);
#else
          part_mapper.SetInputData(part_uGrid); 
#endif

            part_actor.SetMapper(part_mapper);
            part_actor.GetProperty().SetPointSize(5);
            //part_actor.GetProperty().SetLineWidth(1);
            part_actor.GetProperty().SetEdgeVisibility(1);
            part_actor.GetProperty().SetEdgeColor(255 / 255.0, 255 / 255.0, 224 / 255.0);
            //part_actor.GetProperty().SetColor(255 / 255.0, 255 / 255.0, 0 / 255.0);
            Renderer.AddActor(part_actor);

            Refresh();
        }

        private void part_left_click()
        {



            vtkUnstructuredGrid SelectionunStructuredGrid = vtkUnstructuredGrid.New();
            SelectionunStructuredGrid.SetPoints(part_vtkpoint);
            if (part_point_select == true)
            {
                //InterStylePick.OnRightButtonDown();
                /////point picker
                
                //SelectionPoints = vtkPoints.New();

                int[] clickPos = InterWindow.GetEventPosition();
                vtkPointPicker PointPicker = vtkPointPicker.New();
                PointPicker.SetTolerance(0.025);
                PointPicker.Pick(clickPos[0], clickPos[1], 0, Renderer);

                //vtkPoints points = Faces.GetPoints();

                double[] PickPosition = PointPicker.GetPickPosition();
#if VTK_MAJOR_VERSION_5
                long PickpointID = PointPicker.GetPointId(); //x64
                //int PickpointID = PointPicker.GetPointId();
#else
         int PickpointID = PointPicker.GetPointId();    
#endif




                //string pointIDs = "";
                //int pointcounter = 0;
                //bool addtoselection = false;
                //for (int j = 0; j < part_vtkpoint.GetNumberOfPoints(); j++)
                //{
                //    if (Math.Abs(part_vtkpoint.GetPoint(j)[0] - PickPosition[0]) < 1e-6 &&
                //        Math.Abs(part_vtkpoint.GetPoint(j)[1] - PickPosition[1]) < 1e-6 &&
                //        Math.Abs(part_vtkpoint.GetPoint(j)[2] - PickPosition[2]) < 1e-6)
                //    {
                //        pointcounter++;
                //        PickpointID = j;
                //        pointIDs += $"({j}); ";
                //        PickPosition[0] = part_vtkpoint.GetPoint(j)[0];
                //        PickPosition[1] = part_vtkpoint.GetPoint(j)[1];
                //        PickPosition[2] = part_vtkpoint.GetPoint(j)[2];
                //        addtoselection = true;
                //        //break;
                //    }
                //}

                if (PickpointID != -1)
                //if (addtoselection)
                {
                    PickPosition[0] = part_vtkpoint.GetPoint(PickpointID)[0];
                    PickPosition[1] = part_vtkpoint.GetPoint(PickpointID)[1];
                    PickPosition[2] = part_vtkpoint.GetPoint(PickpointID)[2];
              

                        //Console.WriteLine(PickpointID + " ||| " + PickPosition[0] + "," + PickPosition[1] + "," + PickPosition[2]);

                        mainfrm.LogBox_add($"Selected point ID: {PickpointID}" + $" [{PickPosition[0]} , {PickPosition[1]} , {PickPosition[2]}]");
                   

                        //SelectionPoints = new vtkPoints();
                        //SelectionPoints.InsertNextPoint(PickPosition[0], PickPosition[1], PickPosition[2]);
 

                    vtkPolyVertex selectvertices = vtkPolyVertex.New();
                    selectvertices.GetPointIds().SetNumberOfIds(1);//SelectionPoints.GetNumberOfPoints()
                    //for (int i = 0; i < SelectionPoints.GetNumberOfPoints(); i++)
                    selectvertices.GetPointIds().SetId(0, PickpointID);



                    

                    SelectionunStructuredGrid.InsertNextCell(selectvertices.GetCellType(), selectvertices.GetPointIds());


                }

                if (part_cell_select == true) { 
                ///////cell picker
                ////int[] clickPos = InterWindow.GetEventPosition();
                vtkCellPicker CellPicker = vtkCellPicker.New();
                CellPicker.SetTolerance(0.025);
                CellPicker.Pick(clickPos[0], clickPos[1], 0, Renderer);

                    //vtkPoints points = Faces.GetPoints();

                    //double[] PickPosition = CellPicker.GetPickPosition();
#if VTK_MAJOR_VERSION_5
                     long PickCellID = CellPicker.GetCellId();    //x64
                   // int PickCellID = CellPicker.GetCellId();
#else
           int PickCellID = CellPicker.GetCellId();     
#endif

                    //vtkPoints cellpoints = uGrid[0].GetCell(0).GetPoints();
                    //MainForm.displayErrorBox(cellpoints.ToString()  );
                    //vtkActor pickactor = CellPicker.GetActor();
                    //pickactor.GetProperty().SetColor(1, 0, 0);
                    //Renderer.AddActor(pickactor);

                    //Console.WriteLine(PickCellID + " ||| " + PickPosition[0] + "," + PickPosition[1] + "," + PickPosition[2]);
                    if (PickCellID > 0)
                {
                       // mainfrm.LogBox_add("ID:" + PickCellID);
                        SelectionunStructuredGrid.InsertNextCell(part_uGrid.GetCell(PickCellID).GetCellType(), part_uGrid.GetCell(PickCellID).GetPointIds());
                        string log = "";
                        //mainfrm.LogBox_add("type"+part_uGrid.GetCell(PickCellID).GetCellType().ToString());
                        string type = "";
                        if (part_uGrid.GetCell(PickCellID).GetCellType().ToString() == "3")
                        {
                            type = "line";
                        }else if (part_uGrid.GetCell(PickCellID).GetCellType().ToString() == "7")
                        {
                            type = "surface";
                        }
                        log += $"Selected {type} ID: " + PickCellID;
                        vtkIdList listofpoints = part_uGrid.GetCell(PickCellID).GetPointIds();
                        log += " [N. points=" + listofpoints.GetNumberOfIds() + "](";
                        for (int i=0;i< listofpoints.GetNumberOfIds(); i++)
                        {
                            if (i > 0)
                                log += ", ";
                            log += "P"+listofpoints.GetId(i).ToString();
                        }
                        log += ")";
                        mainfrm.LogBox_add(log);
                    }
                }
                //vtkAreaPicker areaPicker = vtkAreaPicker.New();
                //vtkRenderWindowInteractor renderWindowInteractor = renderWindow.GetInteractor();
                //renderWindowInteractor.SetPicker(areaPicker);

                vtkDataSetMapper SelectionMapper = vtkDataSetMapper.New();
#if VTK_MAJOR_VERSION_5
 SelectionMapper.SetInput(SelectionunStructuredGrid);
#else
     SelectionMapper.SetInputData(SelectionunStructuredGrid);          
#endif


                SelectionActor.SetMapper(SelectionMapper);
                SelectionActor.VisibilityOn();
                SelectionActor.GetProperty().SetPointSize(7);
                SelectionActor.GetProperty().SetLineWidth(3);
                SelectionActor.GetProperty().SetColor(1, 0, 0);

                Renderer.AddActor(SelectionActor);
            }

        }

    }
}
