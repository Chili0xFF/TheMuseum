using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingInteractiveObject : MonoBehaviour, InteractiveObject
{
    public void StartDetected()
    {
        Debug.Log("You just detected a block you can chase!");
        this.transform.localScale = new Vector3(3, 3, 3);
    }
    public void StopDetected()
    {
        Debug.Log("You just stopped detecting a block you can chase!");
        this.transform.localScale = new Vector3(1, 1, 1);
    }
    public void StartAction()
    {
        Debug.Log("You just interacted with chasing blocK!");
        this.transform.position += Vector3.forward * 10;
    }
}
