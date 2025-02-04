using MaqCenter.Models;
using MaqCenter.ORM;


namespace MaqCenter.Repositorio
{
    public class ServicoRepositorio
    {
        private DbMaqcenterContext _context;
        public ServicoRepositorio(DbMaqcenterContext context)
        {
            _context = context;
        }
        public bool InserirServico(string tipoServico, decimal valor)
        {
            try
            {
                TbServico servico = new TbServico();
                servico.TipoServico = tipoServico;
                servico.Valor = valor;


                _context.TbServicos.Add(servico);  // Supondo que _context.TbServicos seja o DbSet para a entidade de usuários
                _context.SaveChanges();

                return true;  // Retorna true para indicar sucesso
            }
            catch (Exception ex)
            {
                // Trate o erro ou faça um log do ex.Message se necessário
                return false;  // Retorna false para indicar falha
            }
        }

        public List<ServicoVM> ListarServicos()
        {
            List<ServicoVM> listSer = new List<ServicoVM>();

            var listTb = _context.TbServicos.ToList();

            foreach (var item in listTb)
            {
                var servicos = new ServicoVM
                {
                    Id = item.Id,
                    TipoServico = item.TipoServico,
                    Valor = item.Valor,
                };

                listSer.Add(servicos);
            }

            return listSer;
        }

        public bool AtualizarServico(int id, string tipoServico, decimal valor)
        {
            try
            {
                // Busca o Servico pelo ID
                var servico = _context.TbServicos.FirstOrDefault(u => u.Id == id);
                if (servico != null)
                {
                    // Atualiza os dados do servico
                    servico.TipoServico = tipoServico;
                    servico.Valor = valor;


                    // Salva as mudanças no banco de dados
                    _context.SaveChanges();

                    return true;  // Retorna verdadeiro se a atualização for bem-sucedida
                }
                else
                {
                    return false;  // Retorna falso se o servico não foi encontrado
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o servico com ID {id}: {ex.Message}");
                return false;
            }
        }

        public bool ExcluirServico(int id)
        {
            try
            {
                // Busca o servico pelo ID
                var servico = _context.TbServicos.FirstOrDefault(u => u.Id == id);

                // Se o usuário não for encontrado, lança uma exceção personalizada
                if (servico == null)
                {
                    throw new KeyNotFoundException("Servico não encontrado.");
                }


                // Remove o usuário do banco de dados
                _context.TbServicos.Remove(servico);
                _context.SaveChanges();  // Isso pode lançar uma exceção se houver dependências

                // Se tudo correr bem, retorna true indicando sucesso
                return true;

            }
            catch (Exception ex)
            {
                // Aqui tratamos qualquer erro inesperado e logamos para depuração
                Console.WriteLine($"Erro ao excluir o servico com ID {id}: {ex.Message}");

                // Relança a exceção para ser capturada pelo controlador
                throw new Exception($"Erro ao excluir o servico: {ex.Message}");
            }
        }
        public List<ServicoVM> ListarNomesServicos()
        {
            // Recupera os serviços com filtragem e projeção para ServicoVM diretamente no banco de dados
            var query = _context.TbServicos.ToList();

            // Projeta diretamente para ServicoVM e retorna como lista
            var listServicos = _context.TbServicos
                .Select(s => new ServicoVM
                {
                    Id = s.Id,
                    TipoServico = s.TipoServico,
                })
                .ToList();

            return listServicos;
        }
    }
}