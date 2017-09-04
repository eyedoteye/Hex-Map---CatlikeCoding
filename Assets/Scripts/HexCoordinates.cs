using UnityEngine;

[System.Serializable]
public struct HexCoordinates
{
  [SerializeField]
  private int _x, _z;

  public int x
  {
    get { return _x; }
  }
  public int z
  {
    get { return _z; }
  }
  public int y
  {
    get { return -_x - _z; }
  }

  public HexCoordinates(int x, int z)
  {
    this._x = x;
    this._z = z;
  }
 
  public override string ToString()
  {
    return
      "(" + _x.ToString() + ", " + y.ToString() + ", " + _z.ToString() + ")";
  }
  
  public string ToStringOnSeparateLines()
  {
    return _x.ToString() + "\n" + y.ToString() + "\n" + _z.ToString();
  }
 
  public static HexCoordinates FromZigzagCoordinates(int x, int z)
  {
    return new HexCoordinates(x - z / 2, z);
  }

  private static int RoundToInt(float x)
  {
    return (int)Mathf.Floor(x + 0.5f);
  }

  public static HexCoordinates FromPosition(Vector3 position)
  {
    float x = position.x / (HexMetrics.radiusToEdge * 2f);
    float y = -x;

    float offset = position.z / (HexMetrics.radiusToVertex * 3f);
    x -= offset;
    y -= offset;

    int iX = Mathf.RoundToInt(x);
    int iY = Mathf.RoundToInt(y);
    int iZ = Mathf.RoundToInt(-x - y);

    if(iX + iY + iZ != 0)
    {
      float dX = Mathf.Abs(x - iX);
      float dY = Mathf.Abs(y - iY);
      float dZ = Mathf.Abs(-x - y - iZ);

      if(dX > dY && dX > dZ)
        iX = -iY - iZ;
      else if(dZ > dY)
        iZ = -iX - iY;
    }

    return new HexCoordinates(iX, iZ);
  }
}
