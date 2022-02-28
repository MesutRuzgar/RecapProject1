using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecapProject1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListCategories();
            ListProduct();
        }

        private void ListProduct()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.ToList();
            }
        }
        private void ListCategories()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                cbxCategory.DataSource = context.Categories.ToList();
                //categori ismini göster
                cbxCategory.DisplayMember = "CategoryName";
                //seçtiğimde o kategorinin idsini al
                cbxCategory.ValueMember = "CategoryId";
                
            }
        }
        private void ListProductByCategory(int categoryId)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.Where(p=>p.CategoryId==categoryId).ToList();
            }
        }
        private void ListProductByName(string key)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                dgwProduct.DataSource = context.Products.Where(p => p.ProductName.ToLower().Contains(key.ToLower())).ToList();
            }
        }


        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try cache içine almamızın sebebi sayfa açıldığında yüklemeye
            //çalıştığında boş olunca hata vermesin diye geçici çözüm olarak aldık
            try
            {
                ListProductByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch 
            {

               
            }
          
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            string key = tbxSearch.Text;
            //arama yeri boşsa
            if (string.IsNullOrEmpty(key))
            {
                ListProduct();
            }
            else
            {
                ListProductByName(key);
            }
            
        }
    }
}
