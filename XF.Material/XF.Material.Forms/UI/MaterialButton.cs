﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using XF.Material.Forms.Effects;
using XF.Material.Forms.Resources;
using XF.Material.Forms.Resources.Typography;

namespace XF.Material.Forms.UI
{
    /// <summary>
    /// A control that allow users to take actions, and make choices, with a single tap.
    /// </summary>
    public class MaterialButton : Button, IMaterialButton
    {
        public const string MaterialButtonColorChanged = "BackgroundColorChanged";

        public static readonly BindableProperty AllCapsProperty = BindableProperty.Create(nameof(AllCaps), typeof(bool), typeof(MaterialButton), true);

        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(MaterialColor), typeof(MaterialButton), MaterialColor.Default);

        public static readonly BindableProperty ButtonTypeProperty = BindableProperty.Create(nameof(ButtonType), typeof(MaterialButtonType), typeof(MaterialButton), MaterialButtonType.Elevated, propertyChanged: ButtonTypeChanged);

        public MaterialButton()
        {
            this.SetValue(MaterialTypographyEffect.TypeScaleProperty, MaterialTypeScale.Button);
            this.SetDynamicResource(FontFamilyProperty, MaterialConstants.FontFamily.BUTTON);
            this.SetDynamicResource(FontSizeProperty, MaterialConstants.MATERIAL_FONTSIZE_BUTTON);
            this.SetDynamicResource(FontAttributesProperty, MaterialConstants.MATERIAL_FONTATTRIBUTE_BOLD);
            this.SetDynamicResource(CornerRadiusProperty, MaterialConstants.MATERIAL_BUTTON_CORNERRADIUS);
            this.SetDynamicResource(BackgroundColorProperty, MaterialConstants.Color.SECONDARY);
            this.SetDynamicResource(TextColorProperty, MaterialConstants.Color.ON_SECONDARY);
            this.SetDynamicResource(HeightRequestProperty, MaterialConstants.MATERIAL_BUTTON_HEIGHT);
        }

        /// <summary>
        /// Enumeration of the types of <see cref="MaterialButton"/>.
        /// </summary>
        public enum MaterialButtonType
        {
            /// <summary>
            /// This button will cast a shadow.
            /// </summary>
            Elevated = 1,

            /// <summary>
            /// This button will not cast a shadow.
            /// </summary>
            Flat = 2,

            /// <summary>
            /// This button will have a transparent background with a border.
            /// </summary>
            Outlined = 3,

            /// <summary>
            /// This button will have a transparent background and no border.
            /// </summary>
            Text = 4
        }

        /// <summary>
        /// Gets or sets whether the text of this button should be capitalized. The default value is true.
        /// </summary>
        public bool AllCaps
        {
            get => (bool)this.GetValue(AllCapsProperty);
            set => this.SetValue(AllCapsProperty, value);
        }

        /// <summary>
        /// Gets or sets the background color. The default value is based on the Color value of <see cref="MaterialColorConfiguration.Secondary"/> if you are using a Material resource, otherwise the default value is <see cref="Color.Accent"/>
        /// </summary>
        public new MaterialColor BackgroundColor
        {
            get => (MaterialColor)this.GetValue(BackgroundColorProperty);
            set => this.SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the type of this button. The default value is <see cref="MaterialButtonType.Elevated"/>
        /// </summary>
        public virtual MaterialButtonType ButtonType
        {
            get => (MaterialButtonType)this.GetValue(ButtonTypeProperty);
            set => this.SetValue(ButtonTypeProperty, value);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(this.BackgroundColor))
            {
                base.OnPropertyChanged(MaterialButton.MaterialButtonColorChanged);
            }

            else
            {
                base.OnPropertyChanged(propertyName);

                if (propertyName == nameof(this.Style))
                {
                    this.Style.Setters.ForEach(s => this.SetValue(s.Property, s.Value));
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void ButtonTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MaterialButton materialButton)
            {
                switch (materialButton.ButtonType)
                {
                    case MaterialButtonType.Text:
                        materialButton.RemoveDynamicResource(TextColorProperty);
                        materialButton.SetDynamicResource(TextColorProperty, MaterialConstants.Color.SECONDARY);
                        break;
                    case MaterialButtonType.Outlined:

                        if (materialButton.BorderColor == (Color)BorderColorProperty.DefaultValue)
                        {
                            materialButton.SetDynamicResource(BorderColorProperty, MaterialConstants.MATERIAL_BUTTON_OUTLINED_BORDERCOLOR);
                        }

                        if (materialButton.BorderWidth == (double)BorderWidthProperty.DefaultValue)
                        {
                            materialButton.SetDynamicResource(BorderWidthProperty, MaterialConstants.MATERIAL_BUTTON_OUTLINED_BORDERWIDTH);
                        }

                        materialButton.RemoveDynamicResource(TextColorProperty);
                        materialButton.SetDynamicResource(TextColorProperty, MaterialConstants.Color.SECONDARY);

                        break;
                }
            }
        }
    }
}
