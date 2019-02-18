using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TriagemService.Data.Interfaces;
using TriagemService.Data.Models;
using TriagemService.DataContext;

namespace TriagemService.Services.Messages
{
    public class TriagemApiService
    {

        public TriagemCanceladaMessageResponse CancelarTriagem(Guid idTriagem, AppDataContext _context)
        {
            ITriagemRepository TriagemRepository = new TriagemRepository(_context);

            TriagemCanceladaMessageResponse response = new TriagemCanceladaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Triagem Cancelada Com Sucesso";
            response.TriagemCancelada = new TriagemMessage();

            var triagem = TriagemRepository.GetById(idTriagem);

            if (triagem == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Triagem Não encontrada";
                return response;
            }

            if (triagem.StatusTriagem != "Pendente" && triagem.StatusTriagem != "Confirmada")
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Triagem não pode ser cancelada. Veja seu Status";
                response.TriagemCancelada =  PrepararTriagemRetorno(triagem);;
                return response;
            }

            var dataAgora = DateTime.Now;
            triagem.DataStatus = dataAgora;
            triagem.StatusTriagem = "Cancelada";

            TriagemRepository.Update(triagem);
            response.TriagemCancelada =  PrepararTriagemRetorno(triagem);
            return response;
        }

        public TriagemConfirmadaMessageResponse ConfirmarTriagem(Guid idTriagem, AppDataContext _context)
        {
            ITriagemRepository TriagemRepository = new TriagemRepository(_context);

            TriagemConfirmadaMessageResponse response = new TriagemConfirmadaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Triagem Confirmada Com Sucesso";
            response.TriagemConfirmada = new TriagemMessage();

            var triagem = TriagemRepository.GetById(idTriagem);

            if (triagem == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Triagem Não encontrada";
                return response;
            }


            if (triagem.StatusTriagem != "Pendente")
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Triagem não pode ser confirmada. Veja seu Status";
                response.TriagemConfirmada = PrepararTriagemRetorno(triagem);
                return response;
            }


            var dataAgora = DateTime.Now;
            triagem.DataStatus = dataAgora;
            triagem.StatusTriagem = "Cancelada";

            TriagemRepository.Update(triagem);
            response.TriagemConfirmada = PrepararTriagemRetorno(triagem);
            return response;
        }

        public TriagemFinalizadaMessageResponse FinalizarTriagem(Guid idTriagem, AppDataContext _context)
        {
            ITriagemRepository TriagemRepository = new TriagemRepository(_context);

            TriagemFinalizadaMessageResponse response = new TriagemFinalizadaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Triagem Finalizada Com Sucesso";
            response.TriagemFinalizada = new TriagemMessage();

            var triagem = TriagemRepository.GetById(idTriagem);

            if (triagem == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Triagem Não encontrada";
                return response;
            }


            if (triagem.StatusTriagem != "Confirmada")
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Triagem não pode ser finalizada. Veja seu Status";
                response.TriagemFinalizada = PrepararTriagemRetorno(triagem);
                return response;
            }

            var dataAgora = DateTime.Now;
            triagem.DataStatus = dataAgora;
            triagem.StatusTriagem = "Finalizada";

            TriagemRepository.Update(triagem);
            response.TriagemFinalizada = PrepararTriagemRetorno(triagem);
            return response;
        }

        public ObterListaTriagemStatusMessageResponse ObterTriagensPorStatus(string status, AppDataContext _context)
        {
            ITriagemRepository estoqueRepository = new TriagemRepository(_context);

            ObterListaTriagemStatusMessageResponse response = new ObterListaTriagemStatusMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = String.Format("Triagens {0}s Retornados com sucesso", status);

            var triagens = estoqueRepository.FindTriagemStatus(status);

            if (triagens == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = String.Format("Não existem triagens com o status: {0}", status);
            }

            foreach (Triagem triagem in triagens)
            {
                response.ListaTriagemStatus.Add(PrepararTriagemRetorno(triagem));
            }

            return response;
        }

        public ObterTriagemExpiradaMessageResponse ObterTriagemExpirada(AppDataContext _context)
        {
            ITriagemRepository estoqueRepository = new TriagemRepository(_context);

            ObterTriagemExpiradaMessageResponse response = new ObterTriagemExpiradaMessageResponse();
            response.codRetorno = 0;
            response.StatusRetorno = "Triagens expiradas Retornadas com sucesso";

            var triagens = estoqueRepository.FindTriagemExpirada();

            if (triagens == null)
            {
                response.codRetorno = 1;
                response.StatusRetorno = "Não existem triagens expiradas";
            }

            foreach (Triagem triagem in triagens)
            {
                response.ListaTriagensExpiradas.Add(PrepararTriagemRetorno(triagem));
            }

            return response;
        }

        private TriagemMessage PrepararTriagemRetorno(Triagem triagem)
        {
            var triagemMessage = new TriagemMessage()
            {
                DataCriacao = triagem.DataCriacao,
                DataStatus = triagem.DataCriacao,
                EmailOperador = triagem.Operador.Email,
                Operador = triagem.Operador.NomeOperador,
                IdTriagem = triagem.Id,
                LoteDescarte = triagem.LoteDescarte,
                StatusTriagem = triagem.StatusTriagem
            };

            return triagemMessage;
        }
    }
}