using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    public float spawnInterval = 5f;


    public GameObject damagingCirclePrefab;
    public Transform player;
    public float minDelay = 1f;
    public float maxDelay = 3f;

    public static bool shakouuu = false;

    public int speed = 5;

    public GameObject enemyPrefab;
    private float spawnTimer = 0f;

    void Start()
    {
        StartCoroutine(SpawnProjectiles());
        StartCoroutine(SpawnDamagingCircleCoroutine());
    }

    void Update()
    {
        // Calculate direction from boss to player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move the boss towards the player
        transform.Translate(direction * speed * Time.deltaTime);
        // Spawn enemies periodically
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }
    void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 3;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    IEnumerator SpawnProjectiles()
    {
        while (true)
        {
            for (int i = 0; i < 19; i++)
            {
                float angle = i * 20f;
                Vector2 direction = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
                FireProjectile(direction);

                yield return null;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    IEnumerator SpawnDamagingCircleCoroutine()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
            Vector2 randomOffset = Random.insideUnitCircle.normalized * Random.Range(1f, 4f);
            Vector3 spawnPosition = player.position + (Vector3)randomOffset;
            Instantiate(damagingCirclePrefab, spawnPosition, Quaternion.identity);
            shakouuu = true;
        }
    }
    void FireProjectile(Vector2 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }
}

