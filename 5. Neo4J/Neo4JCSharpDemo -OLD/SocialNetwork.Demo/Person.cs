using Newtonsoft.Json;

namespace SocialNetwork.Demo
{
    public class Person
    {
        //This is required to make the serializer treat the 'C#' naming style as 'Java' in the DB
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
