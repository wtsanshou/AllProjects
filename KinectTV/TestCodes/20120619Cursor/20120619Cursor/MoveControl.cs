using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows;

namespace _20120619Cursor
{
    class MoveControl
    {
        private KinectSensor sensor;
        

        public MoveControl(KinectSensor sensor)
        {
            this.sensor = sensor;
            PopulatePoseLibrary();
        }

       

        public bool TrackPose(Skeleton skeleton, bool moveStatus,ref bool isMoveActive)
        {
            
            if (IsPose(skeleton, this._PoseLibrary[1]))
            {
                if (!isMoveActive)
                {
                    isMoveActive = true;
                    return !moveStatus;
                }
                else
                {
                    return moveStatus;
                }
            }
            else
            {
                isMoveActive = false;
                return moveStatus;
            }
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
            this._PoseLibrary[0].Title = "Move Control !";
            angles = new PoseAngle[2];
            angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 90, 20);
            
            this._PoseLibrary[0].Angles = angles;

            //Both Hands Down
            this._PoseLibrary[1] = new Pose();
            this._PoseLibrary[1].Title = "move!";
            angles = new PoseAngle[2];
            angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 180, 20);
            
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
            DepthImagePoint point = this.sensor.MapSkeletonPointToDepth(joint.Position, DepthImageFormat.Resolution640x480Fps30);


            return new Point((double)point.X, (double)point.Y);
        }
    }
}
