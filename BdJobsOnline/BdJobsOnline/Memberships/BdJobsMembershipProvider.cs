using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using BdJobsOnline.BdJobsDatabase;

namespace BdJobsOnline.Memberships
{
    public class BdJobsMembershipProvider : MembershipProvider
    {

        public override MembershipUser CreateUser(string firstName, string lastName, string username, string password, string email,
                                                  bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var entities = new BdJobsEntities();

            if (entities.Users.Any(u => u.UserName == username))
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }
            else if (entities.Users.Any(u => u.Email == email))
            {
                status = MembershipCreateStatus.DuplicateEmail;
            }
            else
            {

                string hPassword = GetSHA1HashData(password);

                var user = new User { UserName = username, Password = hPassword, RoleId = 3, FirstName = firstName, LastName = lastName, Email = email };
                entities.Users.Add(user);
                entities.SaveChanges();
                status = MembershipCreateStatus.Success;
            }

            return null;
        }


        private string GetSHA1HashData(string data)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnValue = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            return returnValue.ToString();
        }


        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new System.NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var hPassword = GetSHA1HashData(password);
            var entities = new BdJobsEntities();
            if (entities.Users.Any(user => user.UserName == username && user.Password == hPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 10; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 10; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return new MembershipPasswordFormat(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 1; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return "*"; }
        }
        #region Unused

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
                                                             string newPasswordAnswer)
        {
            throw new System.NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }



        public override string ResetPassword(string username, string answer)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new System.NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new System.NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new System.NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override string ApplicationName { get; set; }



        #endregion
    }
}