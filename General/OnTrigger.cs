using UnityEngine;


public class OnTrigger : MonoBehaviour
{
    //[SerializeField]private UnityEvent<Collider2D> EnterTrigger;
    //[System.Serializable]
    [SerializeField]protected UltEvents.UltEvent<Collider2D> _EnterTrigger;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        _EnterTrigger.Invoke(other);
    }
}
