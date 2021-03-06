﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using JetBrains.Annotations;

namespace RibbonLib.Model
{
    /// <summary>
    /// 
    /// </summary>
    [TypeConverter(typeof(RibbonModelItemTypeConverter))]
    public abstract class RibbonModelItem : DependencyObject, IRibbonModelGroupItem, INotifyPropertyChanged, IRibbonModelItem
    {
        
        private ICommand _command;
        private Brush _foreground = Brushes.Black;
        private Brush _background;

        private object _commandTarget;
        private object _commandParameter;
        private object _largeImageSource;
        private object _smallImageSource;
        private double? _maxWidth;
        private double? _maxHeight;
        private double? _minWidth;
        private double? _minHeight;
        private double? _width;
        private Visibility _visibility = Visibility.Visible;
        private Brush _borderBrush;
        private bool _isEnabled = true;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value == _isEnabled) return;
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            "Label", typeof(string), typeof(RibbonModelItem), new PropertyMetadata(default(string)));

        public string Label
        {
            get { return (string) GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
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

        public Brush Foreground
        {
            get { return _foreground; }
            set
            {
                if (Equals(value, _foreground)) return;
                _foreground = value;
                OnPropertyChanged();
            }
        }

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                if (value == _visibility) return;
                _visibility = value;
                OnPropertyChanged();
            }
        }

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
        // ReSharper disable once MemberCanBeProtected.Global
        public abstract ControlKind Kind { get; }

        
        /// <summary>
        /// s
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICommand Command
        {
            get { return _command; }
            set
            {
                if (Equals(value, _command)) return;
                _command = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public object CommandTarget
        {
            get { return _commandTarget; }
            set
            {
                if (Equals(value, _commandTarget)) return;
                _commandTarget = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public object CommandParameter
        {
            get { return _commandParameter; }
            set
            {
                if (Equals(value, _commandParameter)) return;
                _commandParameter = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public object LargeImageSource
        {
            get { return _largeImageSource; }
            set
            {
                if (Equals(value, _largeImageSource)) return;
                _largeImageSource = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public object SmallImageSource
        {
            get { return _smallImageSource; }
            set
            {
                if (Equals(value, _smallImageSource)) return;
                _smallImageSource = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "RibbonModelItem (Kind=" + Kind + ")";
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public double? MaxWidth
        {
            get { return _maxWidth; }
            set
            {
                if (Nullable.Equals(value, _maxWidth)) return;
                _maxWidth = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public double? MaxHeight
        {
            get { return _maxHeight; }
            set
            {
                if (Nullable.Equals(value, _maxHeight)) return;
                _maxHeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public double? MinWidth
        {
            get { return _minWidth; }
            set
            {
                if (Nullable.Equals(value, _minWidth)) return;
                _minWidth = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public double? MinHeight
        {
            get { return _minHeight; }
            set
            {
                if (Nullable.Equals(value, _minHeight)) return;
                _minHeight = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public double? Width
        {
            get { return _width; }
            set
            {
                if (Nullable.Equals(value, _width)) return;
                _width = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(null)]
        public double? Height { get; set; }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual object TemplateKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        // ReSharper disable once VirtualMemberNeverOverridden.Global
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            RibbonDebugUtils.OnPropertyChanged(this, propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RibbonModelControl : RibbonModelItem
    {
        private object _content;

        public object Content
        {
            get { return _content; }
            set
            {
                if (Equals(value, _content)) return;
                _content = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public override ControlKind Kind { get; } = ControlKind.Generic;
    }
}