using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.ViewModels.Apps.EXCISEStandard
{
    public class ExciseRequest
    {
        public class Excise
        {
            public  ExciseDetail RequestData;
            
        }

        public class ExciseFile
        {
          
            public ExciseRequestFile RequestData;
        }

        public class ExciseRequestFile
        {
            public List<ExciseMetaDataREquest> documentList { get; set; }
        }
        public class ExciseDetail
        {
            public string nid { get; set; }
            public string regId { get; set; }
            public string cusType { get; set; }
            public string cusTitleCode { get; set; }
            public string cusName { get; set; }
            public string cusSurname { get; set; }
            public string cusHouseno { get; set; }
            public string cusBuildname { get; set; }
            public string cusFloorno { get; set; }
            public string cusRoomno { get; set; }
            public string cusVillage { get; set; }
            public string cusAddrno { get; set; }
            public string cusMoono { get; set; }
            public string cusSoiname { get; set; }
            public string cusThnname { get; set; }
            public string cusTambolCode { get; set; }
            public string cusPoscode { get; set; }
            public string cusTelno { get; set; }
            public string cusEmail { get; set; }
            public string facTitleCode { get; set; }
            public string facName { get; set; }
            public string facSurname { get; set; }
            public string facHouseno { get; set; }
            public string facBuildname { get; set; }
            public string facFloorno { get; set; }
            public string facRoomno { get; set; }
            public string facVillage { get; set; }
            public string facAddrno { get; set; }
            public string facMoono { get; set; }
            public string facSoiname { get; set; }
            public string facThnname { get; set; }
            public string facTambolCode { get; set; }
            public string facPoscode { get; set; }
            public string facTelno { get; set; }
            public string facEmail { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string authType { get; set; }
            public string addrType { get; set; }
            public string ownerType { get; set; }
            public string cusIsic { get; set; }
            public string isicOther { get; set; }
            public string liqAppReqId { get; set; }
            public string liqType1 { get; set; }
            //public string liqType1Id { get; set; }
            public string liqType2 { get; set; }
            //public string liqType2Id { get; set; }
            public string tobAppReqId { get; set; }
            public string tobType1Cig { get; set; }
            //public string tobType1CigId { get; set; }
            public string tobType1Oth { get; set; }
            //public string tobType1OthId { get; set; }
            public string tobType2Cig { get; set; }
            //public string tobType2CigId { get; set; }
            public string tobType2Oth { get; set; }
            public string tobType2OthId { get; set; }
            public string cardType1 { get; set; }
            public string cardAppReqId { get; set; }
            //public string cardType1Id { get; set; }
            public string cardType2 { get; set; }
            //public string cardType2Id { get; set; }
            public List<ExciseMetaData> documentList { get; set; }
        }

        public class ExciseMetaData
        {

            public string docCode { get; set; }
            public string attachFileName { get; set; }
            public string attachBase64 { get; set; }
            /*
            public string Name { get; set; }
            public string Content { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public string Description { get; set; }
            */
        }

        public class ExciseMetaDataREquest
        {

            public string appReqId { get; set; }
            public string docCode { get; set; }
            public string attachFileName { get; set; }
            public string attachBase64 { get; set; }
            /*
            public string Name { get; set; }
            public string Content { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public string Description { get; set; }
            */
        }




    }
}
