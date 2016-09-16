using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenPlanner
{
    public class Customer
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Garden
    {
        public int GardenId { get; set; }
        public string Name { get; set; }
        //public double Size { get; set; }
        public int PlantHardinessZone { get; set; }
        public DateTime LastSpringFrostDate { get; set; }
        public DateTime FirstFallFrostDate { get; set; }
        public int FrostFreeGrowingSeason { get; set; }
    }

    public class Bed
    {
        public int BedId { get; set; }
        public int GardenId { get; set; }
        public string Name { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public string PicSource { get; set; }
    }

    public class Plant
    {
        public int Id { get; set; }
        public int GardenId { get; set; }
        public int PlantId { get; set; }
        public string Name { get; set; }
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public string PicSource { get; set; }
    }

    public class PlantsLookup
    {
        public int PlantId { get; set; }
        public string CommonName { get; set; }
        public string LatinName { get; set; }
        public string Family { get; set; }
        public bool IsParental { get; set; }
        public string PictureSource { get; set; }
    }
    public class Database
    {
        const string CONN_STRING = @"Data Source=desrosiers.database.windows.net;Initial Catalog=gardenplanner;Integrated Security=False;User ID=sqladmin;Password=16Avril1889;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(CONN_STRING);
            conn.Open();
        }

        public void AddCustomer(Customer c)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Customer(UserName, Email, Password) VALUES (@UserName, @Email, @Password)"))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@UserName", c.UserName);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Password", c.Password);
                cmd.ExecuteNonQuery();
            }
        }
        /*public int GardenId { get; set; }
        public string Name { get; set; }
        //public double Size { get; set; }
        public int PlantHardinessZone { get; set; }
        public DateTime LastSpringFrostDate { get; set; }
        public DateTime FirstFallFrostDate { get; set; }
        public int FrostFreeGrowingSeason { get; set; }*/

        public void AddGarden(Garden g)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Garden(Name, Size, PlantHardinessZone, LastSpringFrostDate, FirstFallFrostDate, FrostFreeGrowingSeason) VALUES (@Name, @Size, @PlantHardinessZone, @LastSpringFrostDate, @FirstFallFrostDate, @FrostFreeGrowingSeason)"))
            { 
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@UserName", g.Name);
                cmd.Parameters.AddWithValue("@Email", g.PlantHardinessZone);
                cmd.Parameters.AddWithValue("@Password", g.LastSpringFrostDate);
                cmd.Parameters.AddWithValue("@Password", g.LastSpringFrostDate);
                cmd.Parameters.AddWithValue("@Password", g.LastSpringFrostDate);
                cmd.ExecuteNonQuery();
            }
        }














        public void AddBed(Bed b)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Bed (Garden_Id, CoordinateX, CoordinateY, BedColor) VALUES (@GardenId, @CoordinateX, @CoordinateY, @BedColor)");
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@GardenId", b.GardenId);
            cmd.Parameters.AddWithValue("@BedColor", b.PicSource);
            cmd.Parameters.AddWithValue("@CoordinateX", b.CoordinateX);
            cmd.Parameters.AddWithValue("@CoordinateY", b.CoordinateY);
            cmd.ExecuteNonQuery();
        }
        /*
        public List<Bed> GetAllBeds(int id)
        {
            SqlCommand cmd = new SqlCommand("SELECT BedId, CoordinateX, CoordinateY, BedColor FROM Bed WHERE Garden_Id = @GardenId", conn);
            cmd.Parameters.AddWithValue("@GardenId", id);
            List<Bed> pList = new List<Bed>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // column by name - the better (preferred) way
                        int id = reader.GetInt32(reader.GetOrdinal("PlantedId"));
                        int gardenID = reader.GetInt32(reader.GetOrdinal("GardenId"));
                        int plantId = reader.GetInt32(reader.GetOrdinal("Plant_Id"));
                        int x = reader.GetInt32(reader.GetOrdinal("CoordinateX"));
                        int y = reader.GetInt32(reader.GetOrdinal("CoordinateY"));
                        string commonName = reader.GetString(reader.GetOrdinal("CommonName"));
                        string picDest = reader.GetString(reader.GetOrdinal("PictureDest"));

                        Plant p = new Plant() { Id = id, GardenId = gardenID, PlantId = plantId, CoordinateX = x, CoordinateY = y, Name = commonName, PicSource = picDest };
                        pList.Add(p);
                    }
                }
            }
            return pList;
        }
        */

        public List<Bed> GetBedByGardenId(int Id)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Bed WHERE Garden_Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            List<Bed> b = new List<Bed>();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //int id = reader.GetInt32(reader.GetOrdinal("PlantId"));
                            //string bedName = reader.GetString(reader.GetOrdinal("BedName"));
                            int cX = reader.GetInt32(reader.GetOrdinal("CoordinateX"));
                            int cY = reader.GetInt32(reader.GetOrdinal("CoordinateY"));
                            string picSource = reader.GetString(reader.GetOrdinal("BedColor"));
                            Bed p = new Bed() { GardenId = Id, CoordinateX = cX, CoordinateY = cY, PicSource = picSource };
                            b.Add(p);
                            
                        }
                    }
                    return b;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return b;
        }

        public void DeleteBedById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBed(Bed b)
        {
            throw new NotImplementedException();
        }

        public void AddPlant(Plant p)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Planted (GardenId, Plant_Id, CoordinateX, CoordinateY) VALUES (@BadId, @PlantId, @CoordinateX, @CoordinateY)");
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@BadId", p.GardenId);
            cmd.Parameters.AddWithValue("@PlantId", p.PlantId);
            cmd.Parameters.AddWithValue("@CoordinateX", p.CoordinateX);
            cmd.Parameters.AddWithValue("@CoordinateY", p.CoordinateY);
            cmd.ExecuteNonQuery();
        }

        public List<Plant> GetAllPlants(int gardenId)
        {
            SqlCommand cmd = new SqlCommand("SELECT Planted.PlantedId, Planted.GardenId, Planted.Plant_Id, Planted.CoordinateX, Planted.CoordinateY, Plant.CommonName, Plant.PictureDest FROM Planted, Plant WHERE Planted.Plant_Id = Plant.PlantId AND Planted.GardenId = @GardenId", conn);
            cmd.Parameters.AddWithValue("@GardenId", gardenId);
            List<Plant> pList = new List<Plant>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // column by name - the better (preferred) way
                        int id = reader.GetInt32(reader.GetOrdinal("PlantedId"));
                        int gardenID = reader.GetInt32(reader.GetOrdinal("GardenId"));
                        int plantId = reader.GetInt32(reader.GetOrdinal("Plant_Id"));
                        int x = reader.GetInt32(reader.GetOrdinal("CoordinateX"));
                        int y = reader.GetInt32(reader.GetOrdinal("CoordinateY"));
                        string commonName = reader.GetString(reader.GetOrdinal("CommonName"));
                        string picDest = reader.GetString(reader.GetOrdinal("PictureDest"));

                        Plant p = new Plant() { Id = id, GardenId = gardenID, PlantId = plantId, CoordinateX = x, CoordinateY = y, Name = commonName, PicSource = picDest };
                        pList.Add(p);
                    }
                }
            }
            return pList;
        }

        public void DeletePlantById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlant(Plant p)
        {
            throw new NotImplementedException();
        }

        public PlantsLookup GetPlantById(int Id)
        {

            SqlCommand cmd = new SqlCommand("SELECT * FROM Plant WHERE PlantId = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            PlantsLookup p = new PlantsLookup();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //int id = reader.GetInt32(reader.GetOrdinal("PlantId"));
                            string commonName = reader.GetString(reader.GetOrdinal("CommonName"));
                            string latinName = reader.GetString(reader.GetOrdinal("LatinName"));
                            string family = reader.GetString(reader.GetOrdinal("Family"));
                            string picSource = reader.GetString(reader.GetOrdinal("PictureDest"));
                            p.PlantId = Id;
                            p.CommonName = commonName;
                            p.LatinName = latinName;
                            p.Family = family;
                            p.PictureSource = picSource;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return p;
        }

        public void DeleteAllPlantsFromGarden(int GardenID)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Planted WHERE GardenId = @GardenID", conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@GardenID", GardenID);
            cmd.ExecuteNonQuery();
        }

        public void DeleteAllBedsFromGarden(int GardenID)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Bed WHERE Garden_Id = @GardenID", conn);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@GardenID", GardenID);
            cmd.ExecuteNonQuery();
        }
    }
}
