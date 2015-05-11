using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using CodeBox.Domain.Abstract;
using CodeBox.Domain.Concrete.ORM;
using Ninject;

namespace CodeBox.WebUI.Infrastructure.Concrete
{
	class CustomMembershipProvider : MembershipProvider
	{
		[Inject]
		public IUserRepository UserRepo { get; set; }

		private const string MailRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum|be)\b";

        private const string UsernameRegex = @"^[A-Za-z0-9_-]{3,15}$";

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{

			//Check for a valid username
			if (!new Regex(UsernameRegex).IsMatch(username))
			{
				status = MembershipCreateStatus.InvalidUserName;
				return null;
			}

			//Check for unique username!
			if (GetUser(username, false) != null)
			{
				status = MembershipCreateStatus.DuplicateUserName;
				return null;
			}

			//Check for a valid password
			var args = new ValidatePasswordEventArgs(username, password, true);
			OnValidatingPassword(args);
			if (args.Cancel)
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}

			//Check the mail address validation
			if (!new Regex(MailRegex).IsMatch(email))
			{
				status = MembershipCreateStatus.InvalidEmail;
				return null;
			}

			//Check for unique email
			if (RequiresUniqueEmail && !string.IsNullOrEmpty(GetUserNameByEmail(email)))
			{
				status = MembershipCreateStatus.DuplicateEmail;
				return null;
			}

			var u = new User
						 {
							 Name = null,
							 Surname = null,
							 Username = username,
							 Password = GetSHA256Hash(password),
							 Mail = email,
							 LastSeen = DateTime.Now,
							 Approved = true,
							 LockedOut = false,
							 CreationDate = DateTime.Now,
							 lastLoginDate = null,
							 LastPasswordChangeDate = null,
							 LastLockOutDate = null,
							 LastActivityDate = DateTime.Now,
							 ProviderName = GetType().Name,
							 Comment = null,
							 passwordQuestion = null,
						 };

			UserRepo.RegisterUser(u);
			UserRepo.AddUserToRole(u, "Users");
			status = MembershipCreateStatus.Success;
			return GetUser(username, true);
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			return false;
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			var user = UserRepo.GetUserByUsername(username, GetSHA256Hash(oldPassword));

			//Proper credentials?
			if (user != null)
			{

				//Valid new password?
				var args = new ValidatePasswordEventArgs(username, newPassword, true);
				OnValidatingPassword(args);
				if (args.Cancel)
				{
					return false;
				}
				//Change password
				user.Password = GetSHA256Hash(newPassword);
				return true;
			}
			return false;
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new NotImplementedException();
		}

		public override bool ValidateUser(string username, string password)
		{

			//Method is only called when logging a user in
			
			User user = UserRepo.GetUserByUsername(username, GetSHA256Hash(password));
			if (user != null && !user.LockedOut && user.Approved)
			{
				//Update login dates
				user.LastActivityDate = DateTime.Now;
				user.lastLoginDate = DateTime.Now;

				UserRepo.UpdateUser(user);
				return true;
			}
			return false;
		}

		public override bool UnlockUser(string userName)
		{
			UserRepo.ChangeLockedStatus(UserRepo.GetUserByUsername(userName), false);
			return false;
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			var user = UserRepo.GetUserByUsername(username);

			if (user != null)
			{
				//Put the dates in variables first, because datetime's are nullable in the database
				DateTime lastloginDenulled = DateTime.MinValue;
				if (user.lastLoginDate != null)
					lastloginDenulled = (DateTime)user.lastLoginDate;

				DateTime lastPassChangeDenulled = DateTime.MinValue; 
				if (user.LastPasswordChangeDate != null)
					lastPassChangeDenulled = (DateTime)user.LastPasswordChangeDate;

				DateTime lastLockoutDenulled = DateTime.MinValue; 
				if (user.LastLockOutDate != null)
					lastLockoutDenulled = (DateTime)user.LastLockOutDate;

				DateTime lastActivityDateDenulled = DateTime.MinValue;
				if (user.LastActivityDate == null)
					lastActivityDateDenulled = DateTime.MinValue;

				return new MembershipUser("CustomMembershipProvider", user.Username, null, user.Mail, "", "", user.Approved, user.LockedOut, user.CreationDate, lastloginDenulled, lastActivityDateDenulled, lastPassChangeDenulled, lastLockoutDenulled);
			}
			return null;
		}

		public override string GetUserNameByEmail(string email)
		{
			return UserRepo.GetUserNameByMail(email);
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new NotImplementedException(); }
		}

		public override bool EnablePasswordReset
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { throw new NotImplementedException(); }
		}

		public override string ApplicationName
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { return 3; }
		}

		public override int PasswordAttemptWindow
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresUniqueEmail
		{
			get { return true; }
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredPasswordLength
		{
			get { return 6; }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { throw new NotImplementedException(); }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { return @"(?=.{6,})(?=(.*\d){1,})(?=(.*\W){1,})"; }
		}

		public static string GetSHA256Hash(string message)
		{
			var ue = new UnicodeEncoding();
			var hashstring = new SHA256Managed();
			var hashvalue = hashstring.ComputeHash(ue.GetBytes(message));
			var hex = hashvalue.Aggregate("", (current, x) => current + String.Format("{0:x2}", x));
			return hex;
		}

		protected override void OnValidatingPassword(ValidatePasswordEventArgs args)
		{
			var r = new Regex(PasswordStrengthRegularExpression);

			if (!r.IsMatch(args.Password))
			{
				args.Cancel = true;
			}

		}


	}


}
