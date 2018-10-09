using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class MyForm : Form
    {
        bool enableClosing = false;
        MyLock @lock = new MyLock("123456789");


        public MyForm()
        {
            @lock.Unlock += lock_Unlock;
            @lock.LockAll += BlockAllButtons;

            InitializeComponent();
        }
        /// <summary>
        /// Разблокирование сейфа
        /// </summary>
        private void lock_Unlock()
        {
            enableClosing = true;
            Close();
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            @lock.Check(b.Text[0]);
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (enableClosing) return;
            e.Cancel = true;
        }

        /// <summary>
        /// Блокировка всех кнопок
        /// </summary>
        private void BlockAllButtons()
        {
            foreach (Control control in this.Controls)
            {
                var but = control as Button;
                
                if (but == null) 
                    continue;

                but.Enabled = false;

            }
        }
    }
}
