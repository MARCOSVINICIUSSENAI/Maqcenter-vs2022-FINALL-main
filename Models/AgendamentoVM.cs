namespace MaqCenter.Models
{
    public class AgendamentoVM
    {
        public int Id { get; set; }
        public DateTime DtHoraAgendamento { get; set; }
        public DateOnly DataAtendimento { get; set; }
        public TimeOnly Horario { get; set; }

        // Propriedades adicionais para o nome e email do usuário
        public string? UsuarioNome { get; set; }
        public string? UsuarioEmail { get; set; }  // Adicionando a propriedade UsuarioEmail
        public string? UsuarioTelefone { get; set; }  // Telefone do usuário (adicionado)
        public string? ServicoNome { get; set; }
        public decimal ServicoValor { get; set; }  // Preço do serviço

        public int FkUsuario { get; set; }

        public int FkServico { get; set; }

        // Propriedades de formatação
        public string DtHoraAgendamentoFormatada => DtHoraAgendamento.ToString("dd/MM/yyyy HH:mm");
        public string DataAtendimentoFormatada => DataAtendimento.ToString("dd/MM/yyyy");
        public string HorarioFormatado => Horario.ToString("HH:mm");

        // Propriedades para navegação
        public UsuarioVM? Usuario { get; set; }
        public ServicoVM? Servico { get; set; }
    }
}
