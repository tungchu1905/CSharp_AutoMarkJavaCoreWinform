using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Auto_Mark
{
    public partial class Mainform : Form
    {
        private string aid;
        public Mainform()
        {
            InitializeComponent();
        }
        public Mainform(string aIdMess) : this()
        {
            aid = aIdMess;
        }
        private void LoadClass()
        {
            DataTable listClass = Database.getAllClassByAccId(aid);
            if (listClass.Rows.Count > 0)
            {
                DataRow dr = listClass.NewRow();
                dr["ClassName"] = "-----All-----";
                listClass.Rows.InsertAt(dr, 0);

                this.cbbClass.DataSource = listClass;
                cbbClass.DisplayMember = "ClassName";
                cbbClass.ValueMember = "ClassName";
                cbbClass.Enabled = true;
            }
        }

        private void LoadDGVStudent()
        {
            dgvStudent.Columns.Clear();
            if (cbbClass.SelectedValue != null)
            {
                if (cbbClass.SelectedIndex == 0)
                {
                    dgvStudent.DataSource = Database.getAllStudent();
                    setHeaderTextDgv();
                }
                else
                {
                    dgvStudent.DataSource = Database.listStudentByClass(cbbClass.SelectedValue.ToString());
                    setHeaderTextDgv();
                }
                addEdit();
            }
            dgvStudent.Enabled = true;
        }
        private void setHeaderTextDgv()
        {
            for (int i = 0; i < dgvStudent.Columns.Count; i++)
            {
                dgvStudent.Columns[i].HeaderText = dgvStudent.Columns[i].HeaderText.ToUpper();
            }
        }
        private void addEdit()
        {
            DataGridViewButtonColumn btnColumnEdit = new DataGridViewButtonColumn();
            btnColumnEdit.Name = "editColumn";
            btnColumnEdit.HeaderText = "EDIT";
            btnColumnEdit.Text = "Edit";
            btnColumnEdit.UseColumnTextForButtonValue = true;
            dgvStudent.Columns.Add(btnColumnEdit);
        }
        private void Mainform_Load(object sender, EventArgs e)
        {
            LoadClass();
            LoadDGVStudent();
            if (dgvStudent.DataSource != null && cbbClass.DataSource != null)
            {
                txtGen.Text = "";
                txtGen.Enabled = false;

            }
            else
            {
                txtGen.Enabled = true;
                cbbClass.Enabled = false;
                dgvStudent.Enabled = false;
                btnAutoMark.Enabled = false;
                btnExport.Enabled = false;
                btnSearch.Enabled = false;
                txtSearch.Enabled = false;
                btnClear.Enabled = false;
            }

        }



        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvStudent.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.ApplicationClass XcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                XcelApp.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dgvStudent.Columns.Count; i++)
                {
                    XcelApp.Cells[1, i] = dgvStudent.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dgvStudent.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvStudent.Columns.Count - 1; j++)
                    {
                        XcelApp.Cells[i + 2, j + 1] = dgvStudent.Rows[i].Cells[j].Value;
                    }
                }
                XcelApp.Columns.AutoFit();
                XcelApp.Visible = true;
            }
        }
        private void txtGen_MouseDown(object sender, MouseEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            txtGen.Text = dialog.SelectedPath;
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtGen.Text.Trim().Length == 0 && txtGen.Enabled == true)
            {
                txtGen.Text = "";
                return;
            }

            Process cmd = new Process();
            // LAY RA DANH SACH LOP 
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine(@"cd \");
            cmd.StandardInput.WriteLine(@"cd /d " + txtGen.Text);
            cmd.StandardInput.WriteLine("dir");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            string txt = cmd.StandardOutput.ReadToEnd();
            string[] list = txt.Split('\n');
            string classId = "";
            txt = "";

            foreach (string item in list)
            {
                if (item.Contains("DIR") && !item.Contains("."))
                {
                    classId = item.Substring(item.Length - 7, 6);
                    Database.InsertClassID(classId, aid);
                    txt += classId + "\n";
                    classId = "";
                }
            }

            string[] listClass = txt.Split('\n');
            txt = "";
            for (int i = 0; i < listClass.Length - 1; i++)
            {
                //LAY RA DANH SACH HOC SINH CUA TUNG CLASS
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
                cmd.StandardInput.WriteLine(@"cd \");
                cmd.StandardInput.WriteLine(@"cd /d " + txtGen.Text + @"\" + listClass[i]);
                cmd.StandardInput.WriteLine("dir");
                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
                cmd.WaitForExit();
                txt = cmd.StandardOutput.ReadToEnd();
                string[] listSt = txt.Split('\n');
                string studentId = "";
                foreach (string item in listSt)
                {
                    if (item.Contains("DIR") && !item.Contains("."))
                    {
                        studentId = item.Substring(item.Length - 9, 8);
                        Database.InsertStudentID(studentId, listClass[i]);
                        //label1.Text += studentId + "\n";
                        studentId = "";
                    }
                }
            }
            txtGen.Text = "";
            txtGen.Enabled = false;
            LoadClass();
            LoadDGVStudent();
            btnAutoMark.Enabled = true;
            btnExport.Enabled = true;
            txtSearch.Enabled = true;
            btnSearch.Enabled = true;
            btnClear.Enabled = true;
            btnGenerate.Enabled = false;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (dgvStudent.Rows.Count > 0)
            {

                DataTable listClass = Database.getAllClassByAccId(aid);
                List<string> listC = new List<string>();
                for (int i = 0; i < listClass.Rows.Count; i++)
                {
                    listC.Add(listClass.Rows[i]["className"].ToString());
                }
                foreach (string cl in listC)
                {
                    Database.DeleteStudent(cl);
                }
                Database.DeleteClass(aid);
                txtGen.Enabled = true;
                cbbClass.DataSource = null;
                cbbClass.Enabled = false;
                dgvStudent.DataSource = null;
                dgvStudent.Enabled = false;
                btnAutoMark.Enabled = false;
                btnExport.Enabled = false;
                btnSearch.Enabled = false;
                txtSearch.Enabled = false;
                btnClear.Enabled = false;
                dgvStudent.Columns.Clear();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length == 0)
            {
                txtSearch.Text = "";
                return;
            }
            dgvStudent.DataSource = Database.search(txtSearch.Text.Trim());
            setHeaderTextDgv();
        }

        private void btnAutoMark_Click(object sender, EventArgs e)
        {
            this.Hide();
            AutoMark autoMark = new AutoMark(aid);
            autoMark.ShowDialog();
            this.Close();
        }
    }
}
