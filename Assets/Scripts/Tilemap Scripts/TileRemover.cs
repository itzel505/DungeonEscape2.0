using UnityEngine;
using UnityEngine.Tilemaps;

// Allows the player to remove tiles from tilemaps either manually or automatically
public class TileRemover : MonoBehaviour
{
    // Tilemap for tiles that are automatically collected when player touches them
    public Tilemap AutoCollectTilemap;
    // Tilemap for tiles that require a button press to collect
    public Tilemap InteractTilemap;
    public KeyboardInput KeyboardInput;
    public GamepadInput GamepadInput;

    private void Update()
    {
        if (WasPickupButtonPressed())
        {
            RemoveTileAtPlayerPosition();
        }
    }

    // Called continuously while player is touching a trigger collider
    private void OnTriggerStay2D(Collider2D collision)
    {
        RemoveAutoCollectTileAtPlayerPosition();
    }

    // Returns true if either keyboard OR gamepad pickup button was pressed
    private bool WasPickupButtonPressed()
    {
        bool keyboardPressed = IsKeyboardPickupPressed();
        bool gamepadPressed = IsGamepadPickupPressed();
        
        return keyboardPressed || gamepadPressed;
    }

    private bool IsKeyboardPickupPressed()
    {
        if (KeyboardInput == null)
        {
            return false;
        }
        
        return KeyboardInput.WasPickupButtonPressed();
    }

    private bool IsGamepadPickupPressed()
    {
        if (GamepadInput == null)
        {
            return false;
        }
        
        return GamepadInput.WasPickupButtonPressed();
    }

    // Removes tile from InteractTilemap (requires button press)
    private void RemoveTileAtPlayerPosition()
    {
        if (InteractTilemap == null)
        {
            return;
        }
        
        Vector3Int cellPosition = InteractTilemap.WorldToCell(transform.position);
        // Setting tile to null removes it from the tilemap
        InteractTilemap.SetTile(cellPosition, null);
    }

    // Removes tile from AutoCollectTilemap (automatic when player touches it)
    private void RemoveAutoCollectTileAtPlayerPosition()
    {
        if (AutoCollectTilemap == null)
        {
            return;
        }
        
        Vector3Int cellPosition = AutoCollectTilemap.WorldToCell(transform.position);
        // Setting tile to null removes it from the tilemap
        AutoCollectTilemap.SetTile(cellPosition, null);
    }
}