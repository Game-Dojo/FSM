using FSM;
using Player.States;

namespace Player
{
    public class PlayerController : StateManager
    {
        private PlayerIdleState _idleState;
        private PlayerJumpState _jumpState;
        
        private void Awake()
        {
            _idleState = new PlayerIdleState(this);
            _jumpState = new PlayerJumpState(this);
        }

        private void Start()
        {
            SwitchState(_idleState);
        }
        
        public PlayerJumpState GetJumpState() => _jumpState;
    }
}
