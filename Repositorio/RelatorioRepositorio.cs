using Microsoft.EntityFrameworkCore;
using MaqCenter.Models;
using MaqCenter.ORM;

namespace MaqCenter.Repositorio
{
    public class RelatorioRepositorio
    {
        private DbMaqcenterContext _context;

        public RelatorioRepositorio(DbMaqcenterContext context)
        {
            _context = context;

        }
        public List<ViewAgendamento> GetAgendamentos(
     string campo1, string campo2, string campo3,
     string valor1, string valor2, string valor3)
        {
            // Verifica se todos os parâmetros estão vazios. Se estiverem, retorna todos os agendamentos.
            if (string.IsNullOrEmpty(campo1) && string.IsNullOrEmpty(campo2) && string.IsNullOrEmpty(campo3) &&
                string.IsNullOrEmpty(valor1) && string.IsNullOrEmpty(valor2) && string.IsNullOrEmpty(valor3))
            {
                return _context.ViewAgendamentos
                    .OrderBy(a => a.DtHoraAgendamento)
                    .Take(1000)
                    .ToList(); // Retorna todos os agendamentos limitados a 1000
            }

            var query = _context.ViewAgendamentos.AsQueryable();

            // Filtro para campo1 e valor1
            if (!string.IsNullOrEmpty(campo1) && !string.IsNullOrEmpty(valor1))
            {
                if (campo1.Equals("TipoServico", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.TipoServico.ToLower() == valor1.ToLower());
                else if (campo1.Equals("Nome", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.Nome.ToLower() == valor1.ToLower());
                else if (campo1.Equals("Email", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.Email.ToLower() == valor1.ToLower());
                else if (campo1.Equals("Telefone", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.Telefone.ToLower() == valor1.ToLower());
                else if (campo1.Equals("DtHoraAgendamento", StringComparison.OrdinalIgnoreCase))
                {
                    if (DateTime.TryParse(valor1, out DateTime dateValue))
                        query = query.Where(a => a.DtHoraAgendamento.Date == dateValue.Date);
                }
            }

            // Filtro para campo2 e valor2
            if (!string.IsNullOrEmpty(campo2) && !string.IsNullOrEmpty(valor2))
            {
                if (campo2.Equals("TipoServico", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.TipoServico.ToLower() == valor2.ToLower());
                else if (campo2.Equals("Nome", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.Nome.ToLower() == valor2.ToLower());
                else if (campo2.Equals("Email", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.Email.ToLower() == valor2.ToLower());
                else if (campo2.Equals("Telefone", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.Telefone.ToLower() == valor2.ToLower());
                else if (campo2.Equals("DtHoraAgendamento", StringComparison.OrdinalIgnoreCase))
                {
                    if (DateTime.TryParse(valor2, out DateTime dateValue))
                        query = query.Where(a => a.DtHoraAgendamento.Date == dateValue.Date);
                }
            }

            // Filtro para campo3 e valor3
            if (!string.IsNullOrEmpty(campo3) && !string.IsNullOrEmpty(valor3))
            {
                if (campo3.Equals("TipoServico", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.TipoServico.ToLower() == valor3.ToLower());
                else if (campo3.Equals("Nome", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.Nome.ToLower() == valor3.ToLower());
                else if (campo3.Equals("Email", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.Email.ToLower() == valor3.ToLower());
                else if (campo3.Equals("Telefone", StringComparison.OrdinalIgnoreCase))
                    query = query.Where(a => a.Telefone.ToLower() == valor3.ToLower());
                else if (campo3.Equals("DtHoraAgendamento", StringComparison.OrdinalIgnoreCase))
                {
                    if (DateTime.TryParse(valor3, out DateTime dateValue))
                        query = query.Where(a => a.DtHoraAgendamento.Date == dateValue.Date);
                }
            }

            // Limita a 1000 registros
            var agendamentos = query
                .OrderBy(a => a.DtHoraAgendamento)
                .Take(1000)
                .ToList(); // Executa a consulta e retorna a lista

            return agendamentos;
        }

    }
}

