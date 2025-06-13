
using UnityEngine;
using UnityEngine.UI;

public class UserDropdownMenu : MonoBehaviour
{
    public GameObject menuPanel; // ��������� �� ���� Panel
    public Button triggerButton; // ��������� �� ������

    private void Start()
    {
        // �������� ������ ����
        menuPanel.SetActive(false);

        // ������ �������� ��䳿 ��� ������
        triggerButton.onClick.AddListener(ToggleMenu);
    }

    public void ToggleMenu()
    {
        // ���������� �������� ����
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}