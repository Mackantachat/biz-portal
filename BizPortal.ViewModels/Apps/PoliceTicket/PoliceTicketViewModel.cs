using BizPortal.Utils.JsonConverter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.Utils.Helpers;
using System.Drawing.Imaging;
using System.Configuration;

namespace BizPortal.ViewModels.Apps.PoliceTicket
{
    public class PoliceTicketViewModel
    {
        public bool Result { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public List<TicketDetail> Tickets
        {
            get
            {
                try
                {
                    return JsonConvert.DeserializeObject<List<TicketDetail>>(Data.ToString());
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
        }
    }

    public class TicketDetail
    {
        #region Ticket
        [JsonProperty("ticket_ID")]
        public string TicketID { get; set; }

        [JsonProperty("ticket_DATE")]
        [JsonConverter(typeof(DateFormatConverter), "yyyyMMddHHmm", "en")]
        public DateTime TicketDate { get; set; }

        [JsonProperty("ticket_STATUS")]
        public TicketStatusEnum TicketStatus { get; set; }
        #endregion

        #region Fine
        [JsonProperty("fine_AMT")]
        public decimal FineAmount { get; set; }

        [JsonProperty("fine_DUE_DATE")]
        [JsonConverter(typeof(DateFormatConverter), "yyyyMMddHHmm", "en")]
        public DateTime FineDueDate { get; set; }
        #endregion

        #region Paid
        [JsonProperty("paid_DATE")]
        public string PaidDate { get; set; }
        [JsonProperty("paid_BY")]
        public string PaidBy { get; set; }
        #endregion

        #region Person
        [JsonProperty("card_ID")]
        public string CardID { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        private string _Plate { get; set; }

        [JsonProperty("plate")]
        public string Plate
        {
            get
            {
                var plateSplit = !string.IsNullOrEmpty(this._Plate) ? this._Plate.Split(' ') : null;
                if (plateSplit != null && plateSplit.Length > 1)
                {
                    this._Plate = $"{plateSplit[0]} {plateSplit[1]}";
                }

                return this._Plate;
            }
            set
            {
                _Plate = value;
            }
        }
        #endregion

        #region Org
        [JsonProperty("org_CODE")]
        public string OrgCode { get; set; }
        [JsonProperty("org_ABBR")]
        public string OrgAbbr { get; set; }
        [JsonProperty("tel")]
        public string Tel { get; set; }
        #endregion

        #region Occur
        [JsonProperty("occur_DT")]
        [JsonConverter(typeof(DateFormatConverter), "yyyyMMddHHmm", "en")]
        public DateTime OccurDate { get; set; }

        [JsonProperty("place")]
        public string OccurPlace { get; set; }

        [JsonProperty("atkilometer_HAPPEN")]
        public string OccurAtKilometer { get; set; }

        [JsonProperty("soi_HAPPEN")]
        public string OccurSoi { get; set; }

        [JsonProperty("road_HAPPEN")]
        public string OccurRoad { get; set; }

        [JsonProperty("subdistrict_HAPPEN")]
        public string OccurSubDistrict { get; set; }

        [JsonProperty("district_HAPPEN")]
        public string OccurDistrict { get; set; }
        #endregion

        #region Accuse
        [JsonProperty("accuse1_CODE")]
        public string Accuse1Code { get; set; }
        [JsonProperty("fine1")]
        public decimal? Fine1 { get; set; }

        [JsonProperty("accuse2_CODE")]
        public string Accuse2Code { get; set; }
        [JsonProperty("fine2")]
        public decimal? Fine2 { get; set; }

        [JsonProperty("accuse3_CODE")]
        public string Accuse3Code { get; set; }
        [JsonProperty("fine3")]
        public decimal? Fine3 { get; set; }

        [JsonProperty("accuse4_CODE")]
        public string Accuse4Code { get; set; }
        [JsonProperty("fine4")]
        public decimal? Fine4 { get; set; }

        [JsonProperty("accuse5_CODE")]
        public string Accuse5Code { get; set; }
        [JsonProperty("fine5")]
        public decimal? Fine5 { get; set; }
        #endregion

        #region QrCode
        public string QrCode
        {
            get
            {
                var policeTicketAccount = ConfigurationManager.AppSettings["PoliceTicketAccount"];
                return QRAndBarCodeHelper.GenerateQRCodeBase64(string.Format("|{0}\r{1}\r{2}\r{3}", policeTicketAccount, this.TicketID, this.CardID, this.FineAmount), false, ImageFormat.Png, 150, 150);
            }
        }
        #endregion
    }

    public enum TicketStatusEnum
    {
        [StringValue("ใบสั่งใหม่")]
        New = 1,
        [StringValue("ใบเตือน")]
        Warning = 2,
        [StringValue("ชำระแล้ว")]
        Paid = 3,
        [StringValue("ตักเตือน")]
        Caution = 4,
        [StringValue("ยกเลิก")]
        Cancel = 5,
        [StringValue("หมดอายุความ")]
        Expired = 6,
        [StringValue("ชำรุด/เสียหาย")]
        Damaged = 7
    }
}
