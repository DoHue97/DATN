using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.IO;
namespace BookStore2019.Help
{
    public class Helper
    {
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            string lowerStr = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();
            return Regex.Replace(lowerStr, "[^a-zA-Z0-9_.-]", "-");
        }
        public static string convertLower(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            string lowerStr = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();
            return lowerStr;
        }
        public static string EncodeSHA1(string pass)
        {

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);

            bs = sha1.ComputeHash(bs);

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)

            {

                s.Append(b.ToString("x1").ToLower());

            }

            pass = s.ToString();

            return pass;

        }
        public static string CutText(string text)
        {
            if (text.Length > 30)
            {
                text = text.Substring(0, 29) + "...";
                return text;
            }
            return text;
        }
        
        //public static MvcHtmlString DropDownListDefault(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object defaultValue, string defaultText, object htmAttribute = null)
        //{
        //    List<SelectListItem> list = new List<SelectListItem>(selectList);
        //    SelectListItem item = new SelectListItem();
        //    item.Text = defaultText;
        //    item.Value = defaultValue.ToString();
        //    list.Insert(0, item);
        //    return htmlHelper.DropDownList(name, list, htmAttribute);
        //}
    }
}