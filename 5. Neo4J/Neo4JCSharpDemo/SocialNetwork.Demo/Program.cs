using System;

namespace SocialNetwork.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Social();
            s.Setup();

            var friendsOfFriends = s.FriendsOfAFriend(new Person { Name = "Joe" });
            Console.WriteLine($"Joe's friends (of friends)");
            foreach (var fof in friendsOfFriends)
            {
                Console.WriteLine($"\t{fof.Name}");
            }

            Console.WriteLine();

            var commonFriends = s.CommonFriends(new Person { Name = "Joe" }, new Person { Name = "Sally" });
            Console.WriteLine("Joe and Sally's common friends");
            foreach (var friend in commonFriends)
            {
                Console.WriteLine($"\t{friend.Name}");
            }

            Console.WriteLine();

            var connectingNames = s.ConnectingPaths(new Person { Name = "Joe" }, new Person { Name = "Billy" });
            Console.WriteLine("Path to Billy");
            foreach (var name in connectingNames)
            {
                Console.WriteLine($"\t{name}");
            }


            Console.ReadKey();
        }
    }
}