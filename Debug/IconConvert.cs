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

        private static int CONST_MIN = 50000;
        private static int CONST_MAX = 50000;
        AreaZone[] g_areaFilter = new AreaZone[]{
            //new AreaZone(112578 - CONST_MIN, 112578+CONST_MAX), // 112578
            //new AreaZone(199839 - CONST_MIN, 199839+CONST_MAX), // 199839
            new AreaZone(193256 - CONST_MIN, 193256+CONST_MAX), // 112578
        };
        private bool isFilter(int destArea) {
            foreach(AreaZone item in g_areaFilter) {
                if((destArea >= item.min) & (destArea <= item.max)) {
                    return true;
                }
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e) {
            //弹出打开图片对话框
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "图片文件|*.jpg;*.jpeg;*.png";

            if(fileDialog.ShowDialog() == DialogResult.OK) {
                //选择图片进行加载
                tb_fileRoot.Text = fileDialog.FileName;
                
                string newName = tb_fileRoot.Text.Substring(0, tb_fileRoot.Text.LastIndexOf(".")) + ".ico";
                //Save(img, newName);
                ConvertImageToIcon(tb_fileRoot.Text, newName, new Size(255, 255));
            }
        }
        private void tb_items_TextChanged(object sender, EventArgs e) {
            
        }

        private void tb_station_TextChanged(object sender, EventArgs e) {
        }

        /// <summary>
        /// MDS 构建 python脚本,全局替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e) {
            
        }
    }
}
