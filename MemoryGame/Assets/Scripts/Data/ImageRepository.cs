using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

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
        string urlMemoryImages = "https://localhost:7193/api/Image";
        
        public void ProcessImageIds(Action<List<int>> processIds)
        {
            StartCoroutine(GetImageIDs(processIds));
        }

        private IEnumerator GetImageIDs(Action<List<int>> processIds)
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
                List<DBImage> images = JsonConvert.DeserializeObject<List<DBImage>>(json);
                List<int> imageBids = images.Select(x => x.ID).ToList();
                processIds(imageBids);
            }
        }
    }
}
