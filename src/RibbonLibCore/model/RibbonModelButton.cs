using System.Windows;

namespace RibbonLib.Model
{
    /// <summary>
    /// &lt;HierarchicalDataTemplate DataType="{x:Type ribbonModel:RibbonModelButton}"&gt;
    /// &lt;RibbonButton Label="{Binding Label}" Command="{Binding Command}" CommandParameter="{Binding CommandParameter}" CommandTarget="{Binding CommandTarget}"&gt;&lt;/RibbonButton&gt;
    /// &lt;/HierarchicalDataTemplate&gt;
    /// </summary>
    public class RibbonModelButton : RibbonModelItem
    {
        private double _fontSize;
        private FontWeight _fontWeight;

        /// <inheritdoc />
        public override ControlKind Kind => ControlKind.RibbonButton;

        public object ModelInstance { get; set; }

        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                if (value.Equals(_fontSize)) return;
                _fontSize = value;
                OnPropertyChanged();
            }
        }

        public FontWeight FontWeight
        {
            get { return _fontWeight; }
            set
            {
                if (value.Equals(_fontWeight)) return;
                _fontWeight = value;
                OnPropertyChanged();
            }
        }

        public string ToolTipDescription { get; set; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ModelItem[ {Kind}; Command={Command}; Label={Label} ]";
        }
    }
}