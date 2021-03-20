using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenCvSharp;

namespace AutoPictureClicker
{
    public partial class Form1
    {
        private void button_SelectTemplate_Click(object sender, EventArgs e)
        {
            if(clickThread_IsActive)
            {
                ShowInformation("请先终止线程。");
                return;
            }

            string oldPath = Config.Get(Config.Name_TemplatePath);
            FileInfo oldFile = new FileInfo(oldPath);
            DirectoryInfo oldDir = oldFile.Directory;

            this.openFileDialog_SelectTemplate.Title = "打开文件。";
            this.openFileDialog_SelectTemplate.InitialDirectory = oldFile.Directory.FullName;
            this.openFileDialog_SelectTemplate.Filter =
                "图像文件(*.png, *.bmp, *.jpg, *.jpeg)|*.png;*.jpg;*.bmp;*.jpeg|" +
                "PNG文件(*.png)|*.png|" +
                "BMP文件(*.bmp)|*.bmp|" +
                "JPG文件(*.jpg, *.jpeg)|*.jpg;*.jpeg|" +
                "所有文件(*.*)|*.*";
            this.openFileDialog_SelectTemplate.FilterIndex = 1;
            this.openFileDialog_SelectTemplate.AddExtension = false;
            this.openFileDialog_SelectTemplate.CheckPathExists = true;
            this.openFileDialog_SelectTemplate.DereferenceLinks = true;
            this.openFileDialog_SelectTemplate.ValidateNames = true;

            if (openFileDialog_SelectTemplate.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog_SelectTemplate.FileName;

                FileInfo file = new FileInfo(path);
                if (!file.Exists) {
                    ShowError("该文件不存在；该文件不存在；无访问权限。");
                    return;
                }
                try
                {
                    new Mat(path);
                }
                catch(Exception)
                {
                    ShowError("该文件类型不支持；或者损坏；无访问权限。");
                    return;
                }

                Config.Set(Config.Name_TemplatePath, path, true);
                label_TemplatePath.Text = path;
            }
        }
    }
}
