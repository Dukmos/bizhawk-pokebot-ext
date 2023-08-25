namespace BizHawk.PokeBot.ext
{
    partial class PokeBot
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
            this.LB_GameName = new System.Windows.Forms.Label();
            this.GB_coords = new System.Windows.Forms.GroupBox();
            this.LB_Map = new System.Windows.Forms.Label();
            this.LB_XY = new System.Windows.Forms.Label();
            this.LB_State = new System.Windows.Forms.Label();
            this.CB_coords = new System.Windows.Forms.CheckBox();
            this.CB_EnableInput = new System.Windows.Forms.CheckBox();
            this.TB_BotInstanceID = new System.Windows.Forms.TextBox();
            this.LB_BotInstanceID = new System.Windows.Forms.Label();
            this.GB_coords.SuspendLayout();
            this.SuspendLayout();
            // 
            // LB_GameName
            // 
            this.LB_GameName.AutoSize = true;
            this.LB_GameName.Location = new System.Drawing.Point(12, 9);
            this.LB_GameName.Name = "LB_GameName";
            this.LB_GameName.Size = new System.Drawing.Size(41, 13);
            this.LB_GameName.TabIndex = 0;
            this.LB_GameName.Text = "Game: ";
            // 
            // GB_coords
            // 
            this.GB_coords.Controls.Add(this.LB_Map);
            this.GB_coords.Controls.Add(this.LB_XY);
            this.GB_coords.Controls.Add(this.LB_State);
            this.GB_coords.Location = new System.Drawing.Point(15, 91);
            this.GB_coords.Name = "GB_coords";
            this.GB_coords.Size = new System.Drawing.Size(125, 86);
            this.GB_coords.TabIndex = 1;
            this.GB_coords.TabStop = false;
            this.GB_coords.Visible = false;
            // 
            // LB_Map
            // 
            this.LB_Map.AutoSize = true;
            this.LB_Map.Location = new System.Drawing.Point(6, 60);
            this.LB_Map.Name = "LB_Map";
            this.LB_Map.Size = new System.Drawing.Size(34, 13);
            this.LB_Map.TabIndex = 2;
            this.LB_Map.Text = "Map: ";
            // 
            // LB_XY
            // 
            this.LB_XY.AutoSize = true;
            this.LB_XY.Location = new System.Drawing.Point(6, 38);
            this.LB_XY.Name = "LB_XY";
            this.LB_XY.Size = new System.Drawing.Size(33, 13);
            this.LB_XY.TabIndex = 1;
            this.LB_XY.Text = "X: Y: ";
            // 
            // LB_State
            // 
            this.LB_State.AutoSize = true;
            this.LB_State.Location = new System.Drawing.Point(6, 16);
            this.LB_State.Name = "LB_State";
            this.LB_State.Size = new System.Drawing.Size(38, 13);
            this.LB_State.TabIndex = 0;
            this.LB_State.Text = "State: ";
            // 
            // CB_coords
            // 
            this.CB_coords.AutoSize = true;
            this.CB_coords.Location = new System.Drawing.Point(12, 68);
            this.CB_coords.Name = "CB_coords";
            this.CB_coords.Size = new System.Drawing.Size(95, 17);
            this.CB_coords.TabIndex = 2;
            this.CB_coords.Text = "Enable Coords";
            this.CB_coords.UseVisualStyleBackColor = true;
            this.CB_coords.CheckedChanged += new System.EventHandler(this.CB_coords_CheckedChanged);
            // 
            // CB_EnableInput
            // 
            this.CB_EnableInput.AutoSize = true;
            this.CB_EnableInput.Location = new System.Drawing.Point(12, 25);
            this.CB_EnableInput.Name = "CB_EnableInput";
            this.CB_EnableInput.Size = new System.Drawing.Size(105, 17);
            this.CB_EnableInput.TabIndex = 3;
            this.CB_EnableInput.Text = "Enable Bot Input";
            this.CB_EnableInput.UseVisualStyleBackColor = true;
            // 
            // TB_BotInstanceID
            // 
            this.TB_BotInstanceID.Location = new System.Drawing.Point(93, 42);
            this.TB_BotInstanceID.Name = "TB_BotInstanceID";
            this.TB_BotInstanceID.Size = new System.Drawing.Size(100, 20);
            this.TB_BotInstanceID.TabIndex = 4;
            this.TB_BotInstanceID.Text = "pokebot_cs";
            // 
            // LB_BotInstanceID
            // 
            this.LB_BotInstanceID.AutoSize = true;
            this.LB_BotInstanceID.Location = new System.Drawing.Point(12, 45);
            this.LB_BotInstanceID.Name = "LB_BotInstanceID";
            this.LB_BotInstanceID.Size = new System.Drawing.Size(75, 13);
            this.LB_BotInstanceID.TabIndex = 5;
            this.LB_BotInstanceID.Text = "BotInstanceID";
            // 
            // PokeBot
            // 
            this.ClientSize = new System.Drawing.Size(278, 190);
            this.Controls.Add(this.LB_BotInstanceID);
            this.Controls.Add(this.TB_BotInstanceID);
            this.Controls.Add(this.CB_EnableInput);
            this.Controls.Add(this.CB_coords);
            this.Controls.Add(this.GB_coords);
            this.Controls.Add(this.LB_GameName);
            this.Name = "PokeBot";
            this.GB_coords.ResumeLayout(false);
            this.GB_coords.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LB_GameName;
        private System.Windows.Forms.GroupBox GB_coords;
        private System.Windows.Forms.CheckBox CB_coords;
        private System.Windows.Forms.Label LB_Map;
        private System.Windows.Forms.Label LB_XY;
        private System.Windows.Forms.Label LB_State;
        private System.Windows.Forms.CheckBox CB_EnableInput;
        private System.Windows.Forms.TextBox TB_BotInstanceID;
        private System.Windows.Forms.Label LB_BotInstanceID;
    }
}