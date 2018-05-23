using Eletrobid.Dal;
using Eletrobid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eletrobid.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly IEmpresaDal _empresaDal;
        private readonly IEstadoDal _estadoDal;
        private readonly ICidadeDal _cidadeDal;


        public EmpresaController(IEmpresaDal empresaDal, IEstadoDal estadoDal, ICidadeDal cidadeDal)
        {
            _empresaDal = empresaDal;
            _estadoDal = estadoDal;
            _cidadeDal = cidadeDal;
        }

        // GET: Empresa
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GerenciaEmpresas()
        {
            var dadosEmpresa = _empresaDal.ListaEmpresas();
            return View(dadosEmpresa);
        }

        public ActionResult DetalhesEmpresa(int idEmpresa)
        {
            var dadosEmpresa = _empresaDal.GetEmpresa(idEmpresa);
            var listaEstado = new SelectList(_estadoDal.ListaEstados(), "EstadoId", "Sigla");
            var listaCidade = new SelectList(_cidadeDal.ListaCidades(), "CidadeId", "Nome");
            var tipoEmpresa = new SelectList(_empresaDal.ListaTipoEmpresa(), "IdTipoEmpresa", "Nome");
            ViewBag.ListaEstados = listaEstado;
            ViewBag.ListaCidades = listaCidade;
            ViewBag.TipoEmpresa = tipoEmpresa;

            return View(dadosEmpresa);
        }
        

        [HttpPost]
        public ActionResult DetalhesEmpresa(Empresa dadosEmpresa)
        {
            if (ModelState.IsValid)
            {
                return View(dadosEmpresa);
            }
            else
            {
                var listaEstado = new SelectList(_estadoDal.ListaEstados(), "EstadoId", "Sigla");
                var listaCidade = new SelectList(_cidadeDal.ListaCidades(), "CidadeId", "Nome");
                var tipoEmpresa = new SelectList(_empresaDal.ListaTipoEmpresa(), "IdTipoEmpresa", "Nome");
                ViewBag.ListaEstados = listaEstado;
                ViewBag.ListaCidades = listaCidade;
                ViewBag.TipoEmpresa = tipoEmpresa;

                return View(dadosEmpresa);
            }

        }

    }
}