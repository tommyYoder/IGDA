using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {
    public static BoardManager Instance { set; get;}
    private bool[,] allowedMoves { set; get;}

    public Chessman [,] Chessmans { set; get;}
    private Chessman selectedChessman;
    private Chessman movingChessman;

    private const float tileSize = 1.0f;
    private const float tileOffset = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public int[] enPassantMove { set; get;}

    private Quaternion Blackorientation = Quaternion.Euler(0, 180, 0);
    private Quaternion Whiteorientation = Quaternion.Euler(0, 0, 0);

    //public Button button;
    public static bool mouseDown;

    public List<GameObject> chessmanPrefabs;
    public List<GameObject> activeChessMan;

    private Material previousMat;
    public Material selectedMat;

    public bool isWhiteTurn = true;
    public bool PieceSwitcherup = false;
    public EventSystem PieceSwitcherEventSystem;
    public Dictionary<string, int> SwitchPiecesDictionary = new Dictionary<string, int>()
    {
        {"WHITE QUEEN", 1 },
        {"WHITE ROOK", 2 },
        {"WHITE BISHOP", 3 },
        {"WHITE KNIGHT", 4 },
        {"BLACK QUEEN", 8 },
        {"BLACK ROOK", 9 },
        {"BLACK BISHOP", 10 },
        {"BLACK KNIGHT", 11 }
    };

    private int SwitchedPieceX;
    private int SwitchedPieceY;
    private bool WhiteCalling;
    private bool isMoving;
    private float MoveInterp;
    private Vector3 curPos;
    private Vector3 destination;
    private float distance;

    private bool isTurning;
    private Quaternion curRot;
    private Quaternion lookRot;
    private float RotInterp;

    private int ChosenSpotX;
    private int ChosenSpotY;

   
    public AudioSource LeatherSound;
    public AudioSource CarrieSound;
    public AudioSource MikeSound;
    public AudioSource FreddySound;
    public AudioSource JasonSound;
    public AudioSource RunSound;

    public SimpleTouchAreaButton areaButton;

    private float fogDensity;
    public GameObject DeathParticles;

    public float FogDensity
    {
        get
        {
            return fogDensity;
        }
        set
        {
            fogDensity = value;
        }
    }

    // Makes this script usable to other scripts and calls upon the spawn all chest mans class.
    private void Start()
    {
        Instance = this;
        SpawnAllChestMans();
        GameManager.InitializeGame();
        MoveInterp = 0;
        RotInterp = 0;
       
    }

    // Draws the board, updates slections made class, and looks for when the mouse button is down to make a move in the X and Y directions.
    void Update()
    {
        UpdatedSelection();
        drawChessboard();

        if (Input.GetMouseButtonDown (0))
        {
            if (!PieceSwitcherup)
            {
                if (!isMoving)
                {
                    if (selectionX >= 0 && selectionY >= 0)
                    {
                        ChosenSpotX = selectionX;
                        ChosenSpotY = selectionY;
                        if (selectedChessman == null)
                        {
                            // Select the chessman
                            SelectedChessman(selectionX, selectionY);
                        }
                        else
                        {
                            // Move the chessman
                            MoveChessman(selectionX, selectionY);
                        }
                    }
                }
            }
        }
        if (isTurning)
        {
            movingChessman.transform.rotation = Quaternion.Slerp(curRot, lookRot, RotInterp);
            RotInterp += Time.deltaTime;
        }

        if(RotInterp > 1)
        {
            RotInterp = 0;
            isTurning = false;
            isMoving = true;
        }

        /*if ((movingChessman != null) && (movingChessman.transform.position == destination))
        {
            print("I am called");
            if(movingChessman.GetType() == typeof(Pawn))
            {
                activeChessMan.Remove(movingChessman.gameObject);
                Destroy(selectedChessman.gameObject);
                GameHUDManager.instance.ActivatePieceSwitcherUI();
                PieceSwitcherup = true;
            }
        }*/

        if (movingChessman != null)
        {
            if (isMoving)
            {
                movingChessman.transform.position = Vector3.Lerp(curPos, destination, MoveInterp);
                MoveInterp += (Time.deltaTime / (distance / 3));
            }

            if (MoveInterp > 1)
            {

                isMoving = false;
                MoveInterp = 0;


                print(movingChessman.GetType().ToString());

                if (movingChessman.GetType() == typeof(Pawn))
                {
                    if (ChosenSpotY == 7)
                    {

                        activeChessMan.Remove(movingChessman.gameObject);
                        Destroy(movingChessman.gameObject);
                        GameHUDManager.instance.ActivatePieceSwitcherUI();
                        PieceSwitcherup = true;
                        SwitchedPieceX = ChosenSpotX;
                        SwitchedPieceY = ChosenSpotY;
                        WhiteCalling = true;
                        //SpawnedChessMan(4, x, y);
                        //selectedChessman = Chessmans[x, y];


                    }
                    else if (ChosenSpotY == 0)
                    {
                        activeChessMan.Remove(movingChessman.gameObject);
                        Destroy(movingChessman.gameObject);
                        GameHUDManager.instance.ActivatePieceSwitcherUI();
                        PieceSwitcherup = true;
                        SwitchedPieceX = ChosenSpotX;
                        SwitchedPieceY = ChosenSpotY;
                        WhiteCalling = false;
                        //SpawnedChessMan(10, x, y);
                        //selectedChessman = Chessmans[x, y];

                    }
                }

            }
        }
    }

    // Updates if selected or not.
    private void SelectedChessman(int x, int y)
    {
        //Is there is no chess piece at this spot.
        if (Chessmans[x, y] == null)
        return;
       

        //is the piece is opposite of the player who has the turn
        if (Chessmans[x,y].isWhite != isWhiteTurn)
        return;
      

        // If selection has been made, then allowed moves becomes true.
        bool hasAtLeastOneMove = false;
        allowedMoves = Chessmans[x, y].PossibleMove();
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (allowedMoves[i, j])
                    hasAtLeastOneMove = true;
        // Sets this back to false when completed.
        if (!hasAtLeastOneMove)
            return;
        // Looks for selected chess piece, changes material to highlight the piece, and then shows the highlight where player can move.
        selectedChessman = Chessmans[x, y];
       // previousMat = selectedChessman.GetComponent<MeshRenderer>().material;
        //selectedMat.mainTexture = previousMat.mainTexture;
        //selectedChessman.GetComponent<MeshRenderer>().material = selectedMat;
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }

    // Moves each chess piece in the allowed moves.
    void MoveChessman(int x, int y)
    {
        if (allowedMoves[x,y])
        {
            //Take reference of the chessman at the cooridinates
            Chessman c = Chessmans[x, y];
            
            //if there is a chess piece there and it is a different color than the current player take it
                if(c != null && c.isWhite != isWhiteTurn)
            {
                c.isTarget = true;
                // Captured a piece

                // If it's the king
                /*if (c.GetType() == typeof(King))
                {
                    EndGame();
                    return;
                }
                //activeChessMan.Remove(c.gameObject);
                //Destroy(c.gameObject);

                if (isWhiteTurn)
                {
                    GameManager.BlackPlayerRemovePiece(c.GetType().ToString());
                    int points = GameManager.GetPieceWorth(c.GetType().ToString());
                    GameManager.WhitePlayerAddScore(points);
                }
                else
                {
                    GameManager.WhitePlayerRemovePiece(c.GetType().ToString());
                    int points = GameManager.GetPieceWorth(c.GetType().ToString());
                    GameManager.BlackPlayerAddScore(points);
                }*/
            }
            // Can only move once if it's either the player or enemys turn.
            if (x == enPassantMove[0] && y == enPassantMove[1])
            {
                if (isWhiteTurn)
                {
                    c = Chessmans [x, y-1];
                    activeChessMan.Remove(c.gameObject);
                    Destroy(c.gameObject);

                }
                else
                {
                    c = Chessmans[x, y + 1];
                    activeChessMan.Remove(c.gameObject);
                    Destroy(c.gameObject);
                }
            }
            enPassantMove[0] =  - 1;
            enPassantMove[1] =  - 1;

            if (selectedChessman.GetType() == typeof(King))

                if (isWhiteTurn)
                {
                    RunSound.Play();
                }
                else
                {
                    LeatherSound.Play();
                }
            if (selectedChessman.GetType() == typeof(Queen))
                if (isWhiteTurn)
                {
                    RunSound.Play();
                }
                else
                {
                    CarrieSound.Play();
                }

            if (selectedChessman.GetType() == typeof(Rook))
                if (isWhiteTurn)
                {
                    RunSound.Play();
                }
                else
                {
                    MikeSound.Play();
                }

            if (selectedChessman.GetType() == typeof(Bishop))
                if (isWhiteTurn)
                {
                    RunSound.Play();
                }
                else
                {
                    FreddySound.Play();
                }

            if (selectedChessman.GetType() == typeof(Knight))
                if (isWhiteTurn)
                {
                    RunSound.Play();
                }
                else
                {
                    JasonSound.Play();
                }

            // If Pawn is selected on either side and you are able to take it over on the right side, then move is possible to destroy.
            if (selectedChessman.GetType() == typeof(Pawn))
            {
                RunSound.Play();
                /*(y == 7)
                {
                   
                        /*activeChessMan.Remove(selectedChessman.gameObject);
                        Destroy(selectedChessman.gameObject);
                        GameHUDManager.instance.ActivatePieceSwitcherUI();
                        PieceSwitcherup = true;
                        SwitchedPieceX = x;
                        SwitchedPieceY = y;
                        WhiteCalling = true;
                        //SpawnedChessMan(4, x, y);
                        //selectedChessman = Chessmans[x, y];
                    
                    
                }
               else if (y == 0)
                {
                    /*activeChessMan.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    GameHUDManager.instance.ActivatePieceSwitcherUI();
                    PieceSwitcherup = true;
                    SwitchedPieceX = x;
                    SwitchedPieceY = y;
                    WhiteCalling = false;
                    //SpawnedChessMan(10, x, y);
                    //selectedChessman = Chessmans[x, y];

                }*/
                // Calls again to only move once on either side.
                if (selectedChessman.CurrentY == 1 && y == 3)
                {
                    enPassantMove[0] = x;
                    enPassantMove[1] = y -1;
                }
                else if (selectedChessman.CurrentY == 6 && y == 4)
                {
                    enPassantMove[0] = x;
                    enPassantMove[1] = y + 1;
                }
       }
            // Allows chess piece to move when selected tile is true.
            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
            //selectedChessman.transform.position = GetTileCenter(x, y);
            movingChessman = selectedChessman;
            curPos = selectedChessman.transform.position;
            curRot = selectedChessman.transform.rotation;
            destination = GetTileCenter(x, y);
            Vector3 direction = destination - selectedChessman.transform.position;
            //print(destination.ToString());
            float angle = Vector3.Angle(selectedChessman.transform.forward, direction);
            Vector3 relativeX = selectedChessman.transform.InverseTransformPoint(GetTileCenter(x, y));

            if(relativeX.x < 0)
            {
                angle = -angle;
            }

            lookRot = Quaternion.Euler(selectedChessman.transform.rotation.eulerAngles.x, selectedChessman.transform.rotation.eulerAngles.y + angle, selectedChessman.transform.rotation.eulerAngles.z);
            distance = Vector3.Distance(curPos, destination);
            if (curRot != lookRot)
            {
                isTurning = true;
            }
            else
            {
                isMoving = true;
            }
            selectedChessman.SetPosition(x, y);
            Chessmans [x, y] = selectedChessman;
            if (isWhiteTurn)
                GameManager.WhitePlayerIncrementMoveCount();
            else
                GameManager.BlackPlayerIncrementMoveCount();
            isWhiteTurn = !isWhiteTurn;
            CameraControlScript.instance.MoveCameraToStartingPosition(isWhiteTurn);
        }

        // If move has been made, then material is reseted, board highlight is set to false, and chess piece selection is set to false.
        //selectedChessman.GetComponent<MeshRenderer>().material = previousMat;
        BoardHighlights.Instance.HideHighlights();
        selectedChessman = null;

        
        //PieceSwitcherup = false;  // This allows the buttons to go to false when selections are made.


    }

    // Updates each selection by casting a hit ray onto the layer ChestPlane through the main camera.
    public void UpdatedSelection()
    {
        if (!Camera.main)

            return;

       
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 30.0f, LayerMask.GetMask("ChestPlane")))
            {
                selectionX = (int)hit.point.x;
                selectionY = (int)hit.point.z;
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
        
        
    }

    // Instantiates the chess prefabs onto the board as game objects.
    public void SpawnedChessMan(int index, int x, int y, Quaternion orientation)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x,y), orientation) as GameObject;
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        activeChessMan.Add(go);
    }

    // Spawns all chess pieces as a list reference.
    public void SpawnAllChestMans()
    {
        activeChessMan = new List<GameObject>();
        Chessmans = new Chessman[8, 8];
        enPassantMove = new int[2] { -1, -1 };

        //Spawn White Team

        //King
        SpawnedChessMan(0, 3, 0, Whiteorientation);


        //Queen
        SpawnedChessMan(1,4, 0, Whiteorientation);

        //Rooks
        SpawnedChessMan(2, 0, 0, Whiteorientation);
        SpawnedChessMan(2, 7, 0, Whiteorientation);

        //Bishops
        SpawnedChessMan(3, 2, 0, Whiteorientation);
        SpawnedChessMan(3, 5, 0, Whiteorientation);

        //Kights
        SpawnedChessMan(4,1, 0, Whiteorientation);
        SpawnedChessMan(4, 6, 0, Whiteorientation);

        //Pawns
       /*for(int i = 0; i < 8; i += 2)
        {
            SpawnedChessMan(5,i, 1, Whiteorientation);
        }

        for (int i = 1; i < 8; i += 2)
        {
            SpawnedChessMan(6, i, 1, Whiteorientation);
        }*/

        int i = 0; 

        while(i < 8)
        {
            if(i % 2 == 0)
            {
                SpawnedChessMan(5, i, 1, Whiteorientation);
            }
            else
            {
                SpawnedChessMan(6, i, 1, Whiteorientation);
            }
            i++;
        }

        //Spawn Black Team

        //King
        SpawnedChessMan(7, 4, 7, Blackorientation);


        //Queen
        SpawnedChessMan(8, 3, 7, Blackorientation);

        //Rooks
        SpawnedChessMan(9, 0, 7, Blackorientation);
        SpawnedChessMan(9, 7, 7, Blackorientation);

        //Bishops
        SpawnedChessMan(10, 2, 7, Blackorientation);
        SpawnedChessMan(10, 5, 7, Blackorientation);

        //Kights
        SpawnedChessMan(11, 1, 7, Blackorientation);
        SpawnedChessMan(11, 6, 7, Blackorientation);

        //Pawns
        /*for (int i = 0; i < 8; i += 2)
        {
            SpawnedChessMan(12, i, 6, Blackorientation);
        }

        for (int i = 1; i < 8; i++)
        {
            SpawnedChessMan(13, i, 6, Blackorientation);
        }*/

        i = 0;

        while (i < 8)
        {
            if (i % 2 == 0)
            {
                SpawnedChessMan(12, i, 6, Blackorientation);
            }
            else
            {
                SpawnedChessMan(13, i, 6, Blackorientation);
            }

            i++;
        }
    }

    // sets each chess piece to the center of each tile.
    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x)+ tileOffset; 
        origin.z += (tileSize * y) + tileOffset;
        return origin;
    }

    // Draws the chessboard lines to allow the player to select and move each chess piece.
    private void drawChessboard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for(int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;

            Debug.DrawLine(start, start + widthLine);

            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;

                Debug.DrawLine(start, start + heightLine);
            }
        }

        //Draw the Selection
        if(selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX +1));

            Debug.DrawLine(
                Vector3.forward * (selectionY +1 )+ Vector3.right * selectionX,
            Vector3.forward * selectionY  + Vector3.right * (selectionX + 1));
        }
    }

    public void SpawnSelectedPiece(string pieceName)
    {
        Quaternion SpawnRot;
        if (WhiteCalling)
            SpawnRot = Blackorientation;
        else
            SpawnRot = Whiteorientation;

        int piecePlace = SwitchPiecesDictionary[pieceName];
        SpawnedChessMan(piecePlace, SwitchedPieceX, SwitchedPieceY, SpawnRot);
        //selectedChessman = Chessmans[SwitchedPieceX, SwitchedPieceY];
    }
    public void AddFog(float density)
    {
        FogDensity += density;

        if (FogDensity >= 0.15f)
            FogDensity = 0.15f;

        RenderSettings.fogDensity = FogDensity;
    }

    // Ends game when either side's King is destroyed.
    public void EndGame()
    {
        // If player wins then game is reseted
        /*if (isWhiteTurn)
            Debug.Log("You Win");
        else
            // If Enemy wins the game is reseted. 
            Debug.Log("You Lose");*/
        GameManager.EndGame(isWhiteTurn);
        foreach (GameObject go in activeChessMan)
            Destroy(go);
        //isWhiteTurn = true;
        BoardHighlights.Instance.HideHighlights();
        //SpawnAllChestMans();
        LevelManager.instance.LoadLevel("End Game Scene");
           
    }
}
