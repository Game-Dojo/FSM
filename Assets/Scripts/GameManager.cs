using Garden.Base;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Plant Management")]
    [SerializeField] private GameObject plant;
    [SerializeField] private Transform plantPots;
    [SerializeField] private Transform plantSpawnContainer;
    [SerializeField] private LayerMask plantLayerMask;
    
    [Header("UI")]
    [SerializeField] private PointScore pointPrefab;
    
    [Header("Input")]
    [SerializeField] private InputActionReference mouseAction;
    
    private void OnEnable()
    {
        mouseAction.action.performed += OnMouseClick;
    }

    private void OnDisable()
    {
        mouseAction.action.performed -= OnMouseClick;
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        if (!Camera.main) return;
        var mousePos = Mouse.current.position.ReadValue();
        var ray = Camera.main.ScreenPointToRay(mousePos);

        Physics.Raycast(ray, out var hit , Mathf.Infinity, plantLayerMask);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 10);

        if (!hit.collider) return;
        var possibleCollider = hit.collider;
        CheckForPlant(possibleCollider);
        CheckForPot(possibleCollider);
    }
    
    private void CheckForPot( Collider potCollider )
    {
        if (!potCollider.TryGetComponent<Pot>(out Pot pot)) return;
        if (!pot.IsOccupied()) pot.ToggleOccupy(false);
        
        var spawnPosition = potCollider.transform.position + Vector3.up * 0.5f;
        InstantiatePlant(spawnPosition);

        if (pot) pot.ToggleOccupy(true);
    }
    
    private void CheckForPlant( Collider plantCollider )
    {
        if (!plantCollider.TryGetComponent<PlantBase>(out PlantBase controller)) return;
        if (!controller.CanHarvest()) return;
        controller.Harvest();
        
        InstantiatePoints(controller.transform.position + Vector3.up * 0.7f, 1);
    }
    
    private void InstantiatePlant(Vector3 position)
    {
        Instantiate(plant, position, Quaternion.identity, plantSpawnContainer);
    }

    private void InstantiatePoints(Vector3 position, int points)
    {
        PointScore point = Instantiate(pointPrefab, position, Quaternion.identity);
        if (!point) return;
        point.SetScore(points);
    }
    
}
