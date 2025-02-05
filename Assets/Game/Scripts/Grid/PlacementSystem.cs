using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject _mouseIndicator, _cellIndicator;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Grid _grid;

    private void Update()
    {
        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        _mouseIndicator.transform.position = mousePosition;

        Vector3Int gridPosition = _grid.WorldToCell(mousePosition);

        _cellIndicator.transform.position = _grid.CellToWorld(gridPosition);
    }
}
