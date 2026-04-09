using Enemy.States;
using FSM;

namespace Enemy
{
    public class EnemyController : StateManager
    {
        private EnemyIdleState _idleState;
        private void Awake()
        {
            _idleState = new EnemyIdleState(this);
        }

        private void Start()
        {
            SwitchState(_idleState);
        }
    }
}