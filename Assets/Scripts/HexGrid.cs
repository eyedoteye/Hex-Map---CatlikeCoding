using UnityEngine;
using UnityEngine.UI;

public class HexGrid: MonoBehaviour
{
  public int width = 6;
  public int height = 6;

  public Color defaultColor = Color.white;

  public HexCell cellPrefab;

  HexCell[] cells;

  public Text cellLabelPrefab;

  Canvas gridCanvas;
  HexMesh hexMesh;

  void Awake()
  {
    gridCanvas = GetComponentInChildren<Canvas>();
    hexMesh = GetComponentInChildren<HexMesh>();

    cells = new HexCell[height * width];

    for(int z = 0, i = 0; z < height; z++)
    {
      for(int x = 0; x < width; x++)
      {
        CreateCell(x, z, i++);
      }
    }
  }

  void Start()
  {
    hexMesh.Triangulate(cells);
  }

  void CreateCell(int x, int z, int i)
  {
    Vector3 position;
    position.x = (x + z * 0.5f - z / 2) * (HexMetrics.radiusToEdge * 2f);
    position.y = 0f;
    position.z = z * (HexMetrics.radiusToVertex * 1.5f);

    HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
    cell.transform.SetParent(transform, false);
    cell.transform.localPosition = position;
    cell.coordinates = HexCoordinates.FromZigzagCoordinates(x, z);
    cell.color = defaultColor;

    Text label = Instantiate<Text>(cellLabelPrefab);
    label.rectTransform.SetParent(gridCanvas.transform, false);
    label.rectTransform.anchoredPosition =
      new Vector2(position.x, position.z);
    label.text = cell.coordinates.ToStringOnSeparateLines();
  }

//  void Update()
//  {
//    if(Input.GetMouseButton(0))
//      HandleInput();
//  }
//
//  void HandleInput()
//  {
//    Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
//    RaycastHit hit;
//    if(Physics.Raycast(inputRay, out hit))
//      TouchCell(hit.point);
//  }

  public void ColorCell(Vector3 position, Color color)
  {
    position = transform.InverseTransformPoint(position);
    HexCoordinates coordinates = HexCoordinates.FromPosition(position);
    int index = coordinates.x + coordinates.z * width + coordinates.z / 2;

    HexCell cell = cells[index];
    cell.color = color;
    hexMesh.Triangulate(cells);
  }
}
