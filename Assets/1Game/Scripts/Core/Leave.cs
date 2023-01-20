using _1Game.Scripts.Empty;
using UnityEngine;

namespace _1Game.Scripts.Core
{
    public class Leave : MonoBehaviour
    {
        private Collider _collider;
        private Rigidbody _body;
        private float _force;

        private void Start()
        {
            _body = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _force = 1f;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent<AirZone>(out AirZone airZone))
            {
                Vector3 normalizedVector = Vector3.Normalize(_collider.transform.position - other.transform.position);
                float distanse = Vector3.Distance(_collider.transform.position, other.transform.parent.position);
                float ratio = distanse / 1.6f;
                _body.AddForce(normalizedVector * (_force / ratio));
            }
        }
    }
}