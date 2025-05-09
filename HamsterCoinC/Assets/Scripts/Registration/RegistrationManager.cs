//using UnityEngine;

//public class RegistrationManager : MonoBehaviour
//{
//    public static RegistrationManager Instance;

//    private string email, username, password, promo, dob;



//    void Awake()
//    {
//        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
//        else { Destroy(gameObject); }
//    }

//    public void SaveEmail(string mail) => email = mail;
//    public void SaveUserData(string user, string pass, string promoCode, string birth)
//    {
//        username = user; password = pass; promo = promoCode; dob = birth;
//    }

//    public void ShowStep1() => Debug.Log("Показати крок 1");
//    public void ShowStep2() => Debug.Log("Показати крок 2");
//    public void ShowStep3() => Debug.Log("Показати крок 3");
//}
