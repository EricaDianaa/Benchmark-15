using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Benchmark_15.Models
{
    public class Camere
    {
        public int IdCamera { get; set; }
        public string Descrizione { get; set; }
        public string Tipologia { get; set; }
        public int IdPrenotaz { get; set; }

        public string Anno { get; set; }

        public static List<Camere> ListCamere = new List<Camere>();
        public static void Select()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
          .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("select  * from Camere ", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();
           
            while (sqlreader1.Read())
            {
                  Camere c = new Camere();
                c.IdCamera = Convert.ToInt16(sqlreader1["IdCamera"]);
                c.Descrizione = sqlreader1["Descrizione"].ToString();
                c.Tipologia = sqlreader1["Tipologia"].ToString();
                ListCamere.Add(c);


            }


            conn.Close();
        }
        public static void Insert(Camere c, string messaggio)
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
           .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Camere VALUES(@Descrizione,@Tipologia)";
                cmd.Parameters.AddWithValue("Descrizione", c.Descrizione);
                cmd.Parameters.AddWithValue("Tipologia", c.Tipologia);

                int inserimentoEffettuato = cmd.ExecuteNonQuery();

                if (inserimentoEffettuato > 0)
                {
                    messaggio = "Inserimento effetuato con successo";
                }

            }
            catch (Exception ex)
            {
                messaggio = $"{ex}";
            }
            finally { conn.Close(); }
        }

        public static void Modifica(Camere c)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
            SqlConnection conn2 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn2;
            cmd.CommandText = "UPDATE Camere SET Descrizione=@Descrizione,Tipologia=@Tipologia  where IdCamera=@id";

            cmd.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["Id"]);
            cmd.Parameters.AddWithValue("Descrizione", c.Descrizione);
            cmd.Parameters.AddWithValue("Tipologia", c.Tipologia);

            conn2.Open();

            cmd.ExecuteNonQuery();

            conn2.Close();
        }
    }
}