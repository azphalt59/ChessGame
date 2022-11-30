using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomMove
{
    public int RawMovement;
    public int ColMovement;
    public bool JumpOverPiece;
}

public class CustomPiece : Piece
{
    public int GlobalRange;
    public int VerticalRange;
    public int HorizontalRange;
    public int DiagonalRange;
    public CustomPieceScriptable customPiece;

    
    public override void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = customPiece.Sprite;
        gameObject.name = customPiece.PieceName;
        GlobalRange = customPiece.GlobalRange;
        VerticalRange = customPiece.VerticalRange;
        HorizontalRange = customPiece.HorizontalRange;
        DiagonalRange = customPiece.DiagonalRange;
        BoardManager.Instance.StartPositionPiece(CurrentTileIndex, this);
        BoardManager.Instance.UpdateTile0();
    }
    public void ReloadData()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = customPiece.Sprite;
        gameObject.name = customPiece.PieceName;
        GlobalRange = customPiece.GlobalRange;
        VerticalRange = customPiece.VerticalRange;
        HorizontalRange = customPiece.HorizontalRange;
        DiagonalRange = customPiece.DiagonalRange;
        BoardManager.Instance.ResetMoves();
    }
    private void OnMouseDown()
    {
        if (BoardManager.Instance.PlayerTurn == PlayerIndex)
        {
            PieceMovement();
        }
    }
    public void CustomPromotion()
    {
        if(customPiece.PromotedPiecesChoices.Count == 1)
        {
            customPiece = customPiece.PromotedPiecesChoices[0];
        }
        else
        {
            //wip customPiece = 
        }
        ReloadData();
    }
    public override void PieceMovement()
    {
        BoardManager.Instance.ResetMoves();
        DiagonalMvt();
        RookMvtLike();
        CustomMoves();
    }   
    public void CustomMoves()
    {
        int tileIndexMovement;
        bool IsAttackmove;
        foreach(CustomMove customMove in customPiece.customMoves)
        {
            if(PlayerIndex == 1)
            {
                tileIndexMovement = CurrentTileIndex + customMove.ColMovement + customMove.RawMovement * BoardManager.Instance.BoardSize;
                IsAttackmove = MovementOrAttack(tileIndexMovement);
            }
            if (PlayerIndex == 2)
            {
                tileIndexMovement = CurrentTileIndex - customMove.ColMovement - customMove.RawMovement * BoardManager.Instance.BoardSize;
                IsAttackmove = MovementOrAttack(tileIndexMovement);
            }
        }
    }
    public void DiagonalMvt()
    {
        BoardManager.Instance.SelectedPiece = this;
        
        row = CurrentTileIndex / BoardManager.Instance.BoardSize;
        column = CurrentTileIndex % BoardManager.Instance.BoardSize;
        int tileIndexMovement;
        bool IsAttackmove = false;

        ///// Diagonal mvt
        // bot left
        for (int r = 1; r < (BoardManager.Instance.BoardSize - row); r++)
        {
            if (customPiece.DiagonalRange == 0)
            {
                break;
            }
            if (customPiece.OnlyForward == true && PlayerIndex == 2)
            {
                break;
            }
            if (customPiece.OnlyBackward == true && PlayerIndex == 1)
            {
                break;
            }

            tileIndexMovement = column - r + (row + r) * BoardManager.Instance.BoardSize;

            if ((tileIndexMovement + 1) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                if (customPiece.JumpOverPiece == false)
                {
                    break;
                }
            }
            if (r == customPiece.DiagonalRange)
            {
                break;
            }
        }
        // Top left
        for (int r = 1; r <= row; r++)
        {
            if (customPiece.DiagonalRange == 0)
            {
                break;
            }
            if (customPiece.OnlyForward == true && PlayerIndex == 1)
            {
                break;
            }
            if (customPiece.OnlyBackward == true && PlayerIndex == 2)
            {
                break;
            }
            tileIndexMovement = BoardManager.Instance.BoardSize * (row - r) + column - r;
            if ((tileIndexMovement + 1) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                if (customPiece.JumpOverPiece == false)
                {
                    break;
                }
            }
            if (r == customPiece.DiagonalRange)
            {
                break;
            }
        }
        // Top right
        for (int r = 1; r <= row; r++)
        {
            if (customPiece.DiagonalRange == 0)
            {
                break;
            }
            if (customPiece.OnlyForward == true && PlayerIndex == 1)
            {
                break;
            }
            if (customPiece.OnlyBackward == true && PlayerIndex == 2)
            {
                break;
            }
            tileIndexMovement = BoardManager.Instance.BoardSize * (row - r) + column + r;
            if ((tileIndexMovement) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                if (customPiece.JumpOverPiece == false)
                {
                    break;
                }
            }
            if (r == customPiece.DiagonalRange)
            {
                break;
            }
        }

        //bot right
        for (int r = 1; r < (BoardManager.Instance.BoardSize - row); r++)
        {
            if (customPiece.DiagonalRange == 0)
            {
                break;
            }
            if (customPiece.OnlyForward == true && PlayerIndex == 2)
            {
                break;
            }
            if (customPiece.OnlyBackward == true && PlayerIndex == 1)
            {
                break;
            }
            tileIndexMovement = column + r + (row + r) * BoardManager.Instance.BoardSize;
            if ((tileIndexMovement) % BoardManager.Instance.BoardSize == 0)
            {
                break;
            }
            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                if (customPiece.JumpOverPiece == false)
                {
                    break;
                }
            }
            if (r == customPiece.DiagonalRange)
            {
                break;
            }
        }
    }
    public void RookMvtLike()
    {
        BoardManager.Instance.SelectedPiece = this;
        row = CurrentTileIndex / BoardManager.Instance.BoardSize;
        column = CurrentTileIndex % BoardManager.Instance.BoardSize;

        int tileIndexMovement;
        bool IsAttackmove = false;
        //bottom
        for (int r = 1; r <= row; r++)
        {
            if (customPiece.VerticalRange == 0)
            {
                break;
            }
            if (customPiece.OnlyForward == true && PlayerIndex == 2)
            {
                break;
            }
            if (customPiece.OnlyBackward == true && PlayerIndex == 1)
            {
                break;
            }
            tileIndexMovement = CurrentTileIndex - (r * BoardManager.Instance.BoardSize);

            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                if (customPiece.JumpOverPiece == false)
                {
                    break;
                }
            }
            if (r == customPiece.VerticalRange)
            {
                break;
            }
        }
        //top
        for (int r = row + 1; r < BoardManager.Instance.BoardSize; r++)
        {
            if (customPiece.VerticalRange == 0)
            {
                break;
            }
            if (customPiece.OnlyForward == true && PlayerIndex == 1)
            {
                break;
            }
            if (customPiece.OnlyBackward == true && PlayerIndex == 2)
            {
                break;
            }
            tileIndexMovement = column + (r * BoardManager.Instance.BoardSize);

            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                if (customPiece.JumpOverPiece == false)
                {
                    break;
                }
            }
            if (r == customPiece.VerticalRange)
            {
                break;
            }
        }
        //left
        for (int c = 1; c <= column; c++)
        {
            if (customPiece.HorizontalRange == 0)
            {
                break;
            }

            tileIndexMovement = CurrentTileIndex - c;

            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                if (customPiece.JumpOverPiece == false)
                {
                    break;
                }
            }
            if (c == customPiece.HorizontalRange)
            {
                break;
            }
        }
        //right
        for (int c = column + 1; c < BoardManager.Instance.BoardSize; c++)
        {
            if (customPiece.HorizontalRange == 0)
            {
                break;
            }
            tileIndexMovement = CurrentTileIndex - column + c;

            IsAttackmove = MovementOrAttack(tileIndexMovement);
            if (IsAttackmove == true)
            {
                if (customPiece.JumpOverPiece == false)
                {
                    break;
                }
            }
            if (c == customPiece.HorizontalRange)
            {
                break;
            }
        }
    }
}
