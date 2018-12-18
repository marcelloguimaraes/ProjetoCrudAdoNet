using System;
using System.Web.Mvc;
using Projeto.Models;
using Projeto.Repository;

namespace Projeto.Controllers
{
    public class FuncionarioController : Controller
    {
        private FuncionarioRepository funcionarioRepository = new FuncionarioRepository();

        // GET: Funcionario/SelecionarFuncionarios
        public ActionResult SelecionarFuncionarios()
        {
            ModelState.Clear();
            return View(funcionarioRepository.SelecionarFuncionarios());
        }

        // GET: Funcionario/AdicionarFuncionario
        public ActionResult AdicionarFuncionario()
        {
            return View();
        }

        // POST: Funcionario/AdicionarFuncionario
        [HttpPost]
        public ActionResult AdicionarFuncionario(Funcionario func)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (funcionarioRepository.AdicionarFuncionario(func))
                    {
                        ViewBag.Message = "Funcionário criado com sucesso!";
                    }
                }

                return View();
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }

        // GET: Funcionario/AtualizarFuncionario/5
        public ActionResult AtualizarFuncionario(int id)
        {
            return View(funcionarioRepository.SelecionarFuncionarios()
                            .Find(func => func.IdFuncionario == id));
        }

        // POST: Funcionario/Edit/5
        [HttpPost]
        public ActionResult AtualizarFuncionario(int id, Funcionario funcionario)
        {
            try
            {
                funcionarioRepository.AtualizarFuncionario(funcionario);
                return RedirectToAction("SelecionarFuncionarios");
            }
            catch(Exception e)
            {
                funcionarioRepository.AtualizarFuncionario(funcionario);
                ViewBag.Message = e.Message;
                return View(funcionarioRepository.SelecionarFuncionarios()
                            .Find(func => func.IdFuncionario == id));
            }
        }

        // GET: Funcionario/ExcluirFuncionario/5
        public ActionResult ExcluirFuncionario(int id)
        {
            try
            {
                if (funcionarioRepository.ExcluirFuncionario(id))
                {
                    ViewBag.Message = "Funcionário excluído com sucesso.";
                }

                return View("SelecionarFuncionarios", funcionarioRepository.SelecionarFuncionarios());
            }
            catch(Exception e)
            {
                ViewBag.Message = e.Message;
                return View("SelecionarFuncionarios");
            }
        }
    }
}
