using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
  Mesh hexMesh;
  List<Vector3> vertices;
  List<int> triangles;
  List<Color> colors;

  MeshCollider meshCollider;

  void Awake()
  {
    GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
    hexMesh.name = "Hex Mesh";

    vertices = new List<Vector3>();
    triangles = new List<int>();
    colors = new List<Color>();

    meshCollider = gameObject.AddComponent<MeshCollider>();
  }

  public void Triangulate(HexCell[] cells)
  {
    hexMesh.Clear();
    vertices.Clear();
    triangles.Clear();
    colors.Clear();

    for(int cellIndex = 0; cellIndex < cells.Length; ++cellIndex) 
      Triangulate(cells[cellIndex]);

    hexMesh.vertices = vertices.ToArray();
    hexMesh.triangles = triangles.ToArray();
    hexMesh.colors = colors.ToArray();
    hexMesh.RecalculateNormals();

    meshCollider.sharedMesh = hexMesh;
  }

  void Triangulate(HexCell cell)
  {
    Vector3 center = cell.transform.localPosition;

    int vertexCount = HexMetrics.corners.Length;
    for(int vertexIndex = 0; vertexIndex < vertexCount; ++vertexIndex)
    {
      AddTriangle(
        center,
        center + HexMetrics.corners[vertexIndex],
        center + HexMetrics.corners[(vertexIndex + 1) % vertexCount]);
      AddTriangleColor(cell.color);
    }
  } 
  
  void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
  {
    int vertexIndex = vertices.Count;

    vertices.Add(v1);
    vertices.Add(v2);
    vertices.Add(v3);

    triangles.Add(vertexIndex);
    triangles.Add(vertexIndex + 1);
    triangles.Add(vertexIndex + 2);
  }

  void AddTriangleColor(Color color)
  {
    colors.Add(color);
    colors.Add(color);
    colors.Add(color);
  }
}
