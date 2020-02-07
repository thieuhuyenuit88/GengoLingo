using UnityEngine;

namespace deVoid.UIFramework {
    /// <summary>
    /// Properties common to all windows
    /// </summary>
    [System.Serializable] 
    public class WindowProperties : IWindowProperties {
        [SerializeField] 
        protected bool hideOnForegroundLost = true;

        [SerializeField] 
        protected WindowPriority windowQueuePriority = WindowPriority.ForceForeground;

        [SerializeField]
        protected bool isPopup = false;

        [SerializeField]
        protected bool waitOutTransitionAnimation = false;

        public WindowProperties() {
            hideOnForegroundLost = true;
            waitOutTransitionAnimation = false;
            windowQueuePriority = WindowPriority.ForceForeground;
            isPopup = false;
        }

        /// <summary>
        /// How should this window behave in case another window
        /// is already opened?
        /// </summary>
        /// <value>Force Foreground opens it immediately, Enqueue queues it so that it's opened as soon as
        /// the current one is closed. </value>
        public WindowPriority WindowQueuePriority {
            get { return windowQueuePriority; }
            set { windowQueuePriority = value; }
        }

        /// <summary>
        /// Should this window be hidden when other window takes its foreground?
        /// </summary>
        /// <value><c>true</c> if hide on foreground lost; otherwise, <c>false</c>.</value>
        public bool HideOnForegroundLost {
            get { return hideOnForegroundLost; }
            set { hideOnForegroundLost = value; }
        }

        /// <summary>
        /// When properties are passed in the Open() call, should the ones
        /// configured in the viewPrefab be overwritten?
        /// </summary>
        /// <value><c>true</c> if suppress viewPrefab properties; otherwise, <c>false</c>.</value>
        public bool SuppressPrefabProperties { get; set; }

        /// <summary>
        /// Popups are displayed with a black background behind them and
        /// in front of all other Windows
        /// </summary>
        /// <value><c>true</c> if this window is a popup; otherwise, <c>false</c>.</value>
        public bool IsPopup {
            get { return isPopup; }
            set { isPopup = value; }
        }

        /// <summary>
        /// Is waiting for out animation finished to go to next window
        /// </summary>
        /// /// <value><c>true</c> waiting for out animation finish to go next window; otherwise, <c>false</c>.</value>
        public bool WaitOutTransitionAnimation {
            get => waitOutTransitionAnimation;
            set => waitOutTransitionAnimation = value;
        }

        public WindowProperties(bool suppressPrefabProperties = false) {
            WindowQueuePriority = WindowPriority.ForceForeground;
            HideOnForegroundLost = false;
            SuppressPrefabProperties = suppressPrefabProperties;
            WaitOutTransitionAnimation = false;
        }

        public WindowProperties(WindowPriority priority, bool hideOnForegroundLost = false, bool waitTransitionAnimation = false, bool suppressPrefabProperties = false) {
            WindowQueuePriority = priority;
            HideOnForegroundLost = hideOnForegroundLost;
            SuppressPrefabProperties = suppressPrefabProperties;
            WaitOutTransitionAnimation = waitTransitionAnimation;
        }
    }
}
