using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BizPortal.Utils.Helpers
{
    public static class XMLHelper
    {
        public static string ToThaiDigit(this string input)
        {
            if ((string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)))
            {
                return input;
            }
            return input.Replace('0', '๐')
                   .Replace('1', '๑')
                   .Replace('2', '๒')
                   .Replace('3', '๓')
                   .Replace('4', '๔')
                   .Replace('5', '๕')
                   .Replace('6', '๖')
                   .Replace('7', '๗')
                   .Replace('8', '๘')
                   .Replace('9', '๙');
        }
        public static string ToArabicDigit(this string input)
        {
            if ((string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)))
            {
                return input;
            }
            return input.Replace('๐', '0')
                   .Replace('๑', '1')
                   .Replace('๒', '2')
                   .Replace('๓', '3')
                   .Replace('๔', '4')
                   .Replace('๕', '5')
                   .Replace('๖', '6')
                   .Replace('๗', '7')
                   .Replace('๘', '8')
                   .Replace('๙', '9');
        }

        public static string RemoveTextDistrict(this string input)
        {
            if ((string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)))
            {
                return input;
            }
            input = Regex.Replace(input, "^(เขต)", " ");
            return input;
        }

        public static string ToMonthNameThai(this string input)
        {
            if ((string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)))
            {
                return input;
            }
            var inputs = input.Split('/');
            var monthName = string.Empty;
            
            if (inputs[1] == "01" || inputs[1] == "๐๑")
            {
                monthName = "มกราคม";
            }
            else if(inputs[1] == "02" || inputs[1] == "๐๒")
            {
                monthName = "กุมภาพันธ์";
            }
            else if (inputs[1] == "03" || inputs[1] == "๐๓")
            {
                monthName = "มีนาคม";
            }
            else if (inputs[1] == "04" || inputs[1] == "๐๔")
            {
                monthName = "เมษายน";
            }
            else if (inputs[1] == "05" || inputs[1] == "๐๕")
            {
                monthName = "พฤษภาคม";
            }
            else if (inputs[1] == "06" || inputs[1] == "๐๖")
            {
                monthName = "มิถุนายน";
            }
            else if (inputs[1] == "07" || inputs[1] == "๐๗")
            {
                monthName = "กรกฎาคม";
            }
            else if (inputs[1] == "08" || inputs[1] == "๐๘")
            {
                monthName = "สิงหาคม";
            }
            else if (inputs[1] == "09" || inputs[1] == "๐๙")
            {
                monthName = "กันยายน";
            }
            else if (inputs[1] == "10" || inputs[1] == "๑๐")
            {
                monthName = "ตุลาคม";
            }
            else if (inputs[1] == "11" || inputs[1] == "๑๑")
            {
                monthName = "พฤศจิกายน";
            }
            else if (inputs[1] == "12" || inputs[1] == "๑๒")
            {
                monthName = "ธันวาคม";
            }

            var newInput = inputs[0] + " " + monthName + " " + inputs[2];

            return newInput;
        }
    }
}
