using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    // Start is called before the first frame update
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

        //Rook  moves
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
        for (int r = row + 1; r < BoardManager.Instance.BoardSize; r++)
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
        for (int c = column + 1; c < BoardManager.Instance.BoardSize; c++)
        {
            tileIndexMovement = CurrentTileIndex - column + c;

            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                break;
            }
        }


        //Bishop moves
        // bot left
        for (int r = 1; r < (BoardManager.Instance.BoardSize - row); r++)
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
        // Top left
        for (int r = 1; r <= row; r++)
        {
            tileIndexMovement = BoardManager.Instance.BoardSize * (row - r) + column - r;
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
        // Top right
        for (int r = 1; r <= row; r++)
        {
            tileIndexMovement = BoardManager.Instance.BoardSize * (row - r) + column + r;
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

        //bot right
        for (int r = 1; r < (BoardManager.Instance.BoardSize - row); r++)
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

    public void BishopMoves()
    {
        

        int tileIndexMovement;
        bool IsAttackmove = false;
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
    }
    public void RookMoves()
    {
        BoardManager.Instance.SelectedPiece = this;
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
        for (int r = row + 1; r < BoardManager.Instance.BoardSize; r++)
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
        for (int c = column + 1; c < BoardManager.Instance.BoardSize; c++)
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
