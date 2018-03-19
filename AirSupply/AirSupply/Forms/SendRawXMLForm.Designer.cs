namespace AirSupply
{
    partial class SendRawXMLForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendRawXMLForm));
            this.lblInput = new System.Windows.Forms.Label();
            this.inputTxtBox = new System.Windows.Forms.RichTextBox();
            this.sendCommandButton = new System.Windows.Forms.Button();
            this.lblResponse = new System.Windows.Forms.Label();
            this.outputTxtBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInput.Location = new System.Drawing.Point(12, 9);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(83, 20);
            this.lblInput.TabIndex = 0;
            this.lblInput.Text = "XML Input";
            // 
            // inputTxtBox
            // 
            this.inputTxtBox.Location = new System.Drawing.Point(16, 32);
            this.inputTxtBox.Name = "inputTxtBox";
            this.inputTxtBox.Size = new System.Drawing.Size(920, 249);
            this.inputTxtBox.TabIndex = 1;
            this.inputTxtBox.Text = "";
            // 
            // sendCommandButton
            // 
            this.sendCommandButton.BackColor = System.Drawing.Color.Red;
            this.sendCommandButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendCommandButton.ForeColor = System.Drawing.Color.White;
            this.sendCommandButton.Location = new System.Drawing.Point(376, 298);
            this.sendCommandButton.Name = "sendCommandButton";
            this.sendCommandButton.Size = new System.Drawing.Size(225, 52);
            this.sendCommandButton.TabIndex = 2;
            this.sendCommandButton.Text = "Send Command";
            this.sendCommandButton.UseVisualStyleBackColor = false;
            this.sendCommandButton.Click += new System.EventHandler(this.sendCommandButton_Click);
            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResponse.Location = new System.Drawing.Point(12, 352);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(132, 20);
            this.lblResponse.TabIndex = 3;
            this.lblResponse.Text = "Server Response";
            // 
            // outputTxtBox
            // 
            this.outputTxtBox.Location = new System.Drawing.Point(16, 375);
            this.outputTxtBox.Name = "outputTxtBox";
            this.outputTxtBox.Size = new System.Drawing.Size(920, 216);
            this.outputTxtBox.TabIndex = 4;
            this.outputTxtBox.Text = "";
            // 
            // SendRawXMLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 603);
            this.Controls.Add(this.outputTxtBox);
            this.Controls.Add(this.lblResponse);
            this.Controls.Add(this.sendCommandButton);
            this.Controls.Add(this.inputTxtBox);
            this.Controls.Add(this.lblInput);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SendRawXMLForm";
            this.Text = "Send Raw XML";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.RichTextBox inputTxtBox;
        private System.Windows.Forms.Button sendCommandButton;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.RichTextBox outputTxtBox;
    }
}