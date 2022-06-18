using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Models
{


    public class ResultData<T>
    {
        public List<T> Result { get; set; }
        public string Status { get; set; }

    }


    public class DEDEGeoData
    {
        public List<GeoInfo> Data { get; set; }
    }


    public class GeoInfo
    {

        public int id { get; set; }
        public string name { get; set; }
        public string en_name { get; set; }
        public string code { get; set; }
        public int ref_citie_id { get; set; }
        public int gis_basis_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

    }


}
