using UnityEngine;

public class DisableCollisionWithPlayer : MonoBehaviour
{
    
    public Collider objectBCollider;

    
    private bool isPlayerColliding = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerColliding = true;
            objectBCollider.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerColliding = false;
            objectBCollider.enabled = true;
        }
    }

}
