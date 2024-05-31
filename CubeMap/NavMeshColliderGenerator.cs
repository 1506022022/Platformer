using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.IO;
using Unity.AI.Navigation;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NavMeshColliderGenerator : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    [ContextMenu("GenerateCollider")]
    public void GenerateCollidersFromNavMesh()
    {
        // NavMesh를 생성합니다.
        navMeshSurface.BuildNavMesh();

        // 생성된 NavMesh의 정보를 가져옵니다.
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // 메시 생성
        Mesh combinedMesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // NavMesh의 삼각형을 순회하면서 콜라이더를 생성합니다.
        for (int i = 0; i < navMeshData.indices.Length; i += 3)
        {
            Vector3 vertex1 = navMeshData.vertices[navMeshData.indices[i]];
            Vector3 vertex2 = navMeshData.vertices[navMeshData.indices[i + 1]];
            Vector3 vertex3 = navMeshData.vertices[navMeshData.indices[i + 2]];

            // 삼각형의 방향을 바탕으로 콜라이더의 방향을 설정합니다.
            Vector3 normal = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).normalized;

            // 삼각형의 면적을 계산하여 콜라이더의 크기를 설정합니다.
            float area = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).magnitude / 2f;

            // 삼각형의 정점을 메시에 추가합니다.
            int baseIndex = vertices.Count;
            vertices.Add(vertex1);
            vertices.Add(vertex2);
            vertices.Add(vertex3);

            // 메시의 삼각형을 설정합니다.
            triangles.Add(baseIndex);
            triangles.Add(baseIndex + 1);
            triangles.Add(baseIndex + 2);
        }

        // 메시에 정점과 삼각형을 설정합니다.
        combinedMesh.vertices = vertices.ToArray();
        combinedMesh.triangles = triangles.ToArray();
        combinedMesh.RecalculateNormals();
        combinedMesh.RecalculateBounds();

        // 메시의 이름을 설정합니다.
        combinedMesh.name = Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) + "_CaveWallMesh";

        // 메시를 리소스 폴더에 저장합니다.
        string path = "Assets/Resources/" + combinedMesh.name + ".asset";
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(combinedMesh, path);
        AssetDatabase.SaveAssets();
#endif

        // 생성된 콜라이더를 보이지 않게 만듭니다.
        gameObject.SetActive(false);
    }
}
