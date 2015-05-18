using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektPPiJ.Models
{
    public class QuestionViewModel
    {

        public short vrstaPitanja {get; set;}

        public string tocanOdgovor {get; set;}
        public string ponudjeniOdgovor {get; set;}

        public bool evaluate() {
            return tocanOdgovor == ponudjeniOdgovor;
        }

    }
}