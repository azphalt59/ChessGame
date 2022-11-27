using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    private void OnMouseDown()
    {
        if (BoardManager.Instance.PlayerTurn == PlayerIndex)
        {
            PieceMovement();
        }
    }
        
    public override void PieceMovement()
    {
        BoardManager.Instance.SelectedPiece = this;
        BoardManager.Instance.ResetMoves();
        row = CurrentTileIndex / BoardManager.Instance.BoardSize;
        column = CurrentTileIndex % BoardManager.Instance.BoardSize;
        int tileIndexMovement;

        // first bot line
        if(row +1 < BoardManager.Instance.BoardSize)
        {
            if(column+2 < BoardManager.Instance.BoardSize)
            {
                tileIndexMovement = column + 2 + (row + 1) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
            if (column - 2 >= 0)
            {
                tileIndexMovement = column - 2 + (row + 1) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
        }

        // second bot line
        if (row + 2 < BoardManager.Instance.BoardSize)
        {
            if (column + 1 < BoardManager.Instance.BoardSize)
            {
                tileIndexMovement = column + 1 + (row + 2) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
            if (column - 1 >= 0)
            {
                tileIndexMovement = column - 1 + (row + 2) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
        }

        // first top line
        if (row - 1 >= 0)
        {
            if (column + 2 < BoardManager.Instance.BoardSize)
            {
                tileIndexMovement = column + 2 + (row - 1) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
            if (column - 2 >= 0)
            {
                tileIndexMovement = column - 2 + (row - 1) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
        }
        // second top line
        if (row - 2 >= 0)
        {
            if (column + 1 < BoardManager.Instance.BoardSize)
            {
                tileIndexMovement = column + 1 + (row - 2) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
            if (column - 1 >= 0)
            {
                tileIndexMovement = column - 1 + (row - 2) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
        }


    }
}
