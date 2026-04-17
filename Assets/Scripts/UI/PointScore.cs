using TMPro;
using UnityEngine;

namespace UI
{
    public class PointScore : MonoBehaviour
    {
        private TMP_Text _scoreText;
        private Camera _mainCamera;
        private float _originalYPosition;

        private void Awake()
        {
            _scoreText = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            transform.LookAt(_mainCamera.transform.position);
            Destroy(gameObject, 2f);
        }

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
