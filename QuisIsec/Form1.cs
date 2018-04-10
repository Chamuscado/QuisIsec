using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using lib;

namespace QuisIsec
{
    public partial class Form1 : Form
    {
        private List<Category> _categorys = new List<Category>();

        public Form1()
        {
            InitializeComponent();

            if (startDialog(out var nameFiles))
            {
                foreach (var nameFile in nameFiles)
                {
                    var file = new CsvFile(nameFile);
                    if (!file.FileExists() || !file.Open())
                        continue;

                    while (file.HasNextLine())
                    {
                        var line = file.GetNextLine();
                        var cat = line[0];
                        line.RemoveAt(0);
                        var quest = line[0];
                        line.RemoveAt(0);
                        var i = 0;
                        for (; i < _categorys.Count; i++)
                        {
                            if (string.Compare(_categorys[i].Name, cat, StringComparison.Ordinal) == 0)
                                break;
                        }

                        if (i >= _categorys.Count)
                        {
                            _categorys.Add(new Category(cat));
                            
                        }

                        _categorys[i].AddQuestion(new Question(quest, line));
                    }
                    _categorys.Sort();
                    file.Close();
                }

                Thread.Sleep(10000);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameFiles"></param>
        /// <returns>false -> não há ficheiros</returns>
        private bool startDialog(out string[] nameFiles)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = @"D:\C#\QuisIsec\QuisIsec\bin\Debug", //@"C:\",
                Filter = "CSVFile(*.csv)|*.csv",
                Multiselect = true
            };
            var @return = dialog.ShowDialog() == DialogResult.OK;
            nameFiles = dialog.FileNames;
            return @return;
        }
    }
}