using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Predrag_Djokic.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Predrag_Djokic.Repository
{
    public class RacunRepository : IRepository<Racun>
    {
        private SqlConnection conn;
        private void LoadConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["AlephDbContext"].ConnectionString;
            conn = new SqlConnection(connString);
        }

        public bool Create(Racun racun)
        {
            string query = "INSERT INTO RACUN (NOSILAC_RACUNA, BROJ_RACUNA, AKTIVAN_RACUN, ONLINE_BANKING ) VALUES (@Nosilac, @Broj, @Aktivan, @Online);";
            query += " SELECT SCOPE_IDENTITY()";        // selektuj id novododatog zapisa nakon upisa u bazu

            LoadConnection();   // inicijaizuj novu konekciju

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;    // ovde dodeljujemo upit koji ce se izvrsiti nad bazom podataka
                cmd.Parameters.AddWithValue("@Nosilac", racun.NosilacRacuna);
                cmd.Parameters.AddWithValue("@Broj", racun.BrojRacuna);
                cmd.Parameters.AddWithValue("@Aktivan", racun.Aktivan);
                cmd.Parameters.AddWithValue("@Online", racun.OnlineBanking);



                conn.Open();
                var newFormedId = cmd.ExecuteScalar();   // izvrsi upit nad bazom, vraca id novododatog zapisa
                conn.Close();

                if (newFormedId != null)
                {
                    return true;    // upis uspesan, generisan novi id
                }
            }
            return false;
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM RACUN WHERE ID_RACUNA = (@Id);";
            LoadConnection();

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;    // ovde dodeljujemo upit koji ce se izvrsiti nad bazom podataka
                cmd.Parameters.AddWithValue("@Id", id);  // stitimo od SQL Injection napada
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public IEnumerable<Racun> GetAll()
        {
            LoadConnection();
            string query = "SELECT * FROM RACUN;";
            DataTable dt = new DataTable(); // objekti u
            DataSet ds = new DataSet();     // koje smestam podatke


            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;

                SqlDataAdapter dadapter = new SqlDataAdapter();
                dadapter.SelectCommand = cmd;

                // Fill(...) metoda je bitna, jer se prilikom poziva te metode izvrsava upit nad bazom podataka
                dadapter.Fill(ds, "Racuni"); // naziv tabele u dataset-u
                dt = ds.Tables["Racuni"];    // formiraj DataTable na osnovu tabele u DataSet-u
                conn.Close();
            }

            List<Racun> racuni = new List<Racun>();

            foreach (DataRow dataRow in dt.Rows)    // izvuci podatke iz svakog reda tj. zapisa tabele
            {
                int racunId = int.Parse(dataRow["ID_RACUNA"].ToString());
                string nosilac = dataRow["NOSILAC_RACUNA"].ToString();
                string brojRacuna = dataRow["BROJ_RACUNA"].ToString();
                bool aktivan = bool.Parse(dataRow["AKTIVAN_RACUN"].ToString());
                bool onlineBanking = bool.Parse(dataRow["ONLINE_BANKING"].ToString());


                racuni.Add(new Racun() {Id = racunId, NosilacRacuna = nosilac, BrojRacuna = brojRacuna, Aktivan = aktivan, OnlineBanking = onlineBanking });
            }

            return racuni;
        }

        public Racun GetById(int id)
        {
            LoadConnection();
            string query = "SELECT * FROM RACUN WHERE ID_RACUNA = @id;";
            DataTable dt = new DataTable(); // objekti u
            DataSet ds = new DataSet();     // koje smestam podatke


            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter dadapter = new SqlDataAdapter();
                dadapter.SelectCommand = cmd;

                // Fill(...) metoda je bitna, jer se prilikom poziva te metode izvrsava upit nad bazom podataka
                dadapter.Fill(ds, "Racuni"); // naziv tabele u dataset-u
                dt = ds.Tables["Racuni"];    // formiraj DataTable na osnovu tabele u DataSet-u
                conn.Close();
            }

            Racun racun = null;

            foreach (DataRow dataRow in dt.Rows)    // izvuci podatke iz svakog reda tj. zapisa tabele
            {
                int racunId = int.Parse(dataRow["ID_RACUNA"].ToString());
                string nosilac = dataRow["NOSILAC_RACUNA"].ToString();
                string brojRacuna = dataRow["BROJ_RACUNA"].ToString();
                bool aktivan = bool.Parse(dataRow["AKTIVAN_RACUN"].ToString());
                bool onlineBanking = bool.Parse(dataRow["ONLINE_BANKING"].ToString());


                racun = new Racun() { Id = racunId, NosilacRacuna = nosilac, BrojRacuna = brojRacuna, Aktivan = aktivan, OnlineBanking = onlineBanking };
            }

            return racun;
        }
    

        public void Update(Racun racun)
        {
            //"UPDATE Genre SET GenreName = @GenreName WHERE GenreId = @GenreId;"
            string query = "UPDATE RACUN SET NOSILAC_RACUNA = @Nosilac WHERE ID_RACUNA = @Id;";
            query += "UPDATE RACUN SET BROJ_RACUNA = @Broj WHERE ID_RACUNA = @Id;";
            query += "UPDATE RACUN SET AKTIVAN_RACUN = @Aktivan WHERE ID_RACUNA = @Id;";
            query += "UPDATE RACUN SET ONLINE_BANKING = @Online WHERE ID_RACUNA = @Id;";

            LoadConnection();   // inicijaizuj novu konekciju

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;    // ovde dodeljujemo upit koji ce se izvrsiti nad bazom podataka
                cmd.Parameters.AddWithValue("@Id", racun.Id);
                cmd.Parameters.AddWithValue("@Nosilac", racun.NosilacRacuna);
                cmd.Parameters.AddWithValue("@Broj", racun.BrojRacuna);
                cmd.Parameters.AddWithValue("@Aktivan", racun.Aktivan);
                cmd.Parameters.AddWithValue("@Online", racun.OnlineBanking);



                conn.Open();
                cmd.ExecuteNonQuery();   // izvrsi upit nad bazom, vraca id novododatog zapisa
                conn.Close();
            }

        }
    }
}