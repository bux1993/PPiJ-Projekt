using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektPPiJ.Models
{
    public class QuestionViewModel
    {

        public short vrstaPitanja { get; set; }

        public string pitanje { get; set; }

        public string tocanOdgovor { get; set; }
        public string ponudjeniOdgovor { get; set; }

        public int KategorijaID { get; set; }

        public int evaluate()
        {
            if (tocanOdgovor == ponudjeniOdgovor)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}