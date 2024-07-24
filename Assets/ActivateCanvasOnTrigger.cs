using UnityEngine;

public class ActivateCanvasOnTrigger : MonoBehaviour
{
    // Reference to the Canvas you want to activate
    public GameObject canvasToActivate;

    // Tag to identify the player
    public string playerTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        if (canvasToActivate != null)
        {
            // Ensure the canvas is initially inactive
            canvasToActivate.SetActive(false);
        }
    }

    // Called when another collider enters the trigger collider attached to this object
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the correct tag
        if (other.CompareTag(playerTag))
        {
            // Activate the canvas
            if (canvasToActivate != null)
            {
                canvasToActivate.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void CloseCrafting()
    {
        canvasToActivate.SetActive(false);
        Time.timeScale = 1;

    }
}
