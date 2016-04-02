using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace TwitterClient.Demo
{
    public class TwitterClient
    {
        #region Singleton Implementation

        private static TwitterClient client;

        public static TwitterClient Instance
        {
            get
            {
                if (client == null)
                {
                    client = new TwitterClient();
                }
                return client;
            }
        }

        private TwitterClient()
        {
            Session = Connect(NODES);
            GetOrCreateKeySpace(KEYSPACE, this.Session);

        }

        private ISession Connect(string[] nodes)
        {
            this.Cluster = Cluster.Builder()
                .AddContactPoints(nodes).Build();

            return this.Cluster.Connect();
        }

        private void GetOrCreateKeySpace(string keyspace, ISession session, Dictionary<string, string> replication = null)
        {
            session.CreateKeyspaceIfNotExists(keyspace, replication ?? REPLICATION_DEFAULT, true);
            session.ChangeKeyspace(keyspace);
        }

        #endregion

        #region Constants & Variables

        private static readonly string KEYSPACE = "twitterwall";
        private static readonly Dictionary<string, string> REPLICATION_DEFAULT
            = new Dictionary<string, string>
            {
                { "class", "SimpleStrategy" },
                { "replication_factor", "1" }
            };
        private static readonly string[] NODES = { "127.0.0.1" };

        public ISession Session { get; private set; }

        public Cluster Cluster { get; private set; }

        #endregion

        public void InitDatabase()
        {
            CreateSchema();
            LoadData();
        }

        public void CreateSchema()
        {
            //1 users column family
            this.Session.Execute(
                   "CREATE TABLE users (" +
                         "username text PRIMARY KEY," +
                         "password text" +
                         ");");

            //2 following column family
            this.Session.Execute(
                   "CREATE TABLE following (" +
                         "username text PRIMARY KEY," +
                         "followed set<text>" +
                         ");");

            //3 following column family
            this.Session.Execute(
                   "CREATE TABLE followers (" +
                         "username text PRIMARY KEY," +
                         "following set<text>" +
                         ");");

            //4 posts column family
            this.Session.Execute(
                   "CREATE TABLE posts (" +
                         "post_id uuid PRIMARY KEY," +
                         "username text," +
                         "body text" +
                         ");");

            //5 posts column family
            this.Session.Execute(
                   "CREATE TABLE userposts (" +
                         "username text," +
                         "post_id uuid," +
                         "body text," +
                         "PRIMARY KEY (username, post_id)" +
                         ");");


            //// this.Session.Execute(
            ////       "CREATE TABLE playlists (" +
            ////             "id uuid," +
            ////             "title text," +
            ////             "album text, " +
            ////             "artist text," +
            ////             "song_id uuid," +
            ////             "PRIMARY KEY (id, title, album, artist)" +
            ////             ");");
        }

        public virtual void LoadData()
        {
            //1. Seed user column family
            Session.Execute("INSERT INTO users (username, password) " +
                  "VALUES (" +
                      "'Jimmy'," +
                      "'abcde999')" +
                      ";");

            Session.Execute("INSERT INTO users (username, password) " +
                  "VALUES (" +
                      "'Molly'," +
                      "'p@ssw0rd')" +
                      ";");

            Session.Execute("INSERT INTO users (username, password) " +
                  "VALUES (" +
                      "'Phil'," +
                      "'test123')" +
                    ";");

            //2. Seed following column family
            Session.Execute("INSERT INTO following (username, followed) " +
                  "VALUES (" +
                      "'Molly'," +
                      "{'Jimmy', 'Phil'})" +
                    ";");

            Session.Execute("INSERT INTO following (username, followed) " +
                  "VALUES (" +
                      "'Jimmy'," +
                      "{'Phil'})" +
                    ";");

            //3. Seed followers column family
            Session.Execute("INSERT INTO followers (username, following) " +
                  "VALUES (" +
                      "'Phil'," +
                      "{'Jimmy', 'Molly'})" +
                    ";");

            Session.Execute("INSERT INTO followers (username, following) " +
                  "VALUES (" +
                      "'Jimmy'," +
                      "{'Molly'})" +
                    ";");

            //4. Seed posts column family +
            //5. Seed userposts column family

            string uuid1 = "556716f7-2e54-4715-9f00-91dcbea6cf50";
            Session.Execute("INSERT INTO posts (post_id, username, body) " +
                  "VALUES (" +
                      uuid1 + "," +
                      "'Phil'," +
                      "'Life is good. Not great, just good.'" +
                      ");");
            Session.Execute("INSERT INTO userposts (username, post_id, body) " +
                  "VALUES (" +
                      "'Phil'," +
                       uuid1 + "," +
                      "'Life is good. Not great, just good.'" +
                      ");");


            string uuid2 = "656716f7-2e54-4715-9f00-91dcbea6cf50";
            Session.Execute("INSERT INTO posts (post_id, username, body) " +
                  "VALUES (" +
                      uuid2 + "," +
                      "'Molly'," +
                      "'I had a burger for dinner'" +
                      ");");
            Session.Execute("INSERT INTO userposts (username, post_id, body) " +
                  "VALUES (" +
                      "'Molly'," +
                       uuid2 + "," +
                      "'I had a burger for dinner'" +
                      ");");

            string uuid3 = "756716f7-2e54-4715-9f00-91dcbea6cf50";
            Session.Execute("INSERT INTO posts (post_id, username, body) " +
                  "VALUES (" +
                      uuid3 + "," +
                      "'Molly'," +
                      "'Having trouble coming up with a 140 character post today...'" +
                      ");");
            Session.Execute("INSERT INTO userposts (username, post_id, body) " +
                  "VALUES (" +
                      "'Molly'," +
                       uuid3 + "," +
                       "'Having trouble coming up with a 140 character post today...'" +
                      ");");

            string uuid4 = "856716f7-2e54-4715-9f00-91dcbea6cf50";
            Session.Execute("INSERT INTO posts (post_id, username, body) " +
                  "VALUES (" +
                      uuid4 + "," +
                      "'Jimmy'," +
                      "'GTL baby => Gym. Tan. Laundry'" +
                      ");");
            Session.Execute("INSERT INTO userposts (username, post_id, body) " +
                  "VALUES (" +
                       uuid4 + "," +
                      "'Jimmy'," +
                      "'GTL baby => Gym. Tan. Laundry'" +
                      ");");

            string uuid5 = "956716f7-2e54-4715-9f00-91dcbea6cf50";
            Session.Execute("INSERT INTO posts (post_id, username, body) " +
                  "VALUES (" +
                      uuid5 + "," +
                      "'Jimmy'," +
                      "'NoSQL is the best evah.'" +
                      ");");
            Session.Execute("INSERT INTO userposts (username, post_id, body) " +
                  "VALUES (" +
                      "'Jimmy'," +
                       uuid5 + "," +
                      "'NoSQL is the best evah.'" +
                      ");");





            ////"CREATE TABLE userposts (" +
            ////      "username text," +
            ////      "post_id uuid," +
            ////      "body text," +
            ////      "PRIMARY KEY (username, post_id)" +



            ////Session.Execute(
            ////      "INSERT INTO songs (id, title, album, artist, tags) " +
            ////      "VALUES (" +
            ////          "756716f7-2e54-4715-9f00-91dcbea6cf50," +
            ////          "'La Petite Tonkinoise'," +
            ////          "'Bye Bye Blackbird'," +
            ////          "'Joséphine Baker'," +
            ////          "{'jazz', '2013'})" +
            ////          ";");

            ////Session.Execute(
            ////      "INSERT INTO songs (id, title, album, artist, tags) " +
            ////      "VALUES (" +
            ////          "756716f7-2e54-4715-9f00-91dcbea6cf50," +
            ////          "'La Petite Tonkinoise'," +
            ////          "'Bye Bye Blackbird'," +
            ////          "'Joséphine Baker'," +
            ////          "{'jazz', '2013'})" +
            ////          ";");
            ////Session.Execute(
            ////      "INSERT INTO playlists (id, song_id, title, album, artist) " +
            ////      "VALUES (" +
            ////          "2cc9ccb7-6221-4ccb-8387-f22b6a1b354d," +
            ////          "756716f7-2e54-4715-9f00-91dcbea6cf50," +
            ////          "'La Petite Tonkinoise'," +
            ////          "'Bye Bye Blackbird'," +
            ////          "'Joséphine Baker'" +
            ////          ");");
        }

        public virtual IEnumerable<Row> QuerySchema()
        {
            RowSet results = this.Session.Execute("SELECT * FROM playlists " +
                    "WHERE id = 2cc9ccb7-6221-4ccb-8387-f22b6a1b354d;");

            return results.GetRows();
            ////Console.WriteLine(String.Format("{0, -30}\t{1, -20}\t{2, -20}\t{3, -30}",
            ////    "title", "album", "artist", "tags"));
            ////Console.WriteLine("-------------------------------+-----------------------+--------------------+-------------------------------");


        }

        public string DropSchema()
        {
            Session.Execute("DROP KEYSPACE " + KEYSPACE);
            return @"Finished dropping {KEYSPACE} keyspace.";
        }

        public void Close()
        {
            this.Cluster.Shutdown();
            Session.Dispose();
        }
    }
}
