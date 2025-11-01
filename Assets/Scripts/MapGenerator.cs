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
    public float sectionWidth = 15f;
    public float sectionGap = 20f;
    public float mapSpeed = 5f; // 기본 속도 (Player가 없을 때 사용)
    public float spawnDistanceFromCamera = 20f;
    public float spawnHeight = 0f;

    // 수정: Player와 Difficulty 연동을 위한 참조 추가
    [Header("Speed Control References")]
    [Tooltip("Player의 CurrentSpeed를 참조하여 맵 속도 동기화")]
    public GamePlayer player;
    [Tooltip("난이도별 속도 배율 적용 (선택사항)")]
    public DifficultyManager difficultyManager;

    [Header("Debug Settings")]
    public bool showDebugLogs = true;
    public bool showGizmos = true;
    public Color gizmoColor = Color.green;

    private List<GameObject> spawnedSections = new List<GameObject>();
    private Camera mainCamera;
    private float lastSpawnX = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("[MapGen] 메인 카메라를 찾을 수 없습니다!");
            return;
        }

        // 수정: Player 참조 확인
        if (player == null)
        {
            Debug.LogWarning("[MapGen] GamePlayer가 할당되지 않았습니다! 씬에서 자동 검색합니다.");
            player = FindObjectOfType<GamePlayer>();
            if (player == null)
            {
                Debug.LogWarning("[MapGen] GamePlayer를 찾을 수 없습니다. 기본 속도를 사용합니다.");
            }
        }

        // 수정: DifficultyManager 참조 확인
        if (difficultyManager == null)
        {
            difficultyManager = FindObjectOfType<DifficultyManager>();
            if (difficultyManager == null)
            {
                Debug.LogWarning("[MapGen] DifficultyManager를 찾을 수 없습니다. 난이도 배율이 적용되지 않습니다.");
            }
        }

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

        if (showDebugLogs)
        {
            Debug.Log($"[MapGen] === 초기 설정 ===");
            Debug.Log($"[MapGen] FirstSection: {firstSectionPrefab.name}");
            Debug.Log($"[MapGen] Section 프리팹 개수: {sectionPrefabs.Length}");
            Debug.Log($"[MapGen] Section Width: {sectionWidth}");
            Debug.Log($"[MapGen] Section Gap: {sectionGap}");
            Debug.Log($"[MapGen] 총 섹션 간격: {sectionWidth + sectionGap}");
            Debug.Log($"[MapGen] Player 연동: {(player != null ? "ON" : "OFF")}");
            Debug.Log($"[MapGen] Difficulty 연동: {(difficultyManager != null ? "ON" : "OFF")}");
        }

        float startX = 0f;
        lastSpawnX = startX;

        SpawnFirstSection(startX);

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

        // 수정: 모든 섹션의 속도를 Player의 CurrentSpeed와 동기화
        UpdateAllSectionSpeeds();

        GameObject rightmostSection = GetRightmostSection();

        if (rightmostSection != null)
        {
            float rightmostX = rightmostSection.transform.position.x;
            float cameraRightEdge = mainCamera.transform.position.x + spawnDistanceFromCamera;

            if (rightmostX < cameraRightEdge)
            {
                float spawnX = rightmostX + sectionWidth + sectionGap;
                SpawnSection(spawnX);
                lastSpawnX = spawnX;
            }
        }
        else if (spawnedSections.Count == 0)
        {
            float spawnX = mainCamera.transform.position.x + spawnDistanceFromCamera;
            SpawnSection(spawnX);
            lastSpawnX = spawnX;
        }

        DeleteOldSections();
    }

    // 수정: Player의 CurrentSpeed를 기반으로 현재 맵 속도 계산
    private float GetCurrentMapSpeed()
    {
        float baseSpeed = mapSpeed;

        // Player의 CurrentSpeed 적용
        if (player != null)
        {
            baseSpeed = player.CurrentSpeed;
        }

        // DifficultyManager의 속도 배율 적용
        if (difficultyManager != null)
        {
            baseSpeed *= difficultyManager.CurrentSpeedMultiplier;
        }

        return baseSpeed;
    }

    // 수정: 모든 섹션의 속도를 실시간으로 업데이트
    private void UpdateAllSectionSpeeds()
    {
        float currentSpeed = GetCurrentMapSpeed();

        foreach (GameObject section in spawnedSections)
        {
            if (section != null)
            {
                SectionController controller = section.GetComponent<SectionController>();
                if (controller != null)
                {
                    controller.moveSpeed = currentSpeed;
                }
            }
        }
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

        newSection.name = $"{selectedSection.name}_#{spawnedSections.Count}";

        SectionController controller = newSection.AddComponent<SectionController>();
        // 수정: 생성 시에도 현재 속도 적용
        controller.moveSpeed = GetCurrentMapSpeed();

        spawnedSections.Add(newSection);

        if (showDebugLogs)
        {
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
                      $"  현재 속도: {GetCurrentMapSpeed():F2}\n" +
                      $"  총 섹션 수: {spawnedSections.Count}");
        }
    }

    void SpawnFirstSection(float xPosition)
    {
        Vector3 spawnPosition = new Vector3(xPosition, spawnHeight, 0);
        GameObject newSection = Instantiate(firstSectionPrefab, spawnPosition, Quaternion.identity);
        newSection.name = $"{firstSectionPrefab.name}_#0";

        SectionController controller = newSection.AddComponent<SectionController>();
        // 수정: 생성 시에도 현재 속도 적용
        controller.moveSpeed = GetCurrentMapSpeed();

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
        Debug.Log($"기본 Map Speed: {mapSpeed}");
        Debug.Log($"현재 Map Speed: {GetCurrentMapSpeed():F2}");
        Debug.Log($"Initial Sections: {initialSections}");
        Debug.Log($"현재 생성된 섹션: {spawnedSections.Count}");
        Debug.Log($"마지막 스폰 X: {lastSpawnX}");
        Debug.Log($"Player 연동: {(player != null ? "ON" : "OFF")}");
        if (player != null)
        {
            Debug.Log($"  Player CurrentSpeed: {player.CurrentSpeed:F2}");
        }
        Debug.Log($"Difficulty 연동: {(difficultyManager != null ? "ON" : "OFF")}");
        if (difficultyManager != null)
        {
            Debug.Log($"  Difficulty Multiplier: {difficultyManager.CurrentSpeedMultiplier:F2}x");
        }
    }

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

    private void OnDrawGizmos()
    {
        if (!showGizmos || mainCamera == null) return;

        Gizmos.color = gizmoColor;

        foreach (GameObject section in spawnedSections)
        {
            if (section != null)
            {
                Vector3 pos = section.transform.position;
                Gizmos.DrawWireCube(pos, new Vector3(sectionWidth, 5f, 1f));

                Gizmos.DrawLine(
                    new Vector3(pos.x + sectionWidth / 2, pos.y, pos.z),
                    new Vector3(pos.x + sectionWidth / 2 + sectionGap, pos.y, pos.z)
                );
            }
        }

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