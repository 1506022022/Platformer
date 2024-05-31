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
        // NavMesh�� �����մϴ�.
        navMeshSurface.BuildNavMesh();

        // ������ NavMesh�� ������ �����ɴϴ�.
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // �޽� ����
        Mesh combinedMesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // NavMesh�� �ﰢ���� ��ȸ�ϸ鼭 �ݶ��̴��� �����մϴ�.
        for (int i = 0; i < navMeshData.indices.Length; i += 3)
        {
            Vector3 vertex1 = navMeshData.vertices[navMeshData.indices[i]];
            Vector3 vertex2 = navMeshData.vertices[navMeshData.indices[i + 1]];
            Vector3 vertex3 = navMeshData.vertices[navMeshData.indices[i + 2]];

            // �ﰢ���� ������ �������� �ݶ��̴��� ������ �����մϴ�.
            Vector3 normal = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).normalized;

            // �ﰢ���� ������ ����Ͽ� �ݶ��̴��� ũ�⸦ �����մϴ�.
            float area = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).magnitude / 2f;

            // �ﰢ���� ������ �޽ÿ� �߰��մϴ�.
            int baseIndex = vertices.Count;
            vertices.Add(vertex1);
            vertices.Add(vertex2);
            vertices.Add(vertex3);

            // �޽��� �ﰢ���� �����մϴ�.
            triangles.Add(baseIndex);
            triangles.Add(baseIndex + 1);
            triangles.Add(baseIndex + 2);
        }

        // �޽ÿ� ������ �ﰢ���� �����մϴ�.
        combinedMesh.vertices = vertices.ToArray();
        combinedMesh.triangles = triangles.ToArray();
        combinedMesh.RecalculateNormals();
        combinedMesh.RecalculateBounds();

        // �޽��� �̸��� �����մϴ�.
        combinedMesh.name = Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) + "_CaveWallMesh";

        // �޽ø� ���ҽ� ������ �����մϴ�.
        string path = "Assets/Resources/" + combinedMesh.name + ".asset";
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(combinedMesh, path);
        AssetDatabase.SaveAssets();
#endif

        // ������ �ݶ��̴��� ������ �ʰ� ����ϴ�.
        gameObject.SetActive(false);
    }
}
