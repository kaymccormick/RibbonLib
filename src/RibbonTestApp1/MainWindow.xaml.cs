using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
            InitializeComponent();

            Binding.AddTargetUpdatedHandler(this, Handler);
            PrimaryRibbonModel model = new PrimaryRibbonModel();
            var modelTab = new RibbonModelTab {Header = "Tab 1"};
            model.RibbonItems.Add(modelTab);
            var group1 = new RibbonModelGroup {Label = "Group 1"};
            var modelControlGroup = new RibbonModelControlGroup();
            modelControlGroup.Items.Add(new RibbonModelButton() { Label = "Click me" });
            modelControlGroup.Items.Add(new RibbonModelButton() { Label = "Click me toos" });
            var comboBox = new RibbonModelItemComboBox();
            var modelGallery = new RibbonModelGallery();

            var modelGalleryCategory = CreateFruitGalleryCategory();
            modelGallery.Items.Add(modelGalleryCategory);

            var cat1 = CreateFruitGalleryCategory();
            RibbonModelItemMenuButton FruitButton = new RibbonModelItemMenuButton();
            var gallery = new RibbonModelGallery();
            gallery.Items.Add(cat1);
            FruitButton.ItemsCollection.Add(gallery);

            group1.Items.Add(FruitButton);
            comboBox.Items.Add(modelGallery);
            group1.Items.Add(comboBox);
            group1.Items.Add(modelControlGroup);
            modelTab.ItemsCollection.Add(group1);
            DataContext = model;
            
        }

        private void Handler(object sender, DataTransferEventArgs e)
        {
            Debug.WriteLine(sender);
            Debug.WriteLine(e.Property.Name);
            Debug.WriteLine(e.TargetObject);
        }

        private RibbonModelGalleryCategory CreateFruitGalleryCategory()
        {
            var modelGalleryCategory = new RibbonModelGalleryCategory {Header = "Fruits"};

            void AddFruit(string s, RibbonModelGalleryCategory galleryCategory)
            {
                void AddFruitImage(Image content)
                {
                    var fruit1 = new RibbonModelGalleryItem() {Content = content};
                    galleryCategory.Items.Add(fruit1);
                }

                var grapes1 = new Image
                    {Source = (ImageSource) TryFindResource(s), Stretch = Stretch.Uniform, Width = 32, Height = 32};
                AddFruitImage(grapes1);
            }

            var grapes = "Grapes";

            AddFruit(grapes, modelGalleryCategory);
            AddFruit("Watermelon", modelGalleryCategory);
            return modelGalleryCategory;
        }
    }
}
