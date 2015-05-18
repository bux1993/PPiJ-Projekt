using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektPPiJ.Models
{
    public class TestViewModel
    {

        public TestViewModel() {
            pitanja = new List<Pitanja>();
            odgovori = new List<QuestionViewModel>();
        }

        [HiddenInput]
        public List<Pitanja> pitanja { get; set; }
        public List<QuestionViewModel> odgovori { get; set; }
    
        public void generirajDummyOdgovore() {
            foreach (Pitanja pitanje in this.pitanja) {
                QuestionViewModel odgovor = new QuestionViewModel();
                odgovor.vrstaPitanja = pitanje.VrstaPitanja;
                odgovor.tocanOdgovor = pitanje.TocanOdgovor;
                odgovor.ponudjeniOdgovor = "Nije odgovoreno";
                odgovori.Add(odgovor);
            }
        }

    }
}