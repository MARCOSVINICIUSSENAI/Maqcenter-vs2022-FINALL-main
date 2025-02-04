using MaqCenter.Models;
using MaqCenter.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace MaqCenter.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(UsuarioRepositorio usuarioRepositorio, ILogger<UsuarioController> logger)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
        }
        

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Cadastro()
        {
            return View();

        }
        public IActionResult Index()
        {
            List<SelectListItem> tipoUsuario = new List<SelectListItem>
             {
                 new SelectListItem { Value = "1", Text = "Administrador" },
                 new SelectListItem { Value = "0", Text = "Cliente" }
             };

            ViewBag.lstTipoUsuario = new SelectList(tipoUsuario, "Value", "Text");

            var Usuarios = _usuarioRepositorio.ListarUsuarios();
            return View(Usuarios);
        }
        public IActionResult VerificarUsuario(string email, string senha)
        {
            try
            {
                // Verifica se os parâmetros foram passados corretamente
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
                {
                    return Json(new { success = false, message = "E-mail e senha são necessários." });
                }

                // Chama o método do repositório para verificar o usuário baseado no e-mail e senha
                var usuario = _usuarioRepositorio.VerificarLogin(email, senha);

                // Verifica se o usuário foi encontrado e autenticado
                if (usuario != null)
                {
                    // Se o usuário foi encontrado, significa que o login foi bem-sucedido
                    return Json(new { success = true, message = "Usuário autenticado com sucesso!" });
                }
                else
                {
                    // Se não encontrar o usuário, a resposta é de falha na autenticação
                    return Json(new { success = false, message = "E-mail ou senha inválidos." });
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro inesperado, captura e exibe o erro
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }
        public IActionResult Logout()
        {
            try
            {
                // Limpar a sessão
                HttpContext.Session.Clear();

                // Limpar as variáveis de ambiente
                Environment.SetEnvironmentVariable("USUARIO_ID", null);
                Environment.SetEnvironmentVariable("USUARIO_NOME", null);
                Environment.SetEnvironmentVariable("USUARIO_EMAIL", null);
                Environment.SetEnvironmentVariable("USUARIO_TELEFONE", null);
                Environment.SetEnvironmentVariable("USUARIO_TIPO", null);

                // Caso esteja usando o SignInManager, também deve chamar o método de logout
                // _signInManager.SignOutAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro, retornar uma resposta de erro com a mensagem
                return Json(new { success = false, message = ex.Message });
            }
        }
        public IActionResult InserirUsuario(string Nome, string Senha, string Email, string Telefone, int TipoUsuario)
        {
            try
            {
                // Chama o método do repositório que realiza a inserção no banco de dados
                var resultado = _usuarioRepositorio.InserirUsuario(Nome, Email, Telefone, Senha, TipoUsuario);

                // Verifica o resultado da inserção
                if (resultado)
                {
                    // Se o resultado for verdadeiro, significa que o usuário foi inserido com sucesso
                    return Json(new { success = true, message = "Cliente inserido com sucesso!" });
                }
                else
                {
                    // Se o resultado for falso, significa que houve um erro ao tentar inserir o usuário
                    return Json(new { success = false, message = "Erro ao inserir o cliente. Tente novamente." });
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro inesperado, captura e exibe o erro
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }
        public IActionResult AtualizarUsuario(int id, string Nome, string Senha, string Email, string Telefone, int TipoUsuario)
        {
            try
            {
                // Chama o repositório para atualizar o usuário
                var resultado = _usuarioRepositorio.AtualizarUsuario(id, Nome, Email, Telefone, Senha, TipoUsuario);

                if (resultado)
                {
                    return Json(new { success = true, message = "Usuário atualizado com sucesso!" });
                }
                else
                {
                    return Json(new { success = false, message = "Erro ao atualizar o usuário. Verifique se o usuário existe." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }
        public IActionResult ExcluirUsuario(int id)
        {
            try
            {
                // Chama o repositório para excluir o usuário
                var resultado = _usuarioRepositorio.ExcluirUsuario(id);

                if (resultado)
                {
                    return Json(new { success = true, message = "Usuário excluído com sucesso!" });
                }
                else
                {
                    // Se o resultado for falso, você pode fornecer uma mensagem mais específica.
                    return Json(new { success = false, message = "Não foi possível excluir o usuário. Verifique se ele está vinculado a outros registros no sistema." });
                }
            }
            catch (Exception ex)
            {
                // Captura qualquer erro e inclui a mensagem detalhada da exceção
                return Json(new { success = false, message = "Erro ao processar a solicitação. Detalhes: " + ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




