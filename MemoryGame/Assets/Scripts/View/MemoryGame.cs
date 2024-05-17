using Memory.Model;
using Memory.Model.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace Memory.View
{
    public class MemoryGame : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]private GameObject _tilePrefab;
        [SerializeField]private GameObject _memoryBoard;
        [SerializeField] private GameObject _player1View;
        [SerializeField] private GameObject _player2View;

        [SerializeField] private BoardStates _boardState;

        private MemoryBoard _board;

        [DllImport("__Internal")]

        private static extern string StringReturnValue();

        void Start()
        {
            _board = new MemoryBoard(3, 3);
            SetPlayers("Player1", "Player2");

            _memoryBoard.GetComponent<MemoryBoardView>().SetUpMemoryBoardView(_board, _tilePrefab);
            _memoryBoard.GetComponent<MemoryBoardView>().SetPlayers(_player1View.GetComponent<PlayerView>(),
                _player2View.GetComponent<PlayerView>());

        }

        private void SetPlayers(string PlayerOneName, string PlayerTwoName)
        {
            ////create players
            Player playerOneModel = new Player();
            Player playerTwoModel = new Player();

            //set player names
            playerOneModel.Name = StringReturnValue().Length != 0 ? StringReturnValue() :  PlayerOneName;
           //playerOneModel.Name = PlayerOneName;
            playerTwoModel.Name = PlayerTwoName;

            //set models
            _player1View.GetComponent<PlayerView>().SetModel(playerOneModel);
            _player2View.GetComponent<PlayerView>().SetModel(playerTwoModel);

            playerOneModel.IsActive = true;
        }

        // Update is called once per frame
        void Update()
        {
            _boardState = _board.BoardState.State;
        }
    }
}
