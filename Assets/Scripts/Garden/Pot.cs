using UnityEngine;

public class Pot : MonoBehaviour
{
    private bool _isOccupied;

    public void ToggleOccupy(bool occupied)
    {
        _isOccupied = occupied;
    }

    public bool IsOccupied()
    {
        return _isOccupied;
    }
}
