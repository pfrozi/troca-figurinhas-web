﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrocaFigurinhas.Models.ModelView;
using TrocaFigurinhas.Models.Persistence;

namespace TrocaFigurinhas.Models.Business
{
    public class BuscaBusiness
    {
        public List<OfertaTrocaModelView> BuscarFigurinhasOferecidas(string user, string nomeFigurinha)
        {
            List<OfertaTrocaModelView> ofertasFigurinhasOferecidas = new List<OfertaTrocaModelView>();
            
            TrocaBusiness trocaBusiness = new TrocaBusiness();
            OfertasBusiness ofertasBusiness = new OfertasBusiness();

            List<Ofertas> ofertas = ofertasBusiness.BuscarOfertadasPorNome(user, nomeFigurinha);

            if (ofertas.Count == 0)
            {
                throw new BusinessException("Nenhuma oferta encontrada.");
            }

            foreach (Ofertas oferta in ofertas)
            {
                OfertaTrocaModelView ofertaTroca = new OfertaTrocaModelView
                {
                    Oferta = oferta,
                    MinhasOfertas = trocaBusiness.buscarTrocasCompletas(user, oferta.Id)
                };

                ofertaTroca.permiteTroca = (ofertaTroca.MinhasOfertas.Count > 0);
                ofertasFigurinhasOferecidas.Add(ofertaTroca);
            }

            return ofertasFigurinhasOferecidas;
        }


        public List<OfertaTrocaModelView> BuscarFigurinhasDesejadas(string user, string nomeFigurinha)
        {

            List<OfertaTrocaModelView> ofertasFigurinhasOferecidas = new List<OfertaTrocaModelView>();

            TrocaBusiness trocaBusiness = new TrocaBusiness();
            OfertasBusiness ofertasBusiness = new OfertasBusiness();

            List<Ofertas> ofertas = ofertasBusiness.BuscarDesejadasPorNome(user, nomeFigurinha);
            

            if (ofertas.Count == 0)
            {
                throw new BusinessException("Nenhuma oferta encontrada.");
            }

            foreach (Ofertas oferta in ofertas)
            {
                OfertaTrocaModelView ofertaTroca = new OfertaTrocaModelView
                {
                    Oferta = oferta,
                    MinhasOfertas = trocaBusiness.buscarTrocasCompletas(user, oferta.Id)
                };

                ofertaTroca.permiteTroca = (ofertaTroca.MinhasOfertas.Count > 0);
                ofertasFigurinhasOferecidas.Add(ofertaTroca);
            }

            return ofertasFigurinhasOferecidas;

        }
         
    }
}