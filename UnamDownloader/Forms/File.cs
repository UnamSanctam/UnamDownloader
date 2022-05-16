using System.Windows.Forms;

namespace UnamDownloader
{
    public partial class File : Form
    {
        public Builder builder = (Builder)Application.OpenForms["Builder"];

        public File()
        {
            InitializeComponent();
        }

        private void File_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            int count = builder.listFiles.Items.Count;
            for (int i = 0; i < count; i++)
            {
                builder.listFiles.Items[i] = builder.listFiles.Items[i];
            }
            Hide();
        }

        public override string ToString()
        {
            return txtFilename.Text + " - " + txtDownloadURL.Text;
        }
    }
}
