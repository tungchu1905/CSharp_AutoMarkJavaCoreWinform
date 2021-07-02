
namespace Auto_Mark
{
    partial class AutoMark
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInsertClass = new System.Windows.Forms.Button();
            this.txtClass = new System.Windows.Forms.TextBox();
            this.btnManage = new System.Windows.Forms.Button();
            this.btnAutoMark = new System.Windows.Forms.Button();
            this.btnInsertTest = new System.Windows.Forms.Button();
            this.txtTest = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Folder Test Case";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Folder Class";
            // 
            // btnInsertClass
            // 
            this.btnInsertClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertClass.Location = new System.Drawing.Point(349, 79);
            this.btnInsertClass.Name = "btnInsertClass";
            this.btnInsertClass.Size = new System.Drawing.Size(91, 23);
            this.btnInsertClass.TabIndex = 14;
            this.btnInsertClass.Text = "Chose File";
            this.btnInsertClass.UseVisualStyleBackColor = true;
            this.btnInsertClass.Click += new System.EventHandler(this.btnInsertClass_Click);
            // 
            // txtClass
            // 
            this.txtClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClass.Location = new System.Drawing.Point(151, 79);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(192, 23);
            this.txtClass.TabIndex = 13;
            // 
            // btnManage
            // 
            this.btnManage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManage.Location = new System.Drawing.Point(349, 127);
            this.btnManage.Name = "btnManage";
            this.btnManage.Size = new System.Drawing.Size(91, 72);
            this.btnManage.TabIndex = 12;
            this.btnManage.Text = "Manage >>>";
            this.btnManage.UseVisualStyleBackColor = true;
            this.btnManage.Click += new System.EventHandler(this.btnManage_Click);
            // 
            // btnAutoMark
            // 
            this.btnAutoMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoMark.Location = new System.Drawing.Point(151, 127);
            this.btnAutoMark.Name = "btnAutoMark";
            this.btnAutoMark.Size = new System.Drawing.Size(126, 72);
            this.btnAutoMark.TabIndex = 11;
            this.btnAutoMark.Text = "Auto Mark";
            this.btnAutoMark.UseVisualStyleBackColor = true;
            this.btnAutoMark.Click += new System.EventHandler(this.btnAutoMark_Click);
            // 
            // btnInsertTest
            // 
            this.btnInsertTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertTest.Location = new System.Drawing.Point(349, 39);
            this.btnInsertTest.Name = "btnInsertTest";
            this.btnInsertTest.Size = new System.Drawing.Size(91, 23);
            this.btnInsertTest.TabIndex = 10;
            this.btnInsertTest.Text = "Chose File";
            this.btnInsertTest.UseVisualStyleBackColor = true;
            this.btnInsertTest.Click += new System.EventHandler(this.btnInsertTest_Click);
            // 
            // txtTest
            // 
            this.txtTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTest.Location = new System.Drawing.Point(151, 39);
            this.txtTest.Name = "txtTest";
            this.txtTest.Size = new System.Drawing.Size(192, 23);
            this.txtTest.TabIndex = 9;
            // 
            // AutoMark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 247);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnInsertClass);
            this.Controls.Add(this.txtClass);
            this.Controls.Add(this.btnManage);
            this.Controls.Add(this.btnAutoMark);
            this.Controls.Add(this.btnInsertTest);
            this.Controls.Add(this.txtTest);
            this.Name = "AutoMark";
            this.Text = "AutoMark";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AutoMark_FormClosed);
            this.Load += new System.EventHandler(this.AutoMark_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInsertClass;
        private System.Windows.Forms.TextBox txtClass;
        private System.Windows.Forms.Button btnManage;
        private System.Windows.Forms.Button btnAutoMark;
        private System.Windows.Forms.Button btnInsertTest;
        private System.Windows.Forms.TextBox txtTest;
    }
}