using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using PdfSharp;
//using PdfSharp.Drawing;
//using PdfSharp.Pdf;
//using PdfSharp.Pdf.Content;
//using PdfSharp.Pdf.Content.Objects;
//using PdfSharp.Pdf.IO;
using Patagames.Pdf;
using Patagames.Pdf.Net;

namespace Debug {
    public partial class IconConvert : Form {
        public IconConvert() {
            InitializeComponent();
        }
        /// <summary>
        /// 将 Image（PNG） 转换成icon并保存到指定目录文件名
        /// </summary>
        /// <param name="image"></param>
        /// <param name="fileRoot"></param>
        /// <returns></returns>
        public static bool Save(Image image, string fileRoot) {
            if(image != null) {
                using(Icon icon = ConvertToIcon(image)) {
                    try {
                        FileStream fs = new FileStream(fileRoot, FileMode.Create, FileAccess.Write);
                        icon.Save(fs);
                        fs.Flush();
                        fs.Close();
                        fs.Dispose();
                        return true;
                    } catch { }
                }
            }
            return false;
        }

        /// <summary>
        /// 转换Image为Icon
        /// </summary>
        /// <param name="image">要转换为图标的Image对象</param>
        /// <param name="nullTonull">当image为null时是否返回null。false则抛空引用异常</param>
        /// <exception cref="ArgumentNullException" />
        public static Icon ConvertToIcon(Image image, bool nullTonull = false) {
            if(image == null) {
                if(nullTonull) { return null; }
                throw new ArgumentNullException("image");
            }

            using(MemoryStream msImg = new MemoryStream()
                                , msIco = new MemoryStream()) {
                image.Save(msImg, ImageFormat.Png);

                using(var bin = new BinaryWriter(msIco)) {
                    //写图标头部
                    bin.Write((short)0);           //0-1保留
                    bin.Write((short)1);           //2-3文件类型。1=图标, 2=光标
                    bin.Write((short)1);           //4-5图像数量（图标可以包含多个图像）

                    bin.Write((byte)image.Width);  //6图标宽度
                    bin.Write((byte)image.Height); //7图标高度
                    bin.Write((byte)0);            //8颜色数（若像素位深>=8，填0。这是显然的，达到8bpp的颜色数最少是256，byte不够表示）
                    bin.Write((byte)0);            //9保留。必须为0
                    bin.Write((short)0);           //10-11调色板
                    bin.Write((short)32);          //12-13位深
                    bin.Write((int)msImg.Length);  //14-17位图数据大小
                    bin.Write(22);                 //18-21位图数据起始字节

                    //写图像数据
                    bin.Write(msImg.ToArray());

                    bin.Flush();
                    bin.Seek(0, SeekOrigin.Begin);
                    return new Icon(msIco);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "请选择要上传的图片";
            ofd.Filter = "JPG图片|*.jpg|PNG图片|*.png|Gif图片|*.gif";
            ofd.CheckFileExists = true;

            ofd.CheckPathExists = true;
            ofd.Multiselect = false;
            if(ofd.ShowDialog() == DialogResult.OK) {
                ConvertImageToIcon(ofd.FileName, @"e:\users\Snipaste_63.ico", new Size(128, 128));
            }
        }
        public static bool ConvertImageToIcon(string origin, string destination, Size iconSize) {
            if(iconSize.Width > 255 || iconSize.Height > 255) {
                return false;
            }
            Image image = new Bitmap(new Bitmap(origin), iconSize); //先读取已有的图片为bitmap，并缩放至设定大小
            MemoryStream bitMapStream = new MemoryStream(); //存原图的内存流
            MemoryStream iconStream = new MemoryStream(); //存图标的内存流
            image.Save(bitMapStream, ImageFormat.Png); //将原图读取为png格式并存入原图内存流
            BinaryWriter iconWriter = new BinaryWriter(iconStream); //新建二进制写入器以写入目标图标内存流
            /**
             * 下面是根据原图信息，进行文件头写入
             */
            iconWriter.Write((short)0);
            iconWriter.Write((short)1);
            iconWriter.Write((short)1);
            iconWriter.Write((byte)image.Width);
            iconWriter.Write((byte)image.Height);
            iconWriter.Write((short)0);
            iconWriter.Write((short)0);
            iconWriter.Write((short)32);
            iconWriter.Write((int)bitMapStream.Length);
            iconWriter.Write(22);
            //写入图像体至目标图标内存流
            iconWriter.Write(bitMapStream.ToArray());
            //保存流，并将流指针定位至头部以Icon对象进行读取输出为文件
            iconWriter.Flush();
            iconWriter.Seek(0, SeekOrigin.Begin);
            Stream iconFileStream = new FileStream(destination, FileMode.Create);
            Icon icon = new Icon(iconStream);
            icon.Save(iconFileStream); //储存图像
            /**
             * 下面开始释放资源
             */
            iconFileStream.Close();
            iconWriter.Close();
            iconStream.Close();
            bitMapStream.Close();
            icon.Dispose();
            image.Dispose();
            return File.Exists(destination);
        }

        private class AreaZone {
            public int min;
            public int max;
            public AreaZone(int min, int max) {
                this.min = min;
                this.max = max;
            }
        }

        private static int CONST_MIN = 1000;
        private static int CONST_MAX = 1000;
        AreaZone[] g_areaFilter = new AreaZone[]{
            new AreaZone(112578 - CONST_MIN, 112578+CONST_MAX), // 112578
            new AreaZone(199839 - CONST_MIN, 199839+CONST_MAX), // 199839
        };
        private bool isFilter(int destArea) {
            foreach(AreaZone item in g_areaFilter) {
                if((destArea >= item.min) & (destArea <= item.max)) {
                    return true;
                }
            }
            return false;
        }
        private void pdfHandler(string name) {
            string path = name.Substring(0, name.LastIndexOf('\\') + 1);
            string[] fileNameExt = name.Substring(name.LastIndexOf('\\') + 1).Split('.');
            string newName = path + fileNameExt[0] + "_"+DateTime.Now.ToString("yyyy_MM_dd-HHmmss") + "." + fileNameExt[1];
            PdfDocument document = PdfDocument.Load(name);
            for(int i = 0; i < document.Pages.Count; i++) {
                PdfPage collection = document.Pages[i];
                Console.WriteLine(collection.Text);
                FS_SIZEF pageSize = document.GetPageSizeByIndex(i);
                //Image image = new Image();
                Console.WriteLine("page: " + i + ", " + collection.PageObjects.Count);
                int removeCount = 0;
                for(int j = collection.PageObjects.Count - 1; j >= 0; j--) {
                    FS_RECTF rec = collection.PageObjects[j].BoundingBox;
                    float area = rec.Height * rec.Width;
                    if(isFilter((int)area)) {
                        removeCount++;
                        Console.WriteLine(string.Format("pages: {0}, RemoveAt Ojbect: {1} , area : {2}", i, j, area));
                        collection.PageObjects.RemoveAt(j);
                    }
                    //Console.WriteLine(string.Format("pages: {0}, Ojbect: {1} , area : {2}", i, j, area));
                }
                if(removeCount != 2) {
                    Console.WriteLine(string.Format("pages: {0} error", removeCount));
                }
                collection.GenerateContent();
            }
            document.WriteBlock += (s, ex) => {
                using(var stream = new FileStream(newName, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
                    stream.Seek(0, SeekOrigin.End);
                    stream.Write(ex.Buffer, 0, ex.Buffer.Length);
                }
            };
            document.Save(Patagames.Pdf.Enums.SaveFlags.NoIncremental | Patagames.Pdf.Enums.SaveFlags.RemoveUnusedObjects);
            document.Dispose();
        }
        public void EnumerateAllPageObjects(PdfPageObjectsCollection collection) {
            foreach(PdfPageObject obj in collection) {
                if(obj is PdfFormObject) {
                    var formObject = obj as PdfFormObject;
                    EnumerateAllPageObjects(formObject.PageObjects);
                } else if(obj is PdfTextObject) {
                    var textObject = obj as PdfTextObject;
                    if(textObject.TextUnicode.Contains("ASPEEDCon")) {
                        //Console.WriteLine(string.Format("Contains feature: {0}", textObject.TextUnicode));
                    }
                    if(textObject.TextUnicode.Contains("dential")) {
                        //Console.WriteLine(string.Format("Contains feature: {0}", textObject.TextUnicode));
                    }
                    Console.WriteLine(string.Format("check feature: {0}", textObject.TextUnicode));
                    // process text object.
                } else if(obj is PdfImageObject) {
                    var imageObject = obj as PdfImageObject;
                    // process image object.
                } else if(obj is PdfPathObject) {
                    var pathObject = obj as PdfPathObject;
                    // process path object.
                } else if(obj is PdfShadingObject) {
                    var shadingObject = obj as PdfShadingObject;
                    // process shading object.
                }
            }
        }
        //private void pdfHandler(string name) {
        //    // Read document into memory for modification
        //    name = @"P:\4proj\1MDS\1ast2500\ast2500v15 - 副本.pdf";
        //    PdfDocument inputDocument = PdfReader.Open(name);
        //    Console.WriteLine(inputDocument.FileSize);

        //    for(int idx = 0; idx < 2; idx++) {
        //        Console.WriteLine(inputDocument.FileSize);
        //        PdfPage page = inputDocument.Pages[idx];
        //        // 获取页面内容

        //        CSequence contents = ContentReader.ReadContent(page);
        //        StringBuilder sb = new StringBuilder();
        //        // 查找要删除的水印内容
        //        for(int i = 0; i < contents.Count; i++) {
        //            StringBuilder tmp = ExtractText((COperator)contents[i]);
        //            if (tmp.ToString().Contains("ASPEEDCondential")) {
        //                Console.WriteLine(tmp.ToString()+ "contents.Count:"+ contents.Count+ "  contents[i]:" + contents[i]);
        //                //newContents.RemoveAt(i);
        //                ExtractText((COperator)contents[i], true);

        //                page.Contents.ReplaceContent(contents);
        //                break;

        //            }
        //            sb.Append(tmp);
        //        }

        //        Console.WriteLine(sb.ToString());
        //    }
        //    inputDocument.Save(name);
        //}
        //private StringBuilder ExtractText(CObject cObject, bool isClear = false) {
        //    StringBuilder textList = new StringBuilder();
        //    if(cObject is COperator) {
        //        var cOperator = cObject as COperator;
        //        if(cOperator.OpCode.Name == OpCodeName.Tj.ToString() || cOperator.OpCode.Name == OpCodeName.TJ.ToString()) {
        //            foreach(var cOperand in cOperator.Operands) {
        //                textList.Append(ExtractText(cOperand, isClear));
        //            }
        //        }
        //    } else if(cObject is CSequence) {
        //        var cSequence = cObject as CSequence;
        //        foreach(var element in cSequence) {
        //            textList.Append(ExtractText(element, isClear));
        //        }
        //    } else if(cObject is CString) {
        //        var cString = cObject as CString;
        //        textList.Append(cString.Value);

        //        if(isClear) {
        //            cString.Value = "A";
        //        }
        //    }
        //    return textList;
        //}
        private void button1_Click(object sender, EventArgs e) {
            //弹出打开图片对话框
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.pdf";

            if(fileDialog.ShowDialog() == DialogResult.OK) {
                //选择图片进行加载
                tb_fileRoot.Text = fileDialog.FileName;
                if(tb_fileRoot.Text.Contains(".pdf")) { // pdf
                    pdfHandler(tb_fileRoot.Text);
                } else { // icon
                    Image img = Image.FromFile(tb_fileRoot.Text);
                    string newName = tb_fileRoot.Text.Substring(0, tb_fileRoot.Text.LastIndexOf(".")) + ".ico";
                    //Save(img, newName);
                    ConvertImageToIcon(tb_fileRoot.Text, newName, new Size(255, 255));
                }
            }
        }
        private string BSSID = string.Empty;
        private void tb_items_TextChanged(object sender, EventArgs e) {
            string oriStr = tb_items.Text;
            Console.WriteLine(oriStr.Length);
            oriStr = oriStr.Trim();
            try {
                while(true) {
                    if(oriStr.Contains("  ")) {
                        oriStr = oriStr.Replace("  ", " ");
                        continue;
                    } else {
                        break;
                    }
                }
                string[] list = oriStr.Split(' ');
                if(list.Length < 10) {
                    return;
                }
                BSSID = list[0];
                string ch = list[5];
                string ESSID = list[10];
                Console.WriteLine(ESSID.Length);
                string dump = string.Format("airodump-ng -w {0} -c {1} --bssid  {2} wlan0mon", ESSID, ch, BSSID);
                Clipboard.SetText(dump);
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private void tb_station_TextChanged(object sender, EventArgs e) {
            string replay = string.Format("aireplay - ng - 0 50 - a {0} - c {1} wlan0mon", BSSID, tb_station.Text);
            Clipboard.SetText(replay);
        }

        /// <summary>
        /// MDS 构建 python脚本,全局替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e) {
            string text = tb_python.Text;
            string[] textSplit = text.Split('\n');
            StringBuilder sb = new StringBuilder();
            foreach(var item in textSplit) {
                string itemVal = item;
                itemVal = itemVal.Replace("0777", "0x777");
                if (itemVal.Trim().StartsWith("print \"")) {
                    string temp;
                    temp = itemVal.Replace("print \"", "print (\"");
                    temp = temp.Replace("\r", ")\r");
                    //temp += ")\r";
                    sb.Append(temp + "\n");
                } else {
                    sb.Append(itemVal + "\n");
                }
            }
            tb_python.Text = sb.ToString();
        }

        private void IconConvert_Load(object sender, EventArgs e) {
            // regedit web https://patagames.com/request-trial/
            // how to use the license
            //Initialize the SDK library with license key
            //You have to call this function before you can call any PDF processing functions.
            PdfCommon.Initialize("EEF6E707-0413E707-04040B50-44464955-4D5F434F-52500D00-756C7667-65403132-362E636F-6D400036-F61625E7-9DDA9455-7D6B9BD6-4901A600-22A0FC7F-389E4B2E-384C583F-5335CCC1-C943CF5F-8A7BE508-8CF71EBF-F4E390D0-A07C94F1-AEE68BDC-0C1F2978-C0F764");

            //Open and load a PDF document from a file.
        }
    }
}
