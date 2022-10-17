using UnityEngine;

public class Leave : MonoBehaviour
{
    private Collider _collider;
    private Rigidbody _body;
    private float _force;

    private ParticleSystem _system;
    [SerializeField] private ParticleSystem.Particle[] m_rParticlesArray = null;

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
    //private void OnParticleCollision(GameObject other)
    //{
    //    Debug.Log(other.name);
    //}

}

































//   if (other.TryGetComponent<AirZone>(out AirZone airZone))
//    {
//        Vector3 normalizedVector = Vector3.Normalize(_collider.transform.position - other.transform.position);
//        float distanse = Vector3.Distance(_collider.transform.position, other.transform.parent.position);
//        float ratio = distanse / 1.6f;
//        _body.AddForce(normalizedVector * (_force / ratio));
//    }


//private void OnTriggerStay(Collider other)
//{
//    Debug.Log(other.name);

//    if (other.TryGetComponent<AirZone>(out AirZone airZone))
//    {
//        Vector3 normalizedVector = Vector3.Normalize(_collider.transform.position - other.transform.position);
//        float distanse = Vector3.Distance(_collider.transform.position, other.transform.parent.position);
//        float ratio = distanse / 1.6f;
//        _body.AddForce(normalizedVector * (_force / ratio));
//    }
//}

//private void OnParticleCollision(GameObject other)
//{
//    if (other.gameObject.TryGetComponent<AirZone>(out AirZone airZone))
//    {
//        //Debug.Log("Contact");
//    }
//}



//void OnTriggerStay(Collider collider)
//{
//    if (switchedOn)
//    {
//        if (collider.transform.tag == "MainChar")
//        {
//            normalizedVector = Vector3.Normalize(collider.transform.position - transform.parent.position); // find direction (substract vectors) between Ventilator and object and normalize it
//            distanse = Vector3.Distance(collider.transform.position, transform.parent.position); // get the distance between Ventilator and object
//            ratio = distanse / 1.5f; // set coefficient for increasins force accordingly to the distance
//            collider.rigidbody.AddForce(normalizedVector * (force / ratio));
//        }
//    }
//}

