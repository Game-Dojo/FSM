using System;
using System.Collections.Generic;
using Garden.Beet;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class GridManager : MonoBehaviour
    {
        private enum GridOrientation { FloorXZ, WallXY, SideYZ }
        
        [Header("Grid")]
        [SerializeField] private GridOrientation orientation = GridOrientation.FloorXZ;
        [SerializeField] private Grid logicGrid;
        [SerializeField] private Transform plantsParent;
        [SerializeField] private int gridRange = 10;
        [SerializeField] private Color gridColor = Color.cyan;
        

        [Header("Prefabs")] [SerializeField] private BeetController beet;
        
        [Header("Raycast")]
        [SerializeField] private InputActionReference mouseAction;
        [SerializeField] private LayerMask plantLayerMask;

        private readonly List<Vector3> _usedPosition = new List<Vector3>();
        
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
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2);

            if (!hit.collider) return;
            CheckGrid(hit.point);
        }
        
        private void CheckGrid(Vector3 hitPoint)
        {
            Vector3Int cellPosition = logicGrid.WorldToCell(hitPoint);
            //print("Cell " + cellPosition.ToString());
            var offsetXZ = new Vector3(.5f, 0, .5f);
            var worldPosition = logicGrid.CellToWorld(cellPosition) + offsetXZ;
            
            if (_usedPosition.Contains(cellPosition)) return;
            var beetController = Instantiate(beet, worldPosition, Quaternion.identity, plantsParent);
            if (!beetController) return;
            beetController.Harvested += Harvested;
            _usedPosition.Add(cellPosition);
        }
        
        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void Harvested(Vector3 worldPosition)
        {
            Vector3Int cellPosition = logicGrid.WorldToCell(worldPosition);
            _usedPosition.Remove(cellPosition);
        }

        private void RemoveListeners()
        {
            foreach (Transform plant in plantsParent)
            {
                if (plant.TryGetComponent<BeetController>(out var beetController))
                {
                    beetController.Harvested -= Harvested;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            DrawGrid();
        }
    
        private void DrawGrid()
        {
            if (logicGrid == null) logicGrid = GetComponent<Grid>();
    
            Gizmos.color = gridColor;
            
            for (int i = -gridRange; i <= gridRange; i++)
            {
                Vector3 start, end;
                Vector3 start2, end2;
    
                switch (orientation)
                {
                    case GridOrientation.FloorXZ:
                        // Lines along X
                        start = logicGrid.CellToWorld(new Vector3Int(i, 0, -gridRange));
                        end = logicGrid.CellToWorld(new Vector3Int(i, 0, gridRange));
                        // Lines along Z
                        start2 = logicGrid.CellToWorld(new Vector3Int(-gridRange, 0, i));
                        end2 = logicGrid.CellToWorld(new Vector3Int(gridRange, 0, i));
                        break;
    
                    case GridOrientation.WallXY:
                        // Lines along X
                        start = logicGrid.CellToWorld(new Vector3Int(i, -gridRange, 0));
                        end = logicGrid.CellToWorld(new Vector3Int(i, gridRange, 0));
                        // Lines along Y
                        start2 = logicGrid.CellToWorld(new Vector3Int(-gridRange, i, 0));
                        end2 = logicGrid.CellToWorld(new Vector3Int(gridRange, i, 0));
                        break;
    
                    case GridOrientation.SideYZ:
                    default: // SideYZ
                        start = logicGrid.CellToWorld(new Vector3Int(0, i, -gridRange));
                        end = logicGrid.CellToWorld(new Vector3Int(0, i, gridRange));
                        start2 = logicGrid.CellToWorld(new Vector3Int(0, -gridRange, i));
                        end2 = logicGrid.CellToWorld(new Vector3Int(0, gridRange, i));
                        break;
                }
    
                Gizmos.DrawLine(start, end);
                Gizmos.DrawLine(start2, end2);
            }
        }
    }
}