using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
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
        /*
        //top left Good
        for (int r = 1; r <= row; r++)
        { 
            tileIndexMovement = column - r + (row - r) * BoardManager.Instance.BoardSize;
            if ((tileIndexMovement + 1) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }
        ////bot right
        for (int r = 1; r <= BoardManager.Instance.BoardSize - row; r++)
        {
            tileIndexMovement = column + r + (row + r) * BoardManager.Instance.BoardSize;
            if ((tileIndexMovement) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }
        //bot left Good
        for (int r = 1; r <= BoardManager.Instance.BoardSize - row; r++)
        {
            tileIndexMovement = column - r + (row + r) * BoardManager.Instance.BoardSize;
            if ((tileIndexMovement + 1) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }
        ////top right
        for (int r = 1; r <= row; r++)
        {
            tileIndexMovement = column + r + (row - r) * BoardManager.Instance.BoardSize;
            if ((tileIndexMovement) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }
        */


        // bot left
        for (int r = 1; r < (BoardManager.Instance.BoardSize - row); r++)
        {
            tileIndexMovement = column - r + (row + r) * BoardManager.Instance.BoardSize;
            if ((tileIndexMovement+1) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }
        // Top left
        for (int r = 1; r <=row; r++)
        {
            tileIndexMovement = BoardManager.Instance.BoardSize*(row-r) + column - r ;
            if ((tileIndexMovement+1) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }
        // Top right
        for (int r = 1; r <= row; r++)
        {
            tileIndexMovement = BoardManager.Instance.BoardSize * (row - r) + column + r;
            if ((tileIndexMovement ) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }

        //bot right
        for (int r = 1; r < (BoardManager.Instance.BoardSize-row); r++)
        {
            tileIndexMovement = column + r + (row + r) * BoardManager.Instance.BoardSize;
            if ((tileIndexMovement) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }

    }
}
