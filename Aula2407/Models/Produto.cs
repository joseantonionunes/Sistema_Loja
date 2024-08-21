namespace Aula2407.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Fabricante { get; set; } = string.Empty;
        public int Codigo { get; set; }
        public int Quantidade { get; set; }
        public decimal ValordeCompra { get; set; }
        public decimal ValordeVenda { get; set; }
        public string Descricao { get; set; } = string.Empty;
    }
}

