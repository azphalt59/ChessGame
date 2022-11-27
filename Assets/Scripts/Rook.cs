using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
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
        bool IsAttackmove = false;
        //bottom
        for (int r = 1; r <= row; r++)
        {
            tileIndexMovement = CurrentTileIndex - (r * BoardManager.Instance.BoardSize);
            
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }
        //top
        for (int r = row+1; r < BoardManager.Instance.BoardSize; r++)
        {
            tileIndexMovement = column + (r * BoardManager.Instance.BoardSize);
            
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }
        //left
        for (int c = 1; c <= column; c++)
        {
            tileIndexMovement = CurrentTileIndex - c;
            
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }
        //right
        for (int c = column+1; c < BoardManager.Instance.BoardSize; c++)
        {
            tileIndexMovement = CurrentTileIndex - column + c;
            
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }
    }
}
