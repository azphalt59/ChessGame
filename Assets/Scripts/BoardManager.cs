using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Tile
{
    public GameObject TileObject;
    public Color TileColor;
    public Color BaseColor;
    public bool IsEmpty;
    public int PlayerIndexPiece;
    public int TileIndex;
}
public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance {get; private set;}
    public int BoardSize = 8;
    private int columns;
    private int rows;
    private float offset = 1.25f;
    public int Turn = 1;
    public int PlayerTurn = 1;
    public TextMeshProUGUI PlayerTurnText;
    
    [SerializeField] public Tile[] tilesGrid;
    [SerializeField] private Transform firstTilePos;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject boardParent;
    [SerializeField] private GameObject player1Pieces;
    [SerializeField] private GameObject player2Pieces;
    [SerializeField] private List<GameObject> piecePrefabs;
    public List<Piece> AllPieces;

    public List<int> MovementPossibilities;
    public List<int> AttackPossibilities;
    public Piece SelectedPiece;

    private void Awake()
    {
        if(Instance!= null && Instance!= this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        columns = BoardSize;
        rows = BoardSize;
        GenerateTilesGrid();
    }
    private void Update()
    {
        PlayerTurnText.text = "Player Turn " + PlayerTurn;
    }
    void GenerateTilesGrid()
    {
        tilesGrid = new Tile[columns * rows];
        float rowPos;
        float colPos;
        int tileIndex;
        Vector2 tilePos;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                colPos = firstTilePos.position.x + (c * offset);
                rowPos = firstTilePos.position.y - (r * offset);
                tileIndex = (r * rows) + c;
                tilePos = new Vector2(colPos, rowPos);

                tilesGrid[tileIndex] = new Tile { TileObject = Instantiate(tilePrefab, tilePos, Quaternion.identity)};
                tilesGrid[tileIndex].TileObject.transform.SetParent(boardParent.transform);
                tilesGrid[tileIndex].IsEmpty = true;
                tilesGrid[tileIndex].TileObject.name = "Tile " + tileIndex;
                tilesGrid[tileIndex].TileIndex = tileIndex;
                tilesGrid[tileIndex].TileObject.AddComponent<TileObject>().thisTile = tilesGrid[tileIndex];
                tilesGrid[tileIndex].TileObject.AddComponent<BoxCollider2D>();
                if((c + r)%2 == 0)
                {
                    tilesGrid[tileIndex].TileColor = Color.black;          
                    tilesGrid[tileIndex].BaseColor = Color.black;          
                }
                else
                {
                    tilesGrid[tileIndex].TileColor = Color.white;
                    tilesGrid[tileIndex].BaseColor = Color.white;
                }
                tilesGrid[tileIndex].TileObject.GetComponent<SpriteRenderer>().color = tilesGrid[tileIndex].TileColor;

            }
        }
        GenerateStarterPiece();
    }
    void GenerateStarterPiece()
    {
        // Player 1 pieces
        for (int i = 8; i < 24; i++)
        {
            GameObject piece = Instantiate(piecePrefabs[1], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
            piece.transform.SetParent(player1Pieces.transform);
            piece.GetComponent<Piece>().PlayerIndex = 1;
            piece.name = piecePrefabs[1].name + " player " + piece.GetComponent<Piece>().PlayerIndex;
            AllPieces.Add(piece.GetComponent<Piece>());
            piece.GetComponent<Piece>().CurrentTileIndex = i;
            tilesGrid[i].PlayerIndexPiece = 1;
        }

        // Player 2 pieces
        for (int i = 50; i < 64; i++)
        {
            GameObject piece2 = Instantiate(piecePrefabs[1], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
            piece2.transform.SetParent(player2Pieces.transform);
            piece2.GetComponent<Piece>().PlayerIndex = 2;
            piece2.name = piecePrefabs[1].name + " player " + piece2.GetComponent<Piece>().PlayerIndex;
            AllPieces.Add(piece2.GetComponent<Piece>());
            piece2.GetComponent<SpriteRenderer>().color = Color.grey;
            piece2.GetComponent<Piece>().CurrentTileIndex = i;
            tilesGrid[i].PlayerIndexPiece = 2;
        }
    }
    public void MovePiece(int currIndex, int newIndex, Piece piece)
    {
        tilesGrid[currIndex].IsEmpty = true;
        tilesGrid[currIndex].PlayerIndexPiece = 0;
        
        tilesGrid[newIndex].IsEmpty = false;
        tilesGrid[currIndex].PlayerIndexPiece = piece.PlayerIndex;

        Tile newTile = GetTile(newIndex);
        piece.transform.position = newTile.TileObject.transform.position;
        piece.CurrentTileIndex = newIndex;
        //MovementPossibilities.Clear();
        //AttackPossibilities.Clear();
    }
    public void StartPositionPiece(int currIndex, Piece piece)
    {
        MovePiece(0, currIndex, piece);
    }

    public Tile GetTile(int tileIndex)
    {
        return tilesGrid[tileIndex];
    }
    public Piece GetPiece()
    {
        if(SelectedPiece != null)
        {
            return SelectedPiece;
        }
        else
        {
            return null;
        }
        
    }
    public void ResetMoves()
    {
        foreach (int tileIndex in MovementPossibilities)
        {
            tilesGrid[tileIndex].TileObject.GetComponent<SpriteRenderer>().color = tilesGrid[tileIndex].BaseColor;
            tilesGrid[tileIndex].TileObject.GetComponent<TileObject>().spriteColor = tilesGrid[tileIndex].BaseColor;
        }
        MovementPossibilities.Clear();
        foreach (int tileIndex in AttackPossibilities)
        {
            tilesGrid[tileIndex].TileObject.GetComponent<SpriteRenderer>().color = tilesGrid[tileIndex].BaseColor;
            tilesGrid[tileIndex].TileObject.GetComponent<TileObject>().spriteColor = tilesGrid[tileIndex].BaseColor;
        }
        AttackPossibilities.Clear();
    }
    public void UpdatePlayerTurn()
    {
        PlayerTurn++;
        if(PlayerTurn >= 3)
        {
            PlayerTurn = 1;
            Turn++;
        }
    }
}
