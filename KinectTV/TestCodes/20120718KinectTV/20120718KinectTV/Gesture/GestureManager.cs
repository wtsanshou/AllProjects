using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows;
using System.ComponentModel;
using System.IO.Ports;

namespace _20120718KinectTV.Gesture
{
    class GestureManager : INotifyPropertyChanged
    {
        
        private Skeleton _skeleton;
        private KinectSensor _kinectSensor;
        private bool _isBackGestureActive = false;
        private bool _isForwardGestureActive = false;

        private bool _isUpGestureActive = false;
        private int _commandType = 0;

        private bool _isArmsExtended = false;

        private SerialPort _serialPort = new SerialPort();
        private const string PORTNAME = "COM3";

        private const string IMAGEPATH = "/20120718KinectTV;component/Images/";

        private int _GuidancePages = 0;

        public GestureManager(KinectSensor kinectSensor)
        {
            this._kinectSensor = kinectSensor;
            PopulatePoseLibrary();
            GestureType = IMAGEPATH + "WatchTV.jpg";

            GestureGuidanceColumn1 = "0";
            GestureGuidanceVisibility1 = "Visible";

            GestureGuidanceColumn2 = "1";
            GestureGuidanceVisibility2 = "Hidden";

            GestureGuidanceColumn3 = "1";
            GestureGuidanceVisibility3 = "Hidden";
        }

        public void ClosePort()
        {
            if (this._serialPort.IsOpen)
            {
                this._serialPort.Close();
            }
        }

        public void TrackPose(Skeleton skeleton)
        {
            
            this._skeleton = skeleton;

            Joint head = new Joint();
            Joint rightHand = new Joint();
            Joint leftHand = new Joint();

            head = skeleton.Joints[JointType.Head];
            rightHand = skeleton.Joints[JointType.HandRight];
            leftHand = skeleton.Joints[JointType.HandLeft];

            ProcessUpGesture(head, leftHand);

            ProcessPose();

            ProcessForwardBackGesture(head, rightHand, leftHand);
        }

        

        public class PoseAngle
        {
            public JointType CenterJoint { get; private set; }
            public JointType AngleJoint { get; private set; }
            public double Angle { get; private set; }
            public double Threshold { get; private set; }

            public PoseAngle(JointType centerJoint, JointType angleJoint, double angle, double threshold)
            {
                this.CenterJoint = centerJoint;
                this.AngleJoint = angleJoint;
                this.Angle = angle;
                this.Threshold = threshold;
            }
        }

        public struct Pose
        {
            public string Title;
            public PoseAngle[] Angles;
        }

        private Pose[] _PoseLibrary;
        private Pose _StartPose;

        private void PopulatePoseLibrary()
        {
            this._PoseLibrary = new Pose[2];
            PoseAngle[] angles;

            //Arms Extended
            this._StartPose = new Pose();
            this._StartPose.Title = "Start Pose";
            angles = new PoseAngle[4];
            angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 180, 20);
            angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 0, 20);
            this._StartPose.Angles = angles;

            //Both Hands Up
            this._PoseLibrary[0] = new Pose();
            this._PoseLibrary[0].Title = "Surrender!";
            angles = new PoseAngle[4];
            angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 90, 20);
            angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 90, 20);
            this._PoseLibrary[0].Angles = angles;

            //Both Hands Down
            this._PoseLibrary[1] = new Pose();
            this._PoseLibrary[1].Title = "Scarecrow!";
            angles = new PoseAngle[4];
            angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 270, 20);
            angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 270, 20);
            this._PoseLibrary[1].Angles = angles;
        }

        private bool IsPose(Skeleton skeleton, Pose pose)
        {
            bool isPose = true;
            double angle;
            double poseAngle;
            double poseThreshold;
            double loAngle;
            double hiAngle;

            for (int i = 0; i < pose.Angles.Length && isPose; i++)
            {
                poseAngle = pose.Angles[i].Angle;
                poseThreshold = pose.Angles[i].Threshold;
                angle = GetJointAngle(skeleton.Joints[pose.Angles[i].CenterJoint], skeleton.Joints[pose.Angles[i].AngleJoint]);

                hiAngle = poseAngle + poseThreshold;
                loAngle = poseAngle - poseThreshold;

                if (hiAngle >= 360 || loAngle < 0)
                {
                    loAngle = (loAngle < 0) ? 360 + loAngle : loAngle;
                    hiAngle = hiAngle % 360;
                    isPose = !(loAngle > angle && angle > hiAngle);
                }
                else
                {
                    isPose = (loAngle <= angle && hiAngle >= angle);
                }
            }
            return isPose;
        }

        private double GetJointAngle(Joint zeroJoint, Joint angleJoint)
        {
            Point zeroPoint = getJointPoint(zeroJoint);
            Point anglePoint = getJointPoint(angleJoint);
            Point x = new Point(zeroPoint.X + anglePoint.X, zeroPoint.Y);

            double a;
            double b;
            double c;

            a = Math.Sqrt(Math.Pow(zeroPoint.X - anglePoint.X, 2) + Math.Pow(zeroPoint.Y - anglePoint.Y, 2));
            b = anglePoint.X;
            c = Math.Sqrt(Math.Pow(anglePoint.X - x.X, 2) + Math.Pow(anglePoint.Y - x.Y, 2));

            double angleRad = Math.Acos((a * a + b * b - c * c) / (2 * a * b));
            double angleDeg = angleRad * 180 / Math.PI;

            if (zeroPoint.Y < anglePoint.Y)
            {
                angleDeg = 360 - angleDeg;
            }
            return angleDeg;
        }

        private Point getJointPoint(Joint joint)
        {
            DepthImagePoint point = this._kinectSensor.MapSkeletonPointToDepth(joint.Position, DepthImageFormat.Resolution640x480Fps30);

            return new Point((double)point.X, (double)point.Y);
        }

        //


        private void ProcessForwardBackGesture(Joint head, Joint rightHand, Joint leftHand)
        {
            if (rightHand.Position.X > head.Position.X + 0.45)
            {
                if (!_isBackGestureActive && !_isForwardGestureActive)
                {
                    _isForwardGestureActive = true;
                    //System.Windows.Forms.SendKeys.SendWait("{Right}");

                    if (this._commandType == 1)
                    {
                        Command = "change guidance right.....";
                        this._GuidancePages = this._GuidancePages + 1;
                        
                        var key = this._GuidancePages % 3;
                        switch (key)
                        {
                            case 0:
                                GestureGuidanceColumn2 = "1";
                                GestureGuidanceVisibility2 = "Hidden";

                                GestureGuidanceColumn3 = "1";
                                GestureGuidanceVisibility3 = "Hidden";

                                GestureGuidanceColumn1 = "0";
                                GestureGuidanceVisibility1 = "Visible";

                                break;
                            case 1:
                                GestureGuidanceColumn1 = "1";
                                GestureGuidanceVisibility1 = "Hidden";

                                GestureGuidanceColumn3 = "1";
                                GestureGuidanceVisibility3 = "Hidden";

                                GestureGuidanceColumn2 = "0";
                                GestureGuidanceVisibility2 = "Visible";

                                break;
                            case 2:
                                GestureGuidanceColumn1 = "1";
                                GestureGuidanceVisibility1 = "Hidden";

                                GestureGuidanceColumn2 = "1";
                                GestureGuidanceVisibility2 = "Hidden";

                                GestureGuidanceColumn3 = "0";
                                GestureGuidanceVisibility3 = "Visible";

                                break;
                            default:
                                break;
                        }
                    }
                    else if (this._commandType == 2)
                    {
                        Command = "Next Page.....";
                        serialSend("Next Page");
                    }
                    else if (this._commandType == 3)
                    {
                        Command = "Sound Increase.....";
                        serialSend("Sound Increase");
                    }
                }
            }
            else
            {
                _isForwardGestureActive = false;
            }

            if (leftHand.Position.X < head.Position.X - 0.45)
            {
                if (!_isBackGestureActive && !_isForwardGestureActive)
                {
                    _isBackGestureActive = true;
                    //System.Windows.Forms.SendKeys.SendWait("{Left}");
                    if (this._commandType == 1)
                    {
                        Command = "change guidance left.....";
                        this._GuidancePages = this._GuidancePages - 1;
                        if (this._GuidancePages < 0)
                        {
                            this._GuidancePages = 2;
                        }

                        var key = this._GuidancePages % 3;
                        switch (key)
                        {
                            case 0:
                                GestureGuidanceColumn2 = "1";
                                GestureGuidanceVisibility2 = "Hidden";

                                GestureGuidanceColumn3 = "1";
                                GestureGuidanceVisibility3 = "Hidden";

                                GestureGuidanceColumn1 = "0";
                                GestureGuidanceVisibility1 = "Visible";

                                break;
                            case 1:
                                GestureGuidanceColumn1 = "1";
                                GestureGuidanceVisibility1 = "Hidden";

                                GestureGuidanceColumn3 = "1";
                                GestureGuidanceVisibility3 = "Hidden";

                                GestureGuidanceColumn2 = "0";
                                GestureGuidanceVisibility2 = "Visible";

                                break;
                            case 2:
                                GestureGuidanceColumn1 = "1";
                                GestureGuidanceVisibility1 = "Hidden";

                                GestureGuidanceColumn2 = "1";
                                GestureGuidanceVisibility2 = "Hidden";

                                GestureGuidanceColumn3 = "0";
                                GestureGuidanceVisibility3 = "Visible";

                                break;
                            default:
                                break;
                        }
                    }
                    else if (this._commandType == 2)
                    {
                        Command = "Previous Page.....";
                        serialSend("Previous Page");
                    }
                    else if (this._commandType == 3)
                    {
                        Command = "Sound Decrease.....";
                        serialSend("Sound Decrease");
                    }
                }
            }
            else
            {
                _isBackGestureActive = false;
            }
        }

        
        private void ProcessUpGesture(Joint head, Joint leftHand)
        {
            if (leftHand.Position.Y > head.Position.Y + 0.25)
            {
                if (!_isUpGestureActive)
                {
                    
                    //System.Windows.Forms.SendKeys.SendWait("{Right}");
                    this._commandType = this._commandType + 1;
                    this._commandType = this._commandType % 4;
                    
                    switch (this._commandType)
                    { 
                        case 0:
                            CommandType = "default...";
                            GestureType = IMAGEPATH + "WatchTV.jpg";
                            break;
                        case 1:
                            CommandType = "Guidance...";
                            GestureType = IMAGEPATH + "Guidence.jpg";
                            break;
                        case 2:
                            CommandType = "Channel...";
                            GestureType = IMAGEPATH + "ChannelControl.jpg";
                            break;
                        case 3:
                            CommandType = "Sound...";
                            GestureType = IMAGEPATH + "SoundControl.jpg";
                            break;
                        default:
                            CommandType = "error...";
                            GestureType = IMAGEPATH + "Error.jpg";
                            break;
                    }
                    _isUpGestureActive = true;
                }
            }
            else
            {
                _isUpGestureActive = false;
            }
        }

        private void ProcessPose()
        {
            if (IsPose(this._skeleton, this._StartPose))
            {
                if (!this._isArmsExtended)
                {
                    if (this._commandType == 2)
                    {
                        Command = "turn on/off the TV.....";
                        serialSend("Turn On Off");
                    }
                    this._isArmsExtended = true;
                }
            }
            else
            {
                this._isArmsExtended = false;
            }

            /*
        else if (IsPose(this._skeleton, this._PoseLibrary[0]))
        {
            Console.WriteLine(this._PoseLibrary[0].Title);
        }
        else if (IsPose(this._skeleton, this._PoseLibrary[1]))
        {
            Console.WriteLine(this._PoseLibrary[1].Title);
        }
             * */
        }

        private void serialSend(String command)
        {
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.PortName = PORTNAME;
                    _serialPort.Open();
                }
                if (_serialPort.IsOpen)
                {
                    _serialPort.Write(command);
                    Console.WriteLine(command);
                }
            }
            catch (Exception e)
            { }
        }

        private string _CommandType;
        public string CommandType
        {
            get { return _CommandType; }
            set
            {
                _CommandType = value;
                OnPropertyChanged("CommandType");
            }
        }

        private string _Command;
        public string Command
        {
            get { return _Command; }
            set
            {
                _Command = value;
                OnPropertyChanged("Command");
            }
        }

        private string _GestureType;
        public string GestureType
        {
            get { return _GestureType; }
            set
            {
                _GestureType = value;
                OnPropertyChanged("GestureType");
            }
        }

        private string _gestureGuidanceColumn1;
        public string GestureGuidanceColumn1
        {
            get { return _gestureGuidanceColumn1; }
            set
            {
                _gestureGuidanceColumn1 = value;
                OnPropertyChanged("GestureGuidanceColumn1");
            }
        }

        private string _gestureGuidanceVisibility1;
        public string GestureGuidanceVisibility1
        {
            get { return _gestureGuidanceVisibility1; }
            set
            {
                _gestureGuidanceVisibility1 = value;
                OnPropertyChanged("GestureGuidanceVisibility1");
            }
        }

        private string _gestureGuidanceColumn2;
        public string GestureGuidanceColumn2
        {
            get { return _gestureGuidanceColumn2; }
            set
            {
                _gestureGuidanceColumn2 = value;
                OnPropertyChanged("GestureGuidanceColumn2");
            }
        }

        private string _gestureGuidanceVisibility2;
        public string GestureGuidanceVisibility2
        {
            get { return _gestureGuidanceVisibility2; }
            set
            {
                _gestureGuidanceVisibility2 = value;
                OnPropertyChanged("GestureGuidanceVisibility2");
            }
        }

        private string _gestureGuidanceColumn3;
        public string GestureGuidanceColumn3
        {
            get { return _gestureGuidanceColumn3; }
            set
            {
                _gestureGuidanceColumn3 = value;
                OnPropertyChanged("GestureGuidanceColumn3");
            }
        }

        private string _gestureGuidanceVisibility3;
        public string GestureGuidanceVisibility3
        {
            get { return _gestureGuidanceVisibility3; }
            set
            {
                _gestureGuidanceVisibility3 = value;
                OnPropertyChanged("GestureGuidanceVisibility3");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }
    }
}
