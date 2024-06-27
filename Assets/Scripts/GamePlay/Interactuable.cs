using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactuable
{
    public IEnumerator Interact(Transform initiator);
}
