using UnityEngine;

namespace _1Game.Scripts
{
    public class ChangeOffset : MonoBehaviour
    {
        [SerializeField] private float _scrollSpeed;
        private Renderer _renderer;

        void Start()
        {
            _renderer=GetComponent<MeshRenderer>();
            _scrollSpeed = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 textureOffset = new Vector2( 0,Time.time * _scrollSpeed);
            _renderer.material.mainTextureOffset = textureOffset;
        }
    }
}
