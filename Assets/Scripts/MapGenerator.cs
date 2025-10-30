using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("섹션 설정")]
    public GameObject firstSectionPrefab; // 수정: 게임 시작 시 반드시 생성될 첫 번째 섹션
    public GameObject[] sectionPrefabs; // 수정: 랜덤으로 생성될 섹션들
    public int initialSections = 3;

    [Header("스폰 설정")]
    public float sectionWidth = 15f;
    public float mapSpeed = 5f;
    public float spawnDistanceFromCamera = 20f; // 카메라로부터 얼마나 먼리서 생성할지
    public float spawnHeight = 0f; // 수정: 섹션 생성 Y 위치 (Ground Y 좌표와 맞춤)

    private List<GameObject> spawnedSections = new List<GameObject>();
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // 수정: 첫 번째 섹션은 반드시 FirstSection 생성 (캐릭터 시작 위치)
        float startX = 0f; // 캐릭터가 있는 위치에서 시작

        if (firstSectionPrefab != null)
        {
            SpawnFirstSection(startX);
            Debug.Log("FirstSection 생성 완료!");
        }
        else
        {
            Debug.LogError("FirstSection 프리팹이 할당되지 않았습니다!");
        }

        // 나머지 초기 섹션들은 랜덤 생성
        for (int i = 1; i < initialSections; i++)
        {
            SpawnSection(startX + (i * sectionWidth));
        }
    }

    void Update()
    {
        // 가장 오른쪽 섹션의 위치 확인
        GameObject rightmostSection = GetRightmostSection();

        if (rightmostSection != null)
        {
            float rightmostX = rightmostSection.transform.position.x;
            float cameraRightEdge = mainCamera.transform.position.x + spawnDistanceFromCamera;

            // 가장 오른쪽 섹션이 화면 가까이 오면 새로운 섹션 생성
            if (rightmostX < cameraRightEdge)
            {
                SpawnSection(rightmostX + sectionWidth);
            }
        }
        else if (spawnedSections.Count == 0)
        {
            // 섹션이 하나도 없으면 생성
            SpawnSection(mainCamera.transform.position.x + spawnDistanceFromCamera);
        }

        DeleteOldSections();
    }

    void SpawnSection(float xPosition)
    {
        if (sectionPrefabs.Length == 0)
        {
            Debug.LogError("섹션 프리팹이 할당되지 않았습니다!");
            return;
        }

        int randomIndex = Random.Range(0, sectionPrefabs.Length);
        GameObject selectedSection = sectionPrefabs[randomIndex];

        Vector3 spawnPosition = new Vector3(xPosition, spawnHeight, 0); // 수정: spawnHeight 사용
        GameObject newSection = Instantiate(selectedSection, spawnPosition, Quaternion.identity);

        // 섹션 이동 컴포넌트 추가
        SectionController controller = newSection.AddComponent<SectionController>();
        controller.moveSpeed = mapSpeed;

        spawnedSections.Add(newSection);

        Debug.Log($"섹션 생성: {selectedSection.name} at X={xPosition}, 현재 섹션 수: {spawnedSections.Count}");
    }

    // 수정: 첫 번째 섹션 전용 생성 메서드
    void SpawnFirstSection(float xPosition)
    {
        Vector3 spawnPosition = new Vector3(xPosition, spawnHeight, 0); // 수정: spawnHeight 사용
        GameObject newSection = Instantiate(firstSectionPrefab, spawnPosition, Quaternion.identity);

        // 섹션 이동 컴포넌트 추가
        SectionController controller = newSection.AddComponent<SectionController>();
        controller.moveSpeed = mapSpeed;

        spawnedSections.Add(newSection);

        Debug.Log($"FirstSection 생성: {firstSectionPrefab.name} at X={xPosition}");
    }

    GameObject GetRightmostSection()
    {
        GameObject rightmost = null;
        float maxX = float.MinValue;

        foreach (GameObject section in spawnedSections)
        {
            if (section != null && section.transform.position.x > maxX)
            {
                maxX = section.transform.position.x;
                rightmost = section;
            }
        }

        return rightmost;
    }

    void DeleteOldSections()
    {
        float cameraLeftEdge = mainCamera.transform.position.x - 30f;

        for (int i = spawnedSections.Count - 1; i >= 0; i--)
        {
            if (spawnedSections[i] == null)
            {
                spawnedSections.RemoveAt(i);
                continue;
            }

            if (spawnedSections[i].transform.position.x < cameraLeftEdge)
            {
                GameObject oldSection = spawnedSections[i];
                spawnedSections.RemoveAt(i);
                Destroy(oldSection);
                Debug.Log($"섹션 삭제, 남은 섹션 수: {spawnedSections.Count}");
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