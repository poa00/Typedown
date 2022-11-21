﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Xml.Linq;
using Typedown.Universal.Interfaces;
using Typedown.Universal.Models;
using Typedown.Universal.Models.UploadConfigModels;
using Typedown.Universal.Pages.SettingPages;
using Typedown.Universal.Services;
using Typedown.Universal.Utilities;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Typedown.Universal.Controls.SettingControls.SettingItems.UploadConfigItems
{
    [ContentProperty(Name = nameof(Detail))]
    public sealed partial class CommonConfig : UserControl
    {
        public static DependencyProperty ImageUploadConfigProperty { get; } = DependencyProperty.Register(nameof(ImageUploadConfig), typeof(ImageUploadConfig), typeof(CommonConfig), null);
        public ImageUploadConfig ImageUploadConfig { get => (ImageUploadConfig)GetValue(ImageUploadConfigProperty); set => SetValue(ImageUploadConfigProperty, value); }

        public static DependencyProperty DetailProperty { get; } = DependencyProperty.Register(nameof(Detail), typeof(UIElement), typeof(CommonConfig), null);
        public UIElement Detail { get => (UIElement)GetValue(DetailProperty); set => SetValue(DetailProperty, value); }

        public CommonConfig()
        {
            InitializeComponent();
        }

        private void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            this.GetAncestor<UploadConfigPage>()?.DeleteConfigAsync();
        }

        private async void OnTestUploadButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            try
            {
                button.IsEnabled = false;
                var filePicker = new FileOpenPicker();
                FileTypeHelper.Image.ToList().ForEach(filePicker.FileTypeFilter.Add);
                filePicker.SetOwnerWindow(this.GetService<IWindowService>().GetWindow(this));
                var file = await filePicker.PickSingleFileAsync();
                if (file == null)
                    return;
                var res = await ImageUploadConfig.LoadUploadConfig().Upload(this.GetService<IServiceProvider>(), file.Path);
                await AppContentDialog.Create("上传成功", res, "Ok").ShowAsync(XamlRoot);
            }
            catch (Exception ex)
            {
                await AppContentDialog.Create("上传失败", ex.Message, "Ok").ShowAsync(XamlRoot);
            }
            finally
            {
                button.IsEnabled = true;
            }
        }
    }
}