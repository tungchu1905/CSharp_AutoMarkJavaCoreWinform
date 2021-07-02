using Group3_Project.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Group3_Project
{
    public partial class frmAutoMark : Form
    {
        private string aId;
        public frmAutoMark()
        {
            InitializeComponent();
        }
        public frmAutoMark(string aIdMess) : this()
        {
            aId = aIdMess;
        }

        private void btnInsertTest_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            txtTest.Text = dialog.SelectedPath;
        }

        private void btnInsertClass_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            txtClass.Text = dialog.SelectedPath;
        }
        private string[] getListTest(string QNumber)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine(@"cd \");
            cmd.StandardInput.WriteLine(@"cd /d " + txtTest.Text);
            cmd.StandardInput.WriteLine("cd " + QNumber);
            cmd.StandardInput.WriteLine("dir");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            string txt = cmd.StandardOutput.ReadToEnd();
            string[] list = txt.Split('\n');
            txt = "";
            foreach (string item in list)
            {
                if (item.Contains(".txt") && item.Contains("TestCase"))
                {
                    txt += item.Substring(item.Length-14,13) + "\n";
                }
            }
            return txt.Split('\n');
        }
        private string[] getListQOfStudent(string student, string folder)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine(@"cd \");
            cmd.StandardInput.WriteLine(@"cd /d " + folder);
            cmd.StandardInput.WriteLine("cd " + student);
            cmd.StandardInput.WriteLine("dir");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            string txt = cmd.StandardOutput.ReadToEnd();
            string[] list = txt.Split('\n');
            txt = "";
            foreach (string item in list)
            {
                if (item.Contains("Final"))
                {
                    txt += item.Substring(item.Length - 3, 2) + "\n";
                }
            }
            return txt.Split('\n');
        }
        private bool checkFolder()
        {
            if (txtClass.Text.Length == 0 || txtTest.Text.Length == 0)
            {
                MessageBox.Show("Must not empty");
                txtClass.Focus();
                return false;
            }           
            if (!txtTest.Text.ToLower().Contains("test_case"))
            {
                MessageBox.Show("Wrong file test case!");
                return false;
            }
            if (!Regex.IsMatch(txtClass.Text.Substring(txtClass.Text.Length - 6, 6), @"^SE\d{4}$"))
            {
                MessageBox.Show("Invalid folder class");
                return false;
            }
            return true;
        }

        private void btnAutoMark_Click(object sender, EventArgs e)
        {
            if(!checkFolder())
            {
                return;
            }
            List<string> listStudent = Database.listStudentByClass(txtClass.Text.Substring(txtClass.Text.Length - 6, 6));
            foreach (string st in listStudent)
            {
                string[] listQ = getListQOfStudent(st, txtClass.Text);
                string scoreDetail = "";
                float totalMark = 0;
                for (int i = 0; i < listQ.Length-1; i++)
                {
                    string[] lissTest = getListTest(listQ[i]);
                    float markQ = 0;
                    for (int j = 0; j < lissTest.Length - 1; j++)
                    {
                        string[] listAll = File.ReadAllLines(txtTest.Text + @"\" + listQ[i] + @"\" + lissTest[j]);
                        string[] listInput = new string[listAll.Length];
                        int indexOutput = 0;
                        for (int k = 0; k < listAll.Length; k++)
                        {
                            if (listAll[k].Contains("OUTPUT"))
                            {
                                indexOutput = k;
                            }
                        }
                        for (int k = 0; k < indexOutput; k++)
                        {
                            listInput[k] = listAll[k];
                        }
                        string output = "";
                        for (int k = indexOutput + 1; k < listAll.Length-1; k++)
                        {
                            output += listAll[k];
                        }
                        string markTest = listAll[listAll.Length - 1];
                        float mark = float.Parse(markTest.Substring(markTest.Length-3,3));
                        Process cmd = new Process();
                        cmd.StartInfo.FileName = "cmd.exe";
                        cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.RedirectStandardOutput = true;
                        cmd.StartInfo.CreateNoWindow = true;
                        cmd.StartInfo.UseShellExecute = false;
                        cmd.Start();
                        cmd.StandardInput.WriteLine(@"cd \");
                        cmd.StandardInput.WriteLine(@"cd /d " + txtClass.Text);
                        cmd.StandardInput.WriteLine("cd " + st);
                        cmd.StandardInput.WriteLine(@"cd Final-" + listQ[i]);
                        cmd.StandardInput.WriteLine("cd " + listQ[i] +"1");
                        cmd.StandardInput.WriteLine(@"cd Given\dist");
                        cmd.StandardInput.WriteLine("java -jar "+ listQ[i] + "1"+".jar");
                        foreach (string o in listInput)
                        {
                            cmd.StandardInput.WriteLine(o);
                        }
                        cmd.StandardInput.Flush();
                        cmd.StandardInput.Close();
                        cmd.WaitForExit();
                        string txt = cmd.StandardOutput.ReadToEnd();
                        string[] listop = txt.Split('\n');
                        int a = 0;
                        int b = 0;
                        for (int k = 0; k < listop.Length; k++)
                        {
                            if (listop[k].Contains("OUTPUT"))
                            {
                                a = k;
                            }
                            if (listop[k].EndsWith(">"))
                            {
                                b = k;
                            }
                        }
                        string outputOfSv = "";
                        for (int k = a + 1; k < b; k++)
                        {
                            outputOfSv += listop[k];
                        }
                        string[] listlast = outputOfSv.Split('\r');
                        outputOfSv = "";
                        for (int k = 0; k < listlast.Length; k++)
                        {
                            outputOfSv += listlast[k];
                        }
                        if (outputOfSv == output)
                        {
                            markQ += mark;
                            totalMark += mark;
                        }                       
                    }
                    scoreDetail += listQ[i] + ":" + markQ + ";";
                }
                Database.updateScore(totalMark, scoreDetail, st);
            }
            MessageBox.Show("Complete grading class "+ txtClass.Text.Substring(txtClass.Text.Length - 6, 6));
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void frmAutoMark_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            frmManage frmManage = new frmManage(aId);
            frmManage.ShowDialog();
        }
    }
}
