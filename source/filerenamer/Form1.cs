using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace filerenamer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Location = Properties.Settings.Default.WindowLocation;
            this.Size = Properties.Settings.Default.WindowSize;
            this.StartPosition = FormStartPosition.Manual;
            numericUpDown1.Value = Properties.Settings.Default.numericUpDown1;
            checkBox1.Checked = Properties.Settings.Default.checkBox1;
            radioButton1.Checked = Properties.Settings.Default.radioButton1;
            radioButton2.Checked = Properties.Settings.Default.radioButton2;
        }
        private static string[] files;
        private void button1_Click(object sender, EventArgs e)
        {
            int nu = 0;
            if (radioButton2.Checked)
            {
                nu = 9999;
            }
            foreach (string item in listBox1.Items)
            {   
                if (File.Exists(item))
                {
                    nu++;
                    string ext = Path.GetExtension(item);
                    string pathwhitoutname = "";
                    pathwhitoutname = Path.GetDirectoryName(item);
                    //System.Diagnostics.Debug.WriteLine(pathwhitoutname+"\\"+nu+ext);
                    string nup = nu.ToString();
                    if (checkBox1.Checked)
                    {
                        nup = nup.PadLeft((int)numericUpDown1.Value, '0');
                    }
                    File.Move(item, pathwhitoutname + "\\" + nup + ext);
                }
            }
            listBox1.Items.Clear();
        }
        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
                e.Effect = DragDropEffects.All;
        }
        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
            {
                listBox1.Items.Add(file);
            }
        }   
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.WindowLocation = this.Location;
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.WindowSize = this.Size;
            }
            else
            {
                Properties.Settings.Default.WindowSize = this.RestoreBounds.Size;
            }
            Properties.Settings.Default.numericUpDown1 = (int)numericUpDown1.Value;
            Properties.Settings.Default.checkBox1 = checkBox1.Checked;
            Properties.Settings.Default.radioButton1 = radioButton1.Checked;
            Properties.Settings.Default.radioButton2 = radioButton2.Checked;
            Properties.Settings.Default.Save();
        }
    }
}