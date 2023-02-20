using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    GameDataSO data;

    [SerializeField]
    Transform[] spawnPoints;

    [SerializeField]
    GameObject[] enemys;

    [SerializeField]
    float spawnTimer;
    float currentTime = 1;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        spawnTimer = data.spawnValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (HudController.HC.IsOver())
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            List<Transform> avaliablePoints = new List<Transform>();
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if ((spawnPoints[i].position - player.position).magnitude > 10f)
                    avaliablePoints.Add(spawnPoints[i]);
            }

            int point = Random.Range(0, avaliablePoints.Count);
            Instantiate(enemys[Random.Range(0, enemys.Length)], avaliablePoints[point].position, Quaternion.identity);

            currentTime = 12 / spawnTimer;
        }
    }
}
