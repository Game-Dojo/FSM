using UnityEngine;

namespace Garden.Beet
{
    public class BeetSmallState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var controller = animator.GetComponentInParent<BeetController>();
            if (!controller) return;
            controller.SetPlantState(BeetController.PlantState.Small);
        }
    }
}
