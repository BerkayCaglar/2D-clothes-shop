using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InteractionController : MonoBehaviour
{
    [System.Obsolete]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Player.Instance.isInZone && !Player.Instance.isOnDialogue && !Player.Instance.isOnFade)
        {
            Interact();
        }
        if (Input.GetKeyDown(KeyCode.I) && !Player.Instance.isOnDialogue && !Player.Instance.isOnFade && !Player.Instance.isOnShopInventory && !Player.Instance.isDraggingItem)
        {
            if (PlayerInventoryManager.Instance.ReturnCanvasIsActive())
            {
                PlayerInventoryManager.Instance.SetActiveCanvas(false);
                Player.Instance.isOnInventory = false;
            }
            else
            {
                PlayerInventoryManager.Instance.SetActiveCanvas(true);
                PlayerInventoryManager.Instance.PutItemsOnPlayerInventoryUI();
                Player.Instance.isOnInventory = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerInventoryManager.Instance.ReturnCanvasIsActive())
        {
            PlayerInventoryManager.Instance.SetActiveCanvas(false);
            Player.Instance.isOnInventory = false;
        }
    }
    private void Interact()
    {
        PlayerInventoryManager.Instance.CheckPlayerInventoryUIIsActive();
        DialogueController.Instance.StartDialogue();
        return;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door Interactions/Exit"))
        {
            if (SceneController.Instance.ReturnCurrentScene() == "Clothes Shop")
            {
                if (other.name == "Clothes Shop Exit Spot")
                {
                    SceneController.Instance.LoadScene("Town");
                    transform.position = SceneSpawnPoints.Instance.SpawnPoints["Clothes Shop To Town"];
                }
                else
                {
                    return;
                }
            }
        }
        else if (other.CompareTag("Door Interactions/Enter"))
        {
            if (SceneController.Instance.ReturnCurrentScene() == "Town")
                if (other.name == "Clothes Shop Enterance Spot")
                {
                    SceneController.Instance.LoadScene("Clothes Shop");
                    transform.position = SceneSpawnPoints.Instance.SpawnPoints["Town To Clothes Shop"];
                }
                else
                {
                    return;
                }
        }
        else if (other.CompareTag("NPC Interact Zone"))
        {
            Player.Instance.isInZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC Interact Zone"))
        {
            Player.Instance.isInZone = false;
        }
    }
}