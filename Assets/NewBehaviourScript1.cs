using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    [SerializeField] GameObject GameObject;

    private void Start()
    {
        GameObject.transform.SetParent(transform, true);
        Tween tween = GameObject.transform.DOMove(transform.position, 1);

        //StartCoroutine(Move());
    }

    private IEnumerator Move()
    {

            while (GameObject.transform.localPosition != transform.position)
            {
                if (GameObject.transform.localPosition == transform.position)
                {
                GameObject.transform.SetParent(transform, false);
            }

                yield return null;
            }

        

        StopCoroutine(Move());
    }
}
