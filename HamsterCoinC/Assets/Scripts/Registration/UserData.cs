using System;

[System.Serializable]
public class UserData
{
    public string email;
    public string username;
    public string password;
    public string promoCode;
    public string birthDate;

    // ����������� ��� 5 ���������
    public UserData(string email, string username, string password, string promoCode, string birthDate)
    {
        this.email = email;
        this.username = username;
        this.password = password;
        this.promoCode = promoCode;
        this.birthDate = birthDate;
    }
}
