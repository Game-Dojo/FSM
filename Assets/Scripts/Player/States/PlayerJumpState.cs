using DG.Tweening;
using FSM;

namespace Player.States
{
    public class PlayerJumpState : BaseState
    {
        private readonly PlayerController _player;
        
        public PlayerJumpState(PlayerController context) : base(context)
        {
            _player = context;
        }
        
        public override void EnterState()
        {
            _player.transform.DOJump(_player.transform.position, 3f, 1, 0.5f);
        }
    }
}