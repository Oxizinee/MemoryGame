using Memory.Model;
using Memory.Model.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private List<Tile> _tiles;
        private Player _playerOneModel;
        private Player _playerTwoModel;

        void Start()
        {
            SetPlayers();
            _board = new MemoryBoard(3, 3, _playerOneModel, _playerTwoModel);
            _tiles = new List<Tile>();
            _tiles = _board.Tiles;
            _memoryBoard.GetComponent<MemoryBoardView>().SetUpMemoryBoardView(_board, _tilePrefab);
        }

        private void SetPlayers()
        {
            _playerOneModel = new Player();
            _playerTwoModel = new Player();
            _playerOneModel.IsActive = true;
            _playerTwoModel.IsActive = false;

            _player1.GetComponent<PlayerView>().SetModel(_playerOneModel);
            _player2.GetComponent<PlayerView>().SetModel(_playerTwoModel);
        }

        // Update is called once per frame
        void Update()
        {
            _boardState = _board.BoardState.State;
        }
    }
}
