using System;
using System.Collections;
using System.Linq;
using Services.Essential;
using UnityEngine;
using UnityEngine.Assertions;

namespace Infrastructure.Loading
{
    public class LoadingService : ILoadingService
    {
        private readonly LoadingScreen _loadingScreen;
        private readonly ICoroutineRunner _coroutineRunner;

        private ILoading[] _currentProcessors;

        public LoadingService(LoadingScreen loadingScreen, ICoroutineRunner coroutineRunner)
        {
            _loadingScreen = loadingScreen;
            _coroutineRunner = coroutineRunner;
        }

        public void RequestLoadingImmediately(params ILoading[] processors)
        {
            ThrowIfAlreadyLoading();
            
            _loadingScreen.FadeInImmediately();
            RunLoadingListening(processors);
        }

        public void RequestLoading(params ILoading[] processors)
        {
            ThrowIfAlreadyLoading();
            
            _loadingScreen.FadeIn();
            RunLoadingListening(processors);
        }

        private void RunLoadingListening(ILoading[] processors)
        {
            _currentProcessors = processors;
            _coroutineRunner.StartCoroutine(WatchForProcessors());
        }

        private void ThrowIfAlreadyLoading()
        {
            if (_currentProcessors != null)
                throw new InvalidOperationException("Already loading");
        }

        private IEnumerator WatchForProcessors()
        {
            Assert.IsNotNull(_currentProcessors);
            
            while (true)
            {
                float overallCompletion = CalculateOverallCompletion();
                _loadingScreen.SetSliderProcess(overallCompletion);
                if (IsLoadingEnded(overallCompletion))
                {
                    break;
                }
                yield return null;
            }
            Deinitialize();
        }

        private void Deinitialize()
        {
            _loadingScreen.FadeOut();
            _currentProcessors = null;
        }

        private static bool IsLoadingEnded(float overallCompletion)
        {
            return Math.Abs(overallCompletion - 1) < Mathf.Epsilon;
        }

        private float CalculateOverallCompletion()
        {
            return _currentProcessors.Average(x => x.Progress);
        }
    }
}