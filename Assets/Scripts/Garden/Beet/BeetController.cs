using Garden.Base;

namespace Garden.Beet
{
    public class BeetController : PlantBase
    {
        public override void Harvest()
        {
            base.Harvest();
            print("Beet Harvested!");
        }

        public override bool CanHarvest() => GetPlantState() == PlantState.Full;
    }
}