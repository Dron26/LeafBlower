using UnityEngine;

public class TrashBag : MonoBehaviour
{
    private MeshRenderer _meshRenderer;


    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void  ChangeMaterial()
    {
        _meshRenderer.materials[0].SetFloat("_OutlineSize", 0);
    }
}
