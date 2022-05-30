using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ItAcademy.Services;

namespace ItAcademy
{
    public partial class Gestor : Form
    {
        Service service = new Service();
        public Gestor()
        {
            InitializeComponent();
        }

        private void btnImportFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.ShowDialog();

                if(!string.IsNullOrEmpty(file.FileName))
                    service.ImportDatabase(file.FileName);
                else
                    MessageBox.Show("Nenhum arquivo foi selecionado, a importação foi cancelada.", "Erro ao efetuar importação.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro:\n {ex.Message}", "Erro durante importação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                switch (tabInfos.SelectedIndex)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(txtName.Text))
                            gridName.DataSource = service.GetMedicamentoByName(txtName.Text);
                        else
                            MessageBox.Show("Insira o nome do medicamento para pesquisar.", "Erro ao efetuar a pesquisa.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 1:
                        if(!string.IsNullOrEmpty(txtCodebar.Text))
                            gridCode.DataSource = service.GetMedicamentosByCode(txtCodebar.Text);
                        else
                            MessageBox.Show("Insira o código do medicamento para pesquisar.", "Erro ao efetuar a pesquisa.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Erro:\n Selecione uma das guias possíveis.");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro:\n {ex.Message}", "Erro durante a coleta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            try
            {
                gridConcessao.DataSource = service.ConsultarConcessaoCredito();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro:\n {ex.Message}", "Erro durante a coleta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
