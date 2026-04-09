using Player.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private InputActionReference jumpAction;
        
        private PlayerController _playerController;
        private PlayerJumpState _jumpState;
        
        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            _jumpState = _playerController.GetJumpState();
        }

        private void OnEnable()
        {
            jumpAction.action.performed += OnJumpPerformed;
        }

        private void OnDisable()
        {
            jumpAction.action.performed -= OnJumpPerformed;
        }
        
        private void OnJumpPerformed(InputAction.CallbackContext ctx)
        {
            _playerController.SwitchState(_jumpState);
        }
    }
}