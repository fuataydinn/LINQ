using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqIntro
{
    public partial class Form1 : Form
    {
        //LINQ
        //Language Integrated Query
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var names = new List<string>()
            {
                "Tsubasa", "Misaki","Hyuga","Misugi","Wakashimuza","Tsubasa"
            };
            //Linq Syntax
            //Linq sentaksı, SQL sorgularına benzer bir sözdizimiyle koleksiyonları (IEnumerable)
            //sorgulamaya yarayan bir kodlama biçimidir.
            var tsubasaNames = from n in names
                          where n == "Tsubasa"
                          select n;

            //Yukarıdaki LINQ sorgusu asagıdaki işlemi yapıyor diyebiliriz
            var searchedNames = new List<string>();

            //Linq ile dönen sorgu sonucu asla null olmaz 
            //Eger sonuc donmuyorsa bu icerdigi eleman sayısı 0 olan bir koleksiyon anlamına gelir 

            var tsubasa = tsubasaNames.FirstOrDefault();

            var queryResult01 = (from n in names
                                 where n == "Tsubasa"
                                 select n).FirstOrDefault();

            var tsubasaNamesCount = (from n in names
                                     where n == "Tsubasa"
                                     select n).Count();

            //orderby belirtilmediginde sıralamayı artan (ascending) yapar
            var orderNames = from n in names
                             orderby n descending
                             select n;

            //İçinde a harfi olan verileri bulma
            var filtretedAndOrderedNames = from n in names
                                           where n.Contains('a')
                                           orderby n
                                           select n;

            //var filtretedAndOrderedList = new List<string>();
            //foreach (var n in names)
            //{
            //    if (n.Contains('a'))
            //    {
            //        filtretedAndOrderedList.Add(n);
            //    }
            //}

            // M harfi ile baslayanlar degerlerin koleksiyonları gelir
            var queryResult02 = from n in names
                                where n.StartsWith('M')

            //select komudunun yanında ne yazıyorsa, linq sorgu sonucundaki koleksiyonun tipi ona göre gelirlenir
                                select 100;

            var queryResult02List = new List<int>()
            {
                100, //Misaki
                100 //Misugi
            };



        }
    }
}
