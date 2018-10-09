using System.Collections.Generic;

namespace WindowsFormsApp2
{
    delegate void UnlockDelegate();
    delegate void BlockButtonsDelegate();

    internal class MyLock
    {
        char[] codeArr;
        private int counter = 3; //счетчик попыток
        public MyLock(string code)
        {
            codeArr = code.ToCharArray();
        }
        public event UnlockDelegate Unlock;
        public event BlockButtonsDelegate LockAll;
        List<char> inCode = new List<char>();

        /// <summary>
        /// Проверка правельности введенного символа
        /// </summary>
        /// <param name="code"></param>
        public void Check(char code)
        {
            inCode.Add(code);
            for (int i = 0; i < inCode.Count; i++)
            {
                if (codeArr[i] != inCode[i])
                {
                    inCode.Clear();
                    counter--;
                    if (counter == 0)
                    {
                        LockAll?.Invoke();
                    }
                    return;
                }
            }
            if (inCode.Count == codeArr.Length)
            {
                Unlock?.Invoke();
            }
            

        }
    }
}