using System;
using Garden.Beet;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject plant;
    [SerializeField] private InputActionReference mouseAction;
    [SerializeField] private LayerMask plantLayerMask;

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
        var mousePos = Mouse.current.position.ReadValue();
        var ray = Camera.main.ScreenPointToRay(mousePos);

        Physics.Raycast(ray, out RaycastHit hit , Mathf.Infinity, plantLayerMask);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

        if (hit.collider)
        {
            if (hit.collider.TryGetComponent<BeetController>(out BeetController controller))
            {
                if (controller.CanHarvest())
                {
                    controller.Harvest();
                }
            }
            else
            {
                if (hit.collider.TryGetComponent<Pot>(out Pot pot))
                {
                    if (pot.IsOccupied())
                    {
                        pot.ToggleOccupy(false);
                    }
                } 

                Vector3 spawnPosition = hit.collider.transform.position + Vector3.up * 0.5f;
                InstantiatePlant(spawnPosition);

                if (pot)
                    pot.ToggleOccupy(true);
            }
        }
    }

    private void InstantiatePlant(Vector3 position)
    {
        Instantiate(plant, position, Quaternion.identity);
    }
}
