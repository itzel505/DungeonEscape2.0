using UnityEngine;
using UnityEngine.Tilemaps;

// Allows the player to place tiles on a tilemap at their current position
public class TilePlacer : MonoBehaviour
{
    public Tilemap InteractTilemap;
    public TileBase TileToPlace;
    public KeyboardInput KeyboardInput;
    public GamepadInput GamepadInput;

    private void Update()
    {
        if (WasPlaceButtonPressed())
        {
            PlaceTileAtPlayerPosition();
        }
    }

    // Returns true if either keyboard OR gamepad place button was pressed
    private bool WasPlaceButtonPressed()
    {
        bool keyboardPressed = IsKeyboardPlacePressed();
        bool gamepadPressed = IsGamepadPlacePressed();
        
        return keyboardPressed || gamepadPressed;
    }

    private bool IsKeyboardPlacePressed()
    {
        if (KeyboardInput == null)
        {
            return false;
        }
        
        return KeyboardInput.WasPlaceButtonPressed();
    }

    private bool IsGamepadPlacePressed()
    {
        if (GamepadInput == null)
        {
            return false;
        }
        
        return GamepadInput.WasPlaceButtonPressed();
    }

    private void PlaceTileAtPlayerPosition()
    {
        if (InteractTilemap == null)
        {
            return;
        }
        
        if (TileToPlace == null)
        {
            return;
        }
        
        // Converts player's world position to the nearest tilemap grid cell
        Vector3Int cellPosition = GetCellPositionAtPlayer();
        InteractTilemap.SetTile(cellPosition, TileToPlace);
    }

    // Converts world coordinates to tilemap grid coordinates
    private Vector3Int GetCellPositionAtPlayer()
    {
        return InteractTilemap.WorldToCell(transform.position);
    }
}