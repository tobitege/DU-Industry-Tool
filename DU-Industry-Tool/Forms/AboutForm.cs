using System;
using System.Windows.Forms;
using DU_Helpers;
using Krypton.Toolkit;

namespace DU_Industry_Tool
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            discordLink.Click += LinkOnClick;
            DimenciaGithubLink.Click += LinkOnClick;
            TobiReleasesLink.Click += LinkOnClick;
            //var version = File.ReadAllText("Version.txt");
            labelMain.Values.Text = labelMain.Values.Text.Replace("XXX", Utils.GetVersion());
        }

        private static void LinkOnClick(object sender, EventArgs e)
        {
            if (!(sender is KryptonLinkLabel klb)) return;
            try
            {
                System.Diagnostics.Process.Start(klb.Text);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode==-2147467259)
                    KryptonMessageBox.Show(noBrowser.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (System.Exception)
            {
                KryptonMessageBox.Show("Sorry, could not open the URL!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
