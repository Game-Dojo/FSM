using DG.Tweening;
using FSM;
using UnityEngine;

namespace Player.States
{
    public class PlayerIdleState : BaseState
    {
        private readonly PlayerController _player;

        public PlayerIdleState(PlayerController context) : base(context) 
        {
            _player = context;
        }

        public override void EnterState()
        {
            Debug.Log("Player Entered Idle State");
            _player.transform.DOScaleY(1.1f, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
