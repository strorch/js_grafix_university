using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Graph_Lab_5
{
    public class PointsView : INotifyPropertyChanged
    {
        private double[][][] points;
        public double[][][] Points
        {
            get => points;
            set
            {
                points = value;
                OnPropertyChanged();

                for(int i = 0; i < Points.Length; i++)
                {
                    if(RobertsAlgorithm.IsVisibleSide(Points[i][0], Points[i][1], Points[i][2], 2000, 45 * Math.PI / 180, 45 * Math.PI / 180))
                        Visible[i] = false;
                    else
                        Visible[i] = true;
                }
                OnPropertyChanged("Visible");
            }
        }

        private double[][] coordinates;
        public double[][] Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value;
                OnPropertyChanged();
            }
        }

        private bool[] visible;
        public bool[] Visible
        {
            get => visible;
            set
            {
                visible = value;
                OnPropertyChanged();
            }
        }

        private double[] observerPoint;
        public double[] ObserverPoint
        {
            get => observerPoint;
            set
            {
                observerPoint = value;
                OnPropertyChanged();
            }
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
            if (RotateVector[0] != 0)
                Points = Points.Select(s => s.Select(
                    p => PointTransformation3D.RotateX(p, RotateVector[0] * Math.PI / 180d)).ToArray()).ToArray();
            if (RotateVector[1] != 0)
                Points = Points.Select(s => s.Select(
                    p => PointTransformation3D.RotateY(p, RotateVector[1] * Math.PI / 180d)).ToArray()).ToArray();
            if (RotateVector[2] != 0)
                Points = Points.Select(s => s.Select(
                    p => PointTransformation3D.RotateZ(p, RotateVector[2] * Math.PI / 180d)).ToArray()).ToArray();
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
            Points = Points.Select(s => s.Select(
                p => PointTransformation3D.Scaling(p, ScalingVector[0], ScalingVector[1], ScalingVector[2])).ToArray()).ToArray();
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
            Points = Points.Select(s => s.Select(
                p => PointTransformation3D.Translation(p, TranslationVector[0], TranslationVector[1], TranslationVector[2])).ToArray()).ToArray();
        }

        private RelayCommand translationCommand;
        public ICommand TranslationCommand
        {
            get => translationCommand;
        }

        public PointsView()
        {
            Visible = new bool[] { true, true, true, true, true, true };

            Points = new double[][][]
            {
                //new double[][] { new double[] {0,100,0,1}, new double[] {0,100,100,1}, new double[] {100,100,100,1}, new double[] {100,100,0,1}, new double[] {0,100,0,1} },
                new double[][] { new double[] {0,0,0,1}, new double[] {100,0,0,1}, new double[] {0,0,100,1}, new double[] {0,0,0,1} },
                new double[][] { new double[] {0,0,100,1}, new double[] {100,0,0,1}, new double[] {0,100,0,1}, new double[] {0,0,100,1} },
                new double[][] { new double[] {0,0,0,1}, new double[] {0,0,100,1}, new double[] {0,100,0,1}, new double[] {0,0,0,1} },
                new double[][] { new double[] {0,0,0,1}, new double[] {0,100,0,1}, new double[] {100,0,0,1}, new double[] {0,0,0,1} },
            };

            Coordinates = new double[][]
            {
                new double[] {200,0,0,1},
                new double[] {0,0,0,1},
                new double[] {0,200,0,1},
                new double[] {0,0,0,1},
                new double[] {0,0,200,1}
            };
            
            ObserverPoint = new double[] { 0, 0, 1000, 1 };

            rotateVector = new double[] { 0, 0, 0 };
            rotateCommand = new RelayCommand(o => Rotate());

            scalingVector = new double[] { 1, 1, 1 };
            scaleCommand = new RelayCommand(o => Scale());

            translationVector = new double[] { 0, 0, 0 };
            translationCommand = new RelayCommand(o => Translation());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
