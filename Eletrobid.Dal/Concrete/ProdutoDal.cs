using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal
{
    public class ProdutoDal : IProdutoDal
    {
        private readonly EletrobidEntities _dbContext = new EletrobidEntities();
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public Produto EditaProduto(Produto dadosProduto)
        {
            _dbContext.Produto.Attach(dadosProduto);
            _dbContext.Entry(dadosProduto).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            return dadosProduto;
        }

        public void ExcluiProduto(int idProduto)
        {
            var getDadosProduto = GetProdutoId(idProduto);
            _dbContext.Produto.Remove(getDadosProduto);
            _dbContext.SaveChanges();
        }

        public Produto GetProdutoCodigoIdentificador(string codigoIdentificador)
        {
            return (from c in _dbContext.Produto where c.CodigoIdentificador == codigoIdentificador select c).FirstOrDefault();
        }

        public Produto GetProdutoCodigoItem(string codigoItem)
        {
            return (from c in _dbContext.Produto where c.CodigoItem == codigoItem select c).FirstOrDefault();
        }

        public Produto GetProdutoId(int idProduto)
        {
            return (from c in _dbContext.Produto where c.IdProduto == idProduto select c).FirstOrDefault();
        }

        public Produto InsereProduto(Produto dadosProduto)
        {
            var getDadosProduto = _dbContext.Produto.Add(dadosProduto);
            _dbContext.SaveChanges();

            return getDadosProduto;
        }

        public IEnumerable<Produto> ListaProdutos()
        {
            return (from c in _dbContext.Produto select c).ToList();
        }

        public IEnumerable<Produto> ListaProdutosFornecedor(int idEmpresaFornecedora)
        {
            return (from c in _dbContext.Produto where c.IdEmpresaFornecedora == idEmpresaFornecedora select c).ToList();
        }

        public IEnumerable<Produto> ListaProdutosLoteProduto(int loteProduto)
        {
            return (from c in _dbContext.Produto where c.LoteProduto == loteProduto select c).ToList();
        }

        public IEnumerable<Produto> ListaProdutosRevendedor(int idEmpresaRevendedora)
        {
            return (from c in _dbContext.Produto where c.IdEmpresaRevendedora == idEmpresaRevendedora select c).ToList();
        }

        public IEnumerable<Produto> ListaProdutosTipoProduto(int idTipoProduto)
        {
            return (from c in _dbContext.Produto where c.IdTipoProduto == idTipoProduto select c).ToList();
        }
    }
}
