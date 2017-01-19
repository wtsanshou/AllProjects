using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Kinect;

namespace _20120601SimonSays
{
    class Simon
    {
        public void Say(Skeleton skeleton,)
        {
            TrackHand(skeleton.Joints[JointType.HandLeft], MainWindow.LeftHandElement);
            TrackHand(skeleton.Joints[JointType.HandRight], RightHandElement);

            switch (this._currentPhase)
            {
                case GamePhase.GameOver:
                    ProcessGameOver(skeleton);
                    break;
                case GamePhase.PlayerPerforming:
                    ProcessPlayerPerforming(skeleton);
                    break;
            }
        }
    }
}
