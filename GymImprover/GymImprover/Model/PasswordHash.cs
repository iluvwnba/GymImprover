using System;
using System.Text;
using System.Security;
using System.Security.Cryptography;


namespace GymImprover.Model
{
    internal class PasswordHash
    {

        public string GenerateSalt()
        {
            //creates new Salt value using guid.newguid
            return Guid.NewGuid().ToString("N");
        }

        private string HashString(string toHash)
        {
            //Creates new object for hashing
            using (SHA256Managed sha = new SHA256Managed())
            {
                //Changes hash string to byte array
                byte[] dataToHash = Encoding.UTF8.GetBytes(toHash);
                //performs hash computation on byte array
                byte[] hashed = sha.ComputeHash(dataToHash);
                //Convert and returns string array of hash, 
                return Convert.ToBase64String(hashed);
            }
        }

        public string HashPassword(string password, string salt)
        {
            //Concatonates Password and salt
            string passSalt = password + salt;
            //Returns string returned from hashstring method
            return HashString(passSalt);
        }

        public bool CheckPassword(string password, string salt, string hash)
        {
            //Checks that the return value of  password and salt entered is equal to expected hash
            return HashPassword(password, salt) == hash;
        }
    }

}