using MouseKeyboardLibrary;
using SampleApplication.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SampleApplication
{
    public partial class Tracker : Form
    {

        MouseHook mouseHook = new MouseHook();
        KeyboardHook keyboardHook = new KeyboardHook();

        public Tracker()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
          

            mouseHook.MouseMove += new MouseEventHandler(mouseHook_MouseMove);
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(mouseHook_MouseUp);
            mouseHook.MouseWheel += new MouseEventHandler(mouseHook_MouseWheel);

            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
            keyboardHook.KeyPress += new KeyPressEventHandler(keyboardHook_KeyPress);

            mouseHook.Start();
            keyboardHook.Start();

            SetXYLabel(MouseSimulator.X, MouseSimulator.Y);

        }

        void keyboardHook_KeyPress(object sender, KeyPressEventArgs e)
        {

            AddKeyboardEvent(
                "KeyPress",
                "",
                e.KeyChar.ToString(),
                "",
                "",
                ""
                );

        }

        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {

            AddKeyboardEvent(
                "KeyUp",
                e.KeyCode.ToString(),
                "",
                e.Shift.ToString(),
                e.Alt.ToString(),
                e.Control.ToString()
                );

        }

        void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {


            AddKeyboardEvent(
                "KeyDown",
                e.KeyCode.ToString(),
                "",
                e.Shift.ToString(),
                e.Alt.ToString(),
                e.Control.ToString()
                );

        }

        void mouseHook_MouseWheel(object sender, MouseEventArgs e)
        {
            if (txtUsuario.Text != String.Empty)
            {
                AddMouseEvent(
                    "MouseWheel",
                    "",
                    "",
                    "",
                    e.Delta.ToString()
                    );
            }

        }

        void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {

            if (txtUsuario.Text != String.Empty)
            {
                AddMouseEvent(
                "MouseUp",
                e.Button.ToString(),
                e.X.ToString(),
                e.Y.ToString(),
                ""
                );
            }

        }

        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {

            if (txtUsuario.Text != String.Empty)
            {

                AddMouseEvent(
                "MouseDown",
                e.Button.ToString(),
                e.X.ToString(),
                e.Y.ToString(),
                ""
                );
            }


        }

        void mouseHook_MouseMove(object sender, MouseEventArgs e)
        {

            if (txtUsuario.Text != String.Empty)
            {
                SetXYLabel(e.X, e.Y);
            }

        }

        void SetXYLabel(int x, int y)
        {

            curXYLabel.Text = String.Format("Current Mouse Point: X={0}, y={1}", x, y);

        }

        List<clMouseEvent> listaMouse = new List<clMouseEvent>();
        string usuario = string.Empty;
        void AddMouseEvent(string eventType, string button, string x, string y,string delta)
        {
            string data = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff tt");
            usuario = txtUsuario.Text;

            string[] add = {
                        eventType,
                        button,
                        x,
                        y,
                        usuario,
                        data
                    };

            lstMouse.Items.Insert(0,
                new ListViewItem(
                   add
                    ));


            listaMouse.Add(new clMouseEvent(eventType,
                            button,
                            x,
                            y,
                            usuario,
                            data)
            );


            var teste = listaMouse.Count;
        }
        class clMouseEvent
        {
            public string eventType { get; set; }
            public string button { get; set; }
            public string x { get; set; }
            public string y { get; set; }
            public string usuario { get; set; }
            public string data { get; set; }

            public clMouseEvent()
            {

            }

            public clMouseEvent(string eventType, string button, string x, string y, string usuario, string data)
            {
                this.eventType = eventType;
                this.button = button;
                this.x = x;
                this.y = y;
                this.usuario = usuario;
                this.data = data;
            }
        }
        void AddKeyboardEvent(string eventType, string keyCode, string keyChar, string shift, string alt, string control)
        {

            listView2.Items.Insert(0,
                 new ListViewItem(
                     new string[]{
                        eventType, 
                        keyCode,
                        keyChar,
                        shift,
                        alt,
                        control
                }));

        }

        
        private void Salvar_Click(object sender, EventArgs e)
        {
          ExportData.ExportCsv ( listaMouse, String.Concat( @"C:\ArquivoMouseTracker\export." ,DateTime.Now.ToString("ddMMyyyhhmmss", DateTimeFormatInfo.InvariantInfo)  ,".csv"));
           listaMouse.Clear();
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            // Not necessary anymore, will stop when application exits

            //mouseHook.Stop();
            //keyboardHook.Stop();

        }


        //private void Tracker_Resize(object sender, EventArgs e)
        //{
        //    notifyIcon.BalloonTipTitle = "Minimize to Tray App";
        //    notifyIcon.BalloonTipText = "You have successfully minimized your form.";

        //    if (FormWindowState.Minimized == this.WindowState)
        //    {
        //        notifyIcon.Visible = true;
        //        notifyIcon.ShowBalloonTip(500);
        //        this.Hide();
        //    }
        //    else if (FormWindowState.Normal == this.WindowState)
        //    {
        //        notifyIcon.Visible = false;
        //    }
        //}

     

        //private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    this.Show();
        //    this.WindowState = FormWindowState.Normal;
        //}
    }
}
