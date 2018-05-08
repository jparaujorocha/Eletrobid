﻿using Eletrobid.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eletrobid.Models;
using ExcelDataReader;
using System.IO;

namespace Eletrobid.Controllers
{
    public class NfeController : Controller
    {
        private readonly IProdutoDal _produtoDal;
        private readonly IEmpresaDal _empresaDal;
        private readonly IImpostoDal _impostoDal;
        private readonly IImpostoProdutoDal _impostoProdutoDal;
        private readonly ITipoProdutoDal _tipoProduto;
        private readonly IVendaDal _vendaDal;
        private readonly INfeDal _nfeDal;
        // GET: Nfe

        private readonly string caminhoAddProdutos = @"C:\Users\joaopedro\Desktop\BDL\Produtos_Adicionados\";

        public NfeController(IProdutoDal produtoDal, IImpostoDal impostoDal, IImpostoProdutoDal impostoProdutoDal, ITipoProdutoDal tipoProduto, IVendaDal vendaDal, IEmpresaDal empresaDal, INfeDal nfeDal)
        {
            _produtoDal = produtoDal;
            _impostoDal = impostoDal;
            _impostoProdutoDal = impostoProdutoDal;
            _tipoProduto = tipoProduto;
            _vendaDal = vendaDal;
            _empresaDal = empresaDal;
            _nfeDal = nfeDal;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GerenciaNfe()
        {
            Produto dadosProduto = new Produto();
            var listaTipoNfe = new SelectList(_nfeDal.ListaTipoNfe(), "IdTipoNotaFiscal", "Nome");
            var listaEmpresaFornecedora = new SelectList(_empresaDal.ListaEmpresas(1), "IdEmpresa", "RazaoSocial");

            ViewBag.ListaTipoNfe = listaTipoNfe;
            ViewBag.ListEmpresaFornecedora = listaEmpresaFornecedora;

            var notas = _nfeDal.ListaNotas();
            return View(notas);
        }

        public ActionResult DetalhesNfe(int idNfe)
        {
            var dadosNfe = _nfeDal.GetNfe(idNfe);

            if (dadosNfe != null)
            {
                return View(dadosNfe);
            }
            else
            {
                return RedirectToAction("GerenciaNfe");
            }
        }

        public ActionResult InserirProduto(int idNfe)
        {
            var listaTipoProduto = new SelectList(_tipoProduto.ListaTipoProduto(), "IdTipoProduto", "Nome");
            var listaEmpresaFornecedora = new SelectList(_empresaDal.ListaEmpresas(1), "IdEmpresa", "RazaoSocial");

            if (_nfeDal.ExisteNota(idNfe) == true)
            {
                Produto dadosProduto = new Produto();
                dadosProduto.IdNfe = idNfe;
                ViewBag.ListTipoProduto = listaTipoProduto;
                ViewBag.ListEmpresaFornecedora = listaEmpresaFornecedora;

                return View(dadosProduto);
            }
            else
            {
                return RedirectToAction("GerenciaNfe");
            }
        }

        [HttpPost]
        public ActionResult InserirProduto(Produto dadosProduto)
        {
            var listaTipoProduto = new SelectList(_tipoProduto.ListaTipoProduto(), "IdTipoProduto", "Nome");
            var listaEmpresaFornecedora = new SelectList(_empresaDal.ListaEmpresas(1), "IdEmpresa", "RazaoSocial");
            ViewBag.ListTipoProduto = listaTipoProduto;
            ViewBag.ListEmpresaFornecedora = listaEmpresaFornecedora;

            if (ModelState.IsValid)
            {
                if (_nfeDal.ExisteNota(dadosProduto.IdNfe) == false)
                {
                    return RedirectToAction("GerenciaNfe");
                }

                if (!string.IsNullOrWhiteSpace(dadosProduto.CodigoItem) && !string.IsNullOrEmpty(dadosProduto.CodigoItem))
                {
                    var getProduto = _produtoDal.GetProdutoCodigoItem(dadosProduto.CodigoItem, dadosProduto.IdEmpresaFornecedora);

                    if (getProduto == null)
                    {
                        var produtoInserido = _produtoDal.InsereProduto(dadosProduto);
                        ModelState.Clear();
                        Produto getIdNfe = new Produto();
                        getIdNfe.IdNfe = produtoInserido.IdNfe;
                        _nfeDal.AtualizaQtde(produtoInserido.IdNfe);
                        return View(getIdNfe);
                    }
                    else
                    {
                        getProduto.Quantidade = getProduto.Quantidade + dadosProduto.Quantidade;
                        _produtoDal.EditaProduto(getProduto);
                        ModelState.Clear();
                        Produto getIdNfe = new Produto();
                        getIdNfe.IdNfe = getProduto.IdNfe;
                        _nfeDal.AtualizaQtde(getProduto.IdNfe);
                        return View(getIdNfe);
                    }
                }
                else
                {
                    return View(dadosProduto);
                }

            }

            return View(dadosProduto);
        }

        public ActionResult InserirProdutos(int idNfe)
        {
            var listaTipoProduto = new SelectList(_tipoProduto.ListaTipoProduto(), "IdTipoProduto", "Nome");
            var listaEmpresaFornecedora = new SelectList(_empresaDal.ListaEmpresas(1), "IdEmpresa", "RazaoSocial");

            if (_nfeDal.ExisteNota(idNfe) == true)
            {
                Produto dadosProduto = new Produto();

                dadosProduto.IdNfe = idNfe;
                ViewBag.ListTipoProduto = listaTipoProduto;
                ViewBag.ListEmpresaFornecedora = listaEmpresaFornecedora;
                return View(dadosProduto);
            }

            else
            {
                return RedirectToAction("GerenciaNfe");
            }
        }

        [HttpPost]
        public ActionResult InserirProdutos(Produto dadosProduto, HttpPostedFileBase[] planilhas)
        {
            List<string> listaErros = new List<string>();
            int numeroErros = 0;
            int numeroProdutosAdicionados = 0;
            int numeroLinhasCorretas = 0;
            var listaTipoProduto = new SelectList(_tipoProduto.ListaTipoProduto(), "IdTipoProduto", "Nome");
            var listaEmpresaFornecedora = new SelectList(_empresaDal.ListaEmpresas(1), "IdEmpresa", "RazaoSocial");

            ViewBag.ListTipoProduto = listaTipoProduto;
            ViewBag.ListEmpresaFornecedora = listaEmpresaFornecedora;
            ModelState.Remove("Nome");
            ModelState.Remove("CodigoItem");
            ModelState.Remove("Quantidade");
            if (ModelState.IsValid)
            {
                //Verifica se a Nota Fiscal existe no Banco
                if (_nfeDal.ExisteNota(dadosProduto.IdNfe) == false)
                {
                    return RedirectToAction("GerenciaProduto");
                }

                //Verificar se há arquivos.
                if (planilhas[0] != null)
                {
                    foreach (var item in planilhas)
                    {
                        //Verifica se são planilhas no formato xls ou xlsx
                        if (Path.GetExtension(item.FileName) == ".xlsx" || Path.GetExtension(item.FileName) == ".xls")
                        {
                            IExcelDataReader excelReader;
                            if (Path.GetExtension(item.FileName) == ".xls")
                            {
                                excelReader = ExcelReaderFactory.CreateBinaryReader(item.InputStream);
                            }
                            else
                            {
                                excelReader = ExcelReaderFactory.CreateOpenXmlReader(item.InputStream);
                            }
                            var tabela = excelReader.AsDataSet().Tables[0];

                            //Verificar se o número de colunas corresponde ao padrão.
                            if (tabela.Columns.Count == 3)
                            {
                                var conteudo = tabela.Rows;

                                for (int i = 1; i < conteudo.Count; i++)
                                {
                                    //Verificar se Alguma coluna da linha está nula
                                    if (!conteudo[i].IsNull(0) && !conteudo[i].IsNull(1) && !conteudo[i].IsNull(2))
                                    {
                                        var dadosLinha = conteudo[i].ItemArray;

                                        //Verifica se os padrões exigidos foram seguidos
                                        if (!string.IsNullOrWhiteSpace(dadosLinha[0].ToString()) && !string.IsNullOrEmpty(dadosLinha[0].ToString()) && !string.IsNullOrWhiteSpace(dadosLinha[1].ToString()) && !string.IsNullOrEmpty(dadosLinha[1].ToString()) && dadosLinha[1].ToString().Length >= 5 && dadosLinha[1].ToString().Length <= 255 && !string.IsNullOrWhiteSpace(dadosLinha[2].ToString()) && !string.IsNullOrEmpty(dadosLinha[2].ToString()) && Convert.ToInt32(dadosLinha[2].ToString()) > 0)
                                        {
                                            Produto dadosInsercao = new Produto();

                                            dadosInsercao.IdEmpresaFornecedora = dadosProduto.IdEmpresaFornecedora;
                                            dadosInsercao.IdTipoProduto = dadosProduto.IdTipoProduto;
                                            dadosInsercao.IdNfe = dadosProduto.IdNfe;
                                            dadosInsercao.CodigoItem = dadosLinha[0].ToString();
                                            dadosInsercao.Nome = dadosLinha[1].ToString();
                                            dadosInsercao.Quantidade = Convert.ToInt32(dadosLinha[2].ToString());

                                            var getProduto = _produtoDal.GetProdutoCodigoItem(dadosInsercao.CodigoItem, dadosProduto.IdEmpresaFornecedora);

                                            if (getProduto == null)
                                            {
                                                _produtoDal.InsereProduto(dadosInsercao);
                                                numeroProdutosAdicionados += dadosInsercao.Quantidade;
                                                numeroLinhasCorretas++;
                                            }
                                            else
                                            {
                                                getProduto.Quantidade = getProduto.Quantidade + dadosInsercao.Quantidade;
                                                _produtoDal.EditaProduto(getProduto);
                                                numeroProdutosAdicionados += dadosInsercao.Quantidade;
                                                numeroLinhasCorretas++;
                                            }

                                        }//Fim do IF das verificações dos padrões

                                        else
                                        {
                                            var erro = "Verifique se os dados da linha " + (i + 1) + " do Arquivo \"" + item.FileName + "\" Estão de arcodo com os padrões";
                                            numeroErros++;
                                            listaErros.Add(erro);

                                        }//Fim do ELSE das verificações dos padrões

                                    }//Fim do IF das verificações de linhas nulas

                                    else
                                    {
                                        var erro = "Verifique se os dados da linha " + (i + 1) + " do Arquivo \"" + item.FileName + "\" Estão preenchidos e de arcodo com os padrões";
                                        numeroErros++;
                                        listaErros.Add(erro);

                                    }//Fim do ELSE das verificações de linhas nulas
                                }
                            }//Fim do IF da verificação de número de colunas

                            else
                            {
                                var erro = "Verifique se o Arquivo \"" + item.FileName + "\" Contém o número de colunas do nosso padrão.";
                                numeroErros++;
                                listaErros.Add(erro);
                            }//Fim do ELSE da verificação de número de colunas

                        }//Fim do IF da verificação de extensão do arquivo

                        else
                        {
                            var erro = "Verifique se o Arquivo \"" + item.FileName + "\" é um arquivo de formato .xls ou .xlsx. Apenas essas extensões são suportadas.";
                            numeroErros++;
                            listaErros.Add(erro);

                        }//Fim do ELSE da verificação de extensão do arquivo

                    }

                    //Atualizar Quantidade de produtos da Nfe
                    _nfeDal.AtualizaQtde(dadosProduto.IdNfe);
                    TempData["listaErros"] = listaErros;
                    return RedirectToAction("ResultadoLeitura", new { idNfe = dadosProduto.IdNfe, numeroErros = numeroErros, numeroLinhasCorretas = numeroLinhasCorretas, numeroProdutosAdicionados = numeroProdutosAdicionados });

                }//Fim do IF para verificar se existe arquivos
                else
                {
                    TempData["inserirPlanilha"] = "Adicione Planilhas para a leitura.";
                    return View(dadosProduto);
                }//Fim do ELSE para verificar se existe arquivos
            }

            return View(dadosProduto);
        }

        public ActionResult ResultadoLeitura(int idNfe, int numeroErros, int numeroLinhasCorretas, int numeroProdutosAdicionados)
        {
            var listaErros = (List<string>)TempData["listaErros"];

            ViewBag.NumeroErros = numeroErros;
            ViewBag.NumeroLinhasCorretas = numeroLinhasCorretas;
            ViewBag.NumeroProdutosAdicionados = numeroProdutosAdicionados;
            ViewBag.idNfe = idNfe;
            return View();
        }
    }
}