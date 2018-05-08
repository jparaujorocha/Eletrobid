using Eletrobid.Dal.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eletrobid.Models;

namespace Eletrobid.Dal.Concrete
{
    public class NfeDal : INfeDal
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

        public Nfe EditarNota(Nfe dadosNota)
        {
            _dbContext.Nfe.Attach(dadosNota);
            _dbContext.Entry(dadosNota).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            return dadosNota;
        }

        public Nfe GetNfe(string chaveDeAcesso)
        {
            return (from c in _dbContext.Nfe where c.ChaveAcesso == chaveDeAcesso select c).FirstOrDefault();
        }
        public int GetNumeroUltimaNfe()
        {
            var dadosNota = (from c in _dbContext.Nfe select c).OrderByDescending(c => c.NumeroNota).FirstOrDefault();

            if (dadosNota == null)
            {
                return 0;
            }
            else
            {
                return dadosNota.NumeroNota;
            }
        }
        public Nfe GetNfe(int idNfe)
        {
            return (from c in _dbContext.Nfe where c.IdNfe == idNfe select c).FirstOrDefault();
        }

        public Nfe InserirNota(Nfe dadosNota)
        {
            var getDadosNota = _dbContext.Nfe.Add(dadosNota);
            _dbContext.SaveChanges();
            return getDadosNota;
        }

        public IEnumerable<TipoNotaFiscal> ListaTipoNfe()
        {
            return (from c in _dbContext.TipoNotaFiscal select c).ToList();
        }

        public IEnumerable<Nfe> ListaNotasCnpj(string cnpj)
        {
            return (from c in _dbContext.Nfe where c.CnpjDestinatario == cnpj select c).ToList();
        }

        public IEnumerable<Nfe> ListaNotasEntrada()
        {
            return (from c in _dbContext.Nfe where c.IdTipoNotaFiscal == 4 select c).ToList();
        }

        public IEnumerable<Nfe> ListaNotas()
        {
            return (from c in _dbContext.Nfe select c).ToList();
        }
        public IEnumerable<Nfe> ListaNotasCpf(string cpf)
        {
            return (from c in _dbContext.Nfe where c.CpfDestinatario == cpf select c).ToList();
        }

        public bool ExisteNota(int idNfe)
        {
            return (from c in _dbContext.Nfe where c.IdNfe == idNfe select c).Any();
        }

        public void AtualizaQtde(int idNfe)
        {
            var dadosNota = GetNfe(idNfe);
            int novaQntde = dadosNota.Produto.Sum(c => c.Quantidade);
            dadosNota.QtdeProdutos = novaQntde;
            EditarNota(dadosNota);
        }
    }
}
