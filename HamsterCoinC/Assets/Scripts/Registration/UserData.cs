using System;

[System.Serializable]
public class UserData
{
    public string email;
    public string username;
    public string password;
    public string promoCode;
    public string birthDate;

    // Конструктор для 5 аргументів
    public UserData(string email, string username, string password, string promoCode, string birthDate)
    {
        this.email = email;
        this.username = username;
        this.password = password;
        this.promoCode = promoCode;
        this.birthDate = birthDate;
    }
}
