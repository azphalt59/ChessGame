using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public Tile thisTile;
    public Color spriteColor;
    public Piece target;
    private void Update()
    {
        spriteColor = this.gameObject.GetComponent<SpriteRenderer>().color;
    }
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        if(spriteColor == Color.red)
        {
            BoardManager.Instance.UpdatePlayerTurn();
            
            for (int piece = 0; piece < BoardManager.Instance.AllPieces.Count; piece++)
            {
                if (BoardManager.Instance.AllPieces[piece].CurrentTileIndex == thisTile.TileIndex && BoardManager.Instance.AllPieces[piece].CurrentTileIndex != BoardManager.Instance.GetPiece().CurrentTileIndex)
                {
                    target = BoardManager.Instance.AllPieces[piece];
                }
            }
            Debug.Log(target.name + " à la position " + target.CurrentTileIndex + " appartenant au joueur " + target.PlayerIndex);
            if (BoardManager.Instance.GetPiece().gameObject.GetComponent<Pawn>() != null)
            {
                BoardManager.Instance.GetPiece().gameObject.GetComponent<Pawn>().AlreadyPlayed = true;
            }
            BoardManager.Instance.AllPieces.Remove(target);
            if(target.gameObject.GetComponent<King>() != null)
            {
                BoardManager.Instance.GameOver(BoardManager.Instance.GetPiece().PlayerIndex);
            }
            Destroy(target.gameObject);
            thisTile.PlayerIndexPiece = BoardManager.Instance.GetPiece().PlayerIndex;
            BoardManager.Instance.GetTile(BoardManager.Instance.GetPiece().CurrentTileIndex).PlayerIndexPiece = 0;
            BoardManager.Instance.GetTile(BoardManager.Instance.GetPiece().CurrentTileIndex).TileObject.GetComponent<TileObject>().thisTile.PlayerIndexPiece = 0;
            BoardManager.Instance.MovePiece(BoardManager.Instance.GetPiece().CurrentTileIndex, thisTile.TileIndex, BoardManager.Instance.GetPiece());
            if (BoardManager.Instance.GetPiece().gameObject.GetComponent<Pawn>() != null)
            {
                if (BoardManager.Instance.GetPiece().PlayerIndex == 1 && thisTile.TileIndex >= (BoardManager.Instance.BoardSize * BoardManager.Instance.BoardSize) -BoardManager.Instance.BoardSize)
                {
                    BoardManager.Instance.GetPiece().gameObject.GetComponent<Pawn>().PawnTransformation();
                }
                if (BoardManager.Instance.GetPiece().PlayerIndex == 2 && thisTile.TileIndex <= BoardManager.Instance.BoardSize)
                {
                    BoardManager.Instance.GetPiece().gameObject.GetComponent<Pawn>().PawnTransformation();
                }
            }
            BoardManager.Instance.ResetMoves();
            
            return;
        }
        if(spriteColor == Color.green)
        {
            Debug.Log("move");
            BoardManager.Instance.UpdatePlayerTurn();
            if (BoardManager.Instance.GetPiece().gameObject.GetComponent<Pawn>() != null)
            {
                BoardManager.Instance.GetPiece().gameObject.GetComponent<Pawn>().AlreadyPlayed = true;
            }
            thisTile.PlayerIndexPiece = BoardManager.Instance.GetPiece().PlayerIndex;
            
            BoardManager.Instance.MovePiece(BoardManager.Instance.GetPiece().CurrentTileIndex, thisTile.TileIndex, BoardManager.Instance.GetPiece());
            BoardManager.Instance.ResetMoves();
            return;
        }
        if (spriteColor == Color.white || spriteColor == Color.black)
        {
            if (thisTile.IsEmpty)
            {
                BoardManager.Instance.ResetMoves();
                return;
            }
            
        }
        
    }
}
