using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float speed;
    
    public void Open()
    {
        StartCoroutine(OpenLoop());
    }

    private IEnumerator OpenLoop()
    {
        while (transform.position.y > -2.5f)
        {
            transform.position += Vector3.down * Time.deltaTime * speed;
            yield return new WaitForFixedUpdate();
        }
    }
}
