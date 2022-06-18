using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.AppsHook
{
    public enum AppsHookResult
    {
        Created,
        Failed
    }

    public enum AppsStage
    {
        None,
        UserCreate, // สร้างข้อมูลโดยประชาชน/ผู้ประกอบการ
        UserUpdate, // ปรับปรุงข้อมูลโดยประชาชน/ผู้ประกอบการ
        AgentUpdate, // ปรับปรุงข้อมูลโดยเจ้าหน้าที่รัฐผ่านระบบหลังบ้าน Biz Portal
        ApiUpdate // ปรับปรุงข้อมูลผ่านทาง API จากหน่วยงานรัฐ
    }

    public enum RenderStage
    {
        UserTracking,
        AgentTracking
    }

    public enum DisplayType
    {
        Static,
        TextBox,
        OrderList,
        UnOrderList,
        Link,
        RadioList,
        CheckboxList,
        CustomHtml,
        Checkbox
    }

    public enum AppsHookKey
    {
        RESULT,
        EXECUTE_DATE,
        SCHEDULE,
        SCHEDULE_DATE,
        APPS_STAGE,
        DATA,
        FILE_SYNCED_STATUS,
        FILE_SYNCED_DATE
    }
    public enum OrderMethodCode
    {
        [StringValue("ระบบตะกร้า")]      
        BASKET = 01,
        [StringValue("ระบบกรอกฟอร์ม")]
        FORM = 02,
        [StringValue("e-Mail")]
        EMAIL = 03,
        [StringValue("โทรศัพท์")]
        PHONE = 04,
        [StringValue("โทรสาร")]
        FAX = 05,
        [StringValue("อื่นๆ ")]
        OTHER = 99
    }
    public enum PaymentMethodCode
    {
        [StringValue("ชำระเงินแบบออฟไลน์ (โอนเงินผ่านธนาคาร ชำระเงินทางไปรษณีย์ ชำระเงินกับพนักงาน เป็นต้น)")]
        OFFLINE = 01,
        [StringValue("ชำระเงินออนไลน์ ผ่านบัตรเครดิต")]
        ONLINE_CREDIT_CARD = 02,
        [StringValue("ชำระเงินออนไลน์ ผ่านระบบ e-Banking")]
        ONLINE_E_BANKING = 03,
        [StringValue("ชำระเงินออนไลน์ ผ่านตัวกลางชำระเงิน เช่น PayPal, PaySbuy เป็นต้น")]
        ONLINE_AGENT = 04,
        [StringValue("อื่นๆ ")]
        OTHER = 99
    }
    public enum DeliveryMethodCode
    {
        [StringValue("บริษัทขนส่ง")]
        TRANSPORT_COMPANY = 01,
        [StringValue("ไปรษณีย์")]
        POST_OFFICE = 02,
        [StringValue("พนักงานส่งสินค้า")]
        DELEVERY_STAFF = 03,
        [StringValue("Download")]
        DOWNLOAD = 04,
        [StringValue("e-Mail")]
        EMAIL = 05,
        [StringValue("อื่นๆ ")]
        OTHER = 99
    }
    public enum URLC_CODE
    {
        [StringValue("WEBSITE")]
        WEBSITE = 01,
        [StringValue("FACEBOOK")]
        FACEBOOK = 02,
        [StringValue("LINE")]
        LINE = 03,
        [StringValue("INSTRAGRAM")]
        INSTRAGRAM = 04,
        [StringValue("MARKETPLACE")]
        MARKETPLACE = 05,     
        [StringValue("APPLICATION")]
        APPLICATION = 06,
        [StringValue("LAZADA")]
        LAZADA = 07,
        [StringValue("SHOPEE")]
        SHOPEE = 08,
        [StringValue("FOODPANDA")]
        FOODPANDA = 09,
        [StringValue("อื่นๆ ")]
        OTHER = 99
    }
}
