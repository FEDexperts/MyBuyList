using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace MyBuyListRest
{
    [DataContract]
    public class UserResponse
    {
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public Exception ex { get; set; }
    }

    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [WebGet(UriTemplate = "User/{un}/{pwd}", ResponseFormat = WebMessageFormat.Json)]
        UserResponse GetUser(string un, string pwd);
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserService : IUserService
    {
        public UserResponse GetUser(string un, string pwd)
        {
            UserResponse response = new UserResponse();
            //try
            //{
            //    var user = User.GetUser(un, pwd);
            //    if (user != null)
            //    {
            //        response.name = user.DisplayName;
            //    }
            //}
            //catch(Exception ex)
            //{
            //    response.ex = ex;
            //}
            return response;
        }
    }
}