using UnityEngine;

[System.Serializable]
public struct HexCoordinates
{
  public int x { get; private set; }
  public int z { get; private set; }

  public HexCoordinates(int x, int z)
  {
    this.x = x;
    this.z = z;
  }
  
  public override string ToString()
  {
    return "(" + x.ToString() + ", " + z.ToString() + ")";
  }
  
  public string ToStringOnSeparateLines()
  {
    return x.ToString() + "\n" + z.ToString();
  }
 
  public static HexCoordinates FromZigzagCoordinates(int x, int z)
  {
    return new HexCoordinates(x - z / 2, z);
  }
}
