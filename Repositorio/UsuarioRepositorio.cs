using Microsoft.EntityFrameworkCore;
using MaqCenter.Models;
using MaqCenter.ORM;

namespace MaqCenter.Repositorio
{
    public class UsuarioRepositorio
    {

        private DbMaqcenterContext _context;
        public UsuarioRepositorio(DbMaqcenterContext context)
        {
            _context = context;
        }


        public UsuarioVM VerificarLogin(string email, string senha)
        {
            // Verifica se o e-mail e a senha estão presentes no banco de dados
            var usuario = _context.TbUsuarios
                .FirstOrDefault(u => u.Email == email && u.Senha == senha);

            // Se encontrar o usuário, cria um objeto UsuarioVM para retornar
            if (usuario != null)
            {
                var usuarioVM = new UsuarioVM
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Telefone = usuario.Telefone,
                    Senha = usuario.Senha, // Senha pode ser omitida por questões de segurança
                    TipoUsuario = usuario.TipoUsuario
                };
                // Definindo variáveis de ambiente
                Environment.SetEnvironmentVariable("USUARIO_ID", usuario.Id.ToString());
                Environment.SetEnvironmentVariable("USUARIO_NOME", usuario.Nome);
                Environment.SetEnvironmentVariable("USUARIO_EMAIL", usuario.Email);
                Environment.SetEnvironmentVariable("USUARIO_TELEFONE", usuario.Senha);
                Environment.SetEnvironmentVariable("USUARIO_TIPO", usuario.TipoUsuario.ToString());
                return usuarioVM;
            }
            // Acessando as variáveis de ambiente
            /*string id = Environment.GetEnvironmentVariable("USUARIO_ID");
            string nome = Environment.GetEnvironmentVariable("USUARIO_NOME");
            string email = Environment.GetEnvironmentVariable("USUARIO_EMAIL");
            string telefone = Environment.GetEnvironmentVariable("USUARIO_TELEFONE");
            string tipoUsuario = Environment.GetEnvironmentVariable("USUARIO_TIPO");
            // Se não encontrar o usuário, retorna null ou uma exceção
            */
            return null; // Ou você pode lançar uma exceção, dependendo de sua estratégia
        }
        public bool InserirUsuario(string nome, string email, string telefone, string senha, int tipoUsuario)
        {
            try
            {
                TbUsuario usuario = new TbUsuario();
                usuario.Nome = nome;
                usuario.Email = email;
                usuario.Telefone = telefone;
                usuario.Senha = senha;
                usuario.TipoUsuario = tipoUsuario;

                _context.TbUsuarios.Add(usuario);  // Supondo que _context.TbUsuarios seja o DbSet para a entidade de usuários
                _context.SaveChanges();

                return true;  // Retorna true para indicar sucesso
            }
            catch (Exception ex)
            {
                // Trate o erro ou faça um log do ex.Message se necessário
                return false;  // Retorna false para indicar falha
            }
        }

        public List<UsuarioVM> ListarUsuarios()
        {
            List<UsuarioVM> listFun = new List<UsuarioVM>();

            var listTb = _context.TbUsuarios.ToList();

            foreach (var item in listTb)
            {
                var usuarios = new UsuarioVM
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Email = item.Email,
                    Telefone = item.Telefone,
                    Senha = item.Senha,
                    TipoUsuario = item.TipoUsuario,
                };

                listFun.Add(usuarios);
            }

            return listFun;
        }

        public bool AtualizarUsuario(int id, string nome, string email, string telefone, string senha, int tipoUsuario)
        {
            try
            {
                // Busca o usuário pelo ID
                var usuario = _context.TbUsuarios.FirstOrDefault(u => u.Id == id);
                if (usuario != null)
                {
                    // Atualiza os dados do usuário
                    usuario.Nome = nome;
                    usuario.Email = email;
                    usuario.Telefone = telefone;
                    usuario.Senha = senha;  // Não se esqueça de criptografar a senha antes de atualizar
                    usuario.TipoUsuario = tipoUsuario;

                    // Salva as mudanças no banco de dados
                    _context.SaveChanges();

                    return true;  // Retorna verdadeiro se a atualização for bem-sucedida
                }
                else
                {
                    return false;  // Retorna falso se o usuário não foi encontrado
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o usuário com ID {id}: {ex.Message}");
                return false;
            }
        }

        public bool ExcluirUsuario(int id)
        {
            try
            {
                // Busca o usuário pelo ID
                var usuario = _context.TbUsuarios.FirstOrDefault(u => u.Id == id);

                // Se o usuário não for encontrado, lança uma exceção personalizada
                if (usuario == null)
                {
                    throw new KeyNotFoundException("Usuário não encontrado.");
                }


                // Remove o usuário do banco de dados
                _context.TbUsuarios.Remove(usuario);
                _context.SaveChanges();  // Isso pode lançar uma exceção se houver dependências

                // Se tudo correr bem, retorna true indicando sucesso
                return true;

            }
            catch (Exception ex)
            {
                // Aqui tratamos qualquer erro inesperado e logamos para depuração
                Console.WriteLine($"Erro ao excluir o usuário com ID {id}: {ex.Message}");

                // Relança a exceção para ser capturada pelo controlador
                throw new Exception($"Erro ao excluir o usuário: {ex.Message}");
            }
        }


    }
}

