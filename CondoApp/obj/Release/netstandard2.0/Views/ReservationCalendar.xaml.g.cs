//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("CondoApp.Views.ReservationCalendar.xaml", "Views/ReservationCalendar.xaml", typeof(global::CondoApp.Views.ReservationCalendar))]

namespace CondoApp.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\ReservationCalendar.xaml")]
    public partial class ReservationCalendar : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.XForms.Buttons.SfButton button;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.SfCalendar.XForms.SfCalendar calendar;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.SfBusyIndicator.XForms.SfBusyIndicator reservation_loading;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(ReservationCalendar));
            button = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.XForms.Buttons.SfButton>(this, "button");
            calendar = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.SfCalendar.XForms.SfCalendar>(this, "calendar");
            reservation_loading = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.SfBusyIndicator.XForms.SfBusyIndicator>(this, "reservation_loading");
        }
    }
}