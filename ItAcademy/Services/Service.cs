using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItAcademy.Dtos;
using ItAcademy.Models;

namespace ItAcademy.Services
{
    public class Service
    {
        Context _dbContext = new Context();

        public List<MedicamentoDTO> GetMedicamentoByName(string nome)
        {
            var medicamentosDTO = new List<MedicamentoDTO>();
            var medicamentoDTO = new MedicamentoDTO();
            var medicamento = this._dbContext.Medicamentos.Where(m => m.Substancia == nome && m.Comercializacao2020).FirstOrDefault();
            if (medicamento != null)
            {
                medicamentoDTO.Substancia = medicamento.Substancia;
                medicamentoDTO.Apresentacao = medicamento.Apresentacao;
                medicamentoDTO.Produto = medicamento.Produto;
                medicamentoDTO.PFIsento = medicamento.PFIsento;
            }
            medicamentosDTO.Add(medicamentoDTO);

            return medicamentosDTO;
        }

        public DataTable GetMedicamentosByCode(string code)
        {
            DataTable data = new DataTable();
            MedicamentoDTO menor = new MedicamentoDTO(), maior = new MedicamentoDTO();
            List<Medicamento> medicamentos = this._dbContext.Medicamentos.Where(m => m.EAN1 == long.Parse(code)).OrderBy(m => m.PMCZero).ToList();
            if (medicamentos != null)
            {
                #region Load DTOs
                menor.Substancia = medicamentos[0].Substancia;
                menor.Apresentacao = medicamentos[0].Apresentacao;
                menor.Produto = medicamentos[0].Produto;
                menor.PFIsento = medicamentos[0].PFIsento;

                maior.Substancia = medicamentos[medicamentos.Count - 1].Substancia;
                maior.Apresentacao = medicamentos[medicamentos.Count - 1].Apresentacao;
                maior.Produto = medicamentos[medicamentos.Count - 1].Produto;
                maior.PFIsento = medicamentos[medicamentos.Count - 1].PFIsento;
                #endregion

                data.Rows.Add(maior);
                data.Rows.Add(menor);
                data.Rows.Add($"Diferença: {(medicamentos[medicamentos.Count - 1].PMCZero - medicamentos[0].PMCZero)}");
            }

            return data;
        }

        public void ImportDatabase(string csvPath)
        {
            try
            {
                List<Medicamento> medicamentos = new List<Medicamento>();
                string[] lines = File.ReadAllLines(csvPath, Encoding.GetEncoding("ISO-8859-1"));

                foreach (string line in lines)
                {
                    if (line.Contains("SUBSTÂNCIA") || string.IsNullOrEmpty(line))
                        continue;

                    var medicamento = new Medicamento();

                    if (line.Contains("\""))
                    {
                        continue;
                        #region não consegui tratar os registros corrompidos com os ; dentro das aspas, tentei de uma forma e foi parcialmente sucedida. Quando encontrar um padrão correto tento novamente.
                        string[] partes = line.Split("\"");
                        string[] row = partes[2].Split(";");

                        #region Setting up props
                        medicamento.Substancia = partes[1];
                        medicamento.CNPJ = row[1];
                        medicamento.Laboratorio = row[2];
                        medicamento.CodGGREM = !row[3].Contains("-") ? long.Parse(row[3]) : null;
                        medicamento.Registro = !row[4].Contains("-") ? long.Parse(row[4]) : null;
                        medicamento.EAN1 = !row[5].Contains("-") ? long.Parse(row[5]) : null;
                        medicamento.EAN2 = !row[6].Contains("-") ? long.Parse(row[6]) : null;
                        medicamento.EAN3 = !row[7].Contains("-") ? long.Parse(row[7]) : null;
                        medicamento.Produto = row[8];
                        medicamento.Apresentacao = row[9];
                        medicamento.ClasseTerapeutica = row[10];
                        medicamento.TipoProduto = row[11];
                        medicamento.RegimePreco = row[12];
                        medicamento.PFIsento = !string.IsNullOrEmpty(row[13]) ? double.Parse(row[13]) : null;
                        medicamento.PFZero = !string.IsNullOrEmpty(row[14]) ? double.Parse(row[14]) : null;
                        medicamento.PF12 = !string.IsNullOrEmpty(row[15]) ? double.Parse(row[15]) : null;
                        medicamento.PF17 = !string.IsNullOrEmpty(row[16]) ? double.Parse(row[16]) : null;
                        medicamento.PF17ALC = !string.IsNullOrEmpty(row[17]) ? double.Parse(row[17]) : null;
                        medicamento.PF175 = !string.IsNullOrEmpty(row[18]) ? double.Parse(row[18]) : null;
                        medicamento.PF18 = !string.IsNullOrEmpty(row[19]) ? double.Parse(row[19]) : null;
                        medicamento.PF18ALC = !string.IsNullOrEmpty(row[20]) ? double.Parse(row[20]) : null;
                        medicamento.PF20 = !string.IsNullOrEmpty(row[21]) ? double.Parse(row[21]) : null;
                        medicamento.PMCZero = !string.IsNullOrEmpty(row[22]) ? double.Parse(row[22]) : null;
                        medicamento.PMC12 = !string.IsNullOrEmpty(row[23]) ? double.Parse(row[23]) : null;
                        medicamento.PMC17 = !string.IsNullOrEmpty(row[24]) ? double.Parse(row[24]) : null;
                        medicamento.PMC17ALC = !string.IsNullOrEmpty(row[25]) ? double.Parse(row[25]) : null;
                        medicamento.PMC175 = !string.IsNullOrEmpty(row[26]) ? double.Parse(row[26]) : null;
                        medicamento.PMC175ALC = !string.IsNullOrEmpty(row[27]) ? double.Parse(row[27]) : null;
                        medicamento.PMC18 = !string.IsNullOrEmpty(row[28]) ? double.Parse(row[28]) : null;
                        medicamento.PMC18ALC = !string.IsNullOrEmpty(row[29]) ? double.Parse(row[29]) : null;
                        medicamento.PMC20 = !string.IsNullOrEmpty(row[30]) ? double.Parse(row[30]) : null;
                        medicamento.Restricao = row[31].ToLower() == "sim" ? true : false;
                        medicamento.CAP = row[32].ToLower() == "sim" ? true : false;
                        medicamento.CONFAZ = row[33].ToLower() == "sim" ? true : false;
                        medicamento.ICMSZero = row[34].ToLower() == "sim" ? true : false;
                        medicamento.AnaliseRecursal = row[35];
                        medicamento.ConcessaoCredito = row[36];
                        medicamento.Comercializacao2020 = row[37].ToLower() == "sim" ? true : false; ;
                        medicamento.TARJA = row[38];
                        #endregion
                        #endregion
                    }
                    else
                    {
                        string[] row = line.Split(";");

                        #region Setting up props
                        medicamento.Substancia = row[0];
                        medicamento.CNPJ = row[1];
                        medicamento.Laboratorio = row[2];
                        medicamento.CodGGREM = !row[3].Contains("-") ? long.Parse(row[3]) : null;
                        medicamento.Registro = !row[4].Contains("-") ? long.Parse(row[4]) : null;
                        medicamento.EAN1 = !row[5].Contains("-") ? long.Parse(row[5]) : null;
                        medicamento.EAN2 = !row[6].Contains("-") ? long.Parse(row[6]) : null;
                        medicamento.EAN3 = !row[7].Contains("-") ? long.Parse(row[7]) : null;
                        medicamento.Produto = row[8];
                        medicamento.Apresentacao = row[9];
                        medicamento.ClasseTerapeutica = row[10];
                        medicamento.TipoProduto = row[11];
                        medicamento.RegimePreco = row[12];
                        medicamento.PFIsento = !string.IsNullOrEmpty(row[13]) ? double.Parse(row[13].Replace(",", ".")) : null;
                        medicamento.PFZero = !string.IsNullOrEmpty(row[14]) ? double.Parse(row[14].Replace(",", ".")) : null;
                        medicamento.PF12 = !string.IsNullOrEmpty(row[15]) ? double.Parse(row[15].Replace(",", ".")) : null;
                        medicamento.PF17 = !string.IsNullOrEmpty(row[16]) ? double.Parse(row[16].Replace(",", ".")) : null;
                        medicamento.PF17ALC = !string.IsNullOrEmpty(row[17]) ? double.Parse(row[17].Replace(",", ".")) : null;
                        medicamento.PF175 = !string.IsNullOrEmpty(row[18]) ? double.Parse(row[18].Replace(",", ".")) : null;
                        medicamento.PF175ALC = !string.IsNullOrEmpty(row[19]) ? double.Parse(row[19].Replace(",", ".")) : null;
                        medicamento.PF18 = !string.IsNullOrEmpty(row[20]) ? double.Parse(row[20].Replace(",", ".")) : null;
                        medicamento.PF18ALC = !string.IsNullOrEmpty(row[21]) ? double.Parse(row[21].Replace(",", ".")) : null;
                        medicamento.PF20 = !string.IsNullOrEmpty(row[22]) ? double.Parse(row[22].Replace(",", ".")) : null;
                        medicamento.PMCZero = !string.IsNullOrEmpty(row[23]) ? double.Parse(row[23].Replace(",", ".")) : null;
                        medicamento.PMC12 = !string.IsNullOrEmpty(row[24]) ? double.Parse(row[24].Replace(",", ".")) : null;
                        medicamento.PMC17 = !string.IsNullOrEmpty(row[25]) ? double.Parse(row[25].Replace(",", ".")) : null;
                        medicamento.PMC17ALC = !string.IsNullOrEmpty(row[26]) ? double.Parse(row[26].Replace(",", ".")) : null;
                        medicamento.PMC175 = !string.IsNullOrEmpty(row[27]) ? double.Parse(row[27].Replace(",", ".")) : null;
                        medicamento.PMC175ALC = !string.IsNullOrEmpty(row[28]) ? double.Parse(row[28].Replace(",", ".")) : null;
                        medicamento.PMC18 = !string.IsNullOrEmpty(row[29]) ? double.Parse(row[29].Replace(",", ".")) : null;
                        medicamento.PMC18ALC = !string.IsNullOrEmpty(row[30]) ? double.Parse(row[30].Replace(",", ".")) : null;
                        medicamento.PMC20 = !string.IsNullOrEmpty(row[31]) ? double.Parse(row[31].Replace(",", ".")) : null;
                        medicamento.Restricao = row[32].ToLower() == "sim" ? true : false;
                        medicamento.CAP = row[33].ToLower() == "sim" ? true : false;
                        medicamento.CONFAZ = row[34].ToLower() == "sim" ? true : false;
                        medicamento.ICMSZero = row[35].ToLower() == "sim" ? true : false;
                        medicamento.AnaliseRecursal = row[36];
                        medicamento.ConcessaoCredito = row[37];
                        medicamento.Comercializacao2020 = row[38].ToLower() == "sim" ? true : false; ;
                        medicamento.TARJA = row[39];
                        #endregion
                    }

                    medicamentos.Add(medicamento);
                }

                while (true)
                {
                    if (!medicamentos.Any())
                        break;

                    _dbContext.AddRange(medicamentos);
                    medicamentos.Clear();

                    if (_dbContext.SaveChanges() > 0)
                        MessageBox.Show("A tabela foi importada com sucesso.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("A tabela não foi importada com sucesso.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
