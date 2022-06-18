using BizPortal.ViewModels.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.AppsHook
{
    public class AppHookRenderViewModel
    {
        public RenderStage Stage { get; set; }

        public ApplicationRequestViewModel Data { get; set; }

        public IAppsHook Hook { get; set; }

        public AppHookRenderViewModel(IAppsHook hook, RenderStage stage, ApplicationRequestViewModel data)
        {
            Hook = hook;
            Stage = stage;
            Data = data;
        }
    }
}
