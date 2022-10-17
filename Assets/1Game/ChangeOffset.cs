using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOffset : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private Renderer _renderer;



    // Start is called before the first frame update
    void Start()
    {
        _renderer=GetComponent<MeshRenderer>();
        scrollSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 textureOffset = new Vector2( 0,Time.time * scrollSpeed);
        _renderer.material.mainTextureOffset = textureOffset;
    }
}
