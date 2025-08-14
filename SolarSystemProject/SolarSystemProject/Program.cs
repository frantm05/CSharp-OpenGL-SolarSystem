using System;
using System.Windows.Forms;

namespace SolarSystemProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       
        [STAThread] // atribut pro zajištění, že aplikace bude spuštěna v jednom vlákně (Single Threaded Apartment)
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 myForm = new Form1();
            myForm.Show();

            while (!myForm.IsDisposed)
            {
                Application.DoEvents();
                myForm.Draw();
            }
        }
    }
}
