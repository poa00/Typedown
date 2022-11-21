﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Typedown.Universal.Models;
using Typedown.Universal.Models.UploadConfigModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Typedown.Universal.Controls.SettingControls.SettingItems.UploadConfigItems
{
    public sealed partial class SCPConfig : UserControl
    {
        public static DependencyProperty ImageUploadConfigProperty { get; } = DependencyProperty.Register(nameof(ImageUploadConfig), typeof(ImageUploadConfig), typeof(SCPConfig), null);
        public ImageUploadConfig ImageUploadConfig { get => (ImageUploadConfig)GetValue(ImageUploadConfigProperty); set => SetValue(ImageUploadConfigProperty, value); }

        public static DependencyProperty SCPConfigModelProperty { get; } = DependencyProperty.Register(nameof(SCPConfigModel), typeof(SCPConfigModel), typeof(SCPConfig), null);
        public SCPConfigModel SCPConfigModel { get => (SCPConfigModel)GetValue(SCPConfigModelProperty); set => SetValue(SCPConfigModelProperty, value); }


        public SCPConfig()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SCPConfigModel = ImageUploadConfig.LoadUploadConfig<SCPConfigModel>();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            ImageUploadConfig.StoreUploadConfig(SCPConfigModel);
        }
    }
}