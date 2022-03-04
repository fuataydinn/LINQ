using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqAdvanced
{
    public partial class Form1 : Form
    {

        private readonly List<Category> _categories = new List<Category>()
        {
            new Category(){Id=1,Name="Elektronik"},
            new Category(){Id=2,Name="Beyaz Eşya"},
            new Category(){Id=3,Name="Küçük Ev Aletleri"},
            new Category(){Id=4,Name="Oturma Grubu"},
            new Category(){Id=5,Name="Işıklandırma"}
        };

        private readonly List<Product> _products = new List<Product>()
        {
            new Product(){Id=1,CategoryId=1,Name="Apple iMac",Price=20000},
            new Product(){Id=2,CategoryId=1,Name="Apple iBook Pro",Price=25000},
            new Product(){Id=3,CategoryId=2,Name="Vestel Buzdolabı",Price=12000},
            new Product(){Id=4,CategoryId=2,Name="Bosch Bulaşık Makinesi",Price=9000},
            new Product(){Id=5,CategoryId=2,Name="Arçelik Fırın",Price=5000},
            new Product(){Id=6,CategoryId=3,Name="Narenciye Sıkacağı",Price=500},
            new Product(){Id=7,CategoryId=4,Name="Tekli Koltuk",Price=2500},
            new Product(){Id=8,CategoryId=4,Name="Zigon Sehpa",Price=1000},
            new Product(){Id=9,CategoryId=4,Name="Köşe Koltuk Takımı",Price=15000},
            new Product(){Id=10,CategoryId=5,Name="Avize",Price=600},
        };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var result01 = (from cat in _categories
                            where cat.Id == 3
                            select cat).SingleOrDefault();

            //isminde e harfi gecen verileri getirme
            var result02 = from cat in _categories
                           where cat.Name.Contains('e', StringComparison.OrdinalIgnoreCase)
                           select cat;


            //Id 3 ten buyuk olanların ismini dizi olarak getir
            var result = (from cat in _categories
                          where cat.Id >= 3
                          select cat.Name).ToArray();


            //Adı 'a' harfi ile biten kategorileri "Id-Name" şeklinde olan bir string ifadesi olarak dondurun. Donus tipi liste olsun

            var result2 = (from cat in _categories
                           where cat.Name.EndsWith("a", StringComparison.OrdinalIgnoreCase)
                           select cat.Id + "-" + cat.Name).ToList();


            //group by
            // bu group by ile Liste içerisindeki nesneler kendi icinde gruplanarak prodGroped değişkenine at
            // kendi içinde nesneler gruplandı
            //result3 koleksiyonların, koleksiyonu şeklinde veri tutar (her veri 2 şer 3 er şekilde gruplanarak koleksiyon oldu)
            var result3 = (from prod in _products
                           group prod by prod.CategoryId into prodGrouped
                           select prodGrouped).ToList();
            //result3'un her bir elemanı, neye gore grupladıysak o degeri Anahtar Deger olarak iceren bir alt koleksiyondur
            IGrouping<int?, Product> productGrouping = result3[0];
            var categoryId = productGrouping.Key;
            foreach (var product in productGrouping)
            { }


            var result4 = from product in _products
                          group product by product.CategoryId into productGroup
                          select new ProductCountByCategory()
                          {
                              CategoryId = productGroup.Key,
                              ProductCount = productGroup.Count()
                          };

            //İnner Join
            /*
             *Id
             *Name
             *CategoryId
             *CategoryName
             */
            var joinResult = from prod in _products
                             join cat in _categories on prod.CategoryId equals cat.Id
                             select new
                             {
                                 Id = prod.Id,
                                 Name = prod.Name,
                                 CategoryId = prod.CategoryId,
                                 CategoryName = cat.Name
                             };
            //Left Join
            var leftJoinResult = from prod in _products
                                 join cat in _categories on prod.CategoryId equals cat.Id into tempCategories
                                 from c in tempCategories.DefaultIfEmpty()
                                 select new
                                 {
                                     Id=prod.Id,
                                     Name=prod.Name,
                                     CategoryId= c!=null ? c.Id : default,
                                     CategoryName=c?.Name //category null degılse isim getir
                                 };

                               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result3 = (from prod in _products
                           group prod by prod.CategoryId into prodGrouped
                           select prodGrouped).ToList();

            listBox1.DataSource = result3[1].ToList();
            MessageBox.Show($"Kategori Id :{result3[1].Key}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //2 adet property oldugu icin sınıf olusturmak zorunda kaldık
            /*
             CatId    Count
             1         2
             2         3
             3         1             
             */
            var result4 = from product in _products
                          group product by product.CategoryId into productGroup
                          select new ProductCountByCategory()
                          {
                              CategoryId = productGroup.Key,
                              ProductCount = productGroup.Count()
                          };
            //windows form toList turunde istedigi icin cevirdik bu IEnumerable tipinde normalde
            listBox1.DataSource = result4.ToList();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Yukarıdaki gibi her seferinde sınıf olusturmamak ıcın ANONİM TİP ten yararlandık
            /*
              CatId    Count
              1         2
              2         3
              3         1             
              */
            var result4 = from product in _products
                          group product by product.CategoryId into productGroup
                          select new
                          {
                              CategoryId = productGroup.Key,
                              Count = productGroup.Count()
                          };

            listBox1.DataSource = result4.ToList();
        }

    }
}
