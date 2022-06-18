using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public class FormControlDisplayCondition
    {
        public bool InitAsShow { get; set; }

        public ControlWithAnswer[] Conditions { get; set; }

        /// <summary>
        /// กรณีต้องการทำ Nested condition ให้เอา sub-conditions มาใส่ไว้ใน object นี้ (เป็น resursive ลึกลงไปไม่จำกัด level)  
        /// และหากมีการกำหนดค่าที่ SubDisplayConditions จะไม่สนใจค่าใน Conditions เวลาทำการ validate
        /// </summary>
        public FormControlDisplayCondition[] SubDisplayConditions { get; set; }

        public bool IsOr { get; set; }

        public class ControlWithAnswer
        {
            public string ControlName { get; set; }
            public string ControlAnswer { get; set; }

            /// <summary>
            /// ถ้าต้องการ compare ว่าค่าของ ControlName ต้องไม่เท่ากับ ControlAnswer ให้กำหนดเป็น true
            /// </summary>
            public bool IsNotEquals { get; set; } = false;
        }
        public FormControlDisplayCondition()
        {
            IsOr = true;
        }
    }

    public interface ISupportDisplayCondition
    {
        FormControlDisplayCondition DisplayCondition { get; set; }
    }

    public class DisplayConditionAttribute
    {
        public string Style;
        public string Class;
        public string Data;
    }

    public class DisplayConditionHelper
    {
        /// <summary>
        /// กรณีที่มีการใช้ Nested condition ระบบจะแปลง condition ให้อยู่ในรูปของ class นี้แทนที่จะเป็น array of string แบบกรณี Normal
        /// </summary>
        public class AdvanceDataDisplayCondition
        {
            public bool IsOr { get; set; } = true;
            public AdvanceDataDisplayCondition[] Conditions { get; set; }
            public string[] ConditionWithAnswers { get; set; }

            public AdvanceDataDisplayCondition[] ToDataDisplayCondition(FormControlDisplayCondition[] formDisplayConditions, List<string> addedControlNames, ref string displayConditionClass)
            {
                List<AdvanceDataDisplayCondition> dataConditions = new List<AdvanceDataDisplayCondition>();
                foreach(var fcond in formDisplayConditions)
                {
                    AdvanceDataDisplayCondition cond = new AdvanceDataDisplayCondition();
                    cond.IsOr = fcond.IsOr;

                    if (fcond.SubDisplayConditions != null && fcond.SubDisplayConditions.Length > 0)
                    {
                        cond.Conditions = cond.ToDataDisplayCondition(fcond.SubDisplayConditions, addedControlNames, ref displayConditionClass);
                    }
                    else if (fcond.Conditions != null && fcond.Conditions.Length > 0)
                    {
                        // Build a condition expression.
                        var answers = DisplayConditionHelper.GetAnswerConditionsData(fcond, addedControlNames, ref displayConditionClass);
                        cond.ConditionWithAnswers = answers.ToArray();
                    }

                    dataConditions.Add(cond);
                }

                return dataConditions.ToArray();
            }
        }

        private static List<string> GetAnswerConditionsData(FormControlDisplayCondition displayCondition, List<string> addedControlNames, ref string displayConditionClass)
        {
            var displayConditionData = new List<string>();
            if (displayCondition.IsOr)
            {
                foreach (var c in displayCondition.Conditions)
                {
                    if (!addedControlNames.Contains(c.ControlName))
                    {
                        displayConditionClass += c.ControlName + " ";
                        addedControlNames.Add(c.ControlName);
                    }

                    if (c.IsNotEquals)
                    {
                        displayConditionData.Add(c.ControlName + "!::" + c.ControlAnswer);
                    }
                    else
                    {
                        displayConditionData.Add(c.ControlName + "::" + c.ControlAnswer);
                    }
                }
            }
            else
            {
                var displayConditionStr = "";
                foreach (var c in displayCondition.Conditions)
                {
                    if (!addedControlNames.Contains(c.ControlName))
                    {
                        displayConditionClass += c.ControlName + " ";
                        addedControlNames.Add(c.ControlName);
                    }

                    if (c.IsNotEquals)
                    {
                        displayConditionStr += c.ControlName + "!::" + c.ControlAnswer + ",";
                    }
                    else
                    {
                        displayConditionStr += c.ControlName + "::" + c.ControlAnswer + ",";
                    }
                }
                displayConditionData.Add(displayConditionStr.Trim(new char[] { ',' }));
            }

            return displayConditionData;
        }

        public static DisplayConditionAttribute GetAttributes(ISupportDisplayCondition ctrl)
        {
            var displayConditionStyle = "";
            var displayConditionClass = "";
            var displayConditionData = new List<string>();
            string advancedDisplayConditionData = "";

            if (ctrl.DisplayCondition != null)
            {
                displayConditionStyle = !ctrl.DisplayCondition.InitAsShow ? "display: none;" : "";
                displayConditionClass = ctrl.DisplayCondition.InitAsShow ? "hide-condition " : "show-condition ignore ";
                List<string> addedControlNames = new List<string>();

                if (ctrl.DisplayCondition.SubDisplayConditions != null && ctrl.DisplayCondition.SubDisplayConditions.Length > 0)
                {
                    // This control uses nested display condition. We need to use another format of display-condition-data
                    AdvanceDataDisplayCondition advanceCond = new AdvanceDataDisplayCondition();
                    advanceCond.IsOr = ctrl.DisplayCondition.IsOr;
                    advanceCond.Conditions = advanceCond.ToDataDisplayCondition(ctrl.DisplayCondition.SubDisplayConditions, addedControlNames, ref displayConditionClass);

                    advancedDisplayConditionData = Newtonsoft.Json.JsonConvert.SerializeObject(advanceCond);
                }
                else
                {
                    // For non-nested display condition, use regular logic as of 2019-11-22
                    if (ctrl.DisplayCondition.IsOr)
                    {
                        foreach (var c in ctrl.DisplayCondition.Conditions)
                        {
                            if (!addedControlNames.Contains(c.ControlName))
                            {
                                displayConditionClass += c.ControlName + " ";
                                addedControlNames.Add(c.ControlName);
                            }

                            if (c.IsNotEquals)
                            {
                                displayConditionData.Add(c.ControlName + "!::" + c.ControlAnswer);
                            }
                            else
                            {
                                displayConditionData.Add(c.ControlName + "::" + c.ControlAnswer);
                            }
                        }
                    }
                    else
                    {
                        var displayConditionStr = "";
                        foreach (var c in ctrl.DisplayCondition.Conditions)
                        {
                            if (!addedControlNames.Contains(c.ControlName))
                            {
                                displayConditionClass += c.ControlName + " ";
                                addedControlNames.Add(c.ControlName);
                            }

                            if (c.IsNotEquals)
                            {
                                displayConditionStr += c.ControlName + "!::" + c.ControlAnswer + ",";
                            }
                            else
                            {
                                displayConditionStr += c.ControlName + "::" + c.ControlAnswer + ",";
                            }
                        }
                        displayConditionData.Add(displayConditionStr.Trim(new char[] { ',' }));
                    }
                }
            }

            

            return new DisplayConditionAttribute
            {
                Style = displayConditionStyle,
                Class = displayConditionClass,
                Data = !string.IsNullOrEmpty(advancedDisplayConditionData) ? advancedDisplayConditionData : Newtonsoft.Json.JsonConvert.SerializeObject(displayConditionData)
            };
        }
    }

    #region Disable condition

    public class FormControlDisableCondition : FormControlDisplayCondition
    {
        public FormControlDisableCondition() : base()
        {
        }

        /// <summary>
        /// กรณีต้องการทำ Nested condition ให้เอา sub-conditions มาใส่ไว้ใน object นี้ (เป็น resursive ลึกลงไปไม่จำกัด level)  
        /// และหากมีการกำหนดค่าที่ SubDisableConditions จะไม่สนใจค่าใน Conditions เวลาทำการ validate
        /// </summary>
        public FormControlDisableCondition[] SubDisableConditions { get; set; }
    }

    public class DisableConditionAttribute : DisplayConditionAttribute
    {
        public DisableConditionAttribute() : base()
        {

        }
    }

    public interface ISupportDisableCondition
    {
        FormControlDisableCondition DisableCondition { get; set; }
    }

    public class DisableConditionHelper
    {
        /// <summary>
        /// กรณีที่มีการใช้ Nested condition ระบบจะแปลง condition ให้อยู่ในรูปของ class นี้แทนที่จะเป็น array of string แบบกรณี Normal
        /// </summary>
        public class AdvanceDataDisableCondition
        {
            public bool IsOr { get; set; } = true;
            public AdvanceDataDisableCondition[] Conditions { get; set; }
            public string[] ConditionWithAnswers { get; set; }

            public AdvanceDataDisableCondition[] ToDataDisableCondition(FormControlDisableCondition[] formDisableConditions, List<string> addedControlNames, ref string disableConditionClass)
            {
                List<AdvanceDataDisableCondition> dataConditions = new List<AdvanceDataDisableCondition>();
                foreach (var fcond in formDisableConditions)
                {
                    AdvanceDataDisableCondition cond = new AdvanceDataDisableCondition();
                    cond.IsOr = fcond.IsOr;

                    if (fcond.SubDisplayConditions != null && fcond.SubDisplayConditions.Length > 0)
                    {
                        cond.Conditions = cond.ToDataDisableCondition(fcond.SubDisableConditions, addedControlNames, ref disableConditionClass);
                    }
                    else if (fcond.Conditions != null && fcond.Conditions.Length > 0)
                    {
                        // Build a condition expression.
                        var answers = DisableConditionHelper.GetAnswerConditionsData(fcond, addedControlNames, ref disableConditionClass);
                        cond.ConditionWithAnswers = answers.ToArray();
                    }

                    dataConditions.Add(cond);
                }

                return dataConditions.ToArray();
            }
        }

        private static List<string> GetAnswerConditionsData(FormControlDisableCondition disableCondition, List<string> addedControlNames, ref string disableConditionClass)
        {
            var displayConditionData = new List<string>();
            if (disableCondition.IsOr)
            {
                foreach (var c in disableCondition.Conditions)
                {
                    if (!addedControlNames.Contains(c.ControlName))
                    {
                        disableConditionClass += c.ControlName + " ";
                        addedControlNames.Add(c.ControlName);
                    }

                    if (c.IsNotEquals)
                    {
                        displayConditionData.Add(c.ControlName + "!::" + c.ControlAnswer);
                    }
                    else
                    {
                        displayConditionData.Add(c.ControlName + "::" + c.ControlAnswer);
                    }
                }
            }
            else
            {
                var displayConditionStr = "";
                foreach (var c in disableCondition.Conditions)
                {
                    if (!addedControlNames.Contains(c.ControlName))
                    {
                        disableConditionClass += c.ControlName + " ";
                        addedControlNames.Add(c.ControlName);
                    }

                    if (c.IsNotEquals)
                    {
                        displayConditionStr += c.ControlName + "!::" + c.ControlAnswer + ",";
                    }
                    else
                    {
                        displayConditionStr += c.ControlName + "::" + c.ControlAnswer + ",";
                    }
                }
                displayConditionData.Add(displayConditionStr.Trim(new char[] { ',' }));
            }

            return displayConditionData;
        }

        public static DisableConditionAttribute GetAttributes(ISupportDisableCondition ctrl)
        {
            string advancedDisableConditionData = "";
            var disableConditionStyle = "";
            var disableConditionClass = "";
            var disableConditionData = new List<string>();
            if (ctrl.DisableCondition != null)
            {
                disableConditionClass = " disable-condition ";
                List<string> addedControlNames = new List<string>();

                // Frontis 2020-02-17: Modified to support nested condition and "not equal" operation
                if (ctrl.DisableCondition.SubDisableConditions != null && ctrl.DisableCondition.SubDisableConditions.Length > 0)
                {
                    // This control uses nested disable condition. We need to use another format of disable-condition-data
                    AdvanceDataDisableCondition advanceCond = new AdvanceDataDisableCondition();
                    advanceCond.IsOr = ctrl.DisableCondition.IsOr;
                    advanceCond.Conditions = advanceCond.ToDataDisableCondition(ctrl.DisableCondition.SubDisableConditions, addedControlNames, ref disableConditionClass);

                    advancedDisableConditionData = Newtonsoft.Json.JsonConvert.SerializeObject(advanceCond);
                }
                else
                {
                    // For non-nested display condition, use regular logic as of 2020-02-17

                    if (ctrl.DisableCondition.IsOr)
                    {
                        foreach (var c in ctrl.DisableCondition.Conditions)
                        {
                            if (!addedControlNames.Contains(c.ControlName))
                            {
                                disableConditionClass += c.ControlName + " ";
                                addedControlNames.Add(c.ControlName);
                            }

                            if (c.IsNotEquals)
                            {
                                disableConditionData.Add(c.ControlName + "!::" + c.ControlAnswer);
                            }
                            else
                            {
                                disableConditionData.Add(c.ControlName + "::" + c.ControlAnswer);
                            }

                        }
                    }
                    else
                    {
                        var disableConditionStr = "";
                        foreach (var c in ctrl.DisableCondition.Conditions)
                        {
                            if (!addedControlNames.Contains(c.ControlName))
                            {
                                disableConditionClass += c.ControlName + " ";
                                addedControlNames.Add(c.ControlName);
                            }

                            if (c.IsNotEquals)
                            {
                                disableConditionStr += c.ControlName + "!::" + c.ControlAnswer + ",";
                            }
                            else
                            {
                                disableConditionStr += c.ControlName + "::" + c.ControlAnswer + ",";
                            }

                        }
                        disableConditionData.Add(disableConditionStr.Trim(new char[] { ',' }));
                    }
                    
                }

                
            }

            return new DisableConditionAttribute
            {
                Style = disableConditionStyle,
                Class = disableConditionClass,
                Data = !string.IsNullOrEmpty(advancedDisableConditionData) ? advancedDisableConditionData : Newtonsoft.Json.JsonConvert.SerializeObject(disableConditionData)
            };
        }
    }
    #endregion
}
