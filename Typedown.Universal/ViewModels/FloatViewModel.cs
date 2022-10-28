﻿using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Typedown.Universal.Controls.FloatControls;
using Typedown.Universal.Interfaces;
using Typedown.Universal.Models;
using Typedown.Universal.Services;
using Typedown.Universal.Utilities;
using Windows.Foundation;

namespace Typedown.Universal.ViewModels
{
    public class FloatViewModel : ObservableObject
    {
        public IServiceProvider ServiceProvider { get; }

        public int SearchOpen { get; set; }

        public AppViewModel ViewModel => ServiceProvider.GetService<AppViewModel>();

        public EventCenter EventCenter => ServiceProvider.GetService<EventCenter>();

        public IMarkdownEditor MarkdownEditor => ServiceProvider.GetService<IMarkdownEditor>();

        public FloatViewModel(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            EventCenter.GetObservable<EditorEventArgs>("OpenFrontMenu").Subscribe(x => OnOpenFrontMenu(x.Args));
            EventCenter.GetObservable<EditorEventArgs>("OpenFormatPicker").Subscribe(x => OnOpenFormatPicker(x.Args));
            EventCenter.GetObservable<EditorEventArgs>("OpenImageSelector").Subscribe(x => OnOpenImageSelector(x.Args));
            EventCenter.GetObservable<EditorEventArgs>("OpenTableTools").Subscribe(x => OnOpenTableTools(x.Args));
            EventCenter.GetObservable<EditorEventArgs>("OpenImageToolbar").Subscribe(x => OnOpenImageToolbar(x.Args));
            EventCenter.GetObservable<EditorEventArgs>("OpenToolTip").Subscribe(x => OnOpenToolTip(x.Args));
            this.WhenPropertyChanged(nameof(SearchOpen)).Subscribe(_ => OnSearchOpenChange(SearchOpen));
        }

        public void OnSearchOpenChange(int open)
        {
            MarkdownEditor?.PostMessage("SearchOpenChange", new { open });
        }

        public void OnOpenImageToolbar(JToken arg)
        {
            //if (!ImageToolbarOpen)
            //{
            //    ImageToolbarArg = arg;
            //    ImageToolbarOpen = true;
            //}
        }

        public void OnOpenFrontMenu(JToken args)
        {
            //FrontMenuArg = arg;
            //FrontMenuOpen = true;
            var frontMenu = ServiceProvider.GetService<FrontMenu>();
            frontMenu.Open(args["boundingClientRect"].ToObject<Rect>());
        }

        public void OnOpenFormatPicker(JToken arg)
        {
            //FormatPickerArg = arg;
            //FormatPickerOpen = true;
        }

        public void OnOpenImageSelector(JToken arg)
        {
            //ImageSelectorOpenArg = arg;
            //ImageSelectorOpen = true;
        }

        public void OnOpenTableTools(JToken arg)
        {
            //TableToolsArg = arg;
            //TableToolsOpen = true;
            //TableToolsBarType = arg["tableInfo"]["barType"].ToString();
        }

        public void OnOpenToolTip(JToken arg)
        {
            //ToolTipArg = arg;
            //ToolTipOpen = arg["open"].ToObject<bool>();
        }

        public void CloseAll()
        {
            //if (FormatPickerOpen)
            //    FormatPickerOpen = false;
            //if (ImageToolbarOpen)
            //    ImageToolbarOpen = false;
            //if (FrontMenuOpen)
            //    FrontMenuOpen = false;
            //if (ImageSelectorOpen)
            //    ImageSelectorOpen = false;
            //if (TableToolsOpen)
            //    TableToolsOpen = false;
        }
    }
}
