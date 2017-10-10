using Windows.UI.Xaml;

namespace ClientApp.Triggers
{
    class IsTenFootTrigger : StateTriggerBase
    {
        private bool _isTenFootRequested;

        public IsTenFootTrigger()
        {
            // Set default values.
            IsTenFoot = true;
        }

        public bool IsTenFoot
        {
            get
            {
                return _isTenFootRequested;
            }
            set
            {
                _isTenFootRequested = value;
                SetActive(SystemInformationHelpers.IsTenFootExperience == _isTenFootRequested);
            }
        }
    }
}
