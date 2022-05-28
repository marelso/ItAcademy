namespace ItAcademy.Models
{
    public class Medicamento
    {
        public int Id { get; set; }
        public string Substancia { get; set; }
        public string CNPJ { get; set; }
        public string Laboratorio { get; set; }
        public long? CodGGREM { get; set; }
        public long? Registro { get; set; }
        public long? EAN1 { get; set; }
        public long? EAN2 { get; set; }
        public long? EAN3 { get; set; }
        public string Produto { get; set; }
        public string Apresentacao { get; set; }
        public string ClasseTerapeutica { get; set; }
        public string TipoProduto { get; set; }
        public string RegimePreco { get; set; }
        public double? PFIsento { get; set; }
        public double? PFZero { get; set; }
        public double? PF12 { get; set; }
        public double? PF17 { get; set; }
        public double? PF17ALC { get; set; }
        public double? PF175 { get; set; }
        public double? PF175ALC { get; set; }
        public double? PF18 { get; set; }
        public double? PF18ALC { get; set; }
        public double? PF20 { get; set; }
        public double? PMCZero { get; set; }
        public double? PMC12 { get; set; }
        public double? PMC17 { get; set; }
        public double? PMC17ALC { get; set; }
        public double? PMC175 { get; set; }
        public double? PMC175ALC { get; set; }
        public double? PMC18 { get; set; }
        public double? PMC18ALC { get; set; }
        public double? PMC20 { get; set; }
        public bool Restricao { get; set; }
        public bool CAP { get; set; }
        public bool CONFAZ { get; set; }
        public bool ICMSZero { get; set; }
        public string AnaliseRecursal { get; set; }
        public string ConcessaoCredito { get; set; }
        public bool Comercializacao2020 { get; set; }
        public string TARJA { get; set; }
    }
}
