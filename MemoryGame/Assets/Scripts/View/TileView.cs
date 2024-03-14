using Memory.Model;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Memory.View
{
    public class TileView : ViewBaseClass<Tile>, IPointerClickHandler
    {
        private Tile _tileModel;
        public void SetModel(Tile tile)
        {
            _tileModel = tile;

            _tileModel.PropertyChanged += Model_PropertyChanged;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(this.name + " clicked");
            _tileModel.Board.PrewingTiles.Add(_tileModel);
            //StartCoroutine(StartAnimation());
        }

        protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(Model.TileState))
            {
                StartCoroutine(StartAnimation());
            }
        }

        private IEnumerator StartAnimation()
        {
            float t = 0;
            Vector3 startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Quaternion startRot = transform.rotation;
            Quaternion flippedRot = Quaternion.AngleAxis(180, Vector3.right) * startRot;

            while (t < 1f)
            {
                t += Time.deltaTime * 2f;

                ////jump
                float angle = t * Mathf.PI;
                float sin = Mathf.Sin(angle);

                transform.position = startPos + Vector3.up * sin;

                //flip
                transform.rotation = Quaternion.Lerp(startRot, flippedRot, t);

                yield return null;
            }
            transform.position = startPos;
            transform.rotation = flippedRot;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
