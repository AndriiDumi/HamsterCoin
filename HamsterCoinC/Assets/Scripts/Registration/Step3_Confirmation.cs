//using UnityEngine;
//using UnityEngine.UI;
//using TMPro; // Додаємо цей простір імен
//using System.Collections.Generic;
//using System.IO;

//public class Step3_Confirmation : MonoBehaviour
//{
//    public Toggle termsToggle;
//    public Button createAccountButton;
//    public Button backButton;
//    public TMP_Text messageText; // Використовуємо TMP_Text замість Text

//    private RegistrationManager regManager;
//    private string filePath;

//    void Start()
//    {
//        regManager = RegistrationManager.Instance;
//        filePath = Application.persistentDataPath + "/users.json";

//        createAccountButton.onClick.AddListener(CreateAccount);
//        backButton.onClick.AddListener(() => regManager.ShowStep2());
//    }

//    void CreateAccount()
//    {
//        if (!termsToggle.isOn)
//        {
//            messageText.text = "Потрібно прийняти умови!";
//            return;
//        }

//        UserData newUser = regManager.GetUserData();
//        List<UserData> users = LoadUsers();
//        users.Add(newUser);
//        SaveUsers(users);

//        messageText.text = "Реєстрація успішна!";
//    }

//    List<UserData> LoadUsers()
//    {
//        if (File.Exists(filePath))
//        {
//            string json = File.ReadAllText(filePath);
//            return JsonUtility.FromJson<UserList>("{\"users\":" + json + "}").users;
//        }
//        return new List<UserData>();
//    }

//    void SaveUsers(List<UserData> users)
//    {
//        string json = JsonUtility.ToJson(new UserList { users = users }, true);
//        File.WriteAllText(filePath, json);
//    }

//    [System.Serializable]
//    private class UserList
//    {
//        public List<UserData> users;
//    }
//}
