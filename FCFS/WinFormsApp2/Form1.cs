namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Add(guna2TextBox1.Text, guna2NumericUpDown1.Text, guna2NumericUpDown2.Text);
            guna2TextBox1.Clear();
            guna2NumericUpDown1.ResetText();
            guna2NumericUpDown2.ResetText();
        }
        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView2.Rows.Clear();
        }
        int dong = 0;
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dong = guna2DataGridView1.CurrentRow.Index;
            guna2TextBox1.Text = guna2DataGridView1[0, dong].Value.ToString();
            guna2NumericUpDown1.Text = guna2DataGridView1[1, dong].Value.ToString();
            guna2NumericUpDown2.Text = guna2DataGridView1[2, dong].Value.ToString();
        }
        private void CopyDataGridView()
        {
            // Xóa hết dữ liệu cũ trong DataGridView đích (dgvNewGrid)
            // Duyệt qua từng hàng của DataGridView gốc (dgvStudents)
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (!row.IsNewRow)  // Bỏ qua hàng mới (hàng trống)
                {
                    // Tạo một mảng chứa các giá trị của hàng hiện tại
                    object[] rowData = new object[row.Cells.Count];

                    // Lấy giá trị từ từng cột của hàng và lưu vào mảng
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        rowData[i] = row.Cells[i].Value;
                    }
                    // Thêm hàng vào DataGridView mới (dgvNewGrid)
                    guna2DataGridView2.Rows.Add(rowData);
                }
            }
        }
        private void guna2GradientButton3_Click(object sender, EventArgs e)
        {
            // Copy dữ liệu từ DataGridView1 sang DataGridView2
            CopyDataGridView();

            // Khai báo mảng hoặc danh sách cho các giá trị
            int[] WastingTime = new int[guna2DataGridView2.RowCount];  // Mảng để lưu wt
            int[] ArrivalTime = new int[guna2DataGridView2.RowCount];  // Mảng cho at (Arrival Time)
            int[] BurstTime = new int[guna2DataGridView2.RowCount];    // Mảng cho bt (Burst Time)
            int[] FinishTime = new int[guna2DataGridView2.RowCount];
            int[] TurnAroundTime = new int[guna2DataGridView2.RowCount];

            // Vòng lặp for để duyệt qua các hàng của DataGridView
            for (int i = 0; i < guna2DataGridView2.RowCount - 1; i++)
            {
                // Lấy giá trị ArrivalTime từ cột 1 (giả sử cột 1 là at)
                ArrivalTime[i] = Convert.ToInt32(guna2DataGridView2.Rows[i].Cells[1].Value);
                // Lấy giá trị BurstTime từ cột 2 (giả sử cột 2 là bt)
                BurstTime[i] = Convert.ToInt32(guna2DataGridView2.Rows[i].Cells[2].Value);

                if (i == 0)
                {
                    // Hàng đầu tiên, giá trị WastingTime (wt) là 0
                    WastingTime[i] = 0;
                }
                else
                {
                    // Tính giá trị WastingTime (wt) theo công thức
                    WastingTime[i] = Math.Max(0, (ArrivalTime[i - 1] + BurstTime[i - 1] + WastingTime[i - 1]) - ArrivalTime[i]);
                }

                // Tính TurnAroundTime
                TurnAroundTime[i] = WastingTime[i] + BurstTime[i];
                FinishTime[i] = ArrivalTime[i] + TurnAroundTime[i];
                // Cập nhật giá trị WastingTime (wt) vào cột mới
                guna2DataGridView2.Rows[i].Cells[5].Value = WastingTime[i];
                // Cập nhật giá trị TurnAroundTime (tat) vào cột mới
                guna2DataGridView2.Rows[i].Cells[4].Value = TurnAroundTime[i];
                guna2DataGridView2.Rows[i].Cells[3].Value = FinishTime[i];
            }

            // Tính Average Wasting Time và Average Turn Around Time
            double  AverageWastingTime = WastingTime.Sum() / (guna2DataGridView2.RowCount - 1);
            guna2HtmlLabel1.Text = "Average Wasting Time: " + AverageWastingTime;

            double AverageTurnAroundTime = TurnAroundTime.Sum() / (guna2DataGridView2.RowCount - 1);
            guna2HtmlLabel2.Text = "Average Turn Around Time: " + AverageTurnAroundTime;
        }

    }
}
