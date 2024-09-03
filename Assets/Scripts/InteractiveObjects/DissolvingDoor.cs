using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvingDoor : MonoBehaviour, InteractiveObject
{
    private Material mat;
    public void StartDetected()
    {
        mat = GetComponent<Renderer>().materials[0];
        Debug.Log("You just detected a DissolvingDoor");
    }
    public void StopDetected()
    {
        Debug.Log("You just stopped detecting a DissolvingDoor");
    }
    public void StartAction()
    {
        Debug.Log("You just interacted with chasing blocK!");
        StartCoroutine(StartDissolving());
    }

    public IEnumerator StartDissolving() {
        for (float a=0f; a < 100f; a+=1f) {
            mat.SetFloat("_Progress", a/100f);
            yield return new WaitForSecondsRealtime(0.1f);
            Debug.Log("test");
        }
        yield return null;
    }
}
