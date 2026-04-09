using UnityEngine;

namespace Garden.Beet
{
    public class BeetSmallState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.TryGetComponent<BeetController>(out BeetController controller))
            {
                controller.SetPlantState(BeetController.PlantState.Small);
            }
        }
    }
}
