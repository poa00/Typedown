﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Typedown.Universal.Models;
using Typedown.Universal.Models.ExportConfigModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Typedown.Universal.Controls.SettingControls.SettingItems.ExportConfigItems
{
    public sealed partial class ImageConfig : UserControl
    {
        public static DependencyProperty ExportConfigProperty { get; } = DependencyProperty.Register(nameof(ExportConfig), typeof(ExportConfig), typeof(ImageConfig), null);
        public ExportConfig ExportConfig { get => (ExportConfig)GetValue(ExportConfigProperty); set => SetValue(ExportConfigProperty, value); }

        public static DependencyProperty ImageConfigModelProperty { get; } = DependencyProperty.Register(nameof(ImageConfigModel), typeof(HTMLConfigModel), typeof(ImageConfig), null);
        public ImageConfigModel ImageConfigModel { get => (ImageConfigModel)GetValue(ImageConfigModelProperty); set => SetValue(ImageConfigModelProperty, value); }

        public ImageConfig()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ImageConfigModel = ExportConfig.LoadExportConfig<ImageConfigModel>();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            ExportConfig.StoreExportConfig(ImageConfigModel);
        }
    }
}