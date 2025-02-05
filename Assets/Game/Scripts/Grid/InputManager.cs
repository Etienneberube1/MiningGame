using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _sceneCamera;

    private Vector3 _lastposition;

    [SerializeField] private LayerMask _placementLayerMask;


    public Vector3 GetSelectedMapPosition()
    { 
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _sceneCamera.nearClipPlane;
        Ray ray = _sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, _placementLayerMask))
        {
            _lastposition = hit.point;
        }
        return _lastposition;
    }
}
