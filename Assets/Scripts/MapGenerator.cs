using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("섹션 설정")]
    public GameObject[] sectionPrefabs;
    public int initialSections = 3;

    [Header("스폰 설정")]
    public float sectionWidth = 15f;
    public float mapSpeed = 10f;

    private float nextSpawnX = 0f;
    private List<GameObject> spawnedSections = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialSections; i++)
        {
            SpawnSection();
        }
    }

    void Update()
    {
        if (nextSpawnX < Camera.main.transform.position.x + 50f)
        {
            SpawnSection();
            DeleteOldSections();
        }
    }

    void SpawnSection()
    {
        int randomIndex = Random.Range(0, sectionPrefabs.Length);
        GameObject selectedSection = sectionPrefabs[randomIndex];

        Vector3 spawnPosition = new Vector3(nextSpawnX, 0, 0);
        GameObject newSection = Instantiate(selectedSection, spawnPosition, Quaternion.identity);

        // 섹션 이동 컴포넌트 추가
        SectionController controller = newSection.AddComponent<SectionController>();
        controller.moveSpeed = mapSpeed;

        spawnedSections.Add(newSection);
        nextSpawnX += sectionWidth;

        Debug.Log($"섹션 생성: {selectedSection.name} at X={spawnPosition.x}");
    }

    void DeleteOldSections()
    {
        for (int i = spawnedSections.Count - 1; i >= 0; i--)
        {
            if (spawnedSections[i].transform.position.x < Camera.main.transform.position.x - 30f)
            {
                GameObject oldSection = spawnedSections[i];
                spawnedSections.RemoveAt(i);
                Destroy(oldSection);
            }
        }
    }
}

public class SectionController : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Update()
    {
        // 왼쪽으로 이동
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}