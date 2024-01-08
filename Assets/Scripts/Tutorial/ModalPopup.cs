// ModalPopup.cs
using TMPro;
using UnityEngine;

public class ModalPopup : MonoBehaviour
{
    public TMP_Text headerText;
    public TMP_Text contentText;
    public TMP_Text footerText;

    public void SetPopupText(string content)
    {
        contentText.text = content;

    }

    public void ShowPopup()
    {
        gameObject.SetActive(true);
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
