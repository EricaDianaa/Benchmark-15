using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Benchmark_15.Models
{
    public class Servizi
    {
        public int IdServizio { get; set; }
        public int IdPrenotazioni { get; set; }
        public int Descrizione { get; set; }
        public DateTime Data { get; set; }
        public int Quantità { get; set; }
        public decimal Prezzo { get; set; }
       
        //TipoServizio
        public int IdTipo { get; set; }
        public string TipoServizi { get; set; }

        public static List<Servizi> ListServizi = new List<Servizi>();

        public static List<TipoServizio>ListPrenotazione=new List<TipoServizio>();
        public static List<SelectListItem> DropdownTipoServizio = new List<SelectListItem>();
        public static List<TipoServizio> SelectTipoServizio()
        {

            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT * From TipoServizio", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();

            while (sqlreader1.Read())
            {
                TipoServizio p = new TipoServizio();
                p.IdTipo = Convert.ToInt16(sqlreader1["IdTipo"]);
                p.TipoServizi = sqlreader1["TipoServizio"].ToString();
                ListPrenotazione.Add(p);
            }
            conn.Close();
            return ListPrenotazione;

        }
        public static void Dropdown()
        {
            List<TipoServizio> sped = new List<TipoServizio>();
            sped = SelectTipoServizio();
            foreach (TipoServizio item in sped)
            {
                SelectListItem l = new SelectListItem { Text = item.TipoServizi, Value = item.IdTipo.ToString() };
                DropdownTipoServizio.Add(l);
            }


        }


        public static void Select()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
          .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("select * from Servizi inner join TipoServizio on Servizi.IdServizio=TipoServizio.IdTipo", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();
           
            while (sqlreader1.Read())
            {
                Servizi c = new Servizi();
                c.IdServizio = Convert.ToInt16(sqlreader1["IdServizio"]);
                c.IdPrenotazioni = Convert.ToInt16(sqlreader1["IdPrenotazioni"]);
                c.Descrizione = Convert.ToInt16( sqlreader1["Descrizione"]);
                c.Data = Convert.ToDateTime( sqlreader1["Data"]);
                c.Quantità = Convert.ToInt16(sqlreader1["Quantità"]);
                c.Prezzo = Convert.ToInt16(sqlreader1["Prezzo"]);
                c.IdTipo = Convert.ToInt16(sqlreader1["IdTipo"]);
                c.TipoServizi = sqlreader1["TipoServizio"].ToString();
                ListServizi.Add(c);

            }


            conn.Close();
        }
        public static void Insert(Servizi s, string messaggio,int TipoServizio)
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
           .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Servizi VALUES(@Descrizione,@Data,@Quantità,@Prezzo,@IdPrenotazioni)";
                cmd.Parameters.AddWithValue("Descrizione", TipoServizio);
                cmd.Parameters.AddWithValue("Data", s.Data);
                cmd.Parameters.AddWithValue("Quantità", s.Quantità);
                cmd.Parameters.AddWithValue("Prezzo", s.Prezzo);
                cmd.Parameters.AddWithValue("IdPrenotazioni", s.IdPrenotazioni);

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

        public static void Modifica(Servizi s,int TipoServizio)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
            SqlConnection conn2 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn2;
            cmd.CommandText = "UPDATE Servizi SET Descrizione=@Descrizione,Data=@Data,Quantità=@Quantità,Prezzo=@Prezzo,IdPrenotazioni=@IdPrenotazioni   where IdServizio=@id";
            cmd.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["Id"]);
            cmd.Parameters.AddWithValue("Descrizione", TipoServizio);
            cmd.Parameters.AddWithValue("Data", s.Data);
            cmd.Parameters.AddWithValue("Quantità", s.Quantità);
            cmd.Parameters.AddWithValue("Prezzo", s.Prezzo);
            cmd.Parameters.AddWithValue("IdPrenotazioni", s.IdPrenotazioni);

            conn2.Open();

            cmd.ExecuteNonQuery();

            conn2.Close();
        }

    }
}