using DG.Tweening;
using FSM;

namespace Enemy.States
{
    public class EnemyIdleState : BaseState
    {
        private readonly EnemyController _controller;
        
        public EnemyIdleState(EnemyController context) : base(context)
        {
            _controller = context;
        }

        public override void EnterState()
        {
            _controller.transform.DOScale(2.0f, 1.0f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}