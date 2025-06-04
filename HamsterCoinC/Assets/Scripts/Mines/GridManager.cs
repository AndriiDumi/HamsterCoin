using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform gridParent;
    public int width = 5;
    public int height = 5;

    [HideInInspector]
    public int mineCount = 5;  // Тепер кількість мін можна змінювати зверху

    private Cell[,] grid;
    public MinesGameManager gameManager;

    void Start()
    {
        if (mineCount >= width * height)
        {
            mineCount = width * height - 1;
            Debug.LogWarning("Занадто багато мін! Зменшено до максимально допустимої кількості.");
        }
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        grid = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject obj = Instantiate(cellPrefab, gridParent);
                Cell cell = obj.GetComponent<Cell>();
                cell.Init(x, y, gameManager);
                grid[x, y] = cell;
            }
        }

        PlaceMines();
    }

    void PlaceMines()
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                positions.Add(new Vector2Int(x, y));

        for (int i = 0; i < positions.Count; i++)
        {
            Vector2Int temp = positions[i];
            int randomIndex = Random.Range(i, positions.Count);
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }

        for (int i = 0; i < mineCount; i++)
        {
            Vector2Int pos = positions[i];
            grid[pos.x, pos.y].hasMine = true;
        }

        Debug.Log("Міни розставлені успішно");
    }

    public void RevealAllCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = grid[x, y];
                if (cell.hasMine)
                    cell.cellImage.sprite = cell.bombSprite;
                else
                    cell.cellImage.sprite = cell.safeSprite;

                cell.button.interactable = false;
            }
        }
    }

    public void RegenerateGrid()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        GenerateGrid();
    }

    public void DisableAllCells()
    {
        if (grid == null) return;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y].Disable();
                }
            }
        }
    }
}
