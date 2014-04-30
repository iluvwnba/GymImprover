using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymImprover.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GymImprover.UnitTests
{
    [TestClass]
    public class PasswordHashTest
    {
        private PasswordHash makePasswordHash()
        {
            return new PasswordHash();
        }

        [TestMethod]
        public void CheckPassword_GoodPassword_ReturnTrue()
        {
            PasswordHash ph = makePasswordHash();
            string salt = ph.GenerateSalt();
            string password = "password";
            string hashedPass = ph.HashPassword(password, salt);
            bool returnFromPasswordCheck = ph.CheckPassword(password, salt, hashedPass);
            Assert.IsTrue(returnFromPasswordCheck);
        }

        [TestMethod]
        public void CheckPassword_BadPassword_ReturnTrue()
        {
            PasswordHash ph = makePasswordHash();
            string salt = ph.GenerateSalt();
            string password = "password";
            string badPassword = "badpassword";
            string hashedPass = ph.HashPassword(password, salt);
            bool returnFromPasswordCheck = ph.CheckPassword(badPassword, salt, hashedPass);
            Assert.IsFalse(returnFromPasswordCheck);
        }
    }
}
