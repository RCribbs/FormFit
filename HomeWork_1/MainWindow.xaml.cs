using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using System.Timers;
using Coding4Fun.Kinect.Wpf;
using Coding4Fun.Kinect.Wpf.Controls;


namespace HomeWork_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        int[] lastCords = new int[2];
        bool resetCords = true;
        const int skeletonCount = 6;
        Skeleton[] allSkeletons = new Skeleton[skeletonCount];
        System.Timers.Timer staticPoseTimer = new System.Timers.Timer(), dynamicPoseTimer = new System.Timers.Timer(), displayResetTimer = new System.Timers.Timer();
        string currentStaticPose = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Setup Kinect data
            kinectSensorChooser1.KinectSensorChanged += new DependencyPropertyChangedEventHandler(kinectSensorChooser1_KinectSensorChanged);
            //staticPoseTimer.Elapsed += new ElapsedEventHandler(poseTimer_Elapsed);
            //staticPoseTimer.Interval = 1500;
            dynamicPoseTimer.Elapsed += new ElapsedEventHandler(dynamicPoseTimer_Elapsed);
            dynamicPoseTimer.Interval = 800;
        }

        

        //Ensures proper window closure
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //helperThread.Abort();
            //helperThread.Join();
            StopKinect(kinectSensorChooser1.Kinect);
        }

        void dynamicPoseTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            resetCords = true;
        }

        void kinectSensorChooser1_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            KinectSensor oldSensor = (KinectSensor)e.OldValue;
            StopKinect(oldSensor);

            KinectSensor newSensor = (KinectSensor)e.NewValue;

            newSensor.ColorStream.Enable();
            newSensor.DepthStream.Enable();
            newSensor.SkeletonStream.Enable();

            newSensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(newSensor_AllFramesReady);
            try
            {
                newSensor.Start();
            }
            catch (System.IO.IOException)
            {
                kinectSensorChooser1.AppConflictOccurred();
            }
        }

        void newSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            Skeleton first = GetFirstSkeleton(e);

            if (first == null)
                return;

            //if (curAction != null)
            //    doAction();

            IdentifyGesture(first, e);
        }

        //Properly stops a kinect sensor
        void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    sensor.Stop();

                    if (sensor.AudioSource != null)
                        sensor.AudioSource.Stop();

                    if (sensor.SkeletonStream != null)
                        sensor.SkeletonStream.Disable();

                    if (sensor.ColorStream != null)
                        sensor.ColorStream.Disable();

                    if (sensor.DepthStream != null)
                        sensor.DepthStream.Disable();
                }
            }
        }

        //Re-initializes objects to start state.
        private void Initialize_Screen()
        {
            title.Text = "Choose Your Exercise";
            label1.Content = "Stretch";

            title.Opacity = 1;
            stackPanel1.Opacity = 1;
            back_button.Opacity = 0;
        }

        //Sets up screen for exercise.
        private void Exercise_Screen(string titleText)
        {
            stackPanel1.Opacity = 0;
            back_button_label.Content = "Finish";
            back_button.Opacity = 1;
            title.Text = titleText;
            if ((label1.Content.ToString() == "All Stretch") && titleText != "All Stretch")
                title.Text += " Stretch";
        }

        //Sets up the screen for results after an exercise.
        private void Results_Screen()
        {
            stackPanel1.Opacity = 0;
            back_button.Opacity = 1;
            title.Text = title.Text + " Performance";
            back_button_label.Content = "Return";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (label1.Content.ToString() == "All Stretch")
            {
                Exercise_Screen(label1.Content.ToString());
            }
            else
            {
                label1.Content = "All Stretch";
                title.Text = "Choose Your Stretch";
                back_button_label.Content = "Back";
                back_button.Opacity = 1;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Exercise_Screen(label2.Content.ToString());
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Exercise_Screen(label3.Content.ToString());
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            Exercise_Screen(label4.Content.ToString());
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Exercise_Screen(label5.Content.ToString());
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Exercise_Screen(label6.Content.ToString());
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            if (stackPanel1.Opacity == 1 || title.Text.Contains("Performance"))
                Initialize_Screen();
            else
                Results_Screen();
        }

        private Joint ScalePosition(FrameworkElement element, Joint joint)
        {
            //convert the value to X/Y
            //Joint scaledJoint = joint.ScaleTo(1280, 720); 

            //convert & scale (.3 = means 1/3 of joint distance)
            Joint scaledJoint = joint.ScaleTo(1280, 720, .7f, .7f);

            Canvas.SetLeft(element, scaledJoint.Position.X);
            Canvas.SetTop(element, scaledJoint.Position.Y);

            return scaledJoint;

        }

        Skeleton GetFirstSkeleton(AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                    return null;

                skeletonFrameData.CopySkeletonDataTo(allSkeletons);

                Skeleton first = (from s in allSkeletons
                                  where s.TrackingState == SkeletonTrackingState.Tracked
                                  select s).FirstOrDefault();
                return first;
            }
        }

        private void IdentifyGesture(Skeleton first, AllFramesReadyEventArgs e)
        {
            using (DepthImageFrame depth = e.OpenDepthImageFrame())
            {
                if (depth == null || kinectSensorChooser1.Kinect == null)
                    return;

                //Get skeleton joint data
                DepthImagePoint headDepthPoint =
                    depth.MapFromSkeletonPoint(first.Joints[JointType.Head].Position),

                    leftDepthPoint =
                    depth.MapFromSkeletonPoint(first.Joints[JointType.HandLeft].Position),

                    rightDepthPoint =
                    depth.MapFromSkeletonPoint(first.Joints[JointType.HandRight].Position),

                    rElbowDepthPoint =
                    depth.MapFromSkeletonPoint(first.Joints[JointType.ElbowRight].Position),

                    lElbowDepthPoint =
                    depth.MapFromSkeletonPoint(first.Joints[JointType.ElbowLeft].Position),

                    torsoDepthPoint =
                    depth.MapFromSkeletonPoint(first.Joints[JointType.HipCenter].Position);


                //set scaled position
                //ScalePosition(leftEllipse, first.Joints[JointType.Head]);
                Joint scaledJoint = ScalePosition(rightEllipse, first.Joints[JointType.HandRight]);


                //This is the error tollerance for static poses
                int STATIC_POSE_OFFSET = 25;


                //Record positions for dynamic poses
                if (resetCords)
                {
                    resetCords = false;
                    lastCords = new int[] { rightDepthPoint.X, rightDepthPoint.Y, leftDepthPoint.X, leftDepthPoint.Y, rightDepthPoint.Depth, leftDepthPoint.Depth };
                    dynamicPoseTimer.Start();
                }

                //Read previous positions to recognize dynamic poses
                else
                {
                    //Push right hand
                    if ((lastCords[4] - rightDepthPoint.Depth) > 80 && Math.Abs(lastCords[0] - rightDepthPoint.X) < STATIC_POSE_OFFSET*2
                        && Math.Abs(lastCords[1] - rightDepthPoint.Y) < STATIC_POSE_OFFSET*2)
                    {
                        //selectButton_Click(null, null); //Select an item
                        if (title.Text.ToString() == "Choose Your Exercise" || title.Text.ToString() == "Choose Your Stretch")
                        {
                            if (scaledJoint.Position.X >= 50 && scaledJoint.Position.X <= 300 && scaledJoint.Position.Y >= 75 && scaledJoint.Position.Y <= 275)
                                button1_Click(null, null);
                            else if (scaledJoint.Position.X >= 500 && scaledJoint.Position.X <= 750 && scaledJoint.Position.Y >= 75 && scaledJoint.Position.Y <= 275)
                                button2_Click(null, null);
                            else if (scaledJoint.Position.X >= 900 && scaledJoint.Position.X <= 1150 && scaledJoint.Position.Y >= 75 && scaledJoint.Position.Y <= 275)
                                button3_Click(null, null);
                            else if (scaledJoint.Position.X >= 50 && scaledJoint.Position.X <= 300 && scaledJoint.Position.Y >= 335 && scaledJoint.Position.Y <= 535)
                                button4_Click(null, null);
                            else if (scaledJoint.Position.X >= 500 && scaledJoint.Position.X <= 750 && scaledJoint.Position.Y >= 335 && scaledJoint.Position.Y <= 535)
                                button5_Click(null, null);
                            else if (scaledJoint.Position.X >= 900 && scaledJoint.Position.X <= 1150 && scaledJoint.Position.Y >= 335 && scaledJoint.Position.Y <= 535)
                                button6_Click(null, null);
                        }
                        if (scaledJoint.Position.X >= 1095 && scaledJoint.Position.X <= 1270 && scaledJoint.Position.Y >= 10 && scaledJoint.Position.Y <= 160)
                                back_button_Click(null, null);
                        dynamicPoseTimer.Stop();
                        resetCords = true;
                    }
                }
            }
        }
    }
}
