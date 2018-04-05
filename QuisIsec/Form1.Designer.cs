namespace QuisIsec
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Pregunta = new System.Windows.Forms.Label();
            this.Resposta_0 = new System.Windows.Forms.Label();
            this.Resposta_1 = new System.Windows.Forms.Label();
            this.Resposta_2 = new System.Windows.Forms.Label();
            this.Resposta_3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Pregunta
            // 
            this.Pregunta.AutoSize = true;
            this.Pregunta.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Pregunta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pregunta.Location = new System.Drawing.Point(15, 10);
            this.Pregunta.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.Pregunta.Name = "Pregunta";
            this.Pregunta.Size = new System.Drawing.Size(770, 205);
            this.Pregunta.TabIndex = 0;
            this.Pregunta.Text = "Pregunta";
            this.Pregunta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Resposta_0
            // 
            this.Resposta_0.AutoSize = true;
            this.Resposta_0.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Resposta_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Resposta_0.Location = new System.Drawing.Point(15, 235);
            this.Resposta_0.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.Resposta_0.Name = "Resposta_0";
            this.Resposta_0.Size = new System.Drawing.Size(770, 36);
            this.Resposta_0.TabIndex = 1;
            this.Resposta_0.Text = "Resposta_0";
            this.Resposta_0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Resposta_0.Click += new System.EventHandler(this.label2_Click);
            // 
            // Resposta_1
            // 
            this.Resposta_1.AutoSize = true;
            this.Resposta_1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Resposta_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Resposta_1.Location = new System.Drawing.Point(15, 291);
            this.Resposta_1.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.Resposta_1.Name = "Resposta_1";
            this.Resposta_1.Size = new System.Drawing.Size(770, 36);
            this.Resposta_1.TabIndex = 2;
            this.Resposta_1.Text = "Resposta_1";
            this.Resposta_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Resposta_1.Click += new System.EventHandler(this.label3_Click);
            // 
            // Resposta_2
            // 
            this.Resposta_2.AutoSize = true;
            this.Resposta_2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Resposta_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Resposta_2.Location = new System.Drawing.Point(15, 347);
            this.Resposta_2.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.Resposta_2.Name = "Resposta_2";
            this.Resposta_2.Size = new System.Drawing.Size(770, 36);
            this.Resposta_2.TabIndex = 3;
            this.Resposta_2.Text = "Resposta_2";
            this.Resposta_2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Resposta_2.Click += new System.EventHandler(this.label4_Click);
            // 
            // Resposta_3
            // 
            this.Resposta_3.AutoSize = true;
            this.Resposta_3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Resposta_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Resposta_3.Location = new System.Drawing.Point(15, 403);
            this.Resposta_3.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.Resposta_3.Name = "Resposta_3";
            this.Resposta_3.Size = new System.Drawing.Size(770, 37);
            this.Resposta_3.TabIndex = 4;
            this.Resposta_3.Text = "Resposta_3";
            this.Resposta_3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Resposta_3.Click += new System.EventHandler(this.label5_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Pregunta, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Resposta_3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.Resposta_0, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Resposta_2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Resposta_1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Pregunta;
        private System.Windows.Forms.Label Resposta_0;
        private System.Windows.Forms.Label Resposta_1;
        private System.Windows.Forms.Label Resposta_2;
        private System.Windows.Forms.Label Resposta_3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

