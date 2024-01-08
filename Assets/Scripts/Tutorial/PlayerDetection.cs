// PlayerDetection.cs
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public float detectionDistance = 3f; // Adjust the distance as needed
    public GameObject modalPopupPrefab;
    public string text;
    public GameObject player;

    private bool isPlayerClose = false;

    void Update()
    {
        // Check if the player is close and the pop-up is not already shown
        if (isPlayerClose)
        {
            // Check the actual distance between this object and the player
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerPosition());
            Debug.Log(PlayerPosition() + " This is the position of the player");


            if (distanceToPlayer <= detectionDistance)
            {
                // Player is close enough, show the pop-up
                ShowModalPopup(text);
            }
            else
            {
                // Player moved away, close the pop-up
                ClosePopUp();
            }
        }
    }

    Vector3 PlayerPosition()
    {
        return player.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = false;
            ClosePopUp();
        }
    }

    void ShowModalPopup(string popupText)
    {
        ModalPopup modalPopupScript = modalPopupPrefab.GetComponent<ModalPopup>();
        modalPopupScript.SetPopupText(popupText);
        modalPopupPrefab.SetActive(true);
    }

    void ClosePopUp()
    {
        modalPopupPrefab.SetActive(false);
    }
}
