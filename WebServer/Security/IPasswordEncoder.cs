namespace HamsterCoin.Security
{
    public interface IPasswordEncoder
    {
        string Encode(string password);
        bool Verify(string enteredPassword, string hashedPassword);
    }
}
