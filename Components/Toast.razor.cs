using BHub.Lib.Enum;
using Microsoft.AspNetCore.Components;
using System.Timers;

namespace BHub.Lib.Components
{
    /// <summary>
    ///
    /// </summary>
    public partial class Toast : ComponentBase
    {
        private Timer m_timer;
        private double m_toastInterval;

        /// <summary>
        /// On Toast component initialized
        /// </summary>
        protected override void OnInitialized()
        {
            m_toastInterval = 5000;

            m_onInitialise = true;
            IsVisible = false;

            if (AlwaysVisible)
            {
                IsVisible = true;
            }
        }

        private void HideToast(object source, ElapsedEventArgs args) => HideToast();

        public void ShowToast() => ShowToast(m_toastInterval);

        /// <summary>
        /// Show the Toast for the specified time interval. If no interval is passed
        /// then it will show for the default configured which is currently 5s.
        /// </summary>
        /// <param name="interval">The time in ms for how long to show the Toast</param>
        public void ShowToast(double interval)
        {
            if (!AlwaysVisible)
            {
                InvokeAsync(() =>
                {
                    IsVisible = true;
                    m_onInitialise = false;

                    StateHasChanged();

                    m_timer = new Timer(interval);
                    m_timer.Elapsed += HideToast;
                    m_timer.AutoReset = false;

                    // Start the Toast countdown
                    m_timer.Start();
                });
            }
        }

        /// <summary>
        /// Hides a Toast if 'IsVisible = true' and not 'AlwaysVisible'
        /// </summary>
        public void HideToast()
        {
            if (!AlwaysVisible && IsVisible)
            {
                InvokeAsync(() =>
                {
                    IsVisible = false;

                    StateHasChanged();
                });
            }
        }

#pragma warning disable CS0414
        private bool m_onInitialise;
#pragma warning restore CS0414

        private string m_typeClass => ToastType switch
        {
            ToastType.Primary => "alert alert-primary",
            ToastType.Secondary => "alert alert-secondary",
            ToastType.Danger => "alert alert-danger",
            ToastType.Warning => "alert alert-warning",
            ToastType.Success => "alert alert-success",
            _ => "alert alert-primary"
        };

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Message { get; set; }

        [Parameter]
        public string IconClass { get; set; }

        [Parameter]
        public ToastType ToastType { get; set; }

        [Parameter]
        public bool AlwaysVisible { get; set; } = false;

        public bool IsVisible { get; private set; }
    }
}