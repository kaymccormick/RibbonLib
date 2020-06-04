using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Diagnostics;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RibbonLib.Model;

namespace RibbonTestApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            VisualDiagnostics.EnableVisualTreeChanged();
            VisualDiagnostics.VisualTreeChanged += VisualDiagnosticsOnVisualTreeChanged;

            InitializeComponent();

            Binding.AddTargetUpdatedHandler(this, Handler);
            PrimaryRibbonModel model = new PrimaryRibbonModel();
            var modelTab = new RibbonModelTab {Header = "Tab 1"};
            model.RibbonItems.Add(modelTab);
            var group1 = new RibbonModelGroup { Label = "Group 1" };
            // var modelControlGroup = new RibbonModelControlGroup();
            // modelControlGroup.Items.Add(new RibbonModelButton() { Label = "Click me" });
            // modelControlGroup.Items.Add(new RibbonModelButton() { Label = "Click me toos" });
            var insertFruitcommand = TryFindResource("InsertFruitCommand") as ICommand;
            var group2 = new RibbonModelGroup { Label = "Fruit" };

            var modelButton = new RibbonModelButton() { Label = "Strawberry", LargeImageSource = TryFindResource("Strawberry") };
            modelButton.Command = insertFruitcommand;
            modelButton.CommandTarget = this;
            modelButton.CommandParameter = modelButton;
            group2.Items.Add(modelButton);
            var modelItem = new RibbonModelButton
            {
                Label = "Grapes", LargeImageSource = TryFindResource("Grapes"), Command = insertFruitcommand
                
            };
            modelItem.CommandParameter = modelItem;
            group2.Items.Add(modelItem);
            modelItem.CommandTarget = this;
            var button = new RibbonModelButton() { Label = "Watermelon", LargeImageSource = TryFindResource("Watermelon") };
            button.Command = insertFruitcommand;
            button.CommandTarget = this;
            button.CommandParameter = button;
            group2.Items.Add(button);
            var comboBox = new RibbonModelItemComboBox(){Label="Fruit dropdown"};
            var modelGallery = new RibbonModelGallery();

            var modelGalleryCategory = CreateFruitGalleryCategory();
            modelGallery.Items.Add(modelGalleryCategory);


            CommandBindings.Add(new CommandBinding(insertFruitcommand, Executed));
            
            var cat1 = CreateFruitGalleryCategory();
            cat1.Command = insertFruitcommand;
            cat1.CommandTarget = this;
            RibbonModelItemMenuButton FruitButton = new RibbonModelItemMenuButton(){Label="Fruit Selector"};
            var gallery = new RibbonModelGallery();
            gallery.Items.Add(cat1);
            FruitButton.ItemsCollection.Add(gallery);

            group1.Items.Add(FruitButton);
            comboBox.Items.Add(modelGallery);
            group1.Items.Add(comboBox);
            // group1.Items.Add(modelControlGroup);
            modelTab.ItemsCollection.Add(group1);
            modelTab.ItemsCollection.Add(group2);
            DataContext = model;
            
        }

        private void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(e.Parameter is RibbonModelButton rmb) 
            // if (e.OriginalSource is RibbonButton rb)
            // {
                // if (rb.DataContext is RibbonModelButton rmb)
                {
                    var rmbLargeImageSource = (ImageSource) rmb.LargeImageSource;
                    Matrix transformToDevice;
                    using (var source = new HwndSource(new HwndSourceParameters()))
                    transformToDevice = source.CompositionTarget.TransformToDevice;

                    var v = new Vector(rmbLargeImageSource.Width, rmbLargeImageSource.Height);
                    var pixelSize = (Size) transformToDevice.Transform((Vector) v);

                    var childUiElement = new Image() {Source = rmbLargeImageSource, Stretch = Stretch.None, Width=pixelSize.Width,Height=pixelSize.Height};
                    var insertionPosition = RichTextBox.CaretPosition.GetInsertionPosition(LogicalDirection.Forward);
                    var paragraph = insertionPosition.Paragraph;
                    paragraph.Inlines.Add(new InlineUIContainer(childUiElement));
                }
            
        }

        /// <inheritdoc />
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            Debug.WriteLine(nameof(OnPreviewMouseDown));
            base.OnPreviewMouseDown(e);
        }

        private void VisualDiagnosticsOnVisualTreeChanged(object sender, VisualTreeChangeEventArgs e)
        {
            Debug.WriteLine(sender);
            Debug.WriteLine(e.ChangeType);
            if (e.Child != null)
            {
                Debug.WriteLine(e.Child);
                Debug.WriteLine(e.Child.GetType());
                if (e.Child is Rectangle ee)
                {

                }
                if (e.Child is FrameworkElement cx)
                {
                    if (cx.Tag != null) Debug.WriteLine(cx.Tag);
                }
            }
        }

        private void Handler(object sender, DataTransferEventArgs e)
        {
            Debug.WriteLine(sender);
            Debug.WriteLine(e.Property.Name);
            Debug.WriteLine(e.TargetObject);
        }

        private RibbonModelGalleryCategory CreateFruitGalleryCategory()
        {
            void AddFruidDrawing(string assetDrawing1, RibbonModelGalleryCategory modelGalleryCategory1)
            {
                var ii = TryFindResource(assetDrawing1) as DrawingGroup;
                DrawingBrush db = new DrawingBrush(ii);
                
                // Matrix transformToDevice;
                // using (var source = new HwndSource(new HwndSourceParameters()))
                    // transformToDevice = source.CompositionTarget.TransformToDevice;
                
                // var pixelSize = (Size)transformToDevice.Transform((Vector)ii.Bounds.Size);
                double width, height;
                if (ii.Bounds.Width > ii.Bounds.Height)
                {
                    width = 32.0;
                    height = ii.Bounds.Height / ii.Bounds.Width* 32.0;
                }
                else
                {
                    height= 32.0;
                    width = ii.Bounds.Width/ ii.Bounds.Height * 32.0;
                }
                Rectangle r = new Rectangle() {Fill = db, Width = width, Height = height};
                modelGalleryCategory1.Items.Add(r);
            }

            var modelGalleryCategory = new RibbonModelGalleryCategory {Header = "Fruits"};

            void AddFruit(string s, RibbonModelGalleryCategory galleryCategory)
            {
                void AddFruitImage(object content, string s1)
                {
                    var fruit1 = new RibbonModelGalleryItem() {Content = content};
                    galleryCategory.Items.Add(fruit1);
                }

                var grapes1 = new Rectangle();
                grapes1.Width = 32;
                grapes1.Height = 32;
                var tryFindResource = (BitmapSource)TryFindResource(s);
                ImageBrush i = new ImageBrush(tryFindResource);
                i.Freeze();
                CachedBitmap x = new CachedBitmap((BitmapSource) TryFindResource(s), BitmapCreateOptions.None, BitmapCacheOption.Default);
                BitmapCacheBrush xx = new BitmapCacheBrush();
                BitmapCache xxx = new BitmapCache();
                grapes1.CacheMode = xxx;
                grapes1.Fill = new ImageBrush(x);
                grapes1.Tag = s;
                AddFruitImage(new Border{Background=i,Width=tryFindResource.PixelWidth,Height=tryFindResource.PixelHeight}, s);
            }

            var grapes = "Grapes";

            var assetDrawing = "Asset_3Drawing";
            AddFruidDrawing(assetDrawing, modelGalleryCategory);
            AddFruidDrawing("Asset_4Drawing", modelGalleryCategory);
            AddFruidDrawing("Asset_2Drawing", modelGalleryCategory);
            // AddFruidDrawing("Asset_4Drawing", modelGalleryCategory);
            // AddFruidDrawing("Asset_4Drawing", modelGalleryCategory);
            // AddFruit(grapes, modelGalleryCategory);
            // AddFruit("Watermelon", modelGalleryCategory);
            // AddFruit("Strawberry", modelGalleryCategory);
            // modelGalleryCategory.Items.Add(new Rectangle { Stroke = Brushes.Green, StrokeThickness =  3.0, Width = 32, Height = 32});
            return modelGalleryCategory;
        }
    }

    public class Myvisual : FrameworkElement {
        private readonly BitmapSource _image;
        private VisualCollection _children;

        public Myvisual(BitmapSource image)
        {
            _image = image;
            _children = new VisualCollection(this);
            _children.Add(CreateDrawingVisualImage());
        }


        private Visual CreateDrawingVisualImage()
        { 
            var drawingVisualImage = new DrawingVisual();
            var dc = drawingVisualImage.RenderOpen();
            dc.DrawImage(_image, new Rect(new Size(_image.PixelWidth, _image.PixelHeight)));
            dc.Close();
            return drawingVisualImage;
        }
        protected override int VisualChildrenCount
        {
            get { return _children.Count; }
        }

        /// <inheritdoc />
        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
        }

        // Provide a required override for the GetVisualChild method.
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _children[index];
        }

        /// <inheritdoc />
        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(_image.PixelWidth, _image.PixelHeight);
        }

    }

}
