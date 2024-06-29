using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialObjectsSpawner : MonoBehaviour
{
    [SerializeField] GameObject essentialObjectsPrefab;

    private void Awake()
    {
        /*
        var existingObjects = FindObjectsOfType<EssentialObjects>();

        // Destruir todas las instancias existentes de EssentialObjects
        foreach (var obj in existingObjects)
        {
            Destroy(obj.gameObject);
        }

        // Crear una nueva instancia de EssentialObjects
        var spawnPos = new Vector3(0, 0, 0);
        var grid = FindObjectOfType<Grid>();
        if (grid != null)
        {
            spawnPos = grid.transform.position; // En la posición de la grilla
        }

        Instantiate(essentialObjectsPrefab, spawnPos, Quaternion.identity);
        */
        
        var existingObjects = FindObjectsOfType<EssentialObjects>();
        if (existingObjects.Length == 0)
        {
            //CAMBIO POR ADDITIVE SCENE
            // spawnmear en origen
            var spawnPos = new Vector3(0, 0, 0);

            var grid = FindObjectOfType<Grid>();
            if (grid != null)
                spawnPos = grid.transform.position; // en la posicion de la grilla

            Instantiate(essentialObjectsPrefab, spawnPos, Quaternion.identity);
        }
    }
}
