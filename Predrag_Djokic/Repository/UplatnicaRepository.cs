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
    public class UplatnicaRepository : IRepository<Uplatnica>
    {
        private SqlConnection conn;
        private void LoadConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["AlephDbContext"].ConnectionString;
            conn = new SqlConnection(connString);
        }
        public bool Create(Uplatnica uplatnica)
        {
           
            string query = "INSERT INTO UPLATNICA (ID_RACUNA, IZNOS_UPLATE, DATUM_PROMETA, SVRHA_UPLATE, UPLATILAC, HITNO) VALUES (@racunID, @iznos, @datum, @svrha_uplate, @uplatilac, @hitno);";
            query += " SELECT SCOPE_IDENTITY()";        // selektuj id novododatog zapisa nakon upisa u bazu

            LoadConnection();   // inicijaizuj novu konekciju

            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;    // ovde dodeljujemo upit koji ce se izvrsiti nad bazom podataka
                cmd.Parameters.AddWithValue("@racunID", uplatnica.Racun.Id);
                cmd.Parameters.AddWithValue("@iznos", uplatnica.Iznos);
                cmd.Parameters.AddWithValue("@datum", uplatnica.DatumPrometa.ToString());
                cmd.Parameters.AddWithValue("@svrha_update", uplatnica.SvrhaUplate);
                cmd.Parameters.AddWithValue("@uplatilac", uplatnica.Uplatilac);
                cmd.Parameters.AddWithValue("@hitno", uplatnica.Hitno);


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
            throw new NotImplementedException();
        }

        public IEnumerable<Uplatnica> GetAll()
        {
            LoadConnection();
            string query = "SELECT * FROM UPLATNICA u inner join RACUN r ON u.ID_RACUNA = r.ID_RACUNA;";
            DataTable dt = new DataTable(); // objekti u
            DataSet ds = new DataSet();     // koje smestam podatke


            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;

                SqlDataAdapter dadapter = new SqlDataAdapter();
                dadapter.SelectCommand = cmd;

                // Fill(...) metoda je bitna, jer se prilikom poziva te metode izvrsava upit nad bazom podataka
                dadapter.Fill(ds, "Uplatnice"); // naziv tabele u dataset-u
                dt = ds.Tables["Uplatnice"];    // formiraj DataTable na osnovu tabele u DataSet-u
                conn.Close();
            }

            List<Uplatnica> uplatnice = new List<Uplatnica>();

            foreach (DataRow dataRow in dt.Rows)    // izvuci podatke iz svakog reda tj. zapisa tabele
            {
                int uplatnicaId = int.Parse(dataRow["ID_UPLATNICE"].ToString());
                string uplatilac = dataRow["UPLATILAC"].ToString();
                string svrhaUplate = dataRow["SVRHA_UPLATE"].ToString();
                bool hitno = bool.Parse(dataRow["HITNO"].ToString());
                DateTime datumPrometa = DateTime.Parse(dataRow["DATUM_PROMETA"].ToString());
                double iznos = double.Parse(dataRow["IZNOS_UPLATE"].ToString());
                int racunId = int.Parse(dataRow["ID_RACUNA"].ToString());
                string nosilac = dataRow["NOSILAC_RACUNA"].ToString();
                string brojRacuna = dataRow["BROJ_RACUNA"].ToString();
                bool aktivan = bool.Parse(dataRow["AKTIVAN_RACUN"].ToString());
                bool onlineBanking = bool.Parse(dataRow["ONLINE_BANKING"].ToString());

                Racun racun = new Racun() { Id = racunId, NosilacRacuna = nosilac, BrojRacuna = brojRacuna, Aktivan = aktivan, OnlineBanking = onlineBanking };
                uplatnice.Add(new Uplatnica() { Id = uplatnicaId, DatumPrometa = datumPrometa, Hitno = hitno, Iznos = iznos, SvrhaUplate = svrhaUplate, Uplatilac = uplatilac, Racun = racun });
            }

            return uplatnice;
        }

        public Uplatnica GetById(int id)
        {
            LoadConnection();
            string query = "SELECT * FROM UPLATNICA u inner join RACUN r ON u.ID_RACUNA = r.ID_RACUNA WHERE ID_UPLATNICE = @id;";
            DataTable dt = new DataTable(); // objekti u
            DataSet ds = new DataSet();     // koje smestam podatke


            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;

                SqlDataAdapter dadapter = new SqlDataAdapter();
                dadapter.SelectCommand = cmd;
                cmd.Parameters.AddWithValue("@id", id);

                // Fill(...) metoda je bitna, jer se prilikom poziva te metode izvrsava upit nad bazom podataka
                dadapter.Fill(ds, "Uplatnice"); // naziv tabele u dataset-u
                dt = ds.Tables["Uplatnice"];    // formiraj DataTable na osnovu tabele u DataSet-u
                conn.Close();
            }

            Uplatnica uplatnica = new Uplatnica();

            foreach (DataRow dataRow in dt.Rows)    // izvuci podatke iz svakog reda tj. zapisa tabele
            {
                int uplatnicaId = int.Parse(dataRow["ID_UPLATNICE"].ToString());
                string uplatilac = dataRow["UPLATILAC"].ToString();
                string svrhaUplate = dataRow["SVRHA_UPLATE"].ToString();
                bool hitno = bool.Parse(dataRow["HITNO"].ToString());
                DateTime datumPrometa = DateTime.Parse(dataRow["DATUM_PROMETA"].ToString());
                double iznos = double.Parse(dataRow["IZNOS_UPLATE"].ToString());
                int racunId = int.Parse(dataRow["ID_RACUNA"].ToString());
                string nosilac = dataRow["NOSILAC_RACUNA"].ToString();
                string brojRacuna = dataRow["BROJ_RACUNA"].ToString();
                bool aktivan = bool.Parse(dataRow["AKTIVAN_RACUN"].ToString());
                bool onlineBanking = bool.Parse(dataRow["ONLINE_BANKING"].ToString());

                Racun racun = new Racun() { Id = racunId, NosilacRacuna = nosilac, BrojRacuna = brojRacuna, Aktivan = aktivan, OnlineBanking = onlineBanking };
                uplatnica = (new Uplatnica() { Id = uplatnicaId, DatumPrometa = datumPrometa, Hitno = hitno, Iznos = iznos, SvrhaUplate = svrhaUplate, Uplatilac = uplatilac, Racun = racun });
            }

            return uplatnica;
        }

        public void Update(Uplatnica uplatnica)
        {
            throw new NotImplementedException();
        }
    }
}