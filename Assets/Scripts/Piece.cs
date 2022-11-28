using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int PlayerIndex;
    public int CurrentTileIndex;

    
    public int row;
    public int column;
    // Start is called before the first frame update
    void Start()
    {
        BoardManager.Instance.StartPositionPiece(CurrentTileIndex, this);
        BoardManager.Instance.UpdateTile0();
    }
    
    public virtual void PieceMovement()
    {

    }
    public bool MovementOrAttack(int mvtTileIndex)
    {
        bool AnotherPiece = false;
        Tile mvtTile = BoardManager.Instance.GetTile(mvtTileIndex);
        if (mvtTile.IsEmpty)
        {
            BoardManager.Instance.MovementPossibilities.Add(mvtTileIndex);
            BoardManager.Instance.tilesGrid[mvtTileIndex].TileColor = Color.green;
            BoardManager.Instance.tilesGrid[mvtTileIndex].TileObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            if (mvtTile.PlayerIndexPiece != PlayerIndex)
            {
                BoardManager.Instance.AttackPossibilities.Add(mvtTileIndex);
                BoardManager.Instance.tilesGrid[mvtTileIndex].TileColor = Color.red;
                BoardManager.Instance.tilesGrid[mvtTileIndex].TileObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            if(mvtTile.PlayerIndexPiece == PlayerIndex)
            {

            }
            AnotherPiece = true;
        }
        return AnotherPiece;
    }

    
}
