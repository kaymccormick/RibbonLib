using System.ComponentModel;

namespace RibbonLib.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class RibbonModelRadioButton : RibbonModelItem
    {
        private bool _isChecked;
        private string _groupName;

        /// <summary>
        /// 
        /// 
        /// </summary>
        [DefaultValue(false)]
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (value == _isChecked) return;
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        public override ControlKind Kind => ControlKind.RadioButton;

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                if (value == _groupName) return;
                _groupName = value;
                OnPropertyChanged();
            }
        }
    }
}