using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public bool AlreadyPlayed = false;
    private void OnMouseDown()
    {
        if (BoardManager.Instance.PlayerTurn == PlayerIndex)
        {
            PieceMovement();
        }
    }
    public void PawnTransformation()
    {
        Debug.Log("Promote");
    }
    public override void PieceMovement()
    {
        BoardManager.Instance.SelectedPiece = this;
        BoardManager.Instance.ResetMoves();
        row = CurrentTileIndex / BoardManager.Instance.BoardSize;
        column = CurrentTileIndex % BoardManager.Instance.BoardSize;
        int tileIndexMovement;
        
        // one range mvt
        if (PlayerIndex == 2 && row - 1 >=0)
        {
            tileIndexMovement = column + (row - 1) * BoardManager.Instance.BoardSize;
            Tile mvtTile = BoardManager.Instance.GetTile(tileIndexMovement);
            if (mvtTile.IsEmpty)
            {
                BoardManager.Instance.MovementPossibilities.Add(tileIndexMovement);
                BoardManager.Instance.tilesGrid[tileIndexMovement].TileColor = Color.green;
                BoardManager.Instance.tilesGrid[tileIndexMovement].TileObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
        if (PlayerIndex == 1 && row + 1 < BoardManager.Instance.BoardSize)
        {

            tileIndexMovement = column + (row + 1) * BoardManager.Instance.BoardSize;
            Tile mvtTile = BoardManager.Instance.GetTile(tileIndexMovement);
            if (mvtTile.IsEmpty)
            {
                BoardManager.Instance.MovementPossibilities.Add(tileIndexMovement);
                BoardManager.Instance.tilesGrid[tileIndexMovement].TileColor = Color.green;
                BoardManager.Instance.tilesGrid[tileIndexMovement].TileObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }

        // two range mvt if never played
        if (PlayerIndex == 2 && row - 2 >= 0 && AlreadyPlayed == false)
        {
            int tileIndexPreviousMovement = column + (row - 1) * BoardManager.Instance.BoardSize;
            Tile previousMvtTile = BoardManager.Instance.GetTile(tileIndexPreviousMovement);
            if(previousMvtTile.IsEmpty == false)
            {
                return;
            }
            tileIndexMovement = column + (row - 2) * BoardManager.Instance.BoardSize;
            Tile mvtTile = BoardManager.Instance.GetTile(tileIndexMovement);
            if (mvtTile.IsEmpty)
            {
                BoardManager.Instance.MovementPossibilities.Add(tileIndexMovement);
                BoardManager.Instance.tilesGrid[tileIndexMovement].TileColor = Color.green;
                BoardManager.Instance.tilesGrid[tileIndexMovement].TileObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
        if (PlayerIndex == 1 && row + 2 < BoardManager.Instance.BoardSize && AlreadyPlayed == false)
        {
            int tileIndexPreviousMovement = column + (row + 1) * BoardManager.Instance.BoardSize;
            Tile previousMvtTile = BoardManager.Instance.GetTile(tileIndexPreviousMovement);
            if (previousMvtTile.IsEmpty == false)
            {
                return;
            }
            tileIndexMovement = column + (row + 2) * BoardManager.Instance.BoardSize;
            Tile mvtTile = BoardManager.Instance.GetTile(tileIndexMovement);
            if (mvtTile.IsEmpty)
            {
                BoardManager.Instance.MovementPossibilities.Add(tileIndexMovement);
                BoardManager.Instance.tilesGrid[tileIndexMovement].TileColor = Color.green;
                BoardManager.Instance.tilesGrid[tileIndexMovement].TileObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }


        if (PlayerIndex == 2 && row - 1 >= 0)
        {
            if (column + 1 < BoardManager.Instance.BoardSize)
            {
                tileIndexMovement = column + 1 + (row - 1) * BoardManager.Instance.BoardSize;
                Tile mvtTile = BoardManager.Instance.GetTile(tileIndexMovement);
                if (mvtTile.IsEmpty == false)
                {
                    BoardManager.Instance.AttackPossibilities.Add(tileIndexMovement);
                    BoardManager.Instance.tilesGrid[tileIndexMovement].TileColor = Color.red;
                    BoardManager.Instance.tilesGrid[tileIndexMovement].TileObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            if (column - 1 >= 0)
            {
                tileIndexMovement = column - 1 + (row - 1) * BoardManager.Instance.BoardSize;
                Tile mvtTile = BoardManager.Instance.GetTile(tileIndexMovement);
                if (mvtTile.IsEmpty == false)
                {
                    BoardManager.Instance.AttackPossibilities.Add(tileIndexMovement);
                    BoardManager.Instance.tilesGrid[tileIndexMovement].TileColor = Color.red;
                    BoardManager.Instance.tilesGrid[tileIndexMovement].TileObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
        if (PlayerIndex == 1 && row + 1 < BoardManager.Instance.BoardSize)
        {
            if (column + 1 < BoardManager.Instance.BoardSize)
            {
                tileIndexMovement = column + 1 + (row + 1) * BoardManager.Instance.BoardSize;
                Tile mvtTile = BoardManager.Instance.GetTile(tileIndexMovement);
                if (mvtTile.IsEmpty == false)
                {
                    BoardManager.Instance.AttackPossibilities.Add(tileIndexMovement);
                    BoardManager.Instance.tilesGrid[tileIndexMovement].TileColor = Color.red;
                    BoardManager.Instance.tilesGrid[tileIndexMovement].TileObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            if (column - 1 >= 0)
            {
                tileIndexMovement = column - 1 + (row + 1) * BoardManager.Instance.BoardSize;
                Tile mvtTile = BoardManager.Instance.GetTile(tileIndexMovement);
                if (mvtTile.IsEmpty == false)
                {
                    BoardManager.Instance.AttackPossibilities.Add(tileIndexMovement);
                    BoardManager.Instance.tilesGrid[tileIndexMovement].TileColor = Color.red;
                    BoardManager.Instance.tilesGrid[tileIndexMovement].TileObject.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }
    
}
