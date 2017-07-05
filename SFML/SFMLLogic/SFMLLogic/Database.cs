//using SFML.System;
//using SFML.Graphics;
//using SFML.Audio;
//using SFML.Window;

//using System.Data.SQLite;
//using System.Data;
//using System.Diagnostics;

//namespace SFMLLogic
//{
//    public class Database
//    {
//        public static void Set_connection()
//        {
//            try { SQLiteConnection.CreateFile("HotPinkPong.sqlite"); }
//            catch { }
//        }
//        public static void ExecuteData(string Query)
//        {
//            DataBaseQueryExcecute(Query);
//        }
//        public static void ReadData(string Query)
//        {
//            DataBaseQueryRead(Query);
//        }
//        public static void DataBaseQueryExcecute(string sql)
//        {
//            SQLiteConnection Conn = DBOpen();
//            SQLiteCommand command = new SQLiteCommand(sql, Conn);
//            command.ExecuteNonQuery();
//            DBClose(Conn);
//        }
//        public static void DataBaseQueryRead(string sql)
//        {
//            SQLiteConnection Conn = DBOpen();
//            SQLiteCommand command = new SQLiteCommand(sql, Conn);
//            SQLiteDataReader reader = command.ExecuteReader();
//            while (reader.Read())
//                System.Console.WriteLine("ID:" + reader.GetInt32(0) + " WIN: " + reader.GetInt32(1) + " LOSE: " + reader.GetInt32(2));
//            DBClose(Conn);
//        }
//        public static SQLiteConnection DBOpen()
//        {
//            SQLiteConnection m_dbConnection;
//            m_dbConnection = new SQLiteConnection("Data Source=HotPinkPong.sqlite;Version=3");
//            m_dbConnection.Open();
//            return m_dbConnection;
//        }
//        public static void DBClose(SQLiteConnection m_dbConnection)
//        {
//            m_dbConnection.Close();
//        }
//    }
//}
