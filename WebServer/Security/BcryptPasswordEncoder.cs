namespace HamsterCoin.Security
{
    public class BcryptPasswordEncoder : IPasswordEncoder
    {
        public string Encode(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string enteredPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }
    }
}

