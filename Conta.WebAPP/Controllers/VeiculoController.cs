//using Frota.Application.Interfaces;
//using Frota.Application.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Linq;

//namespace Frota.WebAPP.Controllers
//{
//    public class VeiculoController : Controller
//    {
//        private readonly IVeiculoApplication _veiculoApplication;

//        public VeiculoController(IVeiculoApplication veiculoApplication)
//        {
//            _veiculoApplication = veiculoApplication;
//        }

//        [HttpGet]
//        public IActionResult Index()
//        {
//            var veiculos = _veiculoApplication.ObterTodos();

//            return View(veiculos);
//        }

//        [HttpPost]
//        public IActionResult Index(string chassi)
//        {
//            if (string.IsNullOrWhiteSpace(chassi))
//            {
//                string message = string.Format("Atenção: Informe um valor para pesquisar");
//                ModelState.AddModelError(string.Empty, message);
//                return View();
//            }
//            else
//            {
//                var veiculos = _veiculoApplication.ObterTodos(chassi);

//                if (veiculos.Count() == 0)
//                {
//                    string message = string.Format("Atenção: Nenhum Veículo encontrado");
//                    ModelState.AddModelError(string.Empty, message);
//                    return View();
//                }

//                return View(veiculos);
//            }
//        }

//        [HttpGet]
//        public IActionResult SaveOrEdit(int? id)
//        {
//            VeiculoModel model = new VeiculoModel();

//            try
//            {
//                if (id.HasValue && id != 0)
//                {
//                    model = _veiculoApplication.ObterPorId(Convert.ToInt32(id));
//                }
//            }
//            catch (Exception ex)
//            {
//                string message = string.Format("Atenção: {0}", ex.Message);
//                ModelState.AddModelError(string.Empty, message);
//            }

//            return View(model);
//        }

//        [HttpPost]
//        public IActionResult SaveOrEdit(VeiculoModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            try
//            {
//                if (model.ID == 0)
//                {
//                    _veiculoApplication.Adicionar(model);

//                    if (model.ID > 0)
//                    {
//                        return RedirectToAction("index");
//                    }
//                    else if (model.MensagemValidacao != null && model.MensagemValidacao.Count > 0)
//                    {
//                        foreach (var item in model.MensagemValidacao)
//                        {
//                            string message = string.Format("Atenção: {0}", item);
//                            ModelState.AddModelError(string.Empty, message);
//                        }
//                    }
//                }
//                else
//                {
//                    var veiculoEntity = _veiculoApplication.ObterPorId(model.ID);

//                    _veiculoApplication.Atualizar(model);

//                    if (veiculoEntity.ID > 0)
//                    {
//                        return RedirectToAction("index");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                string message = string.Format("Atenção: {0}", ex.Message);
//                ModelState.AddModelError(string.Empty, message);
//            }

//            return View(model);
//        }

//        public IActionResult Detail(int? id)
//        {
//            VeiculoModel model = new VeiculoModel();
//            if (id.HasValue && id != 0)
//            {
//                model = _veiculoApplication.ObterPorId(Convert.ToInt32(id));

//            }

//            return View(model);
//        }

//        [HttpGet]
//        public IActionResult Delete(int id)
//        {
//            VeiculoModel model = new VeiculoModel();
//            if (id != 0)
//            {
//                model = _veiculoApplication.ObterPorId(Convert.ToInt32(id));

//            }

//            return View(model);
//        }


//        [HttpPost, ActionName("Delete")]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            try
//            {
//                if (id != 0)
//                {
//                    _veiculoApplication.Remover(id);

//                    return RedirectToAction("Index");
//                }
//                return View();
//            }
//            catch (Exception ex)
//            {
//                string message = string.Format("Atenção: {0}", ex.Message);
//                ModelState.AddModelError(string.Empty, message);
//                return View();
//            }
//        }
//    }
//}
