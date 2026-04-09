using System.Collections;
using UnityEngine;

namespace Garden.Beet
{
    public class BeetController : MonoBehaviour
    {
        public enum PlantState
        {
            Root,
            Small,
            Medium,
            Full
        }
        
        [SerializeField] private PlantState plantState = PlantState.Root;
        
        private Animator _animator;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            SetPlantState(PlantState.Root);
        }

        public void SetPlantState(PlantState state)
        {
            plantState = state;
            foreach (Transform p in transform)
            {
                p.gameObject.SetActive(false);
            }

            StartCoroutine(nameof(ChangeState), (int)state);
        }

        private IEnumerator ChangeState(int plantIndex)
        {
            yield return new WaitForEndOfFrame();
            transform.GetChild(plantIndex).gameObject.SetActive(true);
        }
    }
}