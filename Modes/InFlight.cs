using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
/*
* @author team SMAC
*
* Barebones method and inputs.
*/

namespace InFlightMode
{
    public class InFlight {

        /*
        *CollisionChecks will take two GPS inputs and checks the
        *current distance between them and checks if both objects vector
        *would collide if they continue on same vector.
        *
        *@param GPS1, GPS2
        *@return distance of the two inputs
        */
        public static double collisionCheck(decimal lat1, decimal lon1, decimal alt1, decimal lat2, decimal lon2, decimal alt2)
        {
            double R = 6372.8; // In kilometers
            double dLat = deg2rad((double)(lat2 - lat1));
            double dLon = deg2rad((double)(lon2 - lon1));
            double lat_1 = (double)(lat1);
            double lat_2 = (double)(lat2);
            lat_1 = deg2rad(lat_1);
            lat_2 = deg2rad(lat_2);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat_1) * Math.Cos(lat_2);
            double c = 2 * Math.Asin(Math.Sqrt(a));


            //geometry to calculate total distance between objects, A^2 + B^2 = C^2
            double GPS_dist = (R * 2 * Math.Asin(Math.Sqrt(a)));//A
            double ALT_dist = (double)(Math.Abs(alt1 - alt2));//B
            double dist = Math.Sqrt(Math.Pow(GPS_dist, 2) + Math.Pow(ALT_dist, 2));//Sqrt(A^2 + B^2)


            Console.WriteLine(dist);
            return dist;
        }//calculate

        private static double deg2rad(double deg) {
            return (deg * Math.PI / 180.0);
        }
        private static double rad2deg(double rad) {
            return (rad / Math.PI * 180.0);
        }
        /*
          *WarningMessage will output a warning Message if two objects
          * within a certain distance of each other.
          *
          *@param CollisionDistance
          *@return Warninglevel
          */
        public static int WarningMessage(double CollisionDistance)
        {
        	int message = 0;
            if (CollisionDistance > 300)
                message = 0;
            else if (CollisionDistance > 200 && CollisionDistance <= 300)
                message = 1;
            else if (CollisionDistance > 100 && CollisionDistance <= 200)
                message = 2;
            else if (CollisionDistance >= 0 && CollisionDistance <= 100)
                message = 3;
            return message;
        }//WarningMessage


        /*
          *ActionRequestMessage will send out an ActionRequestMessage
          * When two object need to avoid a collison
          *
          *@param warningLevel;
          *@return ActionRequestMessage
          */
        public static int ActionRequestMessage(int warningLevel)
        {
            if(warningLevel == 0)
            {
                //no danger
                Console.WriteLine("\tYou are safe");
            }
            else if(warningLevel == 1)
            {
                //Stage 1 danger
                Console.WriteLine("\tYou are in Stage 1 danger");
            }
            else if(warningLevel == 2)
            {
                //Stage 2 danger
                Console.WriteLine("\tYou are in Stage 2 danger");
            }
            else if(warningLevel == 3)
            {
                //Stage 3 danger
                Console.WriteLine("\tYou are in Stage 3 danger!!! Take immediate action!!");
            }
            return warningLevel;
        }

        public static decimal[] getData(string table, string col)
        {
			//setting up connection to the database
			//retrieves the data, puts it in array and returns the array
        	string connection =
        		"Server=localhost;" +
        		"Database=gps_data;" +
        		"User ID=root;" +
        		"Password=0427;" +
        		"Pooling=false;";

        		IDbConnection dbConn = new MySqlConnection(connection);
        		dbConn.Open();
        		IDbCommand dbCmd = dbConn.CreateCommand();

        		string sqlQuery =
        			"SELECT " + col +
        			" FROM " + table;

        		dbCmd.CommandText = sqlQuery;
        		IDataReader reader = dbCmd.ExecuteReader();
        		decimal[] vals = new decimal[50];

                Console.WriteLine("\tAdding values from " + table);

        		int i = 0;
        		while(reader.Read())
        		{
        			vals[i] = (decimal) reader[col];
        			i++;
        		}
        		//Close and cleanup MySql connection
        		reader.Close();
        		reader = null;
        		dbCmd.Dispose();
        		dbCmd = null;
        		dbConn.Close();
        		dbConn = null;

        		return vals;
        }

        public static short[] get_ALT_data(string table, string col)
        {

            string connection =
                "Server=localhost;" +
                "Database=gps_data;" +
                "User ID=root;" +
                "Password=0427;" +
                "Pooling=false;";

            IDbConnection dbConn = new MySqlConnection(connection);
            dbConn.Open();
            IDbCommand dbCmd = dbConn.CreateCommand();

            string sqlQuery =
                "SELECT " + col +
                " FROM " + table;

            dbCmd.CommandText = sqlQuery;
            IDataReader reader = dbCmd.ExecuteReader();
            short[] vals = new short[50];
            int i = 0;
            while (reader.Read())
            {
                vals[i] = (short)reader[col];
                i++;
            }
            //Close and cleanup MySql connection
            reader.Close();
            reader = null;
            dbCmd.Dispose();
            dbCmd = null;
            dbConn.Close();
            dbConn = null;

            return vals;
        }

        public static void Main(string[] args) {
            string safeTable = "safe_data", 
                   lat1 = "latitude_1", long1 = "longitude_1", alt1 = "altitude_1", 
                   lat2 = "latitude_2", long2 = "longitude_2", alt2 = "altitude_2";

				//retrieving plane data from database---planes will never get in danger area
        	decimal[] safeLat1 = getData(safeTable, lat1);			//latitudes of plane 1 in safe boundary
        	decimal[] safeLong1 = getData(safeTable, long1);		//longitudes of plane 1 in safe boundary
            short[] safeAlt1 = get_ALT_data(safeTable, alt1);		//altitudes of plane 1 in safe boundary 
        	decimal[] safeLat2 = getData(safeTable, lat2);			//lats of plane 2 in safe
        	decimal[] safeLong2 = getData(safeTable, long2);		//longs of plane 2 in safe
            short[] safeAlt2 = get_ALT_data(safeTable, alt2);		//alts of plane 2 in safe
            for(int i = 0; i < safeLat1.Length; i++)
            {
                if(safeLat1[i] != 0 && safeLong1[i] != 0 && safeLat2[i] != 0 && safeLong2[i] != 0)
                {
                   int safeMessage = ActionRequestMessage(WarningMessage(collisionCheck(safeLat1[i], safeLong1[i], safeAlt1[i], safeLat2[i], safeLong2[i], safeAlt2[i])));
                }
                //Debug.Assert(safeMessage == 0, "Wrong action request message, expecting 0 but was: " + safeMessage);
            }

            Console.ReadLine();
            String dangerTable = "collision_altitude_data";
				//retrieving plane data from database---planes on a collision course in altitude range
            decimal[] dangerLat1 = getData(dangerTable, lat1);
        	decimal[] dangerLong1 = getData(dangerTable, long1);
            short[] dangerAlt1 = get_ALT_data(dangerTable, alt1);
        	decimal[] dangerLat2 = getData(dangerTable, lat2);
        	decimal[] dangerLong2 = getData(dangerTable, long2);
            short[] dangerAlt2 = get_ALT_data(dangerTable, alt2);
            for (int i = 0; i < dangerLat1.Length; i++)
            {
                if (dangerLat1[i] != 0 && dangerLong1[i] != 0 && dangerLat2[i] != 0 && dangerLong2[i] != 0)
                {
                    int collisionMessage = ActionRequestMessage(WarningMessage(collisionCheck(dangerLat1[i], dangerLong1[i], dangerAlt1[i], dangerLat2[i], dangerLong2[i], dangerAlt2[i])));
                }
                //Debug.Assert(collisionMessage == 1, "Wrong action request message, expecting 1 but was: " + collisionMessage);
            }

            Array.Clear(dangerLat1, 0, dangerLat1.Length);
            Array.Clear(dangerLong1, 0, dangerLong1.Length);
            Array.Clear(dangerAlt1, 0, dangerAlt1.Length);
            Array.Clear(dangerLat2, 0, dangerLat2.Length);
            Array.Clear(dangerLong2, 0, dangerLong2.Length);
            Array.Clear(dangerAlt2, 0, dangerAlt2.Length);

            Console.ReadLine();

            dangerTable = "collision_altitude_vector_data";

				//retrieving plane data from database---planes on a collision course in altitude range AND in vector directions
            dangerLat1 = getData(dangerTable, lat1);
            dangerLong1 = getData(dangerTable, long1);
            dangerAlt1 = get_ALT_data(dangerTable, alt1);
            dangerLat2 = getData(dangerTable, lat2);
            dangerLong2 = getData(dangerTable, long2);
            dangerAlt2 = get_ALT_data(dangerTable, alt2);
            for (int i = 0; i < dangerLat1.Length; i++)
            {
                if (dangerLat1[i] != 0 && dangerLong1[i] != 0 && dangerLat2[i] != 0 && dangerLong2[i] != 0)
                {
                    int collisionMessage = ActionRequestMessage(WarningMessage(collisionCheck(dangerLat1[i], dangerLong1[i], dangerAlt1[i], dangerLat2[i], dangerLong2[i], dangerAlt2[i])));
                }
                //Debug.Assert(collisionMessage == 1, "Wrong action request message, expecting 1 but was: " + collisionMessage);
            }

            Array.Clear(dangerLat1, 0, dangerLat1.Length);
            Array.Clear(dangerLong1, 0, dangerLong1.Length);
            Array.Clear(dangerAlt1, 0, dangerAlt1.Length);
            Array.Clear(dangerLat2, 0, dangerLat2.Length);
            Array.Clear(dangerLong2, 0, dangerLong2.Length);
            Array.Clear(dangerAlt2, 0, dangerAlt2.Length);

            Console.ReadLine();
 
            dangerTable = "collision_vector_data";

				//retrieving plane data from database---planes on a collision course in vector directions
            dangerLat1 = getData(dangerTable, lat1);
            dangerLong1 = getData(dangerTable, long1);
            dangerAlt1 = get_ALT_data(dangerTable, alt1);
            dangerLat2 = getData(dangerTable, lat2);
            dangerLong2 = getData(dangerTable, long2);
            dangerAlt2 = get_ALT_data(dangerTable, alt2);
            for (int i = 0; i < dangerLat1.Length; i++)
            {
                if (dangerLat1[i] != 0 && dangerLong1[i] != 0 && dangerLat2[i] != 0 && dangerLong2[i] != 0)
                {
                    int collisionMessage = ActionRequestMessage(WarningMessage(collisionCheck(dangerLat1[i], dangerLong1[i], dangerAlt1[i], dangerLat2[i], dangerLong2[i], dangerAlt2[i])));
                }
                //Debug.Assert(collisionMessage == 1, "Wrong action request message, expecting 1 but was: " + collisionMessage);
            }

            Console.WriteLine("Press any key");
            Console.ReadLine();
        }
    }
}