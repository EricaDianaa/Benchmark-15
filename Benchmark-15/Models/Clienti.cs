using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace Benchmark_15.Models
{
    public class Clienti
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CodiceFiscale { get; set; }
        public string Città { get; set; }
        public string Provincia { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Cellulare { get; set; }

        public static List<Clienti> ListClienti = new List<Clienti>();
        public static void Select()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
          .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("select  * from Clienti ", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();
           
            while (sqlreader1.Read())
            {
                Clienti c = new Clienti();
                c.IdCliente = Convert.ToInt16(sqlreader1["IdCliente"]);
                c.Nome = sqlreader1["Nome"].ToString();
                c.Cognome = sqlreader1["Cognome"].ToString();
                c.CodiceFiscale = sqlreader1["CodiceFiscale"].ToString();
                c.Città = sqlreader1["Città"].ToString();
                c.Provincia = sqlreader1["Provincia"].ToString();
                c.Email = sqlreader1["Email"].ToString();
                c.Telefono = sqlreader1["Telefono"].ToString();
                c.Cellulare = sqlreader1["Cellulare"].ToString();
                ListClienti.Add(c);


            }


            conn.Close();
        }

        public static void Insert(Clienti c, string messaggio)
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
           .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Clienti VALUES(@Nome,@Cognome,@CodiceFiscale,@Città,@Provincia,@Email,@Telefono,@Cellulare)";
                cmd.Parameters.AddWithValue("Nome", c.Nome);
                cmd.Parameters.AddWithValue("Cognome", c.Cognome);
                cmd.Parameters.AddWithValue("CodiceFiscale", c.CodiceFiscale);
                cmd.Parameters.AddWithValue("Città", c.Città);
                cmd.Parameters.AddWithValue("Provincia", c.Provincia);
                cmd.Parameters.AddWithValue("Email", c.Email);
                cmd.Parameters.AddWithValue("Telefono", c.Telefono);
                cmd.Parameters.AddWithValue("Cellulare", c.Cellulare);


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
        public static void Modifica(Clienti s)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
            SqlConnection conn2 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn2;
            cmd.CommandText = "UPDATE Clienti SET Nome=@Nome,Cognome=@Cognome,CodiceFiscale=@CodiceFiscale,Città=@Città,Provincia=@Provincia,Email=@Email,Telefono=@Telefono,Cellulare=@Cellulare  where IdCliente=@id";

            cmd.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["Id"]);
            cmd.Parameters.AddWithValue("Nome", s.Nome);
            cmd.Parameters.AddWithValue("Cognome", s.Cognome);
            cmd.Parameters.AddWithValue("CodiceFiscale", s.CodiceFiscale);
            cmd.Parameters.AddWithValue("Città", s.Città);
            cmd.Parameters.AddWithValue("Provincia", s.Provincia);
            cmd.Parameters.AddWithValue("Email", s.Email);
            cmd.Parameters.AddWithValue("Telefono", s.Telefono);
            cmd.Parameters.AddWithValue("Cellulare", s.Cellulare);

            conn2.Open();

            cmd.ExecuteNonQuery();

            conn2.Close();
        }
        


    }
}