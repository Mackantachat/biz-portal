using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizPortal.ViewModels.V2;
using BizPortal.ViewModels.Apps;
using BizPortal.Utils.Extensions;
using EGA.WS;
using System.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using BizPortal.DAL.MongoDB;
using BizPortal.Utils.Helpers;
using System.Threading;
using BizPortal.ViewModels.SingleForm;

namespace BizPortal.AppsHook
{
    public class VATUtilityHook : SingleFormRendererAppHook
    {
        public override decimal? CalculateFee(List<ISectionData> sectionData)
        {
            return 0;
        }

        public override bool AllowFreeDocument { get; } = false;

        public override InvokeResult Invoke(AppsStage stage, ApplicationRequestViewModel model, AppHookInfo appHookInfo, ref ApplicationRequestEntity request)
        {
            InvokeResult result = new InvokeResult();
            result.DisabledSendingSystemEmail = false;

            try
            {
                bool match = false;

                if (stage == AppsStage.UserCreate && (model.Status == ApplicationStatusV2Enum.CHECK || (model.Status == ApplicationStatusV2Enum.FAILED && model.StatusOther == ApplicationStatusOtherValueConst.RESENDABLE)))
                {
                    request.NoDocument = true;
                    // เพิ่มจังหวัด อำเภอ ตำบล ลงในใบคำร้อง
                    request.ProvinceID = int.Parse(model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS"));
                    request.AmphurID = int.Parse(model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS"));
                    request.TumbolID = int.Parse(model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS"));

                    request.Province = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS_TEXT");
                    request.Amphur = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS_TEXT");
                    request.Tumbol = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS_TEXT");

                    #region juristicTitle
                    Dictionary<string, string> juristicTitles = new Dictionary<string, string>();
                    juristicTitles.Add("5211", "บริษัท");
                    juristicTitles.Add("5212", "บริษัทเงินทุน");
                    juristicTitles.Add("5213", "บริษัทหลักทรัพย์");
                    juristicTitles.Add("5214", "บริษัทเงินทุนหลักทรัพย์");
                    juristicTitles.Add("5216", "บริษัทจัดหางาน");
                    juristicTitles.Add("5217", "บรรษัท");
                    juristicTitles.Add("5218", "บริษัทขนส่งระหว่างประเทศ");
                    juristicTitles.Add("5222", "ห้างหุ้นส่วนจำกัด");
                    juristicTitles.Add("5224", "ห้างหุ้นส่วนสามัญนิติบุคคล");
                    juristicTitles.Add("5231", "กิจการร่วมค้า");
                    juristicTitles.Add("5232", "มูลนิธิ");
                    juristicTitles.Add("5233", "สมาคม");
                    juristicTitles.Add("5234", "สำนักงานผู้แทน");
                    juristicTitles.Add("5236", "สำนักงานผู้แทนภูมิภาค");
                    juristicTitles.Add("5237", "สำนักงานภูมิภาค");
                    juristicTitles.Add("5238", "พรรค");
                    juristicTitles.Add("5240", "ธนาคาร");
                    juristicTitles.Add("5241", "หอการค้า");
                    juristicTitles.Add("5242", "สมาคมหอการค้า");
                    juristicTitles.Add("6110", "คลีนิค");
                    juristicTitles.Add("6120", "ภัตตาคาร");
                    juristicTitles.Add("6140", "โรงงาน");
                    juristicTitles.Add("6150", "โรงพยาบาล");
                    juristicTitles.Add("6160", "โรงพยาบาลสัตว์");
                    juristicTitles.Add("6170", "โรงพิมพ์");
                    juristicTitles.Add("6180", "โรงไฟฟ้า");
                    juristicTitles.Add("6190", "โรงภาพยนตร์");
                    juristicTitles.Add("6200", "โรงรับจำนำ");
                    juristicTitles.Add("6210", "โรงแรม");
                    juristicTitles.Add("6220", "โรงเลื่อย");
                    juristicTitles.Add("6230", "โรงเลื่อยจักร");
                    juristicTitles.Add("6240", "โรงสี");
                    juristicTitles.Add("6260", "สถานพยาบาล");
                    juristicTitles.Add("6270", "สำนักงาน");
                    juristicTitles.Add("6280", "ห้างขายทอง");
                    juristicTitles.Add("6290", "ห้างขายยา");
                    juristicTitles.Add("6310", "ห้องอาหาร");
                    juristicTitles.Add("7115", "กองทุนรวม");
                    juristicTitles.Add("7120", "กองทุนสำรองเลี้ยงชีพ");
                    juristicTitles.Add("7180", "มหาวิทยาลัย");
                    juristicTitles.Add("7190", "โรงเรียน");
                    juristicTitles.Add("7200", "วิทยาลัย");
                    juristicTitles.Add("7210", "ศูนย์");
                    juristicTitles.Add("7220", "สถาบัน");
                    juristicTitles.Add("7230", "สมาพันธ์");
                    juristicTitles.Add("7251", "ชุมนุมสหกรณ์");
                    juristicTitles.Add("7280", "องค์การบริหารส่วนตำบล");
                    juristicTitles.Add("5243", "สมาคมการค้า");

                    //juristicTitles.Add("5211", "บริษัท");
                    //juristicTitles.Add("5213", "บริษัทหลักทรัพย์");
                    //juristicTitles.Add("5214", "บริษัทเงินทุนหลักทรัพย์");
                    //juristicTitles.Add("5217", "บรรษัท");
                    //juristicTitles.Add("5218", "บริษัทขนส่งระหว่างประเทศ");
                    //juristicTitles.Add("5222", "ห้างหุ้นส่วนจำกัด");
                    //juristicTitles.Add("5224", "ห้างหุ้นส่วนสามัญนิติบุคคล");
                    //juristicTitles.Add("5231", "กิจการร่วมค้า");
                    //juristicTitles.Add("5232", "มูลนิธิ");
                    //juristicTitles.Add("5233", "สมาคม");
                    //juristicTitles.Add("5234", "สำนักงานผู้แทน");
                    //juristicTitles.Add("5236", "สำนักงานผู้แทนภูมิภาค");
                    //juristicTitles.Add("5237", "สำนักงานภูมิภาค");
                    //juristicTitles.Add("5238", "พรรค");
                    //juristicTitles.Add("5240", "ธนาคาร");
                    //juristicTitles.Add("5241", "หอการค้า");
                    //juristicTitles.Add("5242", "สมาคมหอการค้า");
                    //juristicTitles.Add("6110", "คลีนิค");
                    //juristicTitles.Add("6120", "ภัตตาคาร");
                    //juristicTitles.Add("6140", "โรงงาน");
                    //juristicTitles.Add("6150", "โรงพยาบาล");
                    //juristicTitles.Add("6160", "โรงพยาบาลสัตว์");
                    //juristicTitles.Add("6170", "โรงพิมพ์");
                    //juristicTitles.Add("6180", "โรงไฟฟ้า");
                    //juristicTitles.Add("6190", "โรงภาพยนตร์");
                    //juristicTitles.Add("6200", "โรงรับจำนำ");
                    //juristicTitles.Add("6210", "โรงแรม");
                    //juristicTitles.Add("6220", "โรงเลื่อย");
                    //juristicTitles.Add("6230", "โรงเลื่อยจักร");
                    //juristicTitles.Add("6240", "โรงสี");
                    //juristicTitles.Add("6260", "สถานพยาบาล");
                    //juristicTitles.Add("6270", "สำนักงาน");
                    //juristicTitles.Add("6280", "ห้างขายทอง");
                    //juristicTitles.Add("6290", "ห้างขายยา");
                    //juristicTitles.Add("6310", "ห้องอาหาร");
                    //juristicTitles.Add("7115", "กองทุนรวม");
                    //juristicTitles.Add("7120", "กองทุนสำรองเลี้ยงชีพ");
                    //juristicTitles.Add("7180", "มหาวิทยาลัย");
                    //juristicTitles.Add("7190", "โรงเรียน");
                    //juristicTitles.Add("7200", "วิทยาลัย");
                    //juristicTitles.Add("7210", "ศูนย์");
                    //juristicTitles.Add("7220", "สถาบัน");
                    //juristicTitles.Add("7230", "สมาพันธ์");
                    //juristicTitles.Add("7251", "ชุมนุมสหกรณ์");
                    //juristicTitles.Add("7280", "องค์การบริหารส่วนตำบล");
                    //juristicTitles.Add("5243", "สมาคมการค้า");
                    #endregion

                    var profile = IdentityHelper.GetJuristicProfile(model.IdentityID);

                    JuristicProfile juristic = profile.ToObject<JuristicProfile>();

                    CultureInfo curtureInfo = Thread.CurrentThread.CurrentCulture;
                    CultureInfo cultureInfoEN = new CultureInfo("en-US");

                    string regdateStr = "";
                    string accountingDateStr = "";
                    string buswatdatStr = "";
                    string forrecdatStr = "";
                    string busfirdatStr = "";
                    string busincdatStr = "";

                    DateTime regDate = DateTime.MinValue;
                    if (DateTime.TryParseExact(juristic.RegisterDate, "yyyy-MM-dd+HH:mm", null, DateTimeStyles.None, out regDate))
                    {
                        regdateStr = regDate.ToString("yyyy-MM-dd");
                    }
                    DateTime accountingDate = DateTime.MinValue;
                    if (DateTime.TryParseExact(model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_ACCOUNTING_DATE"), "dd/MM", null, DateTimeStyles.None, out accountingDate))
                    {
                        accountingDateStr = accountingDate.ToString("MMdd");
                    }

                    buswatdatStr = DateTime.Now.ToString("yyyy-MM-dd", cultureInfoEN);

                    if (model.Data.TryGetData("VAT_INFORMATION").ThenGetBooleanData("VAT_VAT_CONDITION"))
                    {
                        if (model.Data.TryGetData("VAT_INFORMATION").ThenGetBooleanData("VAT_REGIS_VAT"))
                        {
                            busfirdatStr = DateTime.Now.ToString("yyyy-MM-dd", cultureInfoEN);
                        }
                        else
                        {
                            DateTime busincdat = DateTime.MinValue;
                            if (DateTime.TryParseExact(model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_REGIS_VAT_FALSE_DATE"), "dd/MM/yyyy", null, DateTimeStyles.None, out busincdat))
                            {
                                DateTime dt = Convert.ToDateTime(busincdat, curtureInfo);
                                busincdatStr = dt.ToString("yyyy-MM-dd", cultureInfoEN);
                            }
                        }
                    }
                    else
                    {
                        DateTime forrecdat = DateTime.MinValue;
                        if (DateTime.TryParseExact(model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_REGIS_VAT_CHECK_DATE"), "dd/MM/yyyy", null, DateTimeStyles.None, out forrecdat))
                        {
                            forrecdatStr = forrecdat.ToString("yyyy-MM-dd", cultureInfoEN);
                        }
                    }

                    string[] answers = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringArrayData("VAT_VAT001_CHECK_CHECKBOXES");
                    VATUtilityRequest post = new VATUtilityRequest()
                    {
                        TIN = new Tin()
                        {
                            CORPINFO = new CorpInfo()
                            {
                                REGISTERNUMBER = juristic.JuristicID,
                                REGISTERNAME = juristic.JuristicName_TH,
                                REGISTERDATE = regdateStr,
                                ACCOUNTINGDATE = accountingDateStr,
                            },
                            CURRENTADDRESSINFORMATION = new CurrentAddressInformation()
                            {
                                HOUSEIDNUMBER = string.Empty,
                                BUILDINGNAME = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_BUILDING_JURISTIC_HQ_ADDRESS"),
                                ROOMNUMBER = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_HQ_ADDRESS").DefaultString("-"),
                                FLOORNUMBER = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_FLOOR_JURISTIC_HQ_ADDRESS").DefaultString("-"),
                                VILLAGENAME = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_HQ_ADDRESS"),//.NullIfEmpty(),
                                HOUSENUMBER = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_JURISTIC_HQ_ADDRESS"),
                                MOONUMBER = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_MOO_JURISTIC_HQ_ADDRESS"),//.NullIfEmpty(),
                                SOINAME = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS"),
                                //YAEK = !string.IsNullOrEmpty(model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_SOI_JURISTIC_HQ_ADDRESS")) ? model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_YAK_JURISTIC_HQ_ADDRESS") : string.Empty,
                                STREETNAME = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_ROAD_JURISTIC_HQ_ADDRESS"),
                                PROVINCECODE = string.Format("{0}0000", model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS")),
                                AMPURCODE = string.Format("{0}{1}00", model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS"), model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS")),
                                THAMBOLCODE = string.Format("{0}{1}{2}", model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_HQ_ADDRESS"), model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_HQ_ADDRESS"), model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_HQ_ADDRESS")),
                                POSTCODE = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_HQ_ADDRESS"),
                                TELNUMBER = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_HQ_ADDRESS"),//.NullIfEmpty(),
                            },
                            COMMITTEE = new List<Committee>(),
                        },

                        VAT = new Vat()
                        {
                            PP01INFO = new Pp01Info()
                            {
                                TIN = juristic.JuristicID,
                                BRANUM = model.Data.TryGetData("BRANCH_ADDRESS_INFORMATION").ThenGetIntData("BRANCH_ADDRESS_INFORMATION_TOTAL").ToString(),
                                BUSWATDAT = buswatdatStr.NullIfEmpty(),
                                FORRECDAT = forrecdatStr.NullIfEmpty(),
                                BUSFIRDAT = busfirdatStr.NullIfEmpty(),
                                BUSINCDAT = busincdatStr.NullIfEmpty(),
                                BUSCPTAMO = juristic.RegisterCapital.ToString("0.00"),
                                ESTMONINCAMO = model.Data.TryGetData("VAT_INFORMATION").ThenGetDoubleData("VAT_MONTHLY_INCOME").ToString("0.00"),
                                EMAIL = model.Data.TryGetData("JURISTIC_ADDRESS_INFORMATION").ThenGetStringData("JURISTIC_HQ_EMAIL"),

                                AGREE1 = "1",
                                AGREE2 = "1",
                                AGREE3 = "1",
                                AGREE4 = "1",
                                AGREE5 = "1",
                            },
                            PP01CINFO = new Pp01cInfo()
                            {
                                BUSTYPCOD1 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_BUSTYPCOD_1"),
                                BUSTYPCOD2 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_BUSTYPCOD_2").NullIfEmpty(),
                                BUSTYPCOD3 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_BUSTYPCOD_3").NullIfEmpty(),
                                BUSTYPCOD4 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_BUSTYPCOD_4").NullIfEmpty(),
                                BUSTYPCOD5 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_BUSTYPCOD_5").NullIfEmpty(),
                                BUSTYPCOD6 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_BUSTYPCOD_6").NullIfEmpty(),

                                GOOTYPDES1 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_GOOTYPDES_1"),
                                GOOTYPDES2 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_GOOTYPDES_2").NullIfEmpty(),
                                GOOTYPDES3 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_GOOTYPDES_3").NullIfEmpty(),
                                GOOTYPDES4 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_GOOTYPDES_4").NullIfEmpty(),
                                GOOTYPDES5 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_GOOTYPDES_5").NullIfEmpty(),
                                GOOTYPDES6 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_GOOTYPDES_6").NullIfEmpty(),
                            },
                            PP011INFO = new Pp011Info()
                            {
                                BUS1 = answers.Any(o => o == "21A") ? "1" : "0",
                                BUS2 = answers.Any(o => o == "21B") ? "1" : "0",
                                BUS3 = answers.Any(o => o == "21C") ? "1" : "0",
                                BUS4 = answers.Any(o => o == "21D") ? "1" : "0",
                                BUS5 = answers.Any(o => o == "21E") ? "1" : "0",
                                BUS6 = answers.Any(o => o == "21F") ? "1" : "0",
                                BUS7 = answers.Any(o => o == "22") ? "1" : "0",
                                BUS8 = answers.Any(o => o == "23") ? "1" : "0",
                                BUS9 = answers.Any(o => o == "24") ? "1" : "0",
                                BUS10 = model.Data.TryGetData("VAT_INFORMATION").ThenGetStringData("VAT_VAT001_25_CHECK") == "25" ? "1" : "0",
                                PP011STA = model.Data.TryGetData("VAT_INFORMATION").ThenGetBooleanData("VAT_VAT_CONDITION") ? "N" : "Y",
                            },

                            PP01BINFO = new List<Branch>(),
                        },
                    };

                    var hqTITCODE = string.Empty;
                    if (!string.IsNullOrEmpty(juristic.JuristicType))
                    {
                        if (juristic.JuristicType == "บริษัทจำกัด")
                        {
                            hqTITCODE = "5211";
                        }
                        else
                        {
                            hqTITCODE = juristicTitles.FirstOrDefault(x => x.Value == juristic.JuristicType).Key;
                        }
                    }

                    //HQ Branch
                    var hq = new Branch();
                    var hqData = model.Data.TryGetData("BRANCH0_ADDRESS_INFORMATION");
                    if (hqData != null)
                    {
                        hq = new Branch()
                        {
                            BRANO = "0",
                            TITCOD = string.IsNullOrEmpty(hqTITCODE) ? "0000" : hqTITCODE,
                            BRANAM = hqData.ThenGetStringData("JURISTIC_BRANCH0_NAME_0"),
                            BLDGNAM = hqData.ThenGetStringData("ADDRESS_BUILDING_JURISTIC_BRANCH0_ADDRESS_0"),
                            FLOORNO = hqData.ThenGetStringData("ADDRESS_FLOOR_JURISTIC_BRANCH0_ADDRESS_0").DefaultString("-"),
                            ROOMNO = hqData.ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_BRANCH0_ADDRESS_0").DefaultString("-"),
                            ADDNO = hqData.ThenGetStringData("ADDRESS_JURISTIC_BRANCH0_ADDRESS_0"),
                            MOONO = hqData.ThenGetStringData("ADDRESS_MOO_JURISTIC_BRANCH0_ADDRESS_0").DefaultString("-"),
                            VILLAGE = hqData.ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_BRANCH0_ADDRESS_0"),
                            SOINAM = hqData.ThenGetStringData("ADDRESS_SOI_JURISTIC_BRANCH0_ADDRESS_0"),
                            //YAEK = !string.IsNullOrEmpty(hqData.ThenGetStringData("ADDRESS_SOI_JURISTIC_BRANCH0_ADDRESS_0")) ? hqData.ThenGetStringData("ADDRESS_YAK_JURISTIC_BRANCH0_ADDRESS_0") : string.Empty,
                            THNNAM = hqData.ThenGetStringData("ADDRESS_ROAD_JURISTIC_BRANCH0_ADDRESS_0").DefaultString("-"),
                            PROVCOD = string.Format("{0}0000", hqData.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_BRANCH0_ADDRESS_0")),
                            AMPCOD = string.Format("{0}{1}00", hqData.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_BRANCH0_ADDRESS_0"), hqData.ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_BRANCH0_ADDRESS_0")),
                            TAMCOD = string.Format("{0}{1}{2}", hqData.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_BRANCH0_ADDRESS_0"), hqData.ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_BRANCH0_ADDRESS_0"), hqData.ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_BRANCH0_ADDRESS_0")),
                            POSCOD = hqData.ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_BRANCH0_ADDRESS_0"),
                            TELNO = hqData.ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_BRANCH0_ADDRESS_0")
                        };
                    }
                    hq.AdjustStringLength();
                    match = match || hq.AdjustRegEx(RegExPattern.VAT_REGEX);
                    post.VAT.PP01BINFO.Add(hq);

                    var branches = model.Data.TryGetData("BRANCH_ADDRESS_INFORMATION");
                    if (branches != null)
                    {
                        int count = branches.ThenGetIntData("BRANCH_ADDRESS_INFORMATION_TOTAL");
                        for (int i = 0; i < count; i++)
                        {
                            string branchNo = branches.ThenGetStringData("ARR_IDX_" + i);
                            var branchTITCOD = branches.ThenGetStringData("AJAX_DROPDOWN_JURISTIC_BRANCH_TITLE_" + i);
                            var br = new Branch()
                            {
                                BRANO = int.Parse(branchNo).ToString(),
                                TITCOD = string.IsNullOrEmpty(branchTITCOD) ? "0000" : branchTITCOD,
                                BRANAM = branches.ThenGetStringData("JURISTIC_BRANCH_NAME_" + i),
                                BLDGNAM = branches.ThenGetStringData("ADDRESS_BUILDING_JURISTIC_BRANCH_ADDRESS_" + i),
                                FLOORNO = branches.ThenGetStringData("ADDRESS_FLOOR_JURISTIC_BRANCH_ADDRESS_" + i).DefaultString("-"),
                                ROOMNO = branches.ThenGetStringData("ADDRESS_ROOMNO_JURISTIC_BRANCH_ADDRESS_" + i).DefaultString("-"),
                                ADDNO = branches.ThenGetStringData("ADDRESS_JURISTIC_BRANCH_ADDRESS_" + i),
                                MOONO = branches.ThenGetStringData("ADDRESS_MOO_JURISTIC_BRANCH_ADDRESS_" + i).DefaultString("-"),
                                VILLAGE = branches.ThenGetStringData("ADDRESS_VILLAGE_JURISTIC_BRANCH_ADDRESS_" + i),
                                SOINAM = branches.ThenGetStringData("ADDRESS_SOI_JURISTIC_BRANCH_ADDRESS_" + i),
                                //YAEK = !string.IsNullOrEmpty(branches.ThenGetStringData("ADDRESS_SOI_JURISTIC_BRANCH_ADDRESS_" + i)) ? branches.ThenGetStringData("ADDRESS_YAK_JURISTIC_BRANCH_ADDRESS_" + i) : string.Empty,
                                THNNAM = branches.ThenGetStringData("ADDRESS_ROAD_JURISTIC_BRANCH_ADDRESS_" + i).DefaultString("-"),
                                PROVCOD = string.Format("{0}0000", branches.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_BRANCH_ADDRESS_" + i)),
                                AMPCOD = string.Format("{0}{1}00", branches.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_BRANCH_ADDRESS_" + i), branches.ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_BRANCH_ADDRESS_" + i)),
                                TAMCOD = string.Format("{0}{1}{2}", branches.ThenGetStringData("ADDRESS_PROVINCE_JURISTIC_BRANCH_ADDRESS_" + i), branches.ThenGetStringData("ADDRESS_AMPHUR_JURISTIC_BRANCH_ADDRESS_" + i), branches.ThenGetStringData("ADDRESS_TUMBOL_JURISTIC_BRANCH_ADDRESS_" + i)),
                                POSCOD = branches.ThenGetStringData("ADDRESS_POSTCODE_JURISTIC_BRANCH_ADDRESS_" + i),
                                TELNO = branches.ThenGetStringData("ADDRESS_TELEPHONE_JURISTIC_BRANCH_ADDRESS_" + i)
                            };
                            br.AdjustStringLength();
                            match = match || br.AdjustRegEx(RegExPattern.VAT_REGEX);
                            post.VAT.PP01BINFO.Add(br);
                        }
                    }

                    var committees = model.Data.TryGetData("COMMITTEE_INFORMATION");
                    if (committees != null)
                    {
                        int count = committees.ThenGetIntData("COMMITTEE_INFORMATION_TOTAL");
                        DateTime dateOfBirth = DateTime.MinValue;
                        string dateOfBirthStr = "";

                        for (int i = 0; i < count; i++)
                        {

                            if (DateTime.TryParseExact(committees.ThenGetStringData("JURISTIC_COMMITTEE_BIRTHDATE_" + i), "dd/MM/yyyy", null, DateTimeStyles.None, out dateOfBirth))
                            {
                                DateTime dt = Convert.ToDateTime(dateOfBirth, curtureInfo);
                                dateOfBirthStr = dt.ToString("yyyy-MM-dd", cultureInfoEN);
                            }
                            string title = committees.ThenGetStringData("JURISTIC_COMMITTEE_TITLE_" + i);

                            var comm = new Committee()
                            {
                                IDNUMBER = committees.ThenGetStringData("JURISTIC_COMMITTEE_CITIZEN_ID_" + i),
                                TITLECODE = DB.DbdTitles.Where(o => o.TitleName == title).Select(o => o.TitleAbbreviation).First(),
                                NAME = committees.ThenGetStringData("JURISTIC_COMMITTEE_NAME_" + i),
                                SURNAME = committees.ThenGetStringData("JURISTIC_COMMITTEE_LASTNAME_" + i),
                                NATIONALITY = "76400",//Value = "76400", Text = "ไทย" 
                                DATEOFBIRTH = dateOfBirthStr
                            };
                            comm.AdjustStringLength();
                            match = match || comm.AdjustRegEx(RegExPattern.VAT_REGEX);
                            post.TIN.COMMITTEE.Add(comm);
                        }
                    }

                    string regisUrl = ConfigurationManager.AppSettings["VAT_REGIS_WS_URL"];

                    post.TIN.CORPINFO.AdjustStringLength();
                    post.TIN.CURRENTADDRESSINFORMATION.AdjustStringLength();
                    post.VAT.PP01INFO.AdjustStringLength();
                    post.VAT.PP01CINFO.AdjustStringLength();
                    post.VAT.PP011INFO.AdjustStringLength();

                    match = match || post.TIN.CORPINFO.AdjustRegEx(RegExPattern.VAT_REGEX);
                    match = match || post.TIN.CURRENTADDRESSINFORMATION.AdjustRegEx(RegExPattern.VAT_REGEX);
                    match = match || post.VAT.PP01INFO.AdjustRegEx(RegExPattern.VAT_REGEX);
                    match = match || post.VAT.PP01CINFO.AdjustRegEx(RegExPattern.VAT_REGEX);
                    match = match || post.VAT.PP011INFO.AdjustRegEx(RegExPattern.VAT_REGEX);

                    var jsonPost = JsonConvert.SerializeObject(post);

                    if (match)
                    {
                        var error = "ไม่สามารถส่งคำร้องได้ เนื่องจากพบอักขระพิเศษ ^ หรือ | กรุณาตรวจสอบคำร้องและยื่นคำร้องใหม่";
                        result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, error, jsonPost);
                        result.Message = error;
                        throw new Exception(error);
                    }

                    Api.OnCheckingApplicationError += (ex) =>
                    {
                        result.Exception = ex;
                        var egaEx = ex as EGAWSAPIException;
                        if (egaEx != null)
                        {
                            var message = string.Format("{0}: {1}", (int)egaEx.HttpStatusCode, egaEx.ResponseData["Message"].ToString());
                            result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, message, egaEx.ResponseData.ToString(), jsonPost);
                            result.Message = egaEx.ResponseData["Message"].ToString();
                        }
                        else
                        {
                            result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, ex.Message, ex.StackTrace, jsonPost);
                            result.Message = ex.Message;
                        }
                    };

                    Dictionary<string, string> respData = new Dictionary<string, string>();
                    var vatResult = Api.Call(regisUrl, EGA.WS.HttpVerb.POST, null, jsonPost, EGA.WS.ContentType.ApplicationJson);
                    if (vatResult != null)
                    {
                        if (vatResult.HasValues && vatResult["responseStatus"].ToString() == "OK")
                        {
                            respData.Add("VAT_REGIS_DATE", DateTime.Now.ToString());

                            if (request.Data.ContainsKey("VAT_RESPONSE_DATA"))
                            {
                                request.Data.Remove("VAT_RESPONSE_DATA");
                            }

                            request.Data.Add("VAT_RESPONSE_DATA", new ApplicationRequestDataGroupEntity()
                            {
                                GroupName = "VAT_RESPONSE_DATA",
                                Data = respData
                            });

                            result.Data = GenerateAppsHookData(AppsHookResult.Created, stage, vatResult["responseStatus"].ToString(), vatResult.ToString(), jsonPost);
                            result.Success = true;
                        }
                        else
                        {
                            string error = "Unable to request to " + regisUrl + ".";
                            result.Data = GenerateAppsHookData(AppsHookResult.Failed, stage, error, vatResult.ToString(), jsonPost, true);
                            throw new Exception(error);
                        }
                    }
                }
                else
                {
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Exception = ex;
            }

            return result;
        }
    }
}
