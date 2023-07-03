using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Object = System.Object;

namespace vrbits
{
    /// <summary>
    /// Configuration for linking events with scenes to be loaded.
    /// Loading and unloading of scenes will also be handled.
    /// Is the scene allready open in the editor instantiating is canceled after download.
    /// </summary>
    public class SceneLoader : Singleton<SceneLoader>
    {
        const bool SceneActivateOnLoad = false;

        //Highlanders are scenes of which only one can be active at a time.
        //Every other Highlander is unloaded beforehand.
        //Ok, Highlanders can have children that they spawn and tahs also must be unloaded.
        private Stack highlanderScenes = new Stack();
        private Scene? highlanderScene = null; ///////////////////////  löschen!!!!!!!!!


        [Tooltip("For unloading purposes, the MainMap scene addressable is needed")]
        public AssetReference mapScene;
        private AssetReference _mapSceneRuntimeReference = null;
        internal LoadSceneMode loadSceneMode = LoadSceneMode.Additive;

        public void LoadScene(string sceneName)
        {
            throw new NotImplementedException();
        }

        public void LoadScene(Scene scene)
        {
            throw new NotImplementedException();
        }

        public void LoadScene(AssetReference reference)
        {
            Debug.Log($"Start loading of Addressable: {reference}");
            StartCoroutine(LoadSceneAsync(reference));
        }


        /// <summary>
        /// Loading a AssetReference to a scene as Highlander.
        /// Takes care of finish unloading the other scene before loading the new one.
        /// </summary>
        /// <param name="reference"></param>
        public void LoadSceneAsHighlander(AssetReference reference)
        {
            AsyncOperation removingOperation = RemoveOtherHighlander();
            if (removingOperation == null)
                StartCoroutine(LoadSceneAsync(reference, true));
            else
                removingOperation.completed += (op) => StartCoroutine(LoadSceneAsync(reference, true));
        }

        private void RemovingOperation_completed(AsyncOperation obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Highlanders are scenes of which only one can be active at a time.
        /// Every other Highlander is unloaded here.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private AsyncOperation RemoveOtherHighlander()
        {
            if (highlanderScenes.Count == 0)
                return null;

            Scene lastHighlander = (Scene)highlanderScenes.Pop();
            if (!lastHighlander.isLoaded)
                return null;

            if (highlanderScenes.Count > 0)
            {
                return RemoveOtherHighlander();
            }

            return SceneManager.UnloadSceneAsync((Scene)lastHighlander);
        }



        /// <summary>
        /// Highlanders are scenes of which only one can be active at a time.
        /// Every other Highlander is unloaded here.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        //private AsyncOperation RemoveOtherHighlander()
        //{
        //    if (highlanderScenes == null)
        //        return null;
        //    if (highlanderScenes.Count == 0)
        //        return null;


        //    while (highlanderScenes.Count > 0)
        //    {
        //        var lastHighlander = highlanderScenes.Pop();
        //        if (!highlanderScene.Value.isLoaded)
        //            continue;
        //        return SceneManager.UnloadSceneAsync((Scene)lastHighlander);
        //    }
        //}

        private IEnumerator LoadSceneAsync(AssetReference sceneReference, bool highlander = false)
        {
            var async = Addressables.InitializeAsync();
            while (!async.IsDone)
            {
                Debug.Log("Addressable System Init " + async.PercentComplete);
                yield return null;
            }

            while (!sceneReference.IsDone)
            {
                var asyncDownload = Addressables.DownloadDependenciesAsync(sceneReference);
                while (!asyncDownload.IsDone)
                {
                    Debug.Log("Addressable Download " + asyncDownload.PercentComplete);
                    yield return null;
                }
            }

            Addressables.LoadResourceLocationsAsync(sceneReference).Completed += (loc) =>
            {
                Scene sceneToLoad = SceneManager.GetSceneByPath(loc.Result[0].InternalId);

                //only load scene if not loaded in Editor yet
                if (sceneToLoad.isLoaded)
                    return;

                sceneReference.LoadSceneAsync(loadSceneMode, SceneActivateOnLoad).Completed += OnCompleteLoading;
                void OnCompleteLoading(AsyncOperationHandle<SceneInstance> handle)
                {
                    ActivateScene(handle, highlander, sceneReference);
                };
            };
        }

        private void ActivateScene(AsyncOperationHandle<SceneInstance> loadedSceneHandle, bool highlander, AssetReference sceneReference)
        {
            if (loadedSceneHandle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.Log("<b>Scene could't be activated</b> maybe downloading and loading is not finished yet.");
                return;
            }
            loadedSceneHandle.Result.ActivateAsync();

            if (highlander)
                highlanderScenes.Push(loadedSceneHandle.Result.Scene);
            StoreMapSceneReference(sceneReference);
        }

        private void StoreMapSceneReference(AssetReference sceneReference)
        {
            if (mapScene == null)
                return;
            if (sceneReference.RuntimeKey.ToString() == mapScene.RuntimeKey.ToString())
                _mapSceneRuntimeReference = sceneReference;
        }

        internal void UnloadMapScene()
        {
            if (_mapSceneRuntimeReference != null)
                _mapSceneRuntimeReference.UnLoadScene();
#if UNITY_EDITOR
            else
                SceneManager.UnloadSceneAsync(_mapSceneRuntimeReference.editorAsset.name);
#endif
        }
    }

}