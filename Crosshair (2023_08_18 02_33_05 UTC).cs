using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshairTexture;
    private Rect crosshairRect;

    private void Start()
    {
        // Hide and lock the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Calculate the position of the crosshair
        float crosshairSize = 20f;
        float crosshairX = (Screen.width - crosshairSize) / 2f;
        float crosshairY = (Screen.height - crosshairSize) / 2f;
        crosshairRect = new Rect(crosshairX, crosshairY, crosshairSize, crosshairSize);
    }

    private void OnGUI()
    {
        // Draw the crosshair
        GUI.DrawTexture(crosshairRect, crosshairTexture);
    }
}
