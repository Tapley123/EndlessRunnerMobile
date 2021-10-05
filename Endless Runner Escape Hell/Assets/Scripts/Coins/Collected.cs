using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collected : MonoBehaviour
{
    
    //[SerializeField] private float dissapearForTime = 1f;
    private MeshRenderer mr;

    private void Start()
    {
        mr = this.GetComponent<MeshRenderer>();
    }

    private IEnumerator Collect(float time)
    {
        mr.enabled = false;

        yield return new WaitForSeconds(time);

        mr.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //StartCoroutine(Collect(dissapearForTime));
            mr.enabled = false;
        }
    }
}
