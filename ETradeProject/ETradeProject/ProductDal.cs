using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeProject
{
    //Veri Tabanı Veri Ekleme Silme Güncelleme İşlemlerinin Tamamını Yapacağımız Sınıftır.
    public class ProductDal
    {
        //Bağlantı nesnesi oluştur. Herhangi bir fonksiyon içinde tanımlanmadığından adlandırma _ ile başlar.Bütün fonksiyonlarda kullanılacağından globalde tanımladık.
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\mssqllocaldb; initial catalog=ETrade; integrated security=true");
        public List<Product> GetAll()
        {
            ConnectionControl();
            //Sorgu yaz connectiona gönder
            SqlCommand command = new SqlCommand(@"Select * from Products", _connection);
            SqlDataReader reader = command.ExecuteReader();
            //DataTable memory açısından pahalı bir nesnedir. Günümüzde kullanılmaz. Oyüzden bir list döndereceğiz.

            List<Product> list = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                    StockAmount = Convert.ToInt32(reader["StockAmount"])


                };

                list.Add(product);
            }


            reader.Close();
            _connection.Close();
            return list;

        }

        private void ConnectionControl()
        {
            //Bağlantı kapalı ise aç
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void Add(Product product)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("insert into Products values(@name,@unitPrice,@stockAmount)",_connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.ExecuteNonQuery();

            _connection.Close();


        }
        public void Update(Product product)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("update Products set Name=@name, UnitPrice=@unitPrice, StockAmount=@stockAmount where Id=@id", _connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.Parameters.AddWithValue("@id", product.Id);
            command.ExecuteNonQuery();

            _connection.Close();


        }

        public void Delete(int id)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("delete from Products where Id=@id", _connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();

            _connection.Close();


        }
    }
}
