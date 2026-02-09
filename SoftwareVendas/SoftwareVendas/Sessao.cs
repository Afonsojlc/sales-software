namespace SoftwareVendas
{
    public static class Sessao
    {
        public static int ID_Vendedor { get; set; } 

        // CORREÇÃO: Adicionamos = ""; no fim para garantir que nunca é nulo
        public static string Nome { get; set; } = "";
        public static string Cargo;

        public static decimal PercentagemComissao { get; set; }
    }

    
    }