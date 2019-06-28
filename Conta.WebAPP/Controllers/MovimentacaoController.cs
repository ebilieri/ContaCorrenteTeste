using Conta.Application.Interfaces;
using Conta.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conta.WebAPP.Controllers
{
    public class MovimentacaoController : Controller
    {
        private readonly IMovimentacaoApplication _movimentacaoApplication;
        public MovimentacaoController(IMovimentacaoApplication veiculoApplication)
        {
            _movimentacaoApplication = veiculoApplication;
        }

        [HttpGet]
        public IActionResult Index(int idConta)
        {
            var contas = _movimentacaoApplication.ObterPorIdConta(idConta);

            return View(contas);
        }

        [HttpGet]
        public IActionResult Debitar(int idConta)
        {
            var model = new MovimentacaoModel();
            model.ContaId = idConta;

            return View(model);
        }

        [HttpPost]
        public IActionResult Debitar(MovimentacaoModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _movimentacaoApplication.Debitar(model);

                if (model.IdMovimentacao > 0)
                {
                    return RedirectToAction("Index", "ContaCorrente");
                }
                else if (model.MensagemValidacao != null && model.MensagemValidacao.Count > 0)
                {
                    foreach (var item in model.MensagemValidacao)
                    {
                        string message = string.Format("Atenção: {0}", item);
                        ModelState.AddModelError(string.Empty, message);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Atenção: {0}", ex.Message);
                ModelState.AddModelError(string.Empty, message);
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Creditar(int idConta)
        {
            var model = new MovimentacaoModel();
            model.ContaId = idConta;

            return View(model);
        }

        [HttpPost]
        public IActionResult Creditar(MovimentacaoModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _movimentacaoApplication.Creditar(model);

                if (model.IdMovimentacao > 0)
                {
                    return RedirectToAction("Index", "ContaCorrente");
                }
                else if (model.MensagemValidacao != null && model.MensagemValidacao.Count > 0)
                {
                    foreach (var item in model.MensagemValidacao)
                    {
                        string message = string.Format("Atenção: {0}", item);
                        ModelState.AddModelError(string.Empty, message);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("Atenção: {0}", ex.Message);
                ModelState.AddModelError(string.Empty, message);
            }

            return View(model);
        }
        public IActionResult Detail(int? id)
        {
            var model = new MovimentacaoModel();
            if (id.HasValue && id != 0)
            {
                //model = _movimentacaoApplication.ObterPorId(Convert.ToInt32(id));

            }

            return View(model);
        }


    }
}
