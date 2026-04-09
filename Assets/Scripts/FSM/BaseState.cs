using UnityEngine;

namespace FSM
{
    public abstract class BaseState
    {
        protected StateManager Manager;

        protected BaseState(StateManager stateManager)
        {
            Manager = stateManager;
        }

        public virtual void EnterState() {}
        public virtual void UpdateState() {}
        public virtual void FixedUpdateState() {}
        public virtual void ExitState() {}
    
        public virtual void OnCollisionEnter(Collision collision) {}
    }
}
