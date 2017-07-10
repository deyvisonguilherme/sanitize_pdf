using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace SanitizePDF
{
    public partial class Form1 : Form
    {
        public Form1() { InitializeComponent(); }

        ViewModel.Arquivo.Arquivos arq = new ViewModel.Arquivo.Arquivos();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog _dlgFolder = new FolderBrowserDialog())
                {
                    if (_dlgFolder.ShowDialog() != DialogResult.OK) return;

                    arq.diretorio = _dlgFolder.SelectedPath;
                    txtDiretorio.Text = arq.diretorio;

                    DirectoryInfo infoPath = new DirectoryInfo(_dlgFolder.SelectedPath);
                    arq.files = infoPath.GetFiles();
                }
            }
            catch (Exception ex)
            {
                BLL.Log.LogarError(ex.Message, ex.StackTrace);
                MessageBox.Show("Erro " + ex.Message,"Mesangem do sistema",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnSanitizar_Click(object sender, EventArgs e)
        {
            arq.title = txtTitle.Text;
            if (arq.files == null)
                MessageBox.Show("Erro: nenhum arquivo encontrado", "Mesangem do sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                new Thread(() => {

                    string retorno = BLL.ProcessFile.Processamento(arq);
                    MessageBox.Show(retorno);

                }).Start();
        }
    }
}
