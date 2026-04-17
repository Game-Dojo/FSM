using Garden.Base;
using UnityEngine;
using UnityEngine.Events;

namespace Garden.Beet
{
    public class BeetController : PlantBase
    {
        public event UnityAction<Vector3> Harvested;
        public void OnHarvested(Vector3 currentPosition) => Harvested?.Invoke(currentPosition);
        
        public override void Harvest()
        {
            base.Harvest();
            OnHarvested(transform.position);
            print("Beet Harvested!");
        }

        public override bool CanHarvest() => GetPlantState() == PlantState.Full;
    }

    
}