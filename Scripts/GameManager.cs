using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int WhitePlayerScore;
    public static int WhitePlayerMoveCount;
    public static bool WhitePlayerWon;
    public static int WhitePlayerFinalScore;

    public static int BlackPlayerScore;
    public static int BlackPlayerMoveCount;
    public static bool BlackPlayerWon;
    public static int BlackPlayerFinalScore;

    /*private const int KingPointWorth = 10000;
    private const int QueenPointWorth = 8000;
    private const int BishopPointWorth = 6000;
    private const int KnightPointWorth = 5000;
    private const int RookPointWorth = 4000;
    private const int PawnPointWorth = 2000;*/
    private const int MoveCountBaseScore = 80000;
    private const int MovePenalty = 2000;
    private const int PieceRevivedWorth = 5000;
    private const int GameClearReward = 20000;

    private static readonly Dictionary<string, int> ChessPieceWorthDictionaries = new Dictionary<string, int>()
    {
        {"KING", 10000 },
        {"QUEEN", 8000},
        {"BISHOP", 6000 },
        {"KNIGHT", 5000 },
        {"ROOK", 4000 },
        {"PAWN", 2000 }
    };

    private static Dictionary<string, int> whitePlayerChessPieceCounts = new Dictionary<string, int>()
    {
        {"KING", 1 },
        {"QUEEN", 1 },
        {"BISHOP", 2 },
        {"KNIGHT", 2 },
        {"ROOK", 2 },
        {"PAWN", 8 }
    };

    private static Dictionary<string, int> blackPlayerChessPieceCounts = new Dictionary<string, int>()
    {
        {"KING", 1 },
        {"QUEEN", 1 },
        {"BISHOP", 2 },
        {"KNIGHT", 2 },
        {"ROOK", 2 },
        {"PAWN", 8 }
    };

    public static Dictionary<string, int> BlackPlayerChessPieceCounts
    {
        get
        {
            return blackPlayerChessPieceCounts;
        }

    }

    public static Dictionary<string, int> WhitePlayerChessPieceCounts
    {
        get
        {
            return whitePlayerChessPieceCounts;
        }
    }


    // Use this for initialization
    public static void InitializeGame()
    {
        WhitePlayerScore = 0;
        BlackPlayerScore = 0;
        WhitePlayerMoveCount = 0;
        BlackPlayerMoveCount = 0;
        WhitePlayerWon = false;
        BlackPlayerWon = false;
        WhitePlayerFinalScore = 0;
        BlackPlayerFinalScore = 0;
    }

    public static int GetPieceWorth(string pieceName)
    {
        return ChessPieceWorthDictionaries[pieceName.ToUpper()];
    }

    public static void WhitePlayerAddScore(int score)
    {
        WhitePlayerScore += score;
        GameHUDManager.instance.UpdateScores();
        print("White Player is sitting at " + WhitePlayerScore);
    }

    public static void WhitePlayerIncrementMoveCount()
    {
        WhitePlayerMoveCount++;
    }

    public static void WhitePlayerRemovePiece(string pieceName)
    {
        int pieceCount = WhitePlayerChessPieceCounts[pieceName.ToUpper()];
        pieceCount--;
        WhitePlayerChessPieceCounts[pieceName.ToUpper()] = pieceCount;
         
    }

    public static void WhitePlayerAddPiece(string pieceName)
    {
        print("Before adding. " + pieceName + " was " + WhitePlayerChessPieceCounts[pieceName.ToUpper()]);
        int pieceCount = WhitePlayerChessPieceCounts[pieceName.ToUpper()];
        pieceCount++;
        WhitePlayerChessPieceCounts[pieceName.ToUpper()] = pieceCount;
        print("After adding. " + pieceName + " was " + WhitePlayerChessPieceCounts[pieceName.ToUpper()]);


    }

    public static void BlackPlayerRemovePiece(string pieceName)
    {
        int pieceCount = BlackPlayerChessPieceCounts[pieceName.ToUpper()];
        pieceCount--;
        BlackPlayerChessPieceCounts[pieceName.ToUpper()] = pieceCount;
    }

    public static void BlackPlayerAddPiece(string pieceName)
    {
        print("Before adding. " + pieceName + " was " + BlackPlayerChessPieceCounts[pieceName.ToUpper()]);
        int pieceCount = BlackPlayerChessPieceCounts[pieceName.ToUpper()];
        pieceCount++;
        BlackPlayerChessPieceCounts[pieceName.ToUpper()] = pieceCount;
        print("After adding. " + pieceName + " was " + BlackPlayerChessPieceCounts[pieceName.ToUpper()]);
    }

    public static void BlackPlayerAddScore(int score)
    {
        BlackPlayerScore += score;
        GameHUDManager.instance.UpdateScores();
        print("Black Player is sitting at " + BlackPlayerScore);
    }

    public static void BlackPlayerIncrementMoveCount()
    {
        BlackPlayerMoveCount++;
    }

    public static int GetWhiteAdjMoveScore()
    {
        int WhiteAdjustedMoveScore = MoveCountBaseScore - (WhitePlayerMoveCount * MovePenalty);
        return WhiteAdjustedMoveScore;
    }

    public static int GetBlackAdjMoveScore()
    {
        int BlackAdjustedMoveScore = MoveCountBaseScore - (BlackPlayerMoveCount * MovePenalty);
        return BlackAdjustedMoveScore;
    }

    public static int CalculateWhiteFinalScore()
    {
        WhitePlayerFinalScore = 0;
        WhitePlayerFinalScore += WhitePlayerScore;
        //WhitePlayerFinalScore += GetWhiteAdjMoveScore();
        WhitePlayerFinalScore += WhitePlayerChessPieceCounts["KING"] * GetPieceWorth("KING");
        WhitePlayerFinalScore += WhitePlayerChessPieceCounts["QUEEN"] * GetPieceWorth("QUEEN");
        WhitePlayerFinalScore += WhitePlayerChessPieceCounts["BISHOP"] * GetPieceWorth("BISHOP");
        WhitePlayerFinalScore += WhitePlayerChessPieceCounts["KNIGHT"] * GetPieceWorth("KNIGHT");
        WhitePlayerFinalScore += WhitePlayerChessPieceCounts["ROOK"] * GetPieceWorth("ROOK");
        WhitePlayerFinalScore += WhitePlayerChessPieceCounts["PAWN"] * GetPieceWorth("PAWN");

        if (WhitePlayerWon)
            WhitePlayerFinalScore += GameClearReward;

        return WhitePlayerFinalScore;
    }

    public static int CalculateBlackFinalScore()
    {
        BlackPlayerFinalScore = 0;
        BlackPlayerFinalScore += BlackPlayerScore;
        //BlackPlayerFinalScore += GetBlackAdjMoveScore();
        BlackPlayerFinalScore += BlackPlayerChessPieceCounts["KING"] * GetPieceWorth("KING");
        BlackPlayerFinalScore += BlackPlayerChessPieceCounts["QUEEN"] * GetPieceWorth("QUEEN");
        BlackPlayerFinalScore += BlackPlayerChessPieceCounts["BISHOP"] * GetPieceWorth("BISHOP");
        BlackPlayerFinalScore += BlackPlayerChessPieceCounts["KNIGHT"] * GetPieceWorth("KNIGHT");
        BlackPlayerFinalScore += BlackPlayerChessPieceCounts["ROOK"] * GetPieceWorth("ROOK");
        BlackPlayerFinalScore += BlackPlayerChessPieceCounts["PAWN"] * GetPieceWorth("PAWN");

        if (BlackPlayerWon)
            BlackPlayerFinalScore += GameClearReward;

        return BlackPlayerFinalScore;
    }

    public static int PlayerHasRevivedPiece()
    {
        return PieceRevivedWorth;
    }

    public static int PlayerHasWon()
    {
        return GameClearReward;
    }


    public static void EndGame(bool isWhiteTurn)
    {
        if (!isWhiteTurn)
            WhitePlayerWon = true;
        else
            BlackPlayerWon = true;
    }



   
}
