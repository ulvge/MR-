using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug
{
    static class SelectPdf
    {
        public static bool FindPdfByKeyName(string name, out string resName)
        {
            string srcPath = @"300\";
            resName = string.Empty;
            string[] files = Directory.GetFiles(srcPath, "*.*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                if (file.Contains(name))
                {
                    resName = file;
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// PDF转png
        /// </summary>
        /// <param name="pdfPath"></param>
        /// <param name="pngPath"></param>
        /// <returns></returns>
        public static bool Pdf2Jpg(string pdfPath, string pngPath)
        {
            try
            {
                string gsPath = @"D:\Program Files\gs\gs10.01.2\bin";
                //方法传入的参数  name   是文件名地址 下面会用到。 
                StringBuilder str = new StringBuilder();
                //引用CMD的东西
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();//启动CMD程序
                p.StandardInput.WriteLine("path " + gsPath);    //定位到安装目录下
                p.StandardInput.AutoFlush = true;
                str.AppendFormat(@"gswin64.exe -r600*600 -q -dNOPAUSE -sDEVICE=pngalpha -sOutputFile={0} {1} -c quit", pngPath, pdfPath);
                p.StandardInput.WriteLine(str);//向CMD输出 组装后的str
                p.StandardInput.WriteLine("exit");//结束 不然可能会假死
                string output = p.StandardOutput.ReadToEnd();//读取CMD的返回结果
                Console.WriteLine("CMD界面返回结果\n" + output + "\n");
                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
                return true;
            }
            catch (Exception ex)
            {
                //日志记录异常
                return false;
            }
        }
        public static void CopyRenameFile(string srcPath, string destPath)
        {
            try
            {
                File.Copy(srcPath, destPath, true);
                //File.Move(destPath, @"70\" + newName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void Handler()
        {
            StringBuilder ngNames = new StringBuilder();
            string queryedFileName;
            string destDir = @"75";
            try
            {
                string[] allNames = File.ReadAllLines(@"all.txt");
                Console.WriteLine("allNames" + allNames.Length);
                for (int i = 60; i < allNames.Length; i++)
                {
                    if (allNames[i].Length < 5)
                    {
                        continue;
                    }
                    string[] item = allNames[i].Split('\t');
                    if (item.Length != 2)
                    {
                        ngNames.Append(allNames[i]);
                    }
                    if (FindPdfByKeyName(item[1], out queryedFileName))
                    {
                        string subFixed = queryedFileName.Substring(queryedFileName.IndexOf('.'));

                        if (subFixed.ToLower().Contains("pdf"))//convert pdf to jpg
                        {
                            Pdf2Jpg(queryedFileName, destDir + "\\" + item[0] + item[1] + ".jpg");
                        }
                        else // copy jpg
                        {
                            if (!Directory.Exists(destDir))
                            {
                                Directory.CreateDirectory(destDir);
                            }
                            File.Copy(queryedFileName, destDir + "\\" + item[0] + item[1] + subFixed, true);//300\427湘ADV3226.jpg
                        }

                    }
                    Console.WriteLine("finished :" + i);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
