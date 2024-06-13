using Assets.Scripts.Data;
using Memory.Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Memory.Data
{
    public class PlaySessionRepository : Singleton<PlaySessionRepository>
    {
        string urlPlaySession = "http://localhost/www.MemoryImages.com/api/PlaySession";
        string urlPlaySessionOther = "http://localhost/www.MemoryImages.com/api/PlaySessionOther";
        string _shuffledIds ="";

        public void ProcessImageIds(Action<List<int>> processIds, string amountOfIds)
        {
            StartCoroutine(GetImageIDs(processIds, amountOfIds));
        }

        private IEnumerator GetImageIDs(Action<List<int>> processIds, string amountOfIds) //get all images and info from website from database
        {
            List<int> imageIdsList = new List<int>();
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(urlPlaySession + "/" + amountOfIds);
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result != UnityWebRequest.Result.Success)  // if unsuccessful
            {
                Debug.Log("ImageRepository.GetImageIDs: " + unityWebRequest.error);
            }
            else // if successful
            {
                _shuffledIds = unityWebRequest.downloadHandler.text;
                Debug.Log(_shuffledIds);
                string[] parts = _shuffledIds.Split('.');

                foreach (string part in parts)
                {
                    //  Debug.Log(part);
                   imageIdsList.Add(int.Parse(part));
                 //   processIds(imageIdsList);
                }
                processIds(imageIdsList);
            }
        }

        public void PostPlaySessionWeb()
        {
            StartCoroutine(PostPlaySession());
        }

        private IEnumerator PostPlaySession()
        {
            DBImageIds imageIds = new DBImageIds();
            imageIds.ImageIds = _shuffledIds;

            string json = JsonConvert.SerializeObject(imageIds);
            UnityWebRequest unityWebRequest = UnityWebRequest.Put("https://localhost:7193/api/PlaySessionOther", json);
            unityWebRequest.SetRequestHeader("Content-Type", "application/json");
            unityWebRequest.method = "POST";
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(unityWebRequest.error);
            }
            else
            {
                string returnJson = unityWebRequest.downloadHandler.text;
                Debug.Log("put executed with ids: " + _shuffledIds);
                Debug.Log(returnJson);

            }

        }
    }
}