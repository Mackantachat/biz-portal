using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels
{
    public class ShipMortgageViewModel
    {
        public string SHIPSERIAL { get; set; }
        public string SHIPCODE { get; set; }
        public string THANAME { get; set; }
        public string ENGNAME { get; set; }
        public string SHIPTYPE { get; set; }
        public string USETYPENAME { get; set; }
        public string SHIPSTATUS { get; set; }
        public string LOCREG { get; set; }
        public string REGLOC { get; set; }
        public string TGROSS { get; set; }
        public string TNET { get; set; }
        public string WIDTH { get; set; }
        public string SHIP_LENGTH { get; set; }
        public string SHIP_LENGTH1 { get; set; }
        public string DEPTH { get; set; }
        public string PROMISENO { get; set; }
        public string PROMISEDATE { get; set; }
        public string AMOUNT { get; set; }
        public string INC_AMOUNT { get; set; }
        public string DEC_AMOUNT { get; set; }
        public string AMT_DOLLAR { get; set; }
        public string INC_DOLLAR { get; set; }
        public string DEC_DOLLAR { get; set; }
        public List<Mortgage> MORTGAGE { get; set; }
        public List<ShipOwner> SHIPOWNER { get; set; }
    }

    public class Mortgage
    {
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
    }

    public class ShipOwner {
        public string IDCARD { get; set; }
        public string CITIZEN { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
    }

    //public class WSResponseData
    //{
    //    public string result { get; }
    //    public string data { get; }
    //}
}
