﻿#pragma checksum "C:\Users\markleyk\Desktop\hack4good\Hack4Good2023-TheWholeApple\IntakeApp\IntakeApp\Platforms\Windows\App.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F9E8E34AE967E6D98CAFFB541976B39FF6199D252EFF6E5BF2163EC34E3C799F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace IntakeApp.WinUI
{
#if !DISABLE_XAML_GENERATED_MAIN
    /// <summary>
    /// Program class
    /// </summary>
    public static class Program
    {
        [global::System.Runtime.InteropServices.DllImport("Microsoft.ui.xaml.dll")]
        private static extern void XamlCheckProcessRequirements();

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.STAThreadAttribute]
        static void Main(string[] args)
        {
            XamlCheckProcessRequirements();
            
            global::WinRT.ComWrappersSupport.InitializeComWrappers();
            global::Microsoft.UI.Xaml.Application.Start((p) => {
                var context = new global::Microsoft.UI.Dispatching.DispatcherQueueSynchronizationContext(global::Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread());
                global::System.Threading.SynchronizationContext.SetSynchronizationContext(context);
                new App();
            });
        }
    }
#endif

    partial class App : global::Microsoft.Maui.MauiWinUIApplication
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        private bool _contentLoaded;
        /// <summary>
        /// InitializeComponent()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;

            global::System.Uri resourceLocator = new global::System.Uri("ms-appx:///Platforms/Windows/App.xaml");
            global::Microsoft.UI.Xaml.Application.LoadComponent(this, resourceLocator);

#if DEBUG && !DISABLE_XAML_GENERATED_BINDING_DEBUG_OUTPUT
            DebugSettings.BindingFailed += (sender, args) =>
            {
                global::System.Diagnostics.Debug.WriteLine(args.Message);
            };
#endif
#if DEBUG && !DISABLE_XAML_GENERATED_BREAK_ON_UNHANDLED_EXCEPTION
            UnhandledException += (sender, e) =>
            {
                if (global::System.Diagnostics.Debugger.IsAttached) global::System.Diagnostics.Debugger.Break();
            };
#endif
        }
    }
}

