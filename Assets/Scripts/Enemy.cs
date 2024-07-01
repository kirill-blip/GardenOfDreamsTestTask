using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Health Health { get; private set; }

    private void Start() 
    {
        Health = GetComponent<Health>();
    }
}
