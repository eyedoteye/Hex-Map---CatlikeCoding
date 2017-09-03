using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
  Mesh hexMesh;
  List<Vector3> vertices;
  List<int> triangles;

  void Awake()
  {
    GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
    hexMesh.name = "Hex Mesh";
    vertices = new List<Vector3>();
    triangles = new List<int>();
  }

  public void Triangulate(HexCell[] cells)
  {
    hexMesh.Clear();
    vertices.Clear();
    triangles.Clear();

    for(int cellIndex = 0; cellIndex < cells.Length; ++cellIndex) 
      Triangulate(cells[cellIndex]);

    hexMesh.vertices = vertices.ToArray();
    hexMesh.triangles = triangles.ToArray();
    hexMesh.RecalculateNormals();
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
}
