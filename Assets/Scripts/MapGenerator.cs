using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Section Settings")]
    public GameObject firstSectionPrefab;
    public GameObject[] sectionPrefabs;
    public int initialSections = 3;

    [Header("Spawn Settings")]
    // MODIFIED: sectionWidth를 실제 프리팹 크기에 맞게 조절하세요
    public float sectionWidth = 15f;
    // MODIFIED: sectionGap을 조절하여 섹션 간격 설정
    public float sectionGap = 5f;
    public float mapSpeed = 5f;
    public float spawnDistanceFromCamera = 20f;
    public float spawnHeight = 0f;

    // MODIFIED: 디버그용 설정 추가
    [Header("Debug Settings")]
    public bool showDebugLogs = true;
    public bool showGizmos = true;
    public Color gizmoColor = Color.green;

    private List<GameObject> spawnedSections = new List<GameObject>();
    private Camera mainCamera;
    // MODIFIED: 마지막 스폰 위치 추적
    private float lastSpawnX = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("[MapGen] 메인 카메라를 찾을 수 없습니다!");
            return;
        }

        // MODIFIED: 프리팹 검증 강화
        if (firstSectionPrefab == null)
        {
            Debug.LogError("[MapGen] FirstSection 프리팹이 할당되지 않았습니다!");
            return;
        }

        if (sectionPrefabs == null || sectionPrefabs.Length == 0)
        {
            Debug.LogError("[MapGen] Section 프리팹이 할당되지 않았습니다!");
            return;
        }

        // MODIFIED: 각 프리팹의 실제 크기 측정 및 로그
        if (showDebugLogs)
        {
            Debug.Log($"[MapGen] === 초기 설정 ===");
            Debug.Log($"[MapGen] FirstSection: {firstSectionPrefab.name}");
            Debug.Log($"[MapGen] Section 프리팹 개수: {sectionPrefabs.Length}");
            Debug.Log($"[MapGen] Section Width: {sectionWidth}");
            Debug.Log($"[MapGen] Section Gap: {sectionGap}");
            Debug.Log($"[MapGen] 총 섹션 간격: {sectionWidth + sectionGap}");
        }

        float startX = 0f;
        lastSpawnX = startX;

        // 첫 번째 섹션 생성
        SpawnFirstSection(startX);

        // MODIFIED: 나머지 섹션들 생성 (간격 적용)
        for (int i = 1; i < initialSections; i++)
        {
            float spawnX = lastSpawnX + sectionWidth + sectionGap;
            SpawnSection(spawnX);
            lastSpawnX = spawnX;
        }

        if (showDebugLogs)
        {
            Debug.Log($"[MapGen] 초기 섹션 생성 완료. 총 {spawnedSections.Count}개");
        }
    }

    void Update()
    {
        if (mainCamera == null) return;

        GameObject rightmostSection = GetRightmostSection();

        if (rightmostSection != null)
        {
            float rightmostX = rightmostSection.transform.position.x;
            float cameraRightEdge = mainCamera.transform.position.x + spawnDistanceFromCamera;

            // MODIFIED: 새 섹션 생성 조건 확인
            if (rightmostX < cameraRightEdge)
            {
                float spawnX = rightmostX + sectionWidth + sectionGap;
                SpawnSection(spawnX);
                lastSpawnX = spawnX;
            }
        }
        else if (spawnedSections.Count == 0)
        {
            // MODIFIED: 섹션이 없을 경우 비상 생성
            float spawnX = mainCamera.transform.position.x + spawnDistanceFromCamera;
            SpawnSection(spawnX);
            lastSpawnX = spawnX;
        }

        DeleteOldSections();
    }

    void SpawnSection(float xPosition)
    {
        if (sectionPrefabs.Length == 0)
        {
            Debug.LogError("[MapGen] Section 프리팹이 없습니다!");
            return;
        }

        int randomIndex = Random.Range(0, sectionPrefabs.Length);
        GameObject selectedSection = sectionPrefabs[randomIndex];

        if (selectedSection == null)
        {
            Debug.LogError($"[MapGen] Section 프리팹 [{randomIndex}]이 null입니다!");
            return;
        }

        Vector3 spawnPosition = new Vector3(xPosition, spawnHeight, 0);
        GameObject newSection = Instantiate(selectedSection, spawnPosition, Quaternion.identity);

        // MODIFIED: 섹션 이름에 인덱스 추가 (디버깅 용이)
        newSection.name = $"{selectedSection.name}_#{spawnedSections.Count}";

        SectionController controller = newSection.AddComponent<SectionController>();
        controller.moveSpeed = mapSpeed;

        spawnedSections.Add(newSection);

        if (showDebugLogs)
        {
            // MODIFIED: 이전 섹션과의 실제 거리 계산
            float actualGap = 0f;
            if (spawnedSections.Count > 1)
            {
                GameObject prevSection = spawnedSections[spawnedSections.Count - 2];
                actualGap = xPosition - prevSection.transform.position.x;
            }

            Debug.Log($"[MapGen] 섹션 생성: {selectedSection.name} (프리팹 {randomIndex + 1}/{sectionPrefabs.Length})\n" +
                      $"  위치: X={xPosition:F1}\n" +
                      $"  이전 섹션과의 거리: {actualGap:F1}\n" +
                      $"  설정된 간격: {sectionWidth + sectionGap:F1}\n" +
                      $"  총 섹션 수: {spawnedSections.Count}");
        }
    }

    void SpawnFirstSection(float xPosition)
    {
        Vector3 spawnPosition = new Vector3(xPosition, spawnHeight, 0);
        GameObject newSection = Instantiate(firstSectionPrefab, spawnPosition, Quaternion.identity);
        newSection.name = $"{firstSectionPrefab.name}_#0";

        SectionController controller = newSection.AddComponent<SectionController>();
        controller.moveSpeed = mapSpeed;

        spawnedSections.Add(newSection);

        if (showDebugLogs)
        {
            Debug.Log($"[MapGen] FirstSection 생성: {firstSectionPrefab.name} at X={xPosition}");
        }
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
        if (mainCamera == null) return;

        // MODIFIED: 삭제 거리를 섹션 크기를 고려하여 동적으로 계산
        float deleteDistance = (sectionWidth + sectionGap) * 2f;
        float cameraLeftEdge = mainCamera.transform.position.x - deleteDistance;

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
                string sectionName = oldSection.name;

                spawnedSections.RemoveAt(i);
                Destroy(oldSection);

                if (showDebugLogs)
                {
                    Debug.Log($"[MapGen] 섹션 삭제: {sectionName}, 남은 섹션: {spawnedSections.Count}");
                }
            }
        }
    }

    // MODIFIED: 런타임에서 간격 조절 가능한 메서드들
    [ContextMenu("간격 +1 증가")]
    public void IncreaseGap()
    {
        sectionGap += 1f;
        Debug.Log($"[MapGen] 섹션 간격 증가: {sectionGap}");
    }

    [ContextMenu("간격 -1 감소")]
    public void DecreaseGap()
    {
        sectionGap = Mathf.Max(0, sectionGap - 1f);
        Debug.Log($"[MapGen] 섹션 간격 감소: {sectionGap}");
    }

    [ContextMenu("현재 설정 출력")]
    public void PrintCurrentSettings()
    {
        Debug.Log("=== MapGenerator 현재 설정 ===");
        Debug.Log($"프리팹 개수: {sectionPrefabs.Length}");
        Debug.Log($"Section Width: {sectionWidth}");
        Debug.Log($"Section Gap: {sectionGap}");
        Debug.Log($"총 간격: {sectionWidth + sectionGap}");
        Debug.Log($"Map Speed: {mapSpeed}");
        Debug.Log($"Initial Sections: {initialSections}");
        Debug.Log($"현재 생성된 섹션: {spawnedSections.Count}");
        Debug.Log($"마지막 스폰 X: {lastSpawnX}");
    }

    // MODIFIED: 섹션 실제 크기 측정 도구
    [ContextMenu("프리팹 크기 측정")]
    public void MeasurePrefabSizes()
    {
        Debug.Log("=== 프리팹 크기 측정 ===");

        if (firstSectionPrefab != null)
        {
            Bounds bounds = CalculatePrefabBounds(firstSectionPrefab);
            Debug.Log($"FirstSection ({firstSectionPrefab.name}): Width = {bounds.size.x}");
        }

        for (int i = 0; i < sectionPrefabs.Length; i++)
        {
            if (sectionPrefabs[i] != null)
            {
                Bounds bounds = CalculatePrefabBounds(sectionPrefabs[i]);
                Debug.Log($"Section[{i}] ({sectionPrefabs[i].name}): Width = {bounds.size.x}");
            }
        }

        Debug.Log($"\n권장 sectionWidth: 프리팹의 실제 너비와 동일하게 설정");
        Debug.Log($"권장 sectionGap: 3~10 사이 값 (원하는 간격에 따라 조절)");
    }

    // MODIFIED: 프리팹의 실제 바운드 계산
    private Bounds CalculatePrefabBounds(GameObject prefab)
    {
        Bounds bounds = new Bounds(prefab.transform.position, Vector3.zero);
        Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }

    // MODIFIED: Scene 뷰에서 섹션 위치 시각화
    private void OnDrawGizmos()
    {
        if (!showGizmos || mainCamera == null) return;

        Gizmos.color = gizmoColor;

        // 현재 생성된 섹션들의 위치 표시
        foreach (GameObject section in spawnedSections)
        {
            if (section != null)
            {
                Vector3 pos = section.transform.position;
                Gizmos.DrawWireCube(pos, new Vector3(sectionWidth, 5f, 1f));

                // 섹션 간격 표시
                Gizmos.DrawLine(
                    new Vector3(pos.x + sectionWidth / 2, pos.y, pos.z),
                    new Vector3(pos.x + sectionWidth / 2 + sectionGap, pos.y, pos.z)
                );
            }
        }

        // 카메라 스폰 범위 표시
        if (mainCamera != null)
        {
            Gizmos.color = Color.yellow;
            float spawnLine = mainCamera.transform.position.x + spawnDistanceFromCamera;
            Gizmos.DrawLine(
                new Vector3(spawnLine, -10f, 0),
                new Vector3(spawnLine, 10f, 0)
            );
        }
    }
}

public class SectionController : MonoBehaviour
{
    public float moveSpeed = 10f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
}