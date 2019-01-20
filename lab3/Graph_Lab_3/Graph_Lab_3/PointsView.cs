using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graph_Lab_3
{
    public class PointsView : INotifyPropertyChanged
    {
        double[][] points;
        public double[][] Points
        {
            get => points;
            set
            {
                points = value;
                OnPropertyChanged();
            }
        }

        public double[][] points3d,
            points2d;

        double[][] curvesPoints;
        public double[][] CurvesPoints
        {
            get => curvesPoints;
            set
            {
                curvesPoints = value;
                OnPropertyChanged();
            }
        }

        double[][] coordinates;
        public double[][] Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value;
                OnPropertyChanged();
            }
        }

        double[][] coors3d,
            coors2d;


        public void CalculateBezier()
        {
            List<double[]> res = new List<double[]>();
            var rt = Calculation.Curves.BuildBezier(Points);
            double t = 0,
                interval = 1d / 100d;

            while(t <= 1)
            {
                var tmp = rt(t);
                res.Add(new double[] { tmp[0], tmp[1], 1 });
                t += interval;
            }

            CurvesPoints = res.ToArray();
        }

        public void CalculateSpline()
        {
            List<double[]> res = new List<double[]>();
            var rt = Calculation.Splines.GetParamericSplineInterpolant(Points.Select(pt => pt[0]).ToArray(), Points.Select(pt => pt[1]).ToArray(), Points.Select(pt => pt[2]).ToArray());
            double t = 0,
                interval = 1d / 15d;

            for (int i = 1; i < Points.Length; i++)
            {
                while (t <= 1)
                {
                    res.Add(new double[] { rt(t + i).Item1, rt(t + i).Item2, rt(t + i).Item3, 1 });
                    t += interval;
                }
                t = 0;
            }

            CurvesPoints = res.ToArray();
        }

        private double[] rotateVector;
        public double[] RotateVector
        {
            get => rotateVector;
            set
            {
                rotateVector = value;
                OnPropertyChanged();
            }
        }
        public void Rotate()
        {
            if (Points[0].Length == 3)
            {
                Points = Points.Select(p => PointTransformations2D.Turn(p, RotateVector[2] * Math.PI / 180d)).ToArray();
                CurvesPoints = CurvesPoints.Select(p => PointTransformations2D.Turn(p, RotateVector[2] * Math.PI / 180d)).ToArray();
            }
            else
            {
                if (RotateVector[0] != 0)
                {
                    Points = Points.Select(p => PointTransformation3D.RotateX(p, RotateVector[0] * Math.PI / 180d)).ToArray();
                    CurvesPoints = CurvesPoints.Select(p => PointTransformation3D.RotateX(p, RotateVector[0] * Math.PI / 180d)).ToArray();
                }
                if (RotateVector[1] != 0)
                {
                    Points = Points.Select(p => PointTransformation3D.RotateY(p, RotateVector[1] * Math.PI / 180d)).ToArray();
                    CurvesPoints = CurvesPoints.Select(p => PointTransformation3D.RotateY(p, RotateVector[1] * Math.PI / 180d)).ToArray();
                }
                if (RotateVector[2] != 0)
                {
                    Points = Points.Select(p => PointTransformation3D.RotateZ(p, RotateVector[2] * Math.PI / 180d)).ToArray();
                    CurvesPoints = CurvesPoints.Select(p => PointTransformation3D.RotateZ(p, RotateVector[2] * Math.PI / 180d)).ToArray();
                }
            }
        }

        private RelayCommand rotateCommand;
        public ICommand RotateCommand
        {
            get => rotateCommand;
        }

        private double[] scalingVector;
        public double[] ScalingVector
        {
            get => scalingVector;
            set
            {
                scalingVector = value;
                OnPropertyChanged();
            }
        }
        public void Scale()
        {
            if (Points[0].Length == 3)
            {
                Points = Points.Select(p => PointTransformations2D.Scaling(p, ScalingVector[0], ScalingVector[1])).ToArray();
                CurvesPoints = CurvesPoints.Select(p => PointTransformations2D.Scaling(p, ScalingVector[0], ScalingVector[1])).ToArray();
            }
            else
            {
                Points = Points.Select(p => PointTransformation3D.Scaling(p, ScalingVector[0], ScalingVector[1], ScalingVector[2])).ToArray();
                CurvesPoints = CurvesPoints.Select(p => PointTransformation3D.Scaling(p, ScalingVector[0], ScalingVector[1], ScalingVector[2])).ToArray();
            }
        }

        private RelayCommand scaleCommand;
        public ICommand ScaleCommand
        {
            get => scaleCommand;
        }

        private double[] translationVector;
        public double[] TranslationVector
        {
            get => translationVector;
            set
            {
                translationVector = value;
                OnPropertyChanged();
            }
        }
        public void Translation()
        {
            if (Points[0].Length == 3)
            {
                Points = Points.Select(p => PointTransformations2D.Shift(p, TranslationVector[0], TranslationVector[1])).ToArray();
                CurvesPoints = CurvesPoints.Select(p => PointTransformations2D.Shift(p, TranslationVector[0], TranslationVector[1])).ToArray();
            }
            else
            {
                Points = Points.Select(p => PointTransformation3D.Translation(p, TranslationVector[0], TranslationVector[1], TranslationVector[2])).ToArray();
                CurvesPoints = CurvesPoints.Select(p => PointTransformation3D.Translation(p, TranslationVector[0], TranslationVector[1], TranslationVector[2])).ToArray();
            }
        }

        private RelayCommand translationCommand;
        public ICommand TranslationCommand
        {
            get => translationCommand;
        }

        private RelayCommand splineCommand;
        public ICommand SplineCommand
        {
            get => splineCommand;
        }
        public void Spline()
        {
            Points = points3d;
            Coordinates = coors3d;
            CalculateSpline();
        }

        private RelayCommand bezierCommand;
        public ICommand BezierCommand
        {
            get => bezierCommand;
        }
        public void Bezier()
        {
            Points = points2d;
            Coordinates = coors2d;
            CalculateBezier();
        }

        public PointsView()
        {
            rotateVector = new double[] { 0, 0, 0 };
            rotateCommand = new RelayCommand(o => Rotate());

            scalingVector = new double[] { 1, 1, 1 };
            scaleCommand = new RelayCommand(o => Scale());

            translationVector = new double[] { 0, 0, 0 };
            translationCommand = new RelayCommand(o => Translation());

            /*
            points3d = new double[][]
            {
                new double[]{ 350, 250, 0, 1 },
                new double[]{ 250, 52, 80, 1 },
                new double[]{ 50, 70, 150, 1 },
                new double[]{ 65, 300, 200, 1 }
            };
            points2d = points3d.Select(p => new double[] { p[0], p[1], 1 }).ToArray();*/

            double t = 0, interval = 0.25;
            List<double[]> tmp3d = new List<double[]>();
            while(t < 2.5)
            {
                tmp3d.Add(new double[] { 1 - Math.Cos(2 * t), Math.Sin(2 * t), 2 * Math.Cos(t), 1 });
                t += interval;
            }
            t = 0;
            tmp3d.Add(new double[] { 1 - Math.Cos(2 * t), Math.Sin(2 * t), 2 * Math.Cos(t), 1 });
            points3d = tmp3d.ToArray();
            points2d = tmp3d.Select(p => PointTransformation3D.IsometricProection(p)).Select(p => new double[] { p[0], p[1], 1 }).ToArray();

            Points = points3d;

            CalculateSpline();

            coors3d = new double[][]
            {
                new double[]{ 200, 0, 0, 1 },
                new double[]{ 0, 0, 0, 1 },
                new double[]{ 0, 200, 0, 1 },
                new double[]{ 0, 0, 0, 1 },
                new double[]{ 0, 0, 200, 1 }
            };
            coors2d = new double[][]
            {
                new double[]{ 200, 0, 1 },
                new double[]{ 0, 0, 1 },
                new double[]{ 0, 200, 1 }
            };
            Coordinates = coors3d;

            splineCommand = new RelayCommand(o => Spline());
            bezierCommand = new RelayCommand(o => Bezier());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
