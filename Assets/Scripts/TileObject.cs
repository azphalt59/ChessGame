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
            /*foreach (Piece piece in BoardManager.Instance.AllPieces)
            {
                if (piece.CurrentTileIndex == thisTile.TileIndex && piece.CurrentTileIndex != BoardManager.Instance.GetPiece().CurrentTileIndex)
                {
                    Debug.Log(piece.name + " " +  piece.PlayerIndex);
                    Destroy(piece.gameObject);
                    BoardManager.Instance.AllPieces.Remove(piece);
                }
            }*/
            for (int piece = 0; piece < BoardManager.Instance.AllPieces.Count; piece++)
            {
                if (BoardManager.Instance.AllPieces[piece].CurrentTileIndex == thisTile.TileIndex && BoardManager.Instance.AllPieces[piece].CurrentTileIndex != BoardManager.Instance.GetPiece().CurrentTileIndex)
                {
                    target = BoardManager.Instance.AllPieces[piece];
                }
            }
            Debug.Log(target.name + " à la position " + target.CurrentTileIndex + " appartenant au joueur " + target.PlayerIndex);
            BoardManager.Instance.AllPieces.Remove(target);
            Destroy(target.gameObject);
            thisTile.PlayerIndexPiece = BoardManager.Instance.GetPiece().PlayerIndex;
            BoardManager.Instance.MovePiece(BoardManager.Instance.GetPiece().CurrentTileIndex, thisTile.TileIndex, BoardManager.Instance.GetPiece());
            BoardManager.Instance.ResetMoves();
            
            return;
        }
        if(spriteColor == Color.green)
        {
            Debug.Log("move");
            BoardManager.Instance.UpdatePlayerTurn();
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
