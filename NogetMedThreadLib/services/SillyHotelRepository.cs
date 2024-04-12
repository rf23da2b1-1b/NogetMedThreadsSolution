using Microsoft.Data.SqlClient;
using NogetMedThreadLib.model;

namespace NogetMedThreadLib.services
{
    public class SillyHotelRepository : ISillyHotelRepository
    {
        private const String SelectByIdSql = "select * from Hotel where Hotel_No = @id";
        public Hotel GetById(int id)
        {
            Thread.Sleep(35);
            SqlConnection connection = new SqlConnection(Secret.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(SelectByIdSql, connection);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Hotel hotel = ReadHotel(reader);
                connection.Close();
                return hotel;
            }

            throw new KeyNotFoundException("Ingen hotel med id=" + id);
        }

        private Hotel ReadHotel(SqlDataReader reader)
        {
            Hotel hotel = new Hotel();

            hotel.HotelNo = reader.GetInt32(0);
            hotel.Name = reader.GetString(1);
            hotel.Address = reader.GetString(2);


            return hotel;
        }
    }
}
