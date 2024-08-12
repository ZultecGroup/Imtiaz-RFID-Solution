
namespace ZulLabel
{
    partial class LogsFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogsFrm));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.exportPDF = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.bunifuLabel1 = new Bunifu.UI.WinForms.BunifuLabel();
            this.detailLogGrid = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailLogGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel1.Controls.Add(this.exportPDF);
            this.panel1.Controls.Add(this.bunifuLabel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1221, 109);
            this.panel1.TabIndex = 23;
            // 
            // exportPDF
            // 
            this.exportPDF.AllowAnimations = true;
            this.exportPDF.AllowMouseEffects = true;
            this.exportPDF.AllowToggling = false;
            this.exportPDF.AnimationSpeed = 200;
            this.exportPDF.AutoGenerateColors = false;
            this.exportPDF.AutoRoundBorders = false;
            this.exportPDF.AutoSizeLeftIcon = true;
            this.exportPDF.AutoSizeRightIcon = true;
            this.exportPDF.BackColor = System.Drawing.Color.Transparent;
            this.exportPDF.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.exportPDF.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("exportPDF.BackgroundImage")));
            this.exportPDF.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.exportPDF.ButtonText = "bunifuButton1";
            this.exportPDF.ButtonTextMarginLeft = 0;
            this.exportPDF.ColorContrastOnClick = 45;
            this.exportPDF.ColorContrastOnHover = 45;
            this.exportPDF.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.exportPDF.CustomizableEdges = borderEdges1;
            this.exportPDF.DialogResult = System.Windows.Forms.DialogResult.None;
            this.exportPDF.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.exportPDF.DisabledFillColor = System.Drawing.Color.Empty;
            this.exportPDF.DisabledForecolor = System.Drawing.Color.Empty;
            this.exportPDF.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.exportPDF.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.exportPDF.ForeColor = System.Drawing.Color.White;
            this.exportPDF.IconLeft = null;
            this.exportPDF.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.exportPDF.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.exportPDF.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.exportPDF.IconMarginLeft = 11;
            this.exportPDF.IconPadding = 10;
            this.exportPDF.IconRight = null;
            this.exportPDF.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.exportPDF.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.exportPDF.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.exportPDF.IconSize = 25;
            this.exportPDF.IdleBorderColor = System.Drawing.Color.Empty;
            this.exportPDF.IdleBorderRadius = 0;
            this.exportPDF.IdleBorderThickness = 0;
            this.exportPDF.IdleFillColor = System.Drawing.Color.Empty;
            this.exportPDF.IdleIconLeftImage = null;
            this.exportPDF.IdleIconRightImage = null;
            this.exportPDF.IndicateFocus = false;
            this.exportPDF.Location = new System.Drawing.Point(1068, 64);
            this.exportPDF.Name = "exportPDF";
            this.exportPDF.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.exportPDF.OnDisabledState.BorderRadius = 1;
            this.exportPDF.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.exportPDF.OnDisabledState.BorderThickness = 1;
            this.exportPDF.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.exportPDF.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.exportPDF.OnDisabledState.IconLeftImage = null;
            this.exportPDF.OnDisabledState.IconRightImage = null;
            this.exportPDF.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.exportPDF.onHoverState.BorderRadius = 1;
            this.exportPDF.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.exportPDF.onHoverState.BorderThickness = 1;
            this.exportPDF.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this.exportPDF.onHoverState.ForeColor = System.Drawing.Color.White;
            this.exportPDF.onHoverState.IconLeftImage = null;
            this.exportPDF.onHoverState.IconRightImage = null;
            this.exportPDF.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.exportPDF.OnIdleState.BorderRadius = 1;
            this.exportPDF.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.exportPDF.OnIdleState.BorderThickness = 1;
            this.exportPDF.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.exportPDF.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.exportPDF.OnIdleState.IconLeftImage = null;
            this.exportPDF.OnIdleState.IconRightImage = null;
            this.exportPDF.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.exportPDF.OnPressedState.BorderRadius = 1;
            this.exportPDF.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.exportPDF.OnPressedState.BorderThickness = 1;
            this.exportPDF.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.exportPDF.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.exportPDF.OnPressedState.IconLeftImage = null;
            this.exportPDF.OnPressedState.IconRightImage = null;
            this.exportPDF.Size = new System.Drawing.Size(150, 39);
            this.exportPDF.TabIndex = 1;
            this.exportPDF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.exportPDF.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.exportPDF.TextMarginLeft = 0;
            this.exportPDF.TextPadding = new System.Windows.Forms.Padding(0);
            this.exportPDF.UseDefaultRadiusAndThickness = true;
            this.exportPDF.Visible = false;
            this.exportPDF.Click += new System.EventHandler(this.exportPDF_Click);
            // 
            // bunifuLabel1
            // 
            this.bunifuLabel1.AllowParentOverrides = false;
            this.bunifuLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuLabel1.AutoEllipsis = false;
            this.bunifuLabel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.CursorType = System.Windows.Forms.Cursors.Default;
            this.bunifuLabel1.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuLabel1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bunifuLabel1.Location = new System.Drawing.Point(591, 40);
            this.bunifuLabel1.Name = "bunifuLabel1";
            this.bunifuLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bunifuLabel1.Size = new System.Drawing.Size(178, 50);
            this.bunifuLabel1.TabIndex = 0;
            this.bunifuLabel1.Text = "Log Detail";
            this.bunifuLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.bunifuLabel1.TextFormat = Bunifu.UI.WinForms.BunifuLabel.TextFormattingOptions.Default;
            // 
            // detailLogGrid
            // 
            this.detailLogGrid.AllowCustomTheming = false;
            this.detailLogGrid.AllowUserToAddRows = false;
            this.detailLogGrid.AllowUserToDeleteRows = false;
            this.detailLogGrid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(223)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.detailLogGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.detailLogGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.detailLogGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.detailLogGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.detailLogGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.detailLogGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.detailLogGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(102)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.detailLogGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.detailLogGrid.ColumnHeadersHeight = 40;
            this.detailLogGrid.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(223)))));
            this.detailLogGrid.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.detailLogGrid.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.detailLogGrid.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(178)))));
            this.detailLogGrid.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.White;
            this.detailLogGrid.CurrentTheme.BackColor = System.Drawing.Color.Navy;
            this.detailLogGrid.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(216)))));
            this.detailLogGrid.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.Navy;
            this.detailLogGrid.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.detailLogGrid.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.detailLogGrid.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(102)))));
            this.detailLogGrid.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.detailLogGrid.CurrentTheme.Name = null;
            this.detailLogGrid.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(229)))));
            this.detailLogGrid.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.detailLogGrid.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.detailLogGrid.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(178)))));
            this.detailLogGrid.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(229)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(178)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.detailLogGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.detailLogGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailLogGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.detailLogGrid.EnableHeadersVisualStyles = false;
            this.detailLogGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(216)))));
            this.detailLogGrid.HeaderBackColor = System.Drawing.Color.Navy;
            this.detailLogGrid.HeaderBgColor = System.Drawing.Color.Empty;
            this.detailLogGrid.HeaderForeColor = System.Drawing.Color.White;
            this.detailLogGrid.Location = new System.Drawing.Point(0, 109);
            this.detailLogGrid.Name = "detailLogGrid";
            this.detailLogGrid.RowHeadersVisible = false;
            this.detailLogGrid.RowTemplate.Height = 40;
            this.detailLogGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.detailLogGrid.Size = new System.Drawing.Size(1221, 550);
            this.detailLogGrid.TabIndex = 25;
            this.detailLogGrid.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Navy;
            // 
            // LogsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 659);
            this.Controls.Add(this.detailLogGrid);
            this.Controls.Add(this.panel1);
            this.Name = "LogsFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogsFrm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LogsFrm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailLogGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Bunifu.UI.WinForms.BunifuLabel bunifuLabel1;
        private Bunifu.UI.WinForms.BunifuDataGridView detailLogGrid;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton exportPDF;
    }
}