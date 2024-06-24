using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//OBJETOS ESPECIALES QUE SE NECESITAN EN VARIOS NIVELES
public class EssentialObjects : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

