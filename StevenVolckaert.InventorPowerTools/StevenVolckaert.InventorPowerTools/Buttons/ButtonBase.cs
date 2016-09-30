namespace StevenVolckaert.InventorPowerTools.Buttons
{
    using System;
    using System.Drawing;
    using System.Windows;
    using Inventor;

    /// <summary>
    /// Base class for Inventor buttons.
    /// </summary>
    internal abstract class ButtonBase : IDisposable
    {
        protected readonly ButtonDefinition _buttonDefinition;

        /// <summary>
        /// Gets the button's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the button's command type.
        /// </summary>
        public virtual CommandTypesEnum CommandType { get; } = CommandTypesEnum.kShapeEditCmdType;

        /// <summary>
        /// Gets the button's display name.
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Gets the button's display type.
        /// </summary>
        public virtual ButtonDisplayEnum DisplayType { get; } = ButtonDisplayEnum.kDisplayTextInLearningMode;

        /// <summary>
        /// Gets the button's description
        /// </summary>
        public virtual string Description
        {
            get { return DisplayName; }
        }

        /// <summary>
        /// Gets the button's tool tip.
        /// </summary>
        public virtual string ToolTip
        {
            get { return Description; }
        }

        /// <summary>
        /// Gets or sets the panel to which the button belongs.
        /// </summary>
        public RibbonPanel Panel { get; set; }

        protected static DrawingDocument DrawingDocument
        {
            get { return (DrawingDocument)AddIn.ActiveDocument; }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the button is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return _buttonDefinition.Enabled; }
            set { _buttonDefinition.Enabled = value; }
        }

        protected ButtonBase()
        {
            var typeName = GetType().Name;
            Name = "Autodesk:PowerTools:" + typeName;

            try
            {
                var standardIcon = ImageConvert.FromIconToIPictureDisp(
                    icon: new Icon(typeof(ButtonBase), typeName + ".ico")
                );

                _buttonDefinition =
                    AddIn.Inventor.CommandManager.ControlDefinitions.AddButtonDefinition(
                        DisplayName: DisplayName,
                        InternalName: Name,
                        Classification: CommandType,
                        ClientId: AddIn.ClientId,
                        DescriptionText: Description,
                        ToolTipText: ToolTip,
                        StandardIcon: standardIcon,
                        LargeIcon: standardIcon,
                        ButtonDisplay: DisplayType
                    );

                _buttonDefinition.OnExecute += OnExecute;

                IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Dispose()
        {
            if (_buttonDefinition != null)
                _buttonDefinition.OnExecute -= OnExecute;
        }

        protected abstract void OnExecute(NameValueMap context);

        /// <summary>
        /// Adds the button to a specified command bar.
        /// </summary>
        /// <param name="commandBar">The command bar to add the button to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="commandBar"/> is <c>null</c>.</exception>
        public void AddTo(CommandBar commandBar)
        {
            if (commandBar == null)
                throw new ArgumentNullException(nameof(commandBar));

            commandBar.Controls.AddButton(_buttonDefinition, Before: 0);
        }

        /// <summary>
        /// Adds the button to a specified ribbon panel.
        /// </summary>
        /// <param name="ribbonPanel">The ribbon panel.</param>
        public void AddTo(RibbonPanel ribbonPanel)
        {
            AddTo(ribbonPanel, targetControlName: string.Empty, insertBeforeTargetControl: false);
        }

        /// <summary>
        /// Adds the button to a specified ribbon panel.
        /// </summary>
        /// <param name="ribbonPanel">The ribbon panel.</param>
        /// <param name="targetControlName">The name of an existing control to position the button next to.</param>
        /// <param name="insertBeforeTargetControl">A value that indicates whether to position the button before or
        /// after the target control.</param>
        /// <exception cref="ArgumentNullException"><paramref name="ribbonPanel"/> or
        /// <paramref name="targetControlName"/> is <c>null</c>.</exception>
        public void AddTo(RibbonPanel ribbonPanel, string targetControlName, bool insertBeforeTargetControl)
        {
            if (ribbonPanel == null)
                throw new ArgumentNullException(nameof(ribbonPanel));

            if (targetControlName == null)
                throw new ArgumentNullException(nameof(targetControlName));

            ribbonPanel.CommandControls.AddButton(
                ButtonDefinition: _buttonDefinition,
                UseLargeIcon: true,
                ShowText: true,
                TargetControlInternalName: targetControlName,
                InsertBeforeTargetControl: insertBeforeTargetControl
            );
        }

        /// <summary>
        /// Adds the button to its panel.
        /// </summary>
        public virtual void AddToPanel()
        {
            if (Panel == null)
                return;

            try
            {
                AddTo(Panel);
            }
            catch
            {
                // The button already exists.
                // TODO Update it's buttondefinition.
            }
        }

        /// <summary>
        /// Adds the button to a specified command category. If the specified category doesn't exist, it is created.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <param name="displayName">The display name of the category, if it should be created.</param>
        public void AddToCommandCategory(string categoryName, string displayName)
        {
            try
            {
                var commandCategories = AddIn.Inventor.CommandManager.CommandCategories;

                var commandCategory =
                    commandCategories.TryGetValue(categoryName) ??
                    commandCategories.Add(displayName, categoryName, AddIn.ClientId);

                commandCategory.Add(_buttonDefinition);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected void ShowMessageBox(string message)
        {
            ShowMessageBox("{0}", message);
        }

        protected void ShowMessageBox(string messageFormat, params object[] messageArgs)
        {
            AddIn.ShowMessageBox(DisplayName, messageFormat, messageArgs);
        }

        protected void ShowWarningMessageBox(Exception exception)
        {
            ShowWarningMessageBox("{0}", exception.ToString());
        }

        protected void ShowWarningMessageBox(string messageFormat, params object[] messageArgs)
        {
            AddIn.ShowWarningMessageBox(DisplayName, messageFormat, messageArgs);
        }
    }
}
