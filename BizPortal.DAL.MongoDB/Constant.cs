using BizPortal.Utils.Extensions;
using BizPortal.Utils.Helpers;
using System.Collections.Generic;

namespace BizPortal.DAL.MongoDB
{
    public sealed class SingleFormAppFileConst
    {
        public const string JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE = "JURISTIC_AUTHORIZATION_AUTHORIZEE_ID_CARD_SINGLE";
    }

    public class RegexPatternConst
    {
        public const string JS_EMAIL_VALIDATOR_PATTERN = @"/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/";
    }

    public class RequestorInformationValueConst
    {
        public const string REQUEST_TYPE_BOARD = "REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD";
        public const string REQUEST_TYPE_NOMINEE = "REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE";
        public const string REQUEST_TYPE_OWNER = "REQUESTOR_INFORMATION__REQUEST_TYPE_OWNER";
    }
    public class RequestorInformationTextConst
    {
        public const string REQUEST_TYPE_BOARD = "ขออนุญาตเองโดยเจ้าของกิจการ";
        public const string REQUEST_TYPE_NOMINEE = "มอบอำนาจให้ผู้อื่นดำเนินการแทน";
        public const string REQUEST_TYPE_OWNER = "ขออนุญาตเองโดยเจ้าของกิจการ";

    }

    public class COMMITTEERequestorInformationValueConst
    {
        public const string COMMITTEE_REQUEST_TYPE_BOARD = "COMMITTEE_REQUESTOR_INFORMATION__REQUEST_TYPE_BOARD";
        public const string COMMITTEE_REQUEST_TYPE_NOMINEE = "COMMITTEE_REQUESTOR_INFORMATION__REQUEST_TYPE_NOMINEE";

    }
    public class COMMITTEERequestorInformationTextConst
    {
        public const string COMMITTEE_REQUEST_TYPE_BOARD = "กรรมการฯ นิติบุคคลเป็นผู้ขออนุญาตเอง";
        public const string COMMITTEE_REQUEST_TYPE_NOMINEE = "กรรมการฯ นิติบุคคลเป็นผู้มอบอำนาจให้ผู้อื่นดำเนินการแทน";


    }

    public class SellAlcoholAppType
    {
        public const string CAT1 = "SELL_ALCOHOL_CATEGORY1";
        public const string CAT2 = "SELL_ALCOHOL_CATEGORY2";
        public const string CAT3 = "SELL_ALCOHOL_CATEGORY3";
        public const string CAT4 = "SELL_ALCOHOL_CATEGORY4";
        public const string CAT5 = "SELL_ALCOHOL_CATEGORY5";
        public const string CAT6 = "SELL_ALCOHOL_CATEGORY6";
        public const string CAT7 = "SELL_ALCOHOL_CATEGORY7";
    }

    public class StoreInformationBuildingTypeOptionValueConst
    {
        public const string OWNED = "INFORMATION_STORE_BUILDING_TYPE_OPTION__OWNED";
        public const string RENT = "INFORMATION_STORE_BUILDING_TYPE_OPTION__RENT";
        public const string RENT_FREE = "INFORMATION_STORE_BUILDING_TYPE_OPTION__RENT_FREE";
        public const string OTHER = "INFORMATION_STORE_BUILDING_TYPE_OPTION__OTHER";
    }
    public class OrganicStoreInformationBuildingTypeOptionValueConst
    {
        public const string OWNED = "INFORMATION_STORE_BUILDING_TYPE_OPTION__OWNED";
        public const string RENT = "INFORMATION_STORE_BUILDING_TYPE_OPTION__RENT";
        public const string RENT_FREE = "INFORMATION_STORE_BUILDING_TYPE_OPTION__RENT_FREE";
        public const string OTHER = "INFORMATION_STORE_BUILDING_TYPE_OPTION__OTHER";
    }
    public class OrganicStoreInformationBuildingTypeOptionTextConst
    {
        public const string OWNED = "นิติบุคคลเป็นเจ้าของสถานที่ตั้งแหล่งผลิต/ที่ตั้งแปลงเอง";
        public const string OWNED_CITIZEN = "เป็นเจ้าของสถานที่ตั้งแหล่งผลิต/ที่ตั้งแปลงเอง";
        public const string RENT = "เช่าสถานที่ตั้งแหล่งผลิต/ที่ตั้งแปลงของผู้อื่น";
        public const string RENT_FREE = "ใช้สถานที่ตั้งแหล่งผลิต/ที่ตั้งแปลงของผู้อื่นแบบไม่เสียค่าใช้จ่าย";
        public const string OTHER = "ทรัพย์สินของภาครัฐ/ทรัพย์สินพระมหากษัตริย์";
    }

    public class StoreInformationBuildingTypeOptionTextConst
    {
        public const string OWNED = "นิติบุคคลเป็นเจ้าของอาคารเอง";
        public const string OWNED_CITIZEN = "เป็นเจ้าของอาคารเอง";
        public const string RENT = "เช่าอาคารสถานที่ผู้อื่น";
        public const string RENT_FREE = "ใช้อาคารสถานที่ผู้อื่นแบบไม่เสียค่าใช้จ่าย";
        public const string OTHER = "ทรัพย์สินของภาครัฐ/ทรัพย์สินพระมหากษัตริย์";
    }

    public class StoreInformationBuildingRentingOwnerTypeOptionValueConst
    {
        public const string JURISTIC = "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION__JURISTIC";
        public const string CITIZEN = "INFORMATION_STORE_BUILDING_RENTING_OWNER_TYPE_OPTION__CITIZEN";
        public const string Government = "INFORMATION_STORE_BUILDING_TYPE_OPTION__GOVERNMENT";
        public const string Royal = "INFORMATION_STORE_BUILDING_TYPE_OPTION__ROYAL";
    }
    public class StoreInformationBuildingRentingOwnerTypeOptionTextConst
    {
        public const string JURISTIC = "นิติบุคคล";
        public const string CITIZEN = "บุคคลธรรมดา";
        public const string Government = "ภาครัฐ";
        public const string Royal = "ทรัพย์สินส่วนพระมหากษัตริย์";
    }

    public class HoteltypebuildingTypeOptionValueConst
    {
        public const string ONE = "APP_HOTEL_BUILDING_TYPE_ONE";
        public const string TWO = "APP_HOTEL_BUILDING_TYPE_TWO";
        public const string THREE = "APP_HOTEL_BUILDING_TYPE_THREE";
        public const string FOUR = "APP_HOTEL_BUILDING_TYPE_FOUR";
    }
    public class HoteltypebuildingTypeOptionTextConst
    {
        public const string ONE = "อาคารสูง";
        public const string TWO = "อาคารขนาดใหญ่่";
        public const string THREE = "อาคารขนาดใหญ่่พิเศษ";
        public const string FOUR = "อาคารอื่นๆ";
    }

    public class HoteltypebuildingDOCTypeOptionValueConst
    {
        public const string DOCONE = "APP_HOTEL_DOC_TYPE_OPTION_ONE";
        public const string DOCTWO = "APP_HOTEL_DOC_TYPE_OPTION_TWO";
        public const string DOCTHREE = "APP_HOTEL_DOC_TYPE_OPTION_THREE";
        public const string DOCFOUR = "APP_HOTEL_DOC_TYPE_OPTION_FOUR";
    }
    public class HoteltypebuildingDOCTypeOptionTextConst
    {
        public const string DOCONE = "โฉนดที่ดิน";
        public const string DOCTWO = "นส.3ก/น.ส.3";
        public const string DOCTHREE = "ส.ค.1";
        public const string DOCFOUR = "อื่นๆ";
    }

    public class BuildingTypeOptionValueConst
    {
        public const string BUILDING = "APP_BUILDING_BUILDING_SECTION_TYPE_BUILDING";
        public const string MODIFY = "APP_BUILDING_BUILDING_SECTION_TYPE_MODIFY";
        public const string DISMANTLE = "APP_BUILDING_BUILDING_SECTION_TYPE_DISMANTLE";
    }
    public class BuildingTypeOptionTextConst
    {
        public const string BUILDING = "ก่อสร้างอาคาร";
        public const string MODIFY = "ดัดแปลงอาคาร";
        public const string DISMANTLE = "รื้อถอนอาคาร";
    }
    public class BuildingTypeRequestOptionValueConst
    {
        public const string BUILDING = "APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_BUILDING";
        public const string MODIFY = "APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_MODIFY";
        public const string DISMANTLE = "APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_DISMANTLE";
    }
    public class BuildingTypeRequestOptionTextConst
    {
        public const string BUILDING = "ก่อสร้างอาคาร";
        public const string MODIFY = "ดัดแปลงอาคาร";
        public const string DISMANTLE = "รื้อถอนอาคาร";
    }
    public class R6BuildingTypeOptionValueConst
    {
        public const string BUILDING = "APP_BUILDING_BUILDING_SECTION_TYPE_BUILDING";
        public const string MODIFY = "APP_BUILDING_BUILDING_SECTION_TYPE_MODIFY";
        public const string MOVE = "APP_BUILDING_BUILDING_SECTION_TYPE_MOVE";
    }
    public class R6BuildingTypeOptionTextConst
    {
        public const string BUILDING = "ก่อสร้างอาคาร";
        public const string MODIFY = "ดัดแปลงอาคาร";
        public const string MOVE = "เคลื่อนย้ายอาคาร";
    }
    public class R6BuildingTypeRequestOptionValueConst
    {
        public const string BUILDING = "APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_BUILDING";
        public const string MODIFY = "APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_MODIFY";
        public const string MOVE = "APP_BUILDING_BUILDING_SECTION_BUILDING_REQUEST_MOVE";
    }
    public class R6BuildingTypeRequestOptionTextConst
    {
        public const string BUILDING = "ก่อสร้างอาคาร";
        public const string MODIFY = "ดัดแปลงอาคาร";
        public const string MOVE = "เคลื่อนย้ายอาคาร";
    }

    public class TAXTypeOptionValueConst
    {
        public const string TAXONE = "1";
        public const string TAXTWO = "2";
        public const string TAXTHREE = "3";
    }
    public class TAXTypeOptionTextConst
    {
        public const string TAXONE = "ประเภท 1 มีอักษรไทยล้วนมีอักษรไทย";
        public const string TAXTWO = "ประเภท 2 ปนกับอักษรต่างประเทศหรือเครื่องหมาย";
        public const string TAXTHREE = "ประเภท 3 ป้ายที่ไม่มีอักษรไทย";
    }

    public class CommercialRegistrationTypeOptionValueConst
    {
        public const string CITIZEN = "APP_COMMERCIAL_REGISTRATION_TYPE_CITIZEN";
        public const string PARTNERSHIP = "APP_COMMERCIAL_REGISTRATION_TYPE_PARTNERSHIP";
    }
    public class CommercialtypeRegisterTypeOptionTextConst
    {
        public const string CITIZEN = "กรณีผู้ขอจดทะเบียนเป็นบุคคลธรรมดา";
        public const string PARTNERSHIP = "กรณีผู้ขอจดทะเบียนเป็นห้างหุ้นส่วนสามัญ คณะบุคคล และกิจการร่วมค้า";
    }

    public class AnimalMedTypeOptionValueConst
    {
        public const string MEDONE = "APP_ANIMAL_MED_TYPE_ONE";
        public const string MEDTWO = "APP_ANIMAL_MED_TYPE_TWO";

    }
    public class AnimalMedTypeOptionTextConst
    {
        public const string MEDONE = "ประเภทไม่มีที่พักสัตว์ป่วยไว้ค้างคืน";
        public const string MEDTWO = "ประเภทมีที่พักสัตว์ป่วยไว้ค้างคืน";

    }

    public class AnimalLinceseStatusTypeOptionValueConst
    {
        public const string STAONE = "APP_ANIMAL_LICENSE_STATUS_JOB_ONE";
        public const string STATWO = "APP_ANIMAL_LICENSE_STATUS_JOB_TWO";
        public const string STATHREE = "APP_ANIMAL_LICENSE_STATUS_JOB_THREE";

    }
    public class AnimalLicenseStatusTypeOptionTextConst
    {
        public const string STAONE = "ไม่เป็นผู้ดำเนินการสถานพยาบาลสัตว์";
        public const string STATWO = "เป็นผู้ดำเนินการสถานพยาบาลสัตว์ประเภท";
        public const string STATHREE = "เป็นผู้ประกอบวิชาชีพการสัตวแพทย์หรือ ปฏิบัติหน้าที่ในสถานพยาบาลสัตว์ หรือส่วนราชการ หรือหน่วยงานอื่น";
    }

    public class AnimalBuildTypeOptionValueConst
    {
        public const string CLINICONE = "APP_ANIMAL_BUILD_TYPE_ONE";
        public const string CLINICTWO = "APP_ANIMAL_BUILD_TYPE_ONE_TWO";
        public const string CLINICTHREE = "APP_ANIMAL_BUILD_TYPE_ONE_THREE";
        public const string CLINICFOUR = "APP_ANIMAL_BUILD_TYPE_ONE_FOUR";
        public const string CLINICFIVE = "APP_ANIMAL_BUILD_TYPE_ONE_FIVE";
    }
    public class AnimalBuildTypeOptionTextConst
    {
        public const string CLINICONE = "คลินิกทั่วไป";
        public const string CLINICTWO = "คลินิกเฉพาะทาง";
        public const string CLINICTHREE = "โรงพยาบาลสัตว์ทั่วไป";
        public const string CLINICFOUR = "โรงพยาบาลสัตว์เฉพาะทาง";
        public const string CLINICFIVE = "อื่นๆ";
    }


    public class AnimalBuildLocationTypeOptionValueConst
    {
        public const string LOCATONE = "APP_ANIMAL_LOCAT_TYPE_ONE";
        public const string LOCATTWO = "APP_ANIMAL_LOCAT_TYPE_TWO";
        public const string LOCATTHREE = "APP_ANIMAL_LOCAT_TYPE_THREE";
        public const string LOCATFOUR = "APP_ANIMAL_LOCAT_TYPE_FOUR";
        public const string LOCATFIVE = "APP_ANIMAL_LOCAT_TYPE_FIVE";
        public const string LOCATSIX = "APP_ANIMAL_LOCAT_TYPE_SIX";
        public const string LOCATSEVEN = "APP_ANIMAL_LOCAT_TYPE_SEVEN";
        public const string LOCATEIGHT = "APP_ANIMAL_LOCAT_TYPE_EIGHT";
        public const string LOCATNINE = "APP_ANIMAL_LOCAT_TYPE_NINE";
    }
    public class AnimalBuildLocationTypeOptionTextConst
    {
        public const string LOCATONE = "เป็นอาคารสถานพยาบาลสัตว์โดยเฉพาะ";
        public const string LOCATTWO = "เป็นอาคารอยู่อาศัย";
        public const string LOCATTHREE = "เป็นห้องแถว";
        public const string LOCATFOUR = "เป็นตึกแถว";
        public const string LOCATFIVE = "เป็นบ้านแถว";
        public const string LOCATSIX = "เป็นบ้านแฝด";
        public const string LOCATSEVEN = "เป็นอาคารพาณิชย์";
        public const string LOCATEIGHT = "ตั้งอยู่ในศูนย์การค้า";
        public const string LOCATNINE = "อื่นๆ";
    }

    public class HealthstoreTypeOptionValueConst
    {
        public const string SPAONE = "APP_HEALTH_BUILD_TYPE_ONE";
        public const string SPATWO = "APP_HEALTH_BUILD_TYPE_ONE_TWO";
        public const string SPATHREE = "APP_HEALTH_BUILD_TYPE_ONE_THREE";
        public const string SPAFOUR = "APP_HEALTH_BUILD_TYPE_ONE_FOUR";
    }
    public class HealthstoreTypeOptionTextConst
    {
        public const string SPAONE = "บ้าน";
        public const string SPATWO = "อาคาร";
        public const string SPATHREE = "ศูนย์การค้า";
        public const string SPAFOUR = "อื่นๆ";
    }
    public class StoreInformationBuildingInGovernmentAreaValueConst
    {
        public const string YES_IN_GOVERNMENT_AREA = "INFORMATION_STORE_BUILDING_IN_GOVERNMENT_AREA_OPTION__YES";
        public const string NO_NOT_IN_GOVERNMENT_AREA = "INFORMATION_STORE_BUILDING_IN_GOVERNMENT_AREA_OPTION__NO";
    }
    public class StoreInformationBuildingInGovernmentAreaTextConst
    {
        public const string YES_IN_GOVERNMENT_AREA = "ใช่ ร้านของฉันตั้งอยู่ในพื้นที่ภาครัฐ/ราชพัสดุ";
        public const string NO_NOT_IN_GOVERNMENT_AREA = "ไม่ใช่ ร้านของฉันไม่ได้ตั้งอยู่ในพื้นที่ภาครัฐ/ราชพัสดุ";
    }

    public class SellFoodInformationAppTypeOptionValueConst
    {
        public const string LICENSE = "SELL_FOOD_INFORMATION__APP_TYPE_OPTION__LICENSE";
        public const string CERTIFICATE = "SELL_FOOD_INFORMATION__APP_TYPE_OPTION__CERTIFICATE";
    }
    public class SellFoodInformationAppTypeOptionTextConst
    {
        public const string LICENSE = "ใบอนุญาต";
        public const string CERTIFICATE = "หนังสือรับรองการแจ้ง";
    }

    public class SellFoodInformationLicenseTypeOptionValueConst
    {
        public const string LICENSE = "SELL_FOOD_INFORMATION__LICENSE_TYPE_OPTION__LICENSE";
        public const string CERTIFICATE = "SELL_FOOD_INFORMATION__LICENSE_TYPE_OPTION__CERTIFICATE";
    }
    public class SellFoodInformationLicenseTypeOptionTextConst
    {
        public const string LICENSE = "ใบอนุญาต";
        public const string CERTIFICATE = "หนังสือรับรองการแจ้ง";
    }

    public class SellFoodInformationBusinessTypeValueConst
    {
        public const string SELL = "APP_SELL_FOOD_LOCATION_TYPE_OPTION__SELL";
        public const string STOCK = "APP_SELL_FOOD_LOCATION_TYPE_OPTION__STOCK";
    }
    public class SellFoodInformationBusinessTypeTextConst
    {
        public const string SELL = "สถานที่จำหน่ายอาหาร";
        public const string STOCK = "สถานที่สะสมอาหาร";
    }

    public class SellFoodInformationPurposeOptionValueConst
    {
        public const string SELL = "APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__SELL";
        public const string STOCK = "APP_SELL_FOOD_LOCATION_PURPOSE_TYPE_OPTION__STOCK";
    }
    public class SellFoodInformationPurposeOptionTextConst
    {
        public const string SELL = "สถานที่จำหน่ายอาหาร";
        public const string STOCK = "สถานที่สะสมอาหาร";
    }

    #region COMMERCIAL

    public class CommercialRegistrationValueConst
    {
        public const string YES_REGISTRATION = "INFORMATION_STORE_COMMERCE_REGISTRATION_CITIZEN_OPTION__YES";
        public const string NO_NOT_REGISTRATION = "INFORMATION_STORE_COMMERCE_REGISTRATION_CITIZEN_OPTION__NO";
    }
    public class CommercialRegistrationTextConst
    {
        public const string YES_REGISTRATION = "ใช่ ฉันได้จดทะเบียนพาณิชย์";
        public const string NO_NOT_REGISTRATION = "ไม่ใช่ ฉันไม่ได้จดทะเบียนพาณิชย์";
    }

    #endregion

    public class BASE_URL_API
    {
        public const string HSS_SPA = "http://164.115.9.184:88/spa";
    }

    public class AppSystemNameTextConst
    {
        public const string APP_SELL_FOOD_LT_200 = "ขอหนังสือรับรองการแจ้งจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตร.ม.)";
        public const string APP_SELL_FOOD_LT_200_RENEW = "ขอต่ออายุหนังสือรับรองการแจ้งจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตร.ม.)";
        public const string APP_SELL_FOOD_LT_200_EDIT = "ขอแก้ไขหนังสือรับรองการแจ้งจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตร.ม.)";
        public const string APP_SELL_FOOD_LT_200_CANCEL = "ขอยกเลิกหนังสือรับรองการแจ้งจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (ไม่เกิน 200 ตร.ม.)";
        public const string APP_SELL_FOOD_GE_200 = "ขอใบอนุญาตจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (เกิน 200 ตร.ม.)";
        public const string APP_SELL_FOOD_GE_200_RENEW = "ขอต่ออายุใบอนุญาตจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (เกิน 200 ตร.ม.)";
        public const string APP_SELL_FOOD_GE_200_EDIT = "ขอแก้ไขใบอนุญาตจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (เกิน 200 ตร.ม.)";
        public const string APP_SELL_FOOD_GE_200_CANCEL = "ขอยกเลิกใบอนุญาตจัดตั้งสถานที่ จำหน่ายหรือสะสมอาหาร (เกิน 200 ตร.ม.)";
        public const string APP_SELL_ALCOHOL = "ขอใบอนุญาตขายสุรา";
        public const string APP_SELL_ALCOHOL_RENEW = "ขอต่ออายุใบอนุญาตขายสุรา";
        public const string APP_BROTHEL = "ขอใบอนุญาตตั้งสถานบริการในท้องที่กรุงเทพมหานคร";
        public const string APP_VCD = "ขอใบอนุญาตประกอบกิจการร้านวีดิทัศน์ (คาราโอเกะ)";

        #region [ สปา ]
        public const string APP_HEALTH = "ขอใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ";
        public const string APP_HEALTH_RENEW = "ขอต่ออายุใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
        public const string APP_HEALTH_EDIT = "ขอแก้ไขใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
        public const string APP_HEALTH_CANCEL = "ขอยกเลิกใบอนุญาตประกอบกิจการสถานประกอบการเพื่อสุขภาพ : ประเภทกิจการ สปา";
        #endregion

        public const string APP_COSMETIC = "ขอจดแจ้งรายละเอียดการผลิตเพื่อขายหรือนำเข้าเพื่อขายเครื่องสำอางควบคุม";


        #region อภ.1 อาหาร/เครื่องดื่ม

        public const string APP_SELL_TOBACCO = "ขอใบอนุญาตขายยาสูบ";
        public const string APP_SELL_TOBACCO_RENEW = "ขอใบอนุญาตขายยาสูบ";
        public const string APP_DIRECT_SELL = "ขอจดทะเบียนการประกอบธุรกิจขายตรง";
        public const string APP_DIRECT_SELL_EDIT = "คำขอแก้ไขเปลี่ยนแปลงเอกสารและหลักฐานที่ได้รับจดทะเบียนประกอบธุรกิจขายตรง";
        public const string APP_DIRECT_SELL_CANCEL = "ยกเลิกประกอบธุรกิจขายตรง";
        public const string APP_DIRECT_MARKETING = "ขอจดทะเบียนการประกอบธุรกิจตลาดแบบตรง";
        public const string APP_DIRECT_MARKETING_EDIT = "คำขอแก้ไขเปลี่ยนแปลงเอกสารและหลักฐานที่ได้รับจดทะเบียนประกอบธุรกิจตลาดแบบตรง";
        public const string APP_DIRECT_MARKETING_CANCEL = "ยกเลิกประกอบธุรกิจตลาดแบบตรง";
        public const string APP_SELL_ANIMAL_FOOD = "ขอใบอนุญาตขายอาหารสัตว์ควบคุมเฉพาะ";
        public const string APP_SELL_CARCASS = "ขอใบอนุญาตทำการค้าหรือหากำไรในลักษณะคนกลางซึ่งซากสัตว์";
        public const string APP_SELL_ANIMAL = "ขอใบอนุญาตทำการค้าหรือหากำไรในลักษณะคนกลางซึ่งสัตว์";
        public const string APP_STORE_GAS_LE1000KG = "ขอใบรับแจ้ง/แจ้งแก้ไขเปลี่ยนแปลง/แจ้งโอน การประกอบกิจการสถานที่เก็บรักษาก๊าซปิโตรเลียมเหลว ประเภทสถานที่ใช้ ลักษณะที่สอง";
        public const string APP_STORE_GAS_GT1000KG = "ขอใบอนุญาตประกอบกิจการ สถานที่เก็บรักษาก๊าซปิโตรเลียมเหลวประเภทสถานที่ใช้ ลักษณะที่สาม กรณีใช้ถังเก็บและจ่ายก๊าซปิโตรเลียมเหลว แบบทรงกระบอก";

        public const string APP_SELL_CARD = "ขออนุญาตขายไพ่";
        public const string APP_SELL_CARD_RENEW = "ต่ออายุอนุญาตขายไพ่";
        public const string APP_BUILDING = "ใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีพิเศษ";
        public const string APP_BUILDING_RENEW = "ต่ออายุใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีพิเศษ";
        public const string APP_BUILDING_EDIT = "แก้ไขใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีพิเศษ";
        public const string APP_BUILDING_CANCEL = "ยกเลิกการขออนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีพิเศษ";
        public const string APP_BUILDING_DPW = "ขอใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักการโยธา";
        public const string APP_BUILDING_DPW_RENEW = "ขอต่ออายุใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักการโยธา";
        public const string APP_BUILDING_DPW_EDIT = "ขอแก้ไขใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักการโยธา";
        public const string APP_BUILDING_DPW_CANCEL = "ขอยกเลิกใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักการโยธา";
        public const string APP_BUILDING_DISTRICT = "ขอใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักงานเขต";
        public const string APP_BUILDING_DISTRICT_RENEW = "ขอต่ออายุใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักงานเขต";
        public const string APP_BUILDING_DISTRICT_EDIT = "ขอแก้ไขใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักงานเขต";
        public const string APP_BUILDING_DISTRICT_CANCEL = "ขอยกเลิกใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอาคารขนาดใหญ่ ยื่นที่สำนักงานเขต";
        public const string APP_BUILDING_OTHER = "ใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอื่นๆ";
        public const string APP_BUILDING_OTHER_RENEW = "ต่ออายุใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอื่นๆ";
        public const string APP_BUILDING_OTHER_EDIT = "แก้ไขใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอื่นๆ";
        public const string APP_BUILDING_OTHER_CANCEL = "ยกเลิกใบอนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร: กรณีอื่นๆ";
        public const string APP_HOTEL = "ขอใบอนุญาตประกอบธุรกิจโรงแรม";
        public const string APP_HOTEL_RENEW = "ต่ออายุใบอนุญาตประกอบธุรกิจโรงแรม";
        public const string APP_HOTEL_EDIT = "แก้ไขใบอนุญาตประกอบธุรกิจโรงแรม";
        public const string APP_HOTEL_CANCEL = "ยกเลิกใบอนุญาตประกอบธุรกิจโรงแรม";
        public const string APP_COMMERCIAL = "จดทะเบียนพาณิชย์";
        public const string APP_COMMERCIAL_EDIT = "แก้ไขจดทะเบียนพาณิชย์";
        public const string APP_COMMERCIAL_CANCEL = "ยกเลิกจดทะเบียนพาณิชย์";
        public const string APP_TAX = "ยื่นชำระภาษีป้าย";
        public const string APP_TAX_RENEW = "ต่ออายุการยื่นชำระภาษีป้าย";
        public const string APP_TAX_EDIT = "แก้ไขการยื่นชำระภาษีป้าย";
        public const string APP_TAX_CANCEL = "ยกเลิกภาษีป้าย";
        public const string APP_RADIO = "ใบอนุญาตให้ค้า/ให้ค้าเพื่อการซ่อมแซมซึ่งเครื่องวิทยุคมนาคม";
        public const string APP_RADIO_RENEW = "ขอต่ออายุใบอนุญาตให้ค้า/ให้ค้าเพื่อการซ่อมแซมซึ่งเครื่องวิทยุคมนาคม";
        public const string APP_RADIO_EDIT = "แก้ไขใบอนุญาตให้ค้า/ให้ค้าเพื่อการซ่อมแซมซึ่งเครื่องวิทยุคมนาคม";
        public const string APP_RADIO_CANCEL = "ยกเลิกใบอนุญาตให้ค้า/ให้ค้าเพื่อการซ่อมแซมซึ่งเครื่องวิทยุคมนาคม";
        public const string APP_ANIMAL_MED = "ขอใบอนุญาตให้ตั้งสถานพยาบาลสัตว์";
        public const string APP_ANIMAL_MED_RENEW = "ขอต่ออายุใบอนุญาตให้ตั้งสถานพยาบาลสัตว์";
        public const string APP_ANIMAL_MED_EDIT = "แก้ไขคำขอต่ออายุใบจัดตั้งสถานพยาบาลสัตว์";
        public const string APP_ANIMAL_MED_CANCEL = "ยกเลิกคำขอต่ออายุใบจัดตั้งสถานพยาบาลสัตว์";
        public const string APP_ANIMAL_LICENSE = "ขอใบอนุญาตดำเนินการสถานพยาบาลสัตว์";
        public const string APP_ANIMAL_LICENSE_RENEW = "ขอต่ออายุใบอนุญาตดำเนินการสถานพยาบาลสัตว์";
        public const string APP_ANIMAL_LICENSE_EDIT = "แก้ไขคำขอต่ออายุใบอนุญาตดำเนินการสถานพยาบาลสัตว์";
        public const string APP_ANIMAL_LICENSE_CANCEL = "ยกเลิกคำขอต่ออายุใบอนุญาตดำเนินการสถานพยาบาลสัตว์";
        public const string APP_ANIMAL_BUILD = "ขอหนังสืออนุมัติแผนการจัดตั้งสถานพยาบาลสัตว์";
        public const string APP_ANIMAL_BUILD_RENEW = "ขอต่ออายุหนังสืออนุมัติแผนการจัดตั้งสถานพยาบาลสัตว์";
        public const string APP_MOVIE = "ขอใบอนุญาตประกอบกิจการให้เช่า แลกเปลี่ยน หรือจำหน่ายภาพยนตร์ และวีดิทัศน์";
        public const string APP_MOVIE_RENEW = "ขอต่ออายุใบอนุญาตประกอบกิจการให้เช่า แลกเปลี่ยน หรือจำหน่ายภาพยนตร์ และวีดิทัศน์";
        public const string APP_MOVIE_EDIT = "ขอแก้ไขใบอนุญาตประกอบกิจการให้เช่า แลกเปลี่ยน หรือจำหน่ายภาพยนตร์ และวีดิทัศน์";
        public const string APP_MOVIE_CANCEL = "ขอยกเลิกใบอนุญาตประกอบกิจการให้เช่า แลกเปลี่ยน หรือจำหน่ายภาพยนตร์ และวีดิทัศน์";
        public const string APP_KARAOKE = "ขอใบอนุญาตประกอบกิจการร้านวีดิทัศน์ (คาราโอเกะ)";
        public const string APP_KARAOKE_RENEW = "ขอต่ออายุใบอนุญาตประกอบกิจการร้านวีดิทัศน์ (คาราโอเกะ)";
        public const string APP_KARAOKE_EDIT = "ขอแก้ไขใบอนุญาตประกอบกิจการร้านวีดิทัศน์ (คาราโอเกะ)";
        public const string APP_KARAOKE_CANCEL = "ขอยกเลิกใบอนุญาตประกอบกิจการร้านวีดิทัศน์ (คาราโอเกะ)";


        //Frontis  //FRT
        public const string APP_FRT_NEW_001 = "APP_FRT_NEW_001";  //ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การผลิต บรรจุยาสีฟัน แชมพู ผ้าเย็น กระดาษเย็น เครื่องสำอาง รวมทั้งสบู่ที่ใช้กับร่างกาย
        public const string APP_FRT_RENEW_001 = "APP_FRT_RENEW_001";  //ขอต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การผลิต บรรจุยาสีฟัน แชมพู ผ้าเย็น กระดาษเย็น เครื่องสำอาง รวมทั้งสบู่ที่ใช้กับร่างกาย
        public const string APP_FRT_EDIT_001 = "APP_FRT_EDIT_001";  //ขอแก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การผลิต บรรจุยาสีฟัน แชมพู ผ้าเย็น กระดาษเย็น เครื่องสำอาง รวมทั้งสบู่ที่ใช้กับร่างกาย
        public const string APP_FRT_CANCEL_001 = "APP_FRT_CANCEL_001";  //ขอยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การผลิต บรรจุยาสีฟัน แชมพู ผ้าเย็น กระดาษเย็น เครื่องสำอาง รวมทั้งสบู่ที่ใช้กับร่างกาย

        public const string APP_FACTORY_TYPE2 = "APP_FACTORY_TYPE2"; // ขอหนังสือแจ้งผลการรับฟังความคิดเห็นของประชาชนในการพิจารณาเกี่ยวกับโรงงานจำพวกที่ 2

        public const string APP_FACTORY_CLASS_2_NEW = "APP_FACTORY_CLASS_2_NEW"; //ใบรับแจ้งการประกอบกิจการโรงงานจำพวกที่ 2
        public const string APP_FACTORY_CLASS_2_EDIT = "APP_FACTORY_CLASS_2_EDIT"; //ขอแก้ไขการแจ้งการประกอบกิจการโรงงานจำพวกที่ 2
        public const string APP_FACTORY_CLASS_2_CANCEL = "APP_FACTORY_CLASS_2_CANCEL"; //การแจ้งยกเลิกการประกอบกิจการโรงงานจำพวกที่ 2

        public const string APP_ACCOUNTING_SERVICE_RENEW = "APP_ACCOUNTING_SERVICE_RENEW"; // ขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี ต่ออายุ
        public const string APP_ACCOUNTING_SERVICE_RENEW_TYPE_2 = "APP_ACCOUNTING_SERVICE_RENEW_TYPE_2";
        public const string APP_ACCOUNTING_SERVICE_EDIT = "APP_ACCOUNTING_SERVICE_EDIT"; // ขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี แก้ไข
        public const string APP_ACCOUNTING_SERVICE_EDIT_TYPE_2 = "APP_ACCOUNTING_SERVICE_EDIT_TYPE_2";
        public const string APP_ACCOUNTING_SERVICE_CANCEL = "APP_ACCOUNTING_SERVICE_CANCEL"; // ขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี ยกเลิก

        public const string APP_ORGANIC_PLANT_PRODUCTION_NEW = "APP_ORGANIC_PLANT_PRODUCTION_NEW";
        public const string APP_ORGANIC_PLANT_PRODUCTION_RENEW = "APP_ORGANIC_PLANT_PRODUCTION_RENEW"; //ขอต่ออายุใบรับรองแหล่งผลิตพืชอินทรีย์
        public const string APP_ORGANIC_PLANT_PRODUCTION_EDIT = "APP_ORGANIC_PLANT_PRODUCTION_EDIT"; //ขอแก้ไขใบรับรองแหล่งผลิตพืชอินทรีย์
        public const string APP_ORGANIC_PLANT_PRODUCTION_CANCEL = "APP_ORGANIC_PLANT_PRODUCTION_CANCEL"; //ขอยกเลิกใบรับรองแหล่งผลิตพืชอินทรีย์

        public const string APP_SPA_FEE_PER_YEAR = "APP_SPA_FEE_PER_YEAR"; // งานบริการชำระค่าธรรมเนียมการประกอบกิจการสถานประกอบการเพื่อสุขภาพรายปี (สปา)
        public const string APP_CLINIC_NOT_ONE_NIGHT_STAND = "APP_CLINIC_NOT_ONE_NIGHT_STAND"; // งานบริการชำระค่าธรรมเนียมการประกอบกิจการสถานพยาบาล (แบบไม่รับผู้ป่วยค้างคืน)
        public const string APP_CLINIC_OVER_NIGHT = "APP_CLINIC_OVER_NIGHT"; // การชำระค่าธรรมเนียมการประกอบกิจการสถานพยาบาล(แบบรับผู้ป่วยค้างคืน)

        public const string APP_CLINIC_BUSINESS = "APP_CLINIC_BUSINESS"; // ขออนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)
        public const string APP_CLINIC_OPERATION = "APP_CLINIC_OPERATION"; // ขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)

        public const string APP_HOSPITAL_BUSINESS = "APP_HOSPITAL_BUSINESS"; // งานบริการขออนุญาตให้ประกอบกิจการสถานพยาบาล (โรงพยาบาล)
        public const string APP_HOSPITAL_OPERATION = "APP_HOSPITAL_OPERATION"; // งานบริการขอรับใบอนุญาตให้ดำเนินการสถานพยาบาล (โรงพยาบาล)

        public const string APP_CLINIC_BUSINESS_RENEW = "APP_CLINIC_BUSINESS_RENEW"; // ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล (คลินิก)
        public const string APP_CLINIC_OPERATION_RENEW = "APP_CLINIC_OPERATION_RENEW"; // ขอต่ออายุใบอนุญาตให้ดำเนินการสถานพยาบาล (คลินิก)

        public const string APP_HOSPITAL_BUSINESS_RENEW = "APP_HOSPITAL_BUSINESS_RENEW"; // ใบอนุญาตให้ประกอบกิจการสถานพยาบาล (ประเภทที่รับผู้ป่วยค้างคืน)
        public const string APP_HOSPITAL_OPERATION_RENEW = "APP_HOSPITAL_OPERATION_RENEW"; // ใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่รับผู้ป่วยค้างคืน)

        public const string APP_HOSPITAL_BUSINESS_EDIT = "APP_HOSPITAL_BUSINESS_EDIT"; // ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)
        public const string APP_HOSPITAL_OPERATION_EDIT = "APP_HOSPITAL_OPERATION_EDIT"; // ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาลประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน
        public const string APP_HOSPITAL_OPERATION_EDIT_B = "APP_HOSPITAL_OPERATION_EDIT_B"; // 

        public const string APP_CLINIC_BUSINESS_EDIT = "APP_CLINIC_BUSINESS_EDIT"; // ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาล (ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน)
        public const string APP_CLINIC_OPERATION_EDIT = "APP_CLINIC_OPERATION_EDIT"; // ขอเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)
        public const string APP_CLINIC_OPERATION_EDIT_B = "APP_CLINIC_OPERATION_EDIT_B"; // 

        public static string[] APP_CLINIC_EDIT_SPLIT 
        {
            get 
            {
                return new string[]
                {
                    APP_CLINIC_BUSINESS_EDIT,
                    APP_CLINIC_OPERATION_EDIT,
                    APP_CLINIC_OPERATION_EDIT_B
                };
            }
        }

        public static string[] APP_HOSPITAL_EDIT_SPLIT
        {
            get 
            {
                return new string[]
                {
                    APP_HOSPITAL_BUSINESS_EDIT,
                    APP_HOSPITAL_OPERATION_EDIT,
                    APP_HOSPITAL_OPERATION_EDIT_B
                };
            }
        }

        public static string[] APP_HOSPITAL_SPLIT_RENEW 
        {
            get 
            {
                return new string[] 
                {
                    APP_HOSPITAL_BUSINESS_RENEW,
                    APP_HOSPITAL_OPERATION_RENEW
                };
            }
        }

        public static string[] APP_CLINIC_SPLIT_RENEW
        {
            get 
            {
                return new string[]
                {
                    APP_CLINIC_BUSINESS_RENEW,
                    APP_CLINIC_OPERATION_RENEW
                };
            }
        }
            

        //public const string APP_

        #endregion

        #region SECTION GROUP

        #region APP DANGER

        #region GROUP APP SYSTEM NAME

        #region APP DANGER ALL ( ALL )

        public static string[] APP_DANGER_ALL
        {
            get
            {
                return APP_DANGER_FOOD_ALL.ConcatItems(
                    APP_DANGER_SERIVCE_ALL).ConcatItems(
                    APP_DANGER_CONSTRUCTION_ALL).ConcatItems(
                    APP_DANGER_OTHER_ALL).ConcatItems(
                    APP_DANGER_AUTOMOTIVE_ALL).ConcatItems(
                    APP_DANGER_PETROLEUM_ALL);
            }
        }

        #region APP IN APP DANGER ALL ( ALL )

        public static string[] APP_DANGER_FOOD_ALL
        {
            get
            {
                return APP_Danger_Food.ConcatItems(
                    ALL_APP_DANGER_FOOD_RENEW).ConcatItems(
                    ALL_APP_DANGER_FOOD_EDIT).ConcatItems(
                    ALL_APP_DANGER_FOOD_CANCEL);
            }
        }

        public static string[] APP_DANGER_SERIVCE_ALL
        {
            get
            {
                return APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT.ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL).ConcatItems(
                    APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL).ConcatItems(
                    APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL);
            }
        }

        public static string[] APP_DANGER_CONSTRUCTION_ALL
        {
            get
            {
                return APP_BUSINESS_BAD_HEALTH_CONSTRUCTION.ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CANCEL);
            }
        }

        public static string[] APP_DANGER_OTHER_ALL
        {
            get
            {
                return APP_BUSINESS_BAD_HEALTH_OTHER.ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_OTHER_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_OTHER_CANCEL);
            }
        }
        
        public static string[] APP_DANGER_COSMATIC_ALL
        {
            get
            {
                return APP_DANGER_COSMATIC.ConcatItems(
                    ALL_APP_DANGER_COSMATIC_RENEW).ConcatItems(
                    ALL_APP_DANGER_COSMATIC_EDIT).ConcatItems(
                    ALL_APP_DANGER_COSMATIC_CANCEL);
            }
        }

        public static string[] APP_DANGER_AUTOMOTIVE_ALL
        {
            get
            {
                return APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE.ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CANCEL);
            }
        }

        public static string[] APP_DANGER_PETROLEUM_ALL
        {
            get
            {
                return APP_BUSINESS_BAD_HEALTH_PETROLEUM.ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_CANCEL);
            }
        }

        #endregion

        #endregion

        #region APP DANGER ALL ( RENEW )

        public static string[] APP_DANGER_RENEW_ALL
        {
            get
            {
                return ALL_APP_DANGER_FOOD_RENEW.ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW);
            }
        }

        #endregion

        #region APP DANGER ALL ( EDIT )

        public static string[] APP_DANGER_EDIT_ALL
        {
            get
            {
                return ALL_APP_DANGER_FOOD_EDIT.ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_OTHER_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_EDIT);
            }
        }

        #endregion

        #region APP DANGER ALL ( CANCEL )

        public static string[] APP_DANGER_CANCEL_ALL
        {
            get
            {
                return ALL_APP_DANGER_FOOD_CANCEL.ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_OTHER_CANCEL).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CANCEL).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CANCEL).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_CANCEL);
            }
        }

        #endregion

        public static string[] APP_DANGER_ALL_RENEW
        {
            get
            {
                return ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW.ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW);
            }
        }

        public static string[] ALL_APP_FORM_HAS_PROVINCE_ONLY
        {
            get
            {
                return ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_EDIT.ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_OTHER_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_EDIT).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CANCEL).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_CANCEL).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_OTHER_CANCEL).ConcatItems(
                    ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CANCEL).ConcatItems(
                    APP_RADIO_EDIT,
                    APP_RADIO_CANCEL,
                    APP_ANIMAL_MED_EDIT,
                    APP_ANIMAL_MED_CANCEL,
                    APP_ANIMAL_LICENSE_EDIT,
                    APP_ANIMAL_LICENSE_CANCEL,
                    APP_COMMERCIAL_EDIT,
                    APP_COMMERCIAL_CANCEL,
                    APP_BUILDING_EDIT,
                    APP_BUILDING_CANCEL,
                    APP_BUILDING_DPW_EDIT,
                    APP_BUILDING_DPW_CANCEL,
                    APP_BUILDING_DISTRICT_EDIT,
                    APP_BUILDING_DISTRICT_CANCEL,
                    APP_BUILDING_OTHER_EDIT,
                    APP_BUILDING_OTHER_CANCEL,
                    APP_HOTEL_EDIT,
                    APP_HOTEL_CANCEL,
                    APP_TAX_EDIT,
                    APP_TAX_CANCEL,
                    APP_ENERGY_PRODUCTION_NOT_PERMIT);
            }
        }

        #endregion

        #region APP SYSTEM NAME

        #region FOOD

        public const int APP_DANGER_FOOD_CNT = 26; //*** Full version is 26
        public const string APP_DANGER_FOOD_RENEW = "ขอต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม";
        public const string APP_DANGER_FOOD_EDIT = "ขอแก้ไขอายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม";
        public const string APP_DANGER_FOOD_CANCEL = "ขอยกเลิกอายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม";
        public const string APP_DANGER_PREFIX = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_DANGER_PREFIX_RENEW = "ขอต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_DANGER_PREFIX_EDIT = "ขอแก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_DANGER_PREFIX_CANCEL = "ขอยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";


        public static string[] APP_Danger_Food
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_FOOD_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_DANGER_PREFIX + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_ITEM_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_DANGER_FOOD_RENEW
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_FOOD_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_DANGER_PREFIX_RENEW + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_ITEM_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_DANGER_FOOD_EDIT
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_FOOD_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_DANGER_PREFIX_EDIT + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_ITEM_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_DANGER_FOOD_CANCEL
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_FOOD_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_DANGER_PREFIX_CANCEL + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_ITEM_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region SERVICE RESTAURANT

        public const int APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CNT = 1;
        //public const string APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_PREFIX = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_PREFIX_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_PREFIX_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_PREFIX_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";

        public static string[] APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_PREFIX + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_RESTAURANT_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_PREFIX_RENEW + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_RESTAURANT_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_PREFIX_EDIT + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_RESTAURANT_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_PREFIX_CANCEL + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_RESTAURANT_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region SERVICE HOTEL

        public const int APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CNT = 1;
        //public const string APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_PREFIX = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_PREFIX_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_PREFIX_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_PREFIX_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";

        public static string[] APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_PREFIX + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_HOTEL_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_PREFIX_RENEW + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_HOTEL_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_PREFIX_EDIT + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_HOTEL_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_PREFIX_CANCEL + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_HOTEL_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region SERVICE FITNESS

        public const int APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CNT = 1;
        //public const string APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: บริการ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_PREFIX = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_PREFIX_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_PREFIX_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_PREFIX_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";

        public static string[] APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_PREFIX + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_FITNESS_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_PREFIX_RENEW + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_FITNESS_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_PREFIX_EDIT + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_FITNESS_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_PREFIX_CANCEL + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_FITNESS_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region OTHER

        public const int APP_BUSINESS_BAD_HEALTH_OTHER_CNT = 1;
        //public const string APP_BUSINESS_BAD_HEALTH_OTHER = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อื่นๆ";
        public const string APP_BUSINESS_BAD_HEALTH_OTHER_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อื่นๆ";
        public const string APP_BUSINESS_BAD_HEALTH_OTHER_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อื่นๆ";
        public const string APP_BUSINESS_BAD_HEALTH_OTHER_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อื่นๆ";
        public const string APP_BUSINESS_BAD_HEALTH_OTHER_PREFIX = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_OTHER_PREFIX_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_OTHER_PREFIX_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_OTHER_PREFIX_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";

        public static string[] APP_BUSINESS_BAD_HEALTH_OTHER
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_OTHER_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_OTHER_PREFIX + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_OTHER_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_OTHER_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_OTHER_PREFIX_RENEW + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_OTHER_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_OTHER_EDIT
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_OTHER_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_OTHER_PREFIX_EDIT + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_OTHER_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_OTHER_CANCEL
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_OTHER_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_OTHER_PREFIX_CANCEL + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_OTHER_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region COSMATIC
        public const int APP_DANGER_COSMATIC_CNT = 1;
        //public const string APP_DANGER_COSMATIC_NEW = "APP_DANGER_COSMATIC_NEW";
        public const string APP_DANGER_COSMATIC_RENEW = "APP_DANGER_COSMATIC_RENEW";
        public const string APP_DANGER_COSMATIC_EDIT = "APP_DANGER_COSMATIC_EDIT";
        public const string APP_DANGER_COSMATIC_CANCEL = "APP_DANGER_COSMATIC_CANCEL";
        public const string APP_DANGER_COSMATIC_PREFIX = "APP_DANGER_COSMATIC_NEW: ";
        public const string APP_DANGER_COSMATIC_PREFIX_RENEW = "APP_DANGER_COSMATIC_RENEW: ";
        public const string APP_DANGER_COSMATIC_PREFIX_EDIT = "APP_DANGER_COSMATIC_EDIT: ";
        public const string APP_DANGER_COSMATIC_PREFIX_CANCEL = "APP_DANGER_COSMATIC_CANCEL: ";
        public static string[] APP_DANGER_COSMATIC
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_COSMATIC_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_DANGER_COSMATIC_PREFIX + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_OTHER_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_DANGER_COSMATIC_RENEW
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_COSMATIC_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_DANGER_COSMATIC_PREFIX_RENEW + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_OTHER_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_DANGER_COSMATIC_EDIT
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_COSMATIC_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_DANGER_COSMATIC_PREFIX_EDIT + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_OTHER_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_DANGER_COSMATIC_CANCEL
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_COSMATIC_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_DANGER_COSMATIC_CANCEL + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_OTHER_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }
        #endregion


        #region Construction

        public const int APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CNT = 1;
        public const string APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: หิน ดิน ทราย";
        public const string APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: หิน ดิน ทราย";
        public const string APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: หิน ดิน ทราย";
        public const string APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_PREFIX = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_PREFIX_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_PREFIX_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_PREFIX_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";

        public static string[] APP_BUSINESS_BAD_HEALTH_CONSTRUCTION
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_PREFIX + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_CONST_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_PREFIX_RENEW + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_CONST_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_EDIT
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_PREFIX_EDIT + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_CONST_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CANCEL
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_PREFIX_CANCEL + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_CONST_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region Automotive

        public const int APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CNT = 1;
        public const string APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การล้าง ขัดสี เคลือบสี หรืออัดฉีดยานยนต์";
        public const string APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การล้าง ขัดสี เคลือบสี หรืออัดฉีดยานยนต์";
        public const string APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: การล้าง ขัดสี เคลือบสี หรืออัดฉีดยานยนต์";
        public const string APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_PREFIX = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_PREFIX_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_PREFIX_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_PREFIX_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";

        public static string[] APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_PREFIX + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_AUTO_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_PREFIX_RENEW + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_AUTO_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_EDIT
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_PREFIX_EDIT + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_AUTO_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CANCEL
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_PREFIX_CANCEL + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_AUTO_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region Petroleum

        public const int APP_BUSINESS_BAD_HEALTH_PETROLEUM_CNT = 1;
        public const string APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ – ปิโตรเลียม สารเคมีต่างๆ";
        public const string APP_BUSINESS_BAD_HEALTH_PETROLEUM_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ – ปิโตรเลียม สารเคมีต่างๆ";
        public const string APP_BUSINESS_BAD_HEALTH_PETROLEUM_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ – ปิโตรเลียม สารเคมีต่างๆ";
        public const string APP_BUSINESS_BAD_HEALTH_PETROLEUM_PREFIX = "ใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_PETROLEUM_PREFIX_RENEW = "ต่ออายุใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_PETROLEUM_PREFIX_EDIT = "แก้ไขใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";
        public const string APP_BUSINESS_BAD_HEALTH_PETROLEUM_PREFIX_CANCEL = "ยกเลิกใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: ";

        public static string[] APP_BUSINESS_BAD_HEALTH_PETROLEUM
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_PETROLEUM_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_PETROLEUM_PREFIX + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_PETRO_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_PETROLEUM_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_PETROLEUM_PREFIX_RENEW + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_PETRO_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_EDIT
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_PETROLEUM_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_PETROLEUM_PREFIX_EDIT + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_PETRO_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_CANCEL
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_PETROLEUM_CNT + 1; i++)
                {
                    dangerFoods.Add(APP_BUSINESS_BAD_HEALTH_PETROLEUM_PREFIX_CANCEL + ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_PETRO_C" + i, "Apps_SmartQuiz"));
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

      
       
        #endregion

        #region PREFILL

        #region ซึ่งเป็นกิจการที่เป็นอันตรายต่อสุขภาพประเภท

        //public static string[] APP_Danger_Food_Prefill_Business_About
        //{
        //    get
        //    {
        //        List<string> dangerFoods = new List<string>();
        //        for (int i = 1; i < AppSystemNameTextConst.APP_DANGER_FOOD_CNT + 1; i++)
        //        {
        //            dangerFoods.Add(ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_ITEM_C" + i, "Apps_SmartQuiz"));
        //        }
        //        return dangerFoods.ToArray();
        //    }
        //}

        #endregion

        #region ซึ่งเป็นกิจการที่เป็นอันตรายต่อสุขภาพประเภท(หินดินทราย)

        //public static string[] APP_Business_Bad_Health_Construction_Prefill_Business_About
        //{
        //    get
        //    {
        //        List<string> dangerConsts = new List<string>();
        //        for (int i = 1; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CNT + 1; i++)
        //        {
        //            dangerConsts.Add(ResourceHelper.GetResourceWordWithDefault("QUESTION_RESTUARANT_DANGER_CONST_C" + i, "Apps_SmartQuiz"));
        //        }
        //        return dangerConsts.ToArray();
        //    }
        //}

        #endregion

        #region ซึ่งเป็นกิจการที่เป็นอันตรายต่อสุขภาพประเภท (อื่นๆ)

        //public static string[] APP_Business_Bad_Health_Other_Prefill_Business_About
        //{
        //    get
        //    {
        //        List<string> dangerOther= new List<string>();
        //        for (int i = 1; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_OTHER_CNT + 1; i++)
        //        {
        //            dangerOther.Add(ResourceHelper.GetResourceWordWithDefault("APP_BUSINESS_BAD_HEALTH_OTHER_C" + i, "Apps_SmartQuiz"));
        //        }
        //        return dangerOther.ToArray();
        //    }
        //}

        #endregion

        #region ลำดับที่

        //public static string[] APP_Danger_Food_Prefill_Rank
        //{
        //    get
        //    {
        //        List<string> dangerFoods = new List<string>();
        //        for (int i = 1; i < AppSystemNameTextConst.APP_DANGER_FOOD_CNT + 1; i++)
        //        {
        //            dangerFoods.Add("5.3." + i);
        //        }
        //        return dangerFoods.ToArray();
        //    }
        //}

        #endregion

        #region ลำดับที่ (หิน ดิน ทราย)

        //public static string[] APP_Business_Bad_Health_Construction_Prefill_Rank
        //{
        //    get
        //    {
        //        List<string> dangerConsts = new List<string>();
        //        for (int i = 1; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CNT + 1; i++)
        //        {
        //            dangerConsts.Add("5.3." + i);
        //        }
        //        return dangerConsts.ToArray();
        //    }
        //}

        #endregion

        #region ลำดับที่ (อื่นๆ)

        //public static string[] APP_Business_Bad_Health_Other_Prefill_Rank
        //{
        //    get
        //    {
        //        List<string> dangerOther = new List<string>();
        //        for (int i = 1; i < AppSystemNameTextConst.APP_BUSINESS_BAD_HEALTH_OTHER_CNT + 1; i++)
        //        {
        //            dangerOther.Add("5.3." + i);
        //        }
        //        return dangerOther.ToArray();
        //    }
        //}

        #endregion

        #endregion

        #region FORM SECTION GROUP DANGER FOOD

        public static string[] APP_Danger_Food_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_FOOD_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_DANGER_FOOD" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_DANGER_FOOD_RENEW_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_FOOD_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_DANGER_FOOD_RENEW_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_DANGER_FOOD_EDIT_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_FOOD_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_DANGER_FOOD_EDIT_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_DANGER_FOOD_CANCEL_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_FOOD_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_DANGER_FOOD_CANCEL_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion 

        #region FORM SECTION GROUP Construction

        public static string[] APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_CONSTRUCTION" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_RENEW_" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_EDIT_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_EDIT_" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CANCEL_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_CONSTRUCTION_CANCEL_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region FORM SECTION GROUP OTHER

        public static string[] APP_BUSINESS_BAD_HEALTH_OTHER_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_OTHER_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_OTHER" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_OTHER_RENEW_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_OTHER_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_OTHER_RENEW_" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_OTHER_EDIT_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_OTHER_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_EDIT_RENEW_" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_OTHER_CANCEL_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_OTHER_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_OTHER_CANCEL_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region FORM SECTION GROUP COSMATIC

        public static string[] APP_DANGER_COSMATIC_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_COSMATIC_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_DANGER_COSMATIC" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_DANGER_COSMATIC_RENEW_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_COSMATIC_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_DANGER_COSMATIC_RENEW_" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_DANGER_COSMATIC_EDIT_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_COSMATIC_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_DANGER_COSMATIC_EDIT_" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_DANGER_COSMATIC_CANCEL_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_DANGER_COSMATIC_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_DANGER_COSMATIC_CANCEL_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region FORM SECTION GROUP Petroleum

        public static string[] APP_BUSINESS_BAD_HEALTH_PETROLEUM_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_PETROLEUM_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_PETROLEUM" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_PETROLEUM_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_PETROLEUM_RENEW_" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_EDIT_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_PETROLEUM_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_PETROLEUM_EDIT_" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_PETROLEUM_CANCEL_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_PETROLEUM_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_PETROLEUM_CANCEL_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region FORM SECTION GROUP AUTOMOTIVE

        public static string[] APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_RENEW_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_EDIT_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_EDIT_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CANCEL_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_AUTOMOTIVE_CANCEL_" + i);
                }
                return dangerFoods.ToArray();
            }
        }
        #endregion

        #region FORM SECTION GROUP RESTAURANT

        public static string[] APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_RENEW_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_EDIT_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_RESTAURANT_CANCEL_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region FORM SECTION GROUP HOTEL

        public static string[] APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_RENEW_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_EDIT_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_HOTEL_CANCEL_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

        #region FORM SECTION GROUP FITNESS

        public static string[] APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_SystemNames
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_RENEW_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_EDIT_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        public static string[] ALL_APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL_SYSTEM_NAMES
        {
            get
            {
                List<string> dangerFoods = new List<string>();
                for (int i = 1; i < APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CNT + 1; i++)
                {
                    dangerFoods.Add("APP_BUSINESS_BAD_HEALTH_SERVICE_FITNESS_CANCEL_" + i);
                }
                return dangerFoods.ToArray();
            }
        }

        #endregion

       

        public static string[] App_Restaurant_Retail_All
        {
            get
            {
                return App_Retail_All.ConcatItems(
                    App_Restaurant_All).ConcatItems(
                    APP_VETERINARY_HOSPITAL);
            }
        }

        public static string[] App_Retail_All
        {
            get
            {
                return new string[]
                {
                    APP_SELL_ANIMAL_FOOD,
                    APP_SELL_CARCASS,
                    APP_SELL_ANIMAL,
                    APP_SELL_TOBACCO,
                    APP_DIRECT_SELL,
                    APP_DIRECT_SELL_EDIT,
                    APP_DIRECT_SELL_CANCEL,
                    APP_DIRECT_MARKETING,
                    APP_DIRECT_MARKETING_EDIT,
                    APP_DIRECT_MARKETING_CANCEL,
                    APP_SELL_CARD,
                    APP_BUILDING,
                    APP_BUILDING_RENEW,
                    APP_BUILDING_DPW,
                    APP_BUILDING_DPW_RENEW,
                    APP_BUILDING_DISTRICT,
                    APP_BUILDING_DISTRICT_RENEW,
                    APP_BUILDING_OTHER,
                    APP_BUILDING_OTHER_RENEW,
                    APP_HOTEL,
                    APP_HOTEL_RENEW,
                    APP_COMMERCIAL,
                    APP_TAX,
                    APP_TAX_RENEW,
                    APP_RADIO,
                    APP_RADIO_RENEW,
                    APP_RADIO_EDIT,
                    APP_RADIO_CANCEL,
                    APP_ANIMAL_MED,
                    APP_ANIMAL_MED_RENEW,
                    APP_ANIMAL_LICENSE,
                    APP_ANIMAL_LICENSE_RENEW,
                    APP_HEALTH,
                    APP_HEALTH_RENEW,
                    APP_MOVIE,
                    APP_KARAOKE,
                    APP_HOSPITAL,
                    APP_HOSPITAL_PERMISSION,
                    APP_HOSPITAL_PERMISSION_RENEW,
                    APP_HOSPITAL_PERMISSION_EDIT,
                    APP_HOSPITAL_PERMISSION_CANCEL,
                    APP_CLINIC,
                    APP_CLINIC_RENEW,
                    APP_CLINIC_EDIT,
                    APP_CLINIC_CANCEL,
                    APP_LAW_OFFICE,
                    APP_LAW_OFFICE_EDIT,
                    APP_LAW_OFFICE_CANCEL,
                    APP_SECURITIES_BUSINESS,
                    APP_SECURITIES_BUSINESS_EDIT,
                    APP_SECURITIES_BUSINESS_CANCEL,
                    APP_ENERGY_PRODUCTION,
                    APP_ENERGY_PRODUCTION_RENEW,
                    APP_ENERGY_PRODUCTION_EDIT,
                    APP_ENERGY_PRODUCTION_CANCEL,
                    APP_ELECTRONIC_COMMERCIAL,
                    APP_ELECTRONIC_COMMERCIAL_EDIT,
                    APP_ELECTRONIC_COMMERCIAL_CANCEL,
                    APP_ACCOUNTING_SERVICE,
                    APP_ACCOUNTING_SERVICE_RENEW,
                    APP_ACCOUNTING_SERVICE_RENEW_TYPE_2,
                    APP_ACCOUNTING_SERVICE_EDIT,
                    APP_ACCOUNTING_SERVICE_EDIT_TYPE_2,
                    APP_ACCOUNTING_SERVICE_CANCEL,
                    APP_TOURISM_BUSINESS,
                    APP_TRANSPORT_NON_REGULAR_TRUCK,
                    APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW,
                    APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT,
                    APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL,
                    APP_ORGANIC_PLANT_PRODUCTION_NEW,
                    APP_ORGANIC_PLANT_PRODUCTION_RENEW,
                    APP_ORGANIC_PLANT_PRODUCTION_EDIT,
                    APP_ORGANIC_PLANT_PRODUCTION_CANCEL
                };
            }
        }

        public static string[] App_Restaurant_All
        {
            get
            {
                return APP_DANGER_ALL.ConcatItems(
                    APP_SELL_ALCOHOL,
                    APP_SELL_FOOD_GE_200,
                    APP_SELL_FOOD_LT_200,
                    APP_SELL_FOOD_LT_200_RENEW,
                    APP_SELL_FOOD_LT_200_EDIT,
                    APP_SELL_FOOD_LT_200_CANCEL,
                    APP_SELL_FOOD_GE_200_RENEW,
                    APP_SELL_PRODUCT_IN_PUBLIC_AREA,
                    APP_SELL_PRODUCT_IN_PUBLIC_AREA_RENEW,
                    APP_SELL_PRODUCT_IN_PUBLIC_AREA_EDIT,
                    APP_SELL_PRODUCT_IN_PUBLIC_AREA_CANCEL,
                    APP_SELL_FOOD_GE_200_EDIT,
                    APP_SELL_FOOD_GE_200_CANCEL
                );
            }
        }

        public static string[] APP_SME_ALL
        {
            get
            {
                return APP_DANGER_FOOD_ALL.ConcatItems(
                    APP_SELL_FOOD_LT_200,
                    APP_SELL_FOOD_LT_200_EDIT,
                    APP_SELL_FOOD_LT_200_CANCEL,
                    APP_SELL_FOOD_GE_200_RENEW,
                    APP_SELL_FOOD_GE_200,
                    APP_SELL_FOOD_GE_200_RENEW,
                    APP_SELL_FOOD_GE_200_EDIT,
                    APP_SELL_FOOD_GE_200_CANCEL,
                    APP_SELL_ALCOHOL,
                    APP_HOTEL
                );
            }
        }

        public static string[] APP_HEALTH_ALL
        {
            get
            {
                return new string[]
                {
                    //APP_BUSINESS_BAD_HEALTH_SERVICE,
                };
            }
        }

        public static string[] APP_VETERINARY_HOSPITAL
        {
            get
            {
                return new string[]
                {
                    APP_ANIMAL_BUILD,
                    APP_ANIMAL_BUILD_RENEW,
                };
            }
        }

        public static string[] APP_SOFTWARE_HOUSE
        {
            get
            {
                return new string[]
                {
                    APP_SOFTWARE_HOUSE_NEW,
                    APP_SOFTWARE_HOUSE_RENEW,
                    APP_SOFTWARE_HOUSE_EDIT
                };
            }
        }

        #endregion

        #region
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_3 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_3";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_4 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_4";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_5 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_5";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_6 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_6";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_7 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_7";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_8 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_8";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_9 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_9";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_10 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_10";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_11 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_11";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_12 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_12";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_13 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_13";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_14 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_14";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_15 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_15";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_16 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_16";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_17 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_17";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_18 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_18";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_19 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_19";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_20 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_20";
        //public const string APP_BUSINESS_BAD_HEALTH_FOOD_AND_DRINK_21 = "ขอใบอนุญาตประกอบกิจการที่เป็นอันตรายต่อสุขภาพ: อาหาร/เครื่องดื่ม_21";
        #endregion

        /// <summary>
        /// สณ.1
        /// </summary>
        public const string APP_SELL_PRODUCT_IN_PUBLIC_AREA = "ขอใบอนุญาตจำหน่ายสินค้าในที่หรือทางสาธารณะ";
        /// <summary>
        /// สณ.5
        /// ขอต่ออายุใบอนุญาตจำหน่ายสินค้าในที่หรือทางสาธารณะ
        /// </summary>
        public const string APP_SELL_PRODUCT_IN_PUBLIC_AREA_RENEW = "APP_SELL_PRODUCT_IN_PUBLIC_AREA_RENEW"; //"";
        /// <summary>
        /// สณ.9
        /// ขอแก้ไขใบอนุญาตจำหน่ายสินค้าในที่หรือทางสาธารณะ
        /// </summary>
        public const string APP_SELL_PRODUCT_IN_PUBLIC_AREA_EDIT = "APP_SELL_PRODUCT_IN_PUBLIC_AREA_EDIT";// "";
        /// <summary>
        /// สณ.8
        /// ขอยกเลิกใบอนุญาตจำหน่ายสินค้าในที่หรือทางสาธารณะ
        /// </summary>
        public const string APP_SELL_PRODUCT_IN_PUBLIC_AREA_CANCEL = "APP_SELL_PRODUCT_IN_PUBLIC_AREA_CANCEL"; //"";
        /// <summary>
        /// ขอใบอนุมัติแผนงานการจัดตั้งสถานพยาบาลประเภทที่รับผู้ป่วยไว้ค้างคืน
        /// </summary>
        public const string APP_HOSPITAL = "APP_HOSPITAL";
        /// <summary>
        /// ใบอนุญาตให้ประกอบกิจการสถานพยาบาล(ประเภทที่รับผู้ป่วยไว้ค้างคืน)
        /// </summary>
        public const string APP_HOSPITAL_PERMISSION = "APP_HOSPITAL_PERMISSION";
        /// <summary>
        /// ขอใบอนุญาตให้ประกอบกิจการสถานพยาบาล(ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน)
        /// </summary>
        public const string APP_ELDERLY_CARE = "APP_ELDERLY_CARE";
        /// <summary>
        /// ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล(ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน)
        /// </summary>
        public const string APP_ELDERLY_CARE_RENEW = "APP_ELDERLY_CARE_RENEW";
        /// <summary>
        /// ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาล(ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน)
        /// </summary>
        public const string APP_ELDERLY_CARE_EDIT = "APP_ELDERLY_CARE_EDIT";
        /// <summary>
        /// ขอยกเลิกใบอนุญาตให้ประกอบกิจการสถานพยาบาล(ประเภทที่ไม่รับผู้ป่วยไว้ค้างคืน)
        /// </summary>
        public const string APP_ELDERLY_CARE_CANCEL = "APP_ELDERLY_CARE_CANCEL";
        /// <summary>
        /// ขอใบอนุญาตประกอบกิจการสถานพยาบาล (คลินิก)
        /// </summary>
        public const string APP_CLINIC = "APP_CLINIC";
        /// <summary>
        /// ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาลหรือเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (คลินิก)
        /// </summary>
        public const string APP_CLINIC_RENEW = "APP_CLINIC_RENEW";
        /// <summary>
        /// ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาลหรือเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (คลินิก)
        /// </summary>
        public const string APP_CLINIC_EDIT = "APP_CLINIC_EDIT";
        /// <summary>
        /// ขอแจ้งเลิกกิจการสถานพยาบาล (คลินิก)
        /// </summary>
        public const string APP_CLINIC_CANCEL = "APP_CLINIC_CANCEL";
        /// <summary>
        /// ขอต่ออายุใบอนุญาตให้ประกอบกิจการสถานพยาบาล(ประเภทที่รับผู้ป่วยไว้ค้างคืน)
        /// </summary>
        public const string APP_HOSPITAL_PERMISSION_RENEW = "APP_HOSPITAL_PERMISSION_RENEW";
        /// <summary>
        /// ขอแก้ไขใบอนุญาตให้ประกอบกิจการสถานพยาบาลและใบอนุญาตให้ดำเนินการสถานพยาบาลหรือเปลี่ยนตัวผู้ดำเนินการสถานพยาบาล (โรงพยาบาล)
        /// </summary>
        public const string APP_HOSPITAL_PERMISSION_EDIT = "APP_HOSPITAL_PERMISSION_EDIT";
        /// <summary>
        /// ขอแจ้งเลิกกิจการสถานพยาบาล (ประเภทที่รับผู้ป่วยไว้ค้างคืน)
        /// </summary>
        public const string APP_HOSPITAL_PERMISSION_CANCEL = "APP_HOSPITAL_PERMISSION_CANCEL";
        /// <summary>
        /// ขอขึ้นทะเบียนสำนักงานทนายความ
        /// </summary>
        public const string APP_LAW_OFFICE = "APP_LAW_OFFICE";
        /// <summary>
        /// ขอเปลี่ยนแปลงข้อมูลสำนักงานทนายความ
        /// </summary>
        public const string APP_LAW_OFFICE_EDIT = "APP_LAW_OFFICE_EDIT";
        /// <summary>
        /// ขอยกเลิกขึ้นทะเบียนสำนักงํานทนายความ
        /// </summary>
        public const string APP_LAW_OFFICE_CANCEL = "APP_LAW_OFFICE_CANCEL";
        /// <summary>
        /// ขอใบอนุญาตประกอบธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า
        /// </summary>
        public const string APP_SECURITIES_BUSINESS = "APP_SECURITIES_BUSINESS";
        /// <summary>
        /// ขอแก้ไขใบอนุญาตประกอบธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า
        /// </summary>
        public const string APP_SECURITIES_BUSINESS_EDIT = "APP_SECURITIES_BUSINESS_EDIT";
        /// <summary>
        /// ขอยกเลิกใบอนุญาตประกอบธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า
        /// </summary>
        public const string APP_SECURITIES_BUSINESS_CANCEL = "APP_SECURITIES_BUSINESS_CANCEL";
        /// <summary>
        /// ขอใบอนุญาตผลิตพลังงานควบคุม
        /// </summary>
        public const string APP_ENERGY_PRODUCTION = "APP_ENERGY_PRODUCTION";
        /// <summary>
        /// ขอต่ออายุใบอนุญาตผลิตพลังงานควบคุม
        /// </summary>
        public const string APP_ENERGY_PRODUCTION_RENEW = "APP_ENERGY_PRODUCTION_RENEW";
        /// <summary>
        /// ขอแก้ไขใบอนุญาตผลิตพลังงานควบคุม
        /// </summary>
        public const string APP_ENERGY_PRODUCTION_EDIT = "APP_ENERGY_PRODUCTION_EDIT";
        /// <summary>
        /// ขอยกเลิกใบอนุญาตผลิตพลังงานควบคุม
        /// </summary>
        public const string APP_ENERGY_PRODUCTION_CANCEL = "APP_ENERGY_PRODUCTION_CANCEL";
        /// <summary>
        /// การรับแจ้งการประกอบกิจการพลังงานที่ได้รับการยกเว้นไม่ต้องขอรับใบอนุญาต
        /// </summary>
        public const string APP_ENERGY_PRODUCTION_NOT_PERMIT = "APP_ENERGY_PRODUCTION_NOT_PERMIT";
        /// <summary>
        /// ขอจดทะเบียนนิติบุคคลเพื่อประกอบกิจการให้บริการด้านวิชาชีพบัญชี
        /// </summary>
        public const string APP_ACCOUNTING_SERVICE = "APP_ACCOUNTING_SERVICE";



        /// <summary>
        /// จดทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์
        /// </summary>
        public const string APP_ELECTRONIC_COMMERCIAL = "APP_ELECTRONIC_COMMERCIAL";
        /// <summary>
        /// จดทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์ ( แก้ไข )
        /// </summary>
        public const string APP_ELECTRONIC_COMMERCIAL_EDIT = "APP_ELECTRONIC_COMMERCIAL_EDIT";
        /// <summary>
        /// จดทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์ ( ยกเลิก )
        /// </summary>
        public const string APP_ELECTRONIC_COMMERCIAL_CANCEL = "APP_ELECTRONIC_COMMERCIAL_CANCEL";

        /// <summary>
        /// ใบอนุญาตประกอบการขนส่งไม่ประจำทางด้วยรถบรรทุก
        /// </summary>
        public const string APP_TRANSPORT_NON_REGULAR_TRUCK = "APP_TRANSPORT_NON_REGULAR_TRUCK";
        /// <summary>
        /// ใบอนุญาตประกอบการขนส่งไม่ประจำทางด้วยรถบรรทุก ( ขอใหม่ )
        /// </summary>
        public const string APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW = "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW";
        /// <summary>
        /// ใบอนุญาตประกอบการขนส่งไม่ประจำทางด้วยรถบรรทุก ( แก้ไข )
        /// </summary>
        public const string APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT = "APP_TRANSPORT_NON_REGULAR_TRUCK_EDIT";
        /// <summary>
        /// ใบอนุญาตประกอบการขนส่งไม่ประจำทางด้วยรถบรรทุก ( ยกเลิก )
        /// </summary>
        public const string APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL = "APP_TRANSPORT_NON_REGULAR_TRUCK_CANCEL";

        /// <summary>
        /// ใบอนุญาตประกอบธุรกิจนำเที่ยว
        /// </summary>
        public const string APP_TOURISM_BUSINESS = "APP_TOURISM_BUSINESS";
        /// <summary>
        /// ใบอนุญาตประกอบธุรกิจนำเที่ยว ( ต่ออายุ )
        /// </summary>
        public const string APP_TOURISM_BUSINESS_RENEW = "APP_TOURISM_BUSINESS_RENEW";
        /// <summary>
        /// ใบอนุญาตประกอบธุรกิจนำเที่ยว ( แก้ไข )
        /// </summary>
        public const string APP_TOURISM_BUSINESS_EDIT = "APP_TOURISM_BUSINESS_EDIT";
        /// <summary>
        /// ใบอนุญาตประกอบธุรกิจนำเที่ยว ( ยกเลิก )
        /// </summary>
        public const string APP_TOURISM_BUSINESS_CANCEL = "APP_TOURISM_BUSINESS_CANCEL";

        /// <summary>
        /// การยื่นคำขอมีเลขประจำตัวซอฟต์แวร์เฮ้าส์
        /// </summary>
        public const string APP_SOFTWARE_HOUSE_NEW = "APP_SOFTWARE_HOUSE_NEW";
        /// <summary>
        /// การยื่นคำขอมีเลขประจำตัวซอฟต์แวร์เฮ้าส์ ( ขอใหม่ )
        /// </summary>
        public const string APP_SOFTWARE_HOUSE_RENEW = "APP_SOFTWARE_HOUSE_RENEW";
        /// <summary>
        /// การยื่นคำขอมีเลขประจำตัวซอฟต์แวร์เฮ้าส์ ( แก้ไข )
        /// </summary>
        public const string APP_SOFTWARE_HOUSE_EDIT = "APP_SOFTWARE_HOUSE_EDIT";




        #endregion

        #region SSO Permit
        /// <summary>
        /// หนังสือรับรองประกอบการนำเข้าเครื่องมือแพทย์
        /// </summary>
        public const string APP_IMPORT_MEDICAL_EQUIPMENT = "APP_IMPORT_MEDICAL_EQUIPMENT";

        /// <summary>
        /// ขออนุญาตจัดตั้งโรงเรียนนอกระบบ
        /// </summary>
        public const string APP_EDUCATION_PRIVATE_SCHOOL = "APP_EDUCATION_PRIVATE_SCHOOL";

        /// <summary>
        /// หนังสือแจ้งผลการรับฟังความคิดเห็นของประชาชนในการพิจารณาเกี่ยวกับโรงงานจำพวกที่ 3
        /// </summary>
        public const string APP_FACTORY_TYPE3 = "APP_FACTORY_TYPE3";

        /// <summary>
        /// ใบอนุญาตประกอบกิจการโรงงานจำพวกที่ 3
        /// </summary>
        public const string APP_FACTORY_CLASS_3_NEW = "APP_FACTORY_CLASS_3_NEW";

        /// <summary>
        /// การแจ้งเริ่มประกอบกิจการโรงงานจำพวกที่ 3
        /// </summary>
        public const string APP_FACTORY_CLASS_3_START_NEW = "APP_FACTORY_CLASS_3_START_NEW";
        

        /// <summary>
        /// ใบอนุญาตนำเข้าอาหาร
        /// </summary>
        public const string APP_IMPORT_FOOD = "APP_IMPORT_FOOD";

        /// <summary>
        /// ใบอนุญาตผลิตอาหาร
        /// </summary>
        public const string APP_FOOD_PRODUCTION = "APP_FOOD_PRODUCTION";

        /// <summary>
        /// ใบอนุญาตโฆษณาอาหาร (ฆอ.2)
        /// </summary>
        public const string APP_FOOD_ADVERTISEMENT = "APP_FOOD_ADVERTISEMENT";

        /// <summary>
        /// หนังสือรับรองผลิตภัณฑ์อาหาร (Certificate of Free Sale)
        /// </summary>
        public const string APP_FOOD_CERTIFICATE = "APP_FOOD_CERTIFICATE";

        #endregion

        public const string APP_MEA = "แบบฟอร์มขอใช้ไฟฟ้า";
        public const string APP_MWA = "แบบฟอร์มขอใช้น้ำประปา";
        public const string APP_PEA = "แบบฟอร์มขอใช้ไฟฟ้า (ภูมิภาค)";
        public const string APP_PWA = "แบบฟอร์มขอใช้น้ำประปา (ภูมิภาค)";
        public const string APP_TOT = "ขอใช้บริการโทรศัพท์พื้นฐาน และอินเทอร์เน็ต";
        public const string APP_SSO = "การขอขึ้นทะเบียนนายจ้างและผู้ประกันตน";
        public const string APP_VAT = "แบบฟอร์มขอจดทะเบียนภาษีมูลค่าเพิ่ม";
        public const string APP_BUILDING_G1 = "ขออนุญาตก่อสร้างอาคาร ดัดแปลงอาคาร หรือรื้อถอนอาคาร (ข.1)";
        public const string APP_BUILDING_R6 = "ขอใบรับรองการก่อสร้างอาคาร ดัดแปลงอาคาร หรือเคลื่อนย้ายอาคาร (ข.6)";

        public static string[] App_Utilities
        {
            get
            {
                return new string[] 
                {
                    APP_MEA,
                    APP_MWA,
                    APP_TOT,
                    APP_PEA,
                    APP_PWA
                };
            }
        }

        public const string APP_SEC_NEW_A = "APP_SEC_NEW_A";
        public const string APP_SEC_NEW_B = "APP_SEC_NEW_B";
        public const string APP_SEC_NEW_C = "APP_SEC_NEW_C";
        public const string APP_SEC_NEW_D = "APP_SEC_NEW_D";
        public const string APP_SEC_NEW_E = "APP_SEC_NEW_E";
        public const string APP_SEC_NEW_F = "APP_SEC_NEW_F";
        public const string APP_SEC_NEW_G = "APP_SEC_NEW_G";

        public static string[] ALL_APP_SECURITIES_BUSINESS = new string[]
        {
            APP_SEC_NEW_A,
            APP_SEC_NEW_B,
            APP_SEC_NEW_C,
            APP_SEC_NEW_D,
            APP_SEC_NEW_E,
            APP_SEC_NEW_F,
            APP_SEC_NEW_G,
        };

        // APP_SEC_EDIT
        public const string APP_SEC_EDIT = "APP_SEC_EDIT";

        public const string APP_SEC_CANCEL_A = "APP_SEC_CANCEL_A";
        public const string APP_SEC_CANCEL_B = "APP_SEC_CANCEL_B";
        public const string APP_SEC_CANCEL_C = "APP_SEC_CANCEL_C";
        public const string APP_SEC_CANCEL_D = "APP_SEC_CANCEL_D";
        public const string APP_SEC_CANCEL_E = "APP_SEC_CANCEL_E";
        public const string APP_SEC_CANCEL_F = "APP_SEC_CANCEL_F";
        public const string APP_SEC_CANCEL_G = "APP_SEC_CANCEL_G";

        private static string[] ELEMENTS =
        {
            APP_SEC_CANCEL_A,
            APP_SEC_CANCEL_B,
            APP_SEC_CANCEL_C,
            APP_SEC_CANCEL_D,
            APP_SEC_CANCEL_E,
            APP_SEC_CANCEL_F,
            APP_SEC_CANCEL_G
        };

        public static string[] ALL_APP_SEC_CANCEL
        {
            get
            {
                return ELEMENTS;
            }
        }
    }

    public class SpecialSectionGroupConst
    {
        public static string[] USE_SPECIAL_FUNCTION_ARRAY_OF_FORM
        {
            get
            {
                return new string[]
                {
                    "APP_TRANSPORT_NON_REGULAR_TRUCK",
                    "APP_TRANSPORT_NON_REGULAR_TRUCK_RENEW",
                };
            }
        }
    }

    public class SellFoodInformationNationalityOptionValueConst
    {
        public const string THAI = "THAI";
        public const string FOREIGNER = "FOREIGNER";
    }
    public class SellFoodInformationNationalityOptionTextConst
    {
        public const string THAI = "ไทย";
        public const string FOREIGNER = "ต่างด้าว";
    }

    public class NationalityValueConst
    {
        public const string THAI = "thai";
        public const string FOREIGNER = "foreigner";
    }
    public class NationalityTextConst
    {
        public const string THAI = "ไทย";
        public const string FOREIGNER = "ต่างด้าว";
    }

    #region Business Bad Health
    public sealed class BadHealthIsAllowOverNightValueConst
    {
        public const string YES = "YES";
        public const string NO = "NO";
    }

    public sealed class BadHealthIsAllowOverNightTextConst
    {
        public const string YES = "อนุญาต";
        public const string NO = "ไม่อนุญาต";
    }

    public sealed class BadHealthBuildingTypeValueConst
    {
        public const string OLD_BUILDING = "OLD_BUILDING";
        public const string NEW_BUILDING = "NEW_BUILDING";
    }

    public sealed class BadHealthBuildingTypeTextConst
    {
        public const string OLD_BUILDING = "มีอยู่เดิม";
        public const string NEW_BUILDING = "ก่อสร้างใหม่";
    }

    public sealed class BadHealthDocumentTypeBuildingValueConst
    {
        public const string LICENSE = "LICENSE";
        public const string CERTIFICATE = "CERTIFICATE";
    }

    public sealed class BadHealthDocumentTypeBuildingTextConst
    {
        public const string LICENSE = "ใบอนุญาตก่อสร้าง";
        public const string CERTIFICATE = "หนังสือแจ้งความประสงค์จะก่อสร้าง";
    }
    #endregion

    public sealed class FormSectionGroupTextConstant
    {
        public const string APP_BUSINESS_BAD_HEALTH_GENERAL = "APP_BUSINESS_BAD_HEALTH_GENERAL";
        public const string APP_BUSINESS_BAD_HEALTH_DRINK_AND_FOOD = "APP_BUSINESS_BAD_HEALTH_DRINK_AND_FOOD"; //อภ.1 อาหาร/เครื่องดื่ม
        public const string APP_BUSINESS_BAD_HEALTH_SERVICE = "APP_BUSINESS_BAD_HEALTH_SERVICE"; //อภ.1 บริการ

        public const string APP_SELL_PRODUCT_IN_PUBLIC = "APP_SELL_PRODUCT_IN_PUBLIC"; //สณ.1
        /// <summary>
        /// สณ.5
        /// ต่ออายุใบอนุญาตเป็นผู้จำหน่ายสินค้าในที่หรือทางสาธารณะ
        /// </summary>
        public const string APP_SELL_PRODUCT_IN_PUBLIC_RENEW = "APP_SELL_PRODUCT_IN_PUBLIC_RENEW";
        /// <summary>
        /// สณ.9
        /// คำขอเปลี่ยนแปลงชนิดหรือประเภทสินค้าหรือลักษณะวิธีการจำหน่ายหรือสถานที่จัดวางสินค้าหรือผู้ช่วยจำหน่ายสินค้าในที่หรืทางสาธารณะ
        /// </summary>
        public const string APP_SELL_PRODUCT_IN_PUBLIC_EDIT = "APP_SELL_PRODUCT_IN_PUBLIC_EDIT";
        /// <summary>
        /// สณ.8
        /// คำขอบอกเลิกการดำเนินการจำหน่ายสินค้าในที่หรือทางสาธารณะ
        /// </summary>
        public const string APP_SELL_PRODUCT_IN_PUBLIC_CANCEL = "APP_SELL_PRODUCT_IN_PUBLIC_CANCEL";
    }

    public sealed class FormSectionTextConstant
    {
        #region อภ.1 ทั่วไป
        public const string BUSINESS_BAD_HEALTH_GENERAL_INFOMATION = "BBH_GENERAL_INFOMATION";
        #endregion

        #region อภ.1 อาหาร/เครื่องดื่ม
        public const string BUSINESS_BAD_HEALTH_DRINK_AND_FOOD_INFORMATION = "BBH_DRINK_AND_FOOD_INFOMATION";
        public const string BUSINESS_BAD_HEALTH_TYPE_OF_DRINK_AND_FOOD_INFORMATION = "BBH_TYPE_OF_DRINK_AND_FOOD_INFOMATION";
        #endregion

        #region อภ.1 บริการ
        public const string BUSINESS_BAD_HEALTH_SERVICE_INFOMATION = "BBH_SERVICE_INFOMATION";
        #endregion

        #region สณ.1
        public const string SELL_PRODUCT_IN_PUBLIC_INFORMATION = "SELL_PRODUCT_IN_PUBLIC_INFORMATION";
        public const string SELL_PRODUCT_ASSISTANT_INFORMATION = "SELL_PRODUCT_ASSISTANT_INFORMATION";
        public const string SELL_PRODUCT_ASSISTANT_INFORMATION_2 = "SELL_PRODUCT_ASSISTANT_INFORMATION_2";
        #endregion

        #region สณ.5 ต่ออายุใบอนุญาตเป็นผู้จำหน่ายสินค้าในที่หรือทางสาธารณะ
        public const string SELL_PRODUCT_IN_PUBLIC_INFORMATION_RENEW = "SELL_PRODUCT_IN_PUBLIC_INFORMATION_RENEW";
        public const string SELL_PRODUCT_ASSISTANT_INFORMATION_RENEW = "SELL_PRODUCT_ASSISTANT_INFORMATION_RENEW";
        public const string SELL_PRODUCT_ASSISTANT_INFORMATION_2_RENEW = "SELL_PRODUCT_ASSISTANT_INFORMATION_2_RENEW";
        #endregion

        #region สณ.9 คำขอเปลี่ยนแปลงชนิดหรือประเภทสินค้าหรือลักษณะวิธีการจำหน่ายหรือสถานที่จัดวางสินค้าหรือผู้ช่วยจำหน่ายสินค้าในที่หรืทางสาธารณะ
        public const string SELL_PRODUCT_IN_PUBLIC_INFORMATION_EDIT = "SELL_PRODUCT_IN_PUBLIC_INFORMATION_EDIT";
        public const string SELL_PRODUCT_ASSISTANT_INFORMATION_EDIT = "SELL_PRODUCT_ASSISTANT_INFORMATION_EDIT";
        public const string SELL_PRODUCT_ASSISTANT_INFORMATION_2_EDIT = "SELL_PRODUCT_ASSISTANT_INFORMATION_2_EDIT";
        #endregion

        #region สณ.8 คำขอบอกเลิกการดำเนินการจำหน่ายสินค้าในที่หรือทางสาธารณะ
        public const string SELL_PRODUCT_IN_PUBLIC_INFORMATION_CANCEL = "SELL_PRODUCT_IN_PUBLIC_INFORMATION_CANCEL";
        public const string SELL_PRODUCT_ASSISTANT_INFORMATION_CANCEL = "SELL_PRODUCT_ASSISTANT_INFORMATION_CANCEL";
        public const string SELL_PRODUCT_ASSISTANT_INFORMATION_2_CANCEL = "SELL_PRODUCT_ASSISTANT_INFORMATION_2_CANCEL";
        #endregion

    }

    /// <summary>
    /// ช่องทางการส่งใบอนุญาตให้กับผู้ประกอบการ
    /// </summary>
    public sealed class PermitDeliveryTypeValueConst
    {
        /// <summary>
        /// ส่งทางไปษณีย์
        /// </summary>
        public const string BY_MAIL = "BY_MAIL";
        /// <summary>
        /// รับที่หน่วยงาน
        /// </summary>
        public const string AT_OWNER_ORG = "AT_OWNER_ORG";
        /// <summary>
        /// รับที่ OSS
        /// </summary>
        public const string AT_OSS = "AT_OSS";
    }

    /// <summary>
    /// ช่องทางการชำระเงิน
    /// </summary>
    public sealed class PaymentMethodValueConst
    {
        /// <summary>
        /// ทาง QR Code
        /// </summary>
        public const string QR_CODE = "QR_CODE";
        /// <summary>
        /// ทาง Bill Payment
        /// </summary>
        public const string BILL_PAYMENT = "BILL_PAYMENT";
        /// <summary>
        /// ชำระเงินที่ ศูนย์รับคำขออนุญาต (OSS)
        /// </summary>
        public const string AT_OSS = "AT_OSS";
        /// <summary>
        /// ชำระเงินที่ สำนักงาน
        /// </summary>
        public const string AT_OWNER_ORG = "AT_OWNER_ORG";
    }

    /// <summary>
    /// ประเภทเมื่อทำการเลือก payment method แบบ Bill payment
    /// </summary>
    public sealed class BillPaymentTypeValueConst
    {
        /// <summary>
        /// ของหน่วยงาน
        /// </summary>
        public const string BILL_PAYMENT_TYPE_OWNER_ORG = "OWNER_ORG";
        /// <summary>
        /// กรมบัญชีกลาง
        /// </summary>
        public const string BILL_PAYMENT_TYPE_CGD = "CGD";
    }

    /// <summary>
    /// ประเภทเมื่อทำการเลือก ผู้ชำระค่าบริการจัดส่งใบอนุญาตผ่านระบบไปรษณีย์
    /// </summary>
    public sealed class EMSFeePaymentTypeValueConst
    {
        /// <summary>
        /// หน่วยงานผู้ออกใบอนุญาต
        /// </summary>
        public const string OWNER_ORG = "OWNER_ORG";
        /// <summary>
        /// ผู้ประกอบการ
        /// </summary>
        public const string USER = "USER";
    }

    /// <summary>
    /// สถานะย่อยของใบคำร้อง
    /// </summary>
    public sealed class ApplicationStatusOtherValueConst
    {

        /// <summary>
        /// รอเจ้าหน้าที่รับเรื่อง
        /// </summary>
        public const string WAITING_AGENT_READ_REQUEST = "WAITING_AGENT_READ_REQUEST";
        /// <summary>
        /// รอเจ้าหน้าที่ดำเนินการ
        /// </summary>
        public const string WAITING_AGENT_WORKING = "WAITING_AGENT_WORKING";
        /// <summary>
        /// รอเจ้าหน้าที่ยืนยันการขอยกเลิกคำขอ
        /// </summary>
        public const string WAITING_AGENT_APPROVE_CANCEL = "WAITING_AGENT_APPROVE_CANCEL";
        /// <summary>
        /// รอผู้ประกอบการดำเนินการ
        /// </summary>
        public const string WAITING_USER_WORKING = "WAITING_USER_WORKING";
        /// <summary>
        /// รอเจ้าหน้าที่ยืนยันการทำงาน
        /// </summary>
        public const string WAITING_AGENT_PROCESS = "WAITING_AGENT_PROCESS";
        /// <summary>
        /// เสร็จสิ้น
        /// </summary>
        public const string DONE = "DONE";
        /// <summary>
        /// ปฏิเสธ
        /// </summary>
        public const string REJECT = "REJECT";
        /// <summary>
        /// ยกเลิกโดยผู้ประกอบการ
        /// </summary>
        public const string CANCEL_COMPLETED = "CANCEL_COMPLETED";
        /// <summary>
        /// สามารถส่งคำร้องซ้ำได้ กรณีที่ส่งครั้งแรกไม่ผ่าน
        /// </summary>
        public const string RESENDABLE = "Resendable";
    }

    public class APP_COMMERCIAL_DISPLAY_CONDITION
    {
        public static FormControlDisplayCondition COMMERCIAL_TYPE_PARTNERSHIP
        {
            get
            {
                var COMMERCIAL_TYPE_PARTNERSHIP = new FormControlDisplayCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                            new FormControlDisplayCondition.ControlWithAnswer
                            {
                                ControlName = "APP_COMMERCIAL_REGISTRATION_TYPE",
                                ControlAnswer = CommercialRegistrationTypeOptionValueConst.PARTNERSHIP
                            },
                    },
                };
                return COMMERCIAL_TYPE_PARTNERSHIP;
            }
        }
    }
    public class APP_FACTORY_TYPE2_CONDITIO
    {
        private static FormControlDisplayCondition _FACTORY_TYPE2_PROPERTY;
        public static FormControlDisplayCondition FACTORY_TYPE2_PROPERTY
        {
            get
            {
                _FACTORY_TYPE2_PROPERTY = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                   {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "FACTORY_TYPE2_PROPERTY",
                            ControlAnswer = "ยังไม่มีเลขที่บ้าน"
                        },
                   },
                };
                return _FACTORY_TYPE2_PROPERTY;
            }
        }

        private static FormControlDisplayCondition _A;
        public static FormControlDisplayCondition A
        {
            get
            {
                _A = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "FACTORY_TYPE2_PROPERTY",
                            ControlAnswer = "อาคาร"
                        },
                    },
                };
                return _A;
            }
        }

        private static FormControlDisableCondition _B;
        public static FormControlDisableCondition B
        {
            get
            {
                _A = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisableCondition.ControlWithAnswer[]
                   {
                        new FormControlDisableCondition.ControlWithAnswer
                        {
                            ControlName = "FACTORY_TYPE2_PROPERTY",
                            ControlAnswer = "ยังไม่มีเลขที่บ้าน"
                        },
                   },
                };
                return _B;
            }
        }
    }

    public class APP_ORGANIC_NEW
    {
        private static FormControlDisableCondition _HEAD_OF_GROUP;
        public static FormControlDisableCondition HEAD_OF_GROUP
        {
            get
            {
                _HEAD_OF_GROUP = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_HEAD_OF_GROUP",
                            ControlAnswer = "2"
                        },
                    },
                };
                return _HEAD_OF_GROUP;
            }
        }

        private static FormControlDisableCondition _GROUP_PRODUCTION;
        public static FormControlDisableCondition GROUP_PRODUCTION
        {
            get
            {
                _GROUP_PRODUCTION = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_KIND_OF_PLANT",
                            ControlAnswer = "2"
                        },
                    },
                };
                return _GROUP_PRODUCTION;
            }
        }

        private static FormControlDisableCondition _ALONE_PRODUCTION;
        public static FormControlDisableCondition ALONE_PRODUCTION
        {
            get
            {
                _ALONE_PRODUCTION = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_KIND_OF_PLANT",
                            ControlAnswer = "1"
                        },
                    },
                };
                return _ALONE_PRODUCTION;
            }
        }

        private static FormControlDisableCondition _THAI_NAME;
        public static FormControlDisableCondition THAI_NAME
        {
            get
            {
                _THAI_NAME = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_2",
                            ControlAnswer = "true"
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_NEW_SPECIFIC_NEW_KIND_OF_PLANT",
                            ControlAnswer = "1"
                        },
                    },
                    IsOr = false,
                };
                return _THAI_NAME;
            }
        }

        private static FormControlDisableCondition _ENG_NAME;
        public static FormControlDisableCondition ENG_NAME
        {
            get
            {
                _ENG_NAME = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                    {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_3",
                            ControlAnswer = "true"
                        },
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE__APP_ORGANIC_PLANT_PRODUCTION_DOC_TYPE_4",
                            ControlAnswer = "true"
                        },
                    },
                };
                return _ENG_NAME;
            }
        }
    }

    public class APP_FACTORY_CLASS_2_EDIT
    {

        private static FormControlDisplayCondition _opt1;
        public static FormControlDisplayCondition Opt1
        {

            get
            {
                _opt1 = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                   {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_FACTORY_CLASS_2_EDIT_CHANGING__APP_FACTORY_CLASS_2_EDIT_CHANGING_CHECKBOX",
                            ControlAnswer = "true"
                        },
                   },
                };
                return _opt1;
            }
        }

        private static FormControlDisplayCondition _opt2;
        public static FormControlDisplayCondition Opt2
        {

            get
            {
                _opt2 = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                   {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_FACTORY_CLASS_2_EDIT_CHANGING__APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_HEAD_OFFICE_CHECKBOX",
                            ControlAnswer = "true"
                        },
                   },
                };
                return _opt2;
            }

        }

        private static FormControlDisplayCondition _opt3;
        public static FormControlDisplayCondition Opt3
        {

            get
            {
                _opt3 = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                   {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_FACTORY_CLASS_2_EDIT_CHANGING__APP_FACTORY_CLASS_2_EDIT_CHANGING_ADDRESS_CHECKBOX",
                            ControlAnswer = "true"
                        },
                   },
                };
                return _opt3;
            }

        }

        private static FormControlDisplayCondition _opt4;
        public static FormControlDisplayCondition Opt4
        {

            get
            {
                _opt4 = new FormControlDisableCondition
                {
                    Conditions = new FormControlDisplayCondition.ControlWithAnswer[]
                   {
                        new FormControlDisplayCondition.ControlWithAnswer
                        {
                            ControlName = "APP_FACTORY_CLASS_2_EDIT_CHANGING__APP_FACTORY_CLASS_2_EDIT_CHANGING_DETAIL",
                            ControlAnswer = "true"
                        },
                   },
                };
                return _opt4;
            }

        }

    }

}
