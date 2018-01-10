using System;
using System.Windows.Input;

namespace ClipboardManager.DesktopUtil
{
    class DesctopCommands
    {
        private readonly DesktopRegistry _registry = new DesktopRegistry();
        private readonly Desktop _desktop = new Desktop();
        private readonly Storage _storage = new Storage();

        public ICommand SavePositions
        {
            get
            {
                return new DelegateCommand(
                        arg =>
                        {
                            var registryValues = _registry.GetRegistryValues();

                            var iconPositions = _desktop.GetIconsPositions();

                            _storage.SaveIconPositions(iconPositions, registryValues);
                        }
                    );
            }
        }

        public ICommand RestorePositions
        {
            get
            {
                return new DelegateCommand(
                        arg =>
                        {
                            var registryValues = _storage.GetRegistryValues();

                            _registry.SetRegistryValues(registryValues);

                            var iconPositions = _storage.GetIconPositions();

                            _desktop.SetIconPositions(iconPositions);

                            _desktop.Refresh();
                        }
                    );
            }
        }
    }
}
