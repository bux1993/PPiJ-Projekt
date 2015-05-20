using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektPPiJ.Models
{
    public class RangListaViewModel
    {

        public List<Uspjeh> Poredak = new List<Uspjeh>();

        public string KategorijaName { get; set; }

    }

    public class Uspjeh
    {
        public string Username { get; set; }

        public int NajboljiRezultat { get; set; }
    }
}