#region #code
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Xml.Linq;
using DevExpress.Xpf.Charts;
//... 

namespace DataBindingExample {
    public class GSP {
        readonly string region;
        readonly string year;
        readonly double product;

        public string Region { get { return region; } }
        public string Year { get { return year; } }
        public double Product { get { return product; } }

        public GSP(string region, string year, double product) {
            this.region = region;
            this.year = year;
            this.product = product;
        }
    }

    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();
            if (diagram != null) {
                List<GSP> dataSource = CreateDataSource();
                foreach (Series series in diagram.Series)
                    series.DataSource = dataSource;
            }
        }
        List<GSP> CreateDataSource() {
            XDocument document = XDocument.Load("GSP.xml");
            List<GSP> dataSource = new List<GSP>();
            if (document != null) {
                foreach (XElement element in document.Element("GSPs").Elements()) {
                    string region = element.Element("Region").Value;
                    string year = element.Element("Year").Value;
                    double product = Convert.ToDouble(element.Element("Product").Value, CultureInfo.InvariantCulture);
                    dataSource.Add(new GSP(region, year, product));
                }
            }
            return dataSource;
        } 
    }
}
#endregion #code
