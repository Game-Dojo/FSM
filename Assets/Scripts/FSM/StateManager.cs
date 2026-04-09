using UnityEngine;

namespace FSM
{
    public abstract class StateManager : MonoBehaviour
    {
        private BaseState _currentState;

        private void Update()
        {
            _currentState?.UpdateState();
        }

        private void FixedUpdate()
        {
            _currentState?.FixedUpdateState();
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            _currentState?.OnCollisionEnter(collision);
        }
        
        public void SwitchState(BaseState newState)
        {
            _currentState?.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }
    }
}
