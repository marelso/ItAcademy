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
    public class Service0
    {
        Context _dbContext = new Context();

        public MedicamentoDTO GetMedicamentoByName(string nome)
        {
            var medicamentoDTO = new MedicamentoDTO();
            var medicamento = this._dbContext.Medicamentos.Where(m => m.Substancia == nome && m.Comercializacao2020 == true).FirstOrDefault();
            if (medicamento != null)
            {
                medicamentoDTO.Substancia = medicamento.Substancia;
                medicamentoDTO.Apresentacao = medicamento.Apresentacao;
                medicamentoDTO.Produto = medicamento.Produto;
                medicamentoDTO.PFIsento = medicamento.PFIsento;
            }

            return medicamentoDTO;
        }

        public DataTable GetMedicamentosByCode(string code)
        {
            DataTable data = new DataTable();
            MedicamentoDTO menor = new MedicamentoDTO(), maior = new MedicamentoDTO();
            List<Medicamento> medicamentos = this._dbContext.Medicamentos.Where(m => m.EAN1 == Convert.ToInt32(code)).OrderBy(m => m.PMCZero).ToList();
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
            var config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true
            };

            using (var reader = new StreamReader(csvPath))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<FileRows>().Select(row => new Medicamento()
                {
                    #region Setting up props
                    Substancia = row.Substancia,
                    CNPJ = row.CNPJ,
                    Laboratorio = row.Laboratorio,
                    CodGGREM = row.CodGGREM,
                    Registro = row.Registro,
                    EAN1 = row.EAN1,
                    EAN2 = row.EAN2,
                    EAN3 = row.EAN3,
                    Produto = row.Produto,
                    Apresentacao = row.Apresentacao,
                    ClasseTerapeutica = row.ClasseTerapeutica,
                    TipoProduto = row.TipoProduto,
                    RegimePreco = row.RegimePreco,
                    PFIsento = row.PFIsento,
                    PFZero = row.PFZero,
                    PF12 = row.PF12,
                    PF17 = row.PF17,
                    PF17ALC = row.PF17ALC,
                    PF175 = row.PF175,
                    PF18 = row.PF18,
                    PF18ALC = row.PF18ALC,
                    PF20 = row.PF20,
                    PMCZero = row.PMCZero,
                    PMC12 = row.PMC12,
                    PMC17 = row.PMC17,
                    PMC17ALC = row.PMC17ALC,
                    PMC175 = row.PMC175,
                    PMC175ALC = row.PMC175ALC,
                    PMC18 = row.PMC18,
                    PMC18ALC = row.PMC18ALC,
                    PMC20 = row.PMC20,
                    Restricao = row.Restricao,
                    CAP = row.CAP,
                    CONFAZ = row.CONFAZ,
                    ICMSZero = row.ICMSZero,
                    AnaliseRecursal = row.AnaliseRecursal,
                    ConcessaoCredito = row.ConcessaoCredito,
                    Comercializacao2020 = row.Comercializacao2020,
                    TARJA = row.TARJA,
                    #endregion
                });

                using (_dbContext)
                {
                    while (true)
                    {
                        var items = records.Take(100_000).ToList();

                        if (items.Any() == false) break;

                        _dbContext.AddRange(items);
                        _dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
