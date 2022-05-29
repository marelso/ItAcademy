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

                service.ImportDatabase(file.FileName);
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
                        gridName.DataSource = service.GetMedicamentoByName(txtName.Text);
                        break;
                    case 1:
                        gridCode.DataSource = service.GetMedicamentosByCode(txtCodebar.Text);
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
