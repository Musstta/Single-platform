using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    Transform[] spawningObjects;

    [SerializeField]
    Transform[] spawningPoints;

    [SerializeField]
    float timeBetweenSpawn = 3F;

    float _currentTime;
    float _speedMultiplier;

    void Start()
    {
        _currentTime = timeBetweenSpawn;
    }

    void Update()
    {
        // Aumenta el tiempo actual por frame
        _currentTime += Time.deltaTime;

        _speedMultiplier += Time.deltaTime * 0.1F;
        
        // Si el tiempo actual es mayor o igual al tiempo entre spawneo
        if (_currentTime >= timeBetweenSpawn)
        {
            // Resetea el tiempo
            _currentTime = 0.0F;

            // Obtiene un indice aleatorio para el objeto a spawnear
            int spawningIndex = Random.Range(0, spawningObjects.Length);
            Transform prefab = spawningObjects[spawningIndex];

            // Obtiene un indice aleatorio para el punto de spawneo
            SpawningObjectController controller = prefab.GetComponent<SpawningObjectController>();
            int[] spawningPoints = controller.GetSpawningPoints();
            spawningIndex = spawningPoints[Random.Range(0, spawningPoints.Length)];

            // Crea la instancia en el prefab
            foreach(Transform point in this.spawningPoints)
            {
                if (point.gameObject.name.Equals("Point " + spawningIndex.ToString()))
                {
                    Instantiate(prefab, point.position, Quaternion.identity);
                    break;
                }
            }
        }
    }

    public float GetSpeedMultiplier()
    {
        return _speedMultiplier;
    }
}
