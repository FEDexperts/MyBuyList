using System;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    public class SRL_User
    {
        public int UserId { get; set; }
        public int UserType { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }

        public SRL_User()
        {
        }

        public SRL_User(User fullUser)
        {
            this.UserId = fullUser.UserId;
            this.UserType = fullUser.UserTypeId;
            this.UserName = fullUser.Name;

            if (!string.IsNullOrEmpty(fullUser.DisplayName))
            {
                this.Name = fullUser.DisplayName;
            }
            else if (!string.IsNullOrEmpty(fullUser.FirstName))
            {
                this.Name = fullUser.FirstName;
            }
            else
            {
                this.Name = fullUser.Name;
            }
        }
    }
}