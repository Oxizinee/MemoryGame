using Memory.Data;
using Memory.Model;
using Memory.Model.States;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Memory.View
{
    public class TileView : ViewBaseClass<Tile>, IPointerClickHandler
    {
        public GameObject BackSide;
        private Tile _tileModel;
        private Animator _animator;

        public void SetModel(Tile tile)
        {
            _tileModel = tile;
            Model = _tileModel;

        }
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(_tileModel.ToString() + " clicked");
            _tileModel.Board.BoardState.AddPreview(_tileModel);
        }

        protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Model.TileState)))
            {
                StartAnimation();
            }
            else if(e.PropertyName.Equals(nameof(Model.MemoryCardId))) 
            {
                LoadBack();
            }
        }
        private void LoadBack()
        {
            ImageRepository.Instance.GetProcessTexture(Model.MemoryCardId, LoadBack);
        }
        private void LoadBack(Texture2D texture)
        {
            BackSide.GetComponent<Renderer>().material.mainTexture = texture;
        }
        private void StartAnimation()
        {
            if (Model.TileState.State == TileStates.Preview)
            {
                _animator.Play("Shown");
            }
            else if (Model.TileState.State == TileStates.Hidden)
            {
                _animator.Play("Hidden");
            }
        }

        private void AddEvents()
        {
            for (int i = 0; i < _animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                AnimationClip clip = _animator.runtimeAnimatorController.animationClips[i];
              //  Debug.Log(clip.name);

                AnimationEvent animationStart = new AnimationEvent();
                animationStart.time = 0;
                animationStart.functionName = "AnimationStartHandler";
                animationStart.stringParameter = clip.name;

                AnimationEvent animationEnd = new AnimationEvent();
                animationEnd.time = clip.length;
                animationEnd.functionName = "AnimationEndHandler";
                animationEnd.stringParameter = clip.name;

                clip.AddEvent(animationStart);
                clip.AddEvent(animationEnd);
            }
        }

        public void AnimationStartHandler(string name)
        {
          //  Debug.Log($"{name} animation started");
        }

        public void AnimationEndHandler(string name) 
        {
          //  Debug.Log($"{name} animation ended");
            _tileModel.Board.BoardState.TileAnimationEnded(_tileModel);
        }
       
        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            AddEvents();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
