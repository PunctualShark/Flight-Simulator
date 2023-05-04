using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace moeckly_Flight
{
    /// Isaac Moeckly
    /// CS480 FA21 - Flight Sim

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // iterative process repeatedly calling main to update the main window view
            CompositionTarget.Rendering += main;
        }

        // class variables to track key presses, the plane direction, speed, and wheel retraction
        private bool keyw = false; private bool keys = false;
        private bool keya = false; private bool keyd = false;
        private bool space = false; private bool ctrl = false;
        private bool keyr = false;
        private bool key1 = false; private bool key2 = false;
        private double xdir; private double zdir;
        private double speed = 0.0;
        private double wheelsyoffset = 0.0;
        private int pers = 0;

        private void main(object o, EventArgs args)
        {
            // convert plane rotation to radians
            double Crad = CRoty.Angle * 3.14159 / 180.0;

            // take sin and cos of the radian value to determine how much in one direction the plane is traveling
            xdir = Math.Cos(Crad);
            zdir = Math.Sin(Crad);

            bool tip = false;

            lbl3.Content = "xdir =  " + xdir;
            lbl4.Content = "zdir =  " + zdir;

            if (key1 || key2)
            {
                if (key1) { pers = 1; }
                if (key2) { pers = 2; }
                ChangePers(pers);
            }

            // Move the plane forward iteratively
            MoveForward(xdir, zdir);

            if (keyd)
            {
                // if key pressed is D, change the angle of all parts to turn it to the right
                // these are only retained if the plane is off the ground
                CRotx.Angle -= 0.5;
                FinRotx.Angle -= 0.5;
                //WRotx.Angle -= 0.5;
                Wh1Rotx.Angle -= 0.5;
                Wh2Rotx.Angle -= 0.5;
                Wh3Rotx.Angle -= 0.5;
                WhSRotx.Angle -= 0.5;

                if (speed < 1)
                {
                    // if the speed is below 1, make turning the plane slower; angles of everything change at a lower rate
                    WhSRoty.Angle -= 0.5 * speed;
                    Wh3Roty.Angle -= 0.5 * speed;
                    PRoty.Angle -= 0.5 * speed;
                    Wh2Roty.Angle -= 0.5 * speed;
                    Wh1Roty.Angle -= 0.5 * speed;
                    //WRoty.Angle -= 0.5 * speed;
                    FinRoty.Angle -= 0.5 * speed;
                    CRoty.Angle -= 0.5 * speed;
                    ShadRoty.Angle -= 0.5 * speed;

                    CamRoty.Angle -= 0.5 * speed;
                }
                else
                {
                    // at speed 3, turn is maximum
                    WhSRoty.Angle -= 0.5;
                    Wh3Roty.Angle -= 0.5;
                    PRoty.Angle -= 0.5;
                    Wh2Roty.Angle -= 0.5;
                    Wh1Roty.Angle -= 0.5;
                    //WRoty.Angle -= 0.5;
                    FinRoty.Angle -= 0.5;
                    CRoty.Angle -= 0.5;
                    ShadRoty.Angle -= 0.5;

                    CamRoty.Angle -= 0.5;
                }
            }
            if (keya)
            {
                // key A is the same as D but inverses everything to turn the plane to the left instead of right
                CRotx.Angle += 0.5;
                FinRotx.Angle += 0.5;
                //WRotx.Angle += 0.5;
                Wh1Rotx.Angle += 0.5;
                Wh2Rotx.Angle += 0.5;
                Wh3Rotx.Angle += 0.5;
                WhSRotx.Angle += 0.5;

                if (speed < 1)
                {
                    WhSRoty.Angle += 0.5 * speed;
                    Wh3Roty.Angle += 0.5 * speed;
                    PRoty.Angle += 0.5 * speed;
                    Wh2Roty.Angle += 0.5 * speed;
                    Wh1Roty.Angle += 0.5 * speed;
                    //WRoty.Angle += 0.5 * speed;
                    FinRoty.Angle += 0.5 * speed;
                    CRoty.Angle += 0.5 * speed;
                    ShadRoty.Angle += 0.5 * speed;

                    CamRoty.Angle += 0.5 * speed;
                }
                else
                {
                    WhSRoty.Angle += 0.5;
                    Wh3Roty.Angle += 0.5;
                    PRoty.Angle += 0.5;
                    Wh2Roty.Angle += 0.5;
                    Wh1Roty.Angle += 0.5;
                    //WRoty.Angle += 0.5;
                    FinRoty.Angle += 0.5;
                    CRoty.Angle += 0.5;
                    ShadRoty.Angle += 0.5;

                    CamRoty.Angle += 0.5;
                }
            }
            if (keyw)
            {
                // if key pressed is W, then check if speed is above 3. if not, do nothing
                // W makes the plane fly upwards so for realism, if the plane is not at an appropriate speed, it will not lift off the ground
                if (speed > 6)
                {
                    // if speed is adequate, angle everything upwards along the X and Z axis
                    CTrans.OffsetY += 0.03;
                    CRotz.Angle -= 0.5 * xdir;
                    //CRotx.Angle -= 0.5 * zdir;

                    FinTrans.OffsetY += 0.03;
                    FinRotz.Angle -= 0.5 * xdir;
                    //FinRotx.Angle -= 0.5 * zdir;

                    //WTrans.OffsetY += 0.03;
                    //WRotz.Angle -= 0.5 * xdir;
                    //WRotx.Angle -= 0.5 * zdir;

                    Wh1Trans.OffsetY += 0.03;
                    Wh1Rotz.Angle -= 0.5 * xdir;
                    //Wh1Rotx.Angle -= 0.5 * zdir;

                    Wh2Trans.OffsetY += 0.03;
                    Wh2Rotz.Angle -= 0.5 * xdir;
                    //Wh2Rotx.Angle -= 0.5 * zdir;

                    Wh3Trans.OffsetY += 0.03;
                    Wh3Rotz.Angle -= 0.5 * xdir;

                    PTrans.OffsetY += 0.03;
                    PRotz.Angle -= 0.5 * xdir;

                    WhSTrans.OffsetY += 0.03;
                    WhSRotz.Angle -= 0.5 * xdir;


                    CamTrans.OffsetY += 0.03;
                    CamRotz.Angle -= 0.2 * xdir;
                    CamRotx.Angle -= 0.2 * zdir;

                    if (AltEll.Margin.Top > 199)
                    {
                        AltEll.Margin = new Thickness(927, AltEll.Margin.Top - 0.2, 0, 0);
                    }
                }
            }
            if (keys)
            {
                // if S key is pressed, angle the plane downwards
                CTrans.OffsetY -= 0.03;
                CRotz.Angle += 0.5;
                FinTrans.OffsetY -= 0.03;
                FinRotz.Angle += 0.5;
                Wh1Trans.OffsetY -= 0.03;
                Wh2Trans.OffsetY -= 0.03;
                Wh3Trans.OffsetY -= 0.03;
                //WTrans.OffsetY -= 0.03;
                //WRotz.Angle += 0.5;
                PTrans.OffsetY -= 0.03;
                PRotz.Angle += 0.5;
                WhSTrans.OffsetY -= 0.03;

                if (CTrans.OffsetY >= 1)
                {
                    CamTrans.OffsetY -= 0.03;
                    CamRotz.Angle += 0.2 * xdir;
                    CamRotx.Angle += 0.2 * zdir;
                }

                if (AltEll.Margin.Top < 505)
                {
                    AltEll.Margin = new Thickness(927, AltEll.Margin.Top + 0.2, 0, 0);
                }
            }

            if (!keyw && !keys)
            {
                // if neither W nor S are pressed, check if the plane's angle is above or below 0. if it is, slowly level the plane out
                if (CRotz.Angle < 0)
                {
                    CRotz.Angle += 1;
                    FinRotz.Angle += 1;
                    //WRotz.Angle += 1;
                    Wh1Rotz.Angle += 1;
                    Wh2Rotz.Angle += 1;
                    Wh3Rotz.Angle += 1;
                    PRotz.Angle += 1;
                    WhSRotz.Angle += 1;

                    CRotx.Angle += 1;
                    FinRotx.Angle += 1;
                    //WRotx.Angle += 1;
                    Wh1Rotx.Angle += 1;
                    Wh2Rotx.Angle += 1;
                    Wh3Rotx.Angle += 1;
                    PRotx.Angle += 1;
                    WhSRotx.Angle += 1;
                }
                else if (CRotz.Angle > 0)
                {
                    CRotz.Angle -= 1;
                    FinRotz.Angle -= 1;
                    //WRotz.Angle -= 1;
                    Wh1Rotz.Angle -= 1;
                    Wh2Rotz.Angle -= 1;
                    Wh3Rotz.Angle -= 1;
                    PRotz.Angle -= 1;
                    WhSRotz.Angle -= 1;

                    CRotx.Angle -= 1;
                    FinRotx.Angle -= 1;
                    //WRotx.Angle -= 1;
                    Wh1Rotx.Angle -= 1;
                    Wh2Rotx.Angle -= 1;
                    Wh3Rotx.Angle -= 1;
                    PRotx.Angle -= 1;
                    WhSRotx.Angle -= 1;
                }

                if (CamRotz.Angle > 0)
                {
                    CamRotz.Angle -= 0.2;
                    if (CamRotz.Angle < 0) { CamRotz.Angle = 0; }
                }
                else if (CamRotz.Angle < 0)
                {
                    CamRotz.Angle += 0.2;
                    if (CamRotz.Angle > 0) { CamRotz.Angle = 0; }
                }
                if (CamRotx.Angle > 0)
                {
                    CamRotx.Angle -= 0.2;
                    if (CamRotx.Angle < 0) { CamRotx.Angle = 0; }
                }
                else if (CamRotx.Angle < 0)
                {
                    CamRotx.Angle += 0.2;
                    if (CamRotx.Angle > 0) { CamRotx.Angle = 0; }
                }
            }

            if (!keyd && !keya && !tip)
            {
                // if neither D nor A are pressed, check if the plane's angle is skewed. if it is, slowly level the plane out
                if (CRotx.Angle < 0)
                {
                    CRotx.Angle += 1;
                    FinRotx.Angle += 1;
                    //WRotx.Angle += 1;
                    Wh1Rotx.Angle += 1;
                    Wh2Rotx.Angle += 1;
                    Wh3Rotx.Angle += 1;
                    PRotx.Angle += 1;
                    WhSRotx.Angle += 1;
                }
                else if (CRotx.Angle > 0)
                {
                    CRotx.Angle -= 1;
                    FinRotx.Angle -= 1;
                    Wh1Rotx.Angle -= 1;
                    Wh2Rotx.Angle -= 1;
                    Wh3Rotx.Angle -= 1;
                    //WRotx.Angle -= 1;
                    PRotx.Angle -= 1;
                    WhSRotx.Angle -= 1;
                }
            }

            // if space or ctrl are pressed, speed up or slow down the speed of the plane
            if (space)
            {
                if (speed < 10)
                {
                    Camera.FieldOfView += 0.03;
                }

                speed += 0.01;
            }
            if (ctrl)
            {
                if (speed < 10 && speed != 0)
                {
                    Camera.FieldOfView -= 0.03;
                }

                speed -= 0.01;
            }

            if (Camera.FieldOfView < 45)
            {
                Camera.FieldOfView = 45;
            }

            // if speed is below 6, make the plane tip downwards to the ground regardless of if the user is pressing W or not
            if (speed < 6)
            {
                if (CTrans.OffsetY > 1)
                {
                    speed = 5.99;
                }

                CTrans.OffsetY -= 0.03;
                CRotz.Angle += 0.5;
                FinTrans.OffsetY -= 0.03;
                FinRotz.Angle += 0.5;
                Wh1Trans.OffsetY -= 0.03;
                Wh2Trans.OffsetY -= 0.03;
                Wh3Trans.OffsetY -= 0.03;
                //WTrans.OffsetY -= 0.03;
                //WRotz.Angle += 0.5;
                PTrans.OffsetY -= 0.03;
                PRotz.Angle += 0.5;
                WhSTrans.OffsetY -= 0.03;

                CamTrans.OffsetY -= 0.03;

                if (AltEll.Margin.Top < 505)
                {
                    AltEll.Margin = new Thickness(927, AltEll.Margin.Top + 0.2, 0, 0);
                }

                // change label to let the user know the plane is not ready for takeoff yet
                lbl2.Content = "Not Ready. Hold Spacebar to increase speed";
            }

            // if not, the plane is ready for takeoff
            else { lbl2.Content = "Ready for Takeoff!"; }

            // if speed is taken below 0, reset it to 0
            if (speed < 0) { speed = 0; }

            // if speed is above 3, the plane is adequately above the ground and the wheels have not been brought up yet, iteratively
            // bring the wheels up until they are hidden
            if (speed > 6 && CTrans.OffsetY > 4 && wheelsyoffset < 1)
            {
                Wh1Trans.OffsetY += 0.03;
                Wh2Trans.OffsetY += 0.03;
                Wh3Trans.OffsetY += 0.03;
                WhSTrans.OffsetY += 0.03;
                wheelsyoffset += 0.03;
            }

            // if the plane is close to the ground, bring the wheels back out
            if (CTrans.OffsetY <= 4 && wheelsyoffset > 0)
            {
                Wh1Trans.OffsetY -= 0.03;
                Wh2Trans.OffsetY -= 0.03;
                Wh3Trans.OffsetY -= 0.03;
                WhSTrans.OffsetY -= 0.03;
                wheelsyoffset -= 0.03;
            }

            // if R key is pressed, reset the scene so all plane parts are at their default
            if (keyr)
            {
                CTrans.OffsetZ = 0; CTrans.OffsetX = 0; CTrans.OffsetY = 1;
                CRoty.Angle = 0; CRotx.Angle = 0; CRotz.Angle = 0;
                ShadTrans.OffsetZ = 0;
                ShadRoty.Angle = 0;
                ShadTrans.OffsetX = 0;
                speed = 0.0;
                FinTrans.OffsetY = 1; FinTrans.OffsetZ = 0; FinTrans.OffsetX = 0;
                FinRotx.Angle = 0; FinRoty.Angle = 0; FinRotz.Angle = 0;
                //WTrans.OffsetY = 1; WTrans.OffsetZ = 0; WTrans.OffsetX = 0;
                //WRotx.Angle = 0; WRoty.Angle = 0; WRotz.Angle = 0;
                Wh1Trans.OffsetY = 1; Wh1Trans.OffsetZ = 0; Wh1Trans.OffsetX = 0;
                Wh1Rotx.Angle = 0; Wh1Roty.Angle = 0; Wh1Rotz.Angle = 0;
                Wh2Trans.OffsetY = 1; Wh2Trans.OffsetZ = 0; Wh2Trans.OffsetX = 0;
                Wh2Rotx.Angle = 0; Wh2Roty.Angle = 0; Wh2Rotz.Angle = 0;
                Wh3Trans.OffsetY = 1; Wh3Trans.OffsetZ = 0; Wh3Trans.OffsetX = 0;
                Wh3Rotx.Angle = 0; Wh3Roty.Angle = 0; Wh3Rotz.Angle = 0;
                PTrans.OffsetY = 1; PTrans.OffsetZ = 0; PTrans.OffsetX = 0;
                PRotx.Angle = 0; PRoty.Angle = 0; PRotz.Angle = 0;
                WhSTrans.OffsetY = 1; WhSTrans.OffsetZ = 0; WhSTrans.OffsetX = 0;
                WhSRotx.Angle = 0; WhSRoty.Angle = 0; WhSRotz.Angle = 0;

                CamTrans.OffsetY = 1; CamTrans.OffsetX = 0; CamTrans.OffsetZ = 0;
                CamRotx.Angle = 0; CamRotz.Angle = 0; CamRoty.Angle = 0;
                Camera.FieldOfView = 45;
            }

            // if plane's offset is below 1, reset it and rest of plane parts to 1. this is when the plane is in contact with the ground
            if (CTrans.OffsetY < 1)
            {
                CTrans.OffsetY = 1;
                FinTrans.OffsetY = 1;
                //WTrans.OffsetY = 1;
                Wh1Trans.OffsetY = 1;
                Wh2Trans.OffsetY = 1;
                Wh3Trans.OffsetY = 1;
                CamTrans.OffsetY = 1;

                Wh1Rotz.Angle = 0;
                Wh2Rotz.Angle = 0;
                Wh3Rotz.Angle = 0;
                PTrans.OffsetY = 1;
                WhSTrans.OffsetY = 1;
                WhSRotz.Angle = 0;
            }

            /*
            // if the plane goes too far out, prevent it from traveling further. this is to prevent the plane from passing through the visible skybox
            if (CTrans.OffsetX < -500)
            {
                CTrans.OffsetX = -500;
                ShadTrans.OffsetX = -500;
                FinTrans.OffsetX = -500;
                WTrans.OffsetX = -500;
                Wh1Trans.OffsetX = -500;
                Wh2Trans.OffsetX = -500;
                Wh3Trans.OffsetX = -500;
                PTrans.OffsetX = -500;
                WhSTrans.OffsetX = -500;
                CamTrans.OffsetX = -500;
            }
            */

            // if the plane has turned more than 20 degrees in either x direction, keep it at 20 degrees
            if (CRotx.Angle > 20)
            {
                CRotx.Angle = 20;
                FinRotx.Angle = 20;
                //WRotx.Angle = 20;
                Wh1Rotx.Angle = 20;
                Wh2Rotx.Angle = 20;
                Wh3Rotx.Angle = 20;
                WhSRotx.Angle = 20;
            }
            if (CRotx.Angle < -20)
            {
                CRotx.Angle = -20;
                FinRotx.Angle = -20;
                //WRotx.Angle = -20;
                Wh1Rotx.Angle = -20;
                Wh2Rotx.Angle = -20;
                Wh3Rotx.Angle = -20;
                WhSRotx.Angle = -20;
            }

            // if the plane has been tipped upwards or downwards more than 15 degrees, keep it at 15 degrees
            if (CRotz.Angle > 15)
            {
                CRotz.Angle = 15;
                FinRotz.Angle = 15;
                //WRotz.Angle = 15;
                Wh1Rotz.Angle = 15;
                Wh2Rotz.Angle = 15;
                Wh3Rotz.Angle = 15;
                PRotz.Angle = 15;
                WhSRotz.Angle = 15;
            }
            if (CRotz.Angle < -15)
            {
                CRotz.Angle = -15;
                FinRotz.Angle = -15;
                //WRotz.Angle = -15;
                Wh1Rotz.Angle = -15;
                Wh2Rotz.Angle = -15;
                Wh3Rotz.Angle = -15;
                PRotz.Angle = -15;
                WhSRotz.Angle = -15;
            }

            // if the plane's y rotation is above 360 degrees or below 0 degrees, renormalize it
            if (CRoty.Angle < 0)
            {
                CRoty.Angle += 360;
                FinRoty.Angle += 360;
                //WRoty.Angle += 360;
                Wh1Roty.Angle += 360;
                Wh2Roty.Angle += 360;
                Wh3Roty.Angle += 360;
                PRoty.Angle += 360;
                WhSRoty.Angle += 360;
            }
            if (CRoty.Angle >= 360)
            {
                CRoty.Angle -= 360;
                FinRoty.Angle -= 360;
                //WRoty.Angle -= 360;
                Wh1Roty.Angle -= 360;
                Wh2Roty.Angle -= 360;
                Wh3Roty.Angle -= 360;
                PRoty.Angle -= 360;
                WhSRoty.Angle -= 360;
            }

            // if the plane is in contact with the ground, don't rotate anything while turning
            if (CTrans.OffsetY == 1)
            {
                CRotz.Angle = 0;
                CRotx.Angle = 0;
                FinRotz.Angle = 0;
                FinRotx.Angle = 0;
                //WRotz.Angle = 0;
                //WRotx.Angle = 0;
                Wh1Rotx.Angle = 0;
                Wh2Rotx.Angle = 0;
                Wh3Rotx.Angle = 0;
                PRotz.Angle = 0;
                WhSRotx.Angle = 0;
            }

            // if the camera angle is above or below 5 degrees, keep it at 5 degrees
            if (CamRotz.Angle > 5)
            {
                CamRotz.Angle = 5;
            }
            if (CamRotz.Angle < -5)
            {
                CamRotz.Angle = -5;
            }
            if (CamRotx.Angle > 5)
            {
                CamRotx.Angle = 5;
            }
            if (CamRotx.Angle < -5)
            {
                CamRotx.Angle = -5;
            }

            // set maximum speed at 10
            if (speed > 10)
            {
                speed = 10;
            }


            // display the plane speed
            lbl1.Content = "Speed: " + speed;
        }

        // function to iteratively move the plane forward according to the speed
        private void MoveForward(double xdir1, double zdir1)
        {
            //use plane's direction values to determine how much to move the plane in the x and z directions
            CTrans.OffsetX -= (xdir1 * 0.03 * (speed));
            CTrans.OffsetZ += (zdir1 * 0.03 * (speed));
            ShadTrans.OffsetX -= (xdir1 * 0.03 * (speed));
            ShadTrans.OffsetZ += (zdir1 * 0.03 * (speed));
            FinTrans.OffsetX -= (xdir1 * 0.03 * (speed));
            FinTrans.OffsetZ += (zdir1 * 0.03 * (speed));
            //WTrans.OffsetX -= (xdir1 * 0.03 * (speed));
            //WTrans.OffsetZ += (zdir1 * 0.03 * (speed));
            Wh1Trans.OffsetX -= (xdir1 * 0.03 * (speed));
            Wh1Trans.OffsetZ += (zdir1 * 0.03 * (speed));
            Wh2Trans.OffsetX -= (xdir1 * 0.03 * (speed));
            Wh2Trans.OffsetZ += (zdir1 * 0.03 * (speed));
            Wh3Trans.OffsetX -= (xdir1 * 0.03 * (speed));
            Wh3Trans.OffsetZ += (zdir1 * 0.03 * (speed));
            PTrans.OffsetX -= (xdir1 * 0.03 * (speed));
            PTrans.OffsetZ += (zdir1 * 0.03 * (speed));
            WhSTrans.OffsetX -= (xdir1 * 0.03 * (speed));
            WhSTrans.OffsetZ += (zdir1 * 0.03 * (speed));

            CamTrans.OffsetX -= (xdir1 * 0.03 * (speed));
            CamTrans.OffsetZ += (zdir1 * 0.03 * (speed));

            // rotate the propeller according to the speed
            PRotx.Angle += (15 * (speed));
        }

        // function to determine when a key is pressed down to pass through to the main function
        private void keyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D: { keyd = true; break; }
                case Key.A: { keya = true; break; }
                case Key.W: { keyw = true; break; }
                case Key.S: { keys = true; break; }
                case Key.Space: { space = true; break; }
                case Key.LeftCtrl: { ctrl = true; break; }
                case Key.R: { keyr = true; break; }
                case Key.D1: { key1 = true; break; }
                case Key.D2: { key2 = true; break; }
                default: { break; }
            }
        }

        // function to determine when a key is released to pass through to the main function
        private void keyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D: { keyd = false; break; }
                case Key.A: { keya = false; break; }
                case Key.W: { keyw = false; break; }
                case Key.S: { keys = false; break; }
                case Key.Space: { space = false; break; }
                case Key.LeftCtrl: { ctrl = false; break; }
                case Key.R: { keyr = false; break; }
                case Key.D1: { key1 = false; break; }
                case Key.D2: { key2 = false; break; }
                default: { break; }
            }
        }

        // function to change the camera perspective
        private void ChangePers(int pers)
        {
            if (pers == 1)
            {
                Camera.Position = new System.Windows.Media.Media3D.Point3D(12, 2, 0);
            }
            if (pers == 2)
            {
                Camera.Position = new System.Windows.Media.Media3D.Point3D(-1.5, 1, 0);
            }
        }
    }
}
