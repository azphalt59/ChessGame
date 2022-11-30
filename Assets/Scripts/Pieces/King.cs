using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
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

        // top line
        if(row-1>=0)
        {
            if(column+1 < BoardManager.Instance.BoardSize)
            {
                tileIndexMovement = column + 1 + (row-1) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
            if (column - 1 >= 0)
            {
                tileIndexMovement = column - 1 + (row-1) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
            tileIndexMovement = column + (row-1)* BoardManager.Instance.BoardSize;
            MovementOrAttack(tileIndexMovement);
        }

        // bot line
        if (row + 1 < BoardManager.Instance.BoardSize)
        {
            if (column + 1 < BoardManager.Instance.BoardSize)
            {
                tileIndexMovement = column + 1 + (row+1) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
            if (column - 1 >= 0)
            {
                tileIndexMovement = column - 1 + (row+1) * BoardManager.Instance.BoardSize;
                MovementOrAttack(tileIndexMovement);
            }
            tileIndexMovement = column + (row + 1) * BoardManager.Instance.BoardSize;
            MovementOrAttack(tileIndexMovement);
        }

        // left move & right move
        if (column + 1 < BoardManager.Instance.BoardSize)
        {
            tileIndexMovement = column + 1 + (row) * BoardManager.Instance.BoardSize;
            MovementOrAttack(tileIndexMovement);
        }
        if (column - 1 >= 0)
        {
            tileIndexMovement = column - 1 + (row) * BoardManager.Instance.BoardSize;
            MovementOrAttack(tileIndexMovement);
        }

    }
}
