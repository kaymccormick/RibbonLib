﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using RibbonLib.Model;

namespace RibbonLib
{
    /// <summary>
    /// 
    /// </summary>
    public class ComboBoxTemplateSelector : ItemContainerTemplateSelector
    {
        /// <inheritdoc />
        public override DataTemplate SelectTemplate(object item, ItemsControl parentItemsControl)
        {
                if (item is RibbonModelGallery)
            {
                return parentItemsControl.TryFindResource("RibbonModelGalleryContainer") as DataTemplate;
            }
            else if (item is RibbonModelGalleryCategory)
            {
                return parentItemsControl.TryFindResource("RibbonModelGalleryCategoryContainer") as DataTemplate;
            }
            else if (item is RibbonModelGalleryItem)
            {
                return parentItemsControl.TryFindResource("RibbonModelGalleryItemContainer") as DataTemplate;
            }
            else if (item is RibbonModelSeparator)
            {
                return parentItemsControl.TryFindResource("RibbonModelSeparatorContainer") as DataTemplate;
            }
            else if (item is RibbonModelItem i)
            {
                switch (i.Kind)
                {
                    case ControlKind.RibbonGallery:
                        return parentItemsControl.TryFindResource("RibbonModelGalleryContainer") as DataTemplate;
                    case ControlKind.RibbonGalleryCategory:
                        return parentItemsControl.TryFindResource("RibbonModelGalleryCategoryContainer") as DataTemplate;
                    case ControlKind.RibbonGalleryItem:
                        return parentItemsControl.TryFindResource("RibbonModelGalleryItemContainer") as DataTemplate;
                    case ControlKind.RibbonSeparator:
                        return parentItemsControl.TryFindResource("RibbonKindSeparator") as DataTemplate;
                }
            }

            return base.SelectTemplate(item, parentItemsControl);
        }
    }
}