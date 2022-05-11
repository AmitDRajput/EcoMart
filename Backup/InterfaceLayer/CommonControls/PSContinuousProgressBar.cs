using System;
using System.Windows.Forms; 

namespace PharmaSYSRetailPlus.InterfaceLayer.CommonControls
{
    class PSContinuousProgressBar : ProgressBar
    {
        public PSContinuousProgressBar()
        {
            this.Style = ProgressBarStyle.Continuous;
        }
        protected override void CreateHandle()
        {
            base.CreateHandle();
            try { SetWindowTheme(this.Handle, "", ""); }
            catch { }
        }

        [System.Runtime.InteropServices.DllImport("uxtheme.dll")]
        private static extern int SetWindowTheme(IntPtr hwnd, string appname, string idlist);
    }
}
