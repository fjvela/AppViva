using AppViva.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppViva.Controls
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    //public partial class ClassButton : ContentPage
    //{
    //	public ClassButton ()
    //	{
    //		InitializeComponent ();
    //	}
    //}


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassButton : ContentView
    {
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
                                                         propertyName: "TitleText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(ClassButton),
                                                         defaultValue: "",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: TitleTextPropertyChanged);

        public string TitleText
        {
            get { return base.GetValue(TitleTextProperty).ToString(); }
            set { base.SetValue(TitleTextProperty, value); }
        }

        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ClassButton)bindable;
            control.title.Text = newValue.ToString();
        }

        public static BindableProperty ClassDataProperty = BindableProperty.Create(
                                                        propertyName: nameof(ClassData),
                                                        returnType: typeof(ClassModel),
                                                        declaringType: typeof(ClassButton),
                                                        defaultValue: null,
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: ClassDataSourcePropertyChanged);

        private static void ClassDataSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ClassModel data = (ClassModel)newValue;
            ClassButton control = (ClassButton)bindable;
            control.IsVisible = data.ShowButton;

            if (data.IsBooked)
            {
                control.BackgroundColor = Color.Red;
                control.title.Text = "Cancelar";
            }
            else if (data.IsBookingAllowed)
            {

                control.BackgroundColor = Color.FromHex("#76dab2");
                control.title.Text = "Reservar";

            }
        
        }

        public ClassModel ClassData
        {
            get { return (ClassModel)GetValue(ClassDataProperty); }
            set { SetValue(ClassDataProperty, value); }
        }

        //public static readonly BindableProperty ImageProperty = BindableProperty.Create(
        //                                                propertyName: "Image",
        //                                                returnType: typeof(string),
        //                                                declaringType: typeof(ClassButton),
        //                                                defaultValue: "",
        //                                                defaultBindingMode: BindingMode.TwoWay,
        //                                                propertyChanged: ImageSourcePropertyChanged);

        //public string Image
        //{
        //    get { return base.GetValue(ImageProperty).ToString(); }
        //    set { base.SetValue(ImageProperty, value); }
        //}

        //private static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    var control = (ClassButton)bindable;
        //    control.image.Source = ImageSource.FromFile(newValue.ToString());
        //}

        public ClassButton()
        {
            InitializeComponent();
        }


    }
}