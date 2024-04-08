using Memory.Model.States;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

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
        private float _elapsed;
        public float Elapsed 
        { 
            get { return _elapsed; } 
            set 
            {
                if (IsActive)
                {
                    if (_elapsed == value) return;
                    _elapsed = value;
                    OnPropertyChanged();
                }
            } 
        }
        public int mm { get { return (int)(Elapsed / 60); } }
        public int ss { get { return (int)(Elapsed%60); } }
        public int ms { get { return (int)(Elapsed%1) * 1000; } }
        public Player()
        {
            
        }

        public override string ToString()
        {
            return $"{Name} player";
        }
    }
}
