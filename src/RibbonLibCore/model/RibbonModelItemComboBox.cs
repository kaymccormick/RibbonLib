using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace RibbonLib.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class RibbonModelItemComboBox : RibbonModelItem, INotifyPropertyChanged
    {
        private object _selectionBoxItem;
        private string _displayMemberPath;

        public RibbonModelItemComboBox()
        {
            Items = ItemsCollection;
        }

        /// <summary>
        /// 
        /// </summary>

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<object> ItemsCollection
        { get; } = new ObservableCollection<object>();

        public virtual IEnumerable Items { get; set; }


        [DefaultValue(null)]
        public object SelectionBoxItem
        {
            get { return _selectionBoxItem; }
            set
            {
                if (Equals(value, _selectionBoxItem)) return;
                _selectionBoxItem = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="combo"></param>
        /// <returns></returns>
        public object CreateGallery()
        {
            var g = PrimaryRibbonModel.CreateGallery();
            ItemsCollection.Add(g);
            return g;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override ControlKind Kind => ControlKind.RibbonComboBox;

        public string DisplayMemberPath
        {
            get { return _displayMemberPath; }
            set
            {
                if (value == _displayMemberPath) return;
                _displayMemberPath = value;
                OnPropertyChanged();
            }
        }
    }


    public class RibbonModelItemFontComboBox : RibbonModelItemComboBox
    {
        public override IEnumerable Items =>
            System.Windows.Media.Fonts.SystemFontFamilies;//.Select(z => new RibbonModelMenuItem(){Header=});
    }
}