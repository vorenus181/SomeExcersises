using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace PersonBook.Base
{
    public class ProtocolSettingsLayout
    {
        #region Dependency properties

        public static readonly DependencyProperty MvvmHasErrorProperty =
            DependencyProperty.RegisterAttached("MvvmHasError"
                , typeof(bool)
                , typeof(ProtocolSettingsLayout)
                , new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, null,
                    CoerceMvvmHasError));

        private static readonly DependencyProperty HasErrorDescriptorProperty =
            DependencyProperty.RegisterAttached("HasErrorDescriptor"
                , typeof(DependencyPropertyDescriptor)
                , typeof(ProtocolSettingsLayout));

        #endregion

        #region Public methods

        public static bool GetMvvmHasError(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(MvvmHasErrorProperty);
        }

        public static void SetMvvmHasError(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(MvvmHasErrorProperty, value);
        }

        #endregion

        #region Private methods

        private static object CoerceMvvmHasError(DependencyObject dependencyObject, object baseValue)
        {
            bool ret = (bool)baseValue;

            if (BindingOperations.IsDataBound(dependencyObject, MvvmHasErrorProperty))
            {
                if (GetHasErrorDescriptor(dependencyObject) == null)
                {
                    DependencyPropertyDescriptor desc =
                        DependencyPropertyDescriptor.FromProperty(System.Windows.Controls.Validation.HasErrorProperty,
                            dependencyObject.GetType());
                    desc.AddValueChanged(dependencyObject, OnHasErrorChanged);
                    SetHasErrorDescriptor(dependencyObject, desc);
                    ret = System.Windows.Controls.Validation.GetHasError(dependencyObject);
                }
            }
            else
            {
                if (GetHasErrorDescriptor(dependencyObject) != null)
                {
                    DependencyPropertyDescriptor desc = GetHasErrorDescriptor(dependencyObject);
                    desc.RemoveValueChanged(dependencyObject, OnHasErrorChanged);
                    SetHasErrorDescriptor(dependencyObject, null);
                }
            }

            return ret;
        }

        private static DependencyPropertyDescriptor GetHasErrorDescriptor(DependencyObject dependencyObject)
        {
            var ret = dependencyObject.GetValue(HasErrorDescriptorProperty);
            return ret as DependencyPropertyDescriptor;
        }

        private static void OnHasErrorChanged(object sender, EventArgs e)
        {
            var d = sender as DependencyObject;

            d?.SetValue(MvvmHasErrorProperty, d.GetValue(System.Windows.Controls.Validation.HasErrorProperty));
        }

        private static void SetHasErrorDescriptor(DependencyObject dependencyObject, DependencyPropertyDescriptor value)
        {
            var ret = dependencyObject.GetValue(HasErrorDescriptorProperty);
            dependencyObject.SetValue(HasErrorDescriptorProperty, value);
        }

        #endregion
    }
}
