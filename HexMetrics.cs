using UnityEngine;

public class HexMetrics : MonoBehaviour
{
  public const float radiusToVertex = 10f;
  public const float radiusToEdge = radiusToVertex * 0.866025404f;

  public static Vector3[] corners =
  {
    new Vector3(0f, 0f, radiusToVertex),
    new Vector3(radiusToEdge, 0f, 0.5f * radiusToVertex),
    new Vector3(radiusToEdge, 0f, -0.5f * radiusToVertex),
    new Vector3(0f, 0f, -radiusToVertex),
    new Vector3(-radiusToEdge, 0f, -0.5f * radiusToVertex),
    new Vector3(-radiusToEdge, 0f, 0.5f * radiusToVertex)
  }; 
}
