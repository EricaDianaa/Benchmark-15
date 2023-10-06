using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Benchmark_15.Models
{
    public class Prenotazione
    {
        public int IdPrenotazione { get; set; }
        public int Cliente { get; set; }
        public int Camera { get; set; }
        public DateTime DataPrenotazione { get; set; }
        public string Anno { get; set; }
        public DateTime Dal { get; set; }
        public DateTime Al { get; set; }
        public decimal Caparra { get; set; }
        public decimal Tariffa { get; set;}
        public bool MezzaPensione { get; set; }
        public bool PensioneCompleta { get; set; }
        public bool PrimaColazione { get; set; }

        //Cliente
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
       
        //Camera
        public int IdCamera { get; set; }
        public string Descrizione { get; set; }
        public string Tipologia { get; set; }

        public static List<Prenotazione> ListPrenotazione = new List<Prenotazione>();
        public static List<Clienti> ListClienti = new List<Clienti>();
        public static List<Camere> ListCamere = new List<Camere>();
        public static List<SelectListItem> DropdownClienti = new List<SelectListItem>();
        public static List<SelectListItem> DropdownCamere = new List<SelectListItem>();
       
        //Dropdown clienti e camere
        public static List<Clienti> SelectNome()
        {

            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT Nome,IdCliente From Clienti", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();

            while (sqlreader1.Read())
            {
                Clienti c = new Clienti();
                c.Nome = sqlreader1["Nome"].ToString();
                c.IdCliente = Convert.ToInt16(sqlreader1["IdCliente"]);
                ListClienti.Add(c);

            }
            conn.Close();
            return ListClienti;

        }
        public static void Dropdown()
        {
            List<Clienti> sped = new List<Clienti>();
            sped = SelectNome();
            foreach (Clienti item in sped)
            {
                SelectListItem l = new SelectListItem { Text = item.Nome, Value = item.IdCliente.ToString() };
                DropdownClienti.Add(l);
            }


        }

        public static List<Clienti> SelectNomeId( int Id)
        {

            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
             .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("SELECT Nome,IdCliente From Clienti where IdCliente=@id", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            cmd1.Parameters.AddWithValue("id",Id );
            sqlreader1 = cmd1.ExecuteReader();

            while (sqlreader1.Read())
            {
                Clienti c = new Clienti();
                c.Nome = sqlreader1["Nome"].ToString();
                c.IdCliente = Convert.ToInt16(sqlreader1["IdCliente"]);
                ListClienti.Add(c);

            }
            conn.Close();
            return ListClienti;

        }
        public static void DropdownId(int Id)
        {
            List<Clienti> sped = new List<Clienti>();
            sped = SelectNomeId(Id);
            foreach (Clienti item in sped)
            {
                SelectListItem l = new SelectListItem { Text = item.Nome, Value = item.IdCliente.ToString() };
                DropdownClienti.Add(l);
            }


        }
        public static List<Camere> SelectCamera()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
          .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("select  * from Camere full join Prenotazioni on Prenotazioni.IdCamere=Camere.IdCamera where IdPrenotazione is null", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();
        
            while (sqlreader1.Read())
            {
                    Camere c = new Camere();
                c.IdCamera =Convert.ToInt16(sqlreader1["IdCamera"]);
                c.Descrizione =  sqlreader1["Descrizione"].ToString();
                c.Tipologia = sqlreader1["Tipologia"].ToString();
                c.NomeCamera = sqlreader1["Nome"].ToString();
                ListCamere.Add(c);


            }

           
            conn.Close();
            return ListCamere;
        }
        public static void Dropdown1()
        {
            List<Camere> sped = new List<Camere>();
            sped = SelectCamera();
            foreach (Camere item in sped)
            {
                SelectListItem l = new SelectListItem { Text = item.NomeCamera, Value = item.IdCamera.ToString() };
                DropdownCamere.Add(l);
            }
        }

        //Select
        public static void Select()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
          .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("select * from Prenotazioni inner join Clienti on Prenotazioni.IdClienti=Clienti.IdCliente inner join Camere on Prenotazioni.IdCamere=Camere.IdCamera ", conn);
            SqlDataReader sqlreader1;
            conn.Open();
            sqlreader1 = cmd1.ExecuteReader();
           
            while (sqlreader1.Read())
            { 
                Prenotazione c = new Prenotazione();
                c.IdCliente = Convert.ToInt16(sqlreader1["IdCliente"]);
                c.Nome = sqlreader1["Nome"].ToString();
                c.Cognome = sqlreader1["Cognome"].ToString();
                c.IdCamera = Convert.ToInt16(sqlreader1["IdCamera"]);
                c.IdPrenotazione = Convert.ToInt16(sqlreader1["IdPrenotazione"]);
                c.Descrizione = sqlreader1["Descrizione"].ToString();
                c.Tipologia = sqlreader1["Tipologia"].ToString();
                c.DataPrenotazione = Convert.ToDateTime( sqlreader1["DataPrenotazione"]);
                c.Dal = Convert.ToDateTime(sqlreader1["PeriodoSoggiornoDal"]);
                c.Al = Convert.ToDateTime(sqlreader1["PeriodoSoggiornoAl"]);
                c.Anno = sqlreader1["Anno"].ToString();
                c.Caparra = Convert.ToInt16(sqlreader1["Caparra"]);
                c.Tariffa = Convert.ToInt16(sqlreader1["Tariffa"]);
                c.MezzaPensione = Convert.ToBoolean(sqlreader1["MezzaPensione"]);
                c.PensioneCompleta = Convert.ToBoolean(sqlreader1["PensioneCompleta"]);
                c.PrimaColazione = Convert.ToBoolean(sqlreader1["PrimaColazione"]);

                ListPrenotazione.Add(c);


            }



            conn.Close();
        }

        //Insert
        public static void Insert(Prenotazione p, string messaggio,int Cliente,int Camera)
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
           .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Prenotazioni VALUES(@DataPrenotazione,@Anno,@PeriodoSoggiornoDal,@PeriodoSoggiornoAl,@Caparra,@Tariffa,@MezzaPensione,@Cliente,@Camera,@PensioneCompleta,@PrimaColazione)";
              
                cmd.Parameters.AddWithValue("DataPrenotazione", p.DataPrenotazione);
                cmd.Parameters.AddWithValue("Anno", p.Anno);
                cmd.Parameters.AddWithValue("PeriodoSoggiornoDal", p.Dal);
                cmd.Parameters.AddWithValue("PeriodoSoggiornoAl", p.Al);
                cmd.Parameters.AddWithValue("Caparra", p.Caparra);
                cmd.Parameters.AddWithValue("Tariffa", p.Tariffa);
                cmd.Parameters.AddWithValue("MezzaPensione", p.MezzaPensione); 
                cmd.Parameters.AddWithValue("Cliente", Cliente);
                cmd.Parameters.AddWithValue("Camera", Camera);
                cmd.Parameters.AddWithValue("PensioneCompleta", p.PensioneCompleta);
                cmd.Parameters.AddWithValue("PrimaColazione", p.PrimaColazione);
   
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
        public static void Modifica(Prenotazione s, int Cliente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
            SqlConnection conn2 = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn2;
            cmd.CommandText = "UPDATE Prenotazioni SET DataPrenotazione=@DataPrenotazione,Anno=@Anno,PeriodoSoggiornoDal=@PeriodoSoggiornoDal," +
                "PeriodoSoggiornoAl=@PeriodoSoggiornoAl,Caparra=@Caparra,Tariffa=@Tariffa,MezzaPensione=@MezzaPensione,IdClienti=@Cliente" +
                ",IdCamere=@Camera,PensioneCompleta=@PensioneCompleta,PrimaColazione=@PrimaColazione   where IdPrenotazione=@id";

            cmd.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["Id"]);
            cmd.Parameters.AddWithValue("DataPrenotazione", s.DataPrenotazione);
            cmd.Parameters.AddWithValue("Anno", s.Anno);
            cmd.Parameters.AddWithValue("PeriodoSoggiornoDal", s.Dal);
            cmd.Parameters.AddWithValue("PeriodoSoggiornoAl", s.Al);
            cmd.Parameters.AddWithValue("Caparra", s.Caparra);
            cmd.Parameters.AddWithValue("Tariffa", s.Tariffa);
            cmd.Parameters.AddWithValue("MezzaPensione", s.MezzaPensione);
            cmd.Parameters.AddWithValue("Cliente", Cliente);
            cmd.Parameters.AddWithValue("Camera", s.Camera);
            cmd.Parameters.AddWithValue("PensioneCompleta", s.PensioneCompleta);
            cmd.Parameters.AddWithValue("PrimaColazione", s.PrimaColazione);

            conn2.Open();

            cmd.ExecuteNonQuery();

            conn2.Close();
        }
    }
}