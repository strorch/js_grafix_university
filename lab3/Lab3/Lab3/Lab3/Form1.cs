using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algebra;

namespace Lab3
{
    public partial class Form1 : Form
    {
        const double RotationAngle = Math.PI / 36;
        const int TranslationValue = 5;
        const double DelationValue = 0.1;


        Matrix RavenPoints;
        Point[] RavenPointsArray;
        double[] Combinations;
        const int CurvePointsCount = 100;
        const int BezierCurveNodesCount = 3;
        double BezierParamStep;

        Matrix RavenPointsDefault;
        const int SizeCoefficient = 30;
        const int MovementX = 200;
        const int MovementY = 100;
        int RavenPointsCount;

        bool Mode3D;
        const int ParametricPointsCount = 500;
        const double TopParametricBorder = 7 * Math.PI;
        double ParametricStep = TopParametricBorder / (ParametricPointsCount - 1);

        Matrix ParametricPoints3D;
        Matrix ParametricPoints3DDefault;
        Matrix ProjectedPoints3D;


        Matrix SplinePoints3D;
        double[] tNodes;
        Matrix Ps;
        Matrix dPs;


        Point[] Points3DArray;

        Matrix ProjectionMatrix;

        Bitmap Canvas;
        Graphics CanvasGraphics;
        Pen CurvePen;
        SolidBrush WhiteBrush;

        bool DrawSpline
        {
            get => checkBox3D.CheckedIndices[0] == 1;
        }

        Action<Keys> KeyOperation;

        MultiDimensionalVector Average2D;
        MultiDimensionalVector Average3D;

        #region Matrixes
        Matrix[] Translations2DPositive;
        Matrix[] Translations2DNegative;

        Matrix[] Delations2DPositive;
        Matrix[] Delations2DNegative;

        Matrix Rotation2DPositive;
        Matrix Rotation2DNegative;

        Matrix[] Reflections2D;

        Matrix[] Translations3DPositive;
        Matrix[] Translations3DNegative;

        Matrix[] Delations3DPositive;
        Matrix[] Delations3DNegative;

        Matrix[] Rotations3DPositive;
        Matrix[] Rotations3DNegative;

        Matrix[] Reflections3D;
        #endregion

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            checkBox3D.SetItemChecked(0, true);
            checkBox3D.ItemCheck += CheckSpline;
            KeyDown += KeyPressed;
            SplineNodesNumeric.ValueChanged += ChangeTable;
            SetGrid();

            KeyOperation = (x) => { };
            KeyPreview = true;

            Mode3D = false;

            RavenPointsDefault = new Matrix(new double[,] {
            { MovementX + SizeCoefficient * 3, MovementX + SizeCoefficient * 6, MovementX + SizeCoefficient * 7,
                    MovementX + SizeCoefficient * 9, MovementX + SizeCoefficient * 10, MovementX + SizeCoefficient * 15,
                    MovementX + SizeCoefficient * 16, MovementX + SizeCoefficient * 16, MovementX + SizeCoefficient * 15,
                    MovementX + SizeCoefficient * 15, MovementX + SizeCoefficient * 19, MovementX + SizeCoefficient * 18,
                    MovementX + SizeCoefficient * 15, MovementX + SizeCoefficient * 11, MovementX + SizeCoefficient * 10,
                    /*MovementX + SizeCoefficient * 8,*/ MovementX + SizeCoefficient * 9, /*MovementX + SizeCoefficient * 6,*/
                    MovementX + SizeCoefficient * 7, MovementX + SizeCoefficient * 4, MovementX + SizeCoefficient * 2,
                    MovementX + SizeCoefficient * 0, MovementX + SizeCoefficient * 6, /*MovementX + SizeCoefficient * 2,*/
                    MovementX + SizeCoefficient * 5, MovementX + SizeCoefficient * 3},
            { MovementY + SizeCoefficient * 3, MovementY + SizeCoefficient * 5, MovementY + SizeCoefficient * 4,
                    MovementY + SizeCoefficient * 5, MovementY + SizeCoefficient * 7, MovementY + SizeCoefficient * 1,
                    MovementY + SizeCoefficient * 4, MovementY + SizeCoefficient * 7, MovementY + SizeCoefficient * 10,
                    MovementY + SizeCoefficient * 12, MovementY + SizeCoefficient * 15, MovementY + SizeCoefficient * 18,
                    MovementY + SizeCoefficient * 20, MovementY + SizeCoefficient * 19, MovementY + SizeCoefficient * 15,
                    /*MovementY + SizeCoefficient * 18,*/ MovementY + SizeCoefficient * 14,/* MovementY + SizeCoefficient * 18,*/
                    MovementY + SizeCoefficient * 14, MovementY + SizeCoefficient * 14, MovementY + SizeCoefficient * 13,
                    MovementY + SizeCoefficient * 11, MovementY + SizeCoefficient * 8, /*MovementY + SizeCoefficient * 5,*/
                    MovementY + SizeCoefficient * 6, MovementY + SizeCoefficient * 3},
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1} });

            RavenPoints = RavenPointsDefault.Copy();
            FindAverage2D();

            RavenPointsCount = RavenPoints.Width;

            BezierParamStep = 1d / (CurvePointsCount - 1);

            //RavenPointsArray = new Point[tCount];
            RavenPointsArray = new Point[RavenPoints.Width / (BezierCurveNodesCount - 1) * CurvePointsCount];


            for (int i = 0; i < RavenPointsArray.Length; i++)
                RavenPointsArray[i] = new Point();

            CreateCombinations();

            SetImage();

            CurvePen = new Pen(Color.Black, 2);
            WhiteBrush = new SolidBrush(Color.White);

            InitializeMatrixes();

            SetParametricPoints3D();
            FindAverage3D();

            Points3DArray = new Point[ParametricPoints3DDefault.Width];
            for (int i = 0; i < Points3DArray.Length; i++)
                Points3DArray[i] = new Point();

            DrawPoints2D();

        }

        void InitializeMatrixes()
        {
            Translations2DPositive = new Matrix[] {
                Matrix.GetTranslationMatrix2D(TranslationValue, 0),
                Matrix.GetTranslationMatrix2D(0, TranslationValue)};

            Translations2DNegative = new Matrix[] {
                Matrix.GetTranslationMatrix2D(-TranslationValue, 0),
                Matrix.GetTranslationMatrix2D(0, -TranslationValue)};

            Delations2DPositive = new Matrix[] {
                Matrix.GetDelationMatrix2D(1 + DelationValue, 1,  true),
                Matrix.GetDelationMatrix2D(1, 1 + DelationValue,  true)};
            Delations2DNegative = new Matrix[] {
                Matrix.GetDelationMatrix2D(1 - DelationValue, 1,  true),
                Matrix.GetDelationMatrix2D(1, 1 - DelationValue,  true)};

            Rotation2DPositive = Matrix.GetRotationMatrix2D(RotationAngle, true);
            Rotation2DNegative = Matrix.GetRotationMatrix2D(-RotationAngle, true);

            Reflections2D = new Matrix[] {
                Matrix.GetReflectionMatrix2D(Matrix.Axes.oX, true),
                Matrix.GetReflectionMatrix2D(Matrix.Axes.oY, true)};

            Translations3DPositive = new Matrix[] {
                Matrix.GetTranslationMatrix3D(TranslationValue, 0, 0),
                Matrix.GetTranslationMatrix3D(0, TranslationValue, 0),
                Matrix.GetTranslationMatrix3D(0, 0, TranslationValue)};
            Translations3DNegative = new Matrix[] {
                Matrix.GetTranslationMatrix3D(-TranslationValue, 0, 0),
                Matrix.GetTranslationMatrix3D(0, -TranslationValue, 0),
                Matrix.GetTranslationMatrix3D(0, 0, -TranslationValue)};

            Delations3DPositive = new Matrix[] {
                Matrix.GetDelationMatrix3D(1 + DelationValue, Matrix.Axes.oX, true),
                Matrix.GetDelationMatrix3D(1 + DelationValue, Matrix.Axes.oY, true),
                Matrix.GetDelationMatrix3D(1 + DelationValue, Matrix.Axes.oZ, true)};
            Delations3DNegative = new Matrix[] {
                Matrix.GetDelationMatrix3D(1 - DelationValue, Matrix.Axes.oX, true),
                Matrix.GetDelationMatrix3D(1 - DelationValue, Matrix.Axes.oY, true),
                Matrix.GetDelationMatrix3D(1 - DelationValue, Matrix.Axes.oZ, true)};

            Rotations3DPositive = new Matrix[] {
                Matrix.GetRotationMatrix3D(RotationAngle, Matrix.Axes.oX, true),
                Matrix.GetRotationMatrix3D(RotationAngle, Matrix.Axes.oY, true),
                Matrix.GetRotationMatrix3D(RotationAngle, Matrix.Axes.oZ, true)};
            Rotations3DNegative = new Matrix[] {
                Matrix.GetRotationMatrix3D(-RotationAngle, Matrix.Axes.oX, true),
                Matrix.GetRotationMatrix3D(-RotationAngle, Matrix.Axes.oY, true),
                Matrix.GetRotationMatrix3D(-RotationAngle, Matrix.Axes.oZ, true)};

            Reflections3D = new Matrix[] {
                Matrix.GetReflectionMatrix3D(Matrix.Planes.XoY, true),
                Matrix.GetReflectionMatrix3D(Matrix.Planes.XoZ, true),
                Matrix.GetReflectionMatrix3D(Matrix.Planes.YoZ, true)};

            ProjectionMatrix = new EMatrix(4);
            ProjectionMatrix.SetValue(2, 2, 0);
            ProjectionMatrix.SetValue(0, 2, Math.Atan(2 * Math.PI / 3) * Math.Cos(Math.PI / 4));
            ProjectionMatrix.SetValue(1, 2, Math.Atan(2 * Math.PI / 3) * Math.Sin(Math.PI / 4));
            
        }

        void SetParametricPoints3D()
        {
            double[,] Values = new double[4, ParametricPointsCount];
            for (int i = 0; i < ParametricPointsCount; i++)
            {
                Values[0, i] = X(i * ParametricStep);
                Values[1, i] = Y(i * ParametricStep);
                Values[2, i] = Z(i * ParametricStep);
                Values[3, i] = 1;
            }

            ParametricPoints3DDefault = new Matrix(Values);
            ParametricPoints3D = ParametricPoints3DDefault.Copy();
        }
        void SetSplineNodes()
        {

        }
        void SetImage()
        {
            Canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = Canvas;

            CanvasGraphics = Graphics.FromImage(Canvas);
        }
        void FindAverage2D()
        {
            Average2D = new MultiDimensionalVector(3);
            Average2D[0] = RavenPoints.GetRow(0).ToArray().Average();
            Average2D[1] = RavenPoints.GetRow(1).ToArray().Average();
        }
        void FindAverage3D()
        {
            Average3D = new MultiDimensionalVector(4);
            Average3D[0] = ParametricPoints3D.GetRow(0).ToArray().Average();
            Average3D[1] = ParametricPoints3D.GetRow(1).ToArray().Average();
            Average3D[2] = ParametricPoints3D.GetRow(2).ToArray().Average();
        }
        void MovePoints(ref Matrix M, MultiDimensionalVector V) => M += V;


        void CreateCombinations()
        {
            //Combinations = new double[RavenPointsCount];
            Combinations = new double[BezierCurveNodesCount];

            for (int i = 0; i < Combinations.Length; i++)
            {
                if (i > RavenPointsCount - i - 1)
                {
                    Combinations[i] = Factorial(Combinations.Length - 1, i) / Factorial(Combinations.Length - 1 - i);
                }
                else
                    Combinations[i] = Factorial(Combinations.Length - 1, Combinations.Length - 1 - i) / Factorial(i);
            }
        }
        long Factorial(int a, int b)
        {
            if (a <= b) return 1;
            else return a * Factorial(a - 1, b);
        }
        long Factorial(int a)
        {
            if (a <= 1) return 1;
            else return a * Factorial(a - 1);
        }

        double BezierSpline(int i, double t) => Combinations[i] * Math.Pow(t, i) * Math.Pow(1 - t, BezierCurveNodesCount - i - 1);

        double X(double t) => 2 * (t - Math.Sin(t)) * 10;
        double Y(double t) => 2 * (1 - Math.Cos(t)) * 10;
        double Z(double t) => 8 * Math.Cos(t / 2) * 10;

        //double X(double t) => Math.Sin(t) * 50;
        //double Y(double t) => Math.Cos(t) * 50;
        //double Z(double t) => Math.Cos(t) * 50;

        MultiDimensionalVector P(double t) => new MultiDimensionalVector(X(t), Y(t), Z(t));

        double dX(double t) => 2 * (1 - Math.Cos(t)) * 10;
        double dY(double t) => 2 * Math.Sin(t) * 10;
        double dZ(double t) => -4 * Math.Sin(t / 2) * 10;
        
        MultiDimensionalVector dP(double t) => new MultiDimensionalVector(dX(t), dY(t), dZ(t));
        


        void Draw()
        {
            if (Mode3D)
                DrawPoints3D();
            else
                DrawPoints2D();
        }
        void DrawPoints2D()
        {
            double B;
            double SumX, SumY;
            for (int k = 0, i, j; k < RavenPoints.Width - BezierCurveNodesCount + 1; k += BezierCurveNodesCount - 1)
            {
                for (i = 0; i < CurvePointsCount; i++)
                {
                    SumX = 0;
                    SumY = 0;
                    for (j = 0; j < BezierCurveNodesCount; j++)
                    {
                        B = BezierSpline(j, i * BezierParamStep);
                        SumX += B * RavenPoints[0, k + j];
                        SumY += B * RavenPoints[1, k + j];
                    }
                    RavenPointsArray[(k / (BezierCurveNodesCount - 1)) * CurvePointsCount + i].X = (int)SumX;
                    RavenPointsArray[(k / (BezierCurveNodesCount - 1)) * CurvePointsCount + i].Y = (int)SumY;
                }
            }

            CanvasGraphics.FillRectangle(WhiteBrush, 0, 0, Canvas.Width, Canvas.Height);

            CanvasGraphics.DrawImageUnscaled(Properties.Resources.Coordinates2D, new Point(0, pictureBox1.Height - Properties.Resources.Coordinates2D.Height));

            //CanvasGraphics.FillPolygon(new SolidBrush(Color.FromArgb(85, Color.Black)), RavenPointsArray);
            CanvasGraphics.DrawLines(CurvePen, RavenPointsArray);

            for (int i = 0; i < RavenPointsCount; i++)
                CanvasGraphics.DrawImage(Properties.Resources.Cross, new Point((int)RavenPoints[0, i] - Properties.Resources.Cross.Width / 2,
                    (int)RavenPoints[1, i] - Properties.Resources.Cross.Height / 2));

            pictureBox1.Invalidate();
        }
        void DrawPoints3D()
        {
            if (DrawSpline)
            {
                ProjectedPoints3D = ProjectionMatrix * SplinePoints3D;
                Points3DArray = new Point[SplinePoints3D.Width];
            }
            else
            {
                ProjectedPoints3D = ProjectionMatrix * ParametricPoints3D;
                Points3DArray = new Point[ParametricPoints3DDefault.Width];
            }
            for (int i = 0; i < Points3DArray.Length; i++)
            {
                Points3DArray[i].X = (int)(ProjectedPoints3D[0, i] / ProjectedPoints3D[3, i] + Canvas.Width / 2);
                Points3DArray[i].Y = (int)(Canvas.Height / 2 - ProjectedPoints3D[1, i] / ProjectedPoints3D[3, i]);
            }

            CanvasGraphics.FillRectangle(WhiteBrush, 0, 0, Canvas.Width, Canvas.Height);

            CanvasGraphics.DrawImageUnscaled(Properties.Resources.Coordinates, new Point(0, pictureBox1.Height - Properties.Resources.Coordinates.Height));

            CanvasGraphics.DrawLines(CurvePen, Points3DArray);

            pictureBox1.Invalidate();
        }

        void MatrixMultiply(ref Matrix Points, Matrix Operation)
        {
            Points = Operation * Points;
        }
        void Multiply3D(Matrix Operation)
        {
                MatrixMultiply(ref SplinePoints3D, Operation);
                MatrixMultiply(ref ParametricPoints3D, Operation);
        }

        void Translate(Keys Key)
        {
            if (Mode3D)
            {
                Matrix M = DrawSpline ? SplinePoints3D : ParametricPoints3D;
                switch (Key)
                {
                    case Keys.A:
                        Multiply3D(Translations3DNegative[0]);
                        break;
                    case Keys.D:
                        Multiply3D(Translations3DPositive[0]);
                        break;
                    case Keys.W:
                        Multiply3D(Translations3DPositive[1]);
                        break;
                    case Keys.S:
                        Multiply3D(Translations3DNegative[1]);
                        break;
                    case Keys.Q:
                        Multiply3D(Translations3DNegative[2]);
                        break;
                    case Keys.E:
                        Multiply3D(Translations3DPositive[2]);
                        break;
                    default:
                        break;
                }
                FindAverage3D();
                DrawPoints3D();
            }
            else
            {
                switch (Key)
                {
                    case Keys.A:
                        MatrixMultiply(ref RavenPoints, Translations2DNegative[0]);
                        break;
                    case Keys.D:
                        MatrixMultiply(ref RavenPoints, Translations2DPositive[0]);
                        break;
                    case Keys.W:
                        MatrixMultiply(ref RavenPoints, Translations2DNegative[1]);
                        break;
                    case Keys.S:
                        MatrixMultiply(ref RavenPoints, Translations2DPositive[1]);
                        break;
                    default:
                        break;
                }
                FindAverage2D();
                DrawPoints2D();
            }
        }
        void Delay(Keys Key)
        {
            if (Mode3D)
            {
                MovePoints(ref ParametricPoints3D, Average3D * -1);
                switch (Key)
                {
                    case Keys.A:
                        Multiply3D(Delations3DNegative[0]);
                        break;
                    case Keys.D:
                        Multiply3D(Delations3DPositive[0]);
                        break;
                    case Keys.W:
                        Multiply3D(Delations3DPositive[1]);
                        break;
                    case Keys.S:
                        Multiply3D(Delations3DNegative[1]);
                        break;
                    case Keys.Q:
                        Multiply3D(Delations3DNegative[2]);
                        break;
                    case Keys.E:
                        Multiply3D(Delations3DPositive[2]);
                        break;
                    default:
                        break;
                }
                MovePoints(ref ParametricPoints3D, Average3D);
                DrawPoints3D();
            }
            else
            {
                MovePoints(ref RavenPoints, Average2D * -1);
                switch (Key)
                {
                    case Keys.A:
                        MatrixMultiply(ref RavenPoints, Delations2DNegative[0]);
                        break;
                    case Keys.D:
                        MatrixMultiply(ref RavenPoints, Delations2DPositive[0]);
                        break;
                    case Keys.W:
                        MatrixMultiply(ref RavenPoints, Delations2DPositive[1]);
                        break;
                    case Keys.S:
                        MatrixMultiply(ref RavenPoints, Delations2DNegative[1]);
                        break;
                    default:
                        break;
                }
                MovePoints(ref RavenPoints, Average2D);
                DrawPoints2D();
            }
        }
        void Rotate(Keys Key)
        {
            if (Mode3D)
            {
                MovePoints(ref ParametricPoints3D, Average3D * -1);
                switch (Key)
                {
                    case Keys.A:
                        Multiply3D(Rotations3DNegative[0]);
                        break;
                    case Keys.D:
                        Multiply3D(Rotations3DPositive[0]);
                        break;
                    case Keys.W:
                        Multiply3D(Rotations3DPositive[1]);
                        break;
                    case Keys.S:
                        Multiply3D(Rotations3DNegative[1]);
                        break;
                    case Keys.Q:
                        Multiply3D(Rotations3DNegative[2]);
                        break;
                    case Keys.E:
                        Multiply3D(Rotations3DPositive[2]);
                        break;
                    default:
                        break;
                }
                MovePoints(ref ParametricPoints3D, Average3D);
                DrawPoints3D();
            }
            else
            {
                MovePoints(ref RavenPoints, Average2D * -1);
                switch (Key)
                {
                    case Keys.A:
                        MatrixMultiply(ref RavenPoints, Rotation2DNegative);
                        break;
                    case Keys.D:
                        MatrixMultiply(ref RavenPoints, Rotation2DPositive);
                        break;
                    case Keys.W:
                        MatrixMultiply(ref RavenPoints, Rotation2DNegative);
                        break;
                    case Keys.S:
                        MatrixMultiply(ref RavenPoints, Rotation2DPositive);
                        break;
                    default:
                        break;
                }
                MovePoints(ref RavenPoints, Average2D);
                DrawPoints2D();
            }
        }
        void Reflect(Keys Key)
        {
            if (Mode3D)
            {
                MovePoints(ref ParametricPoints3D, Average3D * -1);
                switch (Key)
                {
                    case Keys.A:
                        Multiply3D(Reflections3D[0]);
                        break;
                    case Keys.D:
                        Multiply3D(Reflections3D[0]);
                        break;
                    case Keys.W:
                        Multiply3D(Reflections3D[1]);
                        break;
                    case Keys.S:
                        Multiply3D(Reflections3D[1]);
                        break;
                    case Keys.Q:
                        Multiply3D(Reflections3D[2]);
                        break;
                    case Keys.E:
                        Multiply3D(Reflections3D[2]);
                        break;
                    default:
                        break;
                }
                MovePoints(ref ParametricPoints3D, Average3D);
                DrawPoints3D();
            }
            else
            {
                MovePoints(ref RavenPoints, Average2D * -1);
                switch (Key)
                {
                    case Keys.A:
                        MatrixMultiply(ref RavenPoints, Reflections2D[0]);
                        break;
                    case Keys.D:
                        MatrixMultiply(ref RavenPoints, Reflections2D[0]);
                        break;
                    case Keys.W:
                        MatrixMultiply(ref RavenPoints, Reflections2D[1]);
                        break;
                    case Keys.S:
                        MatrixMultiply(ref RavenPoints, Reflections2D[1]);
                        break;
                    default:
                        break;
                }
                MovePoints(ref RavenPoints, Average2D);
                DrawPoints2D();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Normal;
            else
                WindowState = FormWindowState.Maximized;

            SetImage();
            Draw();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            if (Mode3D)
            {
                if (DrawSpline)
                    BuildSpline();
                else
                    ParametricPoints3D = ParametricPoints3DDefault.Copy();
            }
            else
                RavenPoints = RavenPointsDefault.Copy();
            Draw();
        }

        private void buttonTranslate_Click(object sender, EventArgs e)
        {
            KeyOperation = Translate;
        }

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            KeyOperation = Rotate;
        }

        private void buttonDelate_Click(object sender, EventArgs e)
        {
            KeyOperation = Delay;
        }

        private void buttonReflect_Click(object sender, EventArgs e)
        {
            KeyOperation = Reflect;
        }

        void CheckSpline(object sender, ItemCheckEventArgs e)
        {
            checkBox3D.ItemCheck -= CheckSpline;
            checkBox3D.SetItemChecked(0, false);
            checkBox3D.SetItemChecked(1, false);

            checkBox3D.SetItemChecked(e.Index, true);

            checkBox3D.ItemCheck += CheckSpline;

            SplineNodesNumeric.Enabled = DrawSpline;
            dataGridNodes.Enabled = DrawSpline;
            Draw();
        }
        void KeyPressed(object sender, KeyEventArgs e)
        {
            KeyOperation(e.KeyData);
        }

        private void Button2D_Click(object sender, EventArgs e)
        {
            if (Mode3D)
                DrawPoints2D();
            Mode3D = false;

            checkBox3D.Enabled = false;
            SplineNodesNumeric.Enabled = false;
            dataGridNodes.Enabled = false;
        }

        private void Button3D_Click(object sender, EventArgs e)
        {
            if (!Mode3D)
                DrawPoints3D();
            Mode3D = true;

            checkBox3D.Enabled = true;
            SplineNodesNumeric.Enabled = DrawSpline;
            dataGridNodes.Enabled = DrawSpline;
        }
        void ChangeTable(object sender, EventArgs e) => SetGrid();
        void SetGrid()
        {
            dataGridNodes.RowCount = (int)SplineNodesNumeric.Value;
            double Step = TopParametricBorder / (dataGridNodes.RowCount - 1);
            tNodes = new double[dataGridNodes.RowCount];
            for (int i = 0; i < dataGridNodes.RowCount; i++)
            {
                tNodes[i] = i * Step;
                dataGridNodes.Rows[i].Cells[0].Value = Math.Round(tNodes[i], 4); 
            }
            BuildSpline();
            if(Mode3D && DrawSpline)
                Draw();
        }

        void BuildSpline()
        {
            SplinePoints3D = new Matrix(4, (tNodes.Length - 1) * CurvePointsCount);
            double Step = TopParametricBorder / (tNodes.Length - 1) / CurvePointsCount;
            MultiDimensionalVector Temp;

            Ps = new Matrix(3, tNodes.Length);
            for (int i = 0; i < Ps.Width; i++)
                Ps.SetColumn(i, P(tNodes[i]).ToArray());

            Matrix SwapMatrix = new Matrix(tNodes.Length);

            for (int i = 0; i < tNodes.Length; i++)
            {
                if (i == 0 || i == tNodes.Length - 1)
                    SwapMatrix.SetValue(i, i, 1);
                else
                {
                    SwapMatrix.SetValue(i, i - 1, tNodes[i + 1]);
                    SwapMatrix.SetValue(i, i, 2 * (tNodes[i] + tNodes[i + 1]));
                    SwapMatrix.SetValue(i, i + 1, tNodes[i]);
                }
                
            }
            MultiDimensionalVector[] Bs = new MultiDimensionalVector[3];
            for (int i = 0, j; i < Bs.Length; i++) // i for X, Y, Z
            {
                Bs[i] = new MultiDimensionalVector(tNodes.Length);
                for (j = 0; j < tNodes.Length; j++)
                {
                    if (j == 0 || j == tNodes.Length - 1)
                        Bs[i][j] = dP(tNodes[j])[i];
                    else
                    {
                        Bs[i][j] = 3d / (tNodes[j + 1] * tNodes[j])
                            * (tNodes[j] * tNodes[j] * (Ps[i, j + 1] - Ps[i, j])
                            + tNodes[j + 1] * tNodes[j + 1] * (Ps[i, j] - Ps[i, j - 1]));
                    }
                }
            }
            dPs = new Matrix(3, tNodes.Length);
            dPs.SetRow(0, SystemOfLinearEquations.SolveBySweep(SwapMatrix, Bs[0]).ToArray());
            dPs.SetRow(1, SystemOfLinearEquations.SolveBySweep(SwapMatrix, Bs[1]).ToArray());
            dPs.SetRow(2, SystemOfLinearEquations.SolveBySweep(SwapMatrix, Bs[2]).ToArray());

            double teta;

            for (int i = 0, t; i < tNodes.Length - 1; i++)
                for(t = 0; t < CurvePointsCount; t++)
                {
                    teta = ((i * CurvePointsCount + t) * Step - tNodes[i]) / (tNodes[i + 1] - tNodes[i]);
                    Temp =
                        (2 * teta * teta * teta - 3 * teta * teta + 1) * Ps.GetColumn(i)
                        + (-2 * teta * teta * teta + 3 * teta * teta) * Ps.GetColumn(i + 1)
                        + teta * (teta * teta - 2 * teta + 1) * tNodes[i + 1] * dPs.GetColumn(i)
                        + teta * (teta * teta * teta - teta) * tNodes[i + 1] * dPs.GetColumn(i + 1);



                    //Temp = Spline((i * CurvePointsCount + t) * Step, i);


                    SplinePoints3D.SetValue(0, i * CurvePointsCount + t, Temp[0]);
                    SplinePoints3D.SetValue(1, i * CurvePointsCount + t, Temp[1]);
                    SplinePoints3D.SetValue(2, i * CurvePointsCount + t, Temp[2]);
                    SplinePoints3D.SetValue(3, i * CurvePointsCount + t, 1);
                }
        }

        MultiDimensionalVector BSpline0(int i) => Ps.GetColumn(i);
        MultiDimensionalVector BSpline1(int i) => dPs.GetColumn(i);
        MultiDimensionalVector BSpline2(int i)
        {
            return 3 * (Ps.GetColumn(i + 1) - Ps.GetColumn(i)) / (tNodes[i + 1] * tNodes[i + 1])
                - 2 * dPs.GetColumn(i) / (tNodes[i + 1])
                - dPs.GetColumn(i + 1) / (tNodes[i + 1]);
        }

        MultiDimensionalVector BSpline3(int i)
        {
            return 2 * (Ps.GetColumn(i) - Ps.GetColumn(i + 1)) / (tNodes[i + 1] * tNodes[i + 1] * tNodes[i + 1])
                + dPs.GetColumn(i) / (tNodes[i + 1] * tNodes[i + 1])
                + dPs.GetColumn(i + 1) / (tNodes[i + 1] * tNodes[i + 1]);
        }

        MultiDimensionalVector Spline(double t, int i)
        {
            return BSpline0(i) + BSpline1(i) * t + BSpline2(i) * t * t + BSpline3(i) * t * t * t;
        }
    }
}