using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.AppsHook
{   
    /// <summary>
    /// เชื่อมโยงกับหน้าตรวจสอบสถานะการยื่นคำร้องของเจ้าหน้าที่รัฐ
    /// </summary>
    public sealed class ViewNameForAgentConst
    {
        /// <summary>
        /// เชื่อมโยงกับหน้าตรวจสอบสถานะการยื่นคำร้องของเจ้าหน้าที่รัฐ Razor view work flow แบบใหม่
        /// </summary>
        public const string NEW_FLOW = "Detail_New_Flow";

        /// <summary>
        /// เชื่อมโยงกับหน้าตรวจสอบสถานะการยื่นคำร้องของเจ้าหน้าที่รัฐ Razor view work flow แบบเดิม
        /// </summary>
        public const string ORIGINAL = "Detail";
    }

    public sealed class ViewNameForUserConst
    {
        /// <summary>
        /// เชื่อมโยงกับหน้าติดตามสถานะ Razor view work flow แบบใหม่
        /// </summary>
        public const string NEW_FLOW = "Detail_New_Flow";

        /// <summary>
        /// เชื่อมโยงกับหน้าติดตามสถานะ Razor view work flow แบบเดิม
        /// </summary>
        public const string ORIGINAL = "Detail";
    }

    public sealed class RenderAppHookBaseConst
    {
        public const string HIDE_HEADER_SECTION = "C_RM_H";
        public const string NOW_SHOW_ITEM = "N_S_I";
    }
}
