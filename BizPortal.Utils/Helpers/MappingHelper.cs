using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BizPortal.Utils
{
    public static class MappingHelper
    {
        public static J Convert<T, J>(T src, J dest, Dictionary<string, string> mapper, Mappingirection direction = Mappingirection.LeftToRight)
        {
            var srcJObj = JObject.FromObject(src);
            var destJObj = JObject.FromObject(dest);
            var dictArrayRegx = new Regex(@"\{(.*?)\}");

            foreach (var map in mapper)
            {
                var mapping = map;

                if (direction == Mappingirection.RightToLeft)
                {
                    mapping = new KeyValuePair<string, string>(map.Value.ToString(), map.Key.ToString());
                }

                var srcMapping = mapping.Key.ToString();
                var destMapping = mapping.Value.ToString();

                if (dictArrayRegx.IsMatch(destMapping))
                {
                    // list to dict data
                    destJObj = ObjectToDictData(srcJObj, destJObj, mapping);
                }
                else if (dictArrayRegx.IsMatch(srcMapping))
                {
                    // dict array to data
                    destJObj = DictDataToObject(srcJObj, destJObj, mapping);

                }
                else if (srcMapping.Contains("[*]") && destMapping.Contains("[*]"))
                {
                    // list to list
                }
                else
                {
                    // single value maping
                    var srcToken = srcJObj.SelectToken(srcMapping);
                    var destToken = destJObj.SelectToken(destMapping);

                    if (destToken != null)
                    {
                        destToken.Replace(srcToken);
                    }
                    else
                    {
                        destJObj.Add(destMapping, srcToken);
                    }
                }
            }

            return destJObj.ToObject<J>();
        }

        public static JObject DictDataToObject(JObject src, JObject dest, KeyValuePair<string, string> mapper)
        {
            var dictDataRegx = new Regex(@"\{(.*?)\}.");
            var dictArrayKeyRegx = new Regex(@"_0$");
            var dictArrayTotalRegx = new Regex(@"_TOTAL$");
            var listRegx = new Regex(@"\[(.*?)\].");

            var srcMapping = dictDataRegx.Split(mapper.Key.ToString()).First();
            var srcNameMapping = dictDataRegx.Match(mapper.Key.ToString()).Value.Replace("{", "").Replace("}", "").Replace(".", "");
            var srcPropMapping = dictDataRegx.Split(mapper.Key.ToString()).Last();


            var srcData = src.SelectToken(string.Join(".", srcMapping, srcNameMapping));

            if (srcData != null)
            {
                //var groupName = srcData.SelectToken("GroupName");
                //var GroupDescription = srcData.SelectToken("GroupDescription");
                //var visible = srcData.SelectToken("Visible");
                var data = srcData.SelectToken("Data");

                if (data != null)
                {
                    if (data.Any(e => dictArrayTotalRegx.IsMatch(e.Path)))
                    {
                        // dict array
                        var destMapping = listRegx.Split(mapper.Value.ToString()).First();
                        var destPropMapping = listRegx.Split(mapper.Value.ToString()).Last();
                        var total = (int)src.SelectToken(data.Where(e => dictArrayTotalRegx.IsMatch(e.Path)).Select(e => e.Path).FirstOrDefault());

                        for (int i = 0; i < total; i++)
                        {
                            var destData = dest.SelectToken(destMapping);
                            var desKey = destPropMapping;
                            var destValue = data.SelectToken(srcPropMapping + "_" + i);
                            var destObject = new JObject { { desKey, destValue } };

                            if (!destData.HasValues)
                            {
                                destData.Replace(new JArray(destObject));
                            }
                            else
                            {
                                var childData = destData.SelectToken("[" + i + "]");

                                if (childData == null)
                                {
                                    destData.Last().AddAfterSelf(destObject);
                                }
                                else
                                {
                                    var childProp = childData.SelectToken(destPropMapping);

                                    if (childProp == null)
                                    {
                                        childData.Replace(UpdateJObjectProperty(childData, desKey, destValue));
                                    }
                                    else
                                    {
                                        childProp.Replace(destObject);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // dict object
                        var destPropMapping = mapper.Value.ToString().Split('.').Last();
                        var destMapping = mapper.Value.ToString().Replace("." + destPropMapping, "");
                        var destData = dest.SelectToken(destMapping);
                        var desKey = destPropMapping;
                        var destValue = data.SelectToken(srcPropMapping);
                        var destObject = new JObject { { desKey, destValue } };

                        if (destData != null)
                        {
                            destData.Replace(destValue);
                        }
                        else
                        {
                            destData = UpdateJObjectProperty(dest, desKey, destValue);
                        }
                    }
                }
            }

            return dest;
        }

        public static JObject ObjectToDictData(JObject src, JObject dest, KeyValuePair<string, string> mapper)
        {
            var dictDataRegx = new Regex(@"\{(.*?)\}.");
            var dictArrayKeyRegx = new Regex(@"_0$");
            var dictArrayTotalRegx = new Regex(@"_TOTAL$");
            var listRegx = new Regex(@"\[(.*?)\].");

            var srcMapping = listRegx.Split(mapper.Key.ToString()).First();
            var srcPropMapping = listRegx.Split(mapper.Key.ToString()).Last();
            var destMapping = dictDataRegx.Split(mapper.Value.ToString()).First();
            var destPropMapping = dictDataRegx.Split(mapper.Value.ToString()).Last();
            var destNameMapping = dictDataRegx.Match(mapper.Value.ToString()).Value.Replace("{", "").Replace("}", "").Replace(".", "");

            var srcData = src.SelectToken(srcMapping);
            var total = srcData != null ? srcData.Count() : 0;

            var groupName = srcData.SelectToken("GroupName") != null ? srcData.SelectToken("GroupName") : destNameMapping;
            var groupDescription = srcData.SelectToken("GroupDescription") != null ? srcData.SelectToken("GroupDescription") : "";
            var visible = srcData.SelectToken("Visible") != null ? srcData.SelectToken("GroupName") : true;

            // set dict detail
            var destDetail = dest.SelectToken(string.Join(".", destMapping, destNameMapping));

            if (destDetail != null)
            {
                var destGroupName = destDetail.SelectToken("GroupName");
                var destGroupDescription = destDetail.SelectToken("GroupDescription");
                var destVisible = destDetail.SelectToken("Visible");

                destGroupName.Replace(groupName);
                destGroupDescription.Replace(groupDescription);
                destVisible.Replace(visible);
            }
            else
            {
                var groupData = new JObject { { destNameMapping, new JObject { { "GroupName", groupName }, { "GroupDescription", groupDescription }, { "Visible", visible }, { "Data", new JObject { } } } } };
                destDetail = dest.SelectToken("Data");

                if (destDetail != null)
                {
                    if (!destDetail.HasValues)
                    {
                        destDetail.Replace(groupData);
                    }
                    else
                    {
                        destDetail.Replace(UpdateJObjectProperty(destDetail, "Data", groupData));
                    }
                }
                else
                {
                    dest.Replace(new JObject { { "Data", groupData } });
                }
            }

            var destData = dest.SelectToken(string.Join(".", destMapping, destNameMapping, "Data"));

            if (total > 0)
            {
                // list to dict array
                var destDicArrayTotal = destData.SelectToken(destNameMapping + "_TOTAL");

                if (destDicArrayTotal == null)
                {
                    destData.Replace(UpdateJObjectProperty(destData, destNameMapping + "_TOTAL", total.ToString()));
                }
                else
                {
                    destDicArrayTotal.Replace(total.ToString());
                }

                for (int i = 0; i < total; i++)
                {
                    // set dict array index
                    var destDictArrayIndex = destData.SelectToken("ARR_IDX_" + i);
                    if (destDictArrayIndex == null)
                    {
                        destData = dest.SelectToken(string.Join(".", destMapping, destNameMapping, "Data"));
                        destData.Replace(UpdateJObjectProperty(destData, "ARR_IDX_" + i, (i + 1).ToString()));
                    }
                    else
                    {
                        destDictArrayIndex.Replace((i + 1).ToString());
                    }

                    // set dict array property
                    var srcPropValue = srcData.SelectToken("[" + i + "]." + srcPropMapping);
                    var destDictArrayProp = destData.SelectToken(destPropMapping + "_" + i);

                    if (destDictArrayProp == null)
                    {
                        destData = dest.SelectToken(string.Join(".", destMapping, destNameMapping, "Data"));
                        destData.Replace(UpdateJObjectProperty(destData, destPropMapping + "_" + i, srcPropValue));
                    }
                    else
                    {
                        destDictArrayProp.Replace(srcPropValue);
                    }
                }
            }
            else
            {
                // list to dict object
                var srcPropValue = srcData;
                var destDictProp = destData.SelectToken(destPropMapping);

                if (destDictProp == null)
                {
                    destData = dest.SelectToken(string.Join(".", destMapping, destNameMapping, "Data"));
                    destData.Replace(UpdateJObjectProperty(destData, destPropMapping, srcPropValue));
                }
                else
                {
                    destDictProp.Replace(srcPropValue);
                }
            }

            return dest;
        }

        public static JObject UpdateJObjectProperty(JToken src, string key, JToken value)
        {
            var propValue = src.Value<JObject>();
            propValue.Add(key, value);
            return propValue;
        }

        public enum Mappingirection
        {
            LeftToRight,
            RightToLeft
        }
    }

}
