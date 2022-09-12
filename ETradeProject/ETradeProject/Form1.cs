using System;
using System.Windows.Forms;

namespace ETradeProject
{
    public partial class Form1 : Form
    {
        ProductDal _productDal = new ProductDal();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            dgwProducts.DataSource = _productDal.GetAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool control = true;
            string nameAdd = tbxName.Text;


            try
            {
                int stockAMountAdd = Convert.ToInt32(tbxStockAmount.Text);
               
                decimal unitPriceAdd = Convert.ToDecimal(tbxUnitPrice.Text);
                
            }
            catch
            {
                tbxName.Clear();
                tbxNameUpdate.Clear();
                tbxStockAmount.Clear();
                tbxStockAmountUpdate.Clear();
                tbxUnitPrice.Clear();
                tbxUnitPriceUpdate.Clear();
                control = false;
            }
            if (nameAdd == null)
            {
                control = false;
            }

            if (control == true)
            {

                _productDal.Add(new Product
                {
                    Name = tbxName.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                    StockAmount = Convert.ToInt32(tbxStockAmount.Text)
                });
                LoadProducts();
                MessageBox.Show("Product Added Succesfully");
            }
            else
            {
                MessageBox.Show("You Entered a Wrong Type Of Value. Try Again.");
            }
        }

        private void dgwProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxNameUpdate.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            tbxUnitPriceUpdate.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            tbxStockAmountUpdate.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bool control = true;
            string nameUpdate = tbxNameUpdate.Text;
            try
            {

                int stockAMountUpdate = Convert.ToInt32(tbxStockAmountUpdate.Text);
                decimal unitPriceUpdate = Convert.ToDecimal(tbxUnitPriceUpdate.Text);
            }
            catch
            {
                tbxName.Clear();
                tbxNameUpdate.Clear();
                tbxStockAmount.Clear();
                tbxStockAmountUpdate.Clear();
                tbxUnitPrice.Clear();
                tbxUnitPriceUpdate.Clear();
                control = false;


            }
            if (nameUpdate == null)
            {
                control = false;
            }

            if (control == true)
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                    Name = tbxNameUpdate.Text,
                    StockAmount = Convert.ToInt32(tbxStockAmountUpdate.Text),
                    UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text)


                };

                _productDal.Update(product);
                LoadProducts();
            }
            else
            {
                MessageBox.Show("You Entered a Wrong Type Of Value. Try Again.");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            int Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value);

            _productDal.Delete(Id);
            LoadProducts();
        }
        public bool TextBoxValidation()
        {
            bool control = true;
            string nameAdd = tbxName.Text;
            string nameUpdate = tbxNameUpdate.Text;

            try
            {
                int stockAMountAdd = Convert.ToInt32(tbxStockAmount.Text);
                int stockAMountUpdate = Convert.ToInt32(tbxStockAmountUpdate.Text);
                decimal unitPriceAdd = Convert.ToDecimal(tbxUnitPrice.Text);
                decimal unitPriceUpdate = Convert.ToDecimal(tbxUnitPriceUpdate.Text);
            }
            catch
            {
                MessageBox.Show("PLease enter a number");
                tbxStockAmount.Clear();
                tbxStockAmountUpdate.Clear();
                tbxUnitPrice.Clear();
                tbxUnitPriceUpdate.Clear();
                control = false;
                return control;

            }
            //if(nameAdd == null || nameUpdate == null)
            //{
            //    return control = false;
            //}

            return control;
        }
    }
}

