using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CustomPieceScriptable : ScriptableObject
{
    public Sprite Sprite;
    public string PieceName;
    public bool JumpOverPiece = false;
    public bool CanBePromoted = false;
    public List<CustomPieceScriptable> PromotedPiecesChoices;
    public bool OnlyForward = false;
    public bool OnlyBackward = false;

    public int GlobalRange = 0;
    public int VerticalRange = 0;
    public int HorizontalRange = 0;
    public int DiagonalRange = 0;

    public List<LineMove> customMovesList;
    
}


[System.Serializable]
public class LineMove 
{
    public string name;
    public List<CustomMove> Moves;
}