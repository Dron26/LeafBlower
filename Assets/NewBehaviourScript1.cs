using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    [SerializeField] GameObject GameObject;

    private void Start()
    {
        
        Tween tween = GameObject.transform.DOMove(transform.position, 3);

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {

            while (GameObject.transform.position != transform.position)
            {
                if (GameObject.transform.position == transform.position)
                {
                GameObject.transform.SetParent(transform, false);
            }

                yield return null;
            }

        

        StopCoroutine(Move());
    }
}
