using System;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 10f;

    public event Action<GameObject> Exploded;

    public void Explode(GameObject owner)
    {
        Collider[] affectedColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var collider in affectedColliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        Exploded?.Invoke(owner);
    }
}