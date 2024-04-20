using System.Collections;
using UnityEngine;

public class BossFireSpawner : MonoBehaviour
{
    public GameObject firePrefableft;
    public GameObject firePrefabright;
    public Transform playerTransform;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;

    private void Start()
    {
        StartCoroutine(SpawnFire());
    }

    IEnumerator SpawnFire()
    {
        while (true)
        {
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);
            Instantiate(firePrefabright, new Vector3(9f, playerTransform.position.y, 0f), Quaternion.identity);
            Instantiate(firePrefableft, new Vector3(-9f, playerTransform.position.y, 0f), Quaternion.identity);

        }
    }
}
