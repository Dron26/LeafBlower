using _1Game.Scripts.Particle;
using UnityEngine;
using UnityEngine.Events;

namespace _1Game.Scripts.Core
{
public class ParkPlace : MonoBehaviour
{
    public UnityAction CartEnter;
    private ParticleSystem _refuelParticle;
    private MoveCartParticle _moveCartParticle;
    private void Awake()
    {
        _moveCartParticle = GetComponentInChildren<MoveCartParticle>();
        _refuelParticle=_moveCartParticle.GetComponent<ParticleSystem>();
        _refuelParticle.Stop();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cart cart))
        {
            CartEnter?.Invoke();
            ChangeState(false);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Cart cart))
        {
            ChangeState(true);
        }
    }
    
    private void ChangeState (bool isCartMove)
    {

        if (isCartMove)
        {
            _refuelParticle.Play();
        }
        else
        {
            _refuelParticle.Stop();
        }
    }
}
}