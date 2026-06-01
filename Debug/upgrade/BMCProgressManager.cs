using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Debug.upgrade
{
    public class BMCProgressManager
    {
        private DataGridView dataGridView;
        private Dictionary<string, BMCProgressInfo> progressDict = new Dictionary<string, BMCProgressInfo>();

        public BMCProgressManager(DataGridView dgv)
        {
            dataGridView = dgv;
            InitDataGridView();
        }

        private void InitDataGridView()
        {
            if (dataGridView.InvokeRequired)
            {
                dataGridView.Invoke(new Action(InitDataGridView));
                return;
            }
            // 设置 Dock 填充整个父容器
            //dataGridView.Dock = DockStyle.Fill;
            dataGridView.Columns.Clear();
            dataGridView.Columns.Add("IP", "IP地址");
            dataGridView.Columns.Add("Progress", "进度");
            dataGridView.Columns.Add("Stage", "阶段");
            dataGridView.Columns.Add("UpdateTime", "更新时间");

            // 关键设置：让列自动填充整个控件宽度
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 设置各列的填充权重（比例）
            dataGridView.Columns["IP"].FillWeight = 30;      // 30%
            dataGridView.Columns["Progress"].FillWeight = 20; // 20%
            dataGridView.Columns["Stage"].FillWeight = 25;    // 25%
            dataGridView.Columns["UpdateTime"].FillWeight = 25; // 25%

            // IP列启用排序
            dataGridView.Columns["IP"].SortMode = DataGridViewColumnSortMode.Automatic;

            // 禁用选择和编辑
            dataGridView.ReadOnly = true;                    // 只读，不可编辑
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ClearSelection();                   // 清除默认选中

            // 禁用选中高亮效果
            dataGridView.DefaultCellStyle.SelectionBackColor = dataGridView.DefaultCellStyle.BackColor;
            dataGridView.DefaultCellStyle.SelectionForeColor = dataGridView.DefaultCellStyle.ForeColor;

            // 其他设置
            dataGridView.AllowUserToAddRows = false;
            dataGridView.RowHeadersVisible = false;

            // 设置进度列样式
            dataGridView.Columns["Progress"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
    }
}
