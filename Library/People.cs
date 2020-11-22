
namespace Library
{
    public class People
    {
       public string Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public People(string id = "i", string name = "x", string adress = "y", string password = "0", bool isAdmin = false)
        {
          Id = id;
          Name = name;
          Adress = adress;
          Password = password;
          IsAdmin = isAdmin;
        }

        public static string PeopleIsAdmin(bool isAdmin)
        {
            if (isAdmin == true)
            {
                return " Is an Admin";
            }
            else
            {
                return " Not an Admin";
            }
        }
    }
}
