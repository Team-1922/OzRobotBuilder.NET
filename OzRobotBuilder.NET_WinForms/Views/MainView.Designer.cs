namespace OzRobotBuilder.NET.Views
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.robotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subsystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joystickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.HierarchialView = new System.Windows.Forms.TreeView();
            this.SelectedNodeEditor = new System.Windows.Forms.DataGridView();
            this.ValName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RobotContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SubsystemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addSubsystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addJoystickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPWMMotorControllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTalonSRXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addAnalogInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addQuadratureEncoderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDigitalInputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SelectedNodeEditor)).BeginInit();
            this.RobotContextMenu.SuspendLayout();
            this.SubsystemContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.robotToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(934, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator2,
            this.openToolStripMenuItem,
            this.importToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // robotToolStripMenuItem
            // 
            this.robotToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem});
            this.robotToolStripMenuItem.Name = "robotToolStripMenuItem";
            this.robotToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.robotToolStripMenuItem.Text = "Robot";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subsystemToolStripMenuItem,
            this.commandToolStripMenuItem,
            this.joystickToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // subsystemToolStripMenuItem
            // 
            this.subsystemToolStripMenuItem.Name = "subsystemToolStripMenuItem";
            this.subsystemToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.subsystemToolStripMenuItem.Text = "Subsystem";
            this.subsystemToolStripMenuItem.Click += new System.EventHandler(this.subsystemToolStripMenuItem_Click);
            // 
            // commandToolStripMenuItem
            // 
            this.commandToolStripMenuItem.Name = "commandToolStripMenuItem";
            this.commandToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.commandToolStripMenuItem.Text = "Command";
            this.commandToolStripMenuItem.Click += new System.EventHandler(this.commandToolStripMenuItem_Click);
            // 
            // joystickToolStripMenuItem
            // 
            this.joystickToolStripMenuItem.Name = "joystickToolStripMenuItem";
            this.joystickToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.joystickToolStripMenuItem.Text = "Joystick";
            this.joystickToolStripMenuItem.Click += new System.EventHandler(this.joystickToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.HierarchialView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.SelectedNodeEditor);
            this.splitContainer1.Size = new System.Drawing.Size(934, 503);
            this.splitContainer1.SplitterDistance = 311;
            this.splitContainer1.TabIndex = 2;
            // 
            // HierarchialView
            // 
            this.HierarchialView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HierarchialView.Location = new System.Drawing.Point(0, 0);
            this.HierarchialView.Name = "HierarchialView";
            this.HierarchialView.Size = new System.Drawing.Size(311, 503);
            this.HierarchialView.TabIndex = 0;
            this.HierarchialView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.HierarchialView_BeforeSelect);
            this.HierarchialView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.HierarchialView_AfterSelect);
            this.HierarchialView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.HierarchialView_NodeMouseClick);
            // 
            // SelectedNodeEditor
            // 
            this.SelectedNodeEditor.AllowUserToAddRows = false;
            this.SelectedNodeEditor.AllowUserToDeleteRows = false;
            this.SelectedNodeEditor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SelectedNodeEditor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ValName,
            this.Value});
            this.SelectedNodeEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedNodeEditor.Location = new System.Drawing.Point(0, 0);
            this.SelectedNodeEditor.Name = "SelectedNodeEditor";
            this.SelectedNodeEditor.Size = new System.Drawing.Size(619, 503);
            this.SelectedNodeEditor.TabIndex = 0;
            this.SelectedNodeEditor.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SelectedNodeEditor_CellContentDoubleClick);
            this.SelectedNodeEditor.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.SelectedNodeEditor_CellEndEdit);
            // 
            // ValName
            // 
            this.ValName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ValName.HeaderText = "Name";
            this.ValName.Name = "ValName";
            this.ValName.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // RobotContextMenu
            // 
            this.RobotContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSubsystemToolStripMenuItem,
            this.addCommandToolStripMenuItem,
            this.addJoystickToolStripMenuItem});
            this.RobotContextMenu.Name = "RobotContextMenu";
            this.RobotContextMenu.Size = new System.Drawing.Size(157, 70);
            this.RobotContextMenu.Tag = "";
            // 
            // SubsystemContextMenu
            // 
            this.SubsystemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPWMMotorControllerToolStripMenuItem,
            this.addTalonSRXToolStripMenuItem,
            this.addAnalogInputToolStripMenuItem,
            this.addQuadratureEncoderToolStripMenuItem,
            this.addDigitalInputToolStripMenuItem});
            this.SubsystemContextMenu.Name = "SubsystemContextMenu";
            this.SubsystemContextMenu.Size = new System.Drawing.Size(221, 136);
            // 
            // addSubsystemToolStripMenuItem
            // 
            this.addSubsystemToolStripMenuItem.Name = "addSubsystemToolStripMenuItem";
            this.addSubsystemToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.addSubsystemToolStripMenuItem.Text = "Add Subsystem";
            this.addSubsystemToolStripMenuItem.Click += new System.EventHandler(this.subsystemToolStripMenuItem_Click);
            // 
            // addCommandToolStripMenuItem
            // 
            this.addCommandToolStripMenuItem.Name = "addCommandToolStripMenuItem";
            this.addCommandToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.addCommandToolStripMenuItem.Text = "Add Command";
            this.addCommandToolStripMenuItem.Click += new System.EventHandler(this.commandToolStripMenuItem_Click);
            // 
            // addJoystickToolStripMenuItem
            // 
            this.addJoystickToolStripMenuItem.Name = "addJoystickToolStripMenuItem";
            this.addJoystickToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.addJoystickToolStripMenuItem.Text = "Add Joystick";
            this.addJoystickToolStripMenuItem.Click += new System.EventHandler(this.joystickToolStripMenuItem_Click);
            // 
            // addPWMMotorControllerToolStripMenuItem
            // 
            this.addPWMMotorControllerToolStripMenuItem.Name = "addPWMMotorControllerToolStripMenuItem";
            this.addPWMMotorControllerToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.addPWMMotorControllerToolStripMenuItem.Text = "Add PWM Motor Controller";
            this.addPWMMotorControllerToolStripMenuItem.Click += new System.EventHandler(this.addPWMMotorControllerToolStripMenuItem_Click);
            // 
            // addTalonSRXToolStripMenuItem
            // 
            this.addTalonSRXToolStripMenuItem.Name = "addTalonSRXToolStripMenuItem";
            this.addTalonSRXToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.addTalonSRXToolStripMenuItem.Text = "Add TalonSRX";
            this.addTalonSRXToolStripMenuItem.Click += new System.EventHandler(this.addTalonSRXToolStripMenuItem_Click);
            // 
            // addAnalogInputToolStripMenuItem
            // 
            this.addAnalogInputToolStripMenuItem.Name = "addAnalogInputToolStripMenuItem";
            this.addAnalogInputToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.addAnalogInputToolStripMenuItem.Text = "Add Analog Input";
            this.addAnalogInputToolStripMenuItem.Click += new System.EventHandler(this.addAnalogInputToolStripMenuItem_Click);
            // 
            // addQuadratureEncoderToolStripMenuItem
            // 
            this.addQuadratureEncoderToolStripMenuItem.Name = "addQuadratureEncoderToolStripMenuItem";
            this.addQuadratureEncoderToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.addQuadratureEncoderToolStripMenuItem.Text = "Add Quadrature Encoder";
            this.addQuadratureEncoderToolStripMenuItem.Click += new System.EventHandler(this.addQuadratureEncoderToolStripMenuItem_Click);
            // 
            // addDigitalInputToolStripMenuItem
            // 
            this.addDigitalInputToolStripMenuItem.Name = "addDigitalInputToolStripMenuItem";
            this.addDigitalInputToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.addDigitalInputToolStripMenuItem.Text = "Add Digital Input";
            this.addDigitalInputToolStripMenuItem.Click += new System.EventHandler(this.addDigitalInputToolStripMenuItem_Click);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 527);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainView";
            this.Text = "OzRobotBuilder.NET";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SelectedNodeEditor)).EndInit();
            this.RobotContextMenu.ResumeLayout(false);
            this.SubsystemContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView HierarchialView;
        private System.Windows.Forms.DataGridView SelectedNodeEditor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.ToolStripMenuItem robotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subsystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joystickToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip RobotContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addSubsystemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCommandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addJoystickToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip SubsystemContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addPWMMotorControllerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTalonSRXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addAnalogInputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addQuadratureEncoderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDigitalInputToolStripMenuItem;
    }
}