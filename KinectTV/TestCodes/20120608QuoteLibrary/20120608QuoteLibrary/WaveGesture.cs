using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;

namespace _20120608QuoteLibrary
{
    class WaveGesture
    {

        private const float WAVE_THRESHOLD = 0.1f;
        private const int WAVE_MOVEMENT_TIMEOUT = 5000;
        private const int REOUIRED_ITERATIONS = 4;

        private const int LEFT_HAND = 0;
        private const int RIGHT_HAND = 1;

        private int waveCount = 0;

        public event EventHandler GestureDetected;

        private WaveGestureTracker[,] _PlayerWaveTracker = new WaveGestureTracker[6, 2];

        //public event EventHandler GestureDetected;

        public void Update(Skeleton[] skeletons, long frameTimestamp,ref int count)
        {
            if (skeletons != null)
            {
                Skeleton skeleton;

                for (int i = 0; i < skeletons.Length; i++)
                {
                    skeleton = skeletons[i];
                    if (skeleton.TrackingState != SkeletonTrackingState.NotTracked)
                    {
                        TrackWave(skeleton, true, ref this._PlayerWaveTracker[i, LEFT_HAND], frameTimestamp,ref count);
                        TrackWave(skeleton, false, ref this._PlayerWaveTracker[i, RIGHT_HAND], frameTimestamp,ref count);
                    }
                    else
                    {
                        this._PlayerWaveTracker[i, LEFT_HAND].Reset();
                        this._PlayerWaveTracker[i, RIGHT_HAND].Reset();
                    }
                }

            }
        }

        private void TrackWave(Skeleton skeleton, bool isLeft, ref WaveGestureTracker tracker, long timestamp,ref int count)
        {
            JointType handJointId = (isLeft) ? JointType.HandLeft : JointType.HandRight;
            JointType elbowJointId = (isLeft) ? JointType.ElbowLeft : JointType.ElbowRight;
            Joint hand = skeleton.Joints[handJointId];
            Joint elbow = skeleton.Joints[elbowJointId];

            if (hand.TrackingState != JointTrackingState.NotTracked && elbow.TrackingState != JointTrackingState.NotTracked)
            {
                if (tracker.State == WaveGestureState.InProgress && tracker.Timestamp + WAVE_MOVEMENT_TIMEOUT < timestamp)
                {
                    tracker.UpdateState(WaveGestureState.Failure, timestamp);
                }
                else if (hand.Position.Y > elbow.Position.Y)
                {
                    if (hand.Position.X <= elbow.Position.X - WAVE_THRESHOLD)
                    {
                        tracker.UpdatePosition(WavePosition.Left, timestamp);
                    }
                    else if (hand.Position.X >= elbow.Position.X + WAVE_THRESHOLD)
                    {
                        tracker.UpdatePosition(WavePosition.Right, timestamp);
                    }
                    else
                    {
                        tracker.UpdatePosition(WavePosition.Neutral, timestamp);
                    }

                    if (tracker.State != WaveGestureState.Success && tracker.IterationCount == REOUIRED_ITERATIONS)
                    {
                        tracker.UpdateState(WaveGestureState.Success, timestamp);
                        //Do what you want in this place!!!!!
                        waveCount++;
                        count = waveCount;

                    }
                }
                else
                {
                    if (tracker.State == WaveGestureState.InProgress)
                    {
                        tracker.UpdateState(WaveGestureState.Failure, timestamp);
                    }
                    else
                    {
                        tracker.Reset();
                    }
                }
            }
            else
            {
                tracker.Reset();
            }
        }
        // }

        private enum WavePosition
        {
            None = 0,
            Left = 1,
            Right = 2,
            Neutral = 3
        }

        private enum WaveGestureState
        {
            None = 0,
            Success = 1,
            Failure = 2,
            InProgress = 3


        }

        private struct WaveGestureTracker
        {
            public int IterationCount;
            public WaveGestureState State;
            public long Timestamp;
            public WavePosition StartPosition;
            public WavePosition CurrentPosition;

            public void Reset()
            {
                IterationCount = 0;
                State = WaveGestureState.None;
                Timestamp = 0;
                StartPosition = WavePosition.None;
                CurrentPosition = WavePosition.None;
            }

            public void UpdateState(WaveGestureState state, long timestamp)
            {
                State = state;
                Timestamp = timestamp;
            }

            public void UpdatePosition(WavePosition position, long timestamp)
            {
                if (CurrentPosition != position)
                {
                    if (position == WavePosition.Left || position == WavePosition.Right)
                    {
                        if (State != WaveGestureState.InProgress)
                        {
                            State = WaveGestureState.InProgress;
                            IterationCount = 0;
                            StartPosition = position;
                        }
                        IterationCount++;
                    }
                    CurrentPosition = position;
                    Timestamp = timestamp;
                }
            }
        }
    }
}
