using UnityEngine;

namespace Garden.Beet
{
    public class BeetFullState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.TryGetComponent<BeetController>(out BeetController controller))
            {
                controller.SetPlantState(BeetController.PlantState.Full);
            }
        }
    }
}
