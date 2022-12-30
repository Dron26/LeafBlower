using UnityEngine;

namespace _1Game.Scripts.WorkPlaces
{
public class TrashBag : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private ParticleSystem _particleSystem;
    private ParticleSystem _particleSystemText;
    private TrashBagMover _mover;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleSystemText = GetComponentInChildren<ParticleSystem>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _mover = GetComponent<TrashBagMover>();
    }

    private void OnEnable()
    {
        _mover.ReachedFinish += OnReachedFinish;
    }

    private void OnDisable()
    {
        _mover.ReachedFinish -= OnReachedFinish;
    }

    public void  ChangeMaterial()
    {
        _meshRenderer.materials[0].SetFloat("_OutlineSize", 0);
    }

    public void OnReachedFinish()
    {
        _particleSystem.Play();
        _particleSystemText.Play();
    }
}
}
