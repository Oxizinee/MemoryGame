using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace Memory.Data
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateSingleton);

        public static T Instance => LazyInstance.Value;

        private static T CreateSingleton()
        {
            var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
            var instance = ownerObject.AddComponent<T>();
            DontDestroyOnLoad(ownerObject);
            return instance;
        }
    }

    public class ImageRepository : Singleton<ImageRepository>
    {
        string urlMemoryImages = "http://localhost/www.MemoryImages.com/api/Image";

        public void ProcessImageIds(Action<List<int>> processIds)
        {
            StartCoroutine(GetImageIDs(processIds));
        }

        private IEnumerator GetImageIDs(Action<List<int>> processIds) //get all images and info from website from database
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(urlMemoryImages);
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result != UnityWebRequest.Result.Success)  // if unsuccessful
            {
                Debug.Log("ImageRepository.GetImageIDs: " + unityWebRequest.error);
            }
            else // if successful
            {
                string json = unityWebRequest.downloadHandler.text;
                List<DBImage> _DBimages = JsonConvert.DeserializeObject<List<DBImage>>(json);
                List<int> imageBids = _DBimages.Select(x => x.ID).ToList();
                processIds(imageBids);
            }
        }

        public void GetProcessTexture(int imageID, Action<Texture2D> processTexture)
        {
            StartCoroutine(GetTextures(imageID, processTexture));
        }

        private IEnumerator GetTextures(int imageID, Action<Texture2D> processTexture) //get specific image through image id
        {
            UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(urlMemoryImages + "/" + imageID);
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result != UnityWebRequest.Result.Success)  // if unsuccessful
            {
                Debug.Log("ImageRepository.GetProcessTexture: " + unityWebRequest.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(unityWebRequest);
                processTexture(texture);
            }
        }

        public void PostCombination(int imageID)
        {
            StartCoroutine(PostCombinationFound(imageID));
        }
       
        private IEnumerator PostCombinationFound(int imageID)
        {
            DBImage image = new DBImage();
            image.ID = imageID;
           
                string json = JsonConvert.SerializeObject(image);
                UnityWebRequest unityWebRequest = UnityWebRequest.Put(urlMemoryImages + "/" + imageID, json);
                unityWebRequest.SetRequestHeader("Content-Type", "application/json");
                unityWebRequest.method = "POST";
                yield return unityWebRequest.SendWebRequest();
                if (unityWebRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(unityWebRequest.error);
                }
            else
            {
                Debug.Log("put executed with id: " + imageID);

            }

        }
        
    }
}
