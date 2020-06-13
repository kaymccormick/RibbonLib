using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Windows.Input;

namespace RibbonLib
{
    /// <summary>
    /// 
    /// </summary>
    public class MyRibbonComboBox : RibbonComboBox
    {
        /// <inheritdoc />
        public MyRibbonComboBox()
        {
        }

        static MyRibbonComboBox()
        {
            // DefaultStyleKeyProperty.OverrideMetadata(typeof(MyRibbonComboBox),
            // new FrameworkPropertyMetadata(typeof(MyRibbonComboBox)));
            _firstGallery =
                typeof(RibbonComboBox).GetField("FirstGallery", BindingFlags.Instance | BindingFlags.NonPublic);
            _isSelectedItemCached =
                typeof(RibbonGallery).GetField("IsSelectedItemCached", BindingFlags.Instance | BindingFlags.NonPublic);


        }

        private object _currentItem;
        private static FieldInfo _firstGallery;
        private static FieldInfo _isSelectedItemCached;

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
        }

        /// <inheritdoc />
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            int num;
            switch (item)
            {
                case RibbonMenuItem _:
                case RibbonSeparator _:
                    num = 1;
                    break;
                default:
                    num = item is RibbonGallery ? 1 : 0;
                    break;
            }

            var flag = num != 0;
            if (!flag)
                _currentItem = item;
            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            var currentItem = _currentItem;
            _currentItem = (object) null;
            if (UsesItemContainerTemplate)
            {
                var dataTemplate = ItemContainerTemplateSelector.SelectTemplate(currentItem, (ItemsControl) this);
                if (dataTemplate != null)
                {
                    var obj = (object) dataTemplate.LoadContent();
                    switch (obj)
                    {
                        case RibbonMenuItem _:
                        case RibbonGallery _:
                        case RibbonSeparator _:
                            return obj as DependencyObject;
                        default:
                            throw new InvalidOperationException("Invalid container");
                    }
                }
            }

            return (DependencyObject) new RibbonMenuItem();
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }

        /// <inheritdoc />
        public Guid ControlId { get; }

        public MyRibbonGallery FirstGallery => (MyRibbonGallery) _firstGallery.GetValue(this);
        public bool IsSelectedItemCached
        {
            get
            {
                var value = (bool) _isSelectedItemCached.GetValue(this);
                Debug.WriteLine(value);
                return value;
            }
        }

        public void UpdateSelectionProperties_()
        {

        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class MyRibbonGallery : RibbonGallery
    {
        private Collection<RibbonGalleryItem> _selectedContainers = new Collection<RibbonGalleryItem>();
        private static MethodInfo _moveCurrentToPosition;
        private static FieldInfo _collectionView;
        private static FieldInfo _sourceCollectionView;
        private static MethodInfo _getSelectableValueFromItem;
        private static MethodInfo _SynchronizeWithCurrentItem;
        private static MethodInfo _SynchronizeWithCurrentItem0;

        /// <summary>
        /// 
        /// </summary>
        public Guid ControlId { get; } = Guid.NewGuid();

        /// <inheritdoc />
        protected override void OnSelectionChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            RaiseEvent((RoutedEventArgs) e);
            if (!ShouldExecuteCommand || e.Handled || e.NewValue == null)
                return;
            CommandHelpers.InvokeCommandSource(CommandParameter, PreviewCommandParameter, (ICommandSource) this,
                CommandOperation.Execute);
        }

        public bool ShouldExecuteCommand { get; set; } = true;

        static MyRibbonGallery()
        {
            // DefaultStyleKeyProperty.OverrideMetadata(typeof(MyRibbonGallery),
            // new FrameworkPropertyMetadata(typeof(MyRibbonGallery)));
            _moveCurrentToPosition = typeof(RibbonGallery).GetMethod("MoveCurrentToPosition",
                BindingFlags.Instance | BindingFlags.NonPublic);
            _collectionView =
                typeof(RibbonGallery).GetField("CollectionView", BindingFlags.Instance | BindingFlags.NonPublic);
            _sourceCollectionView =
                typeof(RibbonGallery).GetField("SourceCollectionView", BindingFlags.Instance | BindingFlags.NonPublic);
            _getSelectableValueFromItem =
                typeof(RibbonGallery).GetMethod("GetSelectableValueFromItem", BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(object)}, new ParameterModifier[]{});
            _SynchronizeWithCurrentItem = typeof(RibbonGallery).GetMethod("SynchronizeWithCurrentItem",
                BindingFlags.Instance | BindingFlags.NonPublic,null,
                new Type[] {typeof(RibbonGallery), typeof(object)}, new ParameterModifier[]{});
            _SynchronizeWithCurrentItem0 = typeof(RibbonGallery).GetMethod("SynchronizeWithCurrentItem",
                BindingFlags.Instance | BindingFlags.NonPublic, null,
                new Type[] { }, new ParameterModifier[] { });

        }

        /// <inheritdoc />
        public MyRibbonGallery()
        {
            try
            {
                var f = typeof(RibbonGallery).GetField("_selectedContainers",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                _selectedContainers = (Collection<RibbonGalleryItem>) f.GetValue(this);
            }
            catch 
            {

            }


        }

        /// <inheritdoc />
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            MyRibbonGalleryCategory ribbonGalleryCategory = (MyRibbonGalleryCategory)element;
            ribbonGalleryCategory.RibbonGallery = this;
            base.PrepareContainerForItemOverride(element, item);
        }



        /// <inheritdoc />
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {

            base.ClearContainerForItemOverride(element, item);
            MyRibbonGalleryCategory ribbonGalleryCategory = (MyRibbonGalleryCategory)element;
            ribbonGalleryCategory.RibbonGallery = null;
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return base.IsItemItsOwnContainerOverride(item);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MyRibbonGalleryCategory();
        }

        internal static bool VerifyEqual(object knownValue, object itemValue)
        {
            return object.Equals(knownValue, itemValue);
        }

        public void ChangeSelection(object item, MyRibbonGalleryItem container, bool isSelected)
        {
            if (this.IsSelectionChangeActive)
                return;
            object selectedItem = this.SelectedItem;
            object itemValue = item;
            bool flag = !MyRibbonGallery.VerifyEqual(selectedItem, itemValue);
            try
            {
                Debug.WriteLine("here");
                this.IsSelectionChangeActive = true;
                if (isSelected == flag)
                {
                    Debug.WriteLine("here1");
                    if (!isSelected && container != null)
                    {
                        Debug.WriteLine("here2");
                        container.IsSelected = false;
                        int index = this._selectedContainers.IndexOf(container);
                        if (index > -1)
                        {
                            Debug.WriteLine("here3");
                            this._selectedContainers.RemoveAt(index);
                            container.OnUnselected_(new RoutedEventArgs(RibbonGalleryItem.UnselectedEvent, (object)container));
                        }
                    }
                    else
                    {
                        Debug.WriteLine("here4");
                        for (int index = 0; index < this._selectedContainers.Count; ++index)
                        {
                            MyRibbonGalleryItem selectedContainer = (MyRibbonGalleryItem) this._selectedContainers[index];
                            selectedContainer.IsSelected = false;
                            selectedContainer.OnUnselected_(new RoutedEventArgs(RibbonGalleryItem.UnselectedEvent, (object)selectedContainer));
                            if (!isSelected)
                            {
                                Debug.WriteLine("here5");
                                this.MoveCurrentToPosition_(selectedContainer.RibbonGalleryCategory.CollectionView, -1);
                            }
                        }
                        this._selectedContainers.Clear();
                        if (!isSelected)
                        {
                            Debug.WriteLine("here6");
                            this.InvalidateProperty(RibbonGallery.SelectedItemProperty);
                            this.InvalidateProperty(RibbonGallery.SelectedValueProperty);
                            this.MoveCurrentToPosition_(this.CollectionView, -1);
                            this.MoveCurrentToPosition_(this.SourceCollectionView, -1);
                            if (LogicalTreeHelper.GetParent((DependencyObject)this) is MyRibbonComboBox parent && this == parent.FirstGallery && !parent.IsSelectedItemCached)
                                parent.UpdateSelectionProperties_();
                        }
                    }
                    if (isSelected)
                    {
                        Debug.WriteLine("here7");
                        this.SetCurrentValue(RibbonGallery.SelectedItemProperty, item);
                        this.SetCurrentValue(RibbonGallery.SelectedValueProperty, this.GetSelectableValueFromItem(item));
                        if (container != null)
                            this.SynchronizeWithCurrentItem(container.RibbonGalleryCategory, item);
                        else
                            this.SynchronizeWithCurrentItem();
                    }
                }
                if (isSelected)
                {
                    Debug.WriteLine("here8");
                    if (container != null)
                    {
                        Debug.WriteLine("here9");
                        if (!this._selectedContainers.Contains(container))
                        {
                            Debug.WriteLine("her10e");
                            this._selectedContainers.Add(container);
                            container.IsSelected = true;
                            container.OnSelected_(new RoutedEventArgs(RibbonGalleryItem.SelectedEvent, (object)container));
                        }
                    }
                }
            }
            finally
            {
                this.IsSelectionChangeActive = false;
            }

            if (!flag)
            {
                Debug.WriteLine("here11");
                return;
            }

            Debug.WriteLine("here12");
            this.OnSelectionChanged(new RoutedPropertyChangedEventArgs<object>(selectedItem, isSelected ? itemValue : (object)null, RibbonGallery.SelectionChangedEvent));

        }

        private void SynchronizeWithCurrentItem(MyRibbonGalleryCategory containerGalleryCategory, object item)
        {
            _SynchronizeWithCurrentItem?.Invoke(this, new object[] { containerGalleryCategory,item});
        }

        private void SynchronizeWithCurrentItem()
        {
            _SynchronizeWithCurrentItem0?.Invoke(this, new object[] { });
        }

        private object GetSelectableValueFromItem(object item)
        {
            return _getSelectableValueFromItem.Invoke(this, new object[] {item});


        }

        public CollectionView CollectionView => (CollectionView)_collectionView.GetValue(this);
        public CollectionView SourceCollectionView => (CollectionView)_sourceCollectionView.GetValue(this);

        private void MoveCurrentToPosition_(object collectionView, int i)
        {
            _moveCurrentToPosition.Invoke(this, new object[] {collectionView, i});
        }
        

        public bool IsSelectionChangeActive { get; set; }

        public void ChangeHighlight(object item, MyRibbonGalleryItem container, bool b)
        {
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class MyRibbonGalleryCategory : RibbonGalleryCategory
    {
        private static FieldInfo _collectionView;
        private static FieldInfo _sourceCollectionView;

        static MyRibbonGalleryCategory()    
        {
            // DefaultStyleKeyProperty.OverrideMetadata(typeof(MyRibbonGalleryCategory),
            // new FrameworkPropertyMetadata(typeof(MyRibbonGalleryCategory)));
            _collectionView =
                typeof(RibbonGalleryCategory).GetField("CollectionView", BindingFlags.Instance | BindingFlags.NonPublic);
            _sourceCollectionView =
                typeof(RibbonGalleryCategory).GetField("CollectionView", BindingFlags.Instance | BindingFlags.NonPublic);

        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            MyRibbonGalleryItem ribbonGalleryItem = (MyRibbonGalleryItem)element;
            ribbonGalleryItem.RibbonGalleryCategory = this;
            base.PrepareContainerForItemOverride(element, item);
        }


        /// <inheritdoc />
        protected override void OnItemTemplateChanged(DataTemplate oldItemTemplate, DataTemplate newItemTemplate)
        {
            base.OnItemTemplateChanged(oldItemTemplate, newItemTemplate);
        }

        /// <inheritdoc />
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            MyRibbonGalleryItem container = (MyRibbonGalleryItem)element;
            if (container.IsHighlighted)
                container.RibbonGallery.ChangeHighlight(item, container, false);
            if (container.IsSelected)
                container.RibbonGallery.ChangeSelection(item, container, false);
            container.RibbonGalleryCategory = null;
            base.ClearContainerForItemOverride(element, item);

        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return base.IsItemItsOwnContainerOverride(item);
        }

        /// <inheritdoc />
        protected override DependencyObject GetContainerForItemOverride()
        {

            return new MyRibbonGalleryItem();
        }

        /// <inheritdoc />
        public Guid ControlId { get; } = Guid.NewGuid();

        public MyRibbonGallery RibbonGallery { get; set; }
        public CollectionView CollectionView => (CollectionView)_collectionView.GetValue(this);
        public CollectionView SourceCollectionView => (CollectionView)_sourceCollectionView.GetValue(this);
    }

    public class MyRibbonGalleryItem : RibbonGalleryItem
    {
        private static MethodInfo _onUnSelectedMethod;
        private static MethodInfo _onSelectedMethod;


        private void SetSelectedOnInput()
        {
            this.IsSelected2 = true;
            this.RaiseEvent((RoutedEventArgs)new RibbonDismissPopupEventArgs());
        }

        public static readonly DependencyProperty IsSelected2Property = DependencyProperty.Register(
            "IsSelected2", typeof(bool), typeof(MyRibbonGalleryItem), new PropertyMetadata(default(bool), OnIsSelected2Changed));

        public bool IsSelected2
        {
            get { return (bool) GetValue(IsSelected2Property); }
            set { SetValue(IsSelected2Property, value); }
        }

        private static void OnIsSelected2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MyRibbonGalleryItem) d).OnIsSelected2Changed((bool) e.OldValue, (bool) e.NewValue);
            MyRibbonGalleryItem container = (MyRibbonGalleryItem)d;
            bool newValue = (bool)e.NewValue;
            MyRibbonGalleryCategory ribbonGalleryCategory = container.RibbonGalleryCategory;
            if (ribbonGalleryCategory == null)
                return;
            MyRibbonGallery ribbonGallery = ribbonGalleryCategory.RibbonGallery;
            if (ribbonGallery == null)
                return;
            object obj = ribbonGalleryCategory.ItemContainerGenerator.ItemFromContainer((DependencyObject)container);
            if (obj == DependencyProperty.UnsetValue)
                obj = (object)container;
            ribbonGallery.ChangeSelection(obj, container, newValue);
            Debug.WriteLine("hello");
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            RibbonGallery ribbonGallery = this.RibbonGallery;
            if (ribbonGallery != null)
            {
                // if (ribbonGallery.ShouldGalleryItemsAcquireFocus)
                    // this.Focus();
                // try
                // {
                    // ribbonGallery.HasHighlightChangedViaMouse = true;
                    // this.IsHighlighted = true;
                // }
                // finally
                // {
                    // ribbonGallery.HasHighlightChangedViaMouse = false;
                // }
                if (e.ButtonState == MouseButtonState.Pressed)
                    this.IsPressed = true;
                e.Handled = true;
            }
//            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (this.IsHighlighted)
                this.SetSelectedOnInput();
            this.IsPressed = false;
            e.Handled = true;
            base.OnMouseLeftButtonUp(e);
        }


        public bool IsPressed { get; set; }


        protected virtual void OnIsSelected2Changed(bool oldValue, bool newValue)
        {

        }


        static MyRibbonGalleryItem()
        {

            IsSelectedProperty.OverrideMetadata(
                typeof(MyRibbonGalleryItem),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnIsSelectedChanged)));
            _onUnSelectedMethod =
                typeof(RibbonGalleryItem).GetMethod("OnUnselected", BindingFlags.Instance | BindingFlags.NonPublic);
            _onSelectedMethod =
                typeof(RibbonGalleryItem).GetMethod("OnSselected", BindingFlags.Instance | BindingFlags.NonPublic);

        }

        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyRibbonGalleryItem container = (MyRibbonGalleryItem) d;
            bool newValue = (bool) e.NewValue;
            MyRibbonGalleryCategory ribbonGalleryCategory = container.RibbonGalleryCategory;
            if (ribbonGalleryCategory == null)
                return;
            MyRibbonGallery ribbonGallery = ribbonGalleryCategory.RibbonGallery;
            if (ribbonGallery == null)
                return;
            object obj = ribbonGalleryCategory.ItemContainerGenerator.ItemFromContainer((DependencyObject) container);
            if (obj == DependencyProperty.UnsetValue)
                obj = (object) container;
            ribbonGallery.ChangeSelection(obj, container, newValue);
            Debug.WriteLine("hello");
        }

        public MyRibbonGalleryCategory RibbonGalleryCategory { get; set; }
        public MyRibbonGallery RibbonGallery { get; set; }

        public void OnUnselected_(RoutedEventArgs routedEventArgs)
        {
            _onUnSelectedMethod.Invoke(this, new object[] {routedEventArgs});
        }

        public void OnSelected_(RoutedEventArgs routedEventArgs)
        {
            _onSelectedMethod.Invoke(this, new object[] { routedEventArgs });
        }
    }


    internal static class CommandHelpers
    {
        internal static void InvokeCommandSource(
            object parameter,
            object previewParameter,
            ICommandSource commandSource,
            CommandOperation operation)
        {
            var command = commandSource.Command;
            if (command == null)
                return;
            if (command is RoutedCommand routedCommand)
            {
                var target = commandSource.CommandTarget ?? commandSource as IInputElement;
                if (!routedCommand.CanExecute(parameter, target) || operation != CommandOperation.Execute)
                    return;
                routedCommand.Execute(parameter, target);
            }
            else
            {
                if (!command.CanExecute(parameter))
                    return;
                switch (operation)
                {
                    case CommandOperation.Preview:
                        if (!(command is IPreviewCommand previewCommand))
                            break;
                        previewCommand.Preview(previewParameter);
                        break;
                    case CommandOperation.CancelPreview:
                        if (!(command is IPreviewCommand previewCommand2))
                            break;
                        previewCommand2.CancelPreview();
                        break;
                    case CommandOperation.Execute:
                        command.Execute(parameter);
                        break;
                }
            }
        }

        internal static bool CanExecuteCommandSource(object parameter, ICommandSource commandSource)
        {
            var command = commandSource.Command;
            if (command == null)
                return true;
            if (!(command is RoutedCommand routedCommand))
                return command.CanExecute(parameter);
            var target = commandSource.CommandTarget ?? commandSource as IInputElement;
            return routedCommand.CanExecute(parameter, target);
        }
    }

    internal enum CommandOperation
    {
        Preview,
        CancelPreview,
        Execute
    }
}