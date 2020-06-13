using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using JetBrains.Annotations;

namespace RibbonLib.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PrimaryRibbonModel : INotifyPropertyChanged
    {
        private Brush _background;
        private object _selectedItem;
        private Brush _borderBrush;
        private object _activeContent;

        /// <summary>
        /// 
        /// </summary>
        public RibbonModelApplicationMenu AppMenu { get; set; } = new RibbonModelApplicationMenu();


        /// <summary>
        /// 
        /// </summary>
        public Brush BorderBrush
        {
            get { return _borderBrush; }
            set
            {
                if (Equals(value, _borderBrush)) return;
                _borderBrush = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object SelectedItem
        
        {
            get { return _selectedItem; }
            set
            {
                if (Equals(value, _selectedItem)) return;
                _selectedItem = value;
                if (_selectedItem is RibbonModelTab tab)
                {
                    tab.OnActiveContentChanged(this, new ActiveContentChangedEventArgs(ActiveContent));
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object ActiveContent
        {
            get { return _activeContent; }
            set
            {
                if (Equals(value, _activeContent)) return;
                _activeContent = value;
                (SelectedItem as RibbonModelTab)?.OnActiveContentChanged(this, new ActiveContentChangedEventArgs(value));
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Brush Background
        {
            get { return _background; }
            set
            {
                if (Equals(value, _background)) return;
                _background = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static object CreateGallery()
        {
            //return new MyRibbonGallery();
            return new RibbonModelGallery();
        }


        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<RibbonModelTab> RibbonItems { get; } = new ObservableCollection<RibbonModelTab>();

        /// <summary>
        /// 
        /// </summary>
        public RibbonModelQuickAccessToolBar QuickAccessToolBar { get; set; } = new RibbonModelQuickAccessToolBar();

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<RibbonModelContextualTabGroup> ContextualTabGroups
        {
            get;
        } = new ObservableCollection<RibbonModelContextualTabGroup>();

	/// <summary>
	/// 
	/// </summary>
	public object HelpPaneContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static object CreateGalleryCategory(string header)
        {
            return new RibbonModelGalleryCategory() { Header = header};
            //return new MyRibbonGalleryCategory() {Header = header};
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            RibbonDebugUtils.OnPropertyChanged(this, propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
