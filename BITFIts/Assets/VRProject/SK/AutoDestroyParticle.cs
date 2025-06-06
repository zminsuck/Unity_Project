// AutoDestroyParticle.cs
using UnityEngine;

public class AutoDestroyParticle : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }
}
