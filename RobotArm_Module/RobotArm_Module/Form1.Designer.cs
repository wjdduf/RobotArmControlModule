
namespace RobotArm_Module
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.MoveToFront = new System.Windows.Forms.Button();
            this.MoveToBack = new System.Windows.Forms.Button();
            this.PowerOn = new System.Windows.Forms.Button();
            this.PowerOff = new System.Windows.Forms.Button();
            this.MoveToBack2 = new System.Windows.Forms.Button();
            this.MoveToFront2 = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // MoveToFront
            // 
            this.MoveToFront.Location = new System.Drawing.Point(479, 12);
            this.MoveToFront.Name = "MoveToFront";
            this.MoveToFront.Size = new System.Drawing.Size(300, 102);
            this.MoveToFront.TabIndex = 0;
            this.MoveToFront.Text = "앞";
            this.MoveToFront.UseVisualStyleBackColor = true;
            this.MoveToFront.Click += new System.EventHandler(this.MoveToFront_Click);
            // 
            // MoveToBack
            // 
            this.MoveToBack.Location = new System.Drawing.Point(479, 120);
            this.MoveToBack.Name = "MoveToBack";
            this.MoveToBack.Size = new System.Drawing.Size(300, 92);
            this.MoveToBack.TabIndex = 1;
            this.MoveToBack.Text = "뒤";
            this.MoveToBack.UseVisualStyleBackColor = true;
            this.MoveToBack.Click += new System.EventHandler(this.MoveToBack_Click);
            // 
            // PowerOn
            // 
            this.PowerOn.Location = new System.Drawing.Point(28, 12);
            this.PowerOn.Name = "PowerOn";
            this.PowerOn.Size = new System.Drawing.Size(300, 200);
            this.PowerOn.TabIndex = 2;
            this.PowerOn.Text = "연결";
            this.PowerOn.UseVisualStyleBackColor = true;
            this.PowerOn.Click += new System.EventHandler(this.PowerOn_Click);
            // 
            // PowerOff
            // 
            this.PowerOff.Location = new System.Drawing.Point(28, 225);
            this.PowerOff.Name = "PowerOff";
            this.PowerOff.Size = new System.Drawing.Size(300, 200);
            this.PowerOff.TabIndex = 3;
            this.PowerOff.Text = "종료";
            this.PowerOff.UseVisualStyleBackColor = true;
            this.PowerOff.Click += new System.EventHandler(this.PowerOff_Click);
            // 
            // MoveToBack2
            // 
            this.MoveToBack2.Location = new System.Drawing.Point(479, 333);
            this.MoveToBack2.Name = "MoveToBack2";
            this.MoveToBack2.Size = new System.Drawing.Size(300, 92);
            this.MoveToBack2.TabIndex = 5;
            this.MoveToBack2.Text = "뒤";
            this.MoveToBack2.UseVisualStyleBackColor = true;
            this.MoveToBack2.Click += new System.EventHandler(this.MoveToBack_Click2);
            // 
            // MoveToFront2
            // 
            this.MoveToFront2.Location = new System.Drawing.Point(479, 225);
            this.MoveToFront2.Name = "MoveToFront2";
            this.MoveToFront2.Size = new System.Drawing.Size(300, 102);
            this.MoveToFront2.TabIndex = 4;
            this.MoveToFront2.Text = "앞";
            this.MoveToFront2.UseVisualStyleBackColor = true;
            this.MoveToFront2.Click += new System.EventHandler(this.MoveToFront2_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MoveToBack2);
            this.Controls.Add(this.MoveToFront2);
            this.Controls.Add(this.PowerOff);
            this.Controls.Add(this.PowerOn);
            this.Controls.Add(this.MoveToBack);
            this.Controls.Add(this.MoveToFront);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button MoveToFront;
        private System.Windows.Forms.Button MoveToBack;
        private System.Windows.Forms.Button PowerOn;
        private System.Windows.Forms.Button PowerOff;
        private System.Windows.Forms.Button MoveToBack2;
        private System.Windows.Forms.Button MoveToFront2;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}

