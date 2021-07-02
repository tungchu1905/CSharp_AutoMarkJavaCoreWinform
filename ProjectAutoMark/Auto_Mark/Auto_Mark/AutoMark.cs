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

namespace Auto_Mark
{
    public partial class AutoMark : Form
    {
        private string aid;
        public AutoMark()
        {
            InitializeComponent();
        }
        public AutoMark(string aidmess) : this()
        {
            aid = aidmess;
        }

        private void AutoMark_Load(object sender, EventArgs e)
        {

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
            // GET THE LIST TESTCASE Q1,Q2,Q3
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
                    // get Q1, Q2
                    txt += item.Substring(item.Length - 14, 13) + "\n";
                }
            }
            return txt.Split('\n');
        }
        private string[] getListQOfStudent(string student, string folder)
        {
            Process cmd = new Process();
            // Get list question of student Q11,Q21,Q31
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
            if (!checkFolder())
            {
                return;
            }

            List<string> listSTBYCL = Database.listStudentByClass(txtClass.Text.Substring(txtClass.Text.Length - 6, 6));
            
            foreach (string st in listSTBYCL)
            {
                string[] listQ = getListQOfStudent(st, txtClass.Text);
                string scoreDetail = "";
                float totalMark = 0;

                for (int i = 0; i < listQ.Length - 1; i++)
                {
                    // call the each Q of student
                    string[] listTest = getListTest(listQ[i]);
                    float markQ = 0;
                    for (int j = 0; j < listTest.Length - 1; j++)
                    {
                        // call the file testcase txt of the Q[i]
                        string[] listAll = File.ReadAllLines(txtTest.Text + @"\" + listQ[i] + @"\" + listTest[j]);
                        // list input of Q[i]
                        string[] listInput = new string[listAll.Length];
                        int indexOutput = 0;
                        for (int k = 0; k < listAll.Length; k++)
                        {
                            // read file .txt , when line contain OUTPUT -> get the result under
                            if (listAll[k].Contains("OUTPUT"))
                            {
                                // get lines under output
                                indexOutput = k;
                            }
                        }
                        for (int k = 0; k < indexOutput; k++)
                        {
                            // get the output of testcase
                            listInput[k] = listAll[k];
                        }
                        string output = "";
                        for (int k = indexOutput + 1; k < listAll.Length - 1; k++)
                        {
                            //get the output of testCase
                            output += listAll[k];
                        }
                        //get THE MARK 
                        string markTest = listAll[listAll.Length - 1];
                        float mark = float.Parse(markTest.Substring(markTest.Length - 3, 3));
                        // CALL THE TESTCASE OF USEREXAM, AND GET THE RESULT UNDER OUTPUT
                        Process cmd = new Process();
                        cmd.StartInfo.FileName = "cmd.exe";
                        cmd.StartInfo.RedirectStandardInput = true;
                        cmd.StartInfo.RedirectStandardOutput = true;
                        cmd.StartInfo.CreateNoWindow = true;
                        cmd.StartInfo.UseShellExecute = false;
                        cmd.Start();
                        cmd.StandardInput.WriteLine(@"cd \");
                        cmd.StandardInput.WriteLine(@"cd /d " + txtClass.Text);
                        // to path of student
                        cmd.StandardInput.WriteLine("cd " + st);
                        // to path of each Q of student
                        cmd.StandardInput.WriteLine(@"cd Final-" + listQ[i]);
                        cmd.StandardInput.WriteLine("cd " + listQ[i] + "1");
                        //path of q_do
                        cmd.StandardInput.WriteLine(@"cd Given\dist");
                        // call the output of Java core with .jar + Q[i] of student
                        cmd.StandardInput.WriteLine("java -jar " + listQ[i] + "1" + ".jar");
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
                        // GET THE OUTPUT OF USEREXAM 
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

                        string outputOfSt = "";
                        for (int k = a + 1; k < b; k++)
                        {
                            // output of student exam
                            outputOfSt += listop[k];
                        }

                        string[] listlast = outputOfSt.Split('\r');
                        outputOfSt = "";
                        for (int k = 0; k < listlast.Length; k++)
                        {
                            outputOfSt += listlast[k];
                        }
                        // compare output of student exam with output of testcase of Q[i]
                        if (outputOfSt == output)
                        {
                            // get mark
                            markQ += mark;
                            totalMark += mark;
                        }

                    }
                    // get the score detail  to insert to database
                    scoreDetail += listQ[i] + ":" + markQ + ";";

                }
                Database.UpdateScore(totalMark, scoreDetail, st);
            }
            MessageBox.Show("Complete grading class " + txtClass.Text.Substring(txtClass.Text.Length - 6, 6));

        }

        private void AutoMark_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Mainform frmManage = new Mainform(aid);
            frmManage.ShowDialog();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }
    }
}
