using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractiveObject
{
    public void StartDetected();
    public void StopDetected();
    public void StartAction();
}

public interface Inspectable
{
    public void StartDetected();
    public void StopDetected();
    public void StartAction();
}
