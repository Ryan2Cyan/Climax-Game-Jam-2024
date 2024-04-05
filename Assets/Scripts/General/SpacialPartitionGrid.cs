using System.Collections.Generic;
using UnityEngine;

namespace General
{
    // public class SpacialPartitionGrid : MonoBehaviour
    // {
    //     public List<List<EnemySpacialPartitioning>> Cells = new();
    //     public SpacialPartitionGrid Instance;
    //     public Vector2 MaxBounds = new (500, 500);
    //     public int NumberOfCells = 10;
    //     public float CellSize = 5;
    //
    //     #region UnityFunctions
    //
    //     private void Awake()
    //     {
    //         Instance = this;
    //     }
    //
    //     #endregion
    //
    //     #region PublicFunctions
    //
    //     /// <remarks>Source: https://gameprogrammingpatterns.com/spatial-partition.html</remarks>
    //     public void AddUnit(EnemySpacialPartitioning unit)
    //     {
    //         // Determine which grid cell it's in:
    //         var Position = unit.transform.position;
    //         var cell = new Vector2Int((int) (Position.x / CellSize), (int)(Position.y / CellSize));
    //         
    //         // Add to the front of list for the cell it's in:
    //         unit.PreviousUnit = null;
    //         unit.NextUnit = Cells[cell.x][cell.y];
    //         Cells[cell.x][cell.y] = unit;
    //
    //         // Add into the chain of units for that cell:
    //         if (unit.NextUnit != null) unit.NextUnit.PreviousUnit = unit;
    //     }
    //     
    //     public Vector2Int GetCell(Vector3 position)
    //     {
    //         return Vector2Int.zero;        
    //     }
    //
    //     #endregion
    //
    //     #region PrivateFunctions
    //
    //     private void InitialiseGrid()
    //     {
    //          
    //         for (var i = 0; i < NumberOfCells; i++)
    //         {
    //             for (var j = 0; j < NumberOfCells; j++)
    //             {
    //                 
    //             }
    //         }
    //     }
    //
    //     #endregion
    /*}*/
}
