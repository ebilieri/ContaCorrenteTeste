using Conta.Application.Interfaces;
using Conta.Application.ViewModels;
using Conta.WebAPP.Util;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Conta.WebAPP.Controllers
{
    public class ContaCorrenteController : Controller
    {
        private readonly IContaCorrenteApplication _contaCorrenteApplication;
        private readonly string _urlCotacao;

        public ContaCorrenteController(IContaCorrenteApplication veiculoApplication)
        {
            _contaCorrenteApplication = veiculoApplication;
            _urlCotacao = "https://economia.awesomeapi.com.br/all/USD-BRL";
        }

        [HttpGet]
        public IActionResult Index()
        {
            var contas = _contaCorrenteApplication.ObterTodos();

            return View(contas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ContaCorrenteModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ContaCorrenteModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                _contaCorrenteApplication.Adicionar(model);

                if (model.IdConta > 0)
                {
                    return RedirectToAction("index");
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
            ContaCorrenteModel model = new ContaCorrenteModel();
            if (id.HasValue && id != 0)
            {
                model = _contaCorrenteApplication.ObterPorId(Convert.ToInt32(id));


                model.CotacaoDolar = Convert.ToDecimal(HttpUtil.Get(_urlCotacao).Bid);
                if (model.ValorAtual > 0)
                    model.ValorAtualDolar = model.ValorAtual / model.CotacaoDolar;

            }

            return View(model);
        }

    }
}
