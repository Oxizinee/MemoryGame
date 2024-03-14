using Memory.Model.States;
using System.Collections;
using System.Collections.Generic;

namespace Memory.Model
{
    public class Player : ModelBaseClass
    {
        private string _name;
        public string Name
        {
            get
            { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _score;

        public int Score
        {
            get { return _score; }
            set
            {
                if( _score == value) return;
                _score = value;
                OnPropertyChanged();
            }
        }
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if(_isActive == value) return;
                _isActive = value;
                OnPropertyChanged();
            }
        }
        public float Elapsed {  get; set; }
        public Player()
        {
            
        }
    }
}
