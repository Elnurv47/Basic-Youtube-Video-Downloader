using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using VideoLibrary;

namespace Youtube_Video_Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void download_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                if(folder.ShowDialog() == DialogResult.OK)
                {
                    labelStatus.Text = "Video is downloading, Please Wait";
                    labelStatus.ForeColor = Color.Orange;
                    var youtube = YouTube.Default;
                    try
                    {
                        var video = await youtube.GetVideoAsync(videoUrl.Text);
                        byte[] bytes = await video.GetBytesAsync();
                        File.WriteAllBytes(folder.SelectedPath + @"\" + video.FullName, bytes);
                        labelStatus.Text = "Video downloaded";
                        labelStatus.ForeColor = Color.Lime;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("The URL you provided is invalid, Please make sure you entered a valid URL", "Video can't be downloaded", MessageBoxButtons.OK);
                    }
                }
            }
        }
    }
}
