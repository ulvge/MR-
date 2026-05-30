using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug.tools
{
    public class GetRange
    {
        public static List<string> GetIPRange(string inputPageString)
        {
            List<string> IPList = new List<string>();
            IPList.Clear();
            try
            {
                string[] groups = inputPageString.Split(',');
                foreach (string group in groups)
                {
                    int start, end;
                    string[] item = group.Split('-');
                    switch (item.Length)
                    {
                        case 1:
                            start = int.Parse(item[0]);
                            end = start;
                            break;
                        case 2:
                            start = int.Parse(item[0]);
                            end = int.Parse(item[1]);
                            break;
                        default:
                            continue;
                    }
                    for (int i = start; i <= end; i++)
                    {
                        IPList.Add(i.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return IPList;
        }
    }
}
