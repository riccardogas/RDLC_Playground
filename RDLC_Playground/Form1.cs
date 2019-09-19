using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace RDLC_Playground
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            this.Controls.Add(reportViewer1);
            reportViewer1.Visible = true;
            reportViewer1.ShowToolBar = true;
            this.Refresh();

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.reportViewer1.Location = new System.Drawing.Point(0,0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = this.Size;
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);

            this.Controls.Add(this.reportViewer1);

            reportViewer1.ProcessingMode = ProcessingMode.Local;

            using (var fstream = new FileStream("c:\\tmp\\prova.rdlc", FileMode.Open))
            {
                reportViewer1.LocalReport.LoadReportDefinition(fstream);
                reportViewer1.LocalReport.Refresh();
                IEnumerable<string> dsNames = reportViewer1.LocalReport.GetDataSourceNames();

                XmlDocument doc = new XmlDocument();
                doc.Load(fstream);
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/Report/DataSources/DataSource/ConnectionProperties");


                foreach (XmlNode node in nodes)
                {
                    MessageBox.Show(node.SelectSingleNode("DataProvider").InnerText);
                }



                foreach (string dsName in dsNames)
                {
                    MessageBox.Show(dsName);
                }
            }

            

        }

        private void Form1_Leave(object sender, EventArgs e)
        {

        }
    }
}
