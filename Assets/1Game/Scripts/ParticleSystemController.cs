using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    [SerializeField ]private List<GrabMashine>  _grabMashine;



    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<GrabMashine>(out GrabMashine grabMashine))
        {

        }
    }


}
