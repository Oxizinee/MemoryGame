using Memory.Model;
using Memory.Model.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Memory.View
{
    public class MemoryGame : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]private GameObject _tilePrefab;
        [SerializeField]private GameObject _memoryBoard;
        [SerializeField] private GameObject _player1;
        [SerializeField] private GameObject _player2;

        [SerializeField] private BoardStates _boardState;

        private MemoryBoard _board;
        private Player _playerOneModel;
        private Player _playerTwoModel;

        [Header("Card Materials")]
        [SerializeField] private Material[] _cardMaterials;


        void Start()
        {
            SetPlayers("Player1", "Player2");
            _board = new MemoryBoard(3, 3, _playerOneModel, _playerTwoModel);
            _memoryBoard.GetComponent<MemoryBoardView>().SetUpMemoryBoardView(_board, _tilePrefab, _cardMaterials);
        }

        private void SetPlayers(string PlayerOneName, string PlayerTwoName)
        {
            //create players
            _playerOneModel = new Player();
            _playerTwoModel = new Player();

            //set player names
            _playerOneModel.Name = PlayerOneName;
            _playerTwoModel.Name = PlayerTwoName;
            
            //set models
            _player1.GetComponent<PlayerView>().SetModel(_playerOneModel);
            _player2.GetComponent<PlayerView>().SetModel(_playerTwoModel);
            
            //set initial active states
            _playerTwoModel.IsActive = false;
            _playerOneModel.IsActive = true;

        }

        // Update is called once per frame
        void Update()
        {
            _boardState = _board.BoardState.State;


            if(Input.GetMouseButtonDown(0)) 
            {
                _playerOneModel.IsActive = !_playerOneModel.IsActive;
            }
        }
    }
}
