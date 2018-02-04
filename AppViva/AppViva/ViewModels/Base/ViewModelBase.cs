using AppViva.i18n;
using AppViva.Models;
using AppViva.Services;
using AppViva.ViewModels.Popups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppViva.ViewModels
{
    public abstract class ViewModelBase : BaseNotifyProperty, INavigableViewModel
    {
        protected INavigationService NavigationService { get; } = ServiceLocator.NavigationService;


        private static CancellationToken _currentToken;
        private static CancellationTokenSource _cancelTokenSource;

        /// <summary>
        /// ViewModelBase contains instances for GlobalSettings and LocalizationResources
        /// </summary>
        #region Properties
        protected GlobalSetting GSetting { get { return GlobalSetting.Instance; } }

        private bool _viewIsInitialized;
        public bool ViewIsInitialized
        {
            get { return _viewIsInitialized; }
            set { Set(ref _viewIsInitialized, value); }
        }

        private int _busyOperations = 0;

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (Set(ref _isBusy, value))
                { OnIsBusyChanged(); }
            }
        }

        private object _operationObj = new object();
        protected void StartOperation()
        {
            lock (_operationObj)
                _busyOperations++;

            IsBusy = _busyOperations != 0;
        }

        protected void FinishOperation()
        {
            lock (_operationObj)
                _busyOperations--;

            IsBusy = _busyOperations != 0;
        }
        #endregion

        #region Localization Resources
        protected virtual ResourceManager LocalizationResourceManager { get; } = AppViva.i18n.Resources.ResourceManager;

        /// <summary>
		/// Obtiene el texto desde recursos para la key pasada (index).
		/// </summary>
		/// <param name="index">La "KEY" del texto de recursos que queramos obtener.</param>
		/// <returns>El texto traducido a la cultura establecida.</returns>
		public string this[string index]
        {
            get
            {
                try
                {
                    return LocalizationResourceManager.GetString(index) ?? $"_{index}";
                }
                catch (Exception)
                {
                    if (Debugger.IsAttached)
                    {
                        Debugger.Break();
                    }

                    return $"_{index}";
                }

            }
        }
        #endregion

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.CompletedTask;
        }

        public virtual Task ActivateAsync()
        {
            return Task.CompletedTask;
        }

        public virtual void Deactivate() { }

        protected Task ExecuteOperationAsync(Func<Task> executeAction, Action finallyAction = null, bool retry = true)
        {
            return InternalExecuteOperationAsync(StartOperation, (c) => executeAction(), finallyAction, FinishOperation, retry);
        }

        protected Task ExecuteOperationAsync(Func<CancellationToken, Task> executeAction, Action finallyAction = null, bool retry = true)
        {
            return InternalExecuteOperationAsync(StartOperation, executeAction, finallyAction, FinishOperation, retry);
        }

        protected Task ExecuteOperationQuietlyAsync(Func<Task> executeAction, Action finallyAction = null, bool retry = false)
        {
            return InternalExecuteOperationAsync(null, (c) => executeAction(), finallyAction, null, retry);
        }

        protected Task ExecuteOperationQuietlyAsync(Func<CancellationToken, Task> executeAction, Action finallyAction = null, bool retry = false)
        {
            return InternalExecuteOperationAsync(null, executeAction, finallyAction, null, retry);
        }

        private async Task InternalExecuteOperationAsync(Action startAction, Func<CancellationToken, Task> executeAction, Action finallyAction, Action finishAction, bool retry)
        {
            bool execFinally = true;
            try
            {
                startAction?.Invoke();

                if (!_currentToken.IsCancellationRequested)
                    await executeAction(_currentToken);
            }
            catch (OperationCanceledException) { }
            catch (HttpRequestException ex) when (retry)
            {
                execFinally = false;
                // popup para reintentar
                await NavigationService.ShowPopupAsync(new MessageBoxPopupViewModel()
                {
                    Subtitle = ex.Message
                });
                Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                if (execFinally)
                {
                    finallyAction?.Invoke();

                    finishAction?.Invoke();
                }
            }
        }

        public static void UpdateToken()
        {
            if (_cancelTokenSource == null || _cancelTokenSource.IsCancellationRequested)
                _cancelTokenSource = new CancellationTokenSource();
            _currentToken = _cancelTokenSource.Token;
        }

        public static void TryCancelToken()
        {
            if (_cancelTokenSource != null && !_cancelTokenSource.IsCancellationRequested)
                _cancelTokenSource.Cancel();
        }

        protected virtual void OnIsBusyChanged() { }


        /// <summary>
        /// false is default value when system call back press
        /// </summary>
        /// <returns></returns>
        public virtual bool OnBackButtonPressed()
        {
            return false;
        }


    }
}
