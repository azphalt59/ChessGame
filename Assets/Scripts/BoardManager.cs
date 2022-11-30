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


    [Header("Current piece moves")]
    public Piece SelectedPiece;
    public List<int> MovementPossibilities;
    public List<int> AttackPossibilities;
    public int PlayerTurn = 1;
    

    [Header("Board data")]
    public int BoardSize = 8;
    private int columns;
    private int rows;
    private float offset = 1.25f;
    [SerializeField] public Tile[] tilesGrid;
    [SerializeField] private Transform firstTilePos;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject boardParent;

    [Header("Custom Board")]
    [SerializeField] private bool customBoard = false;
    public List<GameObject> FirstLinePiece;
    public List<GameObject> SecondLinePiece;

    [Header("UI")]
    public GameObject PawnTransformation;
    public GameObject GameOverObject;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI PlayerTurnText;
    [HideInInspector] public int Turn = 1;
    
    
    [Header("Pieces data")]
    public List<Piece> AllPieces;
    public List<Piece> P1;
    public List<Piece> P2;

    [SerializeField] private GameObject player1Pieces;
    [SerializeField] private GameObject player2Pieces;
    [SerializeField] private List<GameObject> piecePrefabs;
    
    public int pieceTest;
    public GameObject PromotedPawn;
    public Piece dataPiece;

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
        FirstLinePiece = new List<GameObject>(GetComponent<CustomBoard>().FirstLinePiece.Count);
        SecondLinePiece = new List<GameObject>(GetComponent<CustomBoard>().SecondLinePiece.Count);
        FirstLinePiece = GetComponent<CustomBoard>().FirstLinePiece;
        SecondLinePiece = GetComponent<CustomBoard>().SecondLinePiece;
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
        //GenerateTestStarterPiece();
        if(customBoard == false)
        {
            ClassicStarterBoard();
        }
        if(customBoard == true)
        {
            if(FirstLinePiece.Count == 0 && SecondLinePiece.Count == 0)
            {
                ClassicStarterBoard();
            }
            else
            {
                CustomBoard();
            }
        }
        
        
    }
    void GenerateTestStarterPiece()
    {
        // Player 1 pieces
        for (int i = 16; i < 18; i++)
        {
            GameObject piece = Instantiate(piecePrefabs[pieceTest], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
            piece.transform.SetParent(player1Pieces.transform);
            piece.GetComponent<Piece>().PlayerIndex = 1;
            piece.name = piecePrefabs[pieceTest].name + " player " + piece.GetComponent<Piece>().PlayerIndex;
            AllPieces.Add(piece.GetComponent<Piece>());
            piece.GetComponent<Piece>().CurrentTileIndex = i;
            tilesGrid[i].PlayerIndexPiece = 1;
        }

        // Player 2 pieces
        for (int i = 45; i < 47; i++)
        {
            GameObject piece2 = Instantiate(piecePrefabs[pieceTest], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
            piece2.transform.SetParent(player2Pieces.transform);
            piece2.GetComponent<Piece>().PlayerIndex = 2;
            piece2.name = piecePrefabs[pieceTest].name + " player " + piece2.GetComponent<Piece>().PlayerIndex;
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
        tilesGrid[currIndex].TileObject.GetComponent<TileObject>().thisTile.PlayerIndexPiece = 0;
        
        tilesGrid[newIndex].IsEmpty = false;
        tilesGrid[currIndex].PlayerIndexPiece = piece.PlayerIndex;

        Tile oldTile = GetTile(currIndex); 
        oldTile.PlayerIndexPiece = 0;
        Tile newTile = GetTile(newIndex);
        piece.transform.position = newTile.TileObject.transform.position;
        piece.CurrentTileIndex = newIndex;
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
    public void CustomBoard()
    {
        int pieceList = 0;
        int pieceList2 = 0;
        int pieceList3 = 0;
        int pieceList4 = 0;
       
        // First Line
        for (int i = 0; i < BoardSize ; i++)
        {
            
            if (FirstLinePiece[pieceList] != null)
            {
                GameObject piece = Instantiate(FirstLinePiece[pieceList], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
                piece.transform.SetParent(player1Pieces.transform);
                if(piece.GetComponent<Piece>() != null)
                {
                    piece.GetComponent<Piece>().PlayerIndex = 1;
                    piece.name = FirstLinePiece[pieceList].name + " player " + piece.GetComponent<Piece>().PlayerIndex;
                    AllPieces.Add(piece.GetComponent<Piece>());
                    P1.Add(piece.GetComponent<Piece>());
                    piece.GetComponent<Piece>().CurrentTileIndex = i;
                    tilesGrid[i].PlayerIndexPiece = 1;
                }
                pieceList++;
            }
        }
        // Second Line
        for (int i = BoardSize; i < BoardSize * 2; i++)
        {
            
            if (SecondLinePiece[pieceList2] != null)
            {
                GameObject piece = Instantiate(SecondLinePiece[pieceList2], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
                piece.transform.SetParent(player1Pieces.transform);
                if (piece.GetComponent<Piece>() != null)
                {
                    piece.GetComponent<Piece>().PlayerIndex = 1;
                    piece.name = SecondLinePiece[pieceList2].name + " player " + piece.GetComponent<Piece>().PlayerIndex;
                    AllPieces.Add(piece.GetComponent<Piece>());
                    P1.Add(piece.GetComponent<Piece>());
                    piece.GetComponent<Piece>().CurrentTileIndex = i;
                    tilesGrid[i].PlayerIndexPiece = 1;
                }
                pieceList2++;
            }
        }

        // First Line
        for (int i = tilesGrid.Length-1; i > tilesGrid.Length - BoardSize-1; i--)
        {
            if (FirstLinePiece[pieceList3] != null)
            {
                GameObject piece = Instantiate(FirstLinePiece[pieceList3], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
                piece.transform.SetParent(player1Pieces.transform);
                if (piece.GetComponent<Piece>() != null)
                {
                    piece.GetComponent<Piece>().PlayerIndex = 2;
                    piece.name = FirstLinePiece[pieceList3].name + " player " + piece.GetComponent<Piece>().PlayerIndex;
                    piece.GetComponent<SpriteRenderer>().color = Color.grey;
                    AllPieces.Add(piece.GetComponent<Piece>());
                    P2.Add(piece.GetComponent<Piece>());
                    piece.GetComponent<Piece>().CurrentTileIndex = i;
                    tilesGrid[i].PlayerIndexPiece = 2;
                }
                pieceList3++;
            }
        }
        //Second Line
        for (int i = tilesGrid.Length - BoardSize-1; i > tilesGrid.Length - (BoardSize * 2)-1; i--)
        {
            
            if (SecondLinePiece[pieceList4] != null)
            {
                GameObject piece = Instantiate(SecondLinePiece[pieceList4], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
                piece.transform.SetParent(player1Pieces.transform);
                if (piece.GetComponent<Piece>() != null)
                {
                    piece.GetComponent<Piece>().PlayerIndex = 2;
                    piece.name = SecondLinePiece[pieceList4].name + " player " + piece.GetComponent<Piece>().PlayerIndex;
                    piece.GetComponent<SpriteRenderer>().color = Color.grey;
                    AllPieces.Add(piece.GetComponent<Piece>());
                    P2.Add(piece.GetComponent<Piece>());
                    piece.GetComponent<Piece>().CurrentTileIndex = i;
                    tilesGrid[i].PlayerIndexPiece = 2;
                }
                pieceList4++;
            }
        }
    }
    public void ClassicStarterBoard()
    {
        // Player 1 pieces
        // First Line 
        for (int i = 0; i < BoardSize; i++)
        {
            int piecePrefabInt = 0;
            if( i==0 || i==7)
            {
                //rook
                piecePrefabInt = 0;
            }
            if (i == 1 || i == 6)
            {
                //knight
                piecePrefabInt = 2;
            }
            if (i == 2 || i == 5)
            {
                //bishop
                piecePrefabInt = 1;
            }
            if (i == BoardSize*0.5 -1)
            {
                //king
                piecePrefabInt = 3;
            }
            if (i == BoardSize*0.5)
            {
                //queen
                piecePrefabInt = 4;
            }
            GameObject piece = Instantiate(piecePrefabs[piecePrefabInt], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
            piece.transform.SetParent(player1Pieces.transform);
            piece.GetComponent<Piece>().PlayerIndex = 1;
            piece.name = piecePrefabs[piecePrefabInt].name + " player " + piece.GetComponent<Piece>().PlayerIndex;
            AllPieces.Add(piece.GetComponent<Piece>());
            P1.Add(piece.GetComponent<Piece>());
            piece.GetComponent<Piece>().CurrentTileIndex = i;
            tilesGrid[i].PlayerIndexPiece = 1;
        }
        // Second Line
        for (int i = BoardSize; i < BoardSize*2; i++)
        {
            GameObject piece = Instantiate(piecePrefabs[5], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
            piece.transform.SetParent(player1Pieces.transform);
            piece.GetComponent<Piece>().PlayerIndex = 1;
            piece.name = piecePrefabs[5].name + " player " + piece.GetComponent<Piece>().PlayerIndex;
            AllPieces.Add(piece.GetComponent<Piece>());
            P1.Add(piece.GetComponent<Piece>());
            piece.GetComponent<Piece>().CurrentTileIndex = i;
            tilesGrid[i].PlayerIndexPiece = 1;
        }

        // Player 2 pieces
        // First Line 
        for (int i = tilesGrid.Length-BoardSize; i < tilesGrid.Length; i++)
        {
            int piecePrefabInt = 10;
            if (i == tilesGrid.Length-1 || i == tilesGrid.Length -  BoardSize)
            {
                //rook
                piecePrefabInt = 0;
            }
            if (i == tilesGrid.Length-2 || i == tilesGrid.Length - (BoardSize-1))
            {
                //knight
                piecePrefabInt = 2;
            }
            if (i == tilesGrid.Length-3 || i == tilesGrid.Length - (BoardSize-2))
            {
                //bishop
                piecePrefabInt = 1;
            }
            if (i == tilesGrid.Length - BoardSize*0.5-1)
            {
                //king
                piecePrefabInt = 3;
            }
            if (i == tilesGrid.Length- BoardSize*0.5)
            {
                //queen
                piecePrefabInt = 4;
            }
            if(piecePrefabInt != 10)
            {
                GameObject piece = Instantiate(piecePrefabs[piecePrefabInt], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
                piece.transform.SetParent(player1Pieces.transform);
                piece.GetComponent<Piece>().PlayerIndex = 2;
                piece.name = piecePrefabs[piecePrefabInt].name + " player " + piece.GetComponent<Piece>().PlayerIndex;
                AllPieces.Add(piece.GetComponent<Piece>());
                P2.Add(piece.GetComponent<Piece>());
                piece.GetComponent<SpriteRenderer>().color = Color.grey;
                piece.GetComponent<Piece>().CurrentTileIndex = i;
                tilesGrid[i].PlayerIndexPiece = 2;
            }
            
        }
        // Second Line
        for (int i = tilesGrid.Length-(BoardSize*2); i < tilesGrid.Length-BoardSize; i++)
        {
            GameObject piece2 = Instantiate(piecePrefabs[5], tilesGrid[i].TileObject.transform.position, Quaternion.identity);
            piece2.transform.SetParent(player2Pieces.transform);
            piece2.GetComponent<Piece>().PlayerIndex = 2;
            piece2.name = piecePrefabs[5].name + " player " + piece2.GetComponent<Piece>().PlayerIndex;
            AllPieces.Add(piece2.GetComponent<Piece>());
            P2.Add(piece2.GetComponent<Piece>());
            piece2.GetComponent<SpriteRenderer>().color = Color.grey;
            piece2.GetComponent<Piece>().CurrentTileIndex = i;
            tilesGrid[i].PlayerIndexPiece = 2;
        }
    }
    public void UpdateTile0()
    {
        Tile tile0 = GetTile(0);
        tile0.PlayerIndexPiece = 1;
        tile0.IsEmpty = false;
    }
    
    public void GameOver(int playerIndex)
    {
        GameOverObject.SetActive(true);
        GameOverText.text = "Player " + playerIndex + " win";
    }
    public void SavePawnData(Piece dataPiece, int playerIndex, int CurrentTileIndex)
    {
        dataPiece.PlayerIndex = playerIndex;
        dataPiece.CurrentTileIndex = CurrentTileIndex;
    }
    public void PawnPromotion(Piece piece)
    {
        PromotedPawn = piece.gameObject;
        SavePawnData(dataPiece, piece.PlayerIndex, piece.CurrentTileIndex);
        PawnTransformation.SetActive(true);
        Destroy(PromotedPawn.GetComponent<Pawn>(),2);
    }
    
    public void KnightPromo()
    {
        PromotedPawn.AddComponent<Knight>();
        PromotedPawn.GetComponent<SpriteRenderer>().sprite = piecePrefabs[2].GetComponent<SpriteRenderer>().sprite;
        PawnTransformation.SetActive(false);
        PromotedPawn.GetComponent<Knight>().PlayerIndex = dataPiece.PlayerIndex;
        PromotedPawn.GetComponent<Knight>().CurrentTileIndex = dataPiece.CurrentTileIndex;

    }
    public void RookPromo()
    {
        PromotedPawn.AddComponent<Rook>();
        PromotedPawn.GetComponent<SpriteRenderer>().sprite = piecePrefabs[0].GetComponent<SpriteRenderer>().sprite;
        PawnTransformation.SetActive(false);
        PromotedPawn.GetComponent<Rook>().PlayerIndex = dataPiece.PlayerIndex;
        PromotedPawn.GetComponent<Rook>().CurrentTileIndex = dataPiece.CurrentTileIndex;
    }
    public void QueenPromo()
    {
        PromotedPawn.AddComponent<Queen>();
        PromotedPawn.GetComponent<SpriteRenderer>().sprite = piecePrefabs[4].GetComponent<SpriteRenderer>().sprite;
        PawnTransformation.SetActive(false);
        PromotedPawn.GetComponent<Queen>().PlayerIndex = dataPiece.PlayerIndex;
        PromotedPawn.GetComponent<Queen>().CurrentTileIndex = dataPiece.CurrentTileIndex;
    }
    public void BishopPromo()
    {
        PromotedPawn.AddComponent<Bishop>();
        PromotedPawn.GetComponent<SpriteRenderer>().sprite = piecePrefabs[1].GetComponent<SpriteRenderer>().sprite;
        PawnTransformation.SetActive(false);
        PromotedPawn.GetComponent<Queen>().PlayerIndex = dataPiece.PlayerIndex;
        PromotedPawn.GetComponent<Queen>().CurrentTileIndex = dataPiece.CurrentTileIndex;
        
    }
    
}
