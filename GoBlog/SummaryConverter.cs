using System;
using System.Linq;

namespace GoBlog
{
    public class SummaryConverter
    {
        public static string Convert(string content)
        {
            return content.Split(new[] { Environment.NewLine }, StringSplitOptions.None).First();
        }
    }
}