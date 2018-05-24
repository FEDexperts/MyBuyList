using System.Linq;

namespace MyBuyListDL.Handlers
{
    public class User
    {
        public static Users GetUser(string userName, string password)
        {
            using (Mbl_Model ctx = new Mbl_Model())
            {
                Users user = ctx.Users.SingleOrDefault(x => x.Name == userName && x.Password == password);
                return user;
            }
        }
    }
}
