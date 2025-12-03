using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRemover : MonoBehaviour
{
    [Tooltip("Tilemap for tiles that are automatically collected when player touches them")]
    public Tilemap AutoCollectTilemap;
    
    [Tooltip("Tilemap for tiles that require a button press to collect")]
    public Tilemap InteractTilemap;
    
    public KeyboardInput KeyboardInput;
    public GamepadInput GamepadInput;

    private void Update()
    {
        if (WasPickupButtonPressed())
            RemoveTileFromInteractTilemap();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        RemoveTileFromAutoCollectTilemap();
    }

    private bool WasPickupButtonPressed()
    {
        bool keyboardPressed = KeyboardInput != null && KeyboardInput.WasPickupButtonPressed();
        bool gamepadPressed = GamepadInput != null && GamepadInput.WasPickupButtonPressed();
        return keyboardPressed || gamepadPressed;
    }

    private void RemoveTileFromInteractTilemap()
    {
        if (InteractTilemap == null)
            return;

        Vector3Int cellPosition = InteractTilemap.WorldToCell(transform.position);
        InteractTilemap.SetTile(cellPosition, null);
    }

    private void RemoveTileFromAutoCollectTilemap()
    {
        if (AutoCollectTilemap == null)
            return;

        Vector3Int cellPosition = AutoCollectTilemap.WorldToCell(transform.position);
        AutoCollectTilemap.SetTile(cellPosition, null);
    }
}