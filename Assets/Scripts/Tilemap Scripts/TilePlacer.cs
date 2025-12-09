using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacer : MonoBehaviour
{
    public Tilemap InteractTilemap;
    public TileBase TileToPlace;
    public KeyboardInput KeyboardInput;
    public GamepadInput GamepadInput;

    private void Update()
    {
        if (WasPlaceButtonPressed())
            PlaceTileAtPlayerPosition();
    }

    private bool WasPlaceButtonPressed()
    {
        bool keyboardPressed = KeyboardInput != null && KeyboardInput.WasPlaceButtonPressed();
        bool gamepadPressed = GamepadInput != null && GamepadInput.WasPlaceButtonPressed();
        return keyboardPressed || gamepadPressed;
    }

    private void PlaceTileAtPlayerPosition()
    {
        if (InteractTilemap == null || TileToPlace == null)
            return;

        Vector3Int cellPosition = InteractTilemap.WorldToCell(transform.position);
        InteractTilemap.SetTile(cellPosition, TileToPlace);
    }
}