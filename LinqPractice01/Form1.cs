using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqPractice01
{
    public partial class Form1 : Form
    {
        //readOnly sadece okunabilir o field degistirilemez

        private readonly List<string> _names = new List<string>
        {
            "Fuat Aydın",
            "Murat Odabaş",
            "Selman Bilgen",
            "Ayşe Yıldız",
            "Buse Varol",
            "Muhammed Koyun",
            "Tuba Yılmaz "
        };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lstNames.DataSource = _names;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var filteredName = (from n in _names
                               where n.Contains(txtSearchText.Text)                              
                               select n).ToList();

            lstNames.DataSource = filteredName;
        }

        private void txtSearchText_TextChanged(object sender, EventArgs e)
        {
            //goruntulerken hem degerleri kucuk yapıp, hemde atama kısmında kucuk yaprak buyuk kucuk duyarlılıgı kaldırdık
            //Ana veri degismedi !! 
            var filteredName = (from n in _names
                                where n.ToLowerInvariant().Contains(txtSearchText.Text.ToLowerInvariant())
                                //where n.Contains(txtSearchText.Text,StringComparison.InvariantCultureIgnoreCase) //kodun 2. yolu
                                select n).ToList();

            lstNames.DataSource = filteredName;
        }
    }
}
