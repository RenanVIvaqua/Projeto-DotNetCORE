using System;
using System.Collections.Generic;

namespace ProAgil.Domain
{
    public class RedeSocial
    {
        public int id {get;set;}

        public string Nome {get;set;}

        public string URL {get;set;}

        public int? IdEvento {get;set;}

        public Evento Evento {get;}

        public int IdPalestrante {get;set;}

        public Palestrante Palestrante {get;}  

    }
}