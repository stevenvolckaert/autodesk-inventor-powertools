using System;
using System.Web.Security;

namespace StevenVolckaert.Web.Mvc
{
    /// <summary>
    /// Provides extension methods for System.Web.Security.MembershipUser objects.
    /// </summary>
    public static class MembershipUserExtensions
    {
        /// <summary>
        /// Returns the date and time when a locked membership user would be unlocked.
        /// </summary>
        /// <param name="membershipUser">The membership user that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="membershipUser"/> is <c>null</c>.</exception>
        public static DateTime UnlockDate(this MembershipUser membershipUser)
        {
            if (membershipUser == null)
                throw new ArgumentNullException("membershipUser");

            return membershipUser.LastLockoutDate + TimeSpan.FromMinutes(Membership.PasswordAttemptWindow);
        }

        /// <summary>
        /// Returns the membership user's remaining locked out time.
        /// </summary>
        /// <param name="membershipUser">The membership user that this extension method affects.</param>
        /// <exception cref="ArgumentNullException"><paramref name="membershipUser"/> is <c>null</c>.</exception>
        public static TimeSpan RemainingLockedOutTime(this MembershipUser membershipUser)
        {
            if (membershipUser == null)
                throw new ArgumentNullException("membershipUser");

            return membershipUser.IsLockedOut
                ? membershipUser.UnlockDate() - DateTime.Now
                : TimeSpan.Zero;
        }
    }
}
